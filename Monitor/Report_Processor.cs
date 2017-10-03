using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;

namespace Monitor
{

    class Report_Processor
    {
        static Dictionary<string, List<string>> Reports = new Dictionary<string, List<string>>();
        List<List<string>> Aggregate_Report = new List<List<string>>();
        private string config_path = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\test\\transitional_data.INI";
        private string monitor_log_path = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\test\\monitor_log.INI";
        private string[] report_exceptions = {"PING_SPIKE", "LDATA_QUEUE_ERROR", "PROCESSOR_ON_HOLD", "PROCESSOR_TASK_TRACKING", "PROCESSOR_TURN_CHECKER", "PROCESSOR_CAD_PRINT", "PROCESSOR_BOLSTER_AUTOLATHE", "DATE"};

        // Get current values from TRANSITIONAL DATA file and store in reports
        public void Process_Transitional_Data()
        {
            try
            {
                Reports.Clear();
                var text = File.ReadAllText(config_path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Length > 1)
                    {
                        string[] entries = line.Split(new string[] { "=" }, StringSplitOptions.None);
                        List<string> list;
                        Reports.Add(entries[0].ToString().Trim(), list = new List<string>());
                        list = Reports[entries[0].ToString().Trim()];
                        if (!(entries[1].Contains("|"))) // If does not contain multiple entries
                        {
                            list.Add(entries[1].ToString().Trim());
                        }
                        else
                        {
                            string[] comments = entries[1].Split(new string[] { "|" }, StringSplitOptions.None);
                            foreach (string comment in comments)
                            {
                                list.Add(comment.ToString().Trim());
                            }
                        }
                        Reports[entries[0]] = list;
                    }
                }

                // Overwrite original date to now
                List<string> date_list = new List<string>();
                date_list.Add(DateTime.Now.ToShortDateString());
                Reports["DATE"] = date_list;
            }
            catch
            {
                Process_Transitional_Data();
            }
        }

        // Rewrite FILE Transitional Data based on current Reports data
        public void Write_Transitional_Data()
        {
            File.Delete(config_path);
            string line = "";
            foreach (KeyValuePair<string, List<string>> pair2 in Reports)
            {
                int i = 0;
                line = line + pair2.Key.ToString() + "=";
                foreach (string element in pair2.Value)
                {
                    i++;
                    if (i < pair2.Value.Count())
                    {
                        line = line + element.ToString() + "|";
                    }
                    else
                    {
                        line = line + element.ToString();
                    }
                }
                line = line + "\r\n";
            }

            if (!File.Exists(config_path))
            {
                using (StreamWriter sw = File.CreateText(config_path)) // Create LOG file
                {
                    sw.Write(line);
                    sw.Close();
                }
            }
        }

        public bool Contains_Date(string date)
        {
            var text = File.ReadAllText(monitor_log_path);
            return text.Contains(date);
        }

        public string Get_Date()
        {
            return Reports["DATE"][0];
        }

        // Add transitional data to Reports, ##DOES NOT UPDATE FILE (overload OVERWRITE)
        public void Add_Transitional_Data(string report_name, List<string> info, bool overwrite=false)
        {
            List<string> list;
            if (!Reports.TryGetValue(report_name.ToString().Trim(), out list))
            {
                Reports.Add(report_name, list = new List<string>());
                list = Reports[report_name];
                Reports[report_name] = info;
            }
            else
            {
                list = Reports[report_name];
                if (!overwrite)
                {
                    foreach (string item in info)
                    {
                        list.Add(item);
                    }
                    Reports[report_name] = list;
                }
                else
                {
                    Reports[report_name] = info;
                }
            }
        }

