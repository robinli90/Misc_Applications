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
    public partial class Administrative : Form
    {

        Main _parent;

        public Administrative(Main form1)
        {
            InitializeComponent();
            _parent = form1;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            _parent.admin_task_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void deativate_monitor_button_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("\\\\10.0.0.8\\shopdata\\TURN\\BOL\\TEMP\\");
            buffer_text.AppendText("Deactivating all Monitor.exe...\r\n");
            buffer_text.AppendText("     -> Terminating... \r\n");
            buffer_text.AppendText("     -> Done!\r\n");
            string query = "update d_active set active = '0'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
        }

        private void deactivate_process_button_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("\\\\10.0.0.8\\shopdata\\TURN\\BOL\\TEMP\\");
            buffer_text.AppendText("Deactivating all production processes...\r\n");
            buffer_text.AppendText("     -> Ending tasks... \r\n");
            buffer_text.AppendText("     -> Done!\r\n");
            string query = "update d_active set BLS_AL_active = '0', CAD_PRINT_PROCESSOR_aCTIVE = '0', turn_checker_ACtive = '0', task_tracker_active = '0', on_hold_active = '0'" +
			               "where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("\\\\10.0.0.8\\shopdata\\TURN\\BOL\\TEMP\\");
            buffer_text.AppendText("Resetting Setup Parameters...\r\n");
            buffer_text.AppendText("     -> Querying... \r\n");
            buffer_text.AppendText("     -> Done!\r\n");
            buffer_text.Refresh();
            buffer_text.SelectionStart = buffer_text.Text.Length;
            buffer_text.ScrollToCaret();
            string query = "update d_active set emailaddress = 'ÇÁ¾kºÉÈ¹¾ºÈY¸ÄÂ', password = '`¯½]{Â¶Ã', SMTP = 'mass-smtp.pathcom.com', smtpport = '25', Signature = '©ºÈÉ' , " +
                           "CAD_print_processor_printer = '\\\\10.0.0.8\\WC5632', pop3 = 'pop3.pathcom.com', pop3port = '110' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
        }

        private void test_folder_button_Click(object sender, EventArgs e)
        {
            try
            {
                string[] files = Directory.GetFiles("\\\\10.0.0.8\\shopdata\\TURN\\BOL\\TEMP\\");
                buffer_text.AppendText("Testing folder monitoring system...\r\n");
                buffer_text.AppendText("     -> Adding files to test Bolster Auto Lathe... \r\n");
                buffer_text.Refresh();
                buffer_text.SelectionStart = buffer_text.Text.Length;
                buffer_text.ScrollToCaret();
                foreach (string file_path in files)
                {

                    File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\TURN\\BOL\\" + file_path.Substring(17), true);
                }
                buffer_text.AppendText("       -> Done!\r\n");
                buffer_text.AppendText("       -> Test implemented\r\n");
                buffer_text.AppendText("     -> Adding files to test Turn Check... \r\n");
                buffer_text.Refresh();
                buffer_text.SelectionStart = buffer_text.Text.Length;
                buffer_text.ScrollToCaret();
                files = Directory.GetFiles("\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\CHECKDONE\\");
                foreach (string file_path in files)
                {

                    File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\LDATA\\CHECK\\" + file_path.Substring(24), true);
                }
                buffer_text.AppendText("       -> Done!\r\n");
                buffer_text.AppendText("       -> Test implemented\r\n");
                buffer_text.Refresh();
                buffer_text.SelectionStart = buffer_text.Text.Length;
                buffer_text.ScrollToCaret();
            }
            catch { }
        }

        private void ping_test_1_TextChanged(object sender, EventArgs e)
        {
            if (ping_test_1.Text.All(char.IsDigit))
            {
                string ping_test_12 = ping_test_1.Text;
                if (ping_test_12.Length == 3)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (ping_test_1.Text[ping_test_1.Text.Length - 1].ToString() == "." && ping_test_1.Text.Length > 1)
            {
                ping_test_1.Text = ping_test_1.Text.Substring(0, ping_test_1.Text.Length - 1);
                ping_test_1.SelectionStart = ping_test_1.Text.Length;
                ping_test_1.SelectionLength = 0;
                SendKeys.Send("{TAB}");
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                ping_test_1.Text = ping_test_1.Text.Substring(0, ping_test_1.Text.Length - 1);
                ping_test_1.SelectionStart = ping_test_1.Text.Length;
                ping_test_1.SelectionLength = 0;
            }
        }

        private void ping_test_2_TextChanged(object sender, EventArgs e)
        {
            if (ping_test_2.Text.All(char.IsDigit))
            {
                string ping_test_22 = ping_test_2.Text;
                if (ping_test_22.Length == 3)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (ping_test_2.Text[ping_test_2.Text.Length - 1].ToString() == "." && ping_test_2.Text.Length > 1)
            {
                ping_test_2.Text = ping_test_2.Text.Substring(0, ping_test_2.Text.Length - 1);
                ping_test_2.SelectionStart = ping_test_2.Text.Length;
                ping_test_2.SelectionLength = 0;
                SendKeys.Send("{TAB}");
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                ping_test_2.Text = ping_test_2.Text.Substring(0, ping_test_2.Text.Length - 1);
                ping_test_2.SelectionStart = ping_test_2.Text.Length;
                ping_test_2.SelectionLength = 0;
            }
        }

        private void ping_test_3_TextChanged(object sender, EventArgs e)
        {
            if (ping_test_3.Text.All(char.IsDigit))
            {
                string ping_test_32 = ping_test_3.Text;
                if (ping_test_32.Length == 3)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (ping_test_3.Text[ping_test_3.Text.Length - 1].ToString() == "." && ping_test_3.Text.Length > 1)
            {
                ping_test_3.Text = ping_test_3.Text.Substring(0, ping_test_3.Text.Length - 1);
                ping_test_3.SelectionStart = ping_test_3.Text.Length;
                ping_test_3.SelectionLength = 0;
                SendKeys.Send("{TAB}");
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                ping_test_3.Text = ping_test_3.Text.Substring(0, ping_test_3.Text.Length - 1);
                ping_test_3.SelectionStart = ping_test_3.Text.Length;
                ping_test_3.SelectionLength = 0;
            }
        }

        private void ping_test_4_TextChanged_1(object sender, EventArgs e)
        {
            if (!ping_test_4.Text.All(char.IsDigit))
            {
                // If letter in SO_number box, do not output and move CARET to end
                ping_test_4.Text = ping_test_4.Text.Substring(0, ping_test_4.Text.Length - 1);
                ping_test_4.SelectionStart = ping_test_4.Text.Length;
                ping_test_4.SelectionLength = 0;

            }
        }

        private void ping_button_Click(object sender, EventArgs e)
        {
            if (ping_test_1.Text.Length > 0 && ping_test_2.Text.Length > 0 && ping_test_3.Text.Length > 0 && ping_test_4.Text.Length > 0)
            {
                Ping ping = new Ping();
                IPAddress address = IPAddress.Loopback;
                PingReply reply = ping.Send(ping_test_1.Text + "." + ping_test_2.Text + "." + ping_test_3.Text + "." + ping_test_4.Text);

                buffer_text.AppendText("Pinging " + ping_test_1.Text + "." + ping_test_2.Text + "." + ping_test_3.Text + "." + ping_test_4.Text + " ...\r\n");
                buffer_text.Refresh();
                buffer_text.SelectionStart = buffer_text.Text.Length;
                buffer_text.ScrollToCaret();
                if (reply.Status == IPStatus.Success)
                {
                    buffer_text.AppendText("     -> Address: " + reply.Address.ToString() + "\r\n");
                    buffer_text.AppendText("     -> Round Trip Time: " + reply.RoundtripTime + "ms\r\n");
                    buffer_text.AppendText("     -> Round Trip Time: " + reply.RoundtripTime + "ms\r\n");
                    buffer_text.AppendText("     -> Round Trip Time: " + reply.RoundtripTime + "ms\r\n");
                    buffer_text.AppendText("     -> Round Trip Time: " + reply.RoundtripTime + "ms\r\n");
                    buffer_text.AppendText("     -> Time to Live: " + reply.Address.ToString() + "\r\n");
                    buffer_text.AppendText("     -> End of test\r\n");
                    buffer_text.Refresh();
                    buffer_text.SelectionStart = buffer_text.Text.Length;
                    buffer_text.ScrollToCaret();
                }
                else
                {
                    buffer_text.AppendText("     -> Error...\r\n");
                    buffer_text.AppendText("     -> IP Unreachable\r\n");
                    buffer_text.AppendText("     -> End of test\r\n");
                    buffer_text.Refresh();
                    buffer_text.SelectionStart = buffer_text.Text.Length;
                    buffer_text.ScrollToCaret();
                }
            }
        }

        private void ping_test_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ping_button.PerformClick();
            }
        }
    }
}
