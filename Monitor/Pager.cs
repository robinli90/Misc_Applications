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

namespace Monitor
{
    public partial class Pager : Form
    {
        Main _parent;
        string employeenumber;

        public Pager(Main form1, string employeenumber1)
        {
            InitializeComponent();
            _parent = form1;
            this.employeenumber = employeenumber1;
            get_employee_list();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            _parent.pager_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private string parse_message(string str)
        {
            string return_str = "";
            foreach (char c in str)
            {
                if (c.ToString() == "'")
                {
                    return_str = return_str + "''";
                }
                else
                { 
                    return_str = return_str + c.ToString();
                }
            }
            return return_str;
        }

        // Process employee list for dropdown box
        private void get_employee_list()
        {
            string query = "select employeenumber, firstname, lastname from d_user where employeenumber <> '" + this.employeenumber + "' order by firstname";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database = ExcoODBC.Instance;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                string str = reader[1].ToString().Trim().ToUpper() + " " + reader[2].ToString().Trim().ToUpper() + " - (" + reader[0].ToString().Trim() + ")";
                nameBox.Items.Add(str);
            }
            reader.Close();
            nameBox.SelectedIndex = 0;
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (nameBox.Text.Length > 5 && message_text.Text.Length > 0) 
            {
                string important = "0";
                if (this.important.Checked) important = "1";

                // Get first name/last name of from_employee
                string query = "select firstname, lastname from d_user where employeenumber = '" + this.employeenumber + "'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database = ExcoODBC.Instance;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                string from_first = reader[0].ToString().Trim();
                string from_last = reader[1].ToString().Trim();
                reader.Close();

                // Get first name/last name of to_employee
                string to_employee_number = (nameBox.Text.Substring(nameBox.Text.Length - 6, 5));
                query = "select firstname, lastname from d_user where employeenumber = '" + to_employee_number + "'";
                database = ExcoODBC.Instance;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                string to_first = reader[0].ToString().Trim();
                string to_last = reader[1].ToString().Trim();
                reader.Close();

                Console.WriteLine(to_employee_number);
                query = "insert into internalpaging_old (FromEmp, FromF, FromL, ToEmp, ToF, ToL, Msg, Pagingtime, Status, RepStatus, Important, ForceToReply) values" +
                    " (" + employeenumber + ", '" + from_first + "', '" + from_last + "', " + to_employee_number + ", '" + to_first + "', '" + to_last + "', '"
                    + parse_message(message_text.Text) + "', '" + DateTime.Now.ToString() + "', 1, 0, " + important + ", 0)";
                Console.WriteLine(query);
                database = ExcoODBC.Instance;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
            AlertBox alert = new AlertBox("Page sent", "");
            alert.HideButton();
            alert.Show();
            close_button.PerformClick();
        }
    }
}

                           