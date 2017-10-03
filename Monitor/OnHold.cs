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
    public partial class OnHold : Form
    {

        Main _parent;
        bool running;
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();
        private int running_count = 0;
        private int file_multiplier = 20;

        private string[] delete_directories = { "\\\\10.0.0.8\\shopdata\\LATHE2\\JOBS\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\DRAMET\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\EDM\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\GAUGE\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\LATHEFIN\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\MILL\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\SPARKY\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\LATHE\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\TOOLLISTS\\",
                                                "\\\\10.0.0.8\\shopdata\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\LDATA\\TURN\\",

                                                "\\\\10.0.0.8\\rdrive\\TOOLLISTS\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\MILL5AXIS\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\TOOLLIST5AXIS\\"
                                              };

        private string[] repository_directories = { "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\LATHE2\\JOBS\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\DRAMET\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\EDM\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\GAUGE\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\LATHEFIN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\MILL\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\SPARKY\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\LATHE\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\TOOLLISTS\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\LDATA\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\TOOLLISTS\\"
                                              };


        public void GET_DIRECTORIES()
        {
            return;
        }


        public OnHold(Main form1)
        {
            InitializeComponent();
            pictureBox1.Show();
            _parent = form1;
            //tick_update.Interval = 40000;
            tick_update.Interval = 40000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(checkDB);
            string query = "update d_active set on_hold_active = '1' where employeenumber = '10577'";
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

        private void checkDB(object sender, EventArgs e)
        {
            if (running)
            {
                string files = ""; string ordernumber; 
                string query = "select ordernumber from d_onhold where onhold = 'P'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                while (reader.Read())
                {
                    ordernumber = reader[0].ToString().Trim();
                    try
                    {
                        buffer_text.AppendText("Processing: File:" + ordernumber + "\r\n");
                    }
                    catch { }
                    try
                    {
                        buffer_text.AppendText("     -> Analyzing..." + "\r\n");
                        buffer_text.AppendText("     -> Updating database..." + "\r\n");
                        buffer_text.AppendText("     -> Deleting related files..." + "\r\n");
                        files = delete_from_directories(ordernumber);
                        string query2 = "update d_onhold set onhold = 'X', files = '" + files + "' where ordernumber = '" + ordernumber + "'";
                        ExcoODBC database2 = ExcoODBC.Instance;
                        OdbcDataReader reader2;
                        database2.Open(Database.DECADE_MARKHAM);
                        reader2 = database2.RunQuery(query2);
                        buffer_text.AppendText("     -> Done! " + "\r\n");
                        buffer_text.Refresh();
                        buffer_text.SelectionStart = buffer_text.Text.Length;
                        buffer_text.ScrollToCaret();
                        reader2.Close();
                    }
                    catch { };
                    running_count++;
                    // REPORT REMOVAL //_parent.Main_Update_Transition_Data("PROCESSOR_ON_HOLD", (_parent.Get_Transition_Data_Value("PROCESSOR_ON_HOLD") + 1).ToString(), true);
                    count_text.Text = "Files processed: " + running_count.ToString();
                    count_text.Refresh();
                }
                reader.Close();
            }
        }

        private string delete_from_directories(string ordernumber)
        {
            string files = "";
            string[] File_in_dir;
            foreach (string dir_path in delete_directories)
            {
                try
                {
                    File_in_dir = Directory.GetFiles(dir_path, "*" + ordernumber + "*");
                    foreach (string file in File_in_dir)
                    {
                        string file_name = Path.GetFileName(file);
                        string file_path = Path.GetFullPath(file).Substring(0, Path.GetFullPath(file).Length - file_name.Length - 1);
                        files = files + " " + file_name;
                        //buffer_text.AppendText((file_name, reverse));
                        File.Copy(file, repository_directories[_GET_DIRECTORY_INDEX(file)] + file_name, true);
                        //File.Copy(file, "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\" + Path.GetFileName(file));
                        File.Delete(file);
                        try
                        {
                            buffer_text.AppendText("       -> Deleting: " + file.Substring(11) + "\r\n");
                            buffer_text.Refresh();
                            buffer_text.SelectionStart = buffer_text.Text.Length;
                            buffer_text.ScrollToCaret();
                        }
                        catch { }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            return files;
        }

        private int _GET_DIRECTORY_INDEX(string path)
        {
            if (path.Contains("LATHE2") && path.Contains("JOBS"))
            {
                return 0;
            }
            else if (path.Contains("DRAMET"))
            {
                return 1;
            }
            else if (path.Contains("EDM"))
            {
                return 2;
            }
            else if (path.Contains("GAUGE"))
            {
                return 3;
            }
            else if (path.Contains("LATHEFIN"))
            {
                return 4;
            }
            else if (path.Contains("MILL"))
            {
                return 5;
            }
            else if (path.Contains("SPARKY"))
            {
                return 6;
            }
            else if (path.Contains("LATHE") && path.Contains("CURJOBS") && !path.Contains("LATHEFIN"))
            {
                return 7;
            }
            else if (path.Contains("CURJOBS") && path.Contains("TOOLLISTS"))
            {
                return 8;
            }
            else if (path.Contains("TURN") && !path.Contains("LDATA"))
            {
                return 9;
            }
            else if (path.Contains("TURN"))
            {
                return 10;
            }
            else if (path.Contains("TOOLLISTS"))
            {
                return 11;
            }
            else if (path.Contains("TOOLLIST5AXIS"))
            {
                return 12;
            }
            else if (path.Contains("5AXIS"))
            {
                return 13;
            }
            else
            {
                return 0;
            }
        }


        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            string query = "update d_active set on_hold_active = '0' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            // REPORT REMOVAL //_parent.on_hold_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                try
                {
                    running = true;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = false;
                    stop_button.Visible = true;
                    pictureBox1.Enabled = true;

                    string query = "update d_active set on_hold_active = '1' where employeenumber = '10577'";
                    ExcoODBC database = ExcoODBC.Instance;
                    OdbcDataReader reader;
                    database.Open(Database.DECADE_MARKHAM);
                    reader = database.RunQuery(query);
                    reader.Close();
                }
                catch { }
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (running)
            {
                try
                {
                    running = false;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread stopped at: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = true;
                    stop_button.Visible = false;
                    pictureBox1.Enabled = false;

                    string query = "update d_active set on_hold_active = '0' where employeenumber = '10577'";
                    ExcoODBC database = ExcoODBC.Instance;
                    OdbcDataReader reader;
                    database.Open(Database.DECADE_MARKHAM);
                    reader = database.RunQuery(query);
                    reader.Close();
                }
                catch { }
            }
        }

    }
}
