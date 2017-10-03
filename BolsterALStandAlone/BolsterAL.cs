using System;
using Databases;
using System.Data.Odbc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace BolsterALStandAlone
{
    public partial class BolsterAL : Form
    {
        private string[] File_Array;
        private string[] bolsters = {"INSERT", "BOLSTER", "INSERT BOLSTER"};
        private string[] sub_Bolster = {"SHIM", "SPACER", "SUB", "SUB-BOLSTER"};
        private bool running = false;
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();
        private int running_count = 0;
        private bool _AUTO_BIG_LATHE = false;

        public BolsterAL()
        {
            InitializeComponent();
            pictureBox1.Show();
            tick_update.Interval = 20000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(check_folder);
            string query = "update d_active set BLS_AL_active = '1' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_TEXAS);
            reader = database.RunQuery(query);
            reader.Close();
            running = true;
            buffer_text.AppendText("===================================\r\n");
            buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
            buffer_text.AppendText("===================================\r\n");
            start_button.Visible = false;

            // Check if log file exists
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\bolster_err_log.txt"))
            {
                using (StreamWriter sw = File.CreateText(Directory.GetCurrentDirectory() + "\\bolster_err_log.txt")) 
                {
                    sw.Write("");
                    sw.Close();
                }
            }
        }

        private void Append_To_Log(string txt)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\bolster_err_log.txt"))
            {
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\bolster_err_log.txt", Environment.NewLine + txt);
            }
        }

        private void check_folder(object sender, EventArgs e)
        {
            if (running)
            {
                Retrieve_File_List("\\\\192.168.12.22\\curjobs\\TURN\\BOL\\");
                Process_Files();
            }
        }

        private void Retrieve_File_List(string path)
        {
            File_Array = Directory.GetFiles(path);
        }

        private void Process_Files()
        {
            foreach (string file_path in File_Array)
            {
                // REPORT REMOVAL //_parent.Main_Update_Transition_Data("PROCESSOR_BOLSTER_AUTOLATHE", (_parent.Get_Transition_Data_Value("PROCESSOR_BOLSTER_AUTOLATHE") + 1).ToString(), true);
                running_count++;
                try
                {
                    buffer_text.AppendText("Processing file: " + file_path.Substring(file_path.Length - 10, 10) + "\r\n");
                    Append_To_Log("Processing file: " + file_path.Substring(file_path.Length - 10, 10));
                    var text = File.ReadAllText(file_path);
                    string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string bolster_type = lines[0].Trim();
                    string customer_code = lines[1].Trim();
                    string hollow_dia = "0;";

                    try
                    {
                        hollow_dia = lines[3].Trim();
                    }
                    catch { }
                    if (customer_code.Length == 4)
                    {
                        customer_code = "0" + customer_code;
                    }
                    string bolster_size = "";
                    foreach (char c in lines[2])
                    {
                        if (!(c.ToString() == " "))
                        {
                            bolster_size = bolster_size + c.ToString();
                        }
                    }
                    string query = "";
                    string SO_number = Path.GetFileName(file_path).Substring(0, 6);
                    string target_path = "\\\\192.168.12.22\\curjobs\\LDATA\\" + SO_number + "LDCRV.txt";
                    string log_path = "\\\\192.168.12.22\\curjobs\\TURN\\BOL\\LOG\\" + SO_number + ".log";
                    //string COL_log_path = "V:\\TURN\\BOL\\BOL_MODEL\\" + SO_number + ".txt";
                    //string BR_log_path = "W:\\TURN\\BOL\\BOL_MODEL\\" + SO_number + ".txt";
                    string error_path = "\\\\192.168.12.22\\curjobs\\TURN\\BOL\\BOL_ERROR\\" + SO_number + ".txt";
                    bool valid_chamfer_size = true;
                    try
                    {
                        if (bolsters.Contains(bolster_type)) // If bolster
                        {
                            buffer_text.AppendText("     -> Bolster found\r\n");
                            query = "select * from d_autolathebo where customercode = '" + customer_code + "' and bolsize = '" + bolster_size + "'";
                        }
                        else if (sub_Bolster.Contains(bolster_type))
                        {
                            buffer_text.AppendText("     -> Sub Bolster found\r\n");
                            query = "select * from d_autolathebo_sub where customercode = '" + customer_code + "' and bolsize = '" + bolster_size + "'";
                        }
                        /*
                        else if (hollow_dia == "0" || hollow_dia == "0.0")
                        {
                            buffer_text.AppendText("     -> (Sub)-Bolster found\r\n");
                            buffer_text.AppendText("     -> Error: Not turned because hollow value = 0\r\n");
                            //buffer_text.AppendText("     -> since 4/16/2015 \r\n");
                        }*/

                        ExcoODBC database = ExcoODBC.Instance;
                        OdbcDataReader reader;
                        database.Open(Database.DECADE_MARKHAM);
                        reader = database.RunQuery(query);
                        reader.Read();
                        if (reader["std"].ToString() == "1")
                        {
                            string[] dimension = new string[5];
                            if (bolsters.Contains(bolster_type)) // If bolster
                            {
                                dimension = calculate_dimensions(
                                                        reader[2].ToString().Trim(),
                                                        reader[3].ToString().Trim(),
                                                        reader[6].ToString().Trim(),
                                                        reader[7].ToString().Trim(),
                                                        reader[8].ToString().Trim(),
                                                        reader[9].ToString().Trim(),
                                                        hollow_dia //"0" // No hollow dia
                                                    );
                            }
                            else if (sub_Bolster.Contains(bolster_type))
                            {
                                dimension = calculate_dimensions(
                                                        reader[2].ToString().Trim(),
                                                        reader[3].ToString().Trim(),
                                                        reader[6].ToString().Trim(),
                                                        reader[7].ToString().Trim(),
                                                        reader[8].ToString().Trim(),
                                                        reader[9].ToString().Trim(),
                                                        hollow_dia //reader[10].ToString().Trim()
                                                    );
                            }

                            // Gidda 9/30 do not process chamfers with size greater than 5mm
                            if (Convert.ToDouble(reader[6]) > 5 || Convert.ToDouble(reader[7]) > 5)
                            {
                                // Gidda 11/09 do not process if chamfer > 5 and diameter less than 400
                                // Gidda 11/26/2015 Do not process if chamfer angle greater than 30 deg
                                if (Convert.ToDouble(reader[2].ToString().Trim()) < 399 && Convert.ToDouble(reader[8].ToString().Trim()) > 30)
                                    valid_chamfer_size = false;
                            }


                            // If dia > 400mm and thk > 160, automark BIG LATHE // Gidda 10/16/2015
                            if ((Convert.ToDouble(reader[2].ToString()) > 392 && (Convert.ToDouble(reader[3].ToString()) > 160)))
                            {
                                _AUTO_BIG_LATHE = true;
                            }
                            else
                            {
                                _AUTO_BIG_LATHE = false;
                            }

                            reader.Close();
                            buffer_text.AppendText("     -> Processing...\r\n");
                            buffer_text.AppendText("     -> Customer: " + customer_code + "   Bolster: " + bolster_size + "\r\n");
                            Append_To_Log("     -> Customer: " + customer_code + "   Bolster: " + bolster_size);
                            buffer_text.AppendText("     -> Bolster Hollow Diameter: " + hollow_dia + "\r\n");
                            if (!File.Exists(target_path) && (valid_chamfer_size))
                            {
                                string processor = process_to_output(SO_number, dimension);
                                using (StreamWriter sw = File.CreateText(target_path)) // Create LDATA file
                                {
                                    sw.Write(processor);
                                    sw.Close();
                                }
                                using (StreamWriter sw = File.CreateText(log_path)) // Create LOG file
                                {
                                    sw.Write(bolster_type + "\r\n" + customer_code + "\r\n" + bolster_size + "\r\n" + "Hollow Diameter: " + hollow_dia + "\r\n\r\n" + processor);
                                    sw.Close();
                                }
                                // TRANSFER TO BRZ/COL
                                try
                                {

                                    //using (StreamWriter sw = File.CreateText(COL_log_path)) // Create LOG file
                                    //{
                                    //    sw.Write(bolster_type + "\r\n" + customer_code + "\r\n" + bolster_size + "\r\n" + "Hollow Diameter: " + hollow_dia + "\r\n\r\n" + processor);
                                    //    sw.Close();
                                    //}

                                    //using (StreamWriter sw = File.CreateText(BR_log_path)) // Create LOG file
                                    //{
                                    //    sw.Write(bolster_type + "\r\n" + customer_code + "\r\n" + bolster_size + "\r\n" + "Hollow Diameter: " + hollow_dia + "\r\n\r\n" + processor);
                                    //    sw.Close();
                                    //}
                                }
                                catch
                                {
                                }

                            }
                            else
                            {
                                buffer_text.AppendText("      -> Does not fit Gidda's criteria for automation. Please manually program");
                                Append_To_Log("      -> Does not fit Gidda's criteria for automation. Please manually program");
                            }
                        }
                        else
                        {
                            buffer_text.AppendText("     -> Invalid Chamfer dimension, file not processed!\r\n");
                            Append_To_Log("     -> Invalid Chamfer dimension, file not processed!\r\n");
                        }
                        buffer_text.AppendText("     -> Finished!\r\n");
                        Append_To_Log("     -> Finished!\r\n");
                    }
                    catch
                    {
                        try
                        {
                            File.Delete(error_path);
                            File.Copy(file_path, error_path);
                            buffer_text.AppendText("     -> *** Error on file, invalid information ***\r\n");
                            buffer_text.AppendText("     -> Customer: " + customer_code + "   Bolster: " + bolster_size + "\r\n");
                            Append_To_Log("     -> Customer: " + customer_code + "   Bolster: " + bolster_size);
                            AlertBox alert = new AlertBox("Invalid bolster error on file: " + file_path, "");
                            Append_To_Log("Invalid bolster error on file: " + file_path);
                            alert.HideButton();
                            alert.Show();
                        }
                        catch { }
                    }
                    buffer_text.SelectionStart = buffer_text.Text.Length;
                    buffer_text.ScrollToCaret();
                    File.Delete(file_path);
                    count_text.Text = "Files processed: " + running_count.ToString();
                    count_text.Refresh();


                    // TRANSFER TO BRZ/COL
                    try
                    {

                        //using (StreamWriter sw = File.CreateText(COL_log_path)) // Create LOG file
                        //{
                        //    sw.Write(bolster_type + "\r\n" + customer_code + "\r\n" + bolster_size + "\r\n" + "Hollow Diameter: " + hollow_dia );
                        //    sw.Close();
                        //}

                        //using (StreamWriter sw = File.CreateText(BR_log_path)) // Create LOG file
                        //{
                        //    sw.Write(bolster_type + "\r\n" + customer_code + "\r\n" + bolster_size + "\r\n" + "Hollow Diameter: " + hollow_dia);
                        //    sw.Close();
                        //}
                    }
                    catch
                    {
                    }
                }
                catch { }
            }

        }

        // Get the line dimensions given parameters
        private string[] calculate_dimensions(string dia, string thk, string frontc, string backc, string frontangle, string backangle, string hollowdia)
        {
            string[] perimeter = new string[5];
            double calc_dia = Convert.ToDouble(dia) / 2;
            double calc_thk = Convert.ToDouble(thk);
            double calc_frontc = Convert.ToDouble(frontc);
            double calc_backc = Convert.ToDouble(backc);
            double calc_frontangle = Convert.ToDouble(frontangle);
            double calc_backangle = Convert.ToDouble(backangle);
            double calc_frontcwidth = calc_frontc*Math.Tan((calc_frontangle*Math.PI)/180.0);
            double calc_backcwidth = calc_backc*Math.Tan((calc_backangle*Math.PI)/180.0);
            double calc_hollowdia = Convert.ToDouble(hollowdia) / 2;
            string horizon = "0.00000000 " + format_decimal(calc_hollowdia.ToString()) + " 0.00000000";  //added
            string point_A = "0.00000000 " + format_decimal((calc_dia - calc_frontc).ToString()) + " 0.00000000";
            string point_B = format_decimal((-calc_frontcwidth).ToString()) + " " + format_decimal(calc_dia.ToString()) + " 0.00000000";
            string point_C = format_decimal((-calc_thk + calc_backcwidth).ToString()) + " " + format_decimal(calc_dia.ToString()) + " 0.00000000";
            string point_D = format_decimal((-calc_thk).ToString()) + " " + format_decimal((calc_dia - calc_backc).ToString()) + " 0.00000000";
            string point_E = format_decimal((-calc_thk).ToString()) + " " + format_decimal((calc_hollowdia).ToString()) + " 0.00000000"; //added
            perimeter[0] = horizon + "  " + point_A;
            perimeter[1] = point_A + "  " + point_B;
            perimeter[2] = point_B + "  " + point_C;
            perimeter[3] = point_C + "  " + point_D;
            perimeter[4] = point_D + "  " + point_E;
            return perimeter;
        } 

        // Format 8 decimal places at end of decimal
        private string format_decimal(string @decimal)
        {
            string decimal_temp = @decimal;
            if (!@decimal.Contains(".")) decimal_temp = @decimal + ".0";
            if (decimal_temp == "0" || decimal_temp == "0.0") return "0.00000000";
            bool decimal_found = false;
            string leading_string = "";  string trailing_string = ""; string return_string = "";
            for (int i = 0; i < decimal_temp.Length; i++)
            {
                if (decimal_temp[i].ToString() == ".") decimal_found = true;
                if (decimal_found)
                {
                    trailing_string = trailing_string + decimal_temp[i].ToString();
                }
                else
                {
                    leading_string = leading_string + decimal_temp[i].ToString();
                }
            }
            for (int i = 0; i < 9 - trailing_string.Length; i++)
            {
                return_string = return_string + "0";
            }
            return leading_string + trailing_string + return_string;
        }

        private string process_to_output(string shop_number, string[] dimensions) 
        {
            string return_string = "";
            if (shop_number[0].ToString() == "1" && shop_number[1].ToString() == "0" ) return_string += "BRAZIL\r\n";
            else if (shop_number[0].ToString() == "1") return_string += "COLOMBIA\r\n";
            else if (_AUTO_BIG_LATHE) return_string += "BIGLATHE\r\n";
            return_string += "SO#:" + shop_number + "\r\n";
            return_string += "DUE DATE:\r\n";
            return_string += "DIE TYPE:HOLLOW\r\n";
            return_string += "BOLCRV\r\n";
            return_string += "CURVE\r\n";
            return_string += "STEPDEP=0.00000000\r\n";
            return_string += "STEPBACKDEP=0.00000000\r\n";
            return_string += "LINE " + dimensions[0] + "\r\n";
            return_string += "LINE " + dimensions[1] + "\r\n";
            return_string += "LINE " + dimensions[2] + "\r\n";
            return_string += "LINE " + dimensions[3] + "\r\n";
            return_string += "LINE " + dimensions[4] + "\r\n";
            return_string += "ENDCURVE\r\n";
            return_string += "END\r\n";
            return return_string;
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            string query = "update d_active set BLS_AL_active = '0' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            // REPORT REMOVAL //_parent.bolster_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                running = true;
                buffer_text.AppendText("===================================\r\n");
                buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
                buffer_text.AppendText("===================================\r\n");
                start_button.Visible = false;
                stop_button.Visible = true;
                pictureBox1.Enabled = true;

                string query = "update d_active set BLS_AL_active = '1' where employeenumber = '10577'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (running) 
            {
                running = false;
                buffer_text.AppendText("===================================\r\n");
                buffer_text.AppendText("  Thread stopped at: " + DateTime.Now.ToString() + "\r\n");
                buffer_text.AppendText("===================================\r\n");
                start_button.Visible = true;
                stop_button.Visible = false;
                pictureBox1.Enabled = false;

                string query = "update d_active set BLS_AL_active = '0' where employeenumber = '10577'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
        }

        private void BolsterAL_Load(object sender, EventArgs e)
        {

        }
    }
}
