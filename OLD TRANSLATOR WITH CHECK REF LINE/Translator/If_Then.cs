using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mset.Extensions;

namespace Translator
{
    public partial class If_Then : Form
    {
        Translator _parent;
        private List<If_Then_Statement> Statements = new List<If_Then_Statement>();
        bool Edit_Mode = false;
        int Edit_Index = -1;
        string Edit_Comment = "";

        int Edit_Condition_Index = 0;

        public If_Then(Translator parent, List<string> edit_statement = null, int edit_index = -1)
        {
            _parent = parent;
            InitializeComponent();

            // Load drop-down boxes
            condition_comparison.Items.Add("=");
            condition_comparison.Items.Add("<");
            condition_comparison.Items.Add(">");
            action_comparison.Items.Add("Set");
            action_comparison.Items.Add("Add");
            action_comparison.Items.Add("Subtract");
            action_comparison.Items.Add("Multiply");
            action_comparison.Items.Add("Divide");

            // Presets
            condition_comparison.Text = "=";
            action_comparison.Text = "Set";

            // Populate Initial Statements (assuming edit)
            if (edit_statement != null && edit_statement.Count > 0 && edit_index >= 0)
            {
                Edit_Mode = true;
                Edit_Index = edit_index;
                reference_box.Text = edit_statement[1];
                string p = edit_statement[5];
                if (p.Contains("[/GRP]"))
                {
                    int end_index = p.IndexOf("[/GRP]") + 6;
                    p = p.Substring(0, end_index);
                    comment_box.Text = edit_statement[5].Substring(end_index);
                }
                Edit_Comment = p;

                string[] line = edit_statement[2].Split(new string[] { "~" }, StringSplitOptions.None);
                if (line.Count() > 0)
                {
                    foreach (string statement in line)
                    {
                        if (statement.Length > 4)
                        {
                            string[] line1 = statement.Split(new string[] { "|" }, StringSplitOptions.None);
                            If_Then_Statement ITS = new If_Then_Statement
                            {
                                condition_bin = line1[0],
                                condition_comparison = line1[1],
                                condition_value = line1[2],
                                action_bin = line1[4],
                                action_comparison = line1[3],
                                action_value = line1[5]
                            };
                            Statements.Add(ITS);
                        }
                    }
                }

                Populate_Statement_List();
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if (reference_box.Text.Length > 0 &&
                condition_bin.Text.Length > 0 && condition_comparison.Text.Length > 0 && condition_value.Text.Length > 0 &&
                action_bin.Text.Length > 0 && action_comparison.Text.Length > 0 && action_value.Text.Length > 0)
            {
                If_Then_Statement ITS = new If_Then_Statement
                {
                    condition_bin = condition_bin.Text,
                    condition_comparison = condition_comparison.Text,
                    condition_value = condition_value.Text,
                    action_bin = action_bin.Text,
                    action_comparison = action_comparison.Text,
                    action_value = action_value.Text
                };
                Statements.Add(ITS);
                Append_Statement_To_List(ITS);
                Edit_Condition_Index = 0;
                save_edit_button.Enabled = false;

                condition_bin.Text = "";
                condition_value.Text = "";
                action_bin.Text = "";
                action_value.Text = "";
            }
            else
            {
                MessageBox.Show("Missing value(s)");
            }
        }

        private void save_edit_button_Click(object sender, EventArgs e)
        {
            if (reference_box.Text.Length > 0 &&
                condition_bin.Text.Length > 0 && condition_comparison.Text.Length > 0 && condition_value.Text.Length > 0 &&
                action_bin.Text.Length > 0 && action_comparison.Text.Length > 0 && action_value.Text.Length > 0)
            {
                If_Then_Statement ITS = new If_Then_Statement
                {
                    condition_bin = condition_bin.Text,
                    condition_comparison = condition_comparison.Text,
                    condition_value = condition_value.Text,
                    action_bin = action_bin.Text,
                    action_comparison = action_comparison.Text,
                    action_value = action_value.Text
                };

                Statements[Edit_Condition_Index] = ITS;
                Populate_Statement_List();

                save_edit_button.Enabled = false;

                condition_bin.Text = "";
                condition_value.Text = "";
                action_bin.Text = "";
                action_value.Text = "";
            }
            else
            {
                MessageBox.Show("Missing value(s)");
            }
        }

        private void Append_Statement_To_List(If_Then_Statement ITS)
        {
            statement_list.Items.Add(
                "{6}) If value in bin# {0} is {1} value of {2} ---> {3} value of bin# {4} the value of {5}".FormatWith
                (
                    ITS.condition_bin,
                    ITS.condition_comparison,
                    ITS.condition_value,
                    ITS.action_comparison,
                    ITS.action_bin + (ITS.action_comparison == "Set" ? " to" : " by"),
                    ITS.action_value,
                    statement_list.Items.Count + 1
                )
            );
        }

        private void Populate_Statement_List()
        {
            statement_list.Items.Clear();
            foreach (If_Then_Statement ITS in Statements)
            {
                Append_Statement_To_List(ITS);
            }
        }

        private void condition_bin_TextChanged(object sender, EventArgs e)
        {
            Verify_Double(sender, e);
        }

        private void condition_value_TextChanged(object sender, EventArgs e)
        {
            Verify_Double(sender, e);
        }

        private void action_bin_TextChanged(object sender, EventArgs e)
        {
            Verify_Double(sender, e);
        }

        private void action_value_TextChanged(object sender, EventArgs e)
        {
            Verify_Double(sender, e);
        }

        // Verify the validity of the string; making sure string is a number (or double)
        private void Verify_Double(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            b.SelectionStart = b.Text.Length;

            if (b.Name.Contains("bin"))
            {
                if (!b.Text.All(char.IsDigit))
                {
                    b.Text = b.Text.Substring(0, b.Text.Length - 1);
                    b.SelectionStart = b.Text.Length;
                    b.SelectionLength = 0;
                }
            }
            else if ((b.Text.Length > 0) && ((Get_Char_Count(b.Text, Convert.ToChar(".")) > 1) || (b.Text[0].ToString() == ".") || (!((b.Text.Substring(b.Text.Length - 1).All(char.IsDigit))) && !(b.Text[b.Text.Length - 1].ToString() == "."))))
            {
                b.TextChanged -= new System.EventHandler(Verify_Double);
                b.Text = b.Text.Substring(0, b.Text.Length - 1);
                b.SelectionStart = b.Text.Length;
                b.SelectionLength = 0;
                b.TextChanged += new System.EventHandler(Verify_Double);
            }
        }

        // Return character count in comparison string
        private int Get_Char_Count(string comparison_text, char reference_char)
        {
            int count = 0;
            foreach (char c in comparison_text)
            {
                if (c == reference_char)
                {
                    count++;
                }
            }
            return count;
        }

        // Return the string index before [parse_char]
        private int Get_Statement_Index_From_String(string line, string parse_char = ")")
        {
            string return_value = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].ToString() == parse_char)
                {
                    i += 9999;
                }
                else
                {
                    return_value += line[i];
                }
            }
            return Convert.ToInt32(return_value);
        }

        private void copy_button_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Get_Statement_Index_From_String((string)statement_list.SelectedItem) - 1;
                Statements.Add(Statements[index]);
                Populate_Statement_List();
                statement_list.SelectedIndex = index;
            }
            catch
            {
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Get_Statement_Index_From_String((string)statement_list.SelectedItem) - 1;
                Statements.RemoveAt(index);
                Populate_Statement_List();
                statement_list.SelectedIndex = index;
            }
            catch
            {
            }
        }

        private void down_button_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Get_Statement_Index_From_String((string)statement_list.SelectedItem) - 1;
                If_Then_Statement ITS_temp = Statements[index];
                Statements[index] = Statements[index + 1];
                Statements[index + 1] = ITS_temp;
                Populate_Statement_List();
                statement_list.SelectedIndex = index + 1;
            }
            catch
            {
            }
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Get_Statement_Index_From_String((string)statement_list.SelectedItem) - 1;
                If_Then_Statement ITS_temp = Statements[index];
                Statements[index] = Statements[index - 1];
                Statements[index - 1] = ITS_temp;
                Populate_Statement_List();
                statement_list.SelectedIndex = index - 1;
            }
            catch
            {
            }
        }

        // Save and close
        private void button2_Click(object sender, EventArgs e)
        {
            if (Statements.Count < 1)
            {
                MessageBox.Show("Missing IF and THEN statements");
            }
            else if (Statements.Count < 2)
            {
                MessageBox.Show("Missing THEN statement");
            }
            else
            {
                string rule_string = "";
                foreach (If_Then_Statement ITS in Statements)
                {
                    rule_string = rule_string + "~" +
                                                ITS.condition_bin + "|" +
                                                ITS.condition_comparison + "|" +
                                                ITS.condition_value + "|" +
                                                ITS.action_comparison + "|" +
                                                ITS.action_bin + "|" +
                                                ITS.action_value;
                }
                if (Edit_Mode)
                {
                    _parent.Add_Rule("IF_THEN", reference_box.Text, rule_string.Substring(1), "", "", Edit_Comment + comment_box.Text, Edit_Index);
                    _parent.delete_rule(Edit_Index + 1);
                    _parent.Set_Rule_Selection(Edit_Index);
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    _parent.Add_Rule("IF_THEN", reference_box.Text, rule_string.Substring(1), "", "", Edit_Comment + comment_box.Text);
                    this.Close();
                    this.Dispose();
                }
                //Success
            }
        }

        private void statement_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            condition_edit(this.statement_list.IndexFromPoint(e.Location));
        }

        private void condition_edit(int _selected_index)
        {
            try
            {
                if (_selected_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    Edit_Condition_Index = _selected_index;
                    save_edit_button.Enabled = true;
                    condition_bin.Text = Statements[_selected_index].condition_bin;
                    condition_comparison.Text = Statements[_selected_index].condition_comparison;
                    condition_value.Text = Statements[_selected_index].condition_value;
                    action_bin.Text = Statements[_selected_index].action_bin;
                    action_comparison.Text = Statements[_selected_index].action_comparison;
                    action_value.Text = Statements[_selected_index].action_value;
                }
            }
            catch { }
        }

        private void If_Then_Load(object sender, EventArgs e)
        {
            statement_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(statement_list_MouseDoubleClick);
        }

    }

    public class If_Then_Statement
    {
        // Conditions
        public string condition_bin { get; set; }
        public string condition_comparison { get; set; }
        public string condition_value { get; set; }

        // Actions
        public string action_bin { get; set; }
        public string action_comparison { get; set; }
        public string action_value { get; set; }

    }
}
