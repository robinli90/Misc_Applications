using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databases;
using System.Data.Odbc;
using System.IO;
using System.Diagnostics;

namespace Exco_Hold_and_Release
{
    public partial class Login : Form
    {

        /// <summary>
        /// CLASS VARIABLES
        /// </summary>

        private string employee_number = string.Empty;
        private string login_password = string.Empty;
        private string external_parameter = string.Empty;

        public Login(string parameter, string shop_order_no, string employee_number)
        {

            if (parameter.Length > 0 && false)
            {
                //external_parameter = parameter;
                Checklist CL = new Checklist(parameter, shop_order_no, employee_number, "0"); // Department, SO NUMBER

                this.Close();
                //this.Dispose();
            }

            InitializeComponent();
        }

        private void employee_number_TextChanged(object sender, EventArgs e)
        {
            print_checklist.Visible = false;
            if (employee_box.Text.All(char.IsDigit))
            {
                if (employee_box.Text.Length == 5)
                {
                    if (employee_box.Text == "99999")
                    {
                        employee_box.Text = "";
                        MessageBox.Show("Cannot use '99999'");
                        print_checklist.Visible = false;
                    }
                    else
                    {
                        print_checklist.Visible = true;
                    }
                }
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                employee_box.Text = employee_box.Text.Substring(0, employee_box.Text.Length - 1);
                employee_box.SelectionStart = employee_box.Text.Length;
                employee_box.SelectionLength = 0;
                print_checklist.Visible = false;
            }
        }

        private void shop_order_TextChanged(object sender, EventArgs e)
        {
            if (shop_order.Text.All(char.IsDigit))
            {
                // Reset
                edit_button.Text = "Edit Form";
                edit_button.Enabled = false;
                hold_button.Enabled = false;
                release_button.Enabled = false;

                this.Size = new Size(251, 83);

                if (shop_order.Text.Length == 6)
                {
                    if (Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\On-hold\", "*" + shop_order.Text + "*").Count() >= 1)
                    {
                        this.Size = new Size(251, 131);
                        edit_button.Enabled = true;
                        hold_button.Enabled = false;
                        release_button.Enabled = true;

                        if (Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released\", "*" + shop_order.Text + "*").Count() >= 1)
                        {
                            //edit_button.Text = "Create Form";
                            //release_button.Enabled = true;
                        }
                    }
                    else
                    {
                        edit_button.Enabled = false;
                        hold_button.Enabled = true;
                        release_button.Enabled = false;
                        this.Size = new Size(251, 131);

                        //query 
                    }
                }
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                shop_order.Text = shop_order.Text.Substring(0, shop_order.Text.Length - 1);
                shop_order.SelectionStart = shop_order.Text.Length;
                shop_order.SelectionLength = 0;
            }

            //hold_button.Enabled = true;
        }

        private void release_button_Click(object sender, EventArgs e)
        {
            string URL = "http://10.0.0.6:8080/tracking/doReleaseChargeChk.asp?ordernumber=" + shop_order.Text + "&chgemp=" + employee_box.Text;
            Process.Start(URL);
            Checklist CL = new Checklist("SALES", shop_order.Text, employee_box.Text, "0");
            CL.Info_List["RELEASE_DATE"] = DateTime.Now.ToString();
            CL.Print_Check_Boxes = true;
            CL.External_Save();
            CL.printDocument2.Print();
            CL.Dispose();
            File.Move(@"\\10.0.0.8\rdrive\OnHold\On-hold\" + shop_order.Text + ".txt", @"\\10.0.0.8\rdrive\OnHold\Released\" + shop_order.Text + "_" + DateTime.Now.ToString("MM-dd-yyyy_HH-mm") + ".txt");
            MessageBox.Show("Job '" + shop_order.Text + "' has been released");
            shop_order.Text = "";
        }

        private void hold_button_Click(object sender, EventArgs e)
        {
            string URL = "http://10.0.0.6:8080/tracking/showjobhold.asp?ordernumber=" + shop_order.Text + "&chgemp=" + employee_box.Text;
            Process.Start(URL);
            Checklist CL = new Checklist("SALES", shop_order.Text, employee_box.Text, "0");
            CL.Show();
            shop_order.Text = "";
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Checklist CL = new Checklist("SALES", shop_order.Text, employee_box.Text, "0", "EDIT");
            CL.Show();
            shop_order.Text = "";
        }

        private void print_checklist_Click(object sender, EventArgs e)
        {
            print_checklist.Visible = false;
            employee_box.Visible = false;
            label1.Visible = false;
            label3.Visible = true;
            shop_order.Visible = true;
            shop_order.Focus();
        }


        private void password_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                print_checklist.PerformClick();
            }
        }
    }
}
