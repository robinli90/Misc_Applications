using System;
using Databases;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Monitor
{
    public partial class SendEmail : Form
    {
        private string current_employeenumber;
        private string current_email;
        List<string> Email_List = new List<string>();
        List<string> File_List = new List<string>();
        MailMessage mailmsg = new MailMessage();

        public SendEmail(string employeenumber, string email, string to_employee = "", string subject = "")
        {
            this.current_email = email;
            this.current_employeenumber = employeenumber;
            InitializeComponent();
            Encrypter @str = new Encrypter();
            this.from_text.Text = "From...  " + @str.Decrypt(this.current_email);
            this.to_text.Text = to_employee;
            this.subject_text.Text = subject;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Visible = false;
            this.Close();
        }

        private void save_settings_button_Click(object sender, EventArgs e)
        {
            EmailSettings settings = new EmailSettings(current_employeenumber);
            settings.Show();
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (to_text.Text.Length > 3)
            {
                get_email_list(to_text.Text);
                if (Email_List.Count > 0)
                {
                    string query = "select * from d_active where employeenumber = '" + this.current_employeenumber + "'";
                    ExcoODBC database = ExcoODBC.Instance;
                    OdbcDataReader reader;
                    database.Open(Database.DECADE_MARKHAM);
                    reader = database.RunQuery(query);
                    try
                    {
                        reader.Read();
                        Encrypter @str = new Encrypter();
                        string my_email = @str.Decrypt(reader[3].ToString().Trim());
                        string my_pw = @str.Decrypt(reader[4].ToString().Trim());
                        string my_smtp = reader[5].ToString().Trim();
                        int my_smtp_port = Convert.ToInt32(reader[7].ToString().Trim());
                        string my_sig = @str.Decrypt(reader[9].ToString().Trim());
                        foreach (string email in Email_List)
                        {
                            if (email.Contains("@") && email.Contains("."))
                            {
                                mailmsg.To.Add(email);
                            }
                        }
                        reader.Close();
                        MailAddress from = new MailAddress(my_email);
                        mailmsg.From = from;
                        mailmsg.Subject = subject_text.Text;
                        mailmsg.Body = message_text.Text + "\r\n\r\n" + my_sig;
                        SmtpClient client = new SmtpClient(my_smtp, my_smtp_port);
                        NetworkCredential credential = new NetworkCredential(my_email, my_pw);
                        client.Credentials = credential;

                        bool hasSend = false;
                        while (!hasSend)
                        {
                            try
                            {
                                client.Send(mailmsg);
                                hasSend = true;
                            }
                            catch
                            {
                                hasSend = false;
                            }
                        }
                        AlertBox alert = new AlertBox("Message sent.", "");
                        alert.Show();
                        alert.HideButton();
                        Visible = false;
                        Dispose();
                        Close();
                    }
                    catch
                    {
                        reader.Close();
                    }
                }
            }
        }

        private void get_email_list(string to_list_preparsed)
        {
            string email = "";
            int iteration = 0;
            Console.WriteLine(to_list_preparsed.Trim());
            string to_list_preparsed2 = to_list_preparsed + "x";
            foreach (char c in to_list_preparsed2.Trim())
            {
                if (c.ToString() == " " || iteration == to_list_preparsed2.Trim().Length-1)
                {
                    Email_List.Add(email);
                    email = "";
                    iteration++;
                }
                else if (c.ToString() != " ")
                {
                    email = email + c.ToString();
                    iteration++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.Filter = "Image files (*.mp3) | *.mp3";
            file.Title = "Add Attachments";
            file.Multiselect = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                foreach (string files in file.FileNames)
                {
                    File_List.Add(files);
                }
            }
            foreach (string file2 in File_List)
            {
                if (File.Exists(file2))
                {
                    mailmsg.Attachments.Add(new Attachment(file2));
                }
            }
        }
    }
}