        // Transfer Transitional Data to main Log  ##### TRANSITIONAL_DATA ---> MONITOR_LOG
        public void Log_update()
        {
            Process_Transitional_Data();
            var text = File.ReadAllText(monitor_log_path);
            string log_lines = "";
            // If current date is not in log, just append transitional data to it
            string current_date = "DATE=" + DateTime.Now.ToShortDateString();
            if (!text.Contains(current_date))
            {
                string line = "";
                foreach (KeyValuePair<string, List<string>> pair2 in Reports)
                {
                    int i = 0;
                    line = line + pair2.Key.ToString() + "=";
                    foreach (string element in pair2.Value)
                    {
                        i++;
                        if (i < pair2.Value.Count())
                        {
                            line = line + element.ToString() + "|";
                        }
                        else
                        {
                            line = line + element.ToString();
                        }
                    }
                    line = line + "~";
                }
                File.AppendAllText(monitor_log_path, line.Substring(0, line.Length - 1) + "\r\n");
            }
            else
            {
                File.Delete(monitor_log_path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Length > 1)
                    {
                        if (!line.Contains(current_date))
                        {
                            log_lines = log_lines + line + "\r\n";
                        }
                    }
                }
                using (StreamWriter sw = File.CreateText(monitor_log_path)) // Create LOG file
                {
                    sw.Write(log_lines);
                    sw.Close();
                }
                Log_update();
            }
        }

        public void Clear_Report_Entries()
        {
            List<string> @list = new List<string>();
            @list.Add("0");
            List<string> @list2 = new List<string>();
            @list2.Add("");
            Add_Transitional_Data("PROCESSOR_BOLSTER_AUTOLATHE", @list, true);
            Add_Transitional_Data("PROCESSOR_CAD_PRINT", @list, true);
            Add_Transitional_Data("PROCESSOR_TURN_CHECKER", @list, true);
            Add_Transitional_Data("PROCESSOR_TASK_TRACKING", @list, true);
            Add_Transitional_Data("PROCESSOR_ON_HOLD", @list, true);
            Add_Transitional_Data("LDATA_QUEUE_ERROR", @list2, true);
            Add_Transitional_Data("PING_SPIKE", @list2, true);
            Write_Transitional_Data();
        }

        // Return the list from Reports
        public List<string> Get_Info(string report_name, string identifier="")
        {
            return Reports[(report_name + identifier)];
        }

        public int Get_Single_Value(string report_name)
        {
            return Convert.ToInt32(Reports[report_name][0]);
        }
        
        private string Get_Single_Value_v2(string report_name, string entire_report)
        {
            string[] date_step1 = entire_report.Split(new string[] { "~" }, StringSplitOptions.None);
            foreach (string entry in date_step1)
            {
                string[] date_step2 = entry.Split(new string[] { "=" }, StringSplitOptions.None);
                if (date_step2[0] == report_name)
                {
                    return date_step2[1];
                }
            }
            return "";
        }

        public bool Delete_Report(string report_name)
        {
            var text = File.ReadAllText(config_path);
            Process_Transitional_Data();
            if (text.Contains(report_name) && !report_exceptions.Contains(report_name))
            {
                Reports.Remove(report_name);
                Write_Transitional_Data();
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<string> Parse_times(string @str, string @first_element, string @memo="")
        {
            bool time_found = false;
            List<string> Times = new List<string>();
            string time = "";
            Times.Add(@first_element);
            foreach (char c in @str)
            {
                if (c.ToString() == "[")
                {
                    time_found = true;
                    time = "";
                }
                else if (c.ToString() == "]")
                {
                    Times.Add(time);
                    time_found = false;

                }
                else if (time_found)
                {
                    time = time + c.ToString();
                }
            }
            Times.Add(@memo);
            return Times;
        }
                    

        // Summarize/Aggregate Data between two dates (## MAIN DATA ONLY, MISC STATS FROM LAST DATE ONLY ##)
        public List<List<string>> Summarize_Data(string fromdate, string todate)
        {
            Aggregate_Report.Clear();

            List<string> g1 = new List<string>();
            DateTime from_datetime_date = DateTime.ParseExact(fromdate, "MM/dd/yyyy", null);
            DateTime to_datetime_date = DateTime.ParseExact(todate, "MM/dd/yyyy", null);
            DateTime latest_datetime_date = from_datetime_date;
            int processor_bolster_autolathe = 0;
            int processor_cad_print = 0;
            int processor_turn_checker = 0;
            int processor_task_tracking = 0;
            int processor_on_hold = 0;
            string ldata_queue_error = ""; //= new List<string>();
            string ping_spike = ""; //new List<string>();
            

            var text = File.ReadAllText(monitor_log_path); 
            string[] aggregate_log = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string daily_report in aggregate_log)
            {
                if (daily_report.Length > 1)
                {
                    string[] date_step1 = daily_report.Split(new string[] { "~" }, StringSplitOptions.None);
                    string[] date_step3 = (date_step1[0].Split(new string[] { "=" }, StringSplitOptions.None)[1]).Split(new string[] { "/" }, StringSplitOptions.None);
                    if (date_step3[0].Length == 1) date_step3[0] = "0" + date_step3[0];
                    if (date_step3[1].Length == 1) date_step3[1] = "0" + date_step3[1];
                    string current_date = date_step3[0] + "/" + date_step3[1] + "/" + date_step3[2];
                    DateTime current_datetime_date = DateTime.ParseExact(current_date, "MM/dd/yyyy", null);

                    if (from_datetime_date <= current_datetime_date && to_datetime_date >= current_datetime_date)
                    {
                        if (current_datetime_date > latest_datetime_date)
                        {
                            latest_datetime_date = current_datetime_date;
                        }
                        processor_bolster_autolathe = processor_bolster_autolathe + Convert.ToInt32(Get_Single_Value_v2("PROCESSOR_BOLSTER_AUTOLATHE", daily_report));
                        processor_cad_print = processor_cad_print + Convert.ToInt32(Get_Single_Value_v2("PROCESSOR_CAD_PRINT", daily_report));
                        processor_turn_checker = processor_turn_checker + Convert.ToInt32(Get_Single_Value_v2("PROCESSOR_TURN_CHECKER", daily_report));
                        processor_task_tracking = processor_task_tracking + Convert.ToInt32(Get_Single_Value_v2("PROCESSOR_TASK_TRACKING", daily_report));
                        processor_on_hold = processor_on_hold + Convert.ToInt32(Get_Single_Value_v2("PROCESSOR_ON_HOLD", daily_report));
                        ldata_queue_error = ldata_queue_error + Get_Single_Value_v2("LDATA_QUEUE_ERROR", daily_report);
                        ping_spike = ping_spike + Get_Single_Value_v2("PING_SPIKE", daily_report);

                        
                    }
                }
            }

            g1.Add("BOLSTER_AUTOLATHE"); g1.Add(processor_bolster_autolathe.ToString()); g1.Add("Bolster Autolathe total count"); Aggregate_Report.Add(g1); g1 = new List<string>();
            g1.Add("CAD_PRINT"); g1.Add(processor_cad_print.ToString()); g1.Add("Cad Print processed total"); Aggregate_Report.Add(g1); g1 = new List<string>();
            g1.Add("TURN_CHECKER"); g1.Add(processor_turn_checker.ToString()); g1.Add("Number of files analyzed"); Aggregate_Report.Add(g1); g1 = new List<string>();
            g1.Add("TASK_TRACKING"); g1.Add(processor_task_tracking.ToString()); g1.Add("Number of legacy tracking method count"); Aggregate_Report.Add(g1); g1 = new List<string>();
            g1.Add("ON_HOLD"); g1.Add(processor_on_hold.ToString()); g1.Add("Number of files put on hold"); Aggregate_Report.Add(g1); g1 = new List<string>();
            Aggregate_Report.Add(Parse_times(ldata_queue_error + "|", "LDATA_QUEUE_ERROR", "Time of LDATA folder overflow"));
            Aggregate_Report.Add(Parse_times(ping_spike + "|", "PING_SPIKE", "Time of 10.0.0.8 ping spike"));


            foreach (string daily_report in aggregate_log)
            {
                if (daily_report.Length > 1)
                {
                    string[] date_step1 = daily_report.Split(new string[] { "~" }, StringSplitOptions.None);
                    string[] date_step3 = (date_step1[0].Split(new string[] { "=" }, StringSplitOptions.None)[1]).Split(new string[] { "/" }, StringSplitOptions.None);
                    if (date_step3[0].Length == 1) date_step3[0] = "0" + date_step3[0];
                    if (date_step3[1].Length == 1) date_step3[1] = "0" + date_step3[1];
                    string current_date = date_step3[0] + "/" + date_step3[1] + "/" + date_step3[2];
                    DateTime current_datetime_date = DateTime.ParseExact(current_date, "MM/dd/yyyy", null);
                    if (current_datetime_date == latest_datetime_date)
                    {
                        foreach (string reports in date_step1)
                        {
                            date_step3 = reports.Split(new string[] { "=" }, StringSplitOptions.None);
                            if (!(report_exceptions.Contains(date_step3[0])))
                            {
                                if (date_step3[1].Contains(";"))
                                {
                                    string[] temp = date_step3[1].Split(new string[] { ";" }, StringSplitOptions.None);
                                    g1.Add(date_step3[0]); g1.Add(temp[0]); g1.Add(temp[1]); Aggregate_Report.Add(g1); g1 = new List<string>();
                                }
                                else
                                {
                                    g1.Add(date_step3[0]); g1.Add(date_step3[1]); g1.Add(""); Aggregate_Report.Add(g1); g1 = new List<string>();
                                }
                            }
                        }
                    }
                }
            }
            /*
            Console.WriteLine("###################################");
            foreach (List<string> report in Aggregate_Report)
            {
                foreach (string g in report)
                {
                    Console.Write(g);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("###################################");
            */
            return Aggregate_Report;    
        }
    }
}
