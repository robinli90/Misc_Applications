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
    public partial class EmailSettings : Form
    {
        private string current_employeenumber;

        public EmailSettings(string emp_number)
        {
            InitializeComponent();
            this.current_employeenumber = emp_number;
            string query = "select * from d_active where employeenumber = '" + emp_number + "'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            try
            {
                Encrypter @str = new Encrypter();
                email_text.Text = @str.Decrypt(reader[3].ToString().Trim());
                email_pw_text.Text = @str.Decrypt(reader[4].ToString().Trim());
                email_repeat_pw_text.Text = email_pw_text.Text;
                smtp_text.Text = reader[5].ToString().Trim();
                pop3_text.Text = reader[6].ToString().Trim();
                pop3_port_text.Text = reader[8].ToString().Trim();
                smtp_port_text.Text = reader[7].ToString().Trim();
                signature_text.Text = @str.Decrypt(reader[9].ToString().Trim());
                reader.Close();
            }
            catch
            {
                reader.Close();
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void test_email_button_Click(object sender, EventArgs e)
        {
            //test_email_button.Enabled = false;
            if (email_pw_text.Text != email_repeat_pw_text.Text)
            {
                error_text.Visible = true;
            }
            else
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
                    MailMessage mailmsg = new MailMessage();
                    mailmsg.To.Add(my_email);
                    MailAddress from = new MailAddress(my_email);
                    mailmsg.From = from;
                    mailmsg.Subject = "Monitor test email message @ " + DateTime.Now.ToString();
                    mailmsg.Body = "Test body. The following (if any) is your signature: " + "\r\n\r\n" + my_sig;
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

                    AlertBox alert = new AlertBox("A test email has been sent to yourself", "");
                    alert.Show();
                    alert.HideButton();
                }
                catch
                {
                }
                reader.Close();
            }
        }

        private void save_settings_button_Click(object sender, EventArgs e)
        {
            if (email_text.Text.Contains(".") &&
                email_text.Text.Contains("@") &&
                email_text.Text.Length > 5 &&
                email_pw_text.Text == email_repeat_pw_text.Text &&
                pop3_text.Text.Length > 4 &&
                smtp_port_text.Text.Length > 0 &&
                smtp_text.Text.Length > 4)
            {
                Encrypter @str = new Encrypter();
                string query = "update d_active set emailaddress = '" + @str.Encrypt(email_text.Text) + "', " +
                                                   "password = '" + @str.Encrypt(email_pw_text.Text) + "', " +
                                                   "SMTP = '" + smtp_text.Text + "', " +
                                                   "POP3 = '" + pop3_text.Text + "', " +
                                                   "smtpport = '" + smtp_port_text.Text + "', " +
                                                   "pop3port = '" + pop3_port_text.Text + "', " +
                                                   "Signature = '" + @str.Encrypt(signature_text.Text) + "' where employeenumber = '" + this.current_employeenumber + "'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
                this.Visible = false;
                this.Dispose();
                this.Close();
            }
            else if (email_pw_text.Text != email_repeat_pw_text.Text)
            {
                error_text.Visible = true;
            }
        }
    }
}
