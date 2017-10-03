using System;
using Databases;
using System.Data.Odbc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
//using System.Net.NetworkInformation;
using System.Net;
using System.Drawing.Printing;

namespace CNC_Schedule
{

    public partial class CNC_Form : Form
    {

        // Print screen capture BITMAP
        //Bitmap memoryImage;

        // Update timer
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();

        // Master list to store all the dies into one location
        // Indices:              0          1        2         3     4      5        6       7         8             9               10        11       12              13                  14             15           16         17         18          19             20            21          22      
        // Column name:      ordernumber, suffix, fastrack, onhold, ncr, shopdate, sales, duedate, diameter, prefix(hollow/solid), onLathe, cncReady, cncDone, lhDone(lifting-hole), lastTaskStation, isMisPunch, onCNCMachine, toDoList, doneLathe, beenBypassed, assignedName, machineEMPname, isPanic
        private List<List<string>> Master_Die_List = new List<List<string>>();

        // Sorted list
        private List<List<string>> ToDo_Die_List = new List<List<string>>();
        private List<List<string>> OnMachine_Die_List = new List<List<string>>();
        private List<List<string>> MisPunched_Die_List = new List<List<string>>();

        //                                                                                   Key: ordernumber                 0        1        2            3
        // List of all tasks per die number stored in dictionary <SO NUMBER, List<List<string>> where it contains a List of [task, tasktime, station, employeenumber]>
        static Dictionary<string, List<List<string>>> Die_Tasks_List = new Dictionary<string, List<List<string>>>();

        // All shop orders from Master_Die_List stored here
        static List<string> All_Die_Order_Numbers = new List<string>();

        // All buttons that have been pushed already (checked shoptrack)
        static Dictionary<string, string> Viewed_Orders = new Dictionary<string, string>();

        // Set Part type for analysis
        private static string Part_Type = "";

        // Lifting hole file path
        private string Lifting_Hole_Path = "\\\\excotrack2\\Shopdata\\LiftingHoles\\";
        private string Lifting_Hole_Path2 = "\\\\10.0.0.8\\Shopdata\\LiftingHoles\\";

        // CNC remote shutoff
        private string Remote_Shutoff_Path = "\\\\excotrack2\\Shopdata\\Development\\Robin\\CNC_Config.txt";
        private string Remote_Shutoff_Path2 = "\\\\10.0.0.8\\Shopdata\\Development\\Robin\\CNC_Config.txt";

        // Employee dictionary for finding name by employee number  <employee number, employee name>
        private Dictionary<string, string> Employee_List = new Dictionary<string, string>();

        // Plate Machine List
        private static string[] Plate_Machine = {"MC27", "MC28", "MC29", "MC30", "MC31", "MC32", "MC33", "MC34"};
        
        // Mandrel Machine List
        private static string[] Mandrel_Machine = { "MC20", "MC21", "MC25", "MC26", "MC35", "MC36", "MC40", "MC43" };

        // Active Plate Machines 
        private List<string> Active_Plate_Machines = new List<string>();
        // Active Mandrel Machines 
        private List<string> Active_Mandrel_Machines = new List<string>();

        // Variable to prevent multiple pages opening at once (with same order number)
        private string Last_Button_Pressed = "";

        // Master query for debug
        private string Master_query = "";

        private Debug DBG = new Debug();

        // Counting variables
        private int toDo_Solid_Count = 0;
        private int toDo_Hollow_Count = 0;
        private int onMachine_Solid_Count = 0;
        private int onMachine_Hollow_Count = 0;
        private int misPunch_Solid_Count = 0;
        private int misPunch_Hollow_Count = 0;


        public CNC_Form()
        {
            InitializeComponent();

            if (!(File.Exists(Remote_Shutoff_Path)) && !(File.Exists(Remote_Shutoff_Path2)))
            {
                MessageBox.Show("CNC_Config.txt missing. Please create a new text file named 'CNC_Config.txt' to continue");
                this.Dispose();
                this.Close();
                Environment.Exit(0);
            }

            Part_Type = "P";
            this.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);

            // Required tasks
            Get_Employee_List();
            Get_Master_Die_List();

            // Filter Master Die List using the below functions
            Remove_Scrap_Entries();
            Remove_Dies_Past_HT();
            Adjust_Due_Dates();
            Check_lhDone_Status();
            Check_Lathe_Status();
            Check_OnLathe_Status();
            Check_CNCDone_Status();
            Check_OnMachine_Jobs();
            Get_Last_Station();
            Analyze_Bypassed_Jobs();
            Check_Bypassed_Jobs();
            Sort_Dies();

            tick_update.Interval = 120000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(Update);

            toDo_Create_Table();
            onMachine_Create_Table();
            misPunch_Create_Table();
            machineStatus_Create_Table();
            plate_button.BackColor = Color.Green;

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        // Main Update function
        private void Update(object sender, EventArgs e)
        {
            if (!(File.Exists(Remote_Shutoff_Path)) && !(File.Exists(Remote_Shutoff_Path2)))
            {
                this.Dispose();
                this.Close();
                Environment.Exit(0);
            }
            Refresh_Tables();
        }

