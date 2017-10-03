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
    public partial class Action_Log : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            string log_str = "";
            foreach (string g in log)
            {
                log_str = log_str + g + "▄";
            }
            _parent._STORE_SETUP_INFORMATION(2, log_str.Trim(Convert.ToChar("▄")));

            _parent._Action_Log_Open = false;
            _parent._STORE_COMPUTER_INFO();
            _parent.Focus();
            //this.Close();
            this.Dispose();

        }

        Office _parent;
        string _EMP_NUM = "";

        List<string> log = new List<string>();

        public Action_Log(Office parent, string transaction_log, string emp_num)
        {

            InitializeComponent();
            _parent = parent;
            _EMP_NUM = emp_num;

            for (int i = 0; i < log_grid.ColumnCount; i++)
            {
                log_grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                log_grid.Columns[i].ReadOnly = true;
            }

            log = transaction_log.Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
            for (int i = log.Count - 1; i >= 0; i--)
            {
                if (log[i].Contains("~") || log[i].Length < 1) log.RemoveAt(i);
            }
            log.Reverse();

            // Sort list by date using LAMBDA ENUMERATION (desc)
            //log.Sort((y, x) => DateTime.Compare(
            //    Convert.ToDateTime(x.Split(new string[] { "] :" }, StringSplitOptions.None)[0].Substring(1)),
            //    Convert.ToDateTime(y.Split(new string[] { "] :" }, StringSplitOptions.None)[0].Substring(1))));

            log = Remove_Rudandant(log);

            Populate_Grid_View();
            log_grid.DefaultCellStyle.SelectionBackColor = log_grid.DefaultCellStyle.BackColor;
            log_grid.DefaultCellStyle.SelectionForeColor = log_grid.DefaultCellStyle.ForeColor;
            if (_parent._ENABLE_ADMINISTRATIVE_COMMANDS) clear_log_button.Visible = true;

            if (clean_log(90, false) == 0)
            {
                clear_90_days.Visible = false;
            }

        }


        public List<string> Remove_Rudandant(List<string> input)
        {
            for (int i = input.Count() - 1; i > 0; i--)
            {
                if (input[i] == input[i - 1])
                {
                    input.RemoveAt(i);
                    //i--;
                }
            }
            return input;
        }


        private void Populate_Grid_View()
        {
            int count = 0;
            log_grid.Rows.Clear();
            
            foreach (string g in log)
            {
                if (g.Length > 5)
                {
                    if (g.Contains("USER-LOGIN") || g.Contains("USER-LOGOFF"))
                    {
                        if (_parent._ENABLE_ADMINISTRATIVE_COMMANDS)
                        {
                            string[] temp = g.Split(new string[] { "] : " }, StringSplitOptions.None);
                            log_grid.Rows.Add(temp[0].Trim(Convert.ToChar("[")).Trim(Convert.ToChar("]")), show_id.Checked ? temp[1] : temp[1].Substring(0, temp[1].Length - 16));
                            count++;
                        }
                    }
                    else if (search_box.Text.Length > 0 ? g.ToLower().Contains(search_box.Text.ToLower()) : true)
                    {
                        string[] temp = g.Split(new string[] { "] : " }, StringSplitOptions.None);
                        log_grid.Rows.Add(temp[0].Trim(Convert.ToChar("[")).Trim(Convert.ToChar("]")), show_id.Checked ? temp[1] : temp[1].Substring(0, temp[1].Length - 16));
                        count++;
                    }
                }
            }
            search_count_box.Text = count.ToString() + " item(s) found";
            search_count_box.ForeColor = count == 0 ? Color.Red : Color.Green;
        }

        private void clear_log_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (search_box.Text.Length == 9)
                {
                    Convert.ToInt32(search_box.Text.Length);
                    for (int i = log.Count() - 1; i >= 0; i--)
                    {
                        if (log[i].Contains("SSID:" + search_box.Text))
                        {
                            log.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    if (search_box.Text.ToLower() != "iamadmin")
                    {
                        for (int i = log.Count() - 1; i >= 0; i--)
                        {
                            if (log[i].Contains("USER-") && log[i].Contains("-LOG"))
                            {
                                log.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        log = new List<string>();
                    }
                    _parent._STORE_SETUP_INFORMATION(2, "");
                    _parent._STORE_COMPUTER_INFO();
                    log_grid.Rows.Clear();
                    Populate_Grid_View();
                }
            }
            catch
            {
                if (search_box.Text.ToLower() != "iamadmin")
                {
                    for (int i = log.Count() - 1; i >= 0; i--)
                    {
                        if (log[i].Contains("USER-") && log[i].Contains("-LOG"))
                        {
                            log.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    log = new List<string>();
                }
                _parent._STORE_SETUP_INFORMATION(2, "");
                _parent._STORE_COMPUTER_INFO();
                log_grid.Rows.Clear();
                Populate_Grid_View(); ;
            }
            //Populate_Grid_View();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            if (search_box.Text.Length == 0)
            {
                search_count_box.Visible = false;
                Populate_Grid_View();
            }
            else if (search_box.Text.ToLower() == "iamadmin")
            {
                search_box.ForeColor = Color.White;
            }
            else
            {
                Populate_Grid_View();
                search_count_box.Visible = true;
            }
        }

        /// <summary>
        /// Remove everything but past 30 days
        /// </summary>
        private int clean_log(int days = 30, bool clean=true)
        {
            if (log.Count > 0)
            {
                int items = 0;
                List<string> ref_day_list = new List<string>();
                List<string> temp = new List<string>();
                for (int i = 0; i < days; i++)
                {
                    ref_day_list.Add(DateTime.Now.AddDays(-i).ToShortDateString());
                }
                foreach (string g in log)
                {
                    string date = g.Split(new string[] { " " }, StringSplitOptions.None)[0];
                    if (ref_day_list.Contains(date.Substring(1)))
                    {
                        //Console.WriteLine(date);
                        temp.Add(g);
                    }
                    else
                    {
                        items++;
                    }
                }
                log = temp;
                //log.Reverse();
                // save in main file
                string log_str = "";
                foreach (string g in log)
                {
                    log_str = log_str + g + "▄";
                }

                if (clean)
                {
                    _parent._STORE_SETUP_INFORMATION(2, log_str.Trim(Convert.ToChar("▄")));
                    _parent._STORE_COMPUTER_INFO();
                    log_grid.Rows.Clear();
                    Populate_Grid_View();
                }
                return items;
            }
            else
            {
                return 0;
            }
        }

        private void clear_90_days_Click(object sender, EventArgs e)
        {
            clean_log(30);
            clear_90_days.Visible = false;
        }

        private void show_id_CheckedChanged(object sender, EventArgs e)
        {
            Populate_Grid_View();
        }

        private void log_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {

            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                //Clipboard.SetText(temp.Substring(temp.IndexOf("(SSID:") + 6, 9));
                if (show_id.Checked)
                {
                    string temp = log_grid[1, e.RowIndex].Value.ToString();
                    Clipboard.SetText(temp.Substring(temp.IndexOf("(SSID:") + 6, 9));
                }
            }
        }
    }
}
