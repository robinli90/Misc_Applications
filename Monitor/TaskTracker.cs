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


namespace Monitor
{
    public partial class TaskTracker : Form
    {
        Main _parent;
        bool running;
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();
        private int running_count = 0;
        private string[] File_Array;
        string employeenumber, task, subpart, station, tasktime; // TASK 
        string ordernumber, part, hard1, hard2, soref1, soref2, partref1, partref2; // HARD
        private List<string> error_files = new List<string>();


        public TaskTracker(Main form1)
        {
            InitializeComponent();
            pictureBox1.Show();
            _parent = form1;
            tick_update.Interval = 40000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(check_folder);
            string query = "update d_active set task_tracker_active = '1' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            running = true;
            buffer_text.AppendText("===================================\r\n");
            buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
            buffer_text.AppendText("===================================\r\n");
            start_button.Visible = false;
        }



        private void check_folder(object sender, EventArgs e)
        {
            if (running)
            {
                Process_Files("\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\");
                //Process_Files("\\\\10.0.0.4\\sdrive\\ACCT\\CMDS\\");
                //Process_Files("\\\\10.0.0.8\\sdrive\\ACCT\\CMDS\\");
            }
        }

        private void Process_Files(string path)
        {
            File_Array = Directory.GetFiles(path);
            foreach (string file_path in File_Array)
            {
                try
                {
                    if (!error_files.Contains(file_path.Substring(13)))
                    {
                        running_count++;
                        buffer_text.AppendText("Processing file: " + file_path.Substring(13) + "\r\n");
                        var text = File.ReadAllText(file_path);
                        string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            try
                            {
                                if (line.Length > 1)
                                {
                                    string[] entries = line.Split(new string[] { "|" }, StringSplitOptions.None);
                                    if (entries[0] == "TASK") // If TASK;
                                    {
                                        buffer_text.AppendText("     -> TASK found\r\n");
                                        ordernumber = entries[1].Trim();
                                          employeenumber = entries[2].Trim();
                                        tasktime = entries[4];
                                        task = entries[3].Trim().Substring(0, 2);
                                        part = entries[3].Trim().Substring(2, 1);
                                        subpart = entries[3].Trim().Substring(3, 1);
                                        station = entries[5].Trim();
                                        if (verify_task_parameter(ordernumber, employeenumber, task, part, subpart, station))
                                        {
                                            buffer_text.AppendText("     -> Order number: " + ordernumber + "\r\n");
                                            buffer_text.AppendText("     -> Querying...\r\n");
                                            string query = "insert into d_task (ordernumber, employeenumber, task, part, subpart, station, tasktime, flags) values (" + ordernumber + ", " + employeenumber + ", '" + task + "', '" + part + "', '" + subpart + "', '" + station + "', '" + tasktime + "', 0);";
                                            ExcoODBC database = ExcoODBC.Instance;
                                            OdbcDataReader reader;
                                            database.Open(Database.DECADE_MARKHAM);
                                            reader = database.RunQuery(query);
                                            reader.Close();
                                            buffer_text.AppendText("     -> Done!\r\n");
                                        }
                                        else
                                        {
                                            File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\" + file_path.Substring(13), true);
                                            buffer_text.AppendText("     -> Invalid paremeters\r\n");
                                            // invalid parameters
                                        }
                                    }
                                    else if (entries[0] == "HARD")
                                    {
                                        buffer_text.AppendText("     -> HARD task found\r\n");
                                        ordernumber = entries[1].Trim();
                                        part = entries[2].Trim();
                                        hard1 = entries[3].Trim();
                                        hard2 = entries[4].Trim();
                                        soref1 = entries[5].Trim();
                                        partref1 = entries[6].Trim();
                                        soref2 = entries[7].Trim();
                                        partref2 = entries[8].Trim();
                                        if (verify_hard_parameter(ordernumber, part, hard1, hard2, soref1, soref2, partref1, partref2))
                                        {
                                            buffer_text.AppendText("     -> Querying...\r\n");
                                            string query = "insert into d_hardness (ordernumber, part, hard1, hard2, ctime, soref1, soref2, partref1, partref2) values (" + ordernumber + ", '" + part + "', '" + hard1 + "', '" + hard2 + "', '" + DateTime.Now.ToString() + "', " + soref1 + ", " + soref2 + ", '" + partref1 + "', '" + partref2 + "');";
                                            ExcoODBC database = ExcoODBC.Instance;
                                            OdbcDataReader reader;
                                            database.Open(Database.DECADE_MARKHAM);
                                            reader = database.RunQuery(query);
                                            reader.Close();
                                            buffer_text.AppendText("     -> Done!\r\n");
                                        }
                                        else
                                        {
                                            File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\" + file_path.Substring("\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\".Length), true);
                                            buffer_text.AppendText("     -> ERROR: Invalid paremeters\r\n");
                                            buffer_text.AppendText("     -> File moved to CMDS//CMDSDUMP \r\n");
                                            buffer_text.Refresh();
                                            // invalid parameters
                                        }
                                    }
                                    else
                                    {
                                        File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\" + file_path.Substring("\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\".Length), true);
                                        buffer_text.AppendText("     -> ERROR: Invalid paremeters\r\n");
                                        buffer_text.AppendText("     -> File moved to CMDS//CMDSDUMP \r\n");
                                        buffer_text.Refresh();
                                        // invalid hard/task paremeter
                                    }
                                }
                            }
                            catch
                            {

                                buffer_text.AppendText("     -> ERROR: Invalid Paremeters\r\n");
                                buffer_text.AppendText("     -> File moved to CMDS//CMDSDUMP \r\n");
                                buffer_text.Refresh();
                                File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\" + file_path.Substring("\\\\10.0.0.8\\shopdata\\ACCT\\CMDS\\CMDSDUMP\\".Length), true);
                            } //error  
                        }
                        buffer_text.Refresh();
                        if (File.Exists(file_path))
                        {
                            File.Delete(file_path);
                        }
                        // REPORT REMOVAL //_parent.Main_Update_Transition_Data("PROCESSOR_TASK_TRACKING", (_parent.Get_Transition_Data_Value("PROCESSOR_TASK_TRACKING") + 1).ToString(), true);
                        count_text.Text = "Files processed: " + running_count.ToString();
                        buffer_text.SelectionStart = buffer_text.Text.Length;
                        buffer_text.ScrollToCaret();
                    }
                }
                catch
                {
                    error_files.Add(file_path.Substring(13));
                    buffer_text.AppendText("     -> ERROR: File Handling Error\r\n");
                    buffer_text.AppendText("     -> Attempting to delete... \r\n");
                    buffer_text.Refresh();
                }
            }
        }

