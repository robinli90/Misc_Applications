using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using Databases;
using System.Data.Odbc;
using System.Drawing.Printing;
using System.IO;

namespace ITManagement
{
    public partial class IT_Information : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            //if (e.CloseReason == CloseReason.WindowsShutDown)
            _STORE_INFO_LIST();
            _parent._Information_Open = false;
            _parent._STORE_COMPUTER_INFO();
            _parent.Focus();
            //System.Threading.Thread.Sleep(200);
            //Environment.Exit(0);
        }

        private Office _parent;
        private string employee_number = "";
        private int selected_tab = 0;
        private int edit_index = -1;
        private int print_index = 1;
        private string current_view_string = string.Empty;
        List<string> print_list = new List<string>();

        // Printing process
        Process _process = new Process();

        // Key = tab name (topic name)   VALUE = List of subjects:
        //                                       List<string> where string = SUBJECT~EMPLOYEE#~INFORMATION
        Dictionary<string, List<string>> _INFO_LIST = new Dictionary<string, List<string>>();
        Dictionary<string, string> _EMPLOYEE_LIST = new Dictionary<string,string>();

        public IT_Information(Office parent, string information, string empnum)
        {
            InitializeComponent();
            _parent = parent;

            employee_number = empnum;
            Get_Employee_List();
            Populate_Dictionary_Info(information);
            Populate_Tab_Info();

            if (_INFO_LIST.Count > 0)
            {
                topic_drop_list.Text = tabControl1.TabPages[0].Text;
            }

            if (_parent._ENABLE_ADMINISTRATIVE_COMMANDS)
            {
                delete_topic.Enabled = true;
                delete_topic.Visible = true;
            }

            info_box.ScrollBars = ScrollBars.Vertical;
            info_box.KeyDown += new KeyEventHandler(textBox1_KeyDown);

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;

        }   

        private void Populate_Dictionary_Info(string information)
        {
            string[] temp = information.Split(new string[] { "█" }, StringSplitOptions.None);
            int i = 0;
            foreach (string z in temp)
            {
                if (z.Length > 1)
                {
                    string[] temp2 = z.Split(new string[] { "~" }, StringSplitOptions.None);
                    if (_INFO_LIST.ContainsKey(temp2[0]))
                    {
                        List<string> g = new List<string>(_INFO_LIST[temp2[0]]);
                        g.Add(temp2[1] + "~" + temp2[2] + "~" + temp2[3] + (temp2.Length == 5 ? "~" + temp2[4] : ""));
                        _INFO_LIST[temp2[0]] = g;
                    }
                    else
                    {
                        List<string> g = new List<string>();
                        g.Add(temp2[1] + "~" + temp2[2] + "~" + temp2[3] + (temp2.Length == 5 ? "~" + temp2[4] : ""));
                        _INFO_LIST.Add(temp2[0], g);
                        topic_drop_list.Items.Add(temp2[0]);
                    }
                }
                i++;
            }
        }

        private void Get_Employee_List()
        {
            _EMPLOYEE_LIST = _parent._EMPLOYEE_LIST;
        }

        private void Populate_Tab_Info()
        {
            _parent._RESET_INACTIVITY();
            if (selected_tab < 0) selected_tab = 0;
            int pre_selected_tab = selected_tab;
            int gg = tabControl1.TabPages.Count;
            for (int i = 0; i < gg; i++)
            {
                tabControl1.TabPages.RemoveAt(0);
            }
            foreach (KeyValuePair<string, List<string>> g in _INFO_LIST)
            {
                RichTextBox new_box = new RichTextBox();
                new_box.Location = new System.Drawing.Point(0, 0);
                new_box.Name = "richTextBox1";
                //new_box.Size = new System.Drawing.Size(892, 100);
                //new_box.Size = new System.Drawing.Size(892, (tabControl1.Location.Y + tabControl1.Size.Height) - 405);
                //new_box.Multiline = true;
                //MessageBox.Show(tabControl1.Size.Height.ToString());
                new_box.Size = new System.Drawing.Size(tabControl1.Size.Width - 10, tabControl1.Size.Height - 26);
                new_box.WordWrap = true;
                new_box.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                new_box.ReadOnly = true;
                new_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top 
                                    | System.Windows.Forms.AnchorStyles.Bottom) 
                                    | System.Windows.Forms.AnchorStyles.Left))
                                  );
                new_box.TabStop = false;


                int index = 0;
                foreach (string z in g.Value)
                {
                    if ((search_box.Checked && z.ToLower().Contains(search_text_box.Text.ToLower())) || !search_box.Checked)
                    {
                        string[] temp = z.Split(new string[] { "~" }, StringSplitOptions.None);
                        new_box.AppendText("[Entry#: " + index + "] - " + temp[0] + " (" + temp[1] + ")", Color.Navy, true);
                        new_box.AppendText("", Color.Black, false);
                        new_box.AppendText(temp[2], Color.Black, false);
                        new_box.AppendText("", Color.Black, false);
                        if (temp.Length == 4 && show_edit_history.Checked) // if has edit
                        {
                            new_box.AppendText("Edited:" + temp[3].Trim(Convert.ToChar(",")), Color.Red, false);
                            new_box.AppendText("", Color.Black, false);
                        }
                        //_EMPLOYEE_LIST["test"] = "";
                    }
                    index++;
                }

                if (new_box.Text.Length > 5)
                {
                    tabControl1.TabPages.Add((g.Key.ToString()));
                    tabControl1.TabPages[tabControl1.TabCount - 1].Controls.Add(new_box);
                    new_box.Parent = tabControl1.TabPages[tabControl1.TabCount - 1];
                }
                //.Text = return_string;
            }

            if (tabControl1.TabPages.Count > 0)
            {   
                try
                {
                    tabControl1.SelectedTab = tabControl1.TabPages[pre_selected_tab];
                    topic_drop_list.Text = tabControl1.SelectedTab.Text;
                }
                catch
                {
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                    edit_index_box.Text = "";
                }
            }
        }

        /// <summary>
        /// Store info into main file by passing store into parent and executing store function
        /// </summary>
        private void _STORE_INFO_LIST()
        {
            string return_string = "";
            foreach (KeyValuePair<string, List<string>> g in _INFO_LIST)
            {
                //return_string = return_string +  + "~";
                foreach (string z in g.Value)
                {
                    return_string = return_string + g.Key + "~" + z + "█";
                }
                //return_string = return_string.Trim(Convert.ToChar("█"));
                //return_string = return_string + "█";
            }
            _parent._STORE_SETUP_INFORMATION(0, return_string.Trim(Convert.ToChar("█")));
        }

        private void transfer_button_Click(object sender, EventArgs e)
        {
            
            if (topic_drop_list.Text.Length < 1)
            {
                MessageBox.Show("Error: Missing topic");
            }
            else if (subject_box.Text.Length < 1)
            {
                MessageBox.Show("Error: Missing subject line");
            }
            else if (info_box.Text.Length < 1)
            {
                MessageBox.Show("Error: Missing information");
            }
            else if (topic_drop_list.Text.Contains("~") || subject_box.Text.Contains("~") || info_box.Text.Contains("~"))
            {
                MessageBox.Show("Error: Invalid character detected '~'");
            }
            else
            {
                _parent._APPEND_TO_SETUP_INFORMATION(2, "IT Information " + (edit_index >= 0 ? "edited " : "added ") + "(Topic: " + topic_drop_list.Text + ", Subject: " + subject_box.Text + (edit_index >= 0 ? ", Entry#: " + edit_index : "") + ") by " + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER]);
                // If not edit
                if (edit_index < 0)
                {
                    if (_INFO_LIST.ContainsKey(topic_drop_list.Text))
                    {
                        List<string> g = new List<string>(_INFO_LIST[topic_drop_list.Text]);
                        g.Add(subject_box.Text + "~" + _EMPLOYEE_LIST[employee_number] + " on " + DateTime.Now.ToShortDateString() + "~" + info_box.Text);
                        _INFO_LIST[topic_drop_list.Text] = g;
                        selected_tab = Get_IndexOf_Tab(topic_drop_list.Text);
                        subject_box.Text = "";
                        info_box.Text = "";
                        Populate_Tab_Info();
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Topic '" + topic_drop_list.Text + "' does not exist. Create it?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            List<string> g = new List<string>();
                            g.Add(subject_box.Text + "~" + _EMPLOYEE_LIST[employee_number] + " on " + DateTime.Now.ToShortDateString() + "~" + info_box.Text);
                            _INFO_LIST.Add(topic_drop_list.Text, g);
                            topic_drop_list.Items.Add(topic_drop_list.Text);
                            selected_tab = tabControl1.TabPages.Count;
                            topic_drop_list.Text = tabControl1.SelectedTab.Text;
                            subject_box.Text = "";
                            info_box.Text = "";
                            Populate_Tab_Info();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }

                    }
                }
                else // if edit
                {
                    string edit = "";
                    if (_INFO_LIST[topic_drop_list.Text][edit_index].Split(new string[] { "~" }, StringSplitOptions.None).Length == 4)
                    {
                        edit = "~ " + _INFO_LIST[topic_drop_list.Text][edit_index].Split(new string[] { "~" }, StringSplitOptions.None)[3] + " " + _EMPLOYEE_LIST[employee_number] + " on " + DateTime.Now.ToShortDateString() + ",";
                    }
                    else
                    {
                        edit = "~ " + _EMPLOYEE_LIST[employee_number] + " on " + DateTime.Now.ToShortDateString() + ",";
                    }
                    _INFO_LIST[topic_drop_list.Text][edit_index] = (subject_box.Text + "~" + _INFO_LIST[topic_drop_list.Text][edit_index].Split(new string[] { "~" }, StringSplitOptions.None)[1] + "~" + info_box.Text + edit);
                    edit_index_box.Text = "";

                    subject_box.Text = "";
                    info_box.Text = "";
                    Populate_Tab_Info();
                }
            }
        }

        private int Get_IndexOf_Tab(string tabName)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Text == tabName)
                {
                    return i;
                }
            }
            return 0;
        }

        private void subject_box_TextChanged(object sender, EventArgs e)
        {
            if (subject_box.Text.EndsWith("~"))
            {
                subject_box.Text = subject_box.Text.Substring(0, subject_box.Text.Length - 1);
                subject_box.SelectionStart = subject_box.Text.Length;
                subject_box.SelectionLength = 0;
            }
        }

        private void info_box_TextChanged(object sender, EventArgs e)
        {
            if (info_box.SelectionStart >= info_box.Text.Length - 1)
            {
                info_box.ScrollToCaret();
            }
            if (info_box.Text.EndsWith("~"))
            {
                info_box.Text = info_box.Text.Substring(0, info_box.Text.Length - 1);
                info_box.SelectionStart = info_box.Text.Length;
                info_box.SelectionLength = 0;
            }
            if (info_box.Text.Length % 70 == 0)
            {
                //Populate_Tab_Info();
                //_parent._RESET_INACTIVITY();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_tab = tabControl1.SelectedIndex;
            try
            {
                topic_drop_list.Text = tabControl1.SelectedTab.Text;
            }
            catch
            {
            }
        }

        private void delete_topic_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(selected_tab);
            if (selected_tab < 0) selected_tab = 0;
            _INFO_LIST.Remove(tabControl1.TabPages[selected_tab].Text);
            if (tabControl1.TabPages.Count - 1 == selected_tab && tabControl1.TabPages.Count > 1)
                selected_tab--;
            Populate_Tab_Info();
        }

        private void search_box_CheckedChanged(object sender, EventArgs e)
        {
            Populate_Tab_Info();
            if (!search_box.Checked) search_text_box.Text = "";
        }

        private void search_text_box_TextChanged(object sender, EventArgs e)
        {
            if (search_text_box.Text.Length == 0)
            {
                search_box.Checked = false;
            }
            else
            {
                search_box.Checked = true;
                Populate_Tab_Info();
            }
            TextBox tbx = this.Controls.Find("search_text_box", true).FirstOrDefault() as TextBox;
            tbx.Focus();
        }

        private void IT_Information_Load(object sender, EventArgs e)
        {
            printDocument2.PrintPage += new PrintPageEventHandler(printDocument2_PrintPage);
            Populate_Tab_Info();
            edit_index_box.Text = "0";
            edit_index_box.Text = "";
        }

        private void edit_index_box_TextChanged(object sender, EventArgs e)
        {
            if (selected_tab < 0) selected_tab = 0;
            if (edit_index_box.Text.Length == 0)
            {
                topic_drop_list.Enabled = true;
                delete_entry_button.Visible = false;
                subject_box.Text = "";
                info_box.Text = "";
                edit_index = -1;
                add_button.Text = "Add Info";
                tabControl1.Enabled = true;
            }
            else if (edit_index_box.Text.All(char.IsDigit) && edit_index_box.Text.Length > 0 && Convert.ToInt32(edit_index_box.Text) >= _INFO_LIST[tabControl1.TabPages[selected_tab].Text].Count)
            {
                topic_drop_list.Enabled = true;
                delete_entry_button.Visible = false;
                subject_box.Text = "";
                info_box.Text = "";
                edit_index = -1;
                add_button.Text = "Add Info";
                edit_index_box.Text = edit_index_box.Text.Substring(0, edit_index_box.Text.Length - 1);
                edit_index_box.SelectionStart = edit_index_box.Text.Length;
                edit_index_box.SelectionLength = 0;
                tabControl1.Enabled = true;
            }
            else if (edit_index_box.Text.All(char.IsDigit) && edit_index_box.Text.Length > 0)
            {
                topic_drop_list.Text = tabControl1.TabPages[selected_tab].Text;
                topic_drop_list.Enabled = false;
                try
                {
                    edit_index = Convert.ToInt32(edit_index_box.Text);
                    List<string> ref_list = _INFO_LIST[tabControl1.TabPages[selected_tab].Text];
                    string[] ref_string = ref_list[edit_index].Split(new string[] { "~" }, StringSplitOptions.None);
                    subject_box.Text = ref_string[0];
                    info_box.Text = ref_string[2];
                    add_button.Text = "Save Changes";
                    delete_entry_button.Visible = true;
                    tabControl1.Enabled = false;
                }
                catch
                {
                    topic_drop_list.Enabled = true;
                    subject_box.Text = "";
                    info_box.Text = "";
                    edit_index = -1;
                    add_button.Text = "Add Info";
                    delete_entry_button.Visible = false;
                    tabControl1.Enabled = true;
                }
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                edit_index_box.Text = edit_index_box.Text.Substring(0, edit_index_box.Text.Length - 1);
                edit_index_box.SelectionStart = edit_index_box.Text.Length;
                edit_index_box.SelectionLength = 0;
            }
        }

        // Delete entry
        private void delete_entry_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete entry?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _parent._APPEND_TO_SETUP_INFORMATION(2, "IT Information deleted " + "(Topic: " + topic_drop_list.Text + ", Subject: " + subject_box.Text + (edit_index >= 0 ? ", Entry#: " + edit_index : "") + ") by " + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER]);
                _INFO_LIST[topic_drop_list.Text].RemoveAt(edit_index);
                edit_index_box.Text = "";

                subject_box.Text = "";
                info_box.Text = "";
                Populate_Tab_Info();
            }
            else if (dialogResult == DialogResult.No)
            {
                edit_index_box.Text = "";
            }
        }

        // Show edit history
        private void show_edit_history_CheckedChanged(object sender, EventArgs e)
        {
            Populate_Tab_Info();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private void print_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to print?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                index = 0;
                if (selected_tab < 0) selected_tab = 0;
                List<string> ref_list = _INFO_LIST[tabControl1.TabPages[selected_tab].Text];
                print_list = new List<string>();
                foreach (string sub in ref_list)
                {
                    if ((search_box.Checked && sub.ToLower().Contains(search_text_box.Text.ToLower())) || !search_box.Checked)
                    {
                        string[] ref_string = sub.Split(new string[] { "~" }, StringSplitOptions.None);
                        print_list.Add("`S`" + ref_string[0] + " (" + ref_string[1] + ")");
                        print_list.Add("");
                        string[] info = ref_string[2].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string line in info)
                        {
                            print_list.Add("   " + line);
                        }
                        print_list.Add("");
                    }
                }

                //printPreviewDialog1.TopMost = true;
                //printPreviewDialog1.ShowDialog();
                if (print_list.Count() > 2)
                {
                    printDocument2.Print();
                }
                else
                {
                    MessageBox.Show("Error: Nothing to print!");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        int index = 0;

        private void printDocument2_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            
            
            int startx = 50;
            int starty = 40;
            int dataheight = 15;
            int height = starty + starty;
            
            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;

            Pen p = new Pen(Brushes.Black, 2.5f);
            Font f2 = new Font("Times New Roman", 9f);
            Font f3 = new Font("Times New Roman", 9.5f, FontStyle.Bold| FontStyle.Underline);
            Font f1 = new Font("Times New Roman", 14.0f, FontStyle.Bold | FontStyle.Underline);

            if (index < 5)
            {
                e.Graphics.DrawString(tabControl1.TabPages[selected_tab].Text, f1, Brushes.Black, new Rectangle(startx, height, 650, dataheight * 2));//, format1);
                height += dataheight;
                height += dataheight;
                height += dataheight;
            }

            while (index < print_list.Count) 
            {
                string g = print_list[index];
                if (height > e.MarginBounds.Height+40)
                {
                    height = starty;
                    e.HasMorePages = true;
                    return;
                }

                if (g.Contains("`S`"))
                {
                    e.Graphics.DrawString(g.Substring(3), f3, Brushes.Black, new Rectangle(startx, height, 650, dataheight));//, format1);
                }
                else
                {
                    e.Graphics.DrawString(g, f2, Brushes.Black, new Rectangle(startx, height, 650, dataheight));//, format1);
                }
                height += dataheight;
                index++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            info_box.Text += Environment.NewLine + "        • ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            info_box.Text += Environment.NewLine + "    ≡";
        }

        private void only_for_me_button_Click(object sender, EventArgs e)
        {

        }
    }

    // Modified RichTextBox to enable color enhancement
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool underline=false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            Font f = new Font("Arial", 10.0f);

            if (underline)
                //box.SelectionFont = new Font(box.Font, FontStyle.Underline | FontStyle.Bold);
                box.SelectionFont = new Font(f, FontStyle.Underline | FontStyle.Bold);

            box.SelectionColor = color;
            //box.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + Environment.NewLine);
            box.AppendText(text + Environment.NewLine);
            box.SelectionColor = box.ForeColor;


            box.SelectionStart = box.Text.Length;
            box.ScrollToCaret();
        }
    }

}
    