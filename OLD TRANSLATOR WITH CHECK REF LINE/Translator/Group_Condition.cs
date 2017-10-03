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
    public partial class Group_Condition : Form
    {
        Translator _parent;
        public Group_Condition(Translator parent, string condition_string)
        {
            InitializeComponent();
            _parent = parent;
            condition_action.Items.Add("Greater Than");
            condition_action.Items.Add("Less Than");
            condition_action.Items.Add("Equal To");

            if (condition_string.Length > 0)
            {
                try
                {
                    string[] temp = condition_string.Split(new string[] { "~" }, StringSplitOptions.None);
                    condition_bin.Text = temp[0];
                    condition_value.Text = temp[2];
                    condition_action.Text = temp[1];
                    button2.Visible = true;
                }
                catch
                {
                }
            }
            else
            {
                condition_action.Text = "Greater Than";
            }
        }

        private void Group_Condition_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (condition_bin.Text.Length > 0 && condition_value.Text.Length > 0 && condition_action.Text.Length > 0)
            {
                _parent.Group_Condition_String = condition_bin.Text + "~" + condition_action.Text + "~" + condition_value.Text;
                _parent.Set_Group_Condtion();
                this.Close();
            }
            else
            {
                MessageBox.Show("Make sure all fields are filled");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _parent.Group_Condition_String = "";
            _parent.Set_Group_Condtion();
            _parent.button27.Text = "Add Group Condition";
            this.Close();
        }
    }
}