        // Verify TASK paremeters
        private bool verify_task_parameter(string ordernumber, string employeenumber, string task, string part, string subpart, string station)
        {
            bool okay = true;
            if (!(ordernumber.All(Char.IsDigit))) okay = false; 
            if (ordernumber.Length > 6 || ordernumber.Length < 5) okay = false;
            if (!(employeenumber.All(Char.IsDigit))) okay = false; 
            if (employeenumber.Length != 5) okay = false;
            if (task.Length != 2) okay = false;
            if (part.Length != 1) okay = false;
            if (subpart.Length != 1) okay = false;
            if (!(station.Length > 1)) okay = false;
            return okay;
        }

        // Verify HARD parameters
        private bool verify_hard_parameter(string ordernumber, string part, string hard1, string hard2, string soref1, string soref2, string partref1, string partref2)
        {
            bool okay = true;
            if (!(ordernumber.All(Char.IsDigit))) okay = false;
            if (ordernumber.Length > 6 || ordernumber.Length < 5) okay = false;
            if (!(part.All(Char.IsLetter))) okay = false;
            if (part.Length != 1) okay = false;
            try
            {
                Convert.ToDouble(hard1);
                Convert.ToDouble(hard2);
            }
            catch
            {
                okay = false;
            }
            if (!(soref1.All(Char.IsDigit))) okay = false;
            if (soref1.Length != 5) okay = false;
            if (!(soref2.All(Char.IsDigit))) okay = false;
            if (soref2.Length != 5) okay = false;
            if (!(partref1.All(char.IsLetter))) okay = false;
            if (partref1.Length != 1) okay = false;
            if (!(partref2.All(char.IsLetter))) okay = false;
            if (partref2.Length != 1) okay = false;
            return okay;
        }


        private void buffer_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            string query = "update d_active set task_tracker_active = '0' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            // REPORT REMOVAL //_parent.task_tracker_done();
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

                string query = "update d_active set task_tracker_active = '1' where employeenumber = '10577'";
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

                string query = "update d_active set task_tracker_active = '0' where employeenumber = '10577'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
        }
    }
}
