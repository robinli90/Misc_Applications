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


namespace Paging
{
    public partial class Login : Form
    {
        public string employee_box_string = "";

        bool testing = false; // Change when testing

        // Entry point for program
        public Login()
        {
            // ROBIN TEST
            InitializeComponent();
            //employee_box.Text = "10577";//test
            //password_box.Text = "";//test            
        }

        public bool IsLoggedIn(string employeenumber)
        {
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
        }



        private int check_directories()
        {
            int error_value = 0;
            //if (!(Directory.Exists("\\\\10.0.0.8\\shopdata\\"))) error_value = 1;
            //if (!(Directory.Exists("T:\\"))) error_value = 2;
            //if (!(Directory.Exists("\\\\10.0.0.8\\sdrive\\"))) error_value = 3;
            return error_value;
        }

        // Check if employeenumber/pw combination is valid 
        public bool checkPassword(string employeenumber, string password)
        {
            bool valid = false;
            string query = "select * from d_user where employeenumber = '" + employeenumber + "' and trackpassword = '" + password + "'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                valid = true;
            }
            reader.Close();
            return valid;
        }

        // Validate login information and open messenger
        private void login_button_Click(object sender, EventArgs e)
        {
            //Main form2 = new Main();
            //form2.Show();
            //this.Visible = false;
            int check_Value = check_directories();
            if (check_Value == 0)
            {
                if (checkPassword(employee_box.Text, password_box.Text) && !IsLoggedIn(employee_box.Text))
                {
                    // Try to check active, if not insert instead of update
                    //string query = "select * from d_active where employeenumber = '" + employee_box.Text + "'";
                    //ExcoODBC database = ExcoODBC.Instance;
                    //OdbcDataReader reader;
                    //database.Open(Database.DECADE_MARKHAM);
                    //eader = database.RunQuery(query);
                    //reader.Read();
                    try
                    {
                        //Console.WriteLine(reader[0].ToString());
                        //reader.Close();
                        //query = "update d_active set active = '1', lastactive = '" + DateTime.Now.ToString() + "' where employeenumber = '" + employee_box.Text + "'";
                        //database.Open(Database.DECADE_MARKHAM);
                        //reader = database.RunQuery(query);
                    }
                    catch
                    {
                       // reader.Close();
                        //query = "insert into d_active (employeenumber, active, lastactive) values ('" + employee_box.Text + "', '1', '" + DateTime.Now.ToString() + "')";
                        //database.Open(Database.DECADE_MARKHAM);
                        //reader = database.RunQuery(query);
                    }
                    //reader.Close();
                    this.Visible = false;
                    // GRANT ACCESS
                    //messenger form = new messenger(employee_box.Text);
                    //ready
                    Main form = new Main(employee_box.Text);
                    form.Show();
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
                    System.Threading.Thread.Sleep(500);
                    login_button.PerformClick();
                }
            }
            else
            {
                if (check_Value == 1)
                {
                    //AlertBox alert = new AlertBox("Missing S: Drive. Please map 10.0.0.8\\SHOPDATA to S: to continue", "");
                    //alert.HideButton();
                    //alert.Show();
                }
                else if (check_Value == 2)
                {
                    //AlertBox alert = new AlertBox("Missing T: Drive. Please map 10.0.0.8\\SHOPDATA to T: to continue", "");
                    //alert.HideButton();
                    //alert.Show();
                }
                else if (check_Value == 3)
                {
                    ////AlertBox alert = new AlertBox("Missing Z: Drive. Please map FILESRV\\sdrive to Z: to continue", "");
                    //alert.HideButton();
                    //alert.Show();
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
    }
}
