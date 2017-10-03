using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OnDemandSync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int file_count = 0;
            if (true)//textBox1.Text.Length == 6)
            {
                string config_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\directory_map_config.txt";
                if (File.Exists(config_path))
                {
                    var text = File.ReadAllText(config_path);
                    string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    bool syncPRTdone = false;
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("#"))
                        {
                            string[] info = line.Substring(1).Split(new string[] { "->" }, StringSplitOptions.None);
                            string[] source_files = Directory.GetFiles(info[0].Trim() + "\\", "*" + textBox1.Text + "*");

                            if (!syncPRTdone)
                            {
                                Sync_Customer_File(info[0].Trim(), info[1].Trim(), textBox1.Text.Trim());
                                syncPRTdone = true;
                            }

                            if (source_files.Count() > 0)
                            {

                                foreach (string path in source_files)
                                {
                                    string file_name = Path.GetFileName(path);
                                    File.Copy(path, info[1].Trim() + "\\" + file_name, true);
                                    richTextBox1.AppendText(path + Environment.NewLine);
                                    file_count++;
                                }
                            }
                        }
                        else if (line.Contains("TITLE"))
                        {
                            string[] info = line.Substring(1).Split(new string[] { "=" }, StringSplitOptions.None);
                            this.Text = info[1].Trim();
                        }

                    }
                }
            }
            MessageBox.Show(file_count > 0 ? file_count.ToString() + " file(s) have been transferred" : "No files were found");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.All(char.IsDigit))
            {
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string config_path = Directory.GetCurrentDirectory() + "\\directory_map_config.txt";
            string config_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\directory_map_config.txt";
            if (File.Exists(config_path))
            {
                var text = File.ReadAllText(config_path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Contains("TITLE"))
                    {
                        string[] info = line.Substring(1).Split(new string[] { "=" }, StringSplitOptions.None);
                        this.Text = info[1];
                    }
                }
            }
        }

        List<string> Plant_IP = new List<string>()
        {
            @"\\10.0.0.8\rdrive\cust\",
            @"\\192.168.1.22\rdrive\cust\",
            @"\\192.168.12.22\rdrive\cust\",
            @"\\192.168.101.22\rdrive\cust\",
            @"\\192.168.16.22\rdrive\cust\"
        };

        ExcoODBC database2 = ExcoODBC.Instance;


        string table_name = "JobPlantDefine"; // Main table query name

        public static string Source_Plant_Name = "";

        private bool Sync_Customer_File(string sourceIP, string destination_plant_IP, string ordernumber)
        {
            string customer_code = "";
            string source_path = "";
            string file_name = "";

            if (sourceIP.Contains("10.0.0.8")) Source_Plant_Name = "MARKHAM";
            else if (sourceIP.Contains("192.168.1.22")) Source_Plant_Name = "MICHIGAN";
            else if (sourceIP.Contains("192.168.12.22")) Source_Plant_Name = "TEXAS";
            else if (sourceIP.Contains("192.168.101.22")) Source_Plant_Name = "COLOMBIA";
            else if (sourceIP.Contains("192.168.16.22")) Source_Plant_Name = "BRAZIL";

            if (destination_plant_IP.Contains("10.0.0.8")) destination_plant_IP = "MARKHAM";
            else if (destination_plant_IP.Contains("192.168.1.22")) destination_plant_IP = "MICHIGAN";
            else if (destination_plant_IP.Contains("192.168.12.22")) destination_plant_IP = "TEXAS";
            else if (destination_plant_IP.Contains("192.168.101.22")) destination_plant_IP = "COLOMBIA";
            else if (destination_plant_IP.Contains("192.168.16.22")) destination_plant_IP = "BRAZIL";

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
                richTextBox1.AppendText("No R/CUST CUTSTOCK.prt files" + Environment.NewLine);
                Console.WriteLine("No R/CUST CUTSTOCK.prt files");
            }
            else if (customer_code.Length < 4) return false;


            Console.WriteLine("Syncing R/CUST CUTSTOCK .prt files");

            if (Source_Plant_Name == "MARKHAM") source_path = Plant_IP[0];
            if (Source_Plant_Name == "MICHIGAN") source_path = Plant_IP[1];
            if (Source_Plant_Name == "TEXAS") source_path = Plant_IP[2];
            if (Source_Plant_Name == "COLOMBIA") source_path = Plant_IP[3];
            if (Source_Plant_Name == "BRAZIL") source_path = Plant_IP[4];

            if (destination_plant_IP == "MARKHAM") destination_plant_IP = Plant_IP[0];
            if (destination_plant_IP == "MICHIGAN") destination_plant_IP = Plant_IP[1];
            if (destination_plant_IP == "TEXAS") destination_plant_IP = Plant_IP[2];
            if (destination_plant_IP == "COLOMBIA") destination_plant_IP = Plant_IP[3];
            if (destination_plant_IP == "BRAZIL") destination_plant_IP = Plant_IP[4];

            try
            {
                Console.WriteLine(source_path + customer_code + "\\" + file_name + ".prt  " + " TO " + destination_plant_IP + file_name + ".prt");
                File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_plant_IP + customer_code + "\\" + file_name + ".prt", true);
                // File.Copy(source_path + customer_code + "\\" + file_name + "_model5x.prt", destination_path + file_name + "_model5x.prt", true);
            }
            catch (Exception e)
            {
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
                richTextBox1.AppendText("No R/CUST PROGRAM .prt files" + Environment.NewLine);
                Console.WriteLine("No R/CUST PROGRAM .prt files");
                return false;
            }
            if (customer_code.Length < 4) return false;

            richTextBox1.AppendText("Syncing R/CUST PROGRAM .prt files" + Environment.NewLine);
            Console.WriteLine("Syncing R/CUST PROGRAM .prt files");

            if (Source_Plant_Name == "MARKHAM") source_path = Plant_IP[0];
            if (Source_Plant_Name == "MICHIGAN") source_path = Plant_IP[1];
            if (Source_Plant_Name == "TEXAS") source_path = Plant_IP[2];
            if (Source_Plant_Name == "COLOMBIA") source_path = Plant_IP[3];
            if (Source_Plant_Name == "BRAZIL") source_path = Plant_IP[4];

            if (destination_plant_IP == "MARKHAM") destination_plant_IP = Plant_IP[0];
            if (destination_plant_IP == "MICHIGAN") destination_plant_IP = Plant_IP[1];
            if (destination_plant_IP == "TEXAS") destination_plant_IP = Plant_IP[2];
            if (destination_plant_IP == "COLOMBIA") destination_plant_IP = Plant_IP[3];
            if (destination_plant_IP == "BRAZIL") destination_plant_IP = Plant_IP[4];

            try
            {
                Console.WriteLine(source_path + customer_code + "\\" + file_name + ".prt" + "TO " + destination_plant_IP + file_name + ".prt");
                File.Copy(source_path + customer_code + "\\" + file_name + ".prt", destination_plant_IP + customer_code + "\\" + file_name + ".prt", true);
                // File.Copy(source_path + customer_code + "\\" + file_name + "_model5x.prt", destination_path + file_name + "_model5x.prt", true);
            }
            catch (Exception e)
            {
                richTextBox1.AppendText("R/CUST COPY ERROR:" + Environment.NewLine);
                Console.WriteLine(@"R/CUST COPY ERROR: " + e.ToString());
            }

            return true;
        }
    }
}
