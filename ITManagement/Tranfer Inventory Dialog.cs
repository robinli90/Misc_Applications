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
    public partial class Tranfer_Inventory_Dialog : Form
    {


        // Override original form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            for (int i = 0; i < _parent._COMPUTER_COUNT; i++)
            {
                _parent._SEARCH_TOGGLE_LIST[i] = "0";
            }
            _parent.Set_Button_Tooltip(true);
            _parent._Tranfer_Index = -1;
            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();
            _parent._Pending_Tranfer = false;
            _parent._STORE_COMPUTER_INFO();
            _parent._RETRIEVE_COMPUTER_INFO();
            _parent_inv.Visible = true;
            _parent_inv.Focus();
            //this.Close();
            this.Dispose();


        }

        public static Office _parent;
        public Inventory _parent_inv;
        private string desc = "";
        private int index = -1;
        private string emp_num = "";

        public Tranfer_Inventory_Dialog(Office g, Inventory _parent_inv2, int _parent_index, string description, string employee_number)
        {
            InitializeComponent();
            _parent_inv = _parent_inv2;
            _parent = g;
            desc = description;
            index = _parent_index;
            emp_num = employee_number;
        }

        private void transfer_button_Click(object sender, EventArgs e)
        {
            string temp = _parent.Add_Hardware("`I`" + desc + (note_box.Text.Length > 0 ? " - " : "") + note_box.Text + " (DateStamp:" + DateTime.Now.ToString() + ")", _parent._Tranfer_Index, 2);
            if (_parent.USER_INFO_BOX_OPEN)
            {
                MessageBox.Show("Please close all user information dialog before trying to transfer inventory");
            }
            else if (temp == "null")
            {
                MessageBox.Show("Please select a station to transfer item.");
            }
            else
            {

                string[] columns = _parent_inv.HARDWARE[index].Split(new string[] { "~" }, StringSplitOptions.None);


                _parent_inv.HARDWARE[index] = columns[0] + "~" + (Convert.ToInt32(columns[1]) - 1).ToString() + "~" + columns[2];
                _parent._Pending_Tranfer = false;
                for (int i = 0; i < _parent._COMPUTER_COUNT; i++)
                {
                    _parent._SEARCH_TOGGLE_LIST[i] = "0";
                }

                // Add to log information
                _parent._APPEND_TO_SETUP_INFORMATION(2, "Inventory '" + desc + (note_box.Text.Length > 0 ? " - " : "") + note_box.Text + "' transfer to '" + temp + "' (executed by: " + _parent._EMPLOYEE_LIST[emp_num] + ")");

                _parent.Set_Button_Tooltip(true);
                _parent._Tranfer_Index = -1;
                _parent.PAINT_X = 0;
                _parent.PAINT_Y = 0;
                _parent.Invalidate();
                _parent_inv._Populate_Inventory_List();
                _parent_inv.Store_Update();
                _parent._STORE_COMPUTER_INFO();
                _parent_inv.Visible = true;
                _parent_inv.Focus();
                this.Close();

                if (Convert.ToInt32(columns[1]) == 2)
                {
                    MessageBox.Show("WARNING: You only have one more remaining '" + columns[0] + "' left!");
                }

            }
        }

        private void Tranfer_Inventory_Dialog_Load(object sender, EventArgs e)
        {
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }
    }
}
