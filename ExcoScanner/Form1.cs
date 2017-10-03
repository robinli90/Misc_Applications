using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.Odbc;
using Databases;

namespace ExcoFiles
{
    public partial class Form1 : Form
    {
        public Thread thread;
        Dictionary<string, string> Checked_path = new Dictionary<string, string>();

        protected override void SetVisibleCore(bool value)
        {
            value = false;
            if (!this.IsHandleCreated) CreateHandle();
            base.SetVisibleCore(value);
        }

        public Form1()
        {
            InitializeComponent();

            this.Resize += Form_Resize;
            //GlobalKeyboardHook GKH = new GlobalKeyboardHook();

            while (true)
            {
                UpdateProgress();
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (WindowState == FormWindowState.Normal)
            {
                this.Hide();
                //this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }

        }

        private string[] File_Array;

        internal void UpdateProgress()
        {
            
            File_Array = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            if (File_Array.Length > 0)
            {
                bool alarm = false;
                List<string> warning_files = new List<string>();
                foreach (string file_path in File_Array)
                {
                    string filen = Path.GetFileName(file_path);
                    if (!Checked_path.ContainsKey(filen) && ((Path.GetFileName(file_path).Length > 25 && !Path.GetFileName(file_path).Contains(' ')) || file_path.ToLower().Contains(".locky") || file_path.ToLower().Contains("_help") || file_path.ToLower().Contains("_instruction") || file_path.ToLower().Contains(".zepto")))
                    {
                        Checked_path.Add(filen, "");
                        try
                        {
                            alarm = true;
                            warning_files.Add(Path.GetFileName(file_path));
                        }
                        catch
                        {

                        }
                    }
                }
                if (alarm)
                {
                    try
                    {
                        string message = "WARNING: Potential virus file(s) found with name(s) " + String.Join(", ", warning_files) + " from computer " + Environment.MachineName + " and user name " + Environment.UserName + "";
                        string query = "insert into internalpaging_old (FromEmp, FromF, FromL, ToEmp, ToF, ToL, Msg, Pagingtime, Status, RepStatus, Important, ForceToReply) values (10403, 'Gary', 'LUO', 10403, 'Gary', 'Li', '" + message + "', '" + DateTime.Now.ToString() + "', 1, 0, 0, 0)";
                        ExcoODBC database2 = ExcoODBC.Instance;
                        OdbcDataReader reader2;
                        database2 = ExcoODBC.Instance;
                        database2.Open(Database.DECADE_MARKHAM);
                        reader2 = database2.RunQuery(query);
                        reader2.Close();
                        query = "insert into internalpaging_old (FromEmp, FromF, FromL, ToEmp, ToF, ToL, Msg, Pagingtime, Status, RepStatus, Important, ForceToReply) values (10403, 'Gary', 'LUO', 10577, 'Robin', 'Li', '" + message + "', '" + DateTime.Now.ToString() + "', 1, 0, 0, 0)";
                        database2 = ExcoODBC.Instance;
                        database2.Open(Database.DECADE_MARKHAM);
                        reader2 = database2.RunQuery(query);
                        reader2.Close();
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.ToString());
                    }
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
