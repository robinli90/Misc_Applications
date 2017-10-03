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


namespace Paging
{
    public partial class Main : Form
    {
        
        // Update timer
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();

        private List<List<string>> Employee_List = new List<List<string>>(); // empnum, empfirstname, emplastname

        private List<List<string>> Message_List = new List<List<string>>(); // pagingtime, msg, status, replymsg, tofirst, fromfirst, important

        private string Filter_Out_Users = " 88888 10118 10403 10283 10327 10171 10079 10078 10065 10592 10151 10230 10223 10215 10095 " +
                                          "10170 22015 10194 10099 10128 10346 10284 20099 10523 " +
                                          "10565 10558 10030 10178 10158 10550 22019 20140 10602";

        private string fromEmpNum = "";
        private string fromEmpName = "";
        private string toEmpNum = "";
        private string toEmpName = "";

        private string Get_Employee_Number(string First_Name, string Last_Name)
        {
            foreach (List<string> Employee in Employee_List)
            {
                if (Employee[1].Contains(First_Name) && Employee[2].Contains(Last_Name))
                {
                    return Employee[0];
                }
            }
            return "";
        }


        // Main Update function
        private void Update(object sender, EventArgs e)
        {
            
            Get_Message_List();
            Get_Messages();
        }

        // Returns first/last name in list
        private List<string> Get_Employee_FullName(string Emp_Num)
        {
            foreach (List<string> Employee in Employee_List)
            {
                if (Employee[0].Trim() == Emp_Num.Trim())
                {
                    return new List<string> {Employee[1], Employee[2]};
                }
            }
            //return new List<string> {"ERROR", " IN NAME"};
            return new List<string> {"Anybody", "Name Error"};
        }

        public Main(string emp="10403")
        {


            InitializeComponent();

            tick_update.Interval = 60000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(Update);

            Get_Employee_List();

            fromEmpNum = emp;
            fromEmpName = Get_Employee_FullName(fromEmpNum)[0];

            displayListBox.Items.Add("All");
            show_count.Items.Add("25"); show_count.Items.Add("50"); show_count.Items.Add("100"); show_count.Items.Add("200");
            show_count.Text = "100";


            fromBox.Text = Get_Employee_FullName(fromEmpNum)[0] + " " + Get_Employee_FullName(fromEmpNum)[1];

            foreach (List<string> Employee in Employee_List)
            {
                if (Filter_Out_Users.Contains(Employee[0]))
                    nameListBox.Items.Add(Employee[1] + " " + Employee[2]);


                displayListBox.Items.Add(Employee[1] + " " + Employee[2]);
            }


            displayListBox.Text = "All";

            msg_table.Visible = false;

            displayListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            displayListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            displayListBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            nameListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            nameListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            nameListBox.AutoCompleteSource = AutoCompleteSource.ListItems;

            Get_Message_List();
            Get_Messages();
            //msg_table_Create_Table();
            
        }

        private void Get_Employee_List()
        {
            string employee_query = "select employeenumber, firstname, lastname from d_user order by firstname asc";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(employee_query);

            while (reader.Read())
            {
                List<string> temp = new List<string>();
                temp.Add(reader[0].ToString().Trim()); //emp num
                temp.Add(reader[1].ToString().Trim()); //first name
                temp.Add(reader[2].ToString().Trim()); //last name
                Employee_List.Add(temp);
            }
            reader.Close();
        }

        private void Get_Message_List()
        {
            Message_List = new List<List<string>>();
            string employee_query = "select top 1500 Pagingtime,Msg,Status,ReplyMsg,Toemp,fromemp,Important from internalpaging_old where FromEmp = '" + fromEmpNum + "' or toemp = '" + fromEmpNum + "' order by ID desc";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(employee_query);

            while (reader.Read())
            {
                List<string> temp = new List<string>();
                temp.Add(reader[0].ToString().Trim()); //Pagingtime
                temp.Add(reader[1].ToString().Trim()); //Msg
                temp.Add(reader[2].ToString().Trim()); //Status
                temp.Add(reader[3].ToString().Trim()); //ReplyMsg
                temp.Add(reader[4].ToString().Trim()); //ToEmpNum
                temp.Add(reader[5].ToString().Trim()); //FromEmpNum
                temp.Add(reader[6].ToString().Trim()); //Important
                Message_List.Add(temp);
            }
            reader.Close();
        }

