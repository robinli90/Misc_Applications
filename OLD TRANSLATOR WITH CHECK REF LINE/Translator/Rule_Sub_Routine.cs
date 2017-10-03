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

    public partial class Rule_Sub_Routine : Form
    {
        private string example_intro = "Using the following example G-Code: " + Environment.NewLine
                                        + "%" + Environment.NewLine
                                        + "N1 (MANDREL)" + Environment.NewLine
                                        + "N2 G21 " + Environment.NewLine
                                        + "N3 G99 " + Environment.NewLine
                                        + "N4 G50 X450 Z300 " + Environment.NewLine
                                        + "N5 M41 " + Environment.NewLine
                                        + "N6 G50 S1000 " + Environment.NewLine
                                        + "N7 G96 S250 M04 " + Environment.NewLine
                                        + "N8 T0303 " + Environment.NewLine
                                        + "N9 M08 " + Environment.NewLine
                                        + "N10 G00 X315.7874 Z159.2679 " + Environment.NewLine
                                        + "N11 G01 X40 Z159.2679 F.24 " + Environment.NewLine
                                        + "N12 X0 Z159.2679 F.12 " + Environment.NewLine
                                        + "N13 G00 X-1.5876 " + Environment.NewLine
                                        + "N14 Z160.7679 " + Environment.NewLine
                                        + "N15 X315.7874 " + Environment.NewLine
                                        + "N16 Z156.7346 " + Environment.NewLine
                                        + "N17 G01 X40 Z156.7346 F.24 " + Environment.NewLine
                                        + "N18 X0 Z156.7346 F.12 " + Environment.NewLine
                                        + "N19 G00 X-1.5876 " + Environment.NewLine
                                        + "N20 Z158.2346" + Environment.NewLine + Environment.NewLine;

        private List<string> List_of_Conditions = new List<string>();
        private List<string> List_of_Actions = new List<string>();
        private Add_Rule _parent;
        private string _reference_string;
        private bool _EDIT_CONDITION = false;
        private bool _EDIT_ACTION = false;
        private int  _EDIT_CONDITION_INDEX;
        private int  _EDIT_ACTION_INDEX;
        private bool _MIRROR_MODE = false; // Enables range/mirroring
        private bool _MIRROR_MODE_EDIT = false;

        public Rule_Sub_Routine(Add_Rule rule, string ref_string, List<string> edit_rules, bool mirror_edit=false)
        {
            InitializeComponent();
            reference_line.Text = ref_string;

            _MIRROR_MODE_EDIT = mirror_edit;
            _parent = rule;
            _reference_string = ref_string;
            reference_line.Text = ref_string;
            condition_direction.Items.Add("Above");
            condition_direction.Items.Add("Below");
            action_direction.Items.Add("Above");
            action_direction.Items.Add("Below");
            action_type.Items.Add("Insert");
            action_type.Items.Add("Delete");
            action_type.Items.Add("InsertAtStart");
            action_type.Items.Add("InsertAtEnd");
            action_type.Items.Add("Replace");
            action_type.Items.Add("ReplaceWithin");

            if (edit_rules.Count > 0) //is edit
            {
                Parse_Edit(edit_rules);
                check6.Enabled = false;
                condition_edit(0);
                action_edit(0);
                condition_line.ForeColor = Color.Red;
                condition_line.BackColor = Color.LightYellow;
                condition_text.ForeColor = Color.Red;
                condition_text.BackColor = Color.LightYellow;
                action_text.ForeColor = Color.Red;
                action_text.BackColor = Color.LightYellow;
                action_line.ForeColor = Color.Red;
                action_line.BackColor = Color.LightYellow;
                replace_within_value.ForeColor = Color.Red;
                replace_within_value.BackColor = Color.LightYellow;
            }
            else
            {
                condition_direction.Items.Add("Special Mode");
            }
            if (_MIRROR_MODE_EDIT)
            {
                condition_direction.Enabled = false;
                condition_line.Enabled = false;
                button10.Enabled = false;
                button2.Enabled = false;
                action_direction.Enabled = false;
                action_line.Enabled = false;
                button11.Enabled = false;
                button3.Enabled = false;
                action_type.Items.Remove("Insert");
                //action_type.Enabled = false;
                //_MIRROR_MODE = true;
            }
        }

        private void Parse_Edit(List<string> list)
        {
            string[] condition = list[0].Split(new string[] { "~" }, StringSplitOptions.None);
            foreach (string c in condition)
            {
                List_of_Conditions.Add(c);
            }
            string[] action = list[1].Split(new string[] { "~" }, StringSplitOptions.None);
            foreach (string a in action)
            {
                List_of_Actions.Add(a);
            }
            Update_Condition_List();
            Update_Action_List();
        }

        private void Update_Condition_List()
        {
            condition_list.Items.Clear();
            if (!_MIRROR_MODE)
            {
                foreach (string condition in List_of_Conditions)
                {
                    string[] lines = condition.Split(new string[] { "|" }, StringSplitOptions.None);
                    condition_list.Items.Add("If " + lines[0].TrimStart("-".ToCharArray()) + " line(s) " + (lines[0].StartsWith("-") ? "above" : "below") + " the text: '" + reference_line.Text.Replace("_", "' or '") + "'" + (lines.Count() > 2 ? (lines[2] == "Does Not" ? " does not" : "") : "" ) + " contain the following text: '" + lines[1] + "'");
                }
            }
            else
            {
                string[] lines = List_of_Conditions[0].Split(new string[] { "|" }, StringSplitOptions.None);
                condition_list.Items.Add("For all numbers +-" + lines[0].TrimStart("-".ToCharArray()) + " lines from line" + (lines.Count() > 2 ? (lines[2] == "Does Not " ? " not " : "") : "" ) + "contain '" + reference_line.Text.Replace("_", "' or '") + "'");
            }
        }

        private void Update_Action_List()
        {
            action_list.Items.Clear();
            if (!_MIRROR_MODE)
            {
                foreach (string action in List_of_Actions)
                {
                    string action_string = "";
                    string[] lines = action.Split(new string[] { "|" }, StringSplitOptions.None);
                    action_string = action_string + lines[0] + " --> from " + lines[1].TrimStart("-".ToCharArray()) + " line(s) " + (lines[1].StartsWith("-") ? "above" : "below") + " the text: '" + reference_line.Text.Replace("_", "' or '") + "'";
                    if (lines.Length > 2) // has 'Value'
                    {
                        action_string = action_string + " --> " + lines[0] + (lines[0].Contains("Replace") ? " with" : "") + " --> '" + lines[2] + "'";
                        if (lines.Length == 4)
                            action_string = action_string + " with the value '" + lines[3] + "'";
                    }
                    action_list.Items.Add(action_string);
                }
            }
            else
            {
                string[] lines = List_of_Actions[0].Split(new string[] { "|" }, StringSplitOptions.None);
                string action_string = "";
                action_string = "Delete from +-" + lines[1] + " lines from reference";
                if (lines.Length > 2) // If no delete
                {
                    action_string = lines[0] + " --> All lines +-" + lines[1] + " lines from reference with '" + lines[2] + "'";
                    if (lines.Length == 4)
                        action_string = action_string + " with the value '" + lines[3] + "'";
                }
                action_list.Items.Add(action_string);
            }
        }

        private void insert_line_number_TextChanged(object sender, EventArgs e)
        {

        }

        // Add new condition
        private void add_condition_Click(object sender, EventArgs e)
        {
            if ((_MIRROR_MODE && List_of_Conditions.Count == 1))
            {
                MessageBox.Show("Cannot add anymore Conditions in special mode");
            }
            else// if (!_MIRROR_MODE_EDIT)
            {
                if (condition_line.Text.Length > 0 && condition_direction.Text.Length > 0 && condition_text.Text.Length > 0 && (!(condition_text.Text.Contains("|") || (condition_text.Text.Contains("~")))))
                {
                    //if (condition_direction.Text == "Above")
                    //{
                    if (!_EDIT_CONDITION && !_MIRROR_MODE_EDIT)
                        List_of_Conditions.Add((condition_direction.Text == "Above" ? "-" : "") + condition_line.Text + "|" + condition_text.Text + "|" + does_or_not.Text);
                    else
                    {
                        List_of_Conditions.RemoveAt(_EDIT_CONDITION_INDEX);
                        List_of_Conditions.Insert(_EDIT_CONDITION_INDEX, (condition_direction.Text == "Above" ? "-" : "") + condition_line.Text + "|" + condition_text.Text + "|" + does_or_not.Text);
                        _EDIT_CONDITION = false;
                        condition_list.SetSelected(_EDIT_CONDITION_INDEX, true);
                    }
                    Update_Condition_List();
                }
            }
        }

        // Add new action
        private void button1_Click_1(object sender, EventArgs e)
            
        {
            if (_MIRROR_MODE && List_of_Actions.Count == 1)
            {
                MessageBox.Show("Cannot add anymore Actions in special mode");
            }
            else
            {
                bool valid = true;
                if (action_type.Text == "ReplaceWithin" && (replace_within_value.Text.Length == 0))
                    valid = false;
                if (action_type.Text.Contains("At") && action_text.Text.Length == 0)
                    valid = false;
                if (action_text.Text.Contains("|") || action_text.Text.Contains("~"))
                {
                    valid = false;
                    MessageBox.Show("Error in action format, cannot contain '~' or '|'");
                }
                if (replace_within_value.Text.Contains("|") || replace_within_value.Text.Contains("~"))
                {
                    valid = false;
                    MessageBox.Show("Error in action format, cannot contain '~' or '|'");
                }
                string action_string = "";
                if (action_type.Text.Length > 0 && action_direction.Text.Length > 0 && action_line.Text.Length > 0 && valid)
                {
                    action_string = action_type.Text + "|";
                    if (action_direction.Text == "Above")
                        action_string = action_string + "-" + action_line.Text;
                    else
                        action_string = action_string + action_line.Text;

                    if (action_type.Text.Contains("Insert") || action_type.Text == "Replace")
                        action_string = action_string + "|" + action_text.Text;
                    else if (action_type.Text == "ReplaceWithin")
                        action_string = action_string + "|" + replace_within_value.Text + "|" + action_text.Text;
                    if (!_EDIT_ACTION && !_MIRROR_MODE_EDIT)
                    {
                        List_of_Actions.Add(action_string);
                    }
                    else // EDIT ACTION
                    {
                        List_of_Actions.RemoveAt(_EDIT_ACTION_INDEX);
                        List_of_Actions.Insert(_EDIT_ACTION_INDEX, action_string);
                        _EDIT_ACTION = false;
                        action_list.SetSelected(_EDIT_ACTION_INDEX, true);
                    }
                    Update_Action_List();
                }
            }
        }

        private void conditon_line_TextChanged(object sender, EventArgs e)
        {
            _parent.Check_Number_Input(condition_line, -1, true);
            if (_MIRROR_MODE)
            {
                try
                {
                    Convert.ToInt32(condition_line.Text);
                    action_line.Text = condition_line.Text;
                }
                catch
                {
                    action_line.Text = "";
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _parent.Check_Number_Input(action_line, -1, true);
        }

        // Finish Button
        private void button4_Click(object sender, EventArgs e)
        {
            if (_MIRROR_MODE && (!(condition_list.Items.Count == 1) || !(action_list.Items.Count == 1)))
                MessageBox.Show("Invalid SPECIAL mode parameters: please have exactly ONE Condition and ONE Action");
            else if (List_of_Conditions.Count > 0 && List_of_Actions.Count > 0)
            {
                // Create condition string
                string _VALUE = "";
                foreach (string condition in List_of_Conditions)
                {
                    _VALUE = _VALUE + condition + "~";
                }
                _VALUE = _VALUE.TrimEnd("~".ToCharArray()); // trim end char "~"

                // Create action string
                string _OPTIONAL_VALUE = "";
                foreach (string action in List_of_Actions)
                {
                    _OPTIONAL_VALUE = _OPTIONAL_VALUE + action + "~";
                }
                _OPTIONAL_VALUE = _OPTIONAL_VALUE.TrimEnd("~".ToCharArray()); // trim end char "~"

                List<string> temp = new List<string>();
                temp.Add(_VALUE);
                temp.Add(_OPTIONAL_VALUE);
                _parent.Store_MULTI_CONDITIONAL_PARAMETERS(temp);
                if (_MIRROR_MODE)
                    _parent.SET_MIRROR_MODE(true);
                else
                    _parent.SET_MIRROR_MODE(false);
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error in format, please make sure that you have at least one condition and one action");
            }
        }

        // Delete selected conditon
        private void button2_Click(object sender, EventArgs e)
        {
            if (condition_list.Items.Count > 0)
            {
                try
                {
                    int selected_index = (int)condition_list.SelectedIndex;
                    List_of_Conditions.RemoveAt(selected_index);
                    Update_Condition_List();
                    if (condition_list.Items.Count > 0)
                        condition_list.SetSelected(selected_index, true);
                }
                catch { }
            }
        }


        // Delete selected action
        private void button3_Click(object sender, EventArgs e)
        {
            if (action_list.Items.Count > 0)
            {
                try
                {
                    int selected_index = (int)action_list.SelectedIndex;
                    List_of_Actions.RemoveAt(selected_index);
                    Update_Action_List();
                    if (action_list.Items.Count > 0)
                        action_list.SetSelected(selected_index, true);
                }
                catch { }
            }
        }

        private void action_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action_type.Text == "Delete")
                action_text.Enabled = false;
            else
                action_text.Enabled = true;
            if (action_type.Text == "ReplaceWithin")
            {
                replace_within_box.Visible = true;
                replace_within_value.Visible = true;
            }
            else
            {
                replace_within_box.Visible = false;
                replace_within_value.Visible = false;
            }   
            
        }


        private void condition_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            condition_edit(this.condition_list.IndexFromPoint(e.Location));
        }

        private void condition_edit(int _selected_index)
        {
            try
            {
                _EDIT_CONDITION = true;
                int selected_index = _selected_index;
                _EDIT_CONDITION_INDEX = selected_index;
                string[] lines = List_of_Conditions[selected_index].Split(new string[] { "|" }, StringSplitOptions.None);
                if (selected_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    condition_line.Text = lines[0].TrimStart("-".ToCharArray());
                    condition_direction.Text = "Below";
                    if (Convert.ToInt32(lines[0]) < 0)
                        condition_direction.Text = "Above";
                    condition_text.Text = lines[1];
                    if (lines.Count() > 2 && lines[2] == "Does Not")
                    {
                        does_or_not.Text = "Does Not";
                    }
                    else
                    {
                        does_or_not.Text = "Does";
                    }
                }
            }
            catch { }
        }

        private void action_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            action_edit(this.action_list.IndexFromPoint(e.Location));
        }

        private void action_edit(int _selected_index)
        {
            try
            {
                _EDIT_ACTION = true;
                int selected_index = _selected_index;
                _EDIT_ACTION_INDEX = selected_index;
                string[] lines = List_of_Actions[selected_index].Split(new string[] { "|" }, StringSplitOptions.None);
                if (selected_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    action_type.Text = lines[0];
                    action_line.Text = lines[1].TrimStart("-".ToCharArray());
                    action_direction.Text = "Below";
                    if (Convert.ToInt32(lines[1]) < 0)
                        action_direction.Text = "Above";
                    if (!(lines[0] == "Delete"))
                        action_text.Text = lines[2];
                    if (lines[0] == "ReplaceWithin")
                    {
                        action_text.Text = lines[3];
                        replace_within_value.Text = lines[2];
                    }
                }
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the line of reference. All conditions and actions are reflective of this line and its line number.");
        }

        // Condition Up
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)condition_list.SelectedIndex;
                if (condition_list.Items.Count > 1)
                {
                    string temp = List_of_Conditions[selected_index];
                    List_of_Conditions[selected_index] = List_of_Conditions[selected_index - 1];
                    List_of_Conditions[selected_index - 1] = temp;
                    Update_Condition_List();
                    condition_list.SetSelected(selected_index - 1, true);
                }
            }
            catch { }
        }

        // Condition down
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)condition_list.SelectedIndex;
                if (condition_list.Items.Count > 1)
                {
                    string temp = List_of_Conditions[selected_index];
                    List_of_Conditions[selected_index] = List_of_Conditions[selected_index + 1];
                    List_of_Conditions[selected_index + 1] = temp;
                    Update_Condition_List();
                    condition_list.SetSelected(selected_index + 1, true);
                }
            }
            catch { }
        }

        // Action up
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)action_list.SelectedIndex;
                if (action_list.Items.Count > 1)
                {
                    string temp = List_of_Actions[selected_index];
                    List_of_Actions[selected_index] = List_of_Actions[selected_index - 1];
                    List_of_Actions[selected_index - 1] = temp;
                    Update_Action_List();
                    action_list.SetSelected(selected_index - 1, true);
                }
            }
            catch { }
        }

        // Action down
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)action_list.SelectedIndex;
                if (action_list.Items.Count > 1)
                {
                    string temp = List_of_Actions[selected_index];
                    List_of_Actions[selected_index] = List_of_Actions[selected_index + 1];
                    List_of_Actions[selected_index + 1] = temp;
                    Update_Action_List();
                    action_list.SetSelected(selected_index + 1, true);
                }
            }
            catch { }
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

        // Action copy
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)action_list.SelectedIndex;
                List_of_Actions.Insert(selected_index + 1, List_of_Actions[selected_index]);
                Update_Action_List();
                action_list.SetSelected(selected_index + 1, true);
            }
            catch { }
        }

        private void check6_CheckedChanged(object sender, EventArgs e)
        {
            if (!_EDIT_ACTION)
            {
                if (check6.Checked)
                {
                    MessageBox.Show("Please note that under this mode, you must have EXACTLY ONE Condition and ONE Action");
                    //action_direction.Enabled = false;
                    action_direction.Items.Add("Special Mode");
                    action_line.Text = condition_line.Text;
                    action_line.Enabled = false;
                    action_type.Items.Remove("Insert");
                    //action_type.Items.Remove("Delete");
                    condition_direction.Text = "Special Mode";
                    action_direction.Text = "Special Mode";
                    action_direction.Enabled = false;
                    condition_direction.Enabled = false;
                    //condition_direction.Enabled = false;
                    _MIRROR_MODE = true;
                }
                else
                {
                    action_direction.Items.Remove("Special Mode");
                    action_line.Enabled = true;
                    action_direction.Enabled = true;
                    action_line.Enabled = true;
                    condition_direction.SelectedIndex = -1;
                    action_direction.SelectedIndex = -1;
                    //condition_direction.Items.Remove("Special Mode");
                    condition_direction.Enabled = true;
                    //condition_direction.Enabled = true;
                    action_type.Items.Add("Insert");
                    //action_type.Items.Add("Delete");
                    _MIRROR_MODE = false;
                }
            }
        }

        private void condition_direction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_MIRROR_MODE && (!(condition_direction.Text == "Special Mode")))
                check6.Checked = false;
            else
                if (condition_direction.Text == "Special Mode") check6.Checked = true;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "This mode is very special. It enables a quicker way to check if a range of numbers from the reference point contains a certain string. If satisified, execute an action on that line that satisfies the condition (generally we Replace or InsertAtStart/InsertAtEnd). This mode will check the lines x lines above and below reference point and see if any of those satisfy the one condition. If that line does, perform action on that line and that line only");
        }

        private void action_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void condition_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void does_or_not_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Rule_Sub_Routine_Load(object sender, EventArgs e)
        {
            does_or_not.Items.Add("Does");
            does_or_not.Items.Add("Does Not");
            does_or_not.Text = "Does";
        }
    }
}