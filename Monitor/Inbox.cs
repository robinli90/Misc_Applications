using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
//using EAGetMail;
using AE.Net.Mail;
using System.Data.Odbc;
using Databases;

namespace Monitor
{
    public partial class Inbox : Form
    {
        private string employee_number;
        static Dictionary<string, List<string>> Email_List = new Dictionary<string, List<string>>();
        private int Email_Count;
        Pop3Client incoming = new Pop3Client();
        string pop3, pop3_port, incoming_email, password;
        int trigger = 0;

        public Inbox(string empnum, string email2, string password2, string pop32, string pop3_port2)
        {
            this.employee_number = empnum;
            InitializeComponent();
            incoming_email = email2;
            password = password2;
            pop3 = pop32;
            pop3_port = pop3_port2;
            Get_Emails();
            Console.WriteLine(Email_Count);
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            incoming.Logout();
            incoming.Disconnect();
            incoming.Dispose();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        public string format_number(int number)
        {
            string zeroes = "";
            if (number < 10)
            {
                zeroes = "00";
            }
            else if (number < 100)
            {
                zeroes = "0";
            }
            return zeroes + number.ToString();
        }

        public void Get_Emails()
        {
           
            inbox_list.Items.Clear();
            Email_List.Clear();
            incoming = new Pop3Client();
            incoming.Connect(pop3, Convert.ToInt32(pop3_port), false);
            incoming.Login(incoming_email, password);
            Email_Count = incoming.GetMessageCount();
            //Email_Count--;
            groupBox2.Text = "Inbox (" + Email_Count.ToString() + ")";
            this.Text = "Inbox (" + Email_Count.ToString() + ")";
            for (int i = incoming.GetMessageCount()-1; i >= 0; i--)
            {
                MailMessage mail = incoming.GetMessage(i);
                if (i == 3) { Console.WriteLine(""); }
                List<string> list;
                if (!Email_List.TryGetValue(i.ToString(), out list))
                {
                    Email_List.Add(i.ToString(), list = new List<string>());
                    list = Email_List[i.ToString()];
                    list.Add(mail.From.ToString()); 
                    list.Add(mail.Subject.ToString());
                    list.Add(mail.Date.ToString());
                    list.Add(parse_crlf(mail.Body.ToString(), i));
                }
            }
            foreach (KeyValuePair<string, List<string>> pair in Email_List)
            {
                inbox_list.Items.Add("[" + format_number(Email_Count - Convert.ToInt32(pair.Key)) + "] " + pair.Value[0] + ": " + pair.Value[1]);
            }
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Remove Carriage Return/Line feed hexidecimal coding from email; parse to \r\n
        private string parse_crlf(string str, int i)
        {
            if (i == 3) { Console.WriteLine(""); }
            string temp_feed = "";
            string ongoing_string = "";
            bool analyzing = false;
            int analysis_count = 0;
            foreach (char c in str)
            {
                if (c.ToString() == "=")
                {
                    analyzing = true;
                }
                if (!analyzing)
                {
                    ongoing_string = ongoing_string + c.ToString();
                }
                else
                {
                    temp_feed = temp_feed + c.ToString();
                    analysis_count++;
                    if (analysis_count == 3)
                    {
                        if (temp_feed == "=0D")
                        {
                            ongoing_string = ongoing_string + "\r";
                        }
                        else if (temp_feed == "=0A")
                        {
                            ongoing_string = ongoing_string + "\n";
                        }
                        else
                        {
                            ongoing_string = ongoing_string + temp_feed;
                        }
                        temp_feed = "";
                        analysis_count = 0;
                        analyzing = false;
                    }
                }
            }
            string return_str = "";
            foreach (char c in ongoing_string)
            {
                if (!(c.ToString() == "=")) return_str = return_str + c.ToString();
            }
            return return_str;
        }


        public void load_email(object sender, EventArgs e)
        {

        }

        private void clearall_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < inbox_list.Items.Count; i++)
            {
                inbox_list.SetItemChecked(i, false);
            }
        }

