using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITManagement
{
    public partial class AddReminder : Form
    {
        Office _parent;

        public AddReminder(Office parent)
        {
            InitializeComponent();
            _parent = parent;
            remind_who.Items.Add("Only me");
            remind_who.Items.Add("Everyone");
            remind_who.Text = "Only me";
        }

        private void add_reminder_button_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Text.Length > 0 && remind_who.Text.Length > 0 && description_box.Text.Length > 0 && !description_box.Text.Contains(Convert.ToChar("~")))
            {
                _parent._APPEND_TO_SETUP_INFORMATION(5, (_parent._MASTER_SETUP_LIST[5].Length > 5 ? "▄" : "") +
                        Convert.ToDateTime(dateTimePicker1.Text).ToString("MM/dd/yyyy") + "~" +
                        description_box.Text + "~" +
                        (remind_who.Text == "Only me" ? _parent._MASTER_LOGIN_EMPLOYEE_NUMBER + "x" : "99999x")
                    );
                this.Close();
            }
            else if (description_box.Text.Contains(Convert.ToChar("~")))
            {
                MessageBox.Show("Error: Invalid character detected '~'");
            }
            else if (dateTimePicker1.Text.Length > 0)
            {
                MessageBox.Show("Error: Missing date");
            }
            else if (description_box.Text.Length > 0)
            {
                MessageBox.Show("Error: Missing description");
            }
            else if (remind_who.Text.Length > 0)
            {
                MessageBox.Show("Error: Missing people/person to remind");
            }
        }
    }
}
