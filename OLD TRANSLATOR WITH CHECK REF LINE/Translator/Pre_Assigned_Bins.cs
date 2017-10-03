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
    public partial class Pre_Assigned_Bins : Form
    {
        // Override original form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            //this.Close();
            try
            {
                if (Existing_Values)
                {
                    BV._parent._GLOBAL_DELETE_FORMAT_RULE("BIN_ASSIGNMENT");
                }
                List<string> g = new List<string>();
                string temp = "";
                foreach (List<string> gg in Bin_Assignment_List)
                {
                    temp = temp + "|" + gg[0] + "~" + gg[1] + "~" + gg[2];
                }
                if (temp.Length > 3)
                {
                    BV._parent._GLOBAL_ADD_FORMAT_RULE(new List<string>() { "FORMAT_RULE", "BIN_ASSIGNMENT", temp.Substring(1), "", "", "" });
                }
                BV.Populate_Grid();
                BV.Focus();
            }
            catch
            {
            }
            this.Dispose();
        }

        int bin_edit = -1;
        Bin_View BV;
        List<List<string>> Rule_List;
        List<List<string>> Bin_Assignment_List = new List<List<string>>();
        bool Existing_Values = false;

        public Pre_Assigned_Bins(Bin_View _BV, List<List<string>> _RL)
        {
            InitializeComponent();
            BV = _BV;
            Rule_List = _RL;
            Set_Bin_Assignment_List();
            Populate_Bins();
        }

        private void Set_Bin_Assignment_List()
        {
            foreach (List<string> rule in Rule_List)
            {
                if (rule[0] == "FORMAT_RULE" && rule[1] == "BIN_ASSIGNMENT")
                {
                    Existing_Values = true;
                    string[] split_1 = rule[2].Split(new string[] { "|" }, StringSplitOptions.None);
                    foreach (string g in split_1)
                    {
                        string[] split_2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                        Bin_Assignment_List.Add(new List<string>() { split_2[0], split_2[1], split_2[2] });
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bin_val.Text.Length > 0 && bin_num.Text.Length > 0)
            {
                if (Convert.ToInt32(bin_num.Text) >= 9997 || Convert.ToInt32(bin_num.Text) < 0)
                {
                    MessageBox.Show("You can only assign bin numbers from 0 to 9997");
                }
                else
                {
                    if (Check_Bin_Number_Collision(bin_num.Text) && !button1.Text.Contains("Edit"))
                    {
                        MessageBox.Show("There is an existing pre-assigned value in bin # " + bin_num.Text);
                    }
                    if (button1.Text.Contains("Edit"))
                    {
                        Bin_Assignment_List[bin_edit] = new List<string>() { bin_num.Text, bin_val.Text, bin_comment.Text };
                    }
                    else
                    {
                        Bin_Assignment_List.Add(new List<string>() { bin_num.Text, bin_val.Text, bin_comment.Text });
                    }
                    Populate_Bins();
                    bin_num.Text = ""; bin_val.Text = ""; bin_comment.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Missing information above");
            }
            button1.Text = "Add Bin";
        }

        private void bin_num_TextChanged(object sender, EventArgs e)
        {
            if (!bin_num.Text.All(char.IsDigit))
            {
                // If letter in SO_number box, do not output and move CARET to end
                bin_num.Text = bin_num.Text.Substring(0, bin_num.Text.Length - 1);
                bin_num.SelectionStart = bin_num.Text.Length;
                bin_num.SelectionLength = 0;
            }
        }

        private void Populate_Bins()
        {
            List<List<string>> SortedList = Bin_Assignment_List.OrderBy(o => Convert.ToInt32(o[0])).ToList();
            Bin_Assignment_List = SortedList;

            dataGridView1.Rows.Clear();
            foreach (List<string> g in Bin_Assignment_List)
            {
                dataGridView1.Rows.Add(false, g[0], g[1], g[2]);
            }
            dataGridView1.Rows.Add(false, "9997", "Loaded File Path", "");
            dataGridView1.Rows.Add(false, "9998", "Loaded File Name", "");
            dataGridView1.Rows.Add(false, "9999", "Run-time Time", "DateTime.Now()");

            var dataGridViewCellStyle2 = new DataGridViewCellStyle { Padding = new Padding(1000, 0, 0, 0) };
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style = dataGridViewCellStyle2;
            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Style = dataGridViewCellStyle2;
            dataGridView1.Rows[dataGridView1.Rows.Count - 3].Cells[0].Style = dataGridViewCellStyle2;
        }

        private bool Check_Bin_Number_Collision(string bin_number)
        {
            foreach (List<string> g in Bin_Assignment_List)
            {
                if (bin_number == g[0])
                {
                    return true;
                }
            }
            return false;
        }

        private void Pre_Assigned_Bins_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (button1.Text.Contains("Edit")) button1.Text = "Add Bin";

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1[0, i].Value = false;
                }
                dataGridView1[0, e.RowIndex].Value = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sel_index = -1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1[0, i].Value))
                {
                    sel_index = i;
                }
            }
            try
            {
                if (sel_index < (dataGridView1.Rows.Count - 3))
                {
                    Bin_Assignment_List.RemoveAt(sel_index);
                    Populate_Bins();
                }
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int sel_index = -1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1[0, i].Value))
                {
                    button1.Text = "Save Edit";
                    sel_index = i;
                }
            }
            try
            {
                if (sel_index < (dataGridView1.Rows.Count - 3))
                {
                    bin_num.Text = Bin_Assignment_List[sel_index][0];
                    bin_val.Text = Bin_Assignment_List[sel_index][1];
                    bin_comment.Text = Bin_Assignment_List[sel_index][2];
                    bin_edit = sel_index;
                    //Populate_Bins();
                }
            }
            catch
            {
            }
            if (sel_index < 0) button1.Text = "Add Bin";
        }
    }
}
