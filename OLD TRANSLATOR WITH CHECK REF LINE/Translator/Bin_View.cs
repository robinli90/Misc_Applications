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

    public partial class Bin_View : Form
    {
        // Override original form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            _parent.Bin_View_Open = false;
            _parent.Focus();
            //this.Close();
            this.Dispose();

        }

        public Translator _parent;
        List<List<string>> Rule_List;
        Point g = new Point();

        public Bin_View(Translator parent, List<List<string>> rules, string rule_name, Point spawn_location)
        {
            _parent = parent;
            Rule_List = rules;
            InitializeComponent();
            Populate_Grid();
            g = spawn_location;
            this.Text = rule_name + "'s Bins Allocated";
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        public void Populate_Grid()
        {
            this.Size = new Size(952, 122);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < Rule_List.Count(); i++)
            {
                if (i >= 22)
                {
                    Console.WriteLine();
                }
                string p = Rule_List[i][5];
                if (p.Contains("[/GRP]"))
                {
                    int start_index = p.IndexOf("[GRP]");
                    int end_index = p.IndexOf("[/GRP]") + 6;
                    p = p.Substring(end_index);
                }
                if (p.Contains(" bin") || p.Contains("(bin") || p.Contains("IFCHK((") ||// If comment contains ' bin'
                    _parent._Setup_Rule_List(true, i).Contains(" bin") || _parent._Setup_Rule_List(true, i).Contains("(bin") || _parent.COUNT_TOKEN(_parent._Setup_Rule_List(true, i), "%") >= 2)
                {
                    // showing the original rule line takes precedence to comment
                    if (_parent._Setup_Rule_List(true, i).Contains(" bin") || _parent._Setup_Rule_List(true, i).Contains("(bin") || _parent.COUNT_TOKEN(_parent._Setup_Rule_List(true, i), "%") >= 2 )
                    {
                        string g = Parse_Bin_Numbers(_parent._Setup_Rule_List(true, i));
                        if (g.Contains(textBox1.Text))
                        dataGridView1.Rows.Add((i + 1).ToString(), _parent._Setup_Rule_List(true, i), g);
                    }
                    else if (p.Contains(" bin") || p.Contains("(bin"))
                    {
                        string g = Parse_Bin_Numbers(Rule_List[i][5]);
                        if (g.Contains(textBox1.Text))
                        dataGridView1.Rows.Add((i + 1).ToString(), p, g);
                    }
                    else if (p.Contains("IFCHK(("))
                    {
                        string g = Parse_Bin_Numbers(Rule_List[i][5]);
                        if (g.Contains(textBox1.Text))
                        dataGridView1.Rows.Add((i + 1).ToString(), _parent._Setup_Rule_List(true, i), g);
                    }
                }
                else if (Rule_List[i][0] == "FORMAT_RULE" && Rule_List[i][1] == "BIN_ASSIGNMENT")
                {
                    int count = 0;
                    string[] split_1 = Rule_List[i][2].Split(new string[] { "|" }, StringSplitOptions.None);
                    foreach (string g in split_1)
                    {
                        string[] split_2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                        if (split_2[0].Contains(textBox1.Text))
                        dataGridView1.Rows.Add("10000", "Pre-assigned Value: " + split_2[1] + " (" + split_2[2] + ")", split_2[0]);
                        count++;
                        int index2 = dataGridView1.Rows.Count - 1;

                        var dataGridViewCellStyle2 = new DataGridViewCellStyle { Padding = new Padding(1000, 0, 0, 0) };
                        if (index2 >= 0)
                        {
                            dataGridView1.Rows[index2].Cells[0].Style = dataGridViewCellStyle2;
                            dataGridView1.Rows[index2].Cells[0].Style.BackColor = (index2 % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                            dataGridView1.Rows[index2].Cells[1].Style.BackColor = (index2 % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                            dataGridView1.Rows[index2].Cells[2].Style.BackColor = (index2 % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                        }
                    }
                }
                int index = dataGridView1.Rows.Count - 1;
                if (index >= 0)
                {
                    dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                    dataGridView1.Rows[index].Cells[1].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                    dataGridView1.Rows[index].Cells[2].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                }
            }
            this.Height += dataGridView1.Rows.Count * 20;
        }

        private string Parse_Bin_Numbers(string rule_line)
        {
            if (rule_line.Contains("Perform Range")) Console.Write(rule_line);

            string return_line = "";
            string parsed_string = "";
            string parsed_number = "";
            bool parsing_number = true;
            string[] Valid_Tokens = { " ", "b", "i", "n", "=", "#", ")", "(", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", ":"};
            string[] Valid_Number_Tokens = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            if (rule_line.Contains("bin"))
            {
                for (int i = 0; i < rule_line.Length; i++)
                {
                    if (Valid_Tokens.Contains(rule_line[i].ToString()))
                    {
                        parsed_string = parsed_string + rule_line[i].ToString();
                        if (parsed_string.Contains("bin"))
                        {
                            if (Char.IsDigit(rule_line[i]))
                            {
                                parsing_number = true;
                                parsed_number += rule_line[i];
                            }
                            if (parsing_number && (!Char.IsDigit(rule_line[i]) || i == rule_line.Length - 1))
                            {
                                return_line += parsed_number + ", ";
                                parsing_number = false;
                                parsed_number = "";
                            }
                        }
                    }
                    else
                    {
                        parsed_string = "";
                        parsing_number = false;
                        // skip
                    }
                }
            }
            if (_parent.COUNT_TOKEN(rule_line, "%") >= 2)
            {
                int it_index = 0;
                string front = "";
                string middle = "";
                string temp = "";
                string end = "";
                bool parsing = false;
                bool done_middle = false;

                foreach (char c in rule_line)
                {
                    temp = temp + c.ToString();
                    if (!done_middle && !parsing && c.ToString() == "%")
                    {
                        parsing = true;
                        temp = "";
                    }
                    else if (parsing && done_middle)
                    {
                        end = end + c.ToString();
                    }
                    else if (!done_middle && parsing && c.ToString() == "%")
                    {
                        try
                        {
                            if (rule_line.Contains("Perform Range")) Console.Write(rule_line.Substring(it_index));
                            string temp5 = (it_index + 1 < rule_line.Length ? Parse_Bin_Numbers(rule_line.Substring(it_index + 1)) : "");
                            string temp6 = temp.Substring(0, temp.Length - 1);
                            try
                            {
                                Convert.ToInt32(temp6);
                                return_line += temp.Substring(0, temp.Length - 1) + ", " + temp5;
                                done_middle = true;
                            }
                            catch
                            {
                                return_line += "";
                                done_middle = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (!parsing && middle.Length < 1 && !done_middle)
                    {
                        temp = "";
                        front = front + c.ToString();
                    }
                    it_index++;
                }
            }
            if (rule_line.Contains("IFCHK(("))
            {
                int index_start = rule_line.IndexOf("IFCHK((") + 7;
                int end_index = rule_line.Substring(index_start).IndexOf(":");
                return_line += rule_line.Substring(index_start, end_index) + ", ";
                Console.WriteLine(return_line);
            }
            if (return_line.Length > 2)
            {
                return_line = return_line.Trim();
                return_line = return_line.Trim(Convert.ToChar(","));
                return return_line;// return_line.Substring(0, return_line.Length - 2);
            }
            else
            {
                return "";
            }
        }

        private void Bin_View_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            dataGridView1.SortCompare += new DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            if (dataGridView1.Rows.Count == 0)
            {
                //MessageBox.Show("There are no rules that utilize the bin functionality");
                //this.Close();
                //this.Dispose();
            }
            this.Location = g;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {

            }
            else if (e.RowIndex >= 0)
            {
                try
                {
                    _parent.translation_rules.ClearSelected();
                    _parent.translation_rules.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) - 1;
                    _parent.translation_rules.TopIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) - 12;
                }
                catch
                {
                }
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pre_Assigned_Bins PAB = new Pre_Assigned_Bins(this, Rule_List);
            PAB.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_parent.file_name_loaded.Length == 0)
            {
                _parent.load_file_button.PerformClick();
            }
            _parent.Preview_Toggle = false;
            _parent.button5.PerformClick();
            if (_parent.Translation_Complete)
            {
                string return_string = "";
                for (int i = 0; i < _parent.MEMORY_BIN.Count(); i++)
                {
                    if (_parent.MEMORY_BIN[i] != "0")
                    {
                        return_string += "Bin = " + i.ToString() + ",   Value = " + _parent.MEMORY_BIN[i] + Environment.NewLine;
                    }
                }
                MessageBox.Show(return_string);
            }
            else
            {
                MessageBox.Show("Translation not executed. Unable to grab bin report estimate");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Populate_Grid();
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Suppose your interested column has index 1
            if (e.Column.Index == 0)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
            else if (e.Column.Index == 2)
            {
                string first = e.CellValue1.ToString().Length == 0 ? "10000" : e.CellValue1.ToString().Length > 4 ? e.CellValue1.ToString().Split(new string[] { ", " }, StringSplitOptions.None)[0] : e.CellValue1.ToString();
                string second = e.CellValue2.ToString().Length == 0 ? "10000" : e.CellValue2.ToString().Length > 4 ? e.CellValue2.ToString().Split(new string[] { ", " }, StringSplitOptions.None)[0] : e.CellValue2.ToString();
                e.SortResult = int.Parse(first).CompareTo(int.Parse(second));
                e.Handled = true;//pass by the default sorting
            }
        }
    }
}