        private void Get_Messages()
        {
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sasn Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            int row_count = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            foreach (List<string> Message in Message_List)
            {

                string[] temp = displayListBox.Text.Split(new string[] { " " }, StringSplitOptions.None);
                string displayEmpNum1 = Get_Employee_Number(temp[0], temp[temp.Length - 1]);

                // DataGridViewButtonCell myButton = new DataGridViewButtonCell();
                // If message from current user
                if (row_count < Convert.ToInt32(show_count.Text) && sent_check.Checked && Message[5] == fromEmpNum && (displayListBox.Text == "All" ? true : displayEmpNum1 == Message[4]))
                {
                    if (row_count == 0) { dataGridView1.Columns[1].HeaderCell.Value = "To"; label1.Text = "Display Pages to: "; }
                    string Employee_FN = Get_Employee_FullName(Message[4])[0] + " " + Get_Employee_FullName(Message[4])[1];
                    //dataGridView1.Rows.Add("1", myButton, "3", "4asdfasdfaasdf aasdfaasd", "faasdfaasdfaasdfaasd faasdfaasdfaasdfaasdfaasdf aasdfaasdfaasdfaasdf aasdfaasdfaasdfaas dfaasdfaa sdfaasdfaa sdfaasdfaasdfaas dfaasdfaa sdfaasdfa asdf", "5");
                    dataGridView1.Rows.Add(
                        Message[2] == "0" ? "Got" : "",
                        Employee_FN,
                        Message[0],
                        Message[1],
                        Message[3],
                        Message[6] == "1" ? "Important" : ""
                        );
                    // Highlight important messages
                    if (Message[6] == "1")
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[1].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[2].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[3].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[4].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[5].Style.BackColor = Color.LightBlue;
                    }
                    // Highlight got messages
                    if (Message[2] == "0")
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.Yellow;
                    }
                    row_count++;
                }
                else if (row_count < Convert.ToInt32(show_count.Text) && received_check.Checked && Message[4] == fromEmpNum && (displayListBox.Text == "All" ? true : displayEmpNum1 == Message[5]))
                {
                    if (row_count == 0) { dataGridView1.Columns[1].HeaderCell.Value = "From"; label1.Text = "Display Pages from: "; }
                    string Employee_FN = Get_Employee_FullName(Message[5])[0] + " " + Get_Employee_FullName(Message[5])[1];
                    //dataGridView1.Rows.Add("1", myButton, "3", "4asdfasdfaasdf aasdfaasd", "faasdfaasdfaasdfaasd faasdfaasdfaasdfaasdfaasdf aasdfaasdfaasdfaasdf aasdfaasdfaasdfaas dfaasdfaa sdfaasdfaa sdfaasdfaasdfaas dfaasdfaa sdfaasdfa asdf", "5");
                    dataGridView1.Rows.Add(
                        Message[2] == "0" ? "Got" : "",
                        Employee_FN,
                        Message[0],
                        Message[1],
                        Message[3],
                        Message[6] == "1" ? "Important" : ""
                        );
                    //dataGridView1.Rows[row_count].Cells[1].Value = "test";
                    //dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.Red;
                    // Highlight important messages
                    if (Message[6] == "1")
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[1].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[2].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[3].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[4].Style.BackColor = Color.LightBlue;
                        dataGridView1.Rows[row_count].Cells[5].Style.BackColor = Color.LightBlue;
                    }
                    // Highlight got messages
                    if (Message[2] == "0")
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        dataGridView1.Rows[row_count].Cells[0].Style.BackColor = Color.Yellow;
                    }
                    row_count++;
                }
            }
        }

        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void dgvSomeDataGridView_SelectionChanged(Object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                MessageBox.Show(e.RowIndex.ToString());
                //TODO - Button Clicked - Execute Code Here
            }
        }

        private void important_button(object sender, EventArgs e)
        {
            var button = (Button)sender;
            MessageBox.Show(button.Name);
        }

        private void sent_check_CheckedChanged(object sender, EventArgs e)
        {
            if (sent_check.Checked)
            {
                sent_check.Enabled = false;
                received_check.Enabled = true;
                received_check.Checked = false;
                Get_Message_List();
                Get_Messages();
            }
        }

        private void received_check_CheckedChanged(object sender, EventArgs e)
        {
            if (received_check.Checked)
            {
                received_check.Enabled = false;
                sent_check.Enabled = true;
                sent_check.Checked = false;
                Get_Message_List();
                Get_Messages();
            }
        }

        private void nameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] temp = nameListBox.Text.Split(new string[] { " " }, StringSplitOptions.None);
            toEmpName = temp[0];
            toEmpNum = Get_Employee_Number(temp[0], temp[temp.Length - 1]);
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (messageBox.Text.Length > 0 && nameListBox.Text.Length > 0)
            {
                //page
                string query = "";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);

				if (important.Checked)
                {
                    query = "insert into internalpaging_old values ('" + fromEmpNum + "','" + fromEmpName + "','" + Get_Employee_FullName(fromEmpNum)[1] + "','" + toEmpNum + "','" + toEmpName + "','" + Get_Employee_FullName(toEmpNum)[1] + "','" + messageBox.Text + "',GetDate(),NULL,NULL,'1','0','1','0')";
                }
				else
                {
                    query = "insert into internalpaging_old values ('" + fromEmpNum + "','" + fromEmpName + "','" + Get_Employee_FullName(fromEmpNum)[1] + "','" + toEmpNum + "','" + toEmpName + "','" + Get_Employee_FullName(toEmpNum)[1] + "','" + messageBox.Text + "',GetDate(),NULL,NULL,'1','0','0','0')";
                }
                reader = database.RunQuery(query);
                reader.Close();

                messageBox.Text = "";
                pageSent.Visible = true;
            }
        }

        private void messageBox_TextChanged(object sender, EventArgs e)
        {
            pageSent.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void displayListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayListBox.Text == "All")
            {
                displayEmpName = "All";
                displayEmpNum = "All";
            }
            else
            {
                string[] temp = displayListBox.Text.Split(new string[] { " " }, StringSplitOptions.None);
                displayEmpName = temp[0];
                displayEmpNum = Get_Employee_Number(temp[0], temp[temp.Length - 1]);
            }
            Get_Message_List();
            Get_Messages();
        }

        private void show_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Message_List();
            Get_Messages();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Message_List();
            Get_Messages();
        }
        /*
        private void msg_table_Add_Row(bool got_message, string from, string date, string message, string reply_message, bool important)
        {
            int row_count = msg_table.RowCount - 1;

            #region Fonts
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            MyButton new_button = new MyButton();
            new@Pimp_Down Hi, I would like to buy your Kingsguard Conquest Chainmail listed for 50 chaos in Perandus (stash tab "~b/o 50 chaos")button.Location = new System.Drawing.Point(12, 12);
            new_button.Name = row_count.ToString();
            new_button.FlatStyle = FlatStyle.Flat;
            new_button.TabStop = false;
            new_button.FlatAppearance.BorderSize = 0;
            //new_button.Size = new System.Drawing.Size(138, 23);
            new_button.TabIndex = 0;
            new_button.Font = f;
            new_button.Text = important ? "UnMark" : "Mark";
            new_button.UseVisualStyleBackColor = true;
            new_button.Click += new System.EventHandler(important_button);


            // Got Message
            msg_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Font = f,
                Text = (got_message == true ? "Got" : ""),
                BackColor = (got_message == true ? Color.Green : Color.White)
            }, 0, row_count);

            // From 
            msg_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Font = f,
                Text = from
            }, 1, row_count);

            // Date 
            msg_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Font = f,
                Text = date
            }, 2, row_count);

            // Message 
            msg_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Font = f,
                Text = message
            }, 3, row_count);

            // Reply message 
            msg_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Font = f,
                Text = reply_message
            }, 4, row_count);

            // Important 
            msg_table.Controls.Add(new_button, 5, row_count);

            msg_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

        }

        // Create machineStatus table
        private void msg_table_Create_Table()
        {
            msg_table.SuspendLayout();
            msg_table.Visible = false;


            for (int j = 1; j >= 0; j--)
            {
                for (int i = 0; i < 5; i++)
                {
                    Control c = msg_table.GetControlFromPosition(i, j);
                    msg_table.Controls.Remove(c);
                }
                try
                {
                    msg_table.RowStyles.RemoveAt(j);
                }
                catch
                {
                }
                //machineStatus_table.RowCount = j-1;
            }

            msg_table.ResumeLayout(false);


            msg_table_Add_Row(true, "Robin", "4/5/2016 11:47:00 AM", "Test meage BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH BLAH", "No reply", true);

            msg_table.PerformLayout();
            msg_table.Visible = true;

        }
        */
    }


    public partial class MyButton : Button
    {

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //omit base.OnPaint completely...

            //base.OnPaint(pevent); 

            using (Pen p = new Pen(BackColor))
            {
                pevent.Graphics.FillRectangle(p.Brush, ClientRectangle);
            }

            //add code here to draw borders...

            using (Pen p = new Pen(ForeColor))
            {
                pevent.Graphics.DrawString(base.Text, Font, p.Brush, new PointF(8, 0));
            }
        }

        public MyButton()
        {
            //if (base.Text.StartsWith("3") || base.Text.StartsWith("1") || base.Text.StartsWith("2") || base.Text.StartsWith("4") || base.Text.StartsWith("5") || base.Text.StartsWith("6") || base.Text.StartsWith("7") || base.Text.StartsWith("8") || base.Text.StartsWith("9") || base.Text.StartsWith("0"))
            //if (!(base.Name.Contains("assign")))
            //{
            //}
            //else
            //{
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            //}
        }
    }
}
