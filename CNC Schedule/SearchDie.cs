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
using System.Drawing.Printing;

namespace CNC_Schedule
{
    public partial class SearchDie : Form
    {
        // Variable to prevent multiple pages opening at once (with same order number)
        private string Last_Button_Pressed = "";
        private string SO_Number = "";
        private string Part_Type = "";
        private bool assigned = false;
        private List<List<string>> Die;
        private CNC_Form _parent;

        public SearchDie(CNC_Form form, List<List<string>> die, string part)
        {
            Die = die;
            _parent = form;
            Part_Type = part;
            InitializeComponent();
            //toDo_Create_Table(die);
        }

        // Button for all shop order numbers
        private void ordernumber_button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (Last_Button_Pressed == button.Name)
            {
                System.Threading.Thread.Sleep(500);
                Last_Button_Pressed = "";
            }
            else
            {
                string URL = "http://shoptrack/tracking/track.asp?id=" + button.Name.Substring(0, 6);
                Process.Start(URL);
                Last_Button_Pressed = button.Name;
            }
        }

        // Button for all shop order numbers
        private void assignment_button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Assign_Task AT = new Assign_Task(_parent, button.Name.Substring(0, 6), assigned, Part_Type);
            AT.ShowDialog();
        }

        // Add new row in toDo list
        private void die_Add_Row(string id, string ordernumber, string die_type, string diameter, string duedate, string station, string status, string assignedName, string onLathe, string doneLathe, string fasttrack)
        {
            Color due_date_color = Color.White;
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 4 : 3))
            {
                due_date_color = Color.Yellow;
            }
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 3 : 2))
            {
                due_date_color = Color.Red;
            }

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            Color background = Color.White;

            #region Shop order number button
            MyButton new_button = new MyButton();
            new_button.Location = new System.Drawing.Point(12, 12);
            new_button.Name = ordernumber + "_button";
            new_button.FlatStyle = FlatStyle.Flat;
            new_button.TabStop = false;
            new_button.FlatAppearance.BorderSize = 0;
            new_button.Size = new System.Drawing.Size(138, 23);
            if (fasttrack.ToLower().Contains("true"))
                new_button.ForeColor = Color.Red;
            new_button.TabIndex = 0;
            new_button.Font = st;
            new_button.Text = ordernumber.Substring(1, 5) + die_type;
            new_button.BackColor = background;
            new_button.UseVisualStyleBackColor = true;
            new_button.Click += new System.EventHandler(ordernumber_button_Click);
            #endregion

            #region Assignment  button
            MyButton assignment_button = new MyButton();
            assignment_button.Location = new System.Drawing.Point(12, 12);
            assignment_button.Name = ordernumber + "_button_assign";
            assignment_button.FlatStyle = FlatStyle.Flat;
            assignment_button.TabStop = false;
            assignment_button.FlatAppearance.BorderSize = 0;
            assignment_button.Size = new System.Drawing.Size(100, 23);
            assignment_button.TabIndex = 0;
            assignment_button.Font = (assignedName.Length > 0 ? bt : a);
            assignment_button.ForeColor = (assignedName.Length > 0 ? Color.Black : Color.LightGray);
            assignment_button.Text = (assignedName.Length > 0 ? assignedName : "Unassigned");
            assignment_button.UseVisualStyleBackColor = true;
            //assignment_button.Click += new System.EventHandler(assignment_button_Click);
            assignment_button.DoubleClick += new System.EventHandler(assignment_button_Click);
            #endregion

            string Lathe_Status = (doneLathe == "true" ? "X " : (onLathe == "true") ? "* " : "");

            die_table.RowCount = die_table.RowCount + 1;
            int row_count = die_table.RowCount - 1;
            //toDo_table.Controls.Add(new Label() { Font = f, Text = ordernumber.Substring(1, 5) + die_type }, 1, row_count);

            die_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), Font = ff, Text = id }, 0, row_count);
            die_table.Controls.Add(new_button, 1, row_count);
            die_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = diameter
            }, 2, row_count);
            die_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = Lathe_Status + Math.Round(Convert.ToDouble(duedate), 1).ToString(),
                BackColor = due_date_color
            }, 3, row_count);
            die_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = st,
                Text = station
            }, 4, row_count);
            die_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = (status == "true" ? "Ready" : ""),
                ForeColor = (status == "true" ? Color.Green : Color.Black)
            }, 5, row_count);
            die_table.Controls.Add(assignment_button, 6, row_count);

            die_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
        }

        // Create toDo table
        private void toDo_Create_Table(List<string> die)
        {
            
            assigned = (die[20] == "true" ? true : false);
            SO_Number = die[0];
            die_Add_Row("1", die[0], die[9], die[8], die[7], die[14], die[11], die[20], die[10], die[18], die[2]);
            die_table.PerformLayout();
            die_table.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 5)
            {
                SO_Number = "3" + textBox1.Text;
            }
            else
            {
                SO_Number = textBox1.Text;
            }        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            die_table.SuspendLayout();
            die_table.Visible = false;


            while (die_table.RowCount > 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    Control c = die_table.GetControlFromPosition(i, die_table.RowCount - 1);
                    die_table.Controls.Remove(c);

                }
                die_table.RowStyles.RemoveAt(die_table.RowCount - 1);
                die_table.RowCount = die_table.RowCount - 1;
            }

            die_table.ResumeLayout(false);

            try
            {
                textBox3.Text = "";
                toDo_Create_Table(Die[_parent.Get_Die_Master_Index(SO_Number)]);
            }
            catch
            {
                textBox3.Text = "Shop order not found";
                textBox3.ForeColor = Color.Red;
            }
        }
    }
}
