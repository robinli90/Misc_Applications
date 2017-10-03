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
    public partial class Add_Global_Condition : Form
    {
        Add_Rule _parent;

        public Add_Global_Condition(Add_Rule parent, string condition="", string count="", string value="")
        {
            _parent = parent;
            InitializeComponent();
            condition_box.Items.Add("Exactly");
            condition_box.Items.Add("At least");
            condition_box.Items.Add("At most");

            for (int i = 0; i <= 100; i++)
            {
                count_box.Items.Add(i.ToString());
            }
            count_box.SelectedIndex = 0;

            if (condition.Length > 0) condition_box.Text = condition;
            if (count.Length > 0) count_box.Text = count;
            if (value.Length > 0) value_text.Text = value;
        }

        private void add_condition_Click(object sender, EventArgs e)
        {
            _parent._GLOBAL_CONDITION = condition_box.Text + "|" + count_box.Text + "|" + value_text.Text;
            _parent._SEARCH_VALUE = _parent._GLOBAL_CONDITION;
            this.Close();
            this.Dispose();
        }
    }
}