        // Refresh all tables
        public void Refresh_Tables()
        {
            // Required tasks
            Reset_Variables();
            Get_Master_Die_List();

            // Filter Master Die List using the below functions
            Remove_Scrap_Entries();
            Remove_Dies_Past_HT(); // Heat-treat removal
            Adjust_Due_Dates();
            Check_lhDone_Status();
            Check_Lathe_Status();
            Check_OnLathe_Status();
            Check_CNCDone_Status();
            Check_OnMachine_Jobs();
            Get_Last_Station();
            Analyze_Bypassed_Jobs();
            Check_Bypassed_Jobs();
            Sort_Dies();

            toDo_Create_Table();
            onMachine_Create_Table();
            misPunch_Create_Table();
            machineStatus_Create_Table();

            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        // Reset all variables for refreshing
        private void Reset_Variables()
        {
            Master_Die_List = new List<List<string>>();
            ToDo_Die_List = new List<List<string>>();
            OnMachine_Die_List = new List<List<string>>();
            MisPunched_Die_List = new List<List<string>>();
            Die_Tasks_List = new Dictionary<string, List<List<string>>>();
            All_Die_Order_Numbers = new List<string>();

            Active_Plate_Machines = new List<string>();
            Active_Mandrel_Machines = new List<string>();


            toDo_Solid_Count = 0;
            toDo_Hollow_Count = 0;
            onMachine_Solid_Count = 0;
            onMachine_Hollow_Count = 0;
            misPunch_Solid_Count = 0;
            misPunch_Hollow_Count = 0;
        }

        // Store list of employees in dictionary
        private void Get_Employee_List()
        {
            string employee_query = "select employeenumber, firstname from d_user";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(employee_query);

            while (reader.Read())
            {
                Employee_List.Add(reader[0].ToString().Trim(), reader[1].ToString().Trim());
            }
            reader.Close();

            foreach (KeyValuePair<string, string> kvpPair in Employee_List)
            {
                if (kvpPair.Value.ToLower() == "feng")
                {
                    Employee_List[kvpPair.Key] = "Naresh";
                    break;
                }
            }
        }

        // Round diameter input
        private double Round_Diameter(double dia)
        {
            if (dia>=5 && dia<=5.2)
                dia = 5;
	        else if (dia>5.2 && dia<=6.6 )
                dia = 6.5;
	        else if (dia>6.6 && dia<=7.2 )
                dia = 7;
	        else if (dia>7.2 && dia<=8.2 )
                dia = 8;
	        else if (dia>8.2 && dia<=9.2 )
                dia = 9;
	        else if (dia>9.2 && dia<=10.2 )
                dia = 10;
	        else if (dia>10.2 && dia<=11.2 )
                dia = 11;
	        else if (dia>11.2 && dia<=12.2 )
                dia = 12;
	        else if (dia>12.2 && dia<=13.2 )
                dia = 13;
	        else if (dia>13.2 && dia<=14.2 )
                dia = 14;
	        else if (dia>14.2 && dia<=15.2 )
                dia = 15;
	        else if (dia>15.2 && dia<=16.2 )
                dia = 16;
	        else if (dia>16.2 && dia<=18.2 )
                dia = 18;
	        else if (dia>18.2 && dia<=19.2 )
                dia = 19;
	        else if (dia>19.2 && dia<=20.2 )
                dia = 20;
	        else if (dia>20.2 && dia<=22.2 )
                dia = 22;
            return dia;
        }

        // Parse diameter from suffix string
        private double Get_Diameter(string suffix)
        {
            if (suffix.Contains("0"))
            {
                return Round_Diameter(Convert.ToDouble(suffix.Substring(2, 5)) > 100 ? Convert.ToDouble(suffix.Substring(2, 5)) / 25.4 : Convert.ToDouble(suffix.Substring(2, 5)));
            }
            else
            {
                return 0;
            }
        }

        // Return the index of the die number in List array position given die SO number
        public int Get_Die_Master_Index(string SO_Number)
        {
            if (Master_Die_List.Count > 0)
            {
                foreach (List<string> Die_Order in Master_Die_List)
                {
                    if (Die_Order[0] == SO_Number)
                    {
                        return Master_Die_List.IndexOf(Die_Order);
                    }
                }
            }
            return -1;
        }

        // Get all information necessary for filtering later on. This master list contains all jobs within the timeframe
        private void Get_Master_Die_List()
        {

            #region DIES: Get all jobs within a single month centered on today's date

            ExcoDateTime DTE = new ExcoDateTime();
            string from_date = DTE.AddWorkdays(DateTime.Now, 10, -1).ToString("yyyy-MM-dd HH:mm:ss");
            string to_date = DTE.AddWorkdays(DateTime.Now, 16).ToString("yyyy-MM-dd HH:mm:ss");
            List<string> Order_Number_List = new List<string>();

            string master_query = "SELECT DISTINCT d_orderitem.ordernumber, suffix, fasttrack, onhold, ncr, shopdate, sales, " + 
                                  "case when prefix like 'h%' " +
                                  "then cast((datediff(hh, getdate(), shopdate) + 12) as float) / cast (24 as float) " + 
                                  "else cast((datediff(hh, getdate(), shopdate) + 24) as float) / cast (24 as float) " +
                                  "end as duedate, prefix FROM d_orderitem, d_order " +
                                  "WHERE invoicedate IS NULL AND shipdate IS NULL " + "AND shopdate >= '" + from_date + "' and shopdate < '" + to_date + "' " +  
                                  "AND d_order.ordernumber = d_orderitem.ordernumber " +
                                  "AND (EXISTS (SELECT DISTINCT * from d_task " +
                                  "WHERE d_task.ordernumber = d_order.ordernumber AND d_task.task = 'CM' " +
                                  "AND d_task.part like '" + Part_Type + "') " +
                                  "OR EXISTS (SELECT * from d_returns " +
                                  "WHERE d_returns.ordernumber = d_order.ordernumber AND d_returns.plt = 1 " +
                                  "AND d_returns.pltcode & 128 <> 0)) " +
                                  "AND (prefix = 'DI' or prefix = 'HO') AND dienumber is not NULL and len(suffix) > 5 " +
                                  "ORDER BY duedate, d_order.shopdate, fasttrack desc, d_orderitem.ordernumber";

            Master_query = master_query;
            //MessageBox.Show(master_query);
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(master_query);

            // Store all appropriate data in master list
            while (reader.Read())
            {
                //ordernumber, suffix, fastrack, onhold, ncr, shopdate, sales, duedate, diameter, prefix(hollow/solid), doneLathe, cncReady
                List<string> temp = new List<string>();
                temp.Add(reader[0].ToString().Trim()); //ordernumber [0]
                temp.Add(reader[1].ToString().Trim()); //suffix [1]
                temp.Add(reader[2].ToString().Trim()); //fastrack [2]
                temp.Add(reader[3].ToString().Trim()); //onhold [3]
                temp.Add(reader[4].ToString().Trim()); //ncr [4]
                temp.Add(reader[5].ToString().Trim()); //shopdate [5]
                temp.Add(reader[6].ToString().Trim()); //sales [6]
                temp.Add(reader[7].ToString().Trim()); //duedate [7]
                temp.Add(Get_Diameter(reader[1].ToString()).ToString()); //diameter [8]
                //prefix(hollow/solid) [9]
                if (reader[8].ToString().Trim().StartsWith("H") || reader[8].ToString().Trim().StartsWith("h"))
                {
                    temp.Add("H"); //Hollow die
                }
                else if (reader[8].ToString().Trim().StartsWith("D") || reader[8].ToString().Trim().StartsWith("P") ||
                         reader[8].ToString().Trim().StartsWith("d") || reader[8].ToString().Trim().StartsWith("p"))
                {
                    temp.Add("S"); //Solid die
                }
                else
                {
                    temp.Add("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"); // No die
                }
                temp.Add("false"); //onLathe [10]
                temp.Add("false"); //cncReady [11]
                temp.Add("false"); //cncDone [12]
                temp.Add("false"); //lhDone [13]
                temp.Add("false"); //lastTaskStation [14]
                temp.Add("false"); //isMisPunch [15]
                temp.Add("false"); //onCNCMachine [16]
                temp.Add("false"); //toDoList [17]
                temp.Add("false"); //doneLathe [18]
                temp.Add("false"); //beenBypassed [19]
                temp.Add(""); //assignedName [20]
                temp.Add(""); //machineEMPName [21]
                temp.Add(""); //isPanic [22]

                // If not on hold, then add to list
                if (!(temp[3] ==  "true"))
                {
                    Master_Die_List.Add(temp);
                    Order_Number_List.Add(reader[0].ToString().Trim());
                }
            }

            reader.Close();

            #endregion

            #region TASKS: Store all tasks according to shop order numbers listed above

            // Store temp list with master die order list
            All_Die_Order_Numbers = Order_Number_List;

            // Get all the appropriate orders for this list instead of querying every list x number of times
            string order_numbers_str = "";

            foreach (string SO in Order_Number_List)
            {
                order_numbers_str = order_numbers_str + "ordernumber = '" + SO + "' or ";
            }
            string sub_part_query = "select ordernumber, task, tasktime, station, employeenumber from d_task where (" + order_numbers_str.Substring(0, order_numbers_str.Length - 3 ) + ") and part like '" + Part_Type.ToUpper() + "' order by ordernumber, tasktime";
            
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(sub_part_query);
            string temp_order_number = "";

            // Store all appropriate data in master list
            while (reader.Read())
            {
                if (!(reader[0].ToString().Trim() == temp_order_number))
                {
                    temp_order_number = reader[0].ToString().Trim();
                }

                List<string> temp = new List<string>();

                temp.Add(reader[1].ToString().Trim());
                temp.Add(reader[2].ToString().Trim());
                temp.Add(reader[3].ToString().Trim());
                temp.Add(reader[4].ToString().Trim());

                List<List<string>> list;

                if (!Die_Tasks_List.TryGetValue(temp_order_number, out list)) // If order number is not in dictionary, add key to report
                {
                    Die_Tasks_List.Add(temp_order_number, list = new List<List<string>>());
                    list.Add(temp);
                    Die_Tasks_List[temp_order_number] = list;
                }
                else // If it is, append task to list of tasks
                {
                    Die_Tasks_List[temp_order_number].Add(temp);
                }
            }

            reader.Close();
            #endregion

            #region ASSIGNMENT: Get Assigned Die data

            foreach (string SO in Order_Number_List)
            {
                order_numbers_str = order_numbers_str + "ordernumber = '" + SO + "' or ";
            }
            string assign_query = "select ordernumber, employeenumber, stage, part, panic from d_cadcamjobassignlist where (" + order_numbers_str.Substring(0, order_numbers_str.Length - 3 ) + ") and stage like 'CNC' and part like '" + Part_Type + "'";
            
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(assign_query);

            // Store all appropriate data in master list
            string name;

            while (reader.Read())
            {
                try
                {
                    name = Employee_List[reader[1].ToString().Trim()];
                }
                catch
                {
                    name = "Unknown";
                }
                Master_Die_List[Get_Die_Master_Index(reader[0].ToString().Trim())][20] = name;
                if (reader[4].ToString().Trim() == "1")
                    Master_Die_List[Get_Die_Master_Index(reader[0].ToString().Trim())][22] = "1";
            }
            reader.Close();
            #endregion

        }

        // Get the task code or (other information depending on value of index: 0 = task, 1 = tasktime, 2 = station, 3 = employeenumber)
        // IF STRING RETURN LENGTH > 0, TASK EXISTS.... ONLY RETURNS THE FIRST INSTANCE OF TASK CODE
        private string Check_Task_Exist(string order_number, string task_code, int check_index=0, int return_index=0)
        {
            string _task_code = task_code.ToUpper();
            try
            {
                foreach (List<string> tasks in Die_Tasks_List[order_number])
                {
                    if (tasks[check_index].ToUpper() == _task_code)
                    {
                        return tasks[return_index];
                    }
                }
            }
            catch
            {
                // No tasks available for job
            }
            return "";
        }

        // Return the value of the last task found (index_from_end is exactly as it sounds, by default 1 = last index, 2 = second last, ...)
        private string Get_Last_Task(string order_number, int return_index, int index_from_end=1)
        {
            try
            {
                return Die_Tasks_List[order_number][Die_Tasks_List[order_number].Count - (index_from_end)][return_index];
            }
            catch
            {
                return "";
            }
        }

        //########################### DIE FILTERS ############################
        //####################################################################
        #region DIE_FILTERING_FUNCTIONS

        // Look at all tasks for all jobs and remove previous scrapping information (so any tasks before scrap time is negated from information)
        private void Remove_Scrap_Entries()
        {
            foreach (string order_number in All_Die_Order_Numbers)
            {
                try
                {
                    List<List<string>> temp = Die_Tasks_List[order_number]; // temp list to retain non-removed tasks
                    int keep_index = 0;
                    int index = 0;
                    foreach (List<string> tasks in Die_Tasks_List[order_number])
                    {
                        if (tasks[0].ToUpper() == "SC")
                        {
                            keep_index = index + 1;
                        }
                        index++;
                    }
                    temp.RemoveRange(0, keep_index);
                    Die_Tasks_List[order_number] = temp;
                }
                catch
                {
                    // No tasks available for job
                }
            }
        }

        // Look at all the dies and see if they've been in HT. If so, remove from master list
        private void Remove_Dies_Past_HT()
        {
            Master_Die_List.RemoveAll(Die => Check_Task_Exist(Die[0], "RK").Length > 0 || Check_Task_Exist(Die[0], "RC").Length > 0 || Check_Task_Exist(Die[0], "D1").Length > 0 ||
                    Check_Task_Exist(Die[0], "D2").Length > 0 || Check_Task_Exist(Die[0], "D3").Length > 0 || Check_Task_Exist(Die[0], "HD").Length > 0);

            All_Die_Order_Numbers.RemoveAll(Die => Check_Task_Exist(Die, "RK").Length > 0 || Check_Task_Exist(Die, "RC").Length > 0 || Check_Task_Exist(Die, "D1").Length > 0 ||
                    Check_Task_Exist(Die, "D2").Length > 0 || Check_Task_Exist(Die, "D3").Length > 0 || Check_Task_Exist(Die, "HD").Length > 0);
        }

        // Adjust the due dates so that if they bypass weekend, subtract 2 days or holidays
        private void Adjust_Due_Dates()
        {
            ExcoDateTime EDT = new ExcoDateTime();
            foreach (List<string> die in Master_Die_List)
            {
                die[7] = EDT.Get_Actual_Due_Date(DateTime.Now, Convert.ToDouble(die[7]), Part_Type).ToString();
            }
        }

        // Determine whether the job is on lathe, if so mark true for onLathe
        private void Check_OnLathe_Status()
        {
            foreach (List<string> die in Master_Die_List)
            {
                if (Check_Task_Exist(die[0], "L0").Length > 0 || Check_Task_Exist(die[0], "L1").Length > 0 || Check_Task_Exist(die[0], "LA").Length > 0)
                {
                    die[10] = "true";
                }
            }
        }

        // Determine whether the job is done lathe and mark is ready for CNC (also check if the die needs lifting hole. If it does, check if it is done lifting holes)
        private void Check_Lathe_Status()
        {
            bool has_LH;
            string Die_Type_Check;

            foreach (List<string> die in Master_Die_List)
            {
                has_LH = false;
                Die_Type_Check = Part_Type == "P" ? "PLATE" : "MANDREL";
                if (Check_Task_Exist(die[0], "LS").Length > 0 || Check_Task_Exist(die[0], "LC").Length > 0 || Check_Task_Exist(die[0], "DS").Length > 0)
                {
                    die[18] = "true";
                    try
                    {
                        var text = "";
                        if (File.Exists(Lifting_Hole_Path + die[0] + ".txt"))
                        {
                            text = File.ReadAllText(Lifting_Hole_Path + die[0] + ".txt");
                        }
                        else if (File.Exists(Lifting_Hole_Path2 + die[0] + ".txt"))
                        {
                            text = File.ReadAllText(Lifting_Hole_Path2 + die[0] + ".txt");
                        }
                        string[] lines = text.Split(new string[] { ":" }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            if (line.Contains(Die_Type_Check))
                            {
                                has_LH = true;
                            }
                        }
                    }
                    catch
                    {
                        die[11] = "true";
                        // File does not exist so not ready
                    }
                    if (has_LH && !(die[13] == "true")) //Has lifting hole but is not done lifting hole
                    {
                        die[11] = "false";
                    }
                    else
                    {
                        die[11] = "true";
                    }
                }
            }
        }

        // Determine whether the job is done cnc for mis-punching
        private void Check_CNCDone_Status()
        {
            foreach (List<string> die in Master_Die_List)
            {
                if (Part_Type == "P" && (Check_Task_Exist(die[0], "N0").Length > 0 || Check_Task_Exist(die[0], "N1").Length > 0 || Check_Task_Exist(die[0], "PB").Length > 0 || Check_Task_Exist(die[0], "PC").Length > 0))
                {
                    die[12] = "true";
                }
                else if (Check_Task_Exist(die[0], "N0").Length > 0 || Check_Task_Exist(die[0], "N1").Length > 0 || Check_Task_Exist(die[0], "MB").Length > 0 || Check_Task_Exist(die[0], "MF").Length > 0)
                {
                    die[12] = "true";
                }
            }
        }

        // Determine whether the job is done lifting holes
        private void Check_lhDone_Status()
        {
            foreach (List<string> die in Master_Die_List)
            {
                if (Check_Task_Exist(die[0], "L12", 2, 2).Length > 0 || Check_Task_Exist(die[0], "LA12", 2, 2).Length > 0)
                {
                    die[13] = "true";
                }
                else if (Check_Task_Exist(die[0], "LH").Length > 0 && !(Convert.ToDateTime(Check_Task_Exist(die[0], "LH", 0, 1)).AddMinutes(30) > DateTime.Now))
                {
                    die[13] = "true";
                }
            }
        }

        // Store the last station into master list
        private void Get_Last_Station()
        {
            foreach (List<string> die in Master_Die_List)
            {
                if (die[0] == "332595")
                {
                    Console.Write("");
                }
                die[14] = Get_Last_Task(die[0], 2);
                //if (die[14].StartsWith("MC")) 
                try
                {
                    die[21] = Employee_List[Get_Last_Task(die[0], 3)];
                }
                catch
                {
                }
                string last_task = Get_Last_Task(die[0], 0);
                if (last_task == "ACDW" || last_task == "DP" || last_task == "EL" || last_task == "WN" || last_task == "LR" || last_task == "AM" || (Get_Last_Task(die[0], 2) == "F" && !(last_task == "GA") && !(last_task == "IP")))

                {
                    Die_Tasks_List[die[0]].RemoveAt(Die_Tasks_List[die[0]].Count-1);
                    try
                    {
                        //Die_Tasks_List[die[0]].RemoveAt(-1);
                        last_task = Get_Last_Task(die[0], 0);
                        die[14] = Get_Last_Task(die[0], 2);
                        if (last_task == "ACDW" || last_task == "DP" || last_task == "EL" || last_task == "WN" || last_task == "LR" || last_task == "AM")
                        {
                            Die_Tasks_List[die[0]].RemoveAt(Die_Tasks_List[die[0]].Count - 1);
                            die[14] = Get_Last_Task(die[0], 2);
                        }
                        //if (die[14].StartsWith("MC")) 
                        try
                        {
                            die[21] = Employee_List[Get_Last_Task(die[0], 3, 2)];
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // Only one task
                }
                    }
            }
        }

        // Sort dies according to location
        private void Sort_Dies()
        {
            string[] Invalid_Stations = { "UBCH", "PFCK", "ASS", "INSP", "MBCH", "SBCH", "MBCK", "WRIN", "PBCK", "HBCH", "REM" }; // stations ahead of cnc
            string[] CNC_Tasks = { "NC", "N1", "N0", "TB" }; // cnc stations
            foreach (List<string> die in Master_Die_List)
            {
                string lastStation = Get_Last_Task(die[0], 2);
                string lastTask = Get_Last_Task(die[0], 0);
                // If not done CNC and went on to the next production step (mis-punch)
                if (!(die[12] == "true") && (Invalid_Stations.Contains(lastStation) || die[14].Contains("SP") || die[14].Contains("F") || die[14].Contains("L13")))
                {
                    if (!(Part_Type == "M" && die[9] == "S"))
                    {
                        die[15] = "true";
                        MisPunched_Die_List.Add(die);
                        if (die[9] == "S")
                        {
                            misPunch_Solid_Count++;
                        }
                        else if (die[9] == "H")
                        {
                            misPunch_Hollow_Count++;
                        }
                    }
                }
                // If on machine
                else if (CNC_Tasks.Contains(lastTask))
                {
                    if (!(Part_Type == "M" && die[9] == "S"))
                    {
                        die[16] = "true";
                        OnMachine_Die_List.Add(die);
                        if (Part_Type == "P")
                        {
                            Active_Plate_Machines.Add(lastStation);
                        }
                        else if (Part_Type == "M")
                        {
                            Active_Mandrel_Machines.Add(lastStation);
                        }

                        if (die[9] == "S")
                        {
                            onMachine_Solid_Count++;
                        }
                        else
                        {
                            onMachine_Hollow_Count++;
                        }
                    }
                }
                // If not done CNC, sort to ToDoList
                else if (!(die[12] == "true") && (die[3] == "False"))
                {
                    if (!(Part_Type == "M" && die[9] == "S"))
                    {
                        die[17] = "true";
                        ToDo_Die_List.Add(die);
                        if (die[9] == "S")
                        {
                            toDo_Solid_Count++;
                        }
                        else if (die[9] == "H")
                        {
                            toDo_Hollow_Count++;
                        }
                    }
                }
            }
        }

        // Analyze if any jobs have been bypassed
        private void Analyze_Bypassed_Jobs()
        {
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            string bypass_query;
            database.Open(Database.DECADE_MARKHAM);

            double duedate = 9999;
            // Get the next next priority due date job
            foreach (List<string> die in Master_Die_List)
            {
                if (die[11] == "true")  // If ready
                {
                    if (Convert.ToDouble(die[7]) <= duedate)
                    {
                        duedate = Convert.ToDouble(die[7]);
                    }
                }
            }

            foreach (List<string> die in Master_Die_List)
            {
                if (die[16] == "true")  // If on machine
                {
                    if (die[20].Length < 1 && Convert.ToDouble(die[7]) > duedate)
                    {
                        try
                        {
                            // Check if job exists using try/catch. Will fail if no ordernumber exists
                            string query = "select * from d_millingreadylist where ordernumber = '" + die[0] + "'";
                            reader = database.RunQuery(query);
                            while (reader.Read())
                            {
                                string temp = reader[0].ToString();
                                die[19] = "true";
                            }
                        }
                        catch
                        {
                            die[19] = "true";
                            bypass_query = "insert into d_millingreadylist values ('" + die[0] + "','" + Part_Type + "','0','1','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "','" + Get_Last_Task(die[0], 3) + "')";
                            reader = database.RunQuery(bypass_query);
                            reader.Close();
                        }
                    }
                }
            }
        }

        private void Check_OnMachine_Jobs()
        {
            string[] CNC_Tasks = { "NC", "N1", "N0", "TB" }; // cnc stations
            foreach (List<string> die in Master_Die_List)
            {
                string lastTask = Get_Last_Task(die[0], 0); 
                if (CNC_Tasks.Contains(lastTask))
                {
                    if (!(Part_Type == "M" && die[9] == "S"))
                    {
                        die[16] = "true";
                    }
                }
            }
        }
        
        // Check bypassed jobs
        private void Check_Bypassed_Jobs()
        {
            string order_numbers_str = "";

            foreach (string SO in All_Die_Order_Numbers)
            {
                order_numbers_str = order_numbers_str + "ordernumber = '" + SO + "' or ";
            }
            string bypass_query = "select ordernumber, isready, isoverdo from d_millingreadylist where (" + order_numbers_str.Substring(0, order_numbers_str.Length - 3) + ") and part like '" + Part_Type.ToUpper() + "'";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(bypass_query);

            while (reader.Read())
           { 
                if (reader[1].ToString().Trim() == "1") // check manually set ready
                {
                    Master_Die_List[Get_Die_Master_Index(reader[0].ToString( ).Trim())][11] = "true";
                }
                if (reader[2].ToString().Trim() == "1") // if overdo
                {
                    Master_Die_List[Get_Die_Master_Index(reader[0].ToString().Trim())][19] = "true";
                }
            }
            reader.Close();
        }

        #endregion
        //####################################################################
        //####################################################################

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            toDo_table.Focus();
        }

        // Load plate schedule
        private void plate_button_Click(object sender, EventArgs e)
        {
            if (!(Part_Type == "P"))
            {
                title_text_box.Text = "PLATE SCHEDULE";
                mandrel_button.BackColor = default(Color);
                plate_button.BackColor = Color.Green;
                Part_Type = "P";
                Refresh_Tables();
                title_text_box.Location = new System.Drawing.Point(402, 2);
            }
        }

        // Load mandrel schedule
        private void mandrel_button_Click(object sender, EventArgs e)
        {
            if (!(Part_Type == "M"))
            {
                title_text_box.Text = "MANDREL SCHEDULE";
                plate_button.BackColor = default(Color);
                mandrel_button.BackColor = Color.Green;
                Part_Type = "M";
                Refresh_Tables();
                title_text_box.Location = new System.Drawing.Point(365, 2);
            }
        }

        // Button for all shop order numbers
        private void ordernumber_button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (Last_Button_Pressed == button.Name)
            {
                System.Threading.Thread.Sleep(500);
                Last_Button_Pressed = "";
            }
            else
            {
                if (!(Viewed_Orders.ContainsKey(button.Name.Substring(0, 6)))) // If order number is not in dictionary, add key to report
                {
                    Viewed_Orders.Add(button.Name.Substring(0, 6), "true");
                }
                string URL = "http://shoptrack/tracking/track.asp?id=" + button.Name.Substring(0, 6);
                Process.Start(URL);
                Last_Button_Pressed = button.Name;
            }
        }

