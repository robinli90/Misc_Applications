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
    public partial class messenger : Form
    {
        bool ready = true;
        public string employeenumber = "";
        public string from_conversation = "";
        public string to_conversation = "";
        public string from_employee_name_g = "";
        public string from_employee_number_g = "";
        public bool ready_to_send = false;
        string[,] conversation = new string[255, 6];
        int msg_id = 0, last_id = 0;
        List<string> alerted_people = new List<string>();
        List<string> conversations_added = new List<string>(); // Employee numbers in conversation to keep track of redundancies
        System.Windows.Forms.Timer conversation_check = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer marquee = new System.Windows.Forms.Timer();
        private Main _parent;

        public messenger(Main form1, string employeenumber)
        {
            InitializeComponent();
            _parent = form1;
            this.employeenumber = employeenumber;
            //testbox.Text = this.employeenumber;
            get_conversations();
            get_employee_list();
            marquee.Interval = 5000;
            marquee.Enabled = true;
            marquee.Tick += new EventHandler(TickProcess);
            conversation_check.Interval = 20000;
            conversation_check.Enabled = true;
            conversation_check.Tick += new EventHandler(update_Convos);
        }

        // When you unminimize window, seen conversation of current person
        protected virtual void this_OnActivated(object sender, EventArgs e)
        {
            if (ready_to_send)
            {
                string query = "update d_msgtable set didread = '1' where fromemp = '" + from_employee_number_g + "' and toemp = '" + this.employeenumber + "' and didread = '0'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database = ExcoODBC.Instance;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
            if (ready)
            {
                alerted_people.Clear();
                ready = false;
            }
        }

        // Disable typing in combobox for name selection
        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Conversation update class
        private void update_Convos(object sender, EventArgs e)
        {
            get_conversations();
            ready = true;
            if (ready_to_send)
            {
                if (is_active())
                {
                    active_text.Text = "Active";
                    active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                    active_text.Visible = true;
                }
                else
                {
                    active_text.Text = "Inactive";
                    active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                    active_text.Visible = true;
                }
            }
        }

        // Get all conversations (Active)
        private void get_conversations()
        {
            #region get both directions of to and from conversations
            string query = "select distinct toname, toemp from d_msgtable where fromemp = '" + this.employeenumber + "' or toemp = '" + this.employeenumber + "'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                if (!(reader[1].ToString().Trim() == this.employeenumber) && (!conversations_added.Contains(reader[1].ToString().Trim())))
                    add_conversation(reader[1].ToString().Trim(), reader[0].ToString().Trim());
            }
            reader.Close();
            query = "select distinct fromname, fromemp from d_msgtable where fromemp = '" + this.employeenumber + "' or toemp = '" + this.employeenumber + "'";
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                if (!(reader[1].ToString().Trim() == this.employeenumber) && (!conversations_added.Contains(reader[1].ToString().Trim())))
                    add_conversation(reader[1].ToString().Trim(), reader[0].ToString().Trim());
            }
            reader.Close();
            #endregion
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
                if (!conversations_added.Contains(reader[0].ToString().Trim())) 
                {
                    //string str = reader[0].ToString().Trim() + " - " + reader[1].ToString().Trim().ToUpper() + " " +  reader[2].ToString().Trim().ToUpper();
                    string str = reader[1].ToString().Trim().ToUpper() + " " + reader[2].ToString().Trim().ToUpper() + " - (" + reader[0].ToString().Trim() + ")";
                    nameBox.Items.Add(str);
                }
            }
            reader.Close();
            nameBox.SelectedIndex = 0;
        }

        // Close button to close screen
        private void close_button_Click(object sender, EventArgs e)
        {
            _parent.messenger_done();
            conversation_check.Dispose();
            marquee.Dispose();
            this.Visible = false;
            this.Close();
        }

        // Minimize functionality
        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        // Cleanse accidental or purposeful sql injection queries
        public bool passes_sql_check(string str)
        {
            if (str.Contains("select")) { MessageBox.Show("Invalid word/char detected: 'select'"); return false; }
            else if (str.Contains("drop")) { MessageBox.Show("Invalid word/char detected: 'drop'"); return false; }
            else if (str.Contains(";")) { MessageBox.Show("Invalid word/char detected: ';'"); return false; }
            else if (str.Contains("--")) { MessageBox.Show("Invalid word/char detected: '--'"); return false; }
            else if (str.Contains("insert")) { MessageBox.Show("Invalid word/char detected: 'insert'"); return false; }
            else if (str.Contains("delete")) { MessageBox.Show("Invalid word/char detected: 'delete'"); return false; }
            else if (str.Contains("xp_")) { MessageBox.Show("Invalid word/char detected: 'xp_'"); return false; }
            else if (str.Contains("update")) { MessageBox.Show("Invalid word/char detected: 'update'"); return false; }
            return true;
        }

        // Return and cleanse the message by escaping "'";
        public string cleanse_apostrophe(string str)
        {
            string return_string = "";
            foreach (char i in str)
            {
                if (i.ToString() == "'") return_string = return_string + "'";
                return_string = return_string + i.ToString();
            }
            return return_string;
        }

        // What to do in the background
        public void TickProcess(object sender, EventArgs e) 
        {
            if (ready_to_send)
            {
                refresh_conversation(from_employee_number_g, this.employeenumber);
            }
            if (has_unread_messages(this.employeenumber))
            {
                FlashWindowEx(this);
            }
        }

        // Retrieve name from employee number
        public string get_name(string number)
        {
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            string query = "select firstname from d_user where employeenumber = '" + number + "'";
            database = ExcoODBC.Instance;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            query = reader[0].ToString().Trim();
            reader.Close();
            return query;
        }

        // Add conversation button on the left side
        public void add_conversation(string employee_number, string employee_name) // From employeenumber/name
        {
                conversation_table.RowCount = 0;
                Button newButton = new Button();
                newButton.Text = "(" + employee_number + ") " + employee_name;
                newButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                newButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                newButton.Size = new System.Drawing.Size(140, 25);
                newButton.UseVisualStyleBackColor = true;
                newButton.Click += new System.EventHandler(this.show_conversation);
                conversation_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
                conversation_table.Controls.Add(newButton, 0, conversation_table.RowCount);
                conversation_table.RowCount = conversation_table.RowCount + 1;
                conversations_added.Add(employee_number);
        }

        // Get equiv spaces
        private string blank_lines(int line_count)
        {
            string spaces = "";
            for (int i = 0; i < line_count; i++)
            {
                spaces = spaces + "\r\n";
            }
            return spaces;
        }

        // Get employee number from button
        private string parse_emp_number(string str)
        {
            int startind = str.IndexOf("Text") + 7;
            return str.Substring(startind, 5);
        }

        // Format conversation window to fit
        private string[] format_conversation(string convo)
        {
            string[] g = new string[2];
            string rest_of_string = convo;
            string setup_String = "";
            int line_count = 1, letter_count = 0;
            while (rest_of_string.Length > 0)
            {
                if (letter_count > 23 && rest_of_string[0].ToString() == " ")
                {
                    setup_String = setup_String + "\r\n";
                    rest_of_string = rest_of_string.Substring(0);
                    letter_count = 0;
                    line_count++;
                }
                else if (letter_count > 25)
                {
                    if ((!setup_String.Contains("www.")) && (!setup_String.Contains("http")) && (!setup_String.Contains(".com")) && (!setup_String.Contains(".ca")) && (!setup_String.Contains(".org")) && (!setup_String.Contains("https")))
                    {
                        setup_String = setup_String + "-\r\n";
                    }
                    else
                    {
                        setup_String = setup_String + "\r\n";
                    }
                    rest_of_string = rest_of_string.Substring(0);
                    letter_count = 0;
                    line_count++;
                }
                if (rest_of_string.Length > 0)
                {
                    setup_String = setup_String + rest_of_string[0];
                    rest_of_string = rest_of_string.Substring(1);
                    letter_count++;
                }
            }
            g[0] = setup_String + "\r\n";
            g[1] = line_count.ToString();
            return g;
        }

        // Show conversation when first pressed on the button
        private void show_conversation(object sender, EventArgs e)
        {
            msg_chat_box.Enabled = true;
            bool @checked = false;
            ready_to_send = true;
            string from_employee_number;
            from_employee_number = parse_emp_number(sender.ToString());
            #region Get Conversation
            to_conversation = ""; from_conversation = "";
            msg_id = 0; last_id = 0;
            conversation = new string[255, 6];
            string query = "select top 10 fromemp, fromname, toemp, toname, msg, cast(msgtime as smalldatetime) as time from d_msgtable where (fromemp = '" +
                            from_employee_number + "' and toemp = '" + this.employeenumber + "') or (toemp = '" +
                            from_employee_number + "' and fromemp = '" + this.employeenumber + "') order by msgtime desc";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                conversation[msg_id, 0] = reader[0].ToString().Trim(); //from employee NUMBER
                conversation[msg_id, 1] = reader[1].ToString().Trim(); //from employee NAME
                conversation[msg_id, 2] = reader[2].ToString().Trim(); //to employee NUMBER
                conversation[msg_id, 3] = reader[3].ToString().Trim(); //to employee NAME

                // Decrypt
                Encrypter en = new Encrypter();
                string encrypted = reader[4].ToString().Trim();
                conversation[msg_id, 4] = en.Decrypt(encrypted); //message

                conversation[msg_id, 5] = reader[5].ToString().Trim(); //message TIME
                if (reader[0].ToString().Trim() == from_employee_number && !@checked)
                {
                    last_id = msg_id;
                    @checked = true;

                }
                msg_id++;
            }
            reader.Close();
            @checked = false;
            #endregion

            from_employee_name_g = get_name(from_employee_number);
            from_employee_number_g = from_employee_number;
            groupBox1.Text = from_employee_name_g;
            string[] wrap = new string[2];
            lastmsgrec_box.Text = "Last message recieved at: " + conversation[last_id, 5];

            if (conversation.Length > 0) {
                for (int i = msg_id-1; i >= 0; i--)//for (int i = 0; i < msg_id; i++)
                {
                    if (conversation[i, 0] == from_employee_number)
                    {

                        wrap = format_conversation(conversation[i, 4]);
                        from_conversation = from_conversation + wrap[0];
                        to_conversation = to_conversation + blank_lines(Convert.ToInt32(wrap[1]));
                    }
                    else
                    {
                        wrap = format_conversation(conversation[i, 4]);
                        to_conversation = to_conversation + wrap[0];
                        from_conversation = from_conversation + blank_lines(Convert.ToInt32(wrap[1]));
                    }
                }
                tobox.Clear();
                frombox.Clear();

                tobox.Text = to_conversation;
                frombox.Text = from_conversation;
            }

            // Active/inactive toggle
            if (is_active())
            {
                active_text.Text = "Active";
                active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(200)))), ((int)(((byte)(10)))));
                active_text.Visible = true;
            }
            else
            {
                active_text.Text = "Inactive";
                active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                active_text.Visible = true;
            }
        }

        // Send message by udpating decade
        private void msg_send_button_Click(object sender, EventArgs e)
        {
            if (ready_to_send && msg_chat_box.Text.Trim().Length > 0)
            {
                string query = "select firstname from d_user where employeenumber = '" + this.employeenumber + "'";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Read();
                string current_name = reader[0].ToString().Trim();
                reader.Close();

                // Encrypt
                Encrypter en = new Encrypter();
                string encrypted = en.Encrypt(cleanse_apostrophe(msg_chat_box.Text));

                query = "insert into d_msgtable (msg, fromemp, fromname, toemp, toname, msgtime, important, didread) values ('" +
                    encrypted + "', '" + this.employeenumber + "', '" + current_name + "', '" + from_employee_number_g + "', '" + from_employee_name_g +
                    "', '" + DateTime.Now.ToString() + "', '0', '0')";
                if (passes_sql_check(msg_chat_box.Text))
                {
                    database = ExcoODBC.Instance;
                    database.Open(Database.DECADE_MARKHAM);
                    reader = database.RunQuery(query);
                    refresh_conversation(from_employee_number_g, this.employeenumber);
                    msg_clear_button.PerformClick();
                    reader.Close();
                }
            }
        }

        // Press enter on chat, send automatically
        public void msg_chat_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                msg_send_button.PerformClick();
            }
        }

        // Every time you click chat box, update that you have seen message
        public void msg_chat_box_Click(object sender, EventArgs e)
        {        }

        // Clear button to clear text form
        public void msg_clear_button_Click(object sender, EventArgs e)
        {
            msg_chat_box.Text = "";
            msg_chat_box.Clear();
            System.Windows.Forms.SendKeys.Send("{BACKSPACE}");
        }

        // Refresh the current conversation
        private void refresh_conversation(string from_employee_number, string to_employee_number)
        {

            to_conversation = ""; from_conversation = "";
            msg_id = 0; 
            conversation = new string[255, 6];
            string query = "select top 10 fromemp, fromname, toemp, toname, msg, cast(msgtime as smalldatetime) as time from d_msgtable where (fromemp = '" +
                            from_employee_number + "' and toemp = '" + to_employee_number + "') or (toemp = '" +
                            from_employee_number + "' and fromemp = '" + to_employee_number + "') order by msgtime desc";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                conversation[msg_id, 0] = reader[0].ToString().Trim(); //from employee NUMBER
                conversation[msg_id, 1] = reader[1].ToString().Trim(); //from employee NAME
                conversation[msg_id, 2] = reader[2].ToString().Trim(); //to employee NUMBER
                conversation[msg_id, 3] = reader[3].ToString().Trim(); //to employee NAME

                // Decrypt
                Encrypter en = new Encrypter();
                string encrypted = reader[4].ToString().Trim();
                conversation[msg_id, 4] = en.Decrypt(encrypted); //message

                conversation[msg_id, 5] = reader[5].ToString().Trim(); //message TIME
                msg_id++;
            }

            reader.Close();
            query = "select firstname from d_user where employeenumber = '" + from_employee_number + "'";
            database = ExcoODBC.Instance;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            from_employee_name_g = reader[0].ToString().Trim(); ;
            reader.Close();
            from_employee_number_g = from_employee_number;
            groupBox1.Text = from_employee_name_g;
            string[] wrap = new string[2];
            lastmsgrec_box.Text = "Last message recieved at: " + conversation[last_id, 5];

            if (conversation.Length > 0)
            {
                for (int i = msg_id-1; i >= 0; i--)
                {
                    if (conversation[i, 0] == from_employee_number)
                    {

                        wrap = format_conversation(conversation[i, 4]);
                        from_conversation = from_conversation + wrap[0];
                        to_conversation = to_conversation + blank_lines(Convert.ToInt32(wrap[1]));
                    }
                    else
                    {
                        wrap = format_conversation(conversation[i, 4]);
                        to_conversation = to_conversation + wrap[0];
                        from_conversation = from_conversation + blank_lines(Convert.ToInt32(wrap[1]));
                    }
                }
                tobox.Clear();
                frombox.Clear();

                tobox.Text = to_conversation;
                frombox.Text = from_conversation;
            }
        }

        // Add conversation from dropdown list
        private void add_new_conversation(string to_employee_number)
        {
            if (!conversations_added.Contains(to_employee_number))
            {
                conversations_added.Add(to_employee_number);
                add_conversation(to_employee_number, get_name(to_employee_number));
            }
        }

        // Select user
        private void select_user_button_Click(object sender, EventArgs e)
        {
            if (nameBox.Text.Length > 5) //Something is selected
            {
                add_new_conversation(nameBox.Text.Substring(nameBox.Text.Length - 6, 5));
            }
        }

        //  Check if there are unread messages
        private bool has_unread_messages(string employeenumber)
        {
            string query = "select * from d_msgtable where toemp = '" + this.employeenumber + "' and didread = '0'";
            ExcoODBC database2 = ExcoODBC.Instance;
            OdbcDataReader reader2;
            database2 = ExcoODBC.Instance;
            database2.Open(Database.DECADE_MARKHAM);
            reader2 = database2.RunQuery(query);
            reader2.Read();
            try
            {
                Console.WriteLine(reader2[0].ToString());
                if ((!alerted_people.Contains(reader2[1].ToString().Trim())))
                {
                    AlertBox alert = new AlertBox("Message recieved from " + reader2[2].ToString().Trim(), employeenumber);
                    alerted_people.Add(reader2[1].ToString().Trim());
                    alert.Show();
                }
                reader2.Close();
                return true;
            }
            catch
            {
                reader2.Close();
                return false;
            }
        }

        private bool is_active()
        {
            string query = "select * from d_active where employeenumber = '" + from_employee_number_g + "' and active = '1'";
            ExcoODBC database2 = ExcoODBC.Instance;
            OdbcDataReader reader2;
            database2 = ExcoODBC.Instance;
            database2.Open(Database.DECADE_MARKHAM);
            reader2 = database2.RunQuery(query);
            reader2.Read();
            try
            {
                Console.WriteLine(reader2[0].ToString());
                reader2.Close();
                return true;
            }
            catch
            {
                reader2.Close();
                return false;
            }
        }

        private void nameBox_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                select_user_button.PerformClick();
            }
        }

        private void nameBox_Click(object sender, EventArgs e)
        {
            //select_user_button.PerformClick();
        }

    }
}