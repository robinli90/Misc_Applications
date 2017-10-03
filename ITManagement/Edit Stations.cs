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
    public partial class Edit_Stations : Form
    {

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();
            base.OnFormClosing(e);
            _parent._Edit_Stations_Open = false;
            _parent.ENABLE_STATION_DRAG = false;
            _parent.ENABLE_STATION_DEL = false;
            _parent.Management_Button_List.ForEach(item => item.Enabled = true);
            _parent._STORE_COMPUTER_INFO();
            _parent.Focus();
            //this.Close();
            this.Dispose();
        }

        Office _parent;

        public Edit_Stations(Office parent)
        {
            InitializeComponent();
            _parent = parent;
            station_type.Items.Add("Small");
            station_type.Items.Add("Medium");
            station_type.Items.Add("Large");
            station_type.Text = "Large";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_parent.ENABLE_STATION_DRAG)
            {
                _parent.ENABLE_STATION_DRAG = true;
                _parent.ENABLE_STATION_DEL = false;
                button1.BackColor = Color.LightBlue;
                remove_station.BackColor = Color.FromKnownColor(KnownColor.Control);
                _parent.Management_Button_List.ForEach(item => item.Enabled = false);
            }
            else
            {
                _parent.ENABLE_STATION_DRAG = false;
                _parent.ENABLE_STATION_DEL = false; ;
                button1.BackColor = Color.FromKnownColor(KnownColor.Control);
                _parent.Management_Button_List.ForEach(item => item.Enabled = true);
            }
            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();
        }

        private void add_station_button_Click(object sender, EventArgs e)
        {
            _parent.ENABLE_STATION_DRAG = false;
            button1.PerformClick();
            _parent._APPEND_TO_SETUP_INFORMATION(4, "▄" + _parent.Button_List.Count() + "~" + "484,407" + "~" + station_type.Text.ToLower(), "▄");
            _parent._MASTER_COMPUTER_LIST.Add(new List<string> { _parent.Button_List.Count().ToString(), "", "", "", "", "", "", "", "" });
            _parent._SEARCH_TOGGLE_LIST.Add("0");
            _parent.Populate_Button_Dictionary();
            _parent.Place_Dynamic_Buttons();
            _parent.ToolTip1.Dispose();
            _parent.Set_Button_Tooltip();
            _parent.Set_Button_Tooltip(true);
            _parent.Paint_Circle(_parent.ref_button_circle, station_type.Text.ToLower());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_parent.ENABLE_STATION_DRAG)
            {
                _parent.ENABLE_STATION_DRAG = false;
                _parent.ENABLE_STATION_DEL = true;
                remove_station.BackColor = Color.LightBlue;
                button1.BackColor = Color.FromKnownColor(KnownColor.Control);
                _parent.Management_Button_List.ForEach(item => item.Enabled = false);
                //_parent.Management_Button_List.ForEach(item => item.Enabled = true);
            }
            else if (!_parent.ENABLE_STATION_DEL)
            {
                _parent.ENABLE_STATION_DRAG = false;
                remove_station.BackColor = Color.LightBlue;
                _parent.ENABLE_STATION_DEL = true;
                _parent.Management_Button_List.ForEach(item => item.Enabled = false);
            }
            else
            {
                _parent.ENABLE_STATION_DRAG = false;
                remove_station.BackColor = Color.FromKnownColor(KnownColor.Control);
                _parent.ENABLE_STATION_DEL = false;
                _parent.Management_Button_List.ForEach(item => item.Enabled = true);
            }

            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();
        }
    }
}
