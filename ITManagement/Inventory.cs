using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databases;
using System.Data.Odbc;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using System.IO;

namespace ITManagement
{
    public partial class Inventory : Form
    {

        // Override original form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            for (int i = 0; i < _parent._SEARCH_TOGGLE_LIST.Count(); i++)
            {
                _parent._SEARCH_TOGGLE_LIST[i] = "0";
            }
            _parent.Set_Button_Tooltip(true);
            Update_Info_List();
            _parent._Inventory_Open = false;
            //_parent._MODIFY_RULE(Inventory_List, _SELECTED_INDEX);
            _parent._STORE_COMPUTER_INFO();
            _parent.Focus();
            _parent._RETRIEVE_COMPUTER_INFO();
            //this.Close();
            this.Dispose();

        }

        public bool overwrite_tracking_info = false;
        private int print_index = 1;
        //private List<string> Inventory_List = new List<string>();
        Office _parent;// = new Office();
        
        private int _EDIT_INDEX = -1;
        public string _CHECKOUT_NOTES = "";
        private string _EMP_NUM = "";

        public List<string> HARDWARE = new List<string>();
        public List<string> SHOPPING_HARDWARE = new List<string>();

        Dictionary<string, List<string>> Delivery_Track_Dict = new Dictionary<string, List<string>>();

        public Inventory(Office _office, string _inv, string employeenumber = "99999")
        {
            InitializeComponent();
            _parent = _office;
            //Inventory_List = _inv;
            //_SELECTED_INDEX = index;
            _EMP_NUM = employeenumber;

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            inventory_grid.SelectionChanged += new EventHandler(inventory_grid_SelectionChanged);
            shopping_grid.SelectionChanged += new EventHandler(shopping_grid_SelectionChanged);
            inventory_grid.DefaultCellStyle.SelectionBackColor = inventory_grid.DefaultCellStyle.BackColor;
            inventory_grid.DefaultCellStyle.SelectionForeColor = inventory_grid.DefaultCellStyle.ForeColor;
            this.inventory_grid.CellClick += new DataGridViewCellEventHandler(inventory_grid_CellContentClick);
            this.shopping_grid.CellClick += new DataGridViewCellEventHandler(shopping_grid_CellContentClick);

            for (int i = 0; i < inventory_grid.ColumnCount; i++)
            {
                inventory_grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i >= 1)
                    inventory_grid.Columns[i].ReadOnly = true;
            }