        // Button for all shop order numbers
        private void assignment_button_Click(object sender, EventArgs e)
        {
            if (Enable_Assign)
            {
                var button = (Button)sender;
                Assign_Task AT = new Assign_Task(this, button.Name.Substring(0, 6), Master_Die_List[Get_Die_Master_Index(button.Name.Substring(0, 6))][20].Length > 0, Part_Type, (Master_Die_List[Get_Die_Master_Index(button.Name.Substring(0, 6))][22] == "1" ? true : false));
                AT.ShowDialog();
            }
        }

        // Add new row in toDo list
        private void toDo_Add_Row(string id, string ordernumber, string die_type, string diameter, string duedate, string station, string status, string assignedName, string onLathe, string doneLathe, string fasttrack, bool isPanic = false)
        {
            Color due_date_color = Color.White;
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 4 : 3))
            {
                due_date_color = Color.Yellow;
            }
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 3 : 2))
            {
                due_date_color = Color.Red;
            }

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            Color background = Color.White;
            
            if ((Viewed_Orders.ContainsKey(ordernumber))) // If order number is not in dictionary, add key to report
            {
                string g = Viewed_Orders[ordernumber];
                background = Color.LightBlue;
            }

            #region Shop order number button
            MyButton new_button = new MyButton();
            new_button.Location = new System.Drawing.Point(12, 12);
            new_button.Name = ordernumber + "_button";
            new_button.FlatStyle = FlatStyle.Flat;
            new_button.TabStop = false;
            new_button.FlatAppearance.BorderSize = 0;
            new_button.Size = new System.Drawing.Size(138, 23);
            if (fasttrack.ToLower().Contains("true"))
                new_button.ForeColor = Color.Red;
            new_button.TabIndex = 0;
            new_button.Font = st;
            new_button.Text = ordernumber.Substring(1, 5) + die_type;
            new_button.BackColor = background;
            new_button.UseVisualStyleBackColor = true;
            new_button.Click += new System.EventHandler(ordernumber_button_Click);
            #endregion

            #region Assignment  button
            MyButton assignment_button = new MyButton();
            assignment_button.Location = new System.Drawing.Point(12, 12);
            assignment_button.Name = ordernumber + "_button_assign";
            assignment_button.FlatStyle = FlatStyle.Flat;
            assignment_button.TabStop = false;
            assignment_button.FlatAppearance.BorderSize = 0;
            assignment_button.Size = new System.Drawing.Size(100, 23);
            assignment_button.TabIndex = 0;
            assignment_button.Font = (assignedName.Length > 0 ? bt : a);
            assignment_button.ForeColor = (assignedName.Length > 0 ? Color.Black : Color.LightGray);
            if (isPanic) assignment_button.BackColor = Color.LightPink;
            assignment_button.Text = (assignedName.Length > 0 ? assignedName : "Unassigned");
            assignment_button.UseVisualStyleBackColor = true;
            //assignment_button.Click += new System.EventHandler(assignment_button_Click);
            assignment_button.DoubleClick += new System.EventHandler(assignment_button_Click);
            #endregion

            string Lathe_Status = (doneLathe == "true" ? "X " : (onLathe == "true") ? "* " : "");

            toDo_table.RowCount = toDo_table.RowCount + 1;
            int row_count = toDo_table.RowCount - 1;
            //toDo_table.Controls.Add(new Label() { Font = f, Text = ordernumber.Substring(1, 5) + die_type }, 1, row_count);

            toDo_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), Font = ff, Text = id }, 0, row_count);
            toDo_table.Controls.Add(new_button, 1, row_count);
            toDo_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), 
                Font = f, Text = diameter }, 2, row_count);
            toDo_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), 
                Font = f, Text = Lathe_Status + Math.Round(Convert.ToDouble(duedate), 1).ToString(), BackColor = due_date_color }, 3, row_count);
            toDo_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), 
                Font = st, Text = station }, 4, row_count);
            toDo_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), 
                Font = f, Text = (status == "true" ? "Ready" : ""), ForeColor = (status == "true" ? Color.Green : Color.Black) }, 5, row_count);
            if (Part_Type == "P")
            {
                if (Convert.ToInt32(id) < 999999)// bhavesh21)
                {
                    toDo_table.Controls.Add(assignment_button, 6, row_count);
                }
                else
                {
                    toDo_table.Controls.Add(new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(1),
                        Font = f,
                        Text = assignedName
                    }, 6, row_count);
                }
            }
            else if (Part_Type == "M")
            {
                if (true)//Convert.ToInt32(id) < 11)
                {
                    toDo_table.Controls.Add(assignment_button, 6, row_count);
                }
                else
                {
                    toDo_table.Controls.Add(new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(1),
                        Font = f,
                        Text = assignedName
                    }, 6, row_count);
                }
            }

            toDo_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
        }

        // Create toDo table
        private void toDo_Create_Table()
        {
            toDo_table.SuspendLayout();
            toDo_table.Visible = false;

            
            while (toDo_table.RowCount > 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    Control c = toDo_table.GetControlFromPosition(i, toDo_table.RowCount - 1);
                    toDo_table.Controls.Remove(c);
                }
                toDo_table.RowStyles.RemoveAt(toDo_table.RowCount - 1);
                toDo_table.RowCount = toDo_table.RowCount - 1;
            }

            toDo_table.ResumeLayout(false);

            int index = 1;

            foreach (List<string> die in ToDo_Die_List)
            {
                if (index < 9999999) // bhavesh disable view 30)
                {
                    toDo_Add_Row(index.ToString(), die[0], die[9], die[8], die[7], die[14], die[11], die[20], die[10], die[18], die[2], (die[22] == "1"));
                }
                index++;
            }
            toDo_table.PerformLayout();
            toDo_table.Visible = true;

            toDoList_counts.Text = "Total: " + (Convert.ToInt32(toDo_Hollow_Count) + Convert.ToInt32(toDo_Solid_Count)).ToString() + "        Hollows: " + Convert.ToInt32(toDo_Hollow_Count) + "        Solids: " + Convert.ToInt32(toDo_Solid_Count);
        }

        // Add new row in onMachine list
        private void onMachine_Add_Row(string id, string ordernumber, string die_type, string diameter, string duedate, string station, string employeeName, string onLathe, string doneLathe, string fasttrack, string beenBypassed)
        {
            Color due_date_color = Color.White;
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 4 : 3))
            {
                due_date_color = Color.Yellow;
            }
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 3 : 2))
            {
                due_date_color = Color.Red;
            }

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            #region Shop order number button
            MyButton new_button = new MyButton();
            new_button.Location = new System.Drawing.Point(12, 12);
            new_button.Name = ordernumber + "_button";
            new_button.BackColor = beenBypassed == "true" ? Color.LightBlue : Color.White;
            new_button.FlatStyle = FlatStyle.Flat;
            new_button.TabStop = false;
            new_button.FlatAppearance.BorderSize = 0;
            new_button.Size = new System.Drawing.Size(138, 23);
            if (fasttrack.ToLower().Contains("true"))
                new_button.ForeColor = Color.Red;
            new_button.TabIndex = 0;
            new_button.Font = st;
            new_button.Text = ordernumber.Substring(1, 5) + die_type;
            new_button.UseVisualStyleBackColor = true;
            new_button.Click += new System.EventHandler(ordernumber_button_Click);

            DateTime Comp_time = Convert.ToDateTime(Get_Last_Task(ordernumber, 1));
            //Color id_bg = DateTime.Now <= Convert.ToDateTime(Get_Last_Task(ordernumber, 1)).AddHours(1) ? Color.LightGreen : Color.White;
            //Color id_bg = DateTime.Now <= Comp_time.AddHours(4) ? System.Drawing.ColorTranslator.FromHtml("#FF0000") : Color.White;
            //id_bg = DateTime.Now <= Comp_time.AddHours(3) ? System.Drawing.ColorTranslator.FromHtml("#FA5858") : id_bg;
            Color id_bg = DateTime.Now <= Comp_time.AddMinutes(45) ? System.Drawing.ColorTranslator.FromHtml("#CEF6D8") : Color.White;
            id_bg = DateTime.Now <= Comp_time.AddMinutes(45) ? System.Drawing.ColorTranslator.FromHtml("#A9F5BC") : id_bg;
            id_bg = DateTime.Now <= Comp_time.AddMinutes(30) ? System.Drawing.ColorTranslator.FromHtml("#81F781") : id_bg;
            id_bg = DateTime.Now <= Comp_time.AddMinutes(15) ? System.Drawing.ColorTranslator.FromHtml("#58FA58") : id_bg;
            id_bg = DateTime.Now <= Comp_time.AddMinutes(7) ? System.Drawing.ColorTranslator.FromHtml("#04B404") : id_bg;
            
            #endregion

            string Lathe_Status = (doneLathe == "true" ? "X " : (onLathe == "true") ? "* " : "");

            onMachine_table.RowCount = onMachine_table.RowCount + 1;
            int row_count = onMachine_table.RowCount - 1;
            //toDo_table.Controls.Add(new Label() { Font = f, Text = ordernumber.Substring(1, 5) + die_type }, 1, row_count);

            onMachine_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), Font = ff, Text = id, BackColor = id_bg}, 0, row_count);
            onMachine_table.Controls.Add(new_button, 1, row_count);
            onMachine_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = diameter
            }, 2, row_count);
            onMachine_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = Lathe_Status + Math.Round(Convert.ToDouble(duedate), 1).ToString(),
                BackColor = due_date_color
            }, 3, row_count);
            onMachine_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = st,
                Text = station
            }, 4, row_count);
            onMachine_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = st,
                Text = employeeName
            }, 5, row_count);

            onMachine_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
        }

        // Create onMachine table
        private void onMachine_Create_Table()
        {
            onMachine_table.SuspendLayout();
            onMachine_table.Visible = false;


            while (onMachine_table.RowCount > 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    Control c = onMachine_table.GetControlFromPosition(i, onMachine_table.RowCount - 1);
                    onMachine_table.Controls.Remove(c);
                }
                onMachine_table.RowStyles.RemoveAt(onMachine_table.RowCount - 1);
                onMachine_table.RowCount = onMachine_table.RowCount - 1;
            }

            onMachine_table.ResumeLayout(false);

            int index = 1;

            foreach (List<string> die in OnMachine_Die_List) 
            {
                onMachine_Add_Row(index.ToString(), die[0], die[9], die[8], die[7], die[14], die[21], die[10], die[18], die[2], die[19]);
                index++;
            }
            onMachine_table.PerformLayout();
            onMachine_table.Visible = true;

            onMachineList_counts.Text = "Total: " + (Convert.ToInt32(onMachine_Hollow_Count) + Convert.ToInt32(onMachine_Solid_Count)).ToString() + "        Hollows: " + Convert.ToInt32(onMachine_Hollow_Count) + "        Solids: " + Convert.ToInt32(onMachine_Solid_Count);
        }

        // Add new row in onMachine list
        //private void misPunch_Add_Row(string id, string ordernumber, string die_type, string diameter, string duedate, string station, string employeeName, string onLathe, string doneLathe, string fasttrack)
        // Add new row in onMachine list
        private void misPunch_Add_Row(string id, string ordernumber, string die_type, string diameter, string duedate, string station, string employeeName, string onLathe, string doneLathe, string fasttrack)
        {
            Color due_date_color = Color.White;
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 4 : 3))
            {
                due_date_color = Color.Yellow;
            }
            if (Convert.ToDouble(duedate) < (die_type == "H" ? 3 : 2))
            {
                due_date_color = Color.Red;
            }

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            #region Shop order number button
            MyButton new_button = new MyButton();
            new_button.Location = new System.Drawing.Point(12, 12);
            new_button.Name = ordernumber + "_button";
            new_button.FlatStyle = FlatStyle.Flat;
            new_button.TabStop = false;
            new_button.FlatAppearance.BorderSize = 0;
            new_button.Size = new System.Drawing.Size(138, 23);
            if (fasttrack.ToLower().Contains("true"))
                new_button.ForeColor = Color.Blue;
            new_button.TabIndex = 0;
            new_button.Font = st;
            new_button.Text = ordernumber.Substring(1, 5) + die_type;
            new_button.UseVisualStyleBackColor = true;
            new_button.Click += new System.EventHandler(ordernumber_button_Click);

            #endregion

            string Lathe_Status = (doneLathe == "true" ? "X " : (onLathe == "true") ? "* " : "");

            misPunch_table.RowCount = misPunch_table.RowCount + 1;
            int row_count = misPunch_table.RowCount - 1;
            //toDo_table.Controls.Add(new Label() { Font = f, Text = ordernumber.Substring(1, 5) + die_type }, 1, row_count);

            misPunch_table.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(1), Font = ff, Text = id }, 0, row_count);
            misPunch_table.Controls.Add(new_button, 1, row_count);
            misPunch_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = diameter
            }, 2, row_count);
            misPunch_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = f,
                Text = Lathe_Status + Math.Round(Convert.ToDouble(duedate), 1).ToString(),
                BackColor = due_date_color
            }, 3, row_count);
            misPunch_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = st,
                Text = station
            }, 4, row_count);
            misPunch_table.Controls.Add(new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                Font = st,
                Text = employeeName
            }, 5, row_count);

            misPunch_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
        }

        // Create onMachine table
        private void misPunch_Create_Table()
        {
            misPunch_table.SuspendLayout();
            misPunch_table.Visible = false;


            while (misPunch_table.RowCount > 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    Control c = misPunch_table.GetControlFromPosition(i, misPunch_table.RowCount - 1);
                    misPunch_table.Controls.Remove(c);
                }
                misPunch_table.RowStyles.RemoveAt(misPunch_table.RowCount - 1);
                misPunch_table.RowCount = misPunch_table.RowCount - 1;
            }

            misPunch_table.ResumeLayout(false);

            int index = 1;

            foreach (List<string> die in MisPunched_Die_List)
            {
                misPunch_Add_Row(index.ToString(), die[0], die[9], die[8], die[7], die[14], die[21], die[10], die[18], die[2]);
                index++;
            }
            misPunch_table.PerformLayout();
            misPunch_table.Visible = true;

            misPunch_counts.Text = "Total: " + (Convert.ToInt32(misPunch_Hollow_Count) + Convert.ToInt32(misPunch_Solid_Count)).ToString() + "        Hollows: " + Convert.ToInt32(misPunch_Hollow_Count) + "        Solids: " + Convert.ToInt32(misPunch_Solid_Count);
        }

        // Add new row in machineStatus list
        private void machineStatus_Add_Rows(string Part_Type)
        {

            #region Fonts
            Font f = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Main Font
            Font st = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Station Font
            Font a = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Assignment Font
            Font bt = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Ordernumber Button Font
            Font ff = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // ID Font
            #endregion

            string[] temp_mach_list =  Part_Type == "P" ? Plate_Machine : Mandrel_Machine;
            List<string> temp_active_mach_list =  Part_Type == "P" ? Active_Plate_Machines : Active_Mandrel_Machines;


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string temp = "";
                    try
                    {
                        temp = temp_mach_list[(i*5) + j];
                    }
                    catch
                    {
                        temp = "";
                    }
                    Color g = Color.LightPink;
                    if (temp_active_mach_list.Contains(temp)) g = Color.LightGreen;
                    machineStatus_table.Controls.Add(new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(1),
                        Font = f,
                        Text = temp,
                        BackColor = (temp == "" ? Color.White : g)
                    }, j, i);
                }
            }
            misPunch_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

        }

        // Create machineStatus table
        private void machineStatus_Create_Table()
        {
            machineStatus_table.SuspendLayout();
            machineStatus_table.Visible = false;


            for (int j = 1; j >= 0; j--)
            {
                for (int i = 0; i < 5; i++)
                {
                    Control c = machineStatus_table.GetControlFromPosition(i, j);
                    machineStatus_table.Controls.Remove(c);
                }
                try
                {
                    machineStatus_table.RowStyles.RemoveAt(j);
                }
                catch
                {
                }
                //machineStatus_table.RowCount = j-1;
            }

            machineStatus_table.ResumeLayout(false);


            machineStatus_Add_Rows(Part_Type);

            machineStatus_table.PerformLayout();
            machineStatus_table.Visible = true;

        }
        
        // Refresh tables button
        private void button1_Click(object sender, EventArgs e)
        {
            Refresh_Tables();
        }

        // Debugging
        private void Admin_button_Click(object sender, EventArgs e)
        {
            Debug DBG = new Debug();
            DBG.Append_Text("########### MIS-PUNCHED JOBS ############");
            int index = 0;
            foreach (List<string> g in Master_Die_List)
            {
                string p = "";
                if (g[15] == "true")
                {
                    index++;
                    foreach (string d in g)
                    {
                        p = p + d + ", ";
                    }
                    DBG.Append_Text(index + ") " + p);
                }
            }
            DBG.Append_Text("########### ON MACHINE ############");
            index = 0;
            foreach (List<string> g in Master_Die_List)
            {
                string p = "";
                if (g[16] == "true")
                {
                    index++;
                    foreach (string d in g)
                    {
                        p = p + d + ", ";
                    }
                    //p = p + g[0] + ", " + g[11] + ", ";
                    DBG.Append_Text(index + ") " + p);
                }
            }
            DBG.Append_Text("########### TO DO LIST ############");
            index = 0;
            foreach (List<string> g in Master_Die_List)
            {
                string p = "";
                if (g[17] == "true")
                {
                    index++;
                    foreach (string d in g)
                    {
                        p = p + d + ", ";
                    }
                    //p = p + g[0] + ", " + g[11] + ", ";
                    DBG.Append_Text(index + ") " + p);
                }
            }
            DBG.ShowDialog();
        }

        int index = 1;

        // Print button
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to print?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                index = 1;
                printDocument1.Print();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
            //index = 1;
            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            //CaptureScreen();
            //printDocument1.DefaultPageSettings.Landscape = true;
            //index = 1;
            //printPreviewDialog1.ShowDialog();
            //printDocument1.Print();
        }

        /* For screen capturing
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();

            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }
        */

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Screen capture
            //e.Graphics.DrawImage(memoryImage, 0, 0);

            // Manually generate
            int height = 0;

            int startx = 50;
            int starty = 40;
            int ColWid1 = 51;
            int ColWid2 = 100;
            int ColWid3 = 65;
            int ColWid4 = 150;
            int ColWid5 = 165;
            int ColWid6 = 80;
            int ColWid7 = 130;


            int headheight = 20;
            int dataheight = 25;

            Pen p = new Pen(Brushes.Black, 2.5f);
            Font f = new Font("Times New Roman", 12.0f);
            Font f1 = new Font("Times New Roman", 14.0f);
            Font f2 = new Font("Times New Roman", 18.0f, FontStyle.Bold);

            #region Header Row
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx, starty, ColWid1, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx, starty, ColWid1, headheight));
            e.Graphics.DrawString("#", f, Brushes.Black, new Rectangle(startx, starty, ColWid1, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1, starty, ColWid2, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1, starty, ColWid2, headheight));
            e.Graphics.DrawString("S.O.#", f, Brushes.Black, new Rectangle(startx + ColWid1, starty, ColWid2, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2, starty, ColWid3, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2, starty, ColWid3, headheight));
            e.Graphics.DrawString("DIA", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2, starty, ColWid3, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 , starty, ColWid4, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, starty, ColWid4, headheight));
            e.Graphics.DrawString("DUE", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 , starty, ColWid4, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));
            e.Graphics.DrawString("Station", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, starty, ColWid5, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));
            e.Graphics.DrawString("Status", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, starty, ColWid6, headheight));

            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            e.Graphics.DrawString("Assigned", f, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, starty, ColWid7, headheight));
            #endregion


            height = starty + headheight;

            while (index < ToDo_Die_List.Count + 1)
            {
                if (index < 30)
                {
                    List<string> die = ToDo_Die_List[index - 1];
                    if (height > e.MarginBounds.Height)
                    {
                        height = starty;
                        e.HasMorePages = true;
                        return;
                    }

                    e.Graphics.DrawRectangle(p, new Rectangle(startx, height, ColWid1, dataheight));
                    e.Graphics.DrawString(index.ToString(), f1, Brushes.Black, new Rectangle(startx, height, ColWid1, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1, height, ColWid2, dataheight));
                    e.Graphics.DrawString(die[0].Substring(1, 5) + die[9], f1, Brushes.Black, new Rectangle(startx + ColWid1, height, ColWid2, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2, height, ColWid3, dataheight));
                    e.Graphics.DrawString(die[8], f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2, height, ColWid3, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, height, ColWid4, dataheight));
                    e.Graphics.DrawString(Math.Round(Convert.ToDouble(die[7]), 1).ToString(), f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3, height, ColWid4, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, height, ColWid5, dataheight));
                    e.Graphics.DrawString(die[14], f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4, height, ColWid5, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, height, ColWid6, dataheight));
                    e.Graphics.DrawString(die[11] == "true" ? "Ready" : "", f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5, height, ColWid6, dataheight));

                    e.Graphics.DrawRectangle(p, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, height, ColWid7, dataheight));
                    e.Graphics.DrawString(die[20], f1, Brushes.Black, new Rectangle(startx + ColWid1 + ColWid2 + ColWid3 + ColWid4 + ColWid5 + ColWid6, height, ColWid7, dataheight));

                    height += dataheight;
                }
                index++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchDie SD = new SearchDie(this, Master_Die_List, Part_Type);
            SD.ShowDialog();
        }

        private void toDo_table_Paint(object sender, PaintEventArgs e)
        {

        }

        bool Enable_Assign = false;

        private void button4_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter administrative password", "Administrative Configuration Mode", "Password", -1, -1);
            if (input == "98741")
            {
                Enable_Assign = true;
                //Refresh_Tables();
            }
            else
            {
                MessageBox.Show("Invalid Password");
            }
        }
    }


    #region MyButton special button class for removing padding around button and enable double click features
    public partial class MyButton : Button
    {

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //omit base.OnPaint completely...

            //base.OnPaint(pevent); 

            using (Pen p = new Pen(BackColor))
            {
                pevent.Graphics.FillRectangle(p.Brush, ClientRectangle);
            }

            //add code here to draw borders...

            using (Pen p = new Pen(ForeColor))
            {
                pevent.Graphics.DrawString(base.Text, Font, p.Brush, new PointF(8, 0));
            }
        }

        public MyButton()
        {
            //if (base.Text.StartsWith("3") || base.Text.StartsWith("1") || base.Text.StartsWith("2") || base.Text.StartsWith("4") || base.Text.StartsWith("5") || base.Text.StartsWith("6") || base.Text.StartsWith("7") || base.Text.StartsWith("8") || base.Text.StartsWith("9") || base.Text.StartsWith("0"))
            //if (!(base.Name.Contains("assign")))
            //{
            //}
            //else
            //{
                SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            //}
        }
    }
#endregion



}
