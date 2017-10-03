using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Translator
{
    public partial class Add_Loop : Form
    {
        List<CheckBox> check_boxes = new List<CheckBox>();
        Translator _parent;
        List<string> _EDIT_RULE = new List<string>();
        int _EDIT_INDEX = -1;

        public Add_Loop(Translator parent, List<string> Rule = null, int index = -1)
        {
            _parent = parent;
            InitializeComponent();

            Add_Check_Boxes();
            condition_direction.Items.Add("Greater Than"); condition_direction.Items.Add("Less Than"); condition_direction.Items.Add("Equal To"); condition_direction.Text = "Greater Than";

            if (!(Rule == null))
            {
                _EDIT_RULE = Rule;
                _EDIT_INDEX = index;


                if (index >= 0)
                {
                    bin_box.Enabled = false;
                    bin_value_box.Enabled = false;
                    condition_direction.Enabled = false;
                    loop_static_box.Enabled = false;
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    Add_Loop_Button.Text = "Save changes";
                    if (_EDIT_RULE[3].Length > 0)
                    {
                        bin_box.Enabled = true;
                        bin_value_box.Enabled = true;
                        condition_direction.Enabled = true;
                        checkBox1.Checked = true;
                        bin_value_box.Text = _EDIT_RULE[2];
                        condition_direction.Text = _EDIT_RULE[3];
                        bin_box.Text = _EDIT_RULE[4];
                    }
                    else
                    {
                        loop_static_box.Enabled = true;
                        checkBox2.Checked = true;
                        loop_static_box.Text = _EDIT_RULE[2];
                    }
                }


                // Setup condition_direction combobox

                if (_EDIT_RULE[3].Length > 0)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox2.Checked = true;
                }
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void Add_Check_Boxes()
        {
            // ORDER MATTERS IN WHICH THEY ARE ADDED (BUTTON LIST IS NOT IN ORDER AS THEY APPEAR)
            check_boxes.Add(checkBox1);  
            check_boxes.Add(checkBox2); 
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { if (checkBox1.Checked) Check_Box_Validity(checkBox1); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { if (checkBox2.Checked) Check_Box_Validity(checkBox2); }


        private void Check_Box_Validity(CheckBox target_box)
        {
            foreach (CheckBox b in check_boxes)
            {
                if (!(b == target_box))
                {
                    b.Checked = false;
                }
            }
        }


        public void Check_Number_Input(TextBox text, int Check_Boxes_Index = -1) // delete insert check excludes placement of DECIMALS
        {
            if (text.Text.Length > 0)
            {
                // check_boxes_index < 0 means outside call
                if (!(text.Text.Contains("%")) && !char.IsDigit(text.Text[text.Text.Length - 1]))// && !(text.Text[text.Text.Length - 1].ToString() == ".") || text.Text[0].ToString() == ".")
                {
                    // If letter in SO_number box, do not output and move CARET to end
                    text.Text = text.Text.Substring(0, text.Text.Length - 1);
                    text.SelectionStart = text.Text.Length;
                    text.SelectionLength = 0;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            Check_Number_Input(loop_static_box, 2);
        }

        private void condition_direction_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void condition_value_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            Check_Number_Input(loop_static_box, 2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            Check_Number_Input(loop_static_box, 2);
        }

        // Add loop button
        private void button20_Click(object sender, EventArgs e)
        {
            string CHECK_VALUE = checkBox1.Checked ? bin_value_box.Text : loop_static_box.Text;
            string COMP = checkBox1.Checked ? condition_direction.Text : "";
            string BIN_NO = checkBox1.Checked ? bin_box.Text : "";

            if (_EDIT_INDEX >= 0) // If EDIT
            {

                if ((checkBox1.Checked && bin_box.Text.Length > 0 && bin_value_box.Text.Length > 0) || (checkBox2.Checked && loop_static_box.Text.Length > 0))
                {
                    _parent.Add_Rule("LOOP_CONDITION_START", _EDIT_RULE[1], CHECK_VALUE, COMP, BIN_NO, "", _EDIT_INDEX);
                    _parent.delete_rule(_EDIT_INDEX + 1);
                    _parent.Set_Rule_Selection(_EDIT_INDEX);
                    this.Close();
                    this.Dispose();
                }
            }
            else
            {
                Random rnd = new Random();
                string ID_Generated = rnd.Next(1000000, 9999999).ToString();


                //_parent.Add_Rule(ACTION, SEARCH_VALUE, DIMENSION, VALUE, OPTIONAL_VALUE, COMMENT, index);
                //                   ACTION           LOOP_ID   CHECK_VALUE  COMP      BIN#          ""       ""

                if ((checkBox1.Checked && bin_box.Text.Length > 0 && bin_value_box.Text.Length > 0) || (checkBox2.Checked && loop_static_box.Text.Length > 0))
                {
                    _parent.Add_Rule("LOOP_CONDITION_START", ID_Generated, CHECK_VALUE, COMP, BIN_NO, "");
                    _parent.Add_Rule("LOOP_CONDITION_END", ID_Generated, "", "", "", "");
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Error in rule format");
                }
            }
        }
    }
}
