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
    public partial class PurchaseHistory : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _parent._PURCHASE_HISTORY_OPEN = false;
            _parent.Focus();
            this.Dispose();
        }

        Office _parent;

        //       0            1               2        3       4
        // DATE[]ITEM#1,`ITEM#2.`ITEM#3...[]TOTAL[]EMPLOYEE[]NOTES
        private List<string> Purchase_History = new List<string>();

        public PurchaseHistory(Office parent)
        {
            InitializeComponent();
            _parent = parent;
            duration_list.Items.Add("Last Month");
            duration_list.Items.Add("Last Quarter");
            duration_list.Items.Add("Last Year");
            duration_list.Items.Add("All Purchases");
            duration_list.Text = "Last Month";

            for (int i = 0; i < purchase_grid.ColumnCount; i++)
            {
                purchase_grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                purchase_grid.Columns[i].ReadOnly = true;
            }

            purchase_grid.DefaultCellStyle.SelectionBackColor = purchase_grid.DefaultCellStyle.BackColor;
            purchase_grid.DefaultCellStyle.SelectionForeColor = purchase_grid.DefaultCellStyle.ForeColor;

            Get_Purchase_History();
            Populate_History();
        }

        private void Get_Purchase_History()
        {
            Purchase_History = new List<string>();

            string temp = duration_list.Text;


            string search_duration = temp.Contains("Month") ? DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy") : temp.Contains("Quarter") ? DateTime.Now.AddDays(-90).ToString("MM/dd/yyyy") : temp.Contains("Year") ? DateTime.Now.AddDays(-365).ToString("MM/dd/yyyy") : " * ";
            string query = "select * from d_IT_Budget where purchase_date not like '%print%' " + (temp.Contains("ll Purch") ? "" : "and convert(datetime, purchase_date, 101) >= '" + search_duration + "'") + " order by ID desc";

            _parent.database.Open();
            _parent.reader = _parent.database.RunQuery(query);

            string temp1 = "";
            while (_parent.reader.Read())
            {
                temp1 = _parent.reader[1].ToString().Trim() + "▄";
                temp1 = temp1 + _parent.reader[2].ToString().Trim() + "▄";
                temp1 = temp1 + _parent.reader[3].ToString().Trim() + "▄";
                temp1 = temp1 + _parent._EMPLOYEE_LIST[_parent.reader[4].ToString().Trim()] + "▄";
                temp1 = temp1 + _parent.reader[5].ToString().Trim();
                Purchase_History.Add(temp1);
            }
            _parent.reader.Close();
        }

        private void Populate_History()
        {
            purchase_grid.Rows.Clear();
            int order_index = 0;
            double total_paid = 0;
            foreach (string order in Purchase_History)
            {
                if (search_box.Text.Length > 0 && order.ToLower().Contains(search_box.Text.ToLower()))
                {
                    string[] temp = order.Split(new string[] { "▄" }, StringSplitOptions.None);
                    string[] items = temp[1].Split(new string[] { ",`" }, StringSplitOptions.None);
                    int index = 0;
                    foreach (string item in items)
                    {
                        if (item.Length > 0)
                        {
                            string item_desc = item.Substring(0, item.LastIndexOf(Convert.ToChar("(")));
                            string item_quantity = item.Substring(item.LastIndexOf(Convert.ToChar("(")) + 1).Substring(0, item.Substring(item.LastIndexOf(Convert.ToChar("(")) + 1).Length - 1);
                            if (index == 0)
                            {
                                purchase_grid.Rows.Add(temp[0], item_desc, item_quantity, temp[2], temp[3], temp[4]);
                                try
                                {
                                    total_paid += Convert.ToDouble(temp[2]);
                                }
                                catch
                                {
                                    // Int conversion error 
                                }
                            }
                            else
                            {
                                purchase_grid.Rows.Add("", item_desc, item_quantity, "", "", "");
                            }

                            index++;
                            if (order_index % 2 == 1)
                            {
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[0].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[2].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[3].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[4].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightGray;
                            }
                        }
                    }
                    order_index++;
                }
                else if (search_box.Text.Length == 0)
                {
                    string[] temp = order.Split(new string[] { "▄" }, StringSplitOptions.None);
                    string[] items = temp[1].Split(new string[] { ",`" }, StringSplitOptions.None);
                    int index = 0;
                    foreach (string item in items)
                    {
                        if (item.Length > 0)
                        {
                            string item_desc = item.Substring(0, item.LastIndexOf(Convert.ToChar("(")));
                            string item_quantity = item.Substring(item.LastIndexOf(Convert.ToChar("(")) + 1).Substring(0, item.Substring(item.LastIndexOf(Convert.ToChar("(")) + 1).Length - 1);
                            if (index == 0)
                            {
                                purchase_grid.Rows.Add(temp[0], item_desc, item_quantity, temp[2], temp[3], temp[4]); try
                                {
                                    total_paid += Convert.ToDouble(temp[2]);
                                }
                                catch
                                {
                                    // Int conversion error 
                                }
                            }
                            else
                            {
                                purchase_grid.Rows.Add("", item_desc, item_quantity, "", "", "");
                            }

                            index++;
                            if (order_index % 2 == 1)
                            {
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[0].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[2].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[3].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[4].Style.BackColor = Color.LightGray;
                                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightGray;
                            }
                        }
                    }
                    order_index++;
                }
            }
            if (purchase_grid.Rows.Count > 0)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(purchase_grid.Font, FontStyle.Bold);
                purchase_grid.Rows.Add("", "", "Total", total_paid > 0 ? "$" + total_paid.ToString() : "NIL", "", "");
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[0].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[2].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[3].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[4].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightPink;
                purchase_grid.Rows[purchase_grid.Rows.Count - 1].DefaultCellStyle = style;
            }
            Reset_Form_Size();
            int cell_height = 20;
            this.Height = this.MinimumSize.Height + (purchase_grid.Rows.Count * cell_height);
        }

        private void Reset_Form_Size()
        {
            this.Size = this.MinimumSize;
        }

        private void duration_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Purchase_History();
            Populate_History();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            //Get_Purchase_History();
            Populate_History();
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = purchase_grid[column, row];
            DataGridViewCell cell2 = purchase_grid[column, row - 1];
            //if (cell1 == null || cell2.Value == null)
            if (cell1.Style.BackColor == cell2.Style.BackColor && cell1.Style.ForeColor == cell2.Style.ForeColor)
            {
                //return cell1.Value.ToString() == cell2.Value.ToString();
                return true;
            }
            return false;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            else if (e.ColumnIndex == 0 || e.ColumnIndex >= 3)// 3|| e.ColumnIndex == 5)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = purchase_grid.AdvancedCellBorderStyle.Top;
                }
            }
            else
                e.AdvancedBorderStyle.Top = purchase_grid.AdvancedCellBorderStyle.Top;
            if (e.RowIndex == purchase_grid.Rows.Count-1)
            {
                e.AdvancedBorderStyle.Bottom = purchase_grid.AdvancedCellBorderStyle.Bottom;
            }

        }

        private void PurchaseHistory_Load(object sender, EventArgs e)
        {
            purchase_grid.AutoGenerateColumns = false;
            purchase_grid.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
            purchase_grid.SelectionChanged += new EventHandler(purchase_grid_SelectionChanged);
        }

        private void purchase_grid_SelectionChanged(object sender, EventArgs e)
        {
            this.purchase_grid.ClearSelection();
        }
    }
}
