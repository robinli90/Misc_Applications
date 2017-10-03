using System;
using Databases;
using System.Data.Odbc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace Monitor
{
    public partial class Main : Form
    {

        Report_Processor report = new Report_Processor();
        public Thread thread;
        public Process process = new Process();
        static List<string> File_List = new List<string>();
        public string SO_number_print = "~";
        public string google_search_string = "";
        public string employee_box_name;
        List<string> alerted_people = new List<string>();
        bool steel_in_box = false; bool messenger_box = false; bool pager_box = false; bool bolster_box = false; bool cad_print_box = false; bool turn_check_box = false;
        bool task_tracker_box = false; bool on_hold_box = false; bool admin_task_box = false; bool statistics_box = false;
        string[] prev_so_filelist;
        int ticks = 20;
        private double non_zero_ping_average = 0.1;
        private int non_zero_ticks = 0;
        private static int ping_tolerance = 5; // Max tolerated ping ms before recording to log
        Random rnd = new Random();
        int last_checked = 0;
        int last_ldata_check = 0;

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        

        public Main(string name)
        {
            InitializeComponent();
            //report.Process_Transitional_Data();
            thread = new Thread(Monitor_Folders);
            thread.IsBackground = true;
            thread.Start();
            this.employee_box_name = name;

            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 20;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            for (int i = 0; i < 21; i++)
            {
                chart1.Series["Series1"].Points.AddXY(i, 1);
                chart1.Series["Series2"].Points.AddXY(i, 2);
                chart1.Series["Series3"].Points.AddXY(i, 1.5);
            }



        }


        #region Form Window Optimization/Functionalities
        // When you unminimize window, seen conversation of current person
        protected virtual void this_OnActivated(object sender, EventArgs e)
        {
            alerted_people.Clear();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            string query = "update d_active set active = '0' where employeenumber = '" + employee_box_name + "'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();

            if (cad_print_box)
            {
                query = "update d_active set CAD_Print_processor_active = '0' where employeenumber = '10577'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }

            if (bolster_box) 
            {
                query = "update d_active set BLS_AL_active = '0' where employeenumber = '10577'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }

            if (turn_check_box)
            {
                query = "update d_active set turn_checker_active = '0' where employeenumber = '10577'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }

            if (task_tracker_box)
            {
                query = "update d_active set task_tracker_active = '0' where employeenumber = '10577'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }

            if (on_hold_box)
            {
                query = "update d_active set on_hold_active = '0' where employeenumber = '10577'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }

            this.Visible = false;
            this.Close();
            Environment.Exit(0);
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        

        // #########################################################################################################################
        // ##############################################[ Folder Monitoring & BG Tasks ]###########################################
        // #########################################################################################################################
        #region FOLDER MONITORING
        int p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0, p6 = 0, p7 = 0, p8 = 0, p9 = 0, p10 = 0;

        // Count number of files in path
        private int count_files(string path)
        {
            //
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            int count = dir.GetFiles().Length * 5;
            if (count < 100) return count;
            return 100;
        }

        // Update each progress bar with the number of files (progressive; each file = 5x value)
        internal void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), new object[] { progress });
                return;
            }

            ticks++;

            if (ticks < 30)
            {
                /*string g = "select partno, unit_price, unit, date, plant, C.JRDES1 as DESCRIPTION, C.JRVND# from " +
                  "(select a.KBVPT# as PartNO, a.KBUPRC as UNIT_Price, a.KBPUNT as UNIT, a.KBRDAT as " +
                  "DATE, a.KBPLNT as PLANT from cmsdat.poi as a) as A, cmsdat.poptvn as c where partno=c.jrvpt# and partno <> '' and " + "upper(c.jrdes1) like upper('%" + "" + "%')  and plant = '" + "001" + "' " +
                  "and date between " + "'2011-04-04'" + " and " + "'2016-04-04'" + " order by plant asc";*/
                //Console.WriteLine(g);
            }

            if (ticks % 239 == 0)
            {

                // REPORT REMOVAL //report.Log_update();
                if (Convert.ToInt32(DateTime.Now.Hour) == 23 && Convert.ToInt32(DateTime.Now.Minute) > 55)
                {
                    Console.WriteLine("###Clearing###");
                    // REPORT REMOVAL //report.Clear_Report_Entries();
                }
            }

            if (ticks % 500 == 0)
            {
                Retrieve_Files("\\\\10.0.0.8\\shopdata\\sonum\\");
                execute_prevso();
                Console.WriteLine("Tick tick");
                last_ldata_check = 0;
            }

            if (ticks % 10000 == 0)
            {
                last_checked = 0;
            }

            if (ticks > last_checked && p2 > 40)
            {
                string date_string = "";
                if (DateTime.Now.Minute.ToString().Length == 1)
                {
                    date_string = "[" + DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString() + "]";
                }
                else
                {
                    date_string = "[" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "]";
                }
                // REPORT REMOVAL //Main_Update_Transition_Data("LDATA_QUEUE_ERROR", date_string, false);
                last_checked = last_checked + 3600;
            }

            /*
            if (ticks % 5 == 0 || ticks == 21)
            {
            
                p1 = count_files("\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\");
             * */
            p2 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\");
            if (ticks % 40 == 0 && p2 > 30 && last_ldata_check == 0)
            //if (p2 > 30)
            {
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                last_ldata_check = 1;
                string query = "insert into internalpaging_old (FromEmp, FromF, FromL, ToEmp, ToF, ToL, Msg, Pagingtime, Status, RepStatus, Important, ForceToReply) values" +
                    " (" + "'10403'" + ", '" + "PAGER" + "', '" + ",''" + "', " + "10577" + ", '" + "Robin" + "', '" + "" + "', '"
                    + "LDATA QUEUE ERROR. CHECK SANGKOO MACHINE TO SEE IF IT STILL PROCESSING" + "', '" + DateTime.Now.ToString() + "', 1, 0, " + "0" + ", 0)";

                reader = database.RunQuery(query);
                reader.Close();

                database.Open(Database.DECADE_MARKHAM);
                last_ldata_check = 1;
                query = "insert into internalpaging_old (FromEmp, FromF, FromL, ToEmp, ToF, ToL, Msg, Pagingtime, Status, RepStatus, Important, ForceToReply) values" +
                    " (" + "'10403'" + ", '" + "PAGER" + "', '" + ",''" + "', " + "10403" + ", '" + "Gary" + "', '" + "" + "', '"
                    + "LDATA QUEUE ERROR. CHECK SANGKOO MACHINE TO SEE IF IT STILL PROCESSING" + "', '" + DateTime.Now.ToString() + "', 1, 0, " + "0" + ", 0)";

                reader = database.RunQuery(query);
                reader.Close();


            }
            /*
                p3 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\");
                p4 = count_files("\\\\10.0.0.8\\shopdata\\TURN\\BOL\\");
                p5 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\PLOT_PRINT\\");
                p6 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\A4_PRINT\\");
                p7 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\LATHEDWG\\");
                p8 = count_files("\\\\10.0.0.8\\shopdata\\Development\\Robin\\TEST\\");
                p9 = count_files("\\\\10.0.0.8\\shopdata\\LDATA\\COLOMBIA\\DATACOPY\\");
                p10 = count_files("\\\\FILESRV\\SDRIVE\\LDATAFROMCOLOMBIA\\");

                progressBar1.Value = p1;
                progressBar2.Value = p2;
                progressBar3.Value = p3;
                progressBar4.Value = p4;
                progressBar5.Value = p5;
                progressBar6.Value = p6;
                progressBar7.Value = p7;
                progressBar8.Value = p8;
                progressBar9.Value = p9;
                progressBar10.Value = p10;


                #region blue bars
                progressBar1.ForeColor = System.Drawing.Color.Cyan;
                progressBar2.ForeColor = System.Drawing.Color.Cyan;
                progressBar3.ForeColor = System.Drawing.Color.Cyan;
                progressBar4.ForeColor = System.Drawing.Color.Cyan;
                progressBar5.ForeColor = System.Drawing.Color.Cyan;
                progressBar6.ForeColor = System.Drawing.Color.Cyan;
                progressBar7.ForeColor = System.Drawing.Color.Cyan;
                progressBar8.ForeColor = System.Drawing.Color.Cyan;
                progressBar9.ForeColor = System.Drawing.Color.Cyan;
                progressBar10.ForeColor = System.Drawing.Color.Cyan;
                #endregion
                #region yellow bars
                if (p1 > 40) progressBar1.ForeColor = System.Drawing.Color.Yellow;
                if (p2 > 40) progressBar2.ForeColor = System.Drawing.Color.Yellow;
                if (p3 > 40) progressBar3.ForeColor = System.Drawing.Color.Yellow;
                if (p4 > 40) progressBar4.ForeColor = System.Drawing.Color.Yellow;
                if (p5 > 40) progressBar5.ForeColor = System.Drawing.Color.Yellow;
                if (p6 > 40) progressBar6.ForeColor = System.Drawing.Color.Yellow;
                if (p7 > 40) progressBar7.ForeColor = System.Drawing.Color.Yellow;
                if (p8 > 40) progressBar8.ForeColor = System.Drawing.Color.Yellow;
                if (p9 > 40) progressBar9.ForeColor = System.Drawing.Color.Yellow;
                if (p10 > 40) progressBar10.ForeColor = System.Drawing.Color.Yellow;
                #endregion
                #region red bars
                if (p1 > 65) progressBar1.ForeColor = System.Drawing.Color.Red;
                if (p2 > 65) progressBar2.ForeColor = System.Drawing.Color.Red;
                if (p3 > 65) progressBar3.ForeColor = System.Drawing.Color.Red;
                if (p4 > 65) progressBar4.ForeColor = System.Drawing.Color.Red;
                if (p5 > 65) progressBar5.ForeColor = System.Drawing.Color.Red;
                if (p6 > 65) progressBar6.ForeColor = System.Drawing.Color.Red;
                if (p7 > 65) progressBar7.ForeColor = System.Drawing.Color.Red;
                if (p8 > 65) progressBar8.ForeColor = System.Drawing.Color.Red;
                if (p9 > 65) progressBar9.ForeColor = System.Drawing.Color.Red;
                if (p10 > 65) progressBar10.ForeColor = System.Drawing.Color.Red;
                #endregion
            }*/

            if (false)//(ticks % 2000 == 0 || ticks == 22) //20 seconds
            {
                // Alert for unread messages
                if (has_unread_messages(this.employee_box_name))
                {
                    //FlashWindowEx(this);
                }

                // Check if still active remotely
                string query = "select * from d_active where employeenumber = '" + employee_box_name + "' and active = '0'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString());
                    AlertBox LA = new AlertBox("Logged in at another location. Logging off", "");
                    LA.HideButton();
                    LA.Show();
                    reader.Close();
                    close_button.PerformClick();
                    //return true;
                }
                catch
                {
                    reader.Close();
                    //return false;
                }



                // Check if threads are active or not
                query = "select * from d_active where BLS_AL_active = '1'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    BLS_AL_active_text.Text = "Active";
                    BLS_AL_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    BLS_AL_active_text.Visible = true;
                }
                catch
                {
                    BLS_AL_active_text.Text = "Inactive";
                    BLS_AL_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    BLS_AL_active_text.Visible = true;
                }
                reader.Close();

                // Check if threads are active or not
                query = "select * from d_active where CAD_print_processor_active = '1'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    cad_print_active_text.Text = "Active";
                    cad_print_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    cad_print_active_text.Visible = true;
                }
                catch
                {
                    cad_print_active_text.Text = "Inactive";
                    cad_print_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    cad_print_active_text.Visible = true;
                }
                reader.Close();

                // Check if threads are active or not
                query = "select * from d_active where turn_checker_active = '1'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    turn_check_active.Text = "Active";
                    turn_check_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    turn_check_active.Visible = true;
                }
                catch
                {
                    turn_check_active.Text = "Inactive";
                    turn_check_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    turn_check_active.Visible = true;
                }
                reader.Close();

                // Check if threads are active or not
                query = "select * from d_active where task_tracker_active = '1'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    task_tracker_active.Text = "Active";
                    task_tracker_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    task_tracker_active.Visible = true;
                }
                catch
                {
                    task_tracker_active.Text = "Inactive";
                    task_tracker_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    task_tracker_active.Visible = true;
                }
                reader.Close();


                // Check if threads are active or not
                query = "select * from d_active where on_hold_active = '1'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    on_hold_active.Text = "Active";
                    on_hold_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    on_hold_active.Visible = true;
                }
                catch
                {
                    on_hold_active.Text = "Inactive";
                    on_hold_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    on_hold_active.Visible = true;
                }
                reader.Close();

            }


            if (false)//ticks > 21)
            {
                //if (ticks % 10 == 0) ping_average = 0;


                cpuCounter = new PerformanceCounter();
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";

                Ping ping = new Ping();
                IPAddress address = IPAddress.Loopback;
                PingReply reply = ping.Send("10.0.0.24");
                double pingz = 1;
                if (reply.Status == IPStatus.Success)
                {
                    ping1_text.Text = "Ping: " + reply.RoundtripTime.ToString() + "ms";
                    pingz = pingz + Convert.ToDouble(reply.RoundtripTime.ToString());
                    if (pingz > 9)
                    {
                        pingz = 9;
                    } 
                    else if (pingz - 1 > ping_tolerance)
                    {
                        string date_string = "";
                        if (DateTime.Now.Minute.ToString().Length == 1)
                        {
                            date_string = "[" + DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString() + "]";
                        }
                        else
                        {
                            date_string = "[" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "]";
                        }
                        // REPORT REMOVAL //Main_Update_Transition_Data("PING_SPIKE", date_string, false);
                    }
                    else if (pingz - 1 > 0)
                    {
                        non_zero_ticks++;
                        non_zero_ping_average = non_zero_ping_average + pingz;
                    } 
                }
                else
                {
                    pingz = 9;
                }
                avg_ping.Text = "Avg Ping (Non-zero): " + Math.Round((non_zero_ping_average / ((non_zero_ticks))), 2) + "ms";
                cpuCounter.NextValue();
                double gs = Convert.ToDouble(cpuCounter.NextValue()) / 18;
                chart1.Series["Series2"].Points.AddXY(ticks, gs + 1.5);
                chart1.Series["Series3"].Points.AddXY(ticks, rnd.Next(1,90)/10);
                cpu_text.Text = "CPU Usage: " + Math.Round(gs*3, 2) + "%";

                chart1.Series["Series1"].Points.AddXY(ticks, pingz); 
                if (chart1.ChartAreas[0].AxisX.Maximum > chart1.ChartAreas[0].AxisX.ScaleView.Size)
                    chart1.ChartAreas[0].AxisX.ScaleView.Scroll(chart1.ChartAreas[0].AxisX.Maximum);

            }

        }

        private void Retrieve_Files(string path)
        {
            prev_so_filelist = Directory.GetFiles(path);
        }

        private void execute_prevso()
        {
            string query;
            foreach (string file_path in prev_so_filelist)
            {
                string current_SO = Path.GetFileName(file_path).Substring(0, 6);
                var text = File.ReadAllText(file_path);
                string[] lines = text.Split(new string[] { ":" }, StringSplitOptions.None);
                string prev_so = "";
                if (lines[1].Contains("'"))
                {
                    foreach (char c in lines[1])
                    {
                        if (!(c.ToString() == "'"))
                        {
                            prev_so = prev_so + c.ToString();
                        }
                    }
                }
                else
                {
                    prev_so = lines[1];
                }
                query = "insert into d_prevso values ('" + current_SO + "', '" + prev_so + "', '" + DateTime.Now.ToString() + "')";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
                File.Delete(file_path);
            }
        }

        // Monitoring folder thread, sleep every five seconds
        internal void Monitor_Folders()
        {
            while (true)
            {
                UpdateProgress(0);
                Thread.Sleep(new TimeSpan(0, 0, 1));
            }
        }

        // Invisible button to open folders
        #region All the invisible buttons
        internal void button1_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\"); }
        internal void button2_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\"); }
        internal void button3_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\"); }
        internal void button4_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\TURN\\BOL\\"); }
        internal void button5_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\PLOT_PRINT\\"); }
        internal void button6_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\A4_PRINT\\"); }
        internal void button7_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\LATHEDWG\\"); }
        internal void button8_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\Development\\Robin\\TEST\\"); }
        internal void button9_Click(object sender, EventArgs e) { Process.Start(@"\\\\10.0.0.8\\shopdata\\LDATA\\COLOMBIA\\DATACOPY\\"); }
        internal void button10_Click(object sender, EventArgs e) { Process.Start("\\\\FILESRV\\SDRIVE\\LDATAFROMCOLOMBIA\\"); }
        #endregion

        // Background check messages
        private bool has_unread_messages(string employeenumber)
        {
            string query = "select * from d_msgtable where toemp = '" + employeenumber + "' and didread = '0'";
            ExcoODBC database2 = ExcoODBC.Instance;
            OdbcDataReader reader2;
            database2 = ExcoODBC.Instance;
            database2.Open(Database.DECADE_MARKHAM);
            reader2 = database2.RunQuery(query);
            reader2.Read();
            try
            {
                Console.WriteLine(reader2[0].ToString());
                if ((!alerted_people.Contains(reader2[1].ToString().Trim())) && notifications.Checked)
                {
                    AlertBox alert = new AlertBox("Message recieved from " + reader2[2].ToString().Trim(), employeenumber);
                    alerted_people.Add(reader2[1].ToString().Trim());
                    alert.Show();
                    reader2.Close();
                    query = "update d_msgtable set didread = '1' where toemp = '" + employeenumber + "'";
                    database2 = ExcoODBC.Instance;
                    database2.Open(Database.DECADE_MARKHAM);
                    reader2 = database2.RunQuery(query);
                    FlashWindowEx(this);
                }
                reader2.Close();
                return true;
            }
            catch
            {
                reader2.Close();
                return false;
            }
        }

        #endregion

        // #########################################################################################################################
        // #####################################################[ PRINT OPTIONS ]###################################################
        // #########################################################################################################################
        #region PRINT OPTIONS

        // Main load (empty)
        private void Main_Load(object sender, EventArgs e)
        {
            bolster_autolathe_button.PerformClick();
            cad_print_button.PerformClick();
            //task_track_button.PerformClick();
            on_hold_button.PerformClick(); 
            this.WindowState = FormWindowState.Minimized;
        }

        // Store files in File_List dictionary
        private void Store_Files(string orig_path, string[] file_path)
        {
            foreach (string path in file_path)
            {
                File_List.Add(path);
            }
        }

        // Get latest filecount selection in box
        private void update_file_count()
        {
            if (SO_number_print.Length == 6)
            {
                int count = 0;
                for (int i = 0; i < print_list.Items.Count; i++)
                {
                    if (print_list.GetItemChecked(i)) count++;
                }
                numberOfFilesText.Text = count.ToString() + " file(s) selected";
            }
        }

        // Store files from all directories (ADD/REMOVE DIRECTORY TO SEARCH LIST)
        private void Get_Files(string SO_Number)
        {
            string file_wildcard, orig_path;
            string[] files;
            file_wildcard = "*" + SO_Number + "*";

            orig_path = "\\\\10.0.0.8\\shopdata\\TURN\\BOL\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\TURN\\BOL\\BOL_ERROR\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\LDATA\\TURN\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\checkERROR\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\checkERROR\\TURNFILE\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\TURN\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);

            orig_path = "\\\\10.0.0.8\\shopdata\\LDATA\\COLOMBIA\\TURN\\";
            files = Directory.GetFiles(orig_path, file_wildcard);
            Store_Files(orig_path, files);
        }

        // Allow user to hit enter instead of clicking recieve
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                retrieve_button.PerformClick();
                numberOfFilesText.Text = "";
            }
        }

        // Get and update designer for shop order number
        private void update_designer(string ordernumber)
        {
            if (ordernumber.Length == 6)
            {
                string query = "select top 1 a.station as Station, b.firstname as Name from d_task " +
                               "as a, d_user as b where a.employeenumber = b.employeenumber and " +
                               "a.ordernumber = " + ordernumber.Trim() + " and a.station like " +
                               "'%DS%' and a.task = 'DD' order by a.tasktime desc ";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                designerBox.Text = "File(s) not found";
                if (File_List.Count > 0)
                {
                    designerBox.Text = "Design is not complete";
                    while (reader.Read())
                    {
                        string name = reader[1].ToString().ToLower();
                        designerBox.Text = "Designer: " + name.First().ToString().ToUpper() + name.Substring(1);
                    }
                }
                reader.Close();
            }
        }

        // Clear shop order number field and reset boxes
        private void clear_button_Click(object sender, EventArgs e)
        {
            SO_text.Clear();
            print_list.Items.Clear();
            File_List.Clear();
            numberOfFilesText.Text = "";
            designerBox.Text = "";
        }

        // Get list of files to display in check box
        private void retrieve_button_Click(object sender, EventArgs e)
        {
            if (SO_number_print.Length == 6)
            {
                print_list.Items.Clear();
                File_List.Clear();
                Get_Files(SO_number_print);
                foreach (string str in File_List)
                {
                    print_list.Items.Add(str);
                }
            }
            update_designer(SO_number_print);
        }

        // Print the selected checked items
        private void print_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < print_list.Items.Count; i++)
            {
                if (print_list.GetItemChecked(i))
                {
                    string print_command = "/c copy " + print_list.Items[i] + " \\\\10.0.0.8\\CAM_PS_OPTRA";

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = "cmd.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = print_command;
                    try
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    catch { }
                }
            }
            for (int i = 0; i < print_list.Items.Count; i++)
            {
                print_list.SetItemChecked(i, false);
            }
            update_file_count();
        }

        // Update textbox variable
        private void SO_text_TextChanged(object sender, EventArgs e)
        {
            if (SO_text.Text.All(char.IsDigit))
            {
                SO_number_print = SO_text.Text;
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                SO_text.Text = SO_text.Text.Substring(0, SO_text.Text.Length - 1);
                SO_text.SelectionStart = SO_text.Text.Length;
                SO_text.SelectionLength = 0;
            }
        }
        
          
        // Check All
        private void selectall_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < print_list.Items.Count; i++)
            {
                print_list.SetItemChecked(i, true);
            }
            update_file_count();
        }

        // Clear All
        private void clearall_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < print_list.Items.Count; i++)
            {
                print_list.SetItemChecked(i, false);
            }
            update_file_count();
        }

        // Update file count on box changes in check box
        private void print_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_file_count();
        }

        #endregion

        // #########################################################################################################################
        // ######################################################[ QUICK TASKS ]####################################################
        // #########################################################################################################################
        #region QUICK TASKS

        // Clear shop order number field and reset boxes
        private void search_button_Click(object sender, EventArgs e)
        {
            if (google_search_string.Length > 0) 
            {
                string new_search = "";
                for (int i = 0; i < google_search_string.Length; i++)
                {
                    if (google_search_string[i].ToString() == " ")
                    {
                        new_search = new_search + "+";
                    }
                    else
                    {
                        new_search = new_search + google_search_string[i].ToString();
                    }
                }
                string URL = "http://google.ca//?gws_rd=ssl#q=";
                Process.Start(URL + new_search);
            }
        }

        // Open Shoptrack URL
        private void shoptrack_button_Click(object sender, EventArgs e)
        {
            string URL = "http://10.0.0.8:8080";
            Process.Start(URL);
        }

        // Non-existing button
        private void g_button_Click(object sender, EventArgs e)
        {
            if (google_search_string.Length > 0)
            {
                string URL = "www." + google_search_string + ".com";
                Process.Start(URL);
            }
        }

        // Google Search button
        private void search_clear_button_Click(object sender, EventArgs e)
        {
            google_search_string = "";
            google_search_box.Clear();
        }

        // Search text box
        private void google_search_box_TextChanged(object sender, EventArgs e)
        {
            google_search_string = google_search_box.Text.Trim();
        }

        // Opens up steel inventory dialog to input steel inventory
        private void steel_button_Click(object sender, EventArgs e)
        {
            if (!steel_in_box)
            {
                steel_in_box = true;
                Steel_IN form = new Steel_IN(this);
                //form.ShowDialog(); //No multiple instance of forms allowed
                form.Show();
            }
        }

        // Prompt Login dialog
        private void messenger_button_Click(object sender, EventArgs e)
        {
            if (!messenger_box)
            {
                messenger form = new messenger(this, this.employee_box_name);
                //messenger form = new messenger("10577");
                form.Show();
                messenger_box = true;
                //form.Show();
            }
        }

        // Allow user to hit enter instead of clicking recieve
        private void google_search_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (google_search_string.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter) && (google_search_string.Last().ToString() == "."))
                {
                    e.Handled = true;
                    google_search_string = google_search_string.Substring(0, google_search_string.Length - 1);
                    g_button.PerformClick();
                }
                else if (e.KeyChar == (char)Keys.Enter && (google_search_string.Last().ToString() != "~"))
                {
                    e.Handled = true;
                    search_button.PerformClick();
                }
            }
        }

        // Open calculator APP
        private void calc_button_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "calc.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process exeProcess = Process.Start(startInfo);
        }

        private void send_email_button_Click(object sender, EventArgs e)
        {
            string query = "select * from d_active where employeenumber = '" + this.employee_box_name + "' and len(emailaddress) > 4 and len(SMTP) > 4 ";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            // Diagnose whether or not user has email settings set up; if not, prompt setup window instead
            try
            {
                string email = reader[3].ToString().Trim();
                SendEmail email_client = new SendEmail(this.employee_box_name, email);
                email_client.Show();
                reader.Close();
            }
            catch
            {
                reader.Close();
                EmailSettings email_settings = new EmailSettings(employee_box_name);
                email_settings.Show();
            }
        }

        private void inbox_button_Click(object sender, EventArgs e)
        {
            string query = "select * from d_active where employeenumber = '" + this.employee_box_name + "' and len(emailaddress) > 4 and len(POP3) > 4 ";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            // Diagnose whether or not user has email settings set up; if not, prompt setup window instead
            try
            {
                string email = reader[3].ToString().Trim();
                Encrypter en = new Encrypter();
                string a = en.Decrypt(reader[3].ToString().Trim());
                string b = en.Decrypt(reader[4].ToString().Trim());
                string c = reader[6].ToString().Trim();
                string d = reader[8].ToString().Trim();
                Console.WriteLine(a + b + c + d);
                Inbox inbox = new Inbox(this.employee_box_name, en.Decrypt(reader[3].ToString().Trim()), en.Decrypt(reader[4].ToString().Trim()), reader[6].ToString().Trim(), reader[8].ToString().Trim());
                inbox.Show();
                reader.Close();
            }
            catch (Exception ee)
            {
                string g = ee.ToString();
                Console.WriteLine(g);
                reader.Close();
                EmailSettings email_settings = new EmailSettings(employee_box_name);
                email_settings.Show();
            }
        }

        private void page_button_Click(object sender, EventArgs e)
        {
            if (!pager_box)
            {
                Pager page = new Pager(this, employee_box_name);
                page.Show();
                pager_box = true;
            }
        }

        #region Statistical data retriever/processor
        public void Main_Update_Transition_Data(string report_name, string value, bool overwrite = false)
        {
            report.Process_Transitional_Data();
            List<string> pp = new List<string>();
            pp.Add(value);
            report.Add_Transitional_Data(report_name, pp, overwrite);
            report.Write_Transitional_Data();
        }

        public int Get_Transition_Data_Value(string report_name)
        {
            report.Process_Transitional_Data();
            return report.Get_Single_Value(report_name);
        }

        private void stat_button_Click(object sender, EventArgs e)
        {
            if (!statistics_box)
            {
                report.Log_update();
                //Get_Aggregate_Report("04/22/2015", "04/29/2015");
                Statistics stats = new Statistics(this);
                stats.Show();
                statistics_box = true;
            }
        }

        public void Refresh_Monitor_Log()
        {
            report.Log_update();
        }

        // Cross-form report access
        public List<List<string>> Get_Aggregate_Report(string fromdate, string todate)
        {
            return report.Summarize_Data(fromdate, todate);
        }

        public bool Delete_Report(string report_name)
        {
            return report.Delete_Report(report_name);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            report.Clear_Report_Entries();
        }
        #endregion
        #endregion

        // #########################################################################################################################
        // ###################################################[ PRODUCTION TASKS ]##################################################
        // #########################################################################################################################
        #region PRODUCTION TASKS

        // Bolster AUTOLATHE implementation
        private void bolster_autolathe_button_Click(object sender, EventArgs e)
        {
            if (!bolster_box)
            {
                /*
                string query = "select * from d_active where BLS_AL_active = '1'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                //reader.Read();
                // Diagnose whether or not an active bolster AL session is present. If not, then open a new one
                try
                {
                    string email = reader[3].ToString().Trim();
                    reader.Close();
                    AlertBox alert = new AlertBox("ERROR: Another instance of Bolster Auto Lathe is already running on the server.", "");
                    alert.HideButton();
                    alert.Show();
                }
                catch
                {*/
                    bolster_box = true;
                    BolsterAL bolster = new BolsterAL(this);
                    bolster.Show();
                    //reader.Close();
//}
            }
        }

        private void cad_print_button_Click(object sender, EventArgs e)
        {
            if (!cad_print_box)
            {
                /*
                string query = "select * from d_active where CAD_PRINT_Processor_active = '1'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                // Diagnose whether or not an active bolster AL session is present. If not, then open a new one
                try
                {
                    Console.WriteLine(reader[3].ToString().Trim());
                    reader.Close();
                    AlertBox alert = new AlertBox("ERROR: Another instance of CAD Print Processor is already running on the server.", "");
                    alert.HideButton();
                    alert.Show();
                }
                catch
                {
                 */
                    cad_print_box = true;
                    CRV_Generator bolster = new CRV_Generator(this);
                    bolster.Show();
                    //reader.Close();
                //}
            }
        }

        private void task_track_button_Click(object sender, EventArgs e)
        {
            if (!task_tracker_box)
            {
                /*
                string query = "select * from d_active where Task_Tracker_active = '1'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                // Diagnose whether or not an active bolster AL session is present. If not, then open a new one
                try
                {
                    Console.WriteLine(reader[3].ToString().Trim());
                    reader.Close();
                    AlertBox alert = new AlertBox("ERROR: Another instance of Task Tracker is already running on the server.", "");
                    alert.HideButton();
                    alert.Show();
                }
                catch
                {*/
                    task_tracker_box = true;
                    TaskTracker task = new TaskTracker(this);
                    task.Show();
                    //reader.Close();
                //}
            }
        }

        private void turn_check_button_Click(object sender, EventArgs e)
        {
            if (true)
            {
                /*
                string query = "select * from d_active where Turn_Checker_active = '1'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                // Diagnose whether or not an active bolster AL session is present. If not, then open a new one
                try
                {
                    Console.WriteLine(reader[3].ToString().Trim());
                    reader.Close();
                    AlertBox alert = new AlertBox("ERROR: Another instance of Turn Checker is already running on the server.", "");
                    alert.HideButton();
                    alert.Show();
                }
                catch
                {*/
                    turn_check_box = true;
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = "Turn_Checker.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    try
                    {
                        Process exeProcess = Process.Start(startInfo);
                    }
                    catch { }

                    //reader.Close();
                //}
            }
        }

        private void on_hold_button_Click(object sender, EventArgs e)
        {
            if (!on_hold_box)
            {
                /*
                string query = "select * from d_active where on_hold_active = '1'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                // Diagnose whether or not an active bolster AL session is present. If not, then open a new one
                try
                {
                    Console.WriteLine(reader[3].ToString().Trim());
                    reader.Close();
                    AlertBox alert = new AlertBox("ERROR: Another instance of On-Hold processor is already running on the server.", "");
                    alert.HideButton();
                    alert.Show();
                }
                catch
                {*/
                    on_hold_box = true;
                    OnHold hold = new OnHold(this);
                    hold.Show();
                    //reader.Close();
                //}
            }
        }


        private void admin_button_Click(object sender, EventArgs e)
        {
            if (!admin_task_box && (employee_box_name == "10577" || employee_box_name == "10403" || employee_box_name == "10178" || employee_box_name == "10100"))
            {
                admin_task_box = true;
                Administrative form = new Administrative(this);
                form.Show();
            }
            else
            {
                AlertBox alert = new AlertBox("ERROR: You are not an admin.", "");
                alert.HideButton();
                alert.Show();
            }
        }

        #endregion

        // #########################################################################################################################
        // #####################################################[ MISCELLANEOUS ]###################################################
        // #########################################################################################################################
        #region MISCELLANEOUS BACKGROUND FUNCTIONS (TOGGLES)
        public void steel_done()
        {
            steel_in_box = false;
        }

        public void messenger_done()
        {
            messenger_box = false;
        }

        public void pager_done()
        {
            pager_box = false;
        }

        public void bolster_done()
        {
            bolster_box = false;
        }

        public void cad_print_done()
        {
            cad_print_box = false;
        }

        public void turn_check_done()
        {
            turn_check_box = false;
        }

        public void task_tracker_done()
        {
            task_tracker_box = false;
        }

        public void on_hold_done()
        {
            on_hold_box = false;
        }

        public void admin_task_done()
        {
            admin_task_box = false;
        }

        public void statistics_done()
        {
            statistics_box = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void print_list_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

    }
}

