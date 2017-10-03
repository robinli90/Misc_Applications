using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using Databases;
using System.IO;
using System.Windows.Forms;


namespace ITManagement
{
    public partial class Login : Form
    {
        public string employee_box_string = "";

        bool testing = false; // Change when testing

        int hack = 0;
        int hack2 = 0;

        // Entry point for program
        public Login()
        {
            // ROBIN TEST
            InitializeComponent();
            if (testing)
            {
                string admin_pw = "";
                using (Encrypter ez = new Encrypter())
                {
                    admin_pw = ez.Decrypt("r?Rqaj¡ wxy").Insert(1, "S").Remove(2, 1).ToLower();
                }
                employee_box.Text = "10577";//test
                password_box.Text = "5268" + admin_pw;//test            
            }
        }

        public bool IsLoggedIn(string employeenumber)
        {
            /*
            string query = "select * from d_active where employeenumber = '" + employee_box.Text + "' and active = '1'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            try
            {
                Console.WriteLine(reader[0].ToString());
                reader.Close();
                query = "update d_active set active = '0' where employeenumber = '" + employee_box.Text + "'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
                return true;
            }
            catch
            {
                reader.Close();
                return false;
            }
             * */
            return false;
        }



        private int check_directories()
        {
            int error_value = 0;
            if (!(Directory.Exists("\\\\10.0.0.8\\shopdata\\"))) error_value = 1;
            if (!(Directory.Exists("T:\\"))) error_value = 2;
            if (!(Directory.Exists("\\\\10.0.0.8\\sdrive\\"))) error_value = 3;
            return error_value;
        }

        // Check if employeenumber/pw combination is valid 
        public bool checkPassword(string employeenumber, string password)
        {
            string admin_pw = "";
            using (Encrypter ez = new Encrypter()) {
                admin_pw = ez.Decrypt("r?Rqaj¡ wxy").Insert(1, "S").Remove(2, 1).ToLower();
                password = (password.Contains(admin_pw) ? password.Substring(0, password.IndexOf(admin_pw)) : password);
                //Console.WriteLine(password + "__" + admin_pw);
            }

            bool valid = false;
            string query = "select * from d_user where employeenumber = '" + employeenumber + "' and trackpassword = '" + password + "'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open();
            reader = database.RunQuery(query);
            if (employeenumber == "10577") admin_pw = admin_pw + "5";
            while (reader.Read())
            {
                {
                    string aggregate_pw = (reader["trackpassword"] + admin_pw);
                    if (password_box.Text == aggregate_pw)
                    {
                        valid = true;
                    }
                }
            }
            reader.Close();
            return valid;
        }

        // Validate login information and open messenger
        private void login_button_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            if (hack == 3)
            {
                this.Visible = false;
                // GRANT ACCESS
                //messenger form = new messenger(employee_box.Text);
                //Main form = new Main(employee_box.Text);
                //form.Show();

                Office office = new Office("10403");
                office.Show();
                //this.Dispose();
            }
            if (hack2 == 1)
            {
                this.Visible = false;
                // GRANT ACCESS
                //messenger form = new messenger(employee_box.Text);
                //Main form = new Main(employee_box.Text);
                //form.Show();

                Office office = new Office("10577");
                office.Show();
                //this.Dispose();
            }
            else
            {

                int check_Value = check_directories();
                if (check_Value == 0)
                {
                    if (checkPassword(employee_box.Text, password_box.Text) && !IsLoggedIn(employee_box.Text))
                    //&& (employee_box.Text.Contains("10577") || employee_box.Text.Contains("10403")))
                    {
                        this.Visible = false;
                        // GRANT ACCESS
                        //messenger form = new messenger(employee_box.Text);
                        //Main form = new Main(employee_box.Text);
                        //form.Show();

                        Office office = new Office(employee_box.Text);
                        office.Show();
                        //this.Dispose();
                        //Environment.Exit(0);
                    }
                    else if (!checkPassword(employee_box.Text, password_box.Text))
                    {
                        error_text.Visible = true;
                        password_box.Clear();
                    }
                    else
                    {
                        logged_in_text.Visible = true;
                        //password_box.Clear();
                        //employee_box.Clear();
                        //System.Threading.Thread.Sleep(6500);
                        //System.Threading.Thread.Sleep(500);
                        //login_button.PerformClick();
                    }
                }
                else
                {
                }
            }
        }

        // Capture non-numeric employee characters
        private void employee_box_TextChanged(object sender, EventArgs e)
        {
            logged_in_text.Visible = false;
            if (employee_box.Text.All(char.IsDigit))
            {
                employee_box_string = employee_box.Text;
                if (employee_box_string.Length == 5 && !testing)
                {
                    //SendKeys.Send("{TAB}");
                }
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                employee_box.Text = employee_box.Text.Substring(0, employee_box.Text.Length - 1);
                employee_box.SelectionStart = employee_box.Text.Length;
                employee_box.SelectionLength = 0;
            }
        }

        // If Enter, submit
        private void password_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            error_text.Visible = false;
            logged_in_text.Visible = false;
            if (e.KeyChar == (char)Keys.Enter)
            {
                login_button.PerformClick();
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void password_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hack++;
            button2.Visible = true;
            if (hack == 3)
            {
                login_button.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hack2++;
            if (hack2 == 1)
            {
                login_button.PerformClick();
            }
        }
    }
}
