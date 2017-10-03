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
    public partial class Add_Rule : Form
    {
        // Setup collapse table (Main one)
        Accordion accordion = new Accordion();
        Expander expander = new Expander();
        Expander expander2 = new Expander();
        Expander expander3 = new Expander();

        // Setup collapse table (Secondary main one)
        Accordion sub_accordion = new Accordion();
        Expander sub_expander1 = new Expander();
        Expander sub_expander2 = new Expander();
        Expander sub_expander3 = new Expander();
        Expander sub_expander4 = new Expander();
        Expander sub_expander5 = new Expander();
        Expander sub_expander6 = new Expander();

        Point spawn_location = new Point();

        // All parameters for EDIT MODE
        private bool   _EDIT_MODE = false;
        private int    _EDIT_SELECTED_INDEX;
        private string _EDIT_ACTION;
        private string _DIMENSION;
        private string _VALUE;
        private string _OPTIONAL_VALUE;
        private string _COMMENT;
        public string _SEARCH_VALUE;
        private string _GROUP_TEXT = "";

        Encrypter enc_obj = new Encrypter();

        // Parameter to set global condition
        public string _GLOBAL_CONDITION = "";

        private List<int> _SELECTED_INDICIES = new List<int>();

        // MIRROR MODE For SPECIAL MODE selection (MUTLICONDITIONAL)
        private bool   _MIRROR_MODE = false;
        //private bool _MULTI_CONDITIONAL_RULE;
        private List<string> _MULTI_CONDITIONAL_PARAMETERS = new List<string>(); // [VALUE, OPTIONAL_VALUE]

        private List<CheckBox> check_boxes = new List<CheckBox>();
        private string Main_Line = "";
        private Translator _parent;
        private string example_intro =    "Using the following example G-Code: " + Environment.NewLine
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

        private bool ERROR_EXISTS = false;

        public Add_Rule(Translator main, Point spawn_location_, bool _EDIT=false, int index=0, string A="", string B="", string C="", string D="", string E="", string F="", List<int> All_Selected_Indicies = null)
        {

            // EDIT VALUE PLACEHOLDERS
            _EDIT_MODE = _EDIT;
            _EDIT_SELECTED_INDEX = index;
            _EDIT_ACTION = A;
            _SEARCH_VALUE = B;
            _DIMENSION = C;
            _VALUE = D;
            _OPTIONAL_VALUE = E;
            _COMMENT = F;

            if (_COMMENT.Contains("[/GRP]"))
            {
                int start_index = _COMMENT.IndexOf("[GRP]");
                int end_index = _COMMENT.IndexOf("[/GRP]") + 6;
                _GROUP_TEXT = _COMMENT.Substring(start_index, end_index - start_index);
                _COMMENT = _COMMENT.Substring(end_index);
                Console.Write(_COMMENT);
            }

            if (!(All_Selected_Indicies == null))
                _SELECTED_INDICIES = All_Selected_Indicies;

            InitializeComponent();
            spawn_location = spawn_location_;
            multi_button.Enabled = false;
            Add_Check_Boxes();
            _parent = main;
            x_arithmetic.Items.Add("Add"); x_arithmetic.Items.Add("Subtract"); x_arithmetic.Items.Add("Multiply"); x_arithmetic.Items.Add("Divide"); x_arithmetic.Items.Add("Set");
            y_arithmetic.Items.Add("Add"); y_arithmetic.Items.Add("Subtract"); y_arithmetic.Items.Add("Multiply"); y_arithmetic.Items.Add("Divide"); y_arithmetic.Items.Add("Set");
            z_arithmetic.Items.Add("Add"); z_arithmetic.Items.Add("Subtract"); z_arithmetic.Items.Add("Multiply"); z_arithmetic.Items.Add("Divide"); z_arithmetic.Items.Add("Set");
            n_arithmetic.Items.Add("Add"); n_arithmetic.Items.Add("Subtract"); n_arithmetic.Items.Add("Multiply"); n_arithmetic.Items.Add("Divide"); n_arithmetic.Items.Add("Set");
            memory_arithmetic.Items.Add("Add"); memory_arithmetic.Items.Add("Subtract"); memory_arithmetic.Items.Add("Multiply"); memory_arithmetic.Items.Add("Divide");
            delete_a.Items.Add("Above"); delete_a.Items.Add("Below");
            insert_a.Items.Add("Above"); insert_a.Items.Add("Below");
            look_for_direction.Items.Add("Above"); look_for_direction.Items.Add("Below");
            look_for_action.Items.Add("InsertAtStart"); look_for_action.Items.Add("InsertAtEnd"); look_for_action.Items.Add("ReplaceWithin");
            condition_direction.Items.Add("Greater Than"); condition_direction.Items.Add("Less Than"); condition_direction.Items.Add("Equal To"); condition_direction.Items.Add("Not Equal To");
            file_behaviour.Items.Add("Delete"); file_behaviour.Items.Add("Move"); file_behaviour.Items.Add("Copy");
            range_fn.Items.Add("Delete"); range_fn.Items.Add("Move"); range_fn.Items.Add("Copy"); range_fn.Text = "";


            

        }

        private void Add_Check_Boxes()
        {
            // ORDER MATTERS IN WHICH THEY ARE ADDED (BUTTON LIST IS NOT IN ORDER AS THEY APPEAR)
            check_boxes.Add(check1);  
            check_boxes.Add(check2);
            check_boxes.Add(check3);
            check_boxes.Add(check4);
            check_boxes.Add(check5);
            check_boxes.Add(check6);
            check_boxes.Add(check7);
            check_boxes.Add(check8);
            check_boxes.Add(check9);
            check_boxes.Add(check10);
            check_boxes.Add(check11);
            check_boxes.Add(check12);
            check_boxes.Add(check13);
            check_boxes.Add(checkBox1);
            check_boxes.Add(check15); 
            check_boxes.Add(check16); 
            check_boxes.Add(check17); 
            check_boxes.Add(check18); 
            check_boxes.Add(check19); 
            check_boxes.Add(check20); 
            check_boxes.Add(check21); 
            check_boxes.Add(check22); 
            check_boxes.Add(check23); 
            check_boxes.Add(check24); 
            check_boxes.Add(check25); 
            check_boxes.Add(check26); 
            check_boxes.Add(check27); 
            check_boxes.Add(check28); 
            check_boxes.Add(check29); 
            check_boxes.Add(check30); 
            check_boxes.Add(check31); 
        }

        // Get the correct checkbox layout (selection logic)
        private void Check_Box_Validity(CheckBox target_box)
        {
            multi_button.Enabled = false;
            if (!(_EDIT_MODE))
            {
                int check_box_number = check_boxes.IndexOf(target_box);

                if (check_box_number == 1 || check_box_number == 2 || check_box_number == 3 || check_box_number == 4 || check_box_number == 5 || check_box_number == 23)
                {
                    check_boxes[1].Checked = true;
                    foreach (CheckBox g in check_boxes)
                    {
                        if (!(check_boxes.IndexOf(g) == 1 || check_boxes.IndexOf(g) == 2 || check_boxes.IndexOf(g) == 3 || check_boxes.IndexOf(g) == 4 || check_boxes.IndexOf(g) == 5 || check_boxes.IndexOf(g) == 23))
                        {
                            g.Checked = false;
                        }
                    }
                }
                else if (check_box_number == 16 || check_box_number == 17 || check_box_number == 18 || check_box_number == 19 || check_box_number == 20 || check_box_number == 26)
                {
                    check_boxes[16].Checked = true;
                    foreach (CheckBox g in check_boxes)
                    {
                        if (!(check_boxes.IndexOf(g) == 16 || check_boxes.IndexOf(g) == 17 || check_boxes.IndexOf(g) == 18 || check_boxes.IndexOf(g) == 19 || check_boxes.IndexOf(g) == 20 || check_boxes.IndexOf(g) == 26))
                        {
                            g.Checked = false;
                        }
                        if (check_box_number == 18)
                        {
                            check_boxes[17].Checked = false;
                            check_boxes[20].Checked = false;
                            check_boxes[26].Checked = false;
                        }
                        if (check_box_number == 17)
                        {
                            check_boxes[18].Checked = false;
                            check_boxes[20].Checked = false;
                            check_boxes[26].Checked = false;
                        }
                        if (check_box_number == 20)
                        {
                            check_boxes[18].Checked = false;
                            check_boxes[17].Checked = false;
                            check_boxes[26].Checked = false;
                        }
                        if (check_box_number == 26)
                        {
                            check_boxes[18].Checked = false;
                            check_boxes[17].Checked = false;
                            check_boxes[20].Checked = false;
                        }
                    }

                }
                else
                {
                    foreach (CheckBox g in check_boxes)
                    {
                        if (!(g == target_box))
                        {
                            g.Checked = false;
                        }
                    }
                }
            }

            // Multi-conditional action
            if (check_boxes[13].Checked && translate_line.Text.Length > 0)
            {
                multi_button.Enabled = true;
                //_MULTI_CONDITIONAL_RULE = true;
            }
            else if (check_boxes[13].Checked && translate_line.Text.Length == 0)
            {
                translate_line.BackColor = Color.LightYellow;
                MessageBox.Show("Requires a line to reference. Please fill in first text box labelled *");
                //_MULTI_CONDITIONAL_RULE = false;
                check_boxes[13].Checked = false;
            }
        }


        // Warn user that the translation affects all lines preemptively
        private void Add_Rule_Check(string ACTION, string SEARCH_VALUE, string DIMENSION, string VALUE, string OPTIONAL_VALUE, string COMMENT, int index=-1, bool dispose_form=true)
        {
            if (ACTION != "RunQuery" && (ACTION.Contains("?") || SEARCH_VALUE.Contains("?") || DIMENSION.Contains("?") || VALUE.Contains("?") || OPTIONAL_VALUE.Contains("?") || COMMENT.Contains("?")))
            {
                MessageBox.Show("Error: Invalid character detected");
                //select shopdate from d_order where ordernumber = "350024"
            }
            else
            {
                if ((check_boxes[16].Checked || check_boxes[0].Checked || check_boxes[1].Checked || check_boxes[6].Checked || check_boxes[7].Checked || check_boxes[8].Checked || check_boxes[9].Checked) &&
                    (!check_boxes[10].Checked && !check_boxes[11].Checked && !check_boxes[12].Checked) && translate_line.Text.Length == 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to create this rule that affects ALL lines in file?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (index < 0) // EDIT MODE
                        {
                            _parent.Add_Rule(ACTION, SEARCH_VALUE, DIMENSION, VALUE, OPTIONAL_VALUE, COMMENT);
                            if (!_MIRROR_MODE)
                            {
                                MessageBox.Show("Rule successfully added");
                                if (dispose_form)
                                {
                                    this.Close();
                                    this.Dispose();

                                }
                            }
                        }
                        else
                        {
                            _parent.Add_Rule(ACTION, SEARCH_VALUE, DIMENSION, VALUE, OPTIONAL_VALUE, COMMENT, index);
                            if (dispose_form)
                            {
                                this.Close();
                                this.Dispose();
                            }
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else
                {
                    if (index < 0) // NOT EDIT MODE
                    {
                        _parent.Add_Rule(ACTION, SEARCH_VALUE, DIMENSION, VALUE, OPTIONAL_VALUE, COMMENT);
                        if (dispose_form)
                        {
                            this.Close();
                            this.Dispose();
                        }
                        if (!_MIRROR_MODE)
                            MessageBox.Show("Rule successfully added");
                    }
                    else
                    {
                        _parent.Add_Rule(ACTION, SEARCH_VALUE, DIMENSION, VALUE, OPTIONAL_VALUE, COMMENT, index);
                        if (dispose_form)
                        {
                            this.Close();
                            this.Dispose();
                        }
                    }
                }
            }
        }


        // Public access to store _MULTI_CONDITIONAL_PARAMETERS;
        public void Store_MULTI_CONDITIONAL_PARAMETERS(List<string> g)
        {
            _MULTI_CONDITIONAL_PARAMETERS = g;
        }

        // Public access to change mode to mirror mode based on bool mode;
        public void SET_MIRROR_MODE(bool mode)
        {
            _MIRROR_MODE = mode;
        }

        // MAIN Rule save functionality based on checkbox
        private void Perform_Save_Rule(int index=-1)
        {
            string temp_comment_str = comment_box.Text;
            if (start_with_instead.Checked) Main_Line = "S~W~I" + Main_Line;
            if (_GROUP_TEXT.Length > 0) comment_box.Text = _GROUP_TEXT + temp_comment_str;
            if (condition_met.Checked && condition_bin.Text.Length > 0 && condition_direction.Text.Length > 0 && condition_value.Text.Length > 0)
            {
                comment_box.Text = "IFCHK((" + condition_bin.Text + ":" + condition_direction.Text + ":" + condition_value.Text + "))" + comment_box.Text;
            }
            else if (condition_met.Checked)
            {
                MessageBox.Show("Condition requested but not applied to rule because of format error. Please go back and revise and make sure all appropriate fields are filled");
            }
            if (check_once.Checked)
            {
                if (!(comment_box.Text.Contains("FIRST_INSTANCE_ONLY")))
                    comment_box.Text = "FIRST_INSTANCE_ONLY" + comment_box.Text;
            }
            if ((ignoreNum2.Checked || ignoreNum.Checked))
            {
                comment_box.Text = "I_L_N" + comment_box.Text;
            }
            if (check_boxes[0].Checked && replace_line.Text.Length > 0)
            {
                Add_Rule_Check("Replace", Main_Line, "", replace_line.Text, "", comment_box.Text, index);
            }
            // Arithmetic function
            else if (check_boxes[1].Checked && (
                (check_boxes[2].Checked && x_arithmetic.Text.Length > 0 && x_value.Text.Length > 0) ||
                (check_boxes[3].Checked && y_arithmetic.Text.Length > 0 && y_value.Text.Length > 0) ||
                (check_boxes[4].Checked && z_arithmetic.Text.Length > 0 && z_value.Text.Length > 0) ||
                (check_boxes[23].Checked && n_arithmetic.Text.Length > 0 && n_value.Text.Length > 0 && n_dimension.Text.Length > 0)
                    )
                )
            {
                #region X-Values
                if (check_boxes[2].Checked && x_arithmetic.Text.Length > 0 && x_value.Text.Length > 0)
                {
                    if (check_boxes[5].Checked) // if All dimensions 
                    {
                        Add_Rule_Check(x_arithmetic.Text, "X", "X", x_value.Text, "", comment_box.Text, index, false);
                    }
                    else
                    {
                        Add_Rule_Check(x_arithmetic.Text, Main_Line, "X", x_value.Text, "", comment_box.Text, index, false);
                    }
                }
                #endregion
                #region Y-Values
                if (check_boxes[3].Checked && y_arithmetic.Text.Length > 0 && y_value.Text.Length > 0) 
                {
                    if (check_boxes[5].Checked) // if All dimensions 
                    {
                        Add_Rule_Check(y_arithmetic.Text, "Y", "Y", y_value.Text, "", comment_box.Text, index, false);
                    }
                    else
                    {
                        Add_Rule_Check(y_arithmetic.Text, Main_Line, "Y", y_value.Text, "", comment_box.Text, index, false);
                    }
                }
                #endregion
                #region Z-Values
                if (check_boxes[4].Checked && z_arithmetic.Text.Length > 0 && z_value.Text.Length > 0)
                {
                    if (check_boxes[5].Checked) // if All dimensions 
                    {
                        Add_Rule_Check(z_arithmetic.Text, "Z", "Z", z_value.Text, "", comment_box.Text, index, false);
                    }
                    else
                    {
                        Add_Rule_Check(z_arithmetic.Text, Main_Line, "Z", z_value.Text, "", comment_box.Text, index, false);
                    }
                }

                #endregion
                #region N-Values
                if (check_boxes[23].Checked && n_arithmetic.Text.Length > 0 && n_value.Text.Length > 0)
                {
                    if (check_boxes[5].Checked) // if All dimensions 
                    {
                        Add_Rule_Check(z_arithmetic.Text, n_dimension.Text, n_dimension.Text, z_value.Text, "", comment_box.Text, index, false);
                    }
                    else
                    {
                        Add_Rule_Check(n_arithmetic.Text, Main_Line, n_dimension.Text, n_value.Text, "", comment_box.Text, index, false);
                    }
                }
                this.Close();
                this.Dispose();

                #endregion
            }
            // Delete lines containing main_text
            else if (check_boxes[6].Checked)
            {
                Add_Rule_Check("DeleteIf", Main_Line, "", "", "", comment_box.Text, index);
            }
            // Replace portion of line
            else if (check_boxes[7].Checked && portion_b.Text.Length > 0 && portion_a.Text.Length > 0)
            {
                Add_Rule_Check("ReplaceSpecific", Main_Line, "", portion_a.Text, portion_b.Text, comment_box.Text, index);
            }
            // Delete line above/below
            else if (check_boxes[8].Checked && delete_lines.Text.Length > 0 && delete_a.Text.Length > 0)
            {
                string temp_delete_lines = delete_lines.Text;
                if (delete_a.Text == "Above") temp_delete_lines = "-" + temp_delete_lines; //switch to negative
                Add_Rule_Check("DeleteFrom", Main_Line, "", temp_delete_lines, "", comment_box.Text, index);
            }
            // Insert line above/below
            else if (check_boxes[9].Checked && insert_lines.Text.Length > 0 && insert_a.Text.Length > 0 && insert_from_optional.Text.Length > 0)
            {
                string temp_insert_lines = insert_lines.Text;
                if (insert_a.Text == "Above") temp_insert_lines = "-" + temp_insert_lines; //switch to negative
                Add_Rule_Check("InsertFrom", Main_Line, "", temp_insert_lines, insert_from_optional.Text, comment_box.Text, index);
            }
            // Delete at line
            else if (check_boxes[10].Checked && delete_line_number.Text.Length > 0)
            {
                Add_Rule_Check("DeleteAt", _GLOBAL_CONDITION, "", delete_line_number.Text, "", comment_box.Text, index);
            }
            // Insert at line
            else if (check_boxes[11].Checked && insert_line_number.Text.Length > 0 && insert_line_text.Text.Length > 0)
            {
                Add_Rule_Check("InsertAt", _GLOBAL_CONDITION, "", insert_line_number.Text, insert_line_text.Text, comment_box.Text, index);
            }
            // Switch
            else if (check_boxes[12].Checked && switch_a.Text.Length > 0 && switch_b.Text.Length > 0)
            {
                Add_Rule_Check("Switch", _GLOBAL_CONDITION, "", switch_a.Text, switch_b.Text, comment_box.Text, index);
            }
            // Multiconditional
            else if (check_boxes[13].Checked && _MULTI_CONDITIONAL_PARAMETERS.Count > 0) // If Multiconditional
            {
                if (!_EDIT_MODE && _MIRROR_MODE)
                {
                    string[] conditions = _MULTI_CONDITIONAL_PARAMETERS[0].Split(new string[] { "|" }, StringSplitOptions.None);
                    string[] actions = _MULTI_CONDITIONAL_PARAMETERS[1].Split(new string[] { "|" }, StringSplitOptions.None);
                    int mirror_range = Convert.ToInt32(conditions[0]);

                    if (actions[0].Contains("ReplaceWithin"))
                        actions[2] = actions[2] + "|" + actions[3];

                    for (int i = mirror_range; i >= 1; i--)
                    {
                        Add_Rule_Check
                        (
                            "MultiRule", Main_Line, "",
                            //_MULTI_CONDITIONAL_PARAMETERS[0], // VALUE
                            i.ToString() + "|" + conditions[1],
                            //_MULTI_CONDITIONAL_PARAMETERS[1], // OPTIONAL_VALUE
                            (actions[0].Contains("Delete") ? actions[0] + "|" + i.ToString() : actions[0] + "|" + i.ToString() + "|" + actions[2]),
                            "*SPECIAL MODE RULE*: " + comment_box.Text,
                            index
                        );
                    }

                    mirror_range = mirror_range * -1;
                    //for (int i = -1; i >= mirror_range; i--)
                    for (int i = mirror_range; i <= -1; i++)
                    {
                        string test = i.ToString();
                        Console.WriteLine(test);
                        Add_Rule_Check
                        (
                            "MultiRule", Main_Line, "",
                            //_MULTI_CONDITIONAL_PARAMETERS[0], // VALUE
                            i.ToString() + "|" + conditions[1],
                            //_MULTI_CONDITIONAL_PARAMETERS[1], // OPTIONAL_VALUE
                            (actions[0].Contains("Delete") ? actions[0] + "|" + i.ToString() : actions[0] + "|" + i.ToString() + "|" + actions[2]),
                            "*SPECIAL MODE RULE*: " + comment_box.Text,
                            index
                        );
                    }
                }
                else
                {
                    if (_SELECTED_INDICIES.Count > 0) // IF MULTI-EDIT
                    {
                        //int delete_count = 0;
                        foreach (int index_value in _SELECTED_INDICIES)
                        {
                            //delete_count++;
                            List<string> GRAB_RULE = _parent.GET_RULE(index_value);
                            string PRE_VALUE = GRAB_RULE[3].Split(new string[] {"|"}, StringSplitOptions.None)[0];
                            string PRE_OPTIONAL_VALUE = GRAB_RULE[4].Split(new string[] {"|"}, StringSplitOptions.None)[0] + "|" + GRAB_RULE[4].Split(new string[] {"|"}, StringSplitOptions.None)[1];
                            string POST_VALUE = _MULTI_CONDITIONAL_PARAMETERS[0].Split(new string[] {"|"}, StringSplitOptions.None)[1];
                            string POST_OPTIONAL_VALUE = (_MULTI_CONDITIONAL_PARAMETERS[1].Split(new string[] { "|" }, StringSplitOptions.None)[0] == "Delete" ? "" : "|" + _MULTI_CONDITIONAL_PARAMETERS[1].Split(new string[] {"|"}, StringSplitOptions.None)[2]);

                            Add_Rule_Check(
                                "MultiRule", Main_Line, "",
                                PRE_VALUE + "|" + POST_VALUE,
                                PRE_OPTIONAL_VALUE + POST_OPTIONAL_VALUE,
                                "*SPECIAL MODE RULE*: ",// + comment_box.Text,
                                index_value
                            );

                            //_parent.delete_rule(index + delete_count);
                            _parent.delete_rule(index_value + 1);
                        }

                    }
                    else
                        Add_Rule_Check
                        (
                            "MultiRule", Main_Line, "",
                            _MULTI_CONDITIONAL_PARAMETERS[0], // VALUE
                            _MULTI_CONDITIONAL_PARAMETERS[1], // OPTIONAL_VALUE
                            comment_box.Text,
                            index
                        );
                }
            }
            // Multiconditional error handler;
            else if (check_boxes[13].Checked && _MULTI_CONDITIONAL_PARAMETERS.Count == 0) // If Multiconditional
            {
                MessageBox.Show("Missing conditions and/or actions");
            }
            else if (check_boxes[14].Checked && replace_line_text.Text.Length > 0 && replace_line_number.Text.Length > 0)
            {
                Add_Rule_Check("ReplaceAt", _GLOBAL_CONDITION, "", replace_line_number.Text, replace_line_text.Text, comment_box.Text, index);
            }
            else if (check_boxes[15].Checked && replace_within_text.Text.Length > 0 && translate_line.Text.Length > 0)
            {
                Add_Rule_Check("LastIndexOfReplace", Main_Line, "", replace_within_text.Text, "", comment_box.Text, index);
            }
            else if (translate_line.Text.Length > 0 && check_boxes[16].Checked && (check_boxes[17].Checked || check_boxes[18].Checked || check_boxes[20].Checked|| check_boxes[26].Checked) && store_bin.Text.Length > 0)
            {
                if (check_boxes[18].Checked) //if store dimension value
                {
                    DialogResult dialogResult = MessageBox.Show("Since you have selected storing dimension value, the translator will only store the numerical value after value. Please see help for more detail. Continue?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Add_Rule_Check("StoreValue", Main_Line, (store_dimension.Text.Length > 0 ? store_dimension.Text : ""), store_bin.Text, (check20.Checked ? memory_arithmetic.Text : ""), comment_box.Text, index);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else if (check_boxes[20].Checked) //store a particular value (use '@#' to denote specific value storage)
                {
                    Add_Rule_Check("StoreValue", Main_Line, "@#" + store_specific.Text, store_bin.Text, (check20.Checked ? memory_arithmetic.Text : ""), comment_box.Text, index);
                }
                else if (check_boxes[26].Checked) // store line number
                {
                    Add_Rule_Check("StoreValue", Main_Line, "@!", store_bin.Text, (check20.Checked ? memory_arithmetic.Text : ""), comment_box.Text, index);
                }
                else
                {
                    if (check20.Checked) MessageBox.Show("You have selected to perform arithmetic on any values stored in memory bin# " + store_bin.Text + ". Please be warned that if the value cannot be added, there will be no warning.");

                    Add_Rule_Check("StoreValue", Main_Line, (store_dimension.Text.Length > 0 ? store_dimension.Text : ""), store_bin.Text, (check20.Checked ? memory_arithmetic.Text : ""), comment_box.Text, index);
                }

            }
            else if (check_boxes[21].Checked && query_box.Text.Length > 0)
            {
                if (query_box.Text.ToUpper().Contains("DROP"))
                {
                    MessageBox.Show("Cannot use drop query");
                }
                else
                {
                    //Add_Rule_Check("RunQuery", "", enc_obj.Encrypt(query_box.Text), querybin.Text, "", comment_box.Text, index);
                    Add_Rule_Check("RunQuery", "", (query_box.Text), querybin.Text, "", comment_box.Text, index);
                }
            }
            else if (check_boxes[22].Checked && translate_line.Text.Length > 0)
            {
                Add_Rule_Check("RunTranslator", Main_Line, run_translator.Text, "", "", comment_box.Text, index);
            }
            else if (check_boxes[24].Checked && translate_line.Text.Length > 0 && warning_message.Text.Length > 0 && termination_check.Checked == false)
            {
                Add_Rule_Check("WarningMSG" + (silent_termination_check.Checked ? "_SILENT" : ""), Main_Line, warning_message.Text, "", "", comment_box.Text, index);
            }
            else if (check_boxes[24].Checked && translate_line.Text.Length > 0 && warning_message.Text.Length > 0 && termination_check.Checked)
            {
                Add_Rule_Check("WarningMSGwTERM" + (silent_termination_check.Checked ? "_SILENT" : ""), Main_Line, warning_message.Text, "", "", comment_box.Text, index);
            }
            else if (check_boxes[25].Checked && from_dest.Text.Length > 0 )
            {
                if (file_behaviour.Text == "Delete")
                {
                    Add_Rule_Check("Delete_File", Main_Line, from_dest.Text, "", "", comment_box.Text, index);
                }
                else if (file_behaviour.Text == "Move" && to_dest.Text.Length > 0)
                {
                    Add_Rule_Check("Move_File", Main_Line, from_dest.Text, to_dest.Text, "", comment_box.Text, index);
                }
                else if (file_behaviour.Text == "Copy" && to_dest.Text.Length > 0)
                {
                    Add_Rule_Check("Copy_File", Main_Line, from_dest.Text, to_dest.Text, "", "FIRST_INSTANCE_ONLY" + comment_box.Text, index);
                }
            }
            else if (Main_Line.Length > 0 && check_boxes[27].Checked && range_fn.Text.Length > 0 && range_from_line.Text.Length > 0 && range_to_line.Text.Length > 0 && (range_fn.Text == "Delete" || range_copy_move_line.Text.Length > 0))
            {
                if (!range_from_line.Text.Contains("%") && !range_to_line.Text.Contains("%") && Convert.ToInt32(range_from_line.Text) >= Convert.ToInt32(range_to_line.Text))
                {
                    MessageBox.Show("Invalid line range");
                    if (Main_Line.Contains("S~W~I")) Main_Line = Main_Line.Substring(5);
                    comment_box.Text = temp_comment_str;
                    ERROR_EXISTS = true;
                }
                else
                {
                    Add_Rule_Check("RANGE_FUNCTION", Main_Line, range_fn.Text + "|" + range_from_line.Text + "|" + range_to_line.Text + (range_copy_move_line.Text.Length > 0 ? "|" + range_copy_move_line.Text : ""), "", "", comment_box.Text, index);
                }
            }
            else if (Main_Line.Length > 0 && check_boxes[28].Checked && look_for_direction.Text.Length > 0 && look_for_value.Text.Length > 0 && look_for_lines.Text.Length > 0 && look_for_action.Text.Length > 0 && look_for_action_value.Text.Length > 0)
            {
                Add_Rule_Check("COMPARE_TO_REF_LINE", Main_Line, look_for_value.Text + "|" + look_for_lines.Text + "|" + look_for_direction.Text + "|" + look_for_action.Text + "|" + look_for_action_value.Text + "|" + (mark_ref_pt.Checked ? "1" : "0"), "", "", comment_box.Text, index);  
            }
            else if (Main_Line.Length > 0 && check_boxes[29].Checked && get_input_desc.Text.Length > 0 && get_input_bin.Text.Length > 0)
            {
                Add_Rule_Check("GET_INPUT", Main_Line, get_input_desc.Text + "|" + get_input_bin.Text, "", "", comment_box.Text, index);
            }
            else if (Main_Line.Length > 0 && check_boxes[30].Checked && delete_past_value.Text.Length > 0)
            {
                Add_Rule_Check("DELETE_PAST_VALUE", Main_Line, delete_past_value.Text, "", "", comment_box.Text, index);
            }
            else
            {
                ERROR_EXISTS = true;
                MessageBox.Show("Error in rule format");
                comment_box.Text = temp_comment_str;
                if (Main_Line.Contains("S~W~I")) Main_Line = Main_Line.Substring(5);
            }
            if (_MIRROR_MODE)
                MessageBox.Show("Rule successfully added");
        }

        #region Checking Boxes via changes
        private void check1_CheckedChanged(object sender, EventArgs e) { if (check1.Checked) Check_Box_Validity(check1); }
        private void check2_CheckedChanged(object sender, EventArgs e) { if (check2.Checked) Check_Box_Validity(check2); }
        private void check3_CheckedChanged(object sender, EventArgs e) { if (check3.Checked) Check_Box_Validity(check3); } // Part of Arithmetic
        private void check4_CheckedChanged(object sender, EventArgs e) { if (check4.Checked) Check_Box_Validity(check4); } // Part of Arithmetic
        private void check5_CheckedChanged(object sender, EventArgs e) { if (check5.Checked) Check_Box_Validity(check5); } // Part of Arithmetic
        private void check6_CheckedChanged(object sender, EventArgs e) { if (check6.Checked) Check_Box_Validity(check6); } // Part of Arithmetic
        private void check7_CheckedChanged(object sender, EventArgs e) { if (check7.Checked) Check_Box_Validity(check7); }
        private void check8_CheckedChanged(object sender, EventArgs e) { if (check8.Checked) Check_Box_Validity(check8); portion_a.Text = Main_Line; }
        private void check9_CheckedChanged(object sender, EventArgs e) { if (check9.Checked) Check_Box_Validity(check9); }
        private void check10_CheckedChanged(object sender, EventArgs e) { if (check10.Checked) Check_Box_Validity(check10); }
        private void check11_CheckedChanged(object sender, EventArgs e) { if (check11.Checked) Check_Box_Validity(check11); }
        private void check12_CheckedChanged(object sender, EventArgs e) { if (check12.Checked) Check_Box_Validity(check12); }
        private void check15_CheckedChanged(object sender, EventArgs e) { if (check15.Checked) Check_Box_Validity(check15); }
        private void check13_CheckedChanged(object sender, EventArgs e) { if (check13.Checked) Check_Box_Validity(check13); }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { if (checkBox1.Checked) Check_Box_Validity(checkBox1); }
        private void check16_CheckedChanged_1(object sender, EventArgs e) { if (check16.Checked) Check_Box_Validity(check16); }
        private void check17_CheckedChanged_1(object sender, EventArgs e) { if (check17.Checked) Check_Box_Validity(check17); }
        private void check18_CheckedChanged_1(object sender, EventArgs e) { if (check18.Checked) Check_Box_Validity(check18); }
        private void check19_CheckedChanged_1(object sender, EventArgs e) { if (check19.Checked) Check_Box_Validity(check19); }
        private void check20_CheckedChanged(object sender, EventArgs e) { if (check20.Checked) Check_Box_Validity(check20); }
        private void check21_CheckedChanged(object sender, EventArgs e) { if (check21.Checked) Check_Box_Validity(check21); }
        private void check22_CheckedChanged(object sender, EventArgs e) { if (check22.Checked) Check_Box_Validity(check22); }
        private void check23_CheckedChanged(object sender, EventArgs e) { if (check23.Checked) Check_Box_Validity(check23); }
        private void check24_CheckedChanged(object sender, EventArgs e) { if (check24.Checked) Check_Box_Validity(check24); }
        private void check25_CheckedChanged(object sender, EventArgs e) { if (check25.Checked) Check_Box_Validity(check25); }
        private void check26_CheckedChanged(object sender, EventArgs e) { if (check26.Checked) Check_Box_Validity(check26); }
        private void check27_CheckedChanged(object sender, EventArgs e) { if (check27.Checked) Check_Box_Validity(check27); }
        private void check28_CheckedChanged(object sender, EventArgs e) { if (check28.Checked) Check_Box_Validity(check28); }
        private void check29_CheckedChanged(object sender, EventArgs e) { if (check29.Checked) Check_Box_Validity(check29); }
        private void check30_CheckedChanged(object sender, EventArgs e) { if (check30.Checked) Check_Box_Validity(check30); }
        private void check31_CheckedChanged(object sender, EventArgs e) { if (check31.Checked) Check_Box_Validity(check31); }
        #endregion

        // Main_Line updater
        private void name_TextChanged(object sender, EventArgs e)
        {
            translate_line.BackColor = Color.DarkGray;
            Main_Line = translate_line.Text;
            //if (check_boxes[7].Checked) portion_a.Text = Main_Line;
            //richTextBox14.Text = "*Note that all actions ABOVE only affect lines that containing the text '" + Main_Line + "'";
            //if (Main_Line.Length == 0)
                //richTextBox14.Text = "*Note that all actions ABOVE affect ALL lines in the translation";
        }

        // Save button
        private void button1_Click(object sender, EventArgs e)
        {
            bool error_found = false;
            if (x_value.Text.Length > 0) if (x_value.Text[0].ToString() == "." || x_value.Text[x_value.Text.Length - 1].ToString() == ".") error_found = true;
            if (y_value.Text.Length > 0) if (y_value.Text[0].ToString() == "." || y_value.Text[y_value.Text.Length - 1].ToString() == ".") error_found = true;
            if (z_value.Text.Length > 0) if (z_value.Text[0].ToString() == "." || z_value.Text[z_value.Text.Length - 1].ToString() == ".") error_found = true;
            if (n_value.Text.Length > 0) if (n_value.Text[0].ToString() == "." || n_value.Text[n_value.Text.Length - 1].ToString() == ".") error_found = true;
            if  (!error_found)
            {
                if (!_EDIT_MODE)
                {
                    bool selected = false;
                    foreach (CheckBox g in check_boxes)
                    {
                        if (g.Checked) selected = true;
                    }
                    if (check_boxes[8].Checked || check_boxes[9].Checked || check_boxes[10].Checked || check_boxes[11].Checked || check_boxes[14].Checked || check_boxes[12].Checked && selected)
                    {
                        DialogResult dialogResult = MessageBox.Show("Please note that if there are rules affecting line(s) beyond the amount of lines within the file will not execute. Continue?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Perform_Save_Rule();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                    else if (selected)
                    {
                        Perform_Save_Rule();
                    }
                }
                else
                {
                    if (!ERROR_EXISTS)
                    {
                        // check why save rule button doesnt actually toggle ERROR_EXISTS
                        if (_SELECTED_INDICIES.Count > 0)
                        {
                            Perform_Save_Rule(_SELECTED_INDICIES[0]);
                            /*foreach (int index in _SELECTED_INDICIES)
                            {
                                _parent.delete_rule(index + 1);
                                _parent.Set_Rule_Selection(index);
                            }*/
                        }
                        else
                        {
                            Perform_Save_Rule(_EDIT_SELECTED_INDEX);
                            _parent.delete_rule(_EDIT_SELECTED_INDEX + 1);
                            _parent.Set_Rule_Selection(_EDIT_SELECTED_INDEX);
                        }
                        this.Close();
                        this.Dispose();
                    }
                    ERROR_EXISTS = false;
                }
                
            }
            else
            {
                MessageBox.Show("Error in rule format");
            }
        }


        // Setup all the controls according to the type of edit
        private void Edit_Parse()
        {
            /*  PARAMETERS
                private bool   _EDIT_MODE = false;
                private int    _EDIT_SELECTED_INDEX;
                private string _EDIT_ACTION;
                private string _SEARCH_VALUE;
                private string _DIMENSION;
                private string _VALUE;
                private string _OPTIONAL_VALUE;
                private string _COMMENT;
            */
            start_with_instead.Enabled = false;
            condition_met.Checked = false;
            condition_met.Enabled = false;
            range_fn.Enabled = false;
            file_behaviour.Enabled = false;
            check_once.Enabled = false;

            // AUTOFILL APPROPRIATE BOXES
            if (_COMMENT.Contains("FIRST_INSTANCE_ONLY"))
            {
                check_once.Checked = true;
                check_once.BackColor = Color.LightYellow;
                check_once.Enabled = true;
            }
            if (_COMMENT.Contains("I_L_N"))
            {
                ignoreNum.Checked = true;
                ignoreNum.BackColor = Color.LightYellow;
                ignoreNum.Enabled = false;
            }
            else
            {
                ignoreNum.Checked = false;
                ignoreNum.Enabled = false;
            }
            if (_COMMENT.Contains("IFCHK"))
            {
                condition_met.Checked = true;
                condition_met.BackColor = Color.LightYellow;
                //condition_met.Enabled = false;
                condition_bin.ForeColor = Color.Red;
                condition_bin.BackColor = Color.LightYellow;
                condition_value.ForeColor = Color.Red;
                condition_value.BackColor = Color.LightYellow;
            }
            if (_SEARCH_VALUE.Length > 0)
            {
                if (_SEARCH_VALUE.Contains("S~W~I"))
                {
                    start_with_instead.Enabled = true;
                    start_with_instead.Checked = true;
                    start_with_instead.BackColor = Color.LightYellow;
                    translate_line.Text = _SEARCH_VALUE.Substring(5);
                }
                else
                {
                    translate_line.Text = _SEARCH_VALUE;
                }
                translate_line.ForeColor = Color.Red;
                translate_line.BackColor = Color.LightYellow;
            }
            if (_COMMENT.Length > 0 && _COMMENT.Contains("FIRST_INSTANCE_ONLY"))
            {
                _COMMENT = _COMMENT.Contains("FIRST_INSTANCE_ONLY") ? _COMMENT.Remove(_COMMENT.IndexOf("FIRST_INSTANCE_ONLY"), 19) : _COMMENT;
                comment_box.ForeColor = Color.Red;
            }
            if (_COMMENT.Length > 0 && _COMMENT.Contains("I_L_N"))
            {
                _COMMENT = _COMMENT.Contains("I_L_N") ? _COMMENT.Substring(0, _COMMENT.IndexOf("I_L_N")) + _COMMENT.Substring(_COMMENT.IndexOf("I_L_N") + 5) : _COMMENT;
                comment_box.ForeColor = Color.Red;
            }
            if (_COMMENT.Length > 0)
            {
                int CHK_LEN = 0;
                if (_COMMENT.Contains("IFCHK"))
                {
                    int a = _COMMENT.IndexOf("))");
                    int b = _COMMENT.IndexOf("((") + 2;
                    CHK_LEN = a + 2;
                    string[] temp = _COMMENT.Substring(b, a-b).Split(new string[] { ":" }, StringSplitOptions.None);
                    condition_bin.Text = temp[0];
                    condition_direction.Text = temp[1];
                    condition_value.Text = temp[2];
                    _COMMENT = _COMMENT.Contains("IFCHK") ? _COMMENT.Substring(CHK_LEN) : _COMMENT;
                    comment_box.ForeColor = Color.Red;
                }
            }
            comment_box.Text = _COMMENT;
            if (_EDIT_ACTION == "Replace")
            {
                check_boxes[0].Checked = true;
                check_boxes[0].BackColor = Color.LightYellow;
                replace_line.Text = _VALUE;
                replace_line.BackColor = Color.LightYellow;
                replace_line.ForeColor = Color.Red;
            }
            if (_EDIT_ACTION == "Add" || _EDIT_ACTION == "Divide" || _EDIT_ACTION == "Multiply" || _EDIT_ACTION == "Subtract" || _EDIT_ACTION == "Set")
            {
                sub_expander2.Expand();
                if (_DIMENSION == "X")
                {
                    check_boxes[1].Checked = true;
                    check_boxes[1].BackColor = Color.LightYellow;
                    check_boxes[2].BackColor = Color.LightYellow;
                    check_boxes[2].Checked = true;
                    x_arithmetic.Text = _EDIT_ACTION;
                    x_arithmetic.ForeColor = Color.Red;
                    x_arithmetic.BackColor = Color.LightYellow;
                    x_value.Text = _VALUE;
                    x_value.ForeColor = Color.Red;
                    if (_SEARCH_VALUE == "") check_boxes[5].Checked = true;
                }
                else if (_DIMENSION == "Y")
                {
                    check_boxes[1].Checked = true;
                    check_boxes[1].BackColor = Color.LightYellow;
                    check_boxes[3].BackColor = Color.LightYellow;
                    check_boxes[3].Checked = true;
                    y_arithmetic.Text = _EDIT_ACTION;
                    y_arithmetic.ForeColor = Color.Red;
                    y_arithmetic.BackColor = Color.LightYellow;
                    y_value.Text = _VALUE;
                    y_value.ForeColor = Color.Red;
                    if (_SEARCH_VALUE == "") check_boxes[5].Checked = true;
                }
                else if (_DIMENSION == "Z")
                {
                    check_boxes[1].Checked = true;
                    check_boxes[4].Checked = true;
                    check_boxes[1].BackColor = Color.LightYellow;
                    check_boxes[4].BackColor = Color.LightYellow;
                    z_arithmetic.Text = _EDIT_ACTION;
                    z_arithmetic.ForeColor = Color.Red;
                    z_arithmetic.BackColor = Color.LightYellow;
                    z_value.Text = _VALUE;
                    z_value.ForeColor = Color.Red;
                    if (_SEARCH_VALUE == "") check_boxes[5].Checked = true;
                }
                else
                {
                    check_boxes[1].Checked = true;
                    check_boxes[23].Checked = true;
                    check_boxes[1].BackColor = Color.LightYellow;
                    check_boxes[23].BackColor = Color.LightYellow;
                    n_arithmetic.Text = _EDIT_ACTION;
                    n_dimension.Text = _DIMENSION;
                    n_arithmetic.ForeColor = Color.Red;
                    n_arithmetic.BackColor = Color.LightYellow;
                    n_value.Text = _VALUE;
                    n_value.ForeColor = Color.Red;
                    n_dimension.ForeColor = Color.Red;
                    if (_SEARCH_VALUE == "") check_boxes[5].Checked = true;
                }
            }
            if (_EDIT_ACTION == "DeleteIf")
            {
                check_boxes[6].Checked = true;
                check_boxes[6].BackColor = Color.LightYellow;
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION == "ReplaceSpecific")
            {
                check_boxes[7].Checked = true;
                check_boxes[7].BackColor = Color.LightYellow;
                portion_a.Text = _VALUE;
                portion_a.ForeColor = Color.Red;
                portion_a.BackColor = Color.LightYellow;
                portion_b.Text = _OPTIONAL_VALUE;
                portion_b.ForeColor = Color.Red;
                portion_b.BackColor = Color.LightYellow;
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION == "DeleteFrom")
            {
                check_boxes[8].Checked = true;
                check_boxes[8].BackColor = Color.LightYellow;
                delete_lines.Text = _VALUE;
                delete_lines.ForeColor = Color.Red;
                delete_lines.BackColor = Color.LightYellow;
                if (Convert.ToInt32(_VALUE) < 0) delete_lines.Text = _VALUE.Substring(1); // Remove Hyphen
                delete_a.Text = "Above";
                delete_a.ForeColor = Color.Red;
                if (Convert.ToInt32(_VALUE) > 0) delete_a.Text = "Below";
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION == "InsertFrom")
            {
                check_boxes[9].Checked = true;
                check_boxes[9].BackColor = Color.LightYellow;
                insert_lines.Text = _VALUE;
                insert_lines.ForeColor = Color.Red;
                insert_lines.BackColor = Color.LightYellow;
                if (Convert.ToInt32(_VALUE) < 0) insert_lines.Text = _VALUE.Substring(1);
                insert_from_optional.Text = _OPTIONAL_VALUE;
                insert_from_optional.ForeColor = Color.Red;
                insert_from_optional.BackColor = Color.LightYellow;
                insert_a.Text = "Above";
                insert_a.ForeColor = Color.Red;
                if (Convert.ToInt32(_VALUE) > 0) insert_a.Text = "Below";
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION == "DeleteAt")
            {
                if (_SEARCH_VALUE.Contains("|")) { button16.BackColor = Color.LightYellow; button16.ForeColor = Color.Red; } translate_line.Enabled = false;
                check_boxes[10].Checked = true;
                check_boxes[10].BackColor = Color.LightYellow;
                delete_line_number.Text = _VALUE;
                delete_line_number.ForeColor = Color.Red;
                delete_line_number.BackColor = Color.LightYellow;
                expander2.Expand();
            }
            if (_EDIT_ACTION == "InsertAt")
            {
                if (_SEARCH_VALUE.Contains("|")) { button16.BackColor = Color.LightYellow; button16.ForeColor = Color.Red; } translate_line.Enabled = false;
                check_boxes[11].Checked = true;
                check_boxes[11].BackColor = Color.LightYellow;
                insert_line_number.Text = _VALUE;
                insert_line_number.ForeColor = Color.Red;
                insert_line_number.BackColor = Color.LightYellow;
                insert_line_text.Text = _OPTIONAL_VALUE;
                insert_line_text.ForeColor = Color.Red;
                insert_line_text.BackColor = Color.LightYellow;
                expander2.Expand();
            }
            if (_EDIT_ACTION == "Switch")
            {
                if (_SEARCH_VALUE.Contains("|")) { button16.BackColor = Color.LightYellow; button16.ForeColor = Color.Red; } translate_line.Enabled = false;
                check_boxes[12].Checked = true;
                check_boxes[12].BackColor = Color.LightYellow;
                switch_a.Text = _VALUE;
                switch_a.ForeColor = Color.Red;
                switch_a.BackColor = Color.LightYellow;
                switch_b.Text = _OPTIONAL_VALUE; 
                switch_b.ForeColor = Color.Red;
                switch_b.BackColor = Color.LightYellow;
                expander2.Expand();
            }
            if (_EDIT_ACTION == "MultiRule")
            {
                check_boxes[13].Checked = true;
                check_boxes[13].BackColor = Color.LightYellow;
                _MULTI_CONDITIONAL_PARAMETERS.Add(_VALUE);
                _MULTI_CONDITIONAL_PARAMETERS.Add(_OPTIONAL_VALUE);
                sub_expander6.Expand();
            }
            if (_EDIT_ACTION == "ReplaceAt")
            {
                if (_SEARCH_VALUE.Contains("|")) { button16.BackColor = Color.LightYellow; button16.ForeColor = Color.Red; }
                check_boxes[14].Checked = true;
                check_boxes[14].BackColor = Color.LightYellow;
                translate_line.Enabled = false;
                replace_line_number.Text = _VALUE;
                replace_line_number.ForeColor = Color.Red;
                replace_line_number.BackColor = Color.LightYellow;
                replace_line_text.Text = _OPTIONAL_VALUE;
                replace_line_text.ForeColor = Color.Red;
                replace_line_text.BackColor = Color.LightYellow;
                expander2.Expand();
            }
            if (_EDIT_ACTION == "LastIndexOfReplace")
            {
                check_boxes[15].Checked = true;
                check_boxes[15].BackColor = Color.LightYellow;
                replace_within_text.Text = _VALUE;
                replace_within_text.ForeColor = Color.Red;
                replace_within_text.BackColor = Color.LightYellow;
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION == "StoreValue")
            {
                check_boxes[16].Checked = true;
                check_boxes[16].BackColor = Color.LightYellow;
                store_bin.Text = _VALUE;
                store_bin.ForeColor = Color.Red;
                store_bin.BackColor = Color.LightYellow;
                if (_DIMENSION.Contains("@!"))
                {
                    check_boxes[26].Checked = true;
                    check_boxes[26].BackColor = Color.LightYellow;
                }
                else if (_DIMENSION.Length > 0 && !(_DIMENSION.Contains("@#"))) // If we want the dimension value:
                {
                    store_dimension.Text = _VALUE;
                    store_dimension.ForeColor = Color.Red;
                    store_dimension.BackColor = Color.LightYellow;
                    store_dimension.Text = _DIMENSION;
                    check_boxes[18].Checked = true;
                    check_boxes[18].BackColor = Color.LightYellow;
                }
                else if (_DIMENSION.Contains("@#"))
                {
                    check_boxes[20].Checked = true;
                    check_boxes[20].BackColor = Color.LightYellow;
                    store_specific.Text = _DIMENSION.Substring(2);
                    store_specific.ForeColor = Color.Red;
                    store_specific.BackColor = Color.LightYellow;
                }
                else
                {
                    check_boxes[17].Checked = true;
                    check_boxes[17].BackColor = Color.LightYellow;
                }
                if (_OPTIONAL_VALUE.Length > 0)
                {
                    check_boxes[19].Checked = true;
                    check_boxes[19].BackColor = Color.LightYellow;
                    memory_arithmetic.Text = _OPTIONAL_VALUE;
                }
                sub_expander3.Expand();
            }
            if (_EDIT_ACTION == "RunQuery")
            {
                check_boxes[21].Checked = true;
                check_boxes[21].BackColor = Color.LightYellow;
                //query_box.Text = enc_obj.Decrypt(_DIMENSION);
                query_box.Text = (_DIMENSION);
                querybin.Text = _VALUE;
                query_box.ForeColor = Color.Red;
                query_box.BackColor = Color.LightYellow;
                expander3.Expand();     
            }
            if (_EDIT_ACTION == "RunTranslator")
            {
                check_boxes[22].Checked = true;
                check_boxes[22].BackColor = Color.LightYellow;
                run_translator.Text = _DIMENSION;
                run_translator.ForeColor = Color.Red;
                run_translator.BackColor = Color.LightYellow;
                sub_expander5.Expand();
            }
            if (_EDIT_ACTION.Contains("WarningMSGwTERM"))
            {
                check_boxes[24].Checked = true;
                check_boxes[24].BackColor = Color.LightYellow;
                warning_message.Text = _DIMENSION;
                warning_message.ForeColor = Color.Red;
                warning_message.BackColor = Color.LightYellow;
                termination_check.Checked = true;
                termination_check.Enabled = false;
                termination_check.BackColor = Color.LightYellow;
                sub_expander4.Expand();
            }
            if (_EDIT_ACTION.Contains("WarningMSG") && !(_EDIT_ACTION.Contains("WarningMSGwTERM")))
            {
                check_boxes[24].Checked = true;
                check_boxes[24].BackColor = Color.LightYellow;
                warning_message.Text = _DIMENSION;
                warning_message.ForeColor = Color.Red;
                warning_message.BackColor = Color.LightYellow;
                sub_expander4.Expand();
            }
            if (_EDIT_ACTION.Contains("_SILENT"))
            {
                silent_termination_check.Visible = true;
                silent_termination_check.Checked = true;
                silent_help_button.Visible = true;
            }
            if (_EDIT_ACTION.Contains("DELETE_PAST_VALUE"))
            {
                check_boxes[30].Checked = true;
                check_boxes[30].BackColor = Color.LightYellow;
                delete_past_value.Text = _DIMENSION;
                delete_past_value.Enabled = true;
                delete_past_value.ForeColor = Color.Red;
                delete_past_value.BackColor = Color.LightYellow;
                sub_expander1.Expand();
            }
            if (_EDIT_ACTION.Contains("RANGE_FUNCTION"))
            {
                string[] temp = _DIMENSION.Split(new string[] { "|" }, StringSplitOptions.None);
                check_boxes[27].Checked = true;
                check_boxes[27].BackColor = Color.LightYellow;
                range_fn.Enabled = true;
                range_fn.Text = temp[0];
                range_copy_move_line.Enabled = true;
                if (temp.Count() > 3) range_copy_move_line.Text = temp[3];
                range_copy_move_line.ForeColor = Color.Red;
                range_copy_move_line.BackColor = Color.LightYellow;
                range_to_line.Enabled = true;
                range_to_line.ForeColor = Color.Red;
                range_to_line.BackColor = Color.LightYellow;
                range_to_line.Text = temp[2];
                range_from_line.Enabled = true;
                range_from_line.ForeColor = Color.Red;
                range_from_line.BackColor = Color.LightYellow;
                range_from_line.Text = temp[1];
                sub_expander5.Expand();
            }
            if (_EDIT_ACTION.Contains("COMPARE_TO_REF_LINE"))
            {
                string[] temp = _DIMENSION.Split(new string[] { "|" }, StringSplitOptions.None);
                check_boxes[28].Checked = true;
                check_boxes[28].BackColor = Color.LightYellow;
                look_for_value.Text = temp[0];
                look_for_lines.Text = temp[1];
                look_for_direction.Text = temp[2];
                look_for_action.Text = temp[3];
                look_for_action_value.Text = temp[4];

                look_for_value.ForeColor = Color.Red;
                look_for_value.BackColor = Color.LightYellow;
                look_for_lines.ForeColor = Color.Red;
                look_for_lines.BackColor = Color.LightYellow;
                look_for_direction.ForeColor = Color.Red;
                look_for_direction.BackColor = Color.LightYellow;
                look_for_action.ForeColor = Color.Red;
                look_for_action.BackColor = Color.LightYellow;
                look_for_action_value.ForeColor = Color.Red;
                look_for_action_value.BackColor = Color.LightYellow;
                sub_expander6.Expand();

                if (temp[5] == "1")
                {
                    mark_ref_pt.Checked = true;
                }
            }
            if (_EDIT_ACTION.Contains("_File"))
            {
                sub_expander5.Expand();
                from_dest.Text = _DIMENSION;
                check_boxes[25].Checked = true;
                check_boxes[25].BackColor = Color.LightYellow;
                if (_EDIT_ACTION.Contains("Delete"))
                {
                    file_behaviour.Text = "Delete";
                }
                if (_EDIT_ACTION.Contains("Move"))
                {
                    file_behaviour.Text = "Move";
                    to_dest.Text = _VALUE;
                    to_dest.BackColor = Color.LightYellow;
                    to_dest.ForeColor = Color.Red;
                }
                if (_EDIT_ACTION.Contains("Copy"))
                {
                    file_behaviour.Text = "Copy";
                    to_dest.Text = _VALUE;
                    to_dest.BackColor = Color.LightYellow;
                    to_dest.ForeColor = Color.Red;
                }
                file_behaviour.ForeColor = Color.Red;
                from_dest.BackColor = Color.LightYellow;
                from_dest.ForeColor = Color.Red;
            }
            if (_EDIT_ACTION.Contains("GET_INPUT"))
            {
                string[] temp = _DIMENSION.Split(new string[] { "|" }, StringSplitOptions.None);
                check_boxes[29].Checked = true;
                get_input_desc.Text = temp[0];
                get_input_desc.BackColor = Color.LightYellow;
                get_input_desc.ForeColor = Color.Red;
                get_input_bin.Text = temp[1];
                get_input_bin.BackColor = Color.LightYellow;
                get_input_bin.ForeColor = Color.Red;
                sub_expander4.Expand();
            }

            if (_COMMENT.Contains("*SPECIAL MODE RULE*")) comment_box.Enabled = false;
        }

        #region HANDLING OBJECT CHANGES

        private void replace_line_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[0].Checked = true;
        }

        public void Check_Number_Input(TextBox text, int Check_Boxes_Index=-1, bool delete_insert_check=false) // delete insert check excludes placement of DECIMALS
        {
            if (text.Text.Length > 0)
            {
                // check_boxes_index < 0 means outside call
                if (!_EDIT_MODE && Check_Boxes_Index >= 0) check_boxes[Check_Boxes_Index].Checked = true;
                bool has_decimal = false;
                if (text.Text.Substring(0, text.Text.Length - 1).Contains(".")) has_decimal = true;
                if (!(text.Text.Contains("%")) && !char.IsDigit(text.Text[text.Text.Length - 1]) && !(text.Text[text.Text.Length - 1].ToString() == ".") || text.Text[0].ToString() == ".")
                {
                    // If letter in SO_number box, do not output and move CARET to end
                    text.Text = text.Text.Substring(0, text.Text.Length - 1);
                    text.SelectionStart = text.Text.Length;
                    text.SelectionLength = 0;
                }
                if (!(text.Text.Contains("%")) && text.Text.Length > 0 && text.Text[text.Text.Length - 1].ToString() == "." && has_decimal)
                {
                    text.Text = text.Text.Substring(0, text.Text.Length - 1);
                    text.SelectionStart = text.Text.Length;
                    text.SelectionLength = 0;
                }
                if (!(text.Text.Contains("%")) && text.Text.Length > 0 && delete_insert_check && text.Text[text.Text.Length - 1].ToString() == ".")
                {
                    if (text.Text.Length > 0)
                    {
                        text.Text = text.Text.Substring(0, text.Text.Length - 1);
                        text.SelectionStart = text.Text.Length;
                        text.SelectionLength = 0;
                    }
                    else
                    {
                        text.Text = "";
                        text.SelectionStart = 0;
                        text.SelectionLength = 0;
                    }
                }
            }
        }

        private void run_translator_TextChanged(object sender, EventArgs e)
        {
            check23.Checked = true;
        }

        private void x_value_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(x_value, 2);
        }

        private void x_arithmetic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[2].Checked = true;
        }

        private void y_value_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(y_value, 3);
        }

        private void y_arithmetic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[3].Checked = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // z-value
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(z_value, 4);
        }

        private void z_arithmetic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[4].Checked = true;
        }

        private void portion_a_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[7].Checked = true;
        }

        private void portion_b_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[7].Checked = true;
        }

        private void delete_a_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[8].Checked = true;
        }

        private void insert_a_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[9].Checked = true;
        }

        private void delete_lines_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE)
            Check_Number_Input(delete_lines, 8, true);
        }

        private void insert_lines_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(insert_lines, 9, true);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[9].Checked = true;
        }

        private void delete_line_number_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(delete_line_number, 10, true);
        }

        private void insert_line_number_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(insert_line_number, 11, true);
        }

        private void replace_line_number_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(replace_line_number, 14, true);
        }

        private void insert_line_text_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[11].Checked = true;
        }

        private void replace_line_text_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[14].Checked = true;
        }

        private void switch_a_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(switch_a, 12, true);
        }

        private void switch_b_TextChanged(object sender, EventArgs e)
        {
            //if (!_EDIT_MODE) 
            Check_Number_Input(switch_b, 12, true);
        }

        private void replace_within_text_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check_boxes[15].Checked = true;
        }

        // Clear all fields
        private void clear_button_Click(object sender, EventArgs e)
        {
            translate_line.Clear();
            replace_line.Clear();
            x_value.Clear();
            y_value.Clear();
            z_value.Clear();
            x_arithmetic.SelectedIndex = -1;
            y_arithmetic.SelectedIndex = -1;
            z_arithmetic.SelectedIndex = -1;
            portion_a.Clear();
            portion_b.Clear();
            delete_lines.Clear();
            insert_lines.Clear();
            insert_from_optional.Clear();
            delete_a.SelectedIndex = -1;
            insert_a.SelectedIndex = -1;
            switch_a.Clear();
            switch_b.Clear();
            insert_line_number.Clear();
            delete_line_number.Clear();
            insert_line_text.Clear();
            comment_box.Clear();
            replace_line.Clear();
            replace_line_number.Clear();
            replace_within_text.Clear();
            store_dimension.Clear();
            store_bin.Clear();
            foreach (CheckBox g in check_boxes)
            {
                g.Checked = false;
            }
        }
        #endregion


        private void n_value_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(n_value, 23);
        }

        private void n_arithmetic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check24.Checked = true;
        }

        private void n_dimension_TextChanged(object sender, EventArgs e)
        {
            if (!_EDIT_MODE) check24.Checked = true;
        }


        #region METHOD DUMP
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region HELP BUTTONS
        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "If you want the action to be specifically executed for line(s) containing 'G50' , enter G50 into the field");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Replaces an entire line containing * text with the value in the field. For example: this rule will can replace T0303 with T0300");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Perform certain arithmetic algorithms on specific dimensions. For example, a rule can ADD the X dimensions by 15 for lines containing 'G00'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Delete entire line containing the text *. For example, delete a line containing 'G00 X450.0'");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "This one may be a little confusing*" + Environment.NewLine + "Search for line containing *. When found, look inside that line for a certain value and replace it with a new line. For example, find 'G00'. When found, look inside that line and find the Z dimension and change it to Y.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Using the line containing * as a reference point, delete the line that is exactly x units above or below this reference point. For example, if we want to delete every line above any occurnences of 'M41', we would delete the line that is 1 line above 'M41'");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Using the line containing * as a reference point, insert 'G00' into lines that are exactly x units above or below this reference point. For example, if we want to insert the text 'G00' in every line above any occurnences of 'M41', we would insert the line that is 1 line above 'M41'");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Delete a specific line number. If you want to delete line 'N4 G50 X45.....', just delete line '4'");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Insert text into a specific line number. If you want to insert '(PLATE)' in the first or second line, just insert '(PLATE)' in line 1 or 2.");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Swap the values of two lines");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show(example_intro + "Execute a list of actions based on whether or not a specific set of conditions are satisfied. ALL of these actions and requirements of conditions are based on 'REFERENCE' LINE number");
        }


        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This function will look through your code for the reference line. When found, you can either store the entire line or choose to find the value associated with a certain dimension. For example, you can store the X value of the following line: X290.00 Y400 and the value stored will be exactly 290.00" + Environment.NewLine + "You can recall the stored information later using %(bin#)%. For example, recalling the value in bin 55 would be typing in a text box '%55%'");
        }

        // Edit conditions and actions
        private void button13_Click(object sender, EventArgs e)
        {
            Rule_Sub_Routine rule;
            if (!_EDIT_MODE) 
                rule = new Rule_Sub_Routine(this, translate_line.Text, new List<string>());
            else
                rule = new Rule_Sub_Routine(this, translate_line.Text, _MULTI_CONDITIONAL_PARAMETERS, _COMMENT.Contains("SPECIAL MODE RULE"));
            rule.ShowDialog();
        }

        // Replace within HELP
        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Replace the last instance of the reference line with a new value. For example, check for last 'T0600' and replace with 'T0400'");
        }

        private void store_bin_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(store_bin, 16, true);
        }

        private void store_dimension_TextChanged(object sender, EventArgs e)
        {
            check_boxes[18].Checked = true;

        }

        // Add Global Condition
        private void button16_Click(object sender, EventArgs e)
        {
            string[] temp = _SEARCH_VALUE.Split(new string[] { "|" }, StringSplitOptions.None);
            Add_Global_Condition g;
            if (!_EDIT_MODE && !_SEARCH_VALUE.Contains("|")) // Not edit
                g = new Add_Global_Condition(this);
            else if (!_SEARCH_VALUE.Contains("|")) // Edit without having a condition before
                g = new Add_Global_Condition(this);
            else
                g = new Add_Global_Condition(this, temp[0], temp[1], temp[2]); // Edit with existing condition
            g.ShowDialog();
        }


        private void query_box_TextChanged(object sender, EventArgs e)
        {
            check_boxes[21].Checked = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Run a Query command. Please note that you cannot drop table using this functionality");
        }


        private void condition_bin_TextChanged(object sender, EventArgs e)
        {
            condition_met.Checked = true;
            Check_Number_Input(condition_bin, -1, true);
        }

        private void condition_value_TextChanged(object sender, EventArgs e)
        {
            condition_met.Checked = true;
            Check_Number_Input(condition_value, -1, false);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Check if a value in a certain bin # and compare it to another value. If satisfied, execute rule");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("When the rule conditions are satisfied, display a warning message. If 'Allow Termination' is checked, this rule will open a YES/NO dialog prompt for operator to determine whether or not to terminate program. If (No) terminate program. If (Yes) continue program from here. Please do note that termination of program is simply not outputting the file.  If the input and output destination of the file for this translator is the same, the termination only affects THIS translator and the next step will still execute.");
        }

        private void warning_message_TextChanged(object sender, EventArgs e)
        {
            check_boxes[24].Checked = true;
        }

        private void check_once_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void file_behaviour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (file_behaviour.Text == "Delete")
            {
                to_dest.Visible = false;
                to_dest_text.Visible = false;
            }
            else
            {
                to_dest.Visible = true;
                to_dest_text.Visible = true;
            }
            check_boxes[25].Checked = true;
        }

        private void termination_check_CheckedChanged(object sender, EventArgs e)
        {
            if (termination_check.Checked)
            {
                silent_termination_check.Visible = true;
                silent_help_button.Visible = true;
            }
            else
            {
                silent_termination_check.Checked = false;
                silent_termination_check.Visible = false;
                silent_help_button.Visible = false;
            }
        }

        private void silent_help_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Checking 'Silent' will terminate the program silently without any messages");
        }


        private void button20_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You can execute certain file behaviour. Make sure you enter the correct path");
        }

        #endregion

        private void button22_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void condition_direction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void range_fn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (range_fn.Text != "Delete")
            {
                range_copy_move_label.Visible = true;
                range_copy_move_line.Visible = true;
            }
            else
            {
                range_copy_move_label.Visible = false;
                range_copy_move_line.Visible = false;
            }

        }

        private void range_from_line_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(range_from_line, 27, true);
        }

        private void range_to_line_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(range_to_line, 27, true);
        }

        private void range_copy_move_line_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(range_copy_move_line, 27, true);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Perform G-Code block functionalities here. You must obtain the line numbers to reference the from-to points");
        }

        private void Add_Rule_Load(object sender, EventArgs e)
        {
            #region Main Accordion 
            this.Location = spawn_location;

            // Adjust to main panel size
            panel2.Size = panel1.Size;
            panel2.Location = panel1.Location;
            panel3.Size = panel1.Size;
            panel3.Location = panel1.Location;

            accordion.Size = panel1.Size;
            accordion.Location = panel1.Location;

            // panel 1
            expander.Size = panel1.Size;
            expander.Padding = new Padding(0);
            expander.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander, "Rules that require a line reference point (conditional rule execution)", System.Drawing.ColorTranslator.FromHtml("#D0EBFF"), Color.Black);
            this.Controls.Remove(panel1);
            expander.Content = panel1;
            accordion.Add(expander);

            // panel 2
            expander2.Size = panel1.Size;
            expander2.Padding = new Padding(0);
            expander2.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander2, "Rules that require a specific line number", System.Drawing.ColorTranslator.FromHtml("#B5E0FF"), Color.Black);
            this.Controls.Remove(panel2);
            expander2.Content = panel2;
            accordion.Add(expander2);

            // panel 2
            expander3.Size = panel1.Size;
            expander3.Padding = new Padding(0);
            expander3.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander3, "Run a specific query", System.Drawing.ColorTranslator.FromHtml("#A2D7FF"), Color.Black);
            this.Controls.Remove(panel3);
            expander3.Content = panel3;
            accordion.Add(expander3);

            this.Controls.Add(accordion);

            #endregion

            #region Sub Accordion

            // Adjust to main panel size
            //panel4.Size = new Size(662, 610);
            sub_accordion.Size = new Size(662, 390);
            sub_accordion.Location = new Point(4, 17);
            panel4.Location = new Point(4, 17);
            panel5.Size = panel4.Size;
            panel5.Location = panel4.Location;
            panel6.Size = panel4.Size;
            panel6.Location = panel4.Location;
            panel7.Size = panel4.Size;
            panel7.Location = panel4.Location;
            panel8.Size = panel4.Size;
            panel8.Location = panel4.Location;
            panel9.Size = panel4.Size;
            panel9.Location = panel4.Location;

            accordion.Size = panel1.Size;
            accordion.Location = panel1.Location;

            // panel 1
            sub_expander1.Size = panel4.Size;
            sub_expander1.Padding = new Padding(0);
            sub_expander1.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander1, "Basic Actions (Replace, Delete, Insert)", System.Drawing.ColorTranslator.FromHtml("#F5D0A9"), Color.Black);
            this.Controls.Remove(panel4);
            sub_expander1.Content = panel4;
            sub_accordion.Add(sub_expander1);

            // panel 2
            sub_expander2.Size = panel4.Size;
            sub_expander2.Padding = new Padding(0);
            sub_expander2.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander2, "Dimension-related Actions (Evaluate Dimension Values)", System.Drawing.ColorTranslator.FromHtml("#F7BE81"), Color.Black);
            this.Controls.Remove(panel5);
            sub_expander2.Content = panel5;
            sub_accordion.Add(sub_expander2);

            // panel 3
            sub_expander3.Size = panel4.Size;
            sub_expander3.Padding = new Padding(0);
            sub_expander3.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander3, "Bin Actions (Storing values in Bins)", System.Drawing.ColorTranslator.FromHtml("#FAAC58"), Color.Black);
            this.Controls.Remove(panel6);
            sub_expander3.Content = panel6;
            sub_accordion.Add(sub_expander3);

            // panel 4
            sub_expander4.Size = panel4.Size;
            sub_expander4.Padding = new Padding(0);
            sub_expander4.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander4, "User Input Actions (Prompt Window, Get Input)", System.Drawing.ColorTranslator.FromHtml("#FE9A2E"), Color.Black);
            this.Controls.Remove(panel7);
            sub_expander4.Content = panel7;
            sub_accordion.Add(sub_expander4);

            // panel 5
            sub_expander5.Size = panel4.Size;
            sub_expander5.Padding = new Padding(0);
            sub_expander5.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander5, "Miscellaneous Actions (Execute Translator, File Behaviour, Range Function)", System.Drawing.ColorTranslator.FromHtml("#FF8000"), Color.Black);
            this.Controls.Remove(panel8);
            sub_expander5.Content = panel8;
            sub_accordion.Add(sub_expander5);

            // panel 6
            sub_expander6.Size = panel4.Size;
            sub_expander6.Padding = new Padding(0);
            sub_expander6.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(sub_expander6, "Multi-conditional Actions (Reference Line and Evaluate Other Lines)", System.Drawing.ColorTranslator.FromHtml("#DF7401"), Color.Black);
            this.Controls.Remove(panel9);
            sub_expander6.Content = panel9;
            sub_accordion.Add(sub_expander6);

            #endregion

            groupBox3.Controls.Add(sub_accordion);

            if (_EDIT_MODE)
            {
                Edit_Parse();
                foreach (CheckBox g in check_boxes)
                {
                    g.Enabled = false;
                }
                clear_button.Enabled = false;
            }

            this.Size = new Size(733, 712);
        }

        private void button24_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void look_for_lines_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(range_from_line, 28, true);
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("This function is similar to the MultiRule function except it's more intuitive and quicker");
        }

        private void look_for_action_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void get_input_bin_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(get_input_bin, 29, true);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Get the input from the user and store information in bin #");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Find value in the line and delete everything beyond including the value");
        }

        private void querybin_TextChanged(object sender, EventArgs e)
        {
            Check_Number_Input(querybin, -1, true);
        }


        /*

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }*/


    }
}