        private void selectall_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < inbox_list.Items.Count; i++)
            {    
                inbox_list.SetItemChecked(i, true);
            }
        }

        private void inbox_list_Click(object sender, EventArgs e)
        {
            if (inbox_list.Items.Count > 0)
            {
                try
                {
                    int email_index = Email_Count - Convert.ToInt32(inbox_list.SelectedItem.ToString().Substring(1, 3));
                    message_text.Text = Email_List[email_index.ToString()][3];
                    from_Text.Text = Email_List[email_index.ToString()][0];
                    date_Text.Text = Email_List[email_index.ToString()][2];
                    subject_text.Text = Email_List[email_index.ToString()][1];
                    onebox.Visible = true; twobox.Visible = true; threebox.Visible = true;
                }
                catch
                { /*Do nothing*/ }
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (trigger == 0)
            {
                int removed_total = 0;
                for (int i = (inbox_list.Items.Count); i > 0; i--)
                {
                    if (inbox_list.GetItemChecked(i - 1))
                    {
                        Console.WriteLine(Email_Count - 1 - i);
                        incoming.DeleteMessage(Email_Count - i);
                        inbox_list.Items.RemoveAt(i - 1);
                        Email_List.Remove((Email_Count - i).ToString());
                        removed_total++;
                    }
                }
                Email_Count = Email_Count - removed_total;
            }
            incoming.Logout();
            incoming.Disconnect();
            Get_Emails();
            message_text.Text = "";
            from_Text.Text = "";
            date_Text.Text = "";
            subject_text.Text = "";
            onebox.Visible = false; twobox.Visible = false; threebox.Visible = false;
        }

        private void reply_button_Click(object sender, EventArgs e)
        {
            if (from_Text.Text.Length > 4)
            {
                string query = "select emailaddress from d_active where employeenumber = '" + this.employee_number + "'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                try
                {
                    Console.WriteLine(reader[0].ToString().Trim());
                    SendEmail emailer = new SendEmail(this.employee_number, reader[0].ToString().Trim(), from_Text.Text.Trim(), "RE: " + subject_text.Text);
                    emailer.Show();
                    reader.Close();
                }
                catch { reader.Close(); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trigger = 1;
            delete_button.PerformClick();
            trigger = 0;
        }

        private void Inbox_Load(object sender, EventArgs e)
        {

        }
    }
}




/* 

TRIAL EMAIL RETRIEVER: do NOT USE UNLESS NECESSARY (1-2 MONTHS FREE) EAGETMAIL
 * 
// Grab emails using POP3 information
        private void Get_Emails() 
        {
            // Create a folder named "inbox" under current directory
            // to save the email retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox", curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            MailServer oServer = new MailServer("pop3.pathcom.com", "rli@etsdies.com", "5Zh2Pman", ServerProtocol.Pop3);
            MailClient oClient = new MailClient("TryIt");

            // If your POP3 server requires SSL connection,
            // Please add the following codes:
            // oServer.SSLConnection = true;
            // oServer.Port = 995;

            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                this.Email_Count = infos.Length;
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    //Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}", info.Index, info.Size, info.UIDL);

                    // Receive email from POP3 server
                    Mail oMail = oClient.GetMail(info);

                    //Console.WriteLine("From: {0}", oMail.From.ToString());
                    //Console.WriteLine("Subject: {0}\r\n", oMail.Subject);
                    //Console.WriteLine("Body: {0}\r\n", oMail.TextBody);

                    List<string> list;
                    if (!Email_List.TryGetValue(info.Index.ToString(), out list)) 
                    {
                        Email_List.Add(info.Index.ToString(), list = new List<string>());
                        list = Email_List[info.Index.ToString()];
                        list.Add(oMail.From.ToString());
                        list.Add(oMail.Subject.ToString());
                        list.Add(oMail.ReceivedDate.ToString());
                        list.Add(oMail.TextBody.ToString());
                    }

                    // Generate an email file name based on date time.
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new
                        System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                        mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                    oMail.SaveAs(fileName, true);

                    // Mark email as deleted from POP3 server.
                    //oClient.Delete(info);
                }
                // Quit and pure emails marked as deleted from POP3 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

            foreach (KeyValuePair<string, List<string>> pair in Email_List)
            {
                Console.WriteLine(pair.Key.ToString() + ":  ");
                foreach (string element in pair.Value)
                {
                    Console.WriteLine(element.ToString() + "  ");
                }
                Console.WriteLine("");
            }

        }

*/