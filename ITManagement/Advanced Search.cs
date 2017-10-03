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
    public partial class Advanced_Search : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            for (int i = 0; i < _MASTER_COMPUTER_LIST.Count; i++)
            {
                _parent._SEARCH_TOGGLE_LIST[i] = "0";
            }
            _parent.ADVANCED_SEARCH_BOX_OPEN = false;
            _parent.Set_Button_Tooltip(true);
            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();
            //this.Close();
            this.Dispose();
        }

        string INVENTORY_INDEX = "";

        internal Office _parent;// = new Office();
        internal List<List<string>> _MASTER_COMPUTER_LIST = new List<List<string>>();
        internal List<int> _SEARCH_BUTTON_ARRAY = new List<int>(); //string is button#
        public int _SEARCH_COUNT = 0;

        private List<string> Date_Range_List = new List<string>();

        private List<CheckBox> check_boxes = new List<CheckBox>();


        public Advanced_Search(Office parent, List<List<string>> _parent_MASTER_COMPUTER_LIST)
        {
            InitializeComponent();
            _parent = parent;
            _MASTER_COMPUTER_LIST = _parent_MASTER_COMPUTER_LIST;
            INVENTORY_INDEX = _parent.Get_Inventory_Index().ToString();

            _POPULATE_CHECK_BOXES();

            #region Search Grid View Setup
            for (int i = 0; i < search_grid.ColumnCount; i++) search_grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            search_grid.SelectionChanged += new EventHandler(search_grid_SelectionChanged);
            search_grid.DefaultCellStyle.SelectionBackColor = search_grid.DefaultCellStyle.BackColor;
            search_grid.DefaultCellStyle.SelectionForeColor = search_grid.DefaultCellStyle.ForeColor;
            this.search_grid.CellClick += new DataGridViewCellEventHandler(search_grid_CellContentClick);

            DataGridViewButtonColumn c = (DataGridViewButtonColumn)search_grid.Columns[0];
            c.FlatStyle = FlatStyle.Flat;
            c.DefaultCellStyle.ForeColor = Color.Black;
            #endregion

            search_by_date.Text = DateTime.Now.ToShortDateString();

            // Populate drop-down list box
            deparment.Items.Add("CAD");
            deparment.Items.Add("CAM");
            deparment.Items.Add("IT");
            deparment.Items.Add("Management");
            deparment.Items.Add("Other");
            deparment.Items.Add("Printer");
            deparment.Items.Add("Sales");
            deparment.Items.Add("Server");
            deparment.Items.Add("SHOP-MACHINE");
            deparment.Items.Add("SHOP-PC");
            deparment.Text = "CAD";

        }

        private void _POPULATE_GRID_VIEW()
        {

            search_grid.Rows.Clear();
            search_grid.Refresh();

            List<string> HARDWARE = new List<string>();
            List<string> SOFTWARE = new List<string>();
            List<string> CHANGES = new List<string>();
            string info = "";

            int row_index = 0;
            for (int i = 0; i < _MASTER_COMPUTER_LIST.Count; i++)
            {
                if (_parent._SEARCH_TOGGLE_LIST[i] == "1")
                {
                    HARDWARE = _MASTER_COMPUTER_LIST[i][6].Trim(Convert.ToChar("▄")).Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
                    SOFTWARE = _MASTER_COMPUTER_LIST[i][7].Trim(Convert.ToChar("▄")).Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
                    CHANGES = _MASTER_COMPUTER_LIST[i][8].Trim(Convert.ToChar("▄")).Split(new string[] { "▄" }, StringSplitOptions.None).ToList();

                    if (HARDWARE[0].Length > 1 && show_hw.Checked)
                    {
                        info = info + "HARDWARE:";
                        string hardware = "";
                        foreach (string hardware2 in HARDWARE)
                            if (hardware2.Length > 0)
                            {
                                hardware = hardware2.StartsWith("`I`") ? hardware2.Substring(3) : hardware2;
                                info = info + " \"" + (hardware.Contains("DateStamp") ? hardware.Substring(0, hardware.LastIndexOf("DateStamp") - 2) : hardware) + "\",";
                            }
                        info = info.Trim((",").ToCharArray()).TrimStart((" ").ToCharArray()) + Environment.NewLine;
                    }

                    if (SOFTWARE[0].Length > 1 && show_sw.Checked)
                    {
                        info = info + "SOFTWARE:";
                        foreach (string software in SOFTWARE)
                            if (software.Length > 0)
                                info = info + " \"" + (software.Contains("DateStamp") ? software.Substring(0, software.LastIndexOf("DateStamp") - 2) : software) + "\",";
                        info = info.Trim((",").ToCharArray()).TrimStart((" ").ToCharArray()) + Environment.NewLine;
                    }

                    if (CHANGES[0].Length > 1 && show_info.Checked)
                    {
                        info = info + "INFORMATION:";
                        foreach (string changes in CHANGES)
                            if (changes.Length > 0)
                                info = info + " \"" + (changes.Contains("DateStamp") ? changes.Substring(0, changes.LastIndexOf("DateStamp") - 2) : changes) + "\",";
                        info = info.Trim((",").ToCharArray()).TrimStart((" ").ToCharArray()) + Environment.NewLine;
                    }
                    //info = info + Environment.NewLine;


                    search_grid.Rows.Add(
                                        _MASTER_COMPUTER_LIST[i][2],
                                        _MASTER_COMPUTER_LIST[i][1],
                                        _MASTER_COMPUTER_LIST[i][3],
                                        _MASTER_COMPUTER_LIST[i][4],
                                        info.TrimEnd(Environment.NewLine.ToCharArray())
                                        );
                    Set_Row_Color(row_index, _MASTER_COMPUTER_LIST[i][1]);
                    info = "";
                    row_index++;

                }
            }
        }

        private void _POPULATE_CHECK_BOXES()
        {
            check_boxes.Add(view_all_box);
            check_boxes.Add(basic_search);
            check_boxes.Add(search_by_single_date);
            check_boxes.Add(search_date_range);
            check_boxes.Add(search_by_dept);
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            _parent._SEARCH_COUNT = 0;
            _SEARCH_BUTTON_ARRAY = new List<int>(); //string is button#
            search_count_box.Visible = true;
            _SEARCH_COUNT = 0;

            for (int i = 0; i < _MASTER_COMPUTER_LIST.Count; i++)
            {
                _parent._SEARCH_TOGGLE_LIST[i] = "0";
            }
            if (basic_search.Checked)
            {
                if (search_box.Text.Length > 0)
                {
                    int index = 0;
                    foreach (List<string> g in _MASTER_COMPUTER_LIST)
                    {
                        if (!(g[0] == INVENTORY_INDEX))
                        {
                            bool included = false;
                            foreach (string info in g.GetRange(1, g.Count - 1)) //ignore button number value in g[0]
                            {
                                if (info.ToLower().Contains(search_box.Text.ToLower()))
                                {
                                    if (!included)
                                    {
                                        _parent._SEARCH_TOGGLE_LIST[index] = "1";
                                        _SEARCH_BUTTON_ARRAY.Add(Convert.ToInt32(g[0]));
                                        included = true;
                                        _SEARCH_COUNT++;
                                    }
                                }
                            }
                            index++;
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }
            else if (view_all_box.Checked)
            {
                for (int i = 0; i < _parent._SEARCH_TOGGLE_LIST.Count; i++)
                {
                    if (!(_MASTER_COMPUTER_LIST[i][0] == INVENTORY_INDEX))
                    {
                        _SEARCH_BUTTON_ARRAY.Add(i);
                        _parent._SEARCH_TOGGLE_LIST[i] = "1";
                        _SEARCH_COUNT++;
                    }
                }
            }
            else if (search_by_single_date.Checked)
            {
                int index = 0;
                foreach (List<string> g in _MASTER_COMPUTER_LIST)
                {
                    if (!(g[0] == INVENTORY_INDEX))
                    {
                        bool included = false;
                        foreach (string info in g.GetRange(1, g.Count - 1))
                        {
                            if (info.ToLower().Contains(search_by_date.Text.ToLower()) && info.ToLower().Contains(search_within.Text.ToLower()))
                            {
                                if (!included)
                                {
                                    _parent._SEARCH_TOGGLE_LIST[index] = "1";
                                    _SEARCH_BUTTON_ARRAY.Add(Convert.ToInt32(g[0]));
                                    included = true;
                                    _SEARCH_COUNT++;
                                }
                            }
                        }
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if (search_date_range.Checked)
            {
                int index = 0;
                foreach (List<string> g in _MASTER_COMPUTER_LIST)
                {
                    if (!(g[0] == INVENTORY_INDEX))
                    {
                        bool included = false;
                        foreach (string info in g.GetRange(1, g.Count - 1))
                        {
                            foreach (string date in Date_Range_List)
                            {
                                if (info.ToLower().Contains(date.ToLower()) && info.ToLower().Contains(search_within.Text.ToLower()))
                                {
                                    if (!included)
                                    {
                                        _parent._SEARCH_TOGGLE_LIST[index] = "1";
                                        _SEARCH_BUTTON_ARRAY.Add(Convert.ToInt32(g[0]));
                                        included = true;
                                        _SEARCH_COUNT++;
                                    }
                                }
                            }
                        }
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if (search_by_dept.Checked)
            {
                int index = 0;
                foreach (List<string> g in _MASTER_COMPUTER_LIST)
                {
                    if (!(g[0] == INVENTORY_INDEX))
                    {
                        if (g[1].ToLower() == deparment.Text.ToLower() && (g[6].ToLower().Contains(search_within.Text.ToLower()) || g[2].ToLower().Contains(search_within.Text.ToLower())))
                        {
                            _parent._SEARCH_TOGGLE_LIST[index] = "1";
                            _SEARCH_BUTTON_ARRAY.Add(Convert.ToInt32(g[0]));
                            _SEARCH_COUNT++;
                        }
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            _parent.Set_Button_Tooltip(true);
            _POPULATE_GRID_VIEW();
            search_count_box.Text = (search_box.Text.Length > 0 || view_all_box.Checked || true) ? _SEARCH_COUNT.ToString() + " item(s) found" : "";
            search_count_box.ForeColor = _SEARCH_COUNT > 0 ? Color.Green : Color.Red;

        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            search_button.PerformClick();
        }

        private void search_by_date_ValueChanged(object sender, EventArgs e)
        {
            search_button.PerformClick();
        }

        // Cell buttons
        private void search_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _parent._RESET_INACTIVITY();
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //MessageBox.Show(e.RowIndex.ToString());//TODO - Button Clicked - Execute Code Here
                //MessageBox.Show(][2]);//TODO - Button Clicked - Execute Code Here
                //_MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]]
                //Button btn = (Button)sender;
                _parent.Paint_Circle(_parent.Get_Button(Convert.ToInt32(_MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]][0])));
                if (!_parent.USER_INFO_BOX_OPEN)
                {
                    User_Info G = new User_Info(_parent, _MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]], Convert.ToInt32(_MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]][0]), "Hardware", (_parent._MASTER_LOGIN_EMPLOYEE_NUMBER == "10577" || _parent._MASTER_LOGIN_EMPLOYEE_NUMBER == "10403"));
                    G.ShowDialog();
                }
            }
        }

        private void Check_Box_Validity(CheckBox target_box)
        {

            _parent.PAINT_X = 0;
            _parent.PAINT_Y = 0;
            _parent.Invalidate();

            search_by_date.Enabled = false;
            search_box.Enabled = false;
            from_date.Enabled = false;
            to_date.Enabled = false;
            deparment.Enabled = false;

            int check_box_number = check_boxes.IndexOf(target_box);

            foreach (CheckBox g in check_boxes)
            {
                if (!(check_boxes.IndexOf(g) == check_box_number))
                {
                    g.Checked = false;
                }
            }
        }


        private void view_all_box_CheckedChanged(object sender, EventArgs e) 
        {
            if (view_all_box.Checked) { Check_Box_Validity(view_all_box); label2.Visible = false; search_within.Visible = false; search_button.PerformClick(); }
        }
        private void basic_search_CheckedChanged(object sender, EventArgs e) 
        {
            if (basic_search.Checked) { Check_Box_Validity(basic_search); search_box.Enabled = true; label2.Visible = false; search_within.Visible = false; search_button.PerformClick(); } 
        }

        private void search_by_single_CheckedChanged(object sender, EventArgs e)
        {
            if (search_by_single_date.Checked) { Check_Box_Validity(search_by_single_date); search_by_date.Enabled = true; label2.Visible = true; search_within.Visible = true; search_button.PerformClick(); }
        }

        private void search_by_dept_CheckedChanged(object sender, EventArgs e)
        {
            if (search_by_dept.Checked) { Check_Box_Validity(search_by_dept); deparment.Enabled = true; label2.Visible = true; search_within.Visible = true; search_button.PerformClick(); }
        }

        private void search_date_range_CheckedChanged(object sender, EventArgs e)
        {
            if (search_date_range.Checked)
            {
                label2.Visible = true; search_within.Visible = true; 
                Check_Box_Validity(search_date_range);
                from_date.Enabled = true; to_date.Enabled = true;
                Date_Range_List.Add(DateTime.Now.ToShortDateString());
                search_button.PerformClick();
            }
        }

        private void from_date_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(from_date.Text) > Convert.ToDateTime(to_date.Text))
            {
                from_date.Text = to_date.Text;
                MessageBox.Show("Invalid date selection");
            }
            else
            {
                Date_Range_List = new List<string>();
                DateTime ref_date = Convert.ToDateTime(from_date.Text);
                while (ref_date <= Convert.ToDateTime(to_date.Text))
                {
                    Date_Range_List.Add(ref_date.ToShortDateString());
                    ref_date =  ref_date.AddDays(1);
                }
            }
            search_button.PerformClick();
        }

        private void to_date_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(from_date.Text) > Convert.ToDateTime(to_date.Text))
            {
                to_date.Text = from_date.Text;
                MessageBox.Show("Invalid date selection");
            }
            else
            {
                Date_Range_List = new List<string>();
                DateTime ref_date = Convert.ToDateTime(from_date.Text);
                while (ref_date <= Convert.ToDateTime(to_date.Text))
                {
                    Date_Range_List.Add(ref_date.ToShortDateString());
                    ref_date = ref_date.AddDays(1);
                }
            }
            search_button.PerformClick();
        }


        /*
        private void view_all_box_CheckedChanged(object sender, EventArgs e) 
        { 
            if (view_all_box.Checked) Check_Box_Validity(view_all_box); 
        }
        private void basic_search_CheckedChanged(object sender, EventArgs e) 
        { 
            if (basic_search.Checked) Check_Box_Validity(basic_search); 
        }*/

        private void Set_Row_Color(int row_index, string dept)
        {
            Color color = Color.White;
            if (dept == "SHOP-MACHINE")
                color = Color.LightGray;
            else if (dept == "CAD")
                color = Color.LightGreen;
            else if (dept == "CAM")
                color = Color.LightYellow;
            else if (dept == "Sales")
                color = Color.LightPink;
            else if (dept == "Management")
                color = Color.LightCyan;
            else if (dept == "SHOP-PC")
                color = Color.LightSlateGray;
            else if (dept == "IT")
                color = Color.Orange;
            else if (dept == "Printer")
                color = Color.Cyan;
            else if (dept == "Server")
                //color = Color.MediumSpringGreen;
                color = System.Drawing.ColorTranslator.FromHtml("#BCA9F5");

            search_grid.Rows[row_index].Cells[0].Style.BackColor = color;
            search_grid.Rows[row_index].Cells[1].Style.BackColor = color;
            search_grid.Rows[row_index].Cells[2].Style.BackColor = color;
            search_grid.Rows[row_index].Cells[3].Style.BackColor = color;
            search_grid.Rows[row_index].Cells[4].Style.BackColor = color;
            //search_grid.Rows[row_index].Cells[5].Style.BackColor = color;
        }


        private void search_grid_SelectionChanged(object sender, EventArgs e)
        {
            this.search_grid.ClearSelection();
            _parent._RESET_INACTIVITY();
        }

        private bool Check_Checked()
        {
            bool checke = false;
            foreach (CheckBox g in check_boxes)
            {
                if (g.Checked)
                    checke = true;
            }
            return checke;
        }

        private void show_info_CheckedChanged(object sender, EventArgs e) { if (Check_Checked()) _POPULATE_GRID_VIEW(); }
        private void show_sw_CheckedChanged(object sender, EventArgs e) { if (Check_Checked()) _POPULATE_GRID_VIEW(); }
        private void show_hw_CheckedChanged(object sender, EventArgs e) { if (Check_Checked())  _POPULATE_GRID_VIEW(); }
        private void deparment_SelectedIndexChanged_1(object sender, EventArgs e) { search_by_dept.Checked = true;  search_button.PerformClick(); }

        private void search_grid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Advanced_Search_Load(object sender, EventArgs e)
        {
            view_all_box.Checked = true;
            label2.Visible = false; search_within.Visible = false; 
        }

        private void search_within_TextChanged(object sender, EventArgs e)
        {
            search_button.PerformClick();
        }
    }

}
