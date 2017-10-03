using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class Statistics : Form
    {

        Main _parent;
        List<List<string>> Aggregate_Report = new List<List<string>>();
        string current_year = DateTime.Now.Year.ToString();
        string from_date = format_date(DateTime.Now.ToShortDateString());
        string to_date = format_date(DateTime.Now.ToShortDateString());
        string last_button;
        public Process process = new Process();

        public Statistics(Main form1)
        {
            InitializeComponent();
            _parent = form1;
            report_table.RowStyles.Add(new RowStyle(SizeType.AutoSize, 20F));
            report_table.Controls.Add(new Label() { Size = new Size(150, 20), Text = "           Report Name" }, 0, report_table.RowCount);
            report_table.Controls.Add(new Label() { Text = "  Value" }, 1, report_table.RowCount);
            report_table.Controls.Add(new Label() { Size = new Size(250, 20), Text = "                                                              Comment" }, 2, report_table.RowCount);
            report_table.RowCount = report_table.RowCount + 1;
            from_date_text.Text = from_date;
            to_date_text.Text = to_date;
        }

        // Add row with information
        private void add_row(string report_name, string report_value, string report_comment)
        {
            report_table.RowCount = report_table.RowCount + 1;
            report_table.RowStyles.Add(new RowStyle(SizeType.AutoSize, 20F));
            report_table.Controls.Add(new Label() { Size = new Size(150, 20), Text = report_name }, 0, report_table.RowCount - 1);
            report_table.Controls.Add(new Label() { Text = report_value }, 1, report_table.RowCount - 1);
            report_table.Controls.Add(new Label() { Size = new Size(250, 20), Text = report_comment }, 2, report_table.RowCount - 1);
        }

        private string formatted_value(string value)
        {
            if (Convert.ToInt32(value) < 10)
            {
                return "0" + value;
            }
            return value;
        }


        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            _parent.statistics_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void last7_button_Click(object sender, EventArgs e)
        {
            last_button = "last7";
            DateTime last_week = DateTime.Now.AddDays(-7);
            string date_01 = formatted_value(DateTime.Now.Month.ToString()) + "/" + formatted_value(DateTime.Now.Day.ToString()) + "/" + current_year;
            string date_02 = formatted_value(last_week.Month.ToString()) + "/" + formatted_value(last_week.Day.ToString()) + "/" + current_year;
            Aggregate_Report = _parent.Get_Aggregate_Report(date_02, date_01);
            Create_Table();
        }

        private void between_button_Click(object sender, EventArgs e)
        {
            if (!(DateTime.ParseExact(from_date, "MM/dd/yyyy", null) > DateTime.ParseExact(to_date, "MM/dd/yyyy", null)))
            {
                last_button = "between";
                Aggregate_Report = _parent.Get_Aggregate_Report(from_date, to_date);
                Create_Table();
            }
            else
            {
                AlertBox alert = new AlertBox("Invalid date entry", "");
                alert.HideButton();
                alert.Show();
            }
       } 

        private void today_button_Click(object sender, EventArgs e)
        {
            last_button = "today";
            string date_01 = formatted_value(DateTime.Now.Month.ToString()) + "/" + formatted_value(DateTime.Now.Day.ToString()) + "/" + current_year;
            Aggregate_Report = _parent.Get_Aggregate_Report(date_01, date_01);
            Create_Table();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            last_button = "yesterday";
            DateTime last_week = DateTime.Now.AddDays(-1);
            string date_02 = formatted_value(last_week.Month.ToString()) + "/" + formatted_value(last_week.Day.ToString()) + "/" + current_year;
            Aggregate_Report = _parent.Get_Aggregate_Report(date_02, date_02);
            Create_Table();
        }

        private void Create_Table()
        {
            report_table.SuspendLayout();
            while (report_table.RowCount > 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Control c = report_table.GetControlFromPosition(i, report_table.RowCount - 1);
                    report_table.Controls.Remove(c);
                }
                report_table.RowStyles.RemoveAt(report_table.RowCount-1);
                report_table.RowCount = report_table.RowCount - 1;
            }
            report_table.ResumeLayout(false);
            report_table.PerformLayout();
            report_table.AutoScroll = false; report_table.AutoScroll = true;

            foreach (List<string> report in Aggregate_Report)
            {
                if (report.Count > 3)
                {
                    for (int i = 1; i < report.Count - 1; i++)
                    {
                        if (!(report[i] == "00:00") && !(report[i] == "0:00")) add_row(report[0], report[i], report[report.Count()-1]);
                    }
                }
                else if (report.Count > 2) 
                {
                    add_row(report[0], report[1], report[2]);
                }
            }
        }

        private void from_date_picker_ValueChanged(object sender, EventArgs e)
        {
            from_date = format_date(DateTime.Parse(from_date_picker.Value.ToString()).ToShortDateString());
            from_date_text.Text = from_date;
        }

        private void to_date_picker_ValueChanged(object sender, EventArgs e)
        {
            to_date = format_date(DateTime.Parse(to_date_picker.Value.ToString()).ToShortDateString());
            to_date_text.Text = to_date;
        }

        static string format_date(string @short_date)
        {
            string final_date = "";
            string[] date = @short_date.Split(new string[] { "/" }, StringSplitOptions.None);
            foreach (string value in date)
            {
                final_date = final_date + "/";
                if (Convert.ToInt32(value) < 10)
                {
                    final_date = final_date + "0" + value;
                }
                else
                {
                    final_date = final_date + value;
                }
            }
            return final_date.Substring(1, final_date.Length - 1);
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            _parent.Refresh_Monitor_Log();
            if (Aggregate_Report.Count() > 0)
            {
                if (last_button.Length > 0)
                {
                    if (last_button == "last7")
                    {
                        DateTime last_week = DateTime.Now.AddDays(-7);
                        string date_01 = formatted_value(DateTime.Now.Month.ToString()) + "/" + formatted_value(DateTime.Now.Day.ToString()) + "/" + current_year;
                        string date_02 = formatted_value(last_week.Month.ToString()) + "/" + formatted_value(last_week.Day.ToString()) + "/" + current_year;
                        Aggregate_Report = _parent.Get_Aggregate_Report(date_02, date_01);
                    }
                    else if (last_button == "between")
                    {
                        Aggregate_Report = _parent.Get_Aggregate_Report(from_date, to_date);
                    }
                    else if (last_button == "today")
                    {
                        string date_01 = formatted_value(DateTime.Now.Month.ToString()) + "/" + formatted_value(DateTime.Now.Day.ToString()) + "/" + current_year;
                        Aggregate_Report = _parent.Get_Aggregate_Report(date_01, date_01);
                    }
                    else if (last_button == "yesterday")
                    {
                        DateTime last_week = DateTime.Now.AddDays(-1);
                        string date_02 = formatted_value(last_week.Month.ToString()) + "/" + formatted_value(last_week.Day.ToString()) + "/" + current_year;
                        Aggregate_Report = _parent.Get_Aggregate_Report(date_02, date_02);
                    }
                }
                Create_Table();
            }
        }

        private void add_report_button_Click(object sender, EventArgs e)
        {
            if (report_name_text.Text.Length > 0 && report_value_text.Text.Length > 0 && report_comment_text.Text.Length > 0)
            {
                _parent.Main_Update_Transition_Data(report_name_text.Text, report_value_text.Text + ";" + report_comment_text.Text, true);
                refresh_button.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool g = _parent.Delete_Report(delete_report_name_text.Text);
            if (!g)
            {
                AlertBox alert = new AlertBox("Report not found or cannot be deleted", "");
                alert.HideButton();
                alert.Show();
            }
            else
            {
                refresh_button.PerformClick();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"\\\\10.0.0.8\\shopdata\\DEVELOPMENT\\ROBIN\\TEST\\monitor_log.ini"); 

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"\\\\10.0.0.8\\shopdata\\DEVELOPMENT\\ROBIN\\TEST\\transitional_data.ini"); 

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Aggregate_Report.Count() > 0)
            {
                Time_Graph graph = new Time_Graph(Aggregate_Report, last_button, from_date_text.Text, to_date_text.Text);
                graph.Show();
            }
            else
            {
                AlertBox alert = new AlertBox("Graph for current report invalid", "");
                alert.HideButton();
                alert.Show();
            }

        }
        
    }
}
