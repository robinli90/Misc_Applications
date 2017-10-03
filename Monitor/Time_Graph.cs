using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class Time_Graph : Form
    {
        List<List<string>> Aggregate_Report;
        List<string> Time_Instances2 = new List<string>();
        int[] Frequency_List2 = new int[24];
        List<string> Time_Instances = new List<string>();
        int[] Frequency_List = new int[24];

        public Time_Graph(List<List<string>> report, string report_date_type, string from_date="", string to_date="")
        {
            InitializeComponent();
            Aggregate_Report = report;
            Get_Instances();
            Parse_Time_Frequencies();
            for (int i = 0; i < 24; i++)
            {
                chart1.Series["Series1"].Points.AddXY(i, Frequency_List[i]);
                chart1.Series["Series2"].Points.AddXY(i, Frequency_List2[i]);
            }
            Console.WriteLine(report_date_type);
            if (report_date_type == "last7")
            {
                report_date.Text = "(Last 7 days)";
            }
            else if (report_date_type == "between")
            {
                report_date.Text = "(Between " + from_date + " to " + to_date + ")";
            }
            else if (report_date_type == "today")
            {
                report_date.Text = "(Today)";
            }
            else if (report_date_type == "yesterday")
            {
                report_date.Text = "(Yesterday)";
            }
        }

        public void Get_Instances()
        {
            foreach (List<string> report in Aggregate_Report)
            {
                if (report.Count > 3)
                {
                    for (int i = 1; i < report.Count - 2; i++)
                    {
                        if (!(report[i] == "00:00") && report[0] == "PING_SPIKE") Time_Instances.Add(report[i]);
                        if (!(report[i] == "00:00") && report[0] == "LDATA_QUEUE_ERROR") Time_Instances2.Add(report[i]);
                    }
                }
            }
        }

        public void Parse_Time_Frequencies()
        {
            string hour = "";
            for (int i = 0; i < 24; i++)
            {
                Frequency_List[i] = 1; // Reset base int
            }
            foreach (string time in Time_Instances)
            {
                hour = time.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                Frequency_List[Convert.ToInt32(hour)] = Frequency_List[Convert.ToInt32(hour)] + 1;
            }
            for (int i = 0; i < 24; i++)
            {
                Frequency_List2[i] = 1; // Reset base int
            }
            foreach (string time in Time_Instances2)
            {
                hour = time.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                Frequency_List2[Convert.ToInt32(hour)] = Frequency_List2[Convert.ToInt32(hour)] + 1;
            }
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dispose();
            this.Close();
        }
    }
}
