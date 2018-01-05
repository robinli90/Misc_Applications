using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Databases;
using System.IO;
using System.Data.Odbc;
using System.Net;
using System.Net.Mail;

namespace FileSyncTool
{
    class Program
    {
        Dictionary<string, string> markhamPlantCustMap = new Dictionary<string, string>() // source, destination
        {
            {"9005", "9000"}, 
            {"9010", "9000"}, 
            {"16350", "16300"}, 
        };

        public string GetCustPlantMap(string custCode)
        {
            if (markhamPlantCustMap.ContainsKey(custCode)) return markhamPlantCustMap[custCode];

            return custCode;
        }
            
        List<string> Plant_IP = new List<string>()
        {
            @"\\10.0.0.8\rdrive\cust\",
            @"\\192.168.1.23\rdrive\cust\",
            @"\\192.168.12.22\rdrive\cust\",
            @"\\192.168.101.22\rdrive\cust\",
            @"\\192.168.16.22\rdrive\cust\"
        };
            
        List<string> _5AxisPlantIP = new List<string>()
        {
            @"\\10.0.0.8\shopdata\curjobs\mill5axis\",
            @"\\192.168.1.23\curjobs\curjobs\mill5axis\",
            @"\\192.168.12.22\curjobs\curjobs\mill5axis\",
            @"\\192.168.101.22\curjobs\curjobs\mill5axis\",
            @"\\192.168.16.22\curjobs\curjobs\mill5axis\"
        };
            
        List<string> _5AxisToollistPlantIP = new List<string>()
        {
            @"\\10.0.0.8\shopdata\curjobs\Toollist5Axis\",
            @"\\192.168.1.23\curjobs\curjobs\Toollist5Axis\",
            @"\\192.168.12.22\curjobs\curjobs\Toollist5Axis\",
            @"\\192.168.101.22\curjobs\curjobs\Toollist5Axis\",
            @"\\192.168.16.22\curjobs\curjobs\Toollist5Axis\"
        };

        string table_name = "JobPlantDefine"; // Main table query name

        public static string Source_Plant_Name = "";

        static void Main(string[] args)
        {
            {
                // Application
                if (args.Length == 1)
                {
                    Source_Plant_Name = args[0];
                    Program p = new Program();
                    p.Run();
                }
            }
        }

