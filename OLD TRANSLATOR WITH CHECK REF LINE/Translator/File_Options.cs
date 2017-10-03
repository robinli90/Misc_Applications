using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Translator
{

    public partial class File_Options : Form
    {
        // Setup collapse table
        Accordion accordion = new Accordion();
        Expander expander = new Expander();
        Expander expander2 = new Expander();
        Expander expander3 = new Expander();

        // Parent rule list, should contain ["FORMAT_RULE", format-rule, param2, param3, param4, param5]
        private List<List<string>> OUTPUT_FORMAT_LIST = new List<List<string>>();
        private Translator _parent;

        // Encryption class
        Encrypter enc_obj = new Encrypter();

        // List of conditions
        List<List<string>> List_of_Conditions = new List<List<string>>();

        // Add to the list as you add more rules
        private string[] FORMAT_RULES = { "LEADING_ZEROES", "LEADING_LETTER", "DATABASE_INFO", "FIRST_LINE", "LAST_LINE" };


        private bool _EDIT_CONDITION = false;
        private int _EDIT_CONDITION_INDEX;

        public File_Options(Translator main, List<List<string>> _parent_OUTPUT_FORMAT_LIST)

        {
            _parent = main;
            InitializeComponent();

            for (int i = 0; i < 10; i++)
                count_box.Items.Add(i.ToString());
            
            file_drop_1.Items.Add("must");
            file_drop_2.Items.Add("must");
            file_drop_1.Items.Add("must not");
            file_drop_2.Items.Add("must not");

            // Gather all format rules;
            if (_parent_OUTPUT_FORMAT_LIST.Count > 0)
            {
                for (int i = _parent_OUTPUT_FORMAT_LIST.Count - 1; i >= 0; i--)
                {
                    if ((_parent_OUTPUT_FORMAT_LIST[i][0] == "FORMAT_RULE"))
                    {
                        if (_parent_OUTPUT_FORMAT_LIST[i][1] == "LIST_OF_CONDITIONS")
                            List_of_Conditions.Add(_parent_OUTPUT_FORMAT_LIST[i]);
                        else
                            OUTPUT_FORMAT_LIST.Add(_parent_OUTPUT_FORMAT_LIST[i]);
                    }
                }
            }

            Scan_Rules();

            /*
            foreach (List<string> format in OUTPUT_FORMAT_LIST)
            {
                if (format[1] == "LIST_OF_CONDITIONS")
                {
                    List_of_Conditions.Add(format);
                }
            }
             */

            Update_Condition_List();

            // Autofill values that already exists
            count_box.Text = Get_Rule_Parameter("LEADING_ZEROES", 2) == "" ? "0" : Get_Rule_Parameter("LEADING_ZEROES", 2);
            value.Text = Get_Rule_Parameter("LEADING_LETTER", 2) == "" ? "N" : Get_Rule_Parameter("LEADING_LETTER", 2);
            first_line_box.Text = Get_Rule_Parameter("FIRST_LINE", 2) == "" ? "" : Get_Rule_Parameter("FIRST_LINE", 2);
            last_line_box.Text = Get_Rule_Parameter("LAST_LINE", 2) == "" ? "" : Get_Rule_Parameter("LAST_LINE", 2);
            try
            {
                db_user.Text = Get_Rule_Parameter("DATABASE_INFO", 2) == "" ? "" : enc_obj.Decrypt(Get_Rule_Parameter("DATABASE_INFO", 2).Split(new string[] { "†" }, StringSplitOptions.None)[0]);
                db_password.Text = Get_Rule_Parameter("DATABASE_INFO", 2) == "" ? "" : enc_obj.Decrypt(Get_Rule_Parameter("DATABASE_INFO", 2).Split(new string[] { "†" }, StringSplitOptions.None)[1]);
                db_name.Text = Get_Rule_Parameter("DATABASE_INFO", 2) == "" ? "(Example: 'dbo')" : enc_obj.Decrypt(Get_Rule_Parameter("DATABASE_INFO", 2).Split(new string[] { "†" }, StringSplitOptions.None)[2]);
                db_conn_str.Text = Get_Rule_Parameter("DATABASE_INFO", 2) == "" ? "(Example: 'Driver={SQL Server};Server=10.0.0.6'" : enc_obj.Decrypt(Get_Rule_Parameter("DATABASE_INFO", 2).Split(new string[] { "†" }, StringSplitOptions.None)[3]);
            }
            catch
            {
                db_name.Text = "(Example: 'dbo')";
                db_conn_str.Text = "(Example: 'Driver={SQL Server};Server=10.0.0.6')";
            }

        }

        // Check rules to see if any are missing. If missing, add
        private void Scan_Rules()
        {
            foreach (string format in FORMAT_RULES)
            {
                bool contains_format = false;
                foreach (List<string> rule in OUTPUT_FORMAT_LIST)
                {
                    if (rule[1] == format)
                        contains_format = true;
                }
                if (!contains_format)
                {
                    List<string> temp = new List<string>();
                    temp.Add("FORMAT_RULE");
                    temp.Add(format);
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    OUTPUT_FORMAT_LIST.Add(temp);
                }
            }
        }

        // Return the string for a certain rule and parameter
        private string Get_Rule_Parameter(string rule_name, int paramater_number)
        {
            foreach (List<string> rule in OUTPUT_FORMAT_LIST)
            {
                if (rule[1] == rule_name)
                {
                    if ((paramater_number < rule.Count))
                        return rule[paramater_number];
                }
            }
            return "";
        }

        private void Update_Condition_List()
        {
            condition_list.Items.Clear();
            foreach (List<string> format in List_of_Conditions)
            {
                // If looking for a certain value
                if (format[2].Contains("CONTAIN"))
                {
                    if (format[2].Contains("FILENAME"))
                        condition_list.Items.Add("The filename " + ((format[4] == "must") ? "must" : "must not") + " contain the following value: " + format[3]);
                    else
                        condition_list.Items.Add("The file " + ((format[4] == "must") ? "must" : "must not") + " have a line that contains the following value: " + format[3]);
                }
            }
        }

        // Set the rule parameters here
        public void Modify_Rule(string rule_name, string param1, string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            foreach (List<string> rule in OUTPUT_FORMAT_LIST)
            {
                if (rule[1] == rule_name)
                {
                    rule[2] = param1;
                    rule[3] = param2;
                    rule[4] = param3;
                    rule[5] = param4;
                }
            }
        }
        
        // Save button
        private void button1_Click(object sender, EventArgs e)
        {
            add_condition.PerformClick();

            List<List<string>> Temp_Rule_List = _parent.Get_Rule_List_Public();

            for (int i = Temp_Rule_List.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (Temp_Rule_List[i][1] == "DATABASE_INFO")
                    {
                        _parent._GLOBAL_DELETE_FORMAT_RULE("DATABASE_INFO");
                    }
                }
                catch
                {
                }
            }
            if (db_password.Text.Length > 0 || db_user.Text.Length > 0)
            {
                
                if (db_conn_str.Text.Length > 0 && db_name.Text.Length > 0 && db_password.Text.Length > 0 && db_user.Text.Length > 0)
                {
                    Modify_Rule("DATABASE_INFO", enc_obj.Encrypt(db_user.Text) + "†"
                                                + enc_obj.Encrypt(db_password.Text) + "†"
                                                + enc_obj.Encrypt(db_name.Text) + "†"
                                                + enc_obj.Encrypt(db_conn_str.Text) 
                                            );
                }
                else
                {
                    MessageBox.Show("Database information missing; database information not stored");
                }
            }
            else
            {
                MessageBox.Show("Database information missing; database information not stored");
            } 

            _parent._GLOBAL_DELETE_FORMAT_RULE("LEADING_ZEROES");
            _parent._GLOBAL_DELETE_FORMAT_RULE("LEADING_LETTER");
            _parent._GLOBAL_DELETE_FORMAT_RULE("FIRST_LINE");
            _parent._GLOBAL_DELETE_FORMAT_RULE("LAST_LINE");
            _parent._GLOBAL_DELETE_FORMAT_RULE("LIST_OF_CONDITIONS");
            Modify_Rule("LEADING_ZEROES", count_box.Text);
            Modify_Rule("LEADING_LETTER", value.Text);
            Modify_Rule("FIRST_LINE", first_line_box.Text);
            Modify_Rule("LAST_LINE", last_line_box.Text);

            // Add conditions
            OUTPUT_FORMAT_LIST.AddRange(List_of_Conditions);

            _parent.Rule_List.AddRange(OUTPUT_FORMAT_LIST);
            _parent._Setup_Rule_List();
            this.Close();
            this.Dispose();
        }

        // Add a condition to the list
        private void Add_Condition(string condition="", string value="", string placeholder1="", string placeholder2="", int index=-1)
        {
            List<string> temp = new List<string>();
                temp.Add("FORMAT_RULE");
                temp.Add("LIST_OF_CONDITIONS");
                temp.Add(condition);
                temp.Add(value);
                temp.Add(placeholder1);
                temp.Add(""); // comment
            if (index < 0)
            {
                List_of_Conditions.Add(temp);
            }
            else
            {       
                List_of_Conditions.Insert(index, temp);
            }

        }

        // Add condition
        private void add_condition_Click(object sender, EventArgs e)
        {
            if (!_EDIT_CONDITION)
            {
                if (filecontain_line.Text.Length > 0)
                {
                    Add_Condition("FILE_CONTAINS", filecontain_line.Text, (file_drop_2.Text.Contains("not") ? "must_not" : "must"));
                    filecontain_line.Text = "";
                    file_drop_2.Text = "";
                }
                if (filename_line.Text.Length > 0)
                {
                    Add_Condition("FILENAME_CONTAINS", filename_line.Text, (file_drop_1.Text.Contains("not") ? "must_not" : "must"));
                    filename_line.Text = "";
                    file_drop_1.Text = "";
                }
            }
            else
            {
                List_of_Conditions.RemoveAt(_EDIT_CONDITION_INDEX);
                if (filecontain_line.Text.Length > 0)
                {
                    Add_Condition("FILE_CONTAINS", filecontain_line.Text, (file_drop_2.Text.Contains("not") ? "must_not" : "must"), "", _EDIT_CONDITION_INDEX);
                    filecontain_line.Text = "";
                    file_drop_2.Text = "";
                }
                if (filename_line.Text.Length > 0)
                {
                    Add_Condition("FILENAME_CONTAINS", filename_line.Text, (file_drop_1.Text.Contains("not") ? "must_not" : "must"), "", _EDIT_CONDITION_INDEX);
                    filename_line.Text = "";
                    file_drop_1.Text = "";
                }
                _EDIT_CONDITION = false;
                condition_list.SetSelected(_EDIT_CONDITION_INDEX, true);
            }
            Update_Condition_List();
            
            file_drop_2.Enabled = true;
            filecontain_line.Enabled = true;
            file_drop_1.Enabled = true;
            filename_line.Enabled = true;
        }

        // Delete selected conditon
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (condition_list.Items.Count > 0)
            {
                int selected_index = (int)condition_list.SelectedIndex;
                try
                {
                    List_of_Conditions.RemoveAt(selected_index);
                    Update_Condition_List();
                    if (condition_list.Items.Count > 0)
                        condition_list.SetSelected(selected_index, true);
                }
                catch
                {
                    condition_list.SetSelected(selected_index - 1, true);
                }
            }
        }

        // Condition copy
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)condition_list.SelectedIndex;
                List_of_Conditions.Insert(selected_index + 1, List_of_Conditions[selected_index]);
                Update_Condition_List();
                condition_list.SetSelected(selected_index + 1, true);
            }
            catch { }
        }

        private void condition_list_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void condition_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            condition_edit(this.condition_list.IndexFromPoint(e.Location));
        }

        private void condition_edit(int _selected_index)
        {
            try
            {
                // Clear fields
                filename_line.Text = "";
                file_drop_1.Text = ""; 
                filecontain_line.Text = "";
                file_drop_2.Text = "";
                _EDIT_CONDITION = true;
                int selected_index = _selected_index;
                _EDIT_CONDITION_INDEX = selected_index;
                if (selected_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    if (List_of_Conditions[selected_index][2] == "FILENAME_CONTAINS")
                    {
                        file_drop_1.Text = List_of_Conditions[selected_index][4] == "must" ? "must" : "must not";
                        filename_line.Text = List_of_Conditions[selected_index][3];
                        file_drop_2.Enabled = false;
                        filecontain_line.Enabled = false;
                    }

                    if (List_of_Conditions[selected_index][2] == "FILE_CONTAINS")
                    {
                        file_drop_2.Text = List_of_Conditions[selected_index][4] == "must" ? "must" : "must not";
                        filecontain_line.Text = List_of_Conditions[selected_index][3];
                        file_drop_1.Enabled = false;
                        filename_line.Enabled = false;
                    }

                }
            }
            catch { }
        }

        private void file_drop_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_EDIT_CONDITION) add_condition.PerformClick();
        }

        private void file_drop_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_EDIT_CONDITION) add_condition.PerformClick();
        }

        private void filecontain_line_TextChanged(object sender, EventArgs e)
        {
            //if (_EDIT_CONDITION) add_condition.PerformClick();
        }

        private void filename_line_TextChanged(object sender, EventArgs e)
        {
            //if (_EDIT_CONDITION) add_condition.PerformClick();
        }

        private void File_Options_Load(object sender, EventArgs e)
        {
            panel2.Size = panel1.Size;
            panel2.Location = panel1.Location;
            panel3.Size = panel1.Size;
            panel3.Location = panel1.Location;

            accordion.Size = panel1.Size;
            //accordion.Dock = DockStyle.Left;
            accordion.Location = panel1.Location;
            //accordion.BorderStyle = BorderStyle.FixedSingle;

            // panel 1
            expander.Size = panel1.Size;
            //expander.Location = new Point(11, 13);
            expander.Padding = new Padding(0);
            expander.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander, "Rules that require a line reference point (conditional rule execution)", System.Drawing.ColorTranslator.FromHtml("#D0EBFF"), Color.Black);
            this.Controls.Remove(panel1);
            expander.Content = panel1;
            accordion.Add(expander);

            // panel 2
            expander2.Size = panel1.Size;
            //expander2.Location = new Point(11, 13);
            expander2.Padding = new Padding(0);
            expander2.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander2, "Rules that require a specific line number", System.Drawing.ColorTranslator.FromHtml("#B5E0FF"), Color.Black);
            this.Controls.Remove(panel2);
            expander2.Content = panel2;
            accordion.Add(expander2);


            // panel 2
            expander3.Size = panel1.Size;
            //expander2.Location = new Point(11, 13);
            expander3.Padding = new Padding(0);
            expander3.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander3, "Run a specific query", System.Drawing.ColorTranslator.FromHtml("#A2D7FF"), Color.Black);
            this.Controls.Remove(panel3);
            expander3.Content = panel3;
            accordion.Add(expander3);

            //Console.Write("TEST");
            //expander.Controls.Add(panel1);
            this.Controls.Add(accordion);
        }

        private void File_Options_Load_1(object sender, EventArgs e)
        {
            panel2.Size = panel1.Size;
            panel2.Location = panel1.Location;
            panel3.Size = panel1.Size;
            panel3.Location = panel1.Location;

            accordion.Size = panel1.Size;
            //accordion.Dock = DockStyle.Left;
            accordion.Location = panel1.Location;
            //accordion.BorderStyle = BorderStyle.FixedSingle;

            // panel 1
            expander.Size = panel1.Size;
            //expander.Location = new Point(11, 13);
            expander.Padding = new Padding(0);
            expander.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander, "Modify Output Format", System.Drawing.ColorTranslator.FromHtml("#D0EBFF"), Color.Black);
            this.Controls.Remove(panel2);
            expander.Content = panel2;
            accordion.Add(expander);

            // panel 2
            expander2.Size = panel1.Size;
            //expander2.Location = new Point(11, 13);
            expander2.Padding = new Padding(0);
            expander2.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander2, "Pre-Translation Conditions", System.Drawing.ColorTranslator.FromHtml("#B5E0FF"), Color.Black);
            this.Controls.Remove(panel1);
            expander2.Content = panel1;
            accordion.Add(expander2);


            // panel 2
            expander3.Size = panel1.Size;
            //expander2.Location = new Point(11, 13);
            expander3.Padding = new Padding(0);
            expander3.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander3, "Database Credentials", System.Drawing.ColorTranslator.FromHtml("#A2D7FF"), Color.Black);
            this.Controls.Remove(panel3);
            expander3.Content = panel3;
            accordion.Add(expander3);

            //Console.Write("TEST");
            //expander.Controls.Add(panel1);
            this.Controls.Add(accordion);
        }
    }
}