            for (int i = 0; i < shopping_grid.ColumnCount; i++)
            {
                shopping_grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i >= 1)
                    shopping_grid.Columns[i].ReadOnly = true;
            }

            HARDWARE = _inv.Split(new string[] { "▄" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < HARDWARE.Count; i++)
            {
                if (HARDWARE[i].Contains("`I`"))
                {
                    HARDWARE[i] = HARDWARE[i].Substring(3);
                }
            }

            if (HARDWARE[0] == "") HARDWARE = new List<string>();
            // Remove all empty entries
            for (int i = HARDWARE.Count - 1; i >= 0; i--)
            {
                if (HARDWARE[i].Length == 0)
                {
                    HARDWARE.RemoveAt(i);
                    i--;
                }
            }

            _Populate_Inventory_List();

            // Enable certain buttons for admin mode
            if (_parent._ENABLE_ADMINISTRATIVE_COMMANDS)
            {
                delete_item.Visible = true;
                move_to_inventory.Visible = true;
                add_inv_button.Visible = true;
                update_item.Visible = true;
                
            }

            DataGridViewButtonColumn c = (DataGridViewButtonColumn)shopping_grid.Columns[3];
            c.FlatStyle = FlatStyle.Flat;
            c.DefaultCellStyle.ForeColor = Color.Gray;
            c.ToolTipText = "";


            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.InitialDelay = 1;
            ToolTip1.ReshowDelay = 1;
            ToolTip1.SetToolTip(notes_button, "Add a note");

        }

        private void Sort_Hardware_Quantity()
        {
            try
            {
                List<string> SortedList = HARDWARE.OrderByDescending(o => Convert.ToInt32(o.Split(new string[] { "~" }, StringSplitOptions.None)[1])).ToList();
                HARDWARE = SortedList;
            }
            catch
            {
            }
        }

        private void Aggregate_Shopping_Cart()
        {
            for (int i = 0; i < HARDWARE.Count(); i++)
            {
                for (int j = HARDWARE.Count() - 1; j >= 0; j--)
                {
                    try
                    {
                        int q1 = Convert.ToInt32(HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None)[1]);
                        int q2 = Convert.ToInt32(HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None)[1]);
                        string[] w1 = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                        string[] w2 = HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None);
                        if (q1 < 0 && q2 < 0 && !(i == j) && w1[0] == w2[0] && w1.Count() == w2.Count())
                        {
                            HARDWARE[i] = w1[0] + "~" + (q2 + q1).ToString() + "~" + w1[2];
                            HARDWARE.RemoveAt(j);
                        }
                    }
                    catch
                    {
                        //cannot aggregate
                    }
                }
            }
        }

        private void Update_Info_List()
        {

            string return_string = "";
            foreach (string g in HARDWARE)
            {
                return_string = return_string + g + "▄";
            }
            _parent._STORE_SETUP_INFORMATION(1, return_string.Trim(Convert.ToChar("▄")));
        }

        public void _Populate_Inventory_List()
        {
            Sort_Hardware_Quantity();
            _parent._RESET_INACTIVITY();
            inventory_grid.Rows.Clear();
            int index = 0;
            foreach (string item in HARDWARE)
            {
                try
                {
                    string[] columns = item.Split(new string[] { "~" }, StringSplitOptions.None);
                    if (display_zero.Checked || Convert.ToInt32(columns[1]) > 0)
                    {
                        if (Convert.ToInt32(columns[1]) >= 0)
                        //if (true)
                        {
                            inventory_grid.Rows.Add(
                            false, //blank checkbox
                                                columns[0],
                                                columns[1],
                                                columns[2]
                                                );

                            if (Convert.ToInt32(columns[1]) == 1)
                            {
                                inventory_grid.Rows[index].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                                inventory_grid.Rows[index].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                                inventory_grid.Rows[index].Cells[2].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0E0");
                                inventory_grid.Rows[index].Cells[3].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                            }
                            else if (Convert.ToInt32(columns[1]) == 0)
                            {
                                inventory_grid.Rows[index].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                                inventory_grid.Rows[index].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                                inventory_grid.Rows[index].Cells[2].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF5050");
                                inventory_grid.Rows[index].Cells[3].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                            }
                            index++;
                        }
                    }
                    else if (display_zero.Checked && Convert.ToInt32(columns[1]) >= 0)
                    {
                        inventory_grid.Rows.Add(
                        false, //blank checkbox
                                            columns[0],
                                            columns[1],
                                            columns[2]
                                            );

                        if (Convert.ToInt32(columns[1]) == 1)
                        {
                            inventory_grid.Rows[index].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                            inventory_grid.Rows[index].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                            inventory_grid.Rows[index].Cells[2].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0E0");
                            inventory_grid.Rows[index].Cells[3].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8E0E0");
                        }
                        else if (Convert.ToInt32(columns[1]) == 0)
                        {
                            inventory_grid.Rows[index].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                            inventory_grid.Rows[index].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                            inventory_grid.Rows[index].Cells[2].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF5050");
                            inventory_grid.Rows[index].Cells[3].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6C6C6");
                        }
                        index++;
                    }

                }
                catch
                {
                    // Parsing non-existant item error
                }
            }

            Update_Info_List();
            //_parent._MODIFY_RULE(Inventory_List, _SELECTED_INDEX);
            //_parent._STORE_COMPUTER_INFO();
            //_parent._RETRIEVE_COMPUTER_INFO();
        }

        private void _Populate_Shopping_List()
        {
            _parent._RESET_INACTIVITY();
            shopping_grid.Rows.Clear();
            Aggregate_Shopping_Cart();
            Sort_Hardware_Quantity();
            int index2 = 0;
            foreach (string item in HARDWARE)
            {
                try
                {
                    string[] columns = item.Split(new string[] { "~" }, StringSplitOptions.None);
                    if (Convert.ToInt32(columns[1]) < 0) // if its part of shopping list
                    {
                        string temp = "";
                        if (columns.Count() > 3)
                        {
                            try
                            {
                                temp = columns[3];
                            }
                            catch
                            {
                            }
                        }
                        shopping_grid.Rows.Add(
                            false,
                            columns[0].Trim() + (temp.Length > 0 ? " (Ordered on " + temp.Substring(3) + ")" : ""),
                            columns[1].Substring(1),
                            "➫"
                            );
                        if (temp.Length > 0)
                        {
                            try
                            {
                                if (columns[3].Contains("`P`"))
                                {
                                    try
                                    {
                                        DateTime order_date = Convert.ToDateTime(columns[3].Substring(3));

                                        Color warning_color = Color.Empty;
                                        if (DateTime.Now.AddDays(-3) < order_date)
                                        {
                                            warning_color = System.Drawing.ColorTranslator.FromHtml("#A8E0E0");
                                        }
                                        else if (DateTime.Now.AddDays(-7) <= order_date)
                                        {
                                            warning_color = Color.LightPink;
                                        }
                                        else if (DateTime.Now.AddDays(-14) <= order_date)
                                        {
                                            warning_color = System.Drawing.ColorTranslator.FromHtml("#ff6666");
                                        }
                                        else
                                        {
                                            warning_color = Color.Red;
                                        }
                                        
                                        shopping_grid.Rows[index2].Cells[0].Style.BackColor = warning_color;
                                        shopping_grid.Rows[index2].Cells[1].Style.BackColor = warning_color;
                                        shopping_grid.Rows[index2].Cells[2].Style.BackColor = warning_color;
                                        string tool_tip_string = "";
                                        if (columns.Count() >= 5)
                                        {
                                            string[] temp3 = columns[4].Split(new string[] { ",`" }, StringSplitOptions.None);
                                            tool_tip_string = temp3[2] + " (" + temp3[1] + ")";
                                        }
                                        shopping_grid.Rows[index2].Cells[3].ToolTipText = tool_tip_string;
                                        //shopping_grid.Rows[index2].Cells[3].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#A8E0E0");
                                    }
                                    catch
                                    {
                                        shopping_grid.Rows[index2].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#A8E0E0");
                                        shopping_grid.Rows[index2].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#A8E0E0");
                                        shopping_grid.Rows[index2].Cells[2].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#AFE0E0");
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            var dataGridViewCellStyle2 = new DataGridViewCellStyle { Padding = new Padding(1000, 0, 0, 0) };
                            shopping_grid.Rows[index2].Cells[3].Style = dataGridViewCellStyle2;
                            shopping_grid.Rows[index2].Cells[3].ToolTipText = "";
                        }
                        index2++;
                    }
                }
                catch
                {
                }
            }
            total_count.Text = shopping_grid.Rows.Count > 0 ? shopping_grid.Rows.Count + " item(s)" : "";
        }

        // Add to inventory
        private void inventory_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _parent._RESET_INACTIVITY();
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                for (int i = 0; i < inventory_grid.Rows.Count; i++)
                {
                    inventory_grid[0, i].Value = false;
                }
                inventory_grid[0, e.RowIndex].Value = true;
                _EDIT_INDEX = -1;
                add_inv_button.Text = "Add to Inventory";
                item_desc.Text = "";
                item_quantity.Text = "";
                _parent._PARENT_SEARCH_ITEMS("`I`" + inventory_grid[1, e.RowIndex].Value.ToString());

            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                Clipboard.SetText(inventory_grid[1, e.RowIndex].Value.ToString());
            }
        }

        // Add
        private void search_button_Click(object sender, EventArgs e)
        {
            if (item_desc.Text.Length > 0 && item_quantity.Text.Length > 0)
            {
                if (_EDIT_INDEX < 0)
                {
                    HARDWARE.Add(item_desc.Text + "~" + item_quantity.Text + "~" + DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else
                {
                    string orig_date = HARDWARE[_EDIT_INDEX].Split(new string[] { "~" }, StringSplitOptions.None)[2];
                    HARDWARE[_EDIT_INDEX] = item_desc.Text + "~" + item_quantity.Text + "~" + orig_date;
                    add_inv_button.Text = "Add to Inventory";
                }
                _Populate_Inventory_List();
                _EDIT_INDEX = -1;


            }
            item_desc.Text = "";
            item_quantity.Text = "";
            Sort_Hardware_Quantity();
            _Populate_Inventory_List();
            _Populate_Inventory_List();
            _Populate_Inventory_List();
        }

        // Update  Item
        private void button2_Click_1(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in inventory_grid.Rows)
            for (int i = 0; i < inventory_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(inventory_grid.Rows[i].Cells[0].Value) == true)
                //if (inventory_grid[0,i].
                {
                    add_inv_button.Text = "Update Inventory";
                    _EDIT_INDEX = i;
                    string[] columns = HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None);
                    item_desc.Text = columns[0];
                    item_quantity.Text = columns[1];
                    // what you want to do
                }
            }
        }

        private void delete_item_Click(object sender, EventArgs e)
        {
            add_inv_button.Text = "Add to Inventory";
            _EDIT_INDEX = -1;
            item_desc.Text = "";
            item_quantity.Text = "";
            //foreach (DataGridViewRow row in inventory_grid.Rows)
            for (int i = 0; i < inventory_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(inventory_grid[0, i].Value))
                {
                    HARDWARE.RemoveAt(i);
                    _Populate_Inventory_List();
                    //string[] columns = HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None);
                    // what you want to do
                }
            }
        }

        private void transfer_item_Click(object sender, EventArgs e)
        {
            add_inv_button.Text = "Add to Inventory";
            _EDIT_INDEX = -1;
            item_desc.Text = "";
            item_quantity.Text = "";

            for (int i = 0; i < inventory_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(inventory_grid[0, i].Value))
                {

                    if (!_parent._Pending_Tranfer)
                    {
                        if (Convert.ToInt32(HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None)[1]) > 0)
                        {
                            _parent._Pending_Tranfer = true;
                            _parent.Focus();
                            Tranfer_Inventory_Dialog TID = new Tranfer_Inventory_Dialog(_parent, this, i, HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None)[0], _EMP_NUM); // pass description
                            TID.Show();
                            this.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Cannot transfer: No more inventory for the selected item");
                        }
                    }

                    _Populate_Inventory_List();
                }
            }
        }

        private void item_quantity_TextChanged(object sender, EventArgs e)
        {
            if (item_quantity.Text.All(char.IsDigit))
            {
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                item_quantity.Text = item_quantity.Text.Substring(0, item_quantity.Text.Length - 1);
                item_quantity.SelectionStart = item_quantity.Text.Length;
                item_quantity.SelectionLength = 0;
            }
        }

        public void Store_Update()
        {
            Update_Info_List();
            //_parent._MODIFY_RULE(Inventory_List, _SELECTED_INDEX);
            _parent._STORE_COMPUTER_INFO();
        }

        private void display_zero_CheckedChanged(object sender, EventArgs e)
        {
            _Populate_Inventory_List();
            add_inv_button.Text = "Add to Inventory";
            _EDIT_INDEX = -1;
            item_desc.Text = "";
            item_quantity.Text = "";
        }

        // Remove highlighting
        private void inventory_grid_SelectionChanged(object sender, EventArgs e)
        {
            this.inventory_grid.ClearSelection();
            _parent._RESET_INACTIVITY();
        }

        private void shopping_grid_SelectionChanged(object sender, EventArgs e)
        {
            this.shopping_grid.ClearSelection();
            _parent._RESET_INACTIVITY();
        }

        // Open/close shopping list
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Width < 800)
            {
                button1.Text = "Hide Shopping List";
                this.MaximumSize = new Size(1100, 1000);
                this.MinimumSize = new Size(1100, 472);
                this.Width = 1100;//(1000, this.Height);
            }
            else
            {
                button1.Text = "View Shopping List";
                this.MaximumSize = new Size(708, 1000);
                this.MinimumSize = new Size(708, 472);
                this.Width = 708;//(1000, this.Height);
            }
            _Populate_Shopping_List();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(708, 1000);
            this.Width = 708;//(1000, this.Height);
        }

        // Transfer to shopping cart
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Width < 800)
            {
                button1.PerformClick();
            }

            _EDIT_INDEX = -1;
            item_desc.Text = "";
            item_quantity.Text = "";
            //foreach (DataGridViewRow row in inventory_grid.Rows)
            for (int i = 0; i < inventory_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(inventory_grid[0, i].Value))
                {

                    //string orig_date = HARDWARE[_EDIT_INDEX].Split(new string[] { "~" }, StringSplitOptions.None)[2];
                    //add_inv_button.Text = "Add to Inventory";
                    string[] columns = HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None);
                    _parent._APPEND_TO_SETUP_INFORMATION(2, "Item '" + columns[0] + "' in quantity: 1 added to Shopping List (executed by: " + _parent._EMPLOYEE_LIST[_EMP_NUM] + ")");
                    HARDWARE.Add(columns[0] + "~" + "-1" + "~" + DateTime.Now.ToString("MM/dd/yyyy"));

                    _Populate_Inventory_List();
                    //string[] columns = HARDWARE[i].Split(new string[] { "~" }, StringSplitOptions.None);
                    // what you want to do
                }
            }
            _Populate_Shopping_List();
        }

        private void shopping_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _parent._RESET_INACTIVITY();
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                if (checkBox1.Checked)
                {
                    checkBox1.Checked = false;
                    for (int i = 0; i < shopping_grid.Rows.Count; i++)
                    {
                        shopping_grid[0, i].Value = true;
                    }
                    shopping_grid[0, e.RowIndex].Value = false;
                }
                //MessageBox.Show(e.RowIndex.ToString());//TODO - Button Clicked - Execute Code Here
                //MessageBox.Show(][2]);//TODO - Button Clicked - Execute Code Here
                //_MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]]
                //Button btn = (Button)sender;
                //for (int i = 0; i < shopping_grid.Rows.Count; i++)
                //{
                    //shopping_grid[0, i].Value = false;
                //}
                //shopping_grid[0, e.RowIndex].Value = true;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                string temp = shopping_grid[1, e.RowIndex].Value.ToString();
                Clipboard.SetText(temp.Contains("Ordered on") ? temp.Substring(0, temp.IndexOf("Ordered on") - 2) : temp);
                _parent._PARENT_SEARCH_ITEMS(shopping_grid[1, e.RowIndex].Value.ToString());
            }
        }

        private void move_to_inventory_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in inventory_grid.Rows)
            for (int i = 0; i < shopping_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(shopping_grid[0, i].Value))
                {
                    for (int j = 0; j < HARDWARE.Count(); j++)
                    {
                        try
                        {
                            string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                            if (columns[0] == shopping_grid[1, i].Value.ToString() && columns[1].StartsWith("-"))
                            {
                                HARDWARE[j] = columns[0] + "~" + columns[1].Substring(1) + "~" + columns[2];
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            _Populate_Shopping_List();
            _Populate_Inventory_List();
        }

        private void delete_shopping_item_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shopping_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(shopping_grid[0, i].Value))
                {
                    for (int j = HARDWARE.Count() - 1; j >= 0; j--)
                    {
                        try
                        {
                            string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                            if (shopping_grid[1, i].Value.ToString().Trim().Contains(columns[0].Trim()) && columns[1].StartsWith("-") && (columns.Count() <= 3 || _parent._ENABLE_ADMINISTRATIVE_COMMANDS))
                            {
                                HARDWARE.RemoveAt(j);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            _Populate_Shopping_List();
            _Populate_Inventory_List();
        }

        // Checkout cart
        private void button3_Click(object sender, EventArgs e)
        {
            if (shopping_grid.Rows.Count > 0 && total_amt.Text.Length > 1)
            {
                string description = "";
                bool ready_for_checkout = true;

                #region Validate that all items selected are ready for checkout (i.e. they have been ordered)
                ////
                int check_count = 0;
                for (int i = shopping_grid.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(shopping_grid[0, i].Value))
                    {
                        for (int j = 0; j < HARDWARE.Count(); j++)
                        {
                            check_count++;
                            try
                            {
                                string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                                if (columns[0] == shopping_grid[1, i].Value.ToString() && columns[1].StartsWith("-") && columns.Count() > 3 && !columns[3].Contains("`P`"))
                                {
                                    ready_for_checkout = false;
                                }
                                else if (columns[0] == shopping_grid[1, i].Value.ToString() && columns[1].StartsWith("-") && columns.Count() <= 3)
                                {
                                    ready_for_checkout = false;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                #endregion

                if (ready_for_checkout && check_count > 0)
                {
                    for (int i = shopping_grid.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToBoolean(shopping_grid[0, i].Value))
                        {
                            for (int j = 0; j < HARDWARE.Count(); j++)
                            {
                                try
                                {
                                    string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                                    if (shopping_grid[1, i].Value.ToString().Contains(columns[0]) && columns[1].StartsWith("-"))
                                    {
                                        description = description + columns[0] + "(" + columns[1].Substring(1) + "),`";
                                        HARDWARE[j] = columns[0] + "~" + columns[1].Substring(1) + "~" + columns[2] + "~" + columns[3];
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    _parent._APPEND_TO_SETUP_INFORMATION(2, "Shopping List Order Recieved - " + (description.Length > 0 ? description.Substring(0, description.Length - 2) : "")+ " (executed by: " + _parent._EMPLOYEE_LIST[_EMP_NUM] + ")");
                    string query = "insert into d_it_budget values ('" + DateTime.Now.ToString("MM/dd/yyyy") + "', '" + description.TrimEnd(Convert.ToChar(",")) + "', '" + total_amt.Text.Substring(1) + "', '" + _EMP_NUM + "', '" + _CHECKOUT_NOTES + "')";


                    _parent.database.Open();
                    _parent.reader = _parent.database.RunQuery(query);
                    _parent.reader.Close();

                    _Populate_Shopping_List();
                    _Populate_Inventory_List();

                    total_amt.Text = "";
                    _CHECKOUT_NOTES = "";
                }
                else
                {
                    MessageBox.Show("One or more items selected for receiving has not been ordered. Please verify and check out again");
                }
            }
            else if (total_amt.Text.Length <= 1)
            {
                MessageBox.Show("Checkout error: Missing receiving total amount");
            }
            else
            {
                MessageBox.Show("Checkout error: Cart is empty");
            }

        }

        private void add_shopping_cart_Click(object sender, EventArgs e)
        {

            if (item_desc.Text.Contains("~") || item_quantity.Text.Contains("~"))
            {
                MessageBox.Show("Error: Invalid character detected: '~'");
            }
            else
            {
                if (item_desc.Text.Length > 0 && item_quantity.Text.Length > 0)
                {
                    if (this.Width < 800)
                    {
                        button1.PerformClick();
                    }

                    _parent._APPEND_TO_SETUP_INFORMATION(2, "Item '" + item_desc.Text + "' in quantity: " + item_quantity.Text + " added to Shopping List (executed by: " + _parent._EMPLOYEE_LIST[_EMP_NUM] + ")");
                    HARDWARE.Add(item_desc.Text + "~-" + item_quantity.Text + "~" + DateTime.Now.ToString("MM/dd/yyyy"));
                    item_quantity.Text = "";
                    item_desc.Text = "";
                    Sort_Hardware_Quantity();
                    _Populate_Shopping_List();
                    _Populate_Inventory_List();
                    _Populate_Inventory_List();
                    _Populate_Inventory_List();
                }
                else
                {
                    MessageBox.Show(item_desc.Text.Length == 0 ? "Missing item description" : "Missing item quantity");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < shopping_grid.Rows.Count; i++)
                {
                    shopping_grid[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < shopping_grid.Rows.Count; i++)
                {
                    shopping_grid[0, i].Value = false;
                }
            }

        }

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

        private void total_amt_TextChanged(object sender, EventArgs e)
        {
            if (!(total_amt.Text.StartsWith("$")))
            {
                if (Get_Char_Count(total_amt.Text, Convert.ToChar("$")) == 1)
                {
                    string temp = total_amt.Text;
                    total_amt.Text = temp.Substring(1) + temp[0];
                    total_amt.SelectionStart = total_amt.Text.Length;
                    total_amt.SelectionLength = 0;
                }
                else
                {
                    total_amt.Text = "$" + total_amt.Text;
                }
            }
            else if ((total_amt.Text.Length > 1) && ((Get_Char_Count(total_amt.Text, Convert.ToChar(".")) > 1) || (total_amt.Text[1].ToString() == ".") || (Get_Char_Count(total_amt.Text, Convert.ToChar("$")) > 1) || (!((total_amt.Text.Substring(total_amt.Text.Length - 1).All(char.IsDigit))) && !(total_amt.Text[total_amt.Text.Length - 1].ToString() == "."))))
            {
                total_amt.TextChanged -= new System.EventHandler(total_amt_TextChanged);
                total_amt.Text = total_amt.Text.Substring(0, total_amt.Text.Length - 1);
                total_amt.SelectionStart = total_amt.Text.Length;
                total_amt.SelectionLength = 0;
                total_amt.TextChanged   += new System.EventHandler(total_amt_TextChanged);
            }
        }

        private void print_list_Click(object sender, EventArgs e)
        {
            bool ready_to_print = false;
            for (int i = 0; i < shopping_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(shopping_grid[0, i].Value))
                    ready_to_print = true;
            }


            #region Validate that all items selected are ready for checkout (i.e. they have been ordered)
            ////


            for (int i = shopping_grid.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToBoolean(shopping_grid[0, i].Value))
                {
                    for (int j = 0; j < HARDWARE.Count(); j++)
                    {
                        try
                        {
                            string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                            if (shopping_grid[1, i].Value.ToString().Contains(columns[0].Trim()) && columns[1].StartsWith("-") && columns.Count() > 3 && columns[3].Contains("`P`"))
                            {
                                ready_to_print = false;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            #endregion

            if (ready_to_print)
            {


                DialogResult dialogResult = MessageBox.Show("You cannot reverse this order once placed. Do you wish to continue?", "", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    Random r = new Random();
                    string ORDER_ID = r.Next(100000000, 999999999).ToString(); // len = 9

                    // Mark selected as pending
                    for (int i = shopping_grid.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToBoolean(shopping_grid[0, i].Value))
                        {
                            for (int j = 0; j < HARDWARE.Count(); j++)
                            {
                                try
                                {
                                    string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                                    if (shopping_grid[1, i].Value.ToString() == (columns[0].Trim()) && columns[1].StartsWith("-"))
                                    {
                                        HARDWARE[j] = HARDWARE[j] + "~`P`" + DateTime.Now.ToString("MM/dd/yyyy") + "~" + ORDER_ID;  // Mark as pending
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    print_index = 1;
                    //Dialog1.TopMost = true;
                    //printPreviewDialog1.ShowDialog();
                    
                    DialogResult dialogResult2 = MessageBox.Show("Do you wish to print the order form?", "", MessageBoxButtons.YesNo);

                    if (dialogResult2 == DialogResult.Yes)
                    {
                        printDocument1.Print();
                    }
                    else if (dialogResult2 == DialogResult.No)
                    {
                    }
                    _Populate_Shopping_List();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            else
            {
                MessageBox.Show("Please select the item(s) you wish to print and make sure the selected item(s) have not been ordered");
            }
        }


        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Screen capture
            //e.Graphics.DrawImage(memoryImage, 0, 0);

            // Manually generate
            int height = 0;

            int startx = 50;
            int starty = 40;
            int ColWid1 = 351;
            int ColWid2 = 70;
            int ColWid3 = 100;
            int ColWid4 = 190;
            //int ColWid5 = 165;
            //int ColWid6 = 80;
            //int ColWid7 = 130;


            int headheight = 30;
            int dataheight = 40;

            Pen p = new Pen(Brushes.Black, 2.5f);
            Font f = new Font("Times New Roman", 12.0f);
            Font f1 = new Font("Times New Roman", 14.0f);
            Font f2 = new Font("Times New Roman", 18.0f, FontStyle.Bold);

            e.Graphics.DrawString("Invoice #:", f2, Brushes.Black, new Rectangle(startx, starty, 200, dataheight));
            //e.Graphics.DrawString("test", f2, Brushes.Black, new Rectangle(startx + 200, starty, 200, dataheight));
            starty += dataheight;
            e.Graphics.DrawString("Date:", f2, Brushes.Black, new Rectangle(startx, starty, 200, dataheight));
            e.Graphics.DrawString(DateTime.Now.ToString("MM/dd/yyyy"), f2, Brushes.Black, new Rectangle(startx + 65, starty, 200, dataheight));
            starty += dataheight + 20;


            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;

            int vertical_buffer_space = 6;
            int vertical_buffer_space2 = 9;

            #region Header Row
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx, starty, ColWid1, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx, starty, ColWid1, headheight));
            e.Graphics.DrawString("Item Description", f, Brushes.Black, new Rectangle(startx, starty + vertical_buffer_space, ColWid1, headheight), format1);

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1, starty, ColWid2, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1, starty, ColWid2, headheight));
            e.Graphics.DrawString("Quantity", f, Brushes.Black, new Rectangle(startx + ColWid1, starty + vertical_buffer_space, ColWid2, headheight), format1);

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2, starty, ColWid3, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2, starty, ColWid3, headheight));
            e.Graphics.DrawString("Unit Price", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2, starty + vertical_buffer_space, ColWid3, headheight), format1);

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, starty, ColWid4, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, starty, ColWid4, headheight));
            e.Graphics.DrawString("Purchase Location", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, starty + vertical_buffer_space, ColWid4, headheight), format1);

            /*
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));
            e.Graphics.DrawString("Station", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));
            e.Graphics.DrawString("Status", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            e.Graphics.DrawString("Assigned", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            */
            #endregion


            height = starty + headheight;
            
            List<string> Filtered = new List<string>(HARDWARE);
            Filtered.RemoveAll(o => (Convert.ToInt32(o.Split(new string[] { "~" }, StringSplitOptions.None)[1])) >= 0); // Remove all hardware from list that is not shopping
            Filtered.RemoveAll(o => Convert.ToBoolean(shopping_grid[0, Filtered.IndexOf(o)].Value) == false); // Remove all hardware from list that is not shopping

            string description = "";
            while (print_index < Filtered.Count + 1)
            {

                string[] die = Filtered[print_index - 1].Split(new string[] { "~" }, StringSplitOptions.None);
                if (height > e.MarginBounds.Height)
                {
                    height = starty;
                    e.HasMorePages = true;
                    return;
                }

                e.Graphics.DrawRectangle(p, new Rectangle(startx, height, ColWid1, dataheight));
                e.Graphics.DrawString(die[0], f1, Brushes.Black, new Rectangle(startx, height + vertical_buffer_space2, ColWid1, dataheight), format1);

                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1, height, ColWid2, dataheight));
                e.Graphics.DrawString(die[1].Substring(1), f1, Brushes.Black, new Rectangle(startx + ColWid1, height + vertical_buffer_space2, ColWid2, dataheight), format1);

                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2, height, ColWid3, dataheight));
                e.Graphics.DrawString("", f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2, height + vertical_buffer_space2, ColWid3, dataheight), format1);

                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, height, ColWid4, dataheight));
                e.Graphics.DrawString("", f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, height + vertical_buffer_space2, ColWid4, dataheight), format1);

                // Database purpose line
                description = description + die[0] + "(" + die[1].Substring(1) + "),";

                /*
                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, height, ColWid5, dataheight));
                e.Graphics.DrawString(die[14], f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, height, ColWid5, dataheight));

                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, height, ColWid6, dataheight));
                e.Graphics.DrawString(die[11] == "true" ? "Ready" : "", f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, height, ColWid6, dataheight));

                e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, height, ColWid7, dataheight));
                e.Graphics.DrawString(die[20], f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, height, ColWid7, dataheight));
                */
                height += dataheight;
                print_index++;


            }

            height += dataheight;
            e.Graphics.DrawString("Total: $", f1, Brushes.Black, new Rectangle(620, height, 200, dataheight));


            _parent._APPEND_TO_SETUP_INFORMATION(2, "Shopping List Order Created (Not checked out) - " + description.TrimEnd(Convert.ToChar(",")) + " (executed by: " + _parent._EMPLOYEE_LIST[_EMP_NUM] + ")");
            string query = "insert into d_it_budget values ('"  + DateTime.Now.ToString("MM/dd/yyyy") + " (Print out; not purchase)', '" + description.TrimEnd(Convert.ToChar(",")) + "', '" + total_amt.Text.Substring(1) + "', '" + _EMP_NUM + "', '')";

            _parent.database.Open();
            _parent.reader = _parent.database.RunQuery(query);
            _parent.reader.Close();

            _CHECKOUT_NOTES = "";
        }

        private void item_desc_TextChanged(object sender, EventArgs e)
        {
            if (item_desc.Text.EndsWith("~"))
            {
                item_desc.Text = item_desc.Text.Substring(0, item_desc.Text.Length - 1);
                item_desc.SelectionStart = item_desc.Text.Length;
                item_desc.SelectionLength = 0;
            }
        }

        private void notes_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            _CHECKOUT_NOTES = Microsoft.VisualBasic.Interaction.InputBox("If you want to attach a note to the receiving entry, enter it here.", "Add Note", _CHECKOUT_NOTES, -1, -1);
            this.Show();
        }

        public string tracking_number = "";
        public string eta_date = "";

        private void shopping_grid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Random r = new Random();
            string ORDER_ID = r.Next(100000000, 999999999).ToString(); // len = 9

            tracking_number = "";
            eta_date = "";
            overwrite_tracking_info = false;

            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                //MessageBox.Show(e.RowIndex.ToString());//TODO - Button Clicked - Execute Code Here
                //MessageBox.Show(][2]);//TODO - Button Clicked - Execute Code Here
                //_MASTER_COMPUTER_LIST[_SEARCH_BUTTON_ARRAY[e.RowIndex]]
                //Button btn = (Button)sender;
                //MessageBox.Show(shopping_grid[e.RowIndex, 3].Value.ToString());// [e.RowIndex][3]);
                bool load = false;
                for (int j = 0; j < HARDWARE.Count(); j++)
                {
                    try
                    {
                        string[] columns = HARDWARE[j].Split(new string[] { "~" }, StringSplitOptions.None);
                        string temp = shopping_grid[1, e.RowIndex].Value.ToString();
                        if (temp.Contains(columns[0].Trim()) && columns[1].StartsWith("-") && columns.Count() >= 4)
                        {
                            string[] columns2 = columns[4].Split(new string[] { ",`" }, StringSplitOptions.None);
                            if (!load)
                            {
                                //HARDWARE[j] = HARDWARE[j] + "~`P`" + DateTime.Now.ToString("MM/dd/yyyy");  // Mark as pending
                                Shipping_Information_Dialog SID = new Shipping_Information_Dialog(this, _parent, columns2.Count() > 1 ? columns2[1] : "", columns2.Count() > 1 ? columns2[2] : "");
                                SID.ShowDialog(this);
                                //MessageBox.Show(shopping_grid[1, e.RowIndex].Value.ToString());// [e.RowIndex][3]);
                                string temp1 = columns[0] + "~" + columns[1] + "~" + columns[2] + "~" + columns[3] + "~" + (overwrite_tracking_info ? ORDER_ID : columns2[0]) + (tracking_number.Length > 0 ? ",`" + tracking_number : "") + (tracking_number.Length > 0 ? ",`" + eta_date : "");
                                HARDWARE[j] = temp1;
                                //MessageBox.Show(e.RowIndex.ToString());// [e.RowIndex][3]);
                                load = true;
                            }

                            if (!overwrite_tracking_info)
                            {
                                for (int jj = 0; jj < HARDWARE.Count(); jj++)
                                {
                                    try
                                    {
                                        string[] columnsj = HARDWARE[jj].Split(new string[] { "~" }, StringSplitOptions.None);
                                        string tempj = shopping_grid[1, e.RowIndex].Value.ToString();
                                        string[] columns2j = columnsj[4].Split(new string[] { ",`" }, StringSplitOptions.None);
                                        if (HARDWARE[jj].Contains(columns2[0].Trim()) && columns[1].StartsWith("-") && columns.Count() >= 4 && j != jj)
                                        {
                                            string temp1j = columnsj[0] + "~" + columnsj[1] + "~" + columnsj[2] + "~" + columnsj[3] + "~" + columns2j[0] + (tracking_number.Length > 0 ? ",`" + tracking_number : "") + (tracking_number.Length > 0 ? ",`" + eta_date : "");
                                            HARDWARE[jj] = temp1j;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                _Populate_Shopping_List();
            }
        }

    }
}