        public void WriteLog(string msg)
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FileSyncLoc_" + Source_Plant_Name + ".txt");
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.Write(Environment.NewLine + "[" + DateTime.Now + "] - " + msg);
                    sw.Close();
                }
            }
            catch
            {
                // Save failed
            }
        }

        List<string> Lines = new List<string>();

        private void Run()
        {
            try
            {
                Dictionary<string, string> Order_Numbers = Get_Jobs_To_Sync();

                Console.WriteLine("Checking Database.........");

                // If has order numbers, sync
                foreach (KeyValuePair<string, string> Entry in Order_Numbers)
                {
                    Sync_Customer_File(Entry.Value, Entry.Key);
                    Sync5Axis(Entry.Key);

                    Console.WriteLine("Finding files for " + Entry.Key);

                    bool error = false;

                    string config_path = Directory.GetCurrentDirectory() + "\\directory_map_config.txt";
                    if (File.Exists(config_path))
                    {
                        var text = File.ReadAllText(config_path);
                        string[] lines = text.Split(new string[] {Environment.NewLine}, StringSplitOptions.None);

                        foreach (string line in lines)
                        {
                            if (line.Trim().StartsWith("#"))
                            {
                                string[] info = line.Trim().Substring(1)
                                    .Split(new string[] {"->"}, StringSplitOptions.None);

                                string Destination_Plant = info[0].Split(new string[] {":"}, StringSplitOptions.None)[1]
                                    .Trim();

                                if (Destination_Plant == Entry.Value)
                                {
                                    try
                                    {
                                        string temp = info[1].Trim() + "\\";
                                        string temp2 = "*" + Entry.Key + "*";
                                        string[] source_files = Directory.GetFiles(temp, temp2);
                                        if (source_files.Count() > 0)
                                        {
                                            Console.WriteLine("Files found for " + Entry.Key);

                                            foreach (string path in source_files)
                                            {
                                                try
                                                {
                                                    if (Directory.Exists(info[2].Trim() + "\\"))
                                                    {
                                                        Console.WriteLine("Copying File: " + path);
                                                        Console.WriteLine("Destination Path: " + info[2]);
                                                        string file_name = Path.GetFileName(path);
                                                        File.Copy(path, info[2].Trim() + "\\" + file_name, true);
                                                        WriteLog("Copying file..... " + path + " ----> " + info[2]);
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Transfer error..... trying next file");
                                                    WriteLog("Transfer error..... trying next file");
                                                    error = true;
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Invalid path/directory detected: " + info[1].Trim());
                                        WriteLog("Invalid path/directory detected: " + info[1].Trim());
                                        error = true;
                                    }
                                }
                            }
                        }
                    }
                    
                    if (!error)
                    {
                        Console.WriteLine("Updating Database........");

                        if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
                        if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
                        if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
                        if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
                        if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

                        // Update
                        string query = "update [tiger].[dbo].[" + table_name +
                                       "] set flag = '0' where ordernumber = '" + Entry.Key +
                                       "' and manufacturesite = '" + Entry.Value + "'";
                        OdbcDataReader reader2;
                        reader2 = database2.RunQuery(query);
                        reader2.Close();

                        if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
                        if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
                        if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
                        if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
                        if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

                        // Update
                        query = "update [tiger].[dbo].[" + table_name +
                                       "] set flag5x = '0' where ordernumber = '" + Entry.Key +
                                       "' and manufacturesite = '" + Entry.Value + "'";
                        reader2 = database2.RunQuery(query);
                        reader2.Close();

                        WriteLog("Updating database for ordernumber = " + Entry.Key);
                    }
                }
            }
            catch
            {}
        }

        ExcoODBC database2 = ExcoODBC.Instance;

        private Dictionary<string, string> Get_Jobs_To_Sync()
        {
            _5AxisFileList = new Dictionary<string, string>();

            Dictionary<string, string> Return_String = new Dictionary<string, string>();

            // Get appropriate database
            if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
            if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
            if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
            if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
            if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

            string query = "select * from [tiger].[dbo].[" + table_name + "] where flag = '1' or flag5x = '3'";
            OdbcDataReader reader2;

            reader2 = database2.RunQuery(query);
            while (reader2.Read())
            {
                Return_String.Add(reader2[0].ToString().Trim(), reader2[1].ToString().Trim());

                // Save 5 axis locations
                if (reader2[7].ToString().Trim() == "3" && reader2[6].ToString().Trim().Length > 2)
                {
                    _5AxisFileList.Add(reader2[0].ToString().Trim(), reader2[6].ToString().Trim());
                }
            }
            reader2.Close();
                
            return Return_String;
        }

        private Dictionary<string, string> _5AxisFileList = new Dictionary<string, string>();


        string source_path = "";
        string destination_path = "";

        private bool Sync_Customer_File(string destination_plant, string ordernumber)
        {
            string customer_code = "";
            string file_name = "";

            // Get appropriate database
            if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
            if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
            if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
            if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
            if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

            // CUTSTOCKFILENAME
            string query = "select * from [tiger].[dbo].[CAM_order] where ordernumber = '" + ordernumber + "'";
            OdbcDataReader reader2;

            reader2 = database2.RunQuery(query);
            while (reader2.Read())
            {
                customer_code = reader2[3].ToString().Trim();
                file_name = reader2[4].ToString().Trim();
            }
            reader2.Close();

            if (file_name.Length < 4)
            {
                WriteLog("[NO FILE NAME] No R/CUST CUTSTOCK .prt file exists for " + ordernumber + " filename: " + file_name);
                Console.WriteLine("No R/CUST CUTSTOCK.prt files");
            }
            else if (customer_code.Length < 4) return false;


            Console.WriteLine("Syncing R/CUST CUTSTOCK .prt files");

            if (Source_Plant_Name == "MARKHAM") source_path = Plant_IP[0];
            if (Source_Plant_Name == "MICHIGAN") source_path = Plant_IP[1];
            if (Source_Plant_Name == "TEXAS") source_path = Plant_IP[2];
            if (Source_Plant_Name == "COLOMBIA") source_path = Plant_IP[3];
            if (Source_Plant_Name == "BRAZIL") source_path = Plant_IP[4];

            if (destination_plant == "MARKHAM") destination_path = Plant_IP[0];
            if (destination_plant == "MICHIGAN") destination_path = Plant_IP[1];
            if (destination_plant == "TEXAS") destination_path = Plant_IP[2];
            if (destination_plant == "COLOMBIA") destination_path = Plant_IP[3];
            if (destination_plant == "BRAZIL") destination_path = Plant_IP[4];

            try
            {
                WriteLog("Syncing r/cust for ordernumber = " + ordernumber);
                Console.WriteLine(source_path + customer_code + "\\" + file_name + ".prt  " + " TO " + destination_path + file_name + ".prt");
                File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_path + GetCustPlantMap(customer_code) + "\\" + file_name + ".prt", true);
                
                // Copy program to other plant
                if (_5AxisFileList.ContainsKey(ordernumber))
                {
                    destination_plant = _5AxisFileList[ordernumber];
                    if (destination_plant == "MARKHAM") destination_path = Plant_IP[0];
                    if (destination_plant == "MICHIGAN") destination_path = Plant_IP[1];
                    if (destination_plant == "TEXAS") destination_path = Plant_IP[2];
                    if (destination_plant == "COLOMBIA") destination_path = Plant_IP[3];
                    if (destination_plant == "BRAZIL") destination_path = Plant_IP[4];
                    File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_path + GetCustPlantMap(customer_code) + "\\" + file_name + ".prt", true);
                }
                // File.Copy(source_path + customer_code + "\\" + file_name + "_model5x.prt", destination_path + file_name + "_model5x.prt", true);
            }
            catch (Exception e)
            {
                SendErrorEmail(ordernumber);
                Console.WriteLine(@"#1 R/CUST COPY ERROR: " + ordernumber + " " + e.ToString());
                WriteLog("Syncing error.... " + e.ToString());
            }

            // FILE NAME PRT

            query = "select * from [tiger].[dbo].[CAM_order] where ordernumber = '" + ordernumber + "'";

            reader2 = database2.RunQuery(query);
            while (reader2.Read())
            {
                customer_code = reader2[3].ToString().Trim();
                file_name = reader2[1].ToString().Trim();
            }
            reader2.Close();

            if (file_name.Length < 4)
            {
                WriteLog("[NO FILE NAME] No R/CUST PROGRAM .prt file exists for " + ordernumber);
                Console.WriteLine("No R/CUST PROGRAM .prt files");
                return false;
            }
            if (customer_code.Length < 4) return false;

            Console.WriteLine("Syncing R/CUST PROGRAM .prt files");

            if (Source_Plant_Name == "MARKHAM") source_path = Plant_IP[0];
            if (Source_Plant_Name == "MICHIGAN") source_path = Plant_IP[1];
            if (Source_Plant_Name == "TEXAS") source_path = Plant_IP[2];
            if (Source_Plant_Name == "COLOMBIA") source_path = Plant_IP[3];
            if (Source_Plant_Name == "BRAZIL") source_path = Plant_IP[4];

            if (destination_plant == "MARKHAM") destination_path = Plant_IP[0];
            if (destination_plant == "MICHIGAN") destination_path = Plant_IP[1];
            if (destination_plant == "TEXAS") destination_path = Plant_IP[2];
            if (destination_plant == "COLOMBIA") destination_path = Plant_IP[3];
            if (destination_plant == "BRAZIL") destination_path = Plant_IP[4];

            try
            {
                Console.WriteLine(source_path + customer_code + "\\" + file_name + ".prt" + "TO " + destination_path + file_name + ".prt");
                File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_path + GetCustPlantMap(customer_code) + "\\" + file_name + ".prt", true);

                // Copy program to other plant
                if (_5AxisFileList.ContainsKey(ordernumber))
                {
                    destination_plant = _5AxisFileList[ordernumber];
                    if (destination_plant == "MARKHAM") destination_path = Plant_IP[0];
                    if (destination_plant == "MICHIGAN") destination_path = Plant_IP[1];
                    if (destination_plant == "TEXAS") destination_path = Plant_IP[2];
                    if (destination_plant == "COLOMBIA") destination_path = Plant_IP[3];
                    if (destination_plant == "BRAZIL") destination_path = Plant_IP[4];
                    File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_path + GetCustPlantMap(customer_code) + "\\" + file_name + ".prt", true);
                }
            }
            catch (Exception e)
            {
                SendErrorEmail(ordernumber);
                WriteLog("#2 Copy error for r/cust for ordernumber = " + ordernumber);
                Console.WriteLine(@"R/CUST COPY ERROR: " + e.ToString());
            }

            return true;
        }


        private void Sync5Axis(string orderNumber)
        {
            if (!_5AxisFileList.ContainsKey(orderNumber)) return;

            OdbcDataReader reader2;

            // Get appropriate database
            if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
            if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
            if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
            if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
            if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

            // CUTSTOCKFILENAME
            string query = "select * from [tiger].[dbo].[jobplantdefine] where ordernumber = '" + orderNumber + "' and flag5x = '3'";

            reader2 = database2.RunQuery(query);
            while (reader2.Read())
            {
                try
                {
                    string destination_plant = reader2[1].ToString().Trim();
                    string Source_Plant_Name = reader2[6].ToString().Trim();

                    if (Source_Plant_Name == "MARKHAM") source_path = _5AxisPlantIP[0];
                    if (Source_Plant_Name == "MICHIGAN") source_path = _5AxisPlantIP[1];
                    if (Source_Plant_Name == "TEXAS") source_path = _5AxisPlantIP[2];
                    if (Source_Plant_Name == "COLOMBIA") source_path = _5AxisPlantIP[3];
                    if (Source_Plant_Name == "BRAZIL") source_path = _5AxisPlantIP[4];

                    if (destination_plant == "MARKHAM") destination_path = _5AxisPlantIP[0];
                    if (destination_plant == "MICHIGAN") destination_path = _5AxisPlantIP[1];
                    if (destination_plant == "TEXAS") destination_path = _5AxisPlantIP[2];
                    if (destination_plant == "COLOMBIA") destination_path = _5AxisPlantIP[3];
                    if (destination_plant == "BRAZIL") destination_path = _5AxisPlantIP[4];

                    File.Copy(source_path + orderNumber + "_1.h", destination_path + orderNumber + "_1.h", true);

                    // Copy tool list file
                    if (Source_Plant_Name == "MARKHAM") source_path = _5AxisToollistPlantIP[0];
                    if (Source_Plant_Name == "MICHIGAN") source_path = _5AxisToollistPlantIP[1];
                    if (Source_Plant_Name == "TEXAS") source_path = _5AxisToollistPlantIP[2];
                    if (Source_Plant_Name == "COLOMBIA") source_path = _5AxisToollistPlantIP[3];
                    if (Source_Plant_Name == "BRAZIL") source_path = _5AxisToollistPlantIP[4];

                    if (destination_plant == "MARKHAM") destination_path = _5AxisToollistPlantIP[0];
                    if (destination_plant == "MICHIGAN") destination_path = _5AxisToollistPlantIP[1];
                    if (destination_plant == "TEXAS") destination_path = _5AxisToollistPlantIP[2];
                    if (destination_plant == "COLOMBIA") destination_path = _5AxisToollistPlantIP[3];
                    if (destination_plant == "BRAZIL") destination_path = _5AxisToollistPlantIP[4];

                    File.Copy(source_path + orderNumber + "_1.h", destination_path + orderNumber + "_1.h", true);
                }
                catch (Exception e)
                {
                }
            }

            reader2.Close();

            // Update flag
            if (Source_Plant_Name == "MARKHAM") database2.Open(Database.DECADE_MARKHAM);
            if (Source_Plant_Name == "MICHIGAN") database2.Open(Database.DECADE_MICHIGAN);
            if (Source_Plant_Name == "TEXAS") database2.Open(Database.DECADE_TEXAS);
            if (Source_Plant_Name == "COLOMBIA") database2.Open(Database.DECADE_COLOMBIA);
            if (Source_Plant_Name == "BRAZIL") database2.Open(Database.DECADE_BRAZIL);

            // Update
            query = "update [tiger].[dbo].[" + table_name +
                           "] set flag5x = '0' where ordernumber = '" + orderNumber +
                           "' and programsite5x = '" + Source_Plant_Name + "'";
            reader2 = database2.RunQuery(query);
            reader2.Close();
        }


        private void SendErrorEmail(string orderNo)
        {

            MailMessage mailmsg = new MailMessage();
            MailAddress from = new MailAddress("gluo@etsdies.com");
            mailmsg.From = from;
            mailmsg.To.Add("gluo@etsdies.com");
            mailmsg.Subject = "File Sync /r/cust error!";
            mailmsg.Body = "File does not exist for '" + orderNo + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "This is an automated message. Please do not reply to this email. " + Environment.NewLine +
                           Environment.NewLine;
            // smtp client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential credential = new NetworkCredential("gluo@etsdies.com", "5Zh2P8k4@2");
            client.Credentials = credential;
            //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            Task.Run(() => { client.Send(mailmsg); });

        }
    }
}

