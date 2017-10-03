using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using Databases;
using System.Runtime.InteropServices;

namespace ITManagement
{
    public partial class Office : Form
    {
        /// <summary>
        /// TEST
        /// </summary>
        //                       taskkill /F /IM MyApp.vshost.exe > %temp%\out.txt 2>&1 || exit /B 0
        bool test = false;

        // ################################################################################################
        // ################################################################################################

        // Detect user input in order to reset inactivity
        [DllImport("User32.dll")]
        private static extern bool
                GetLastInputInfo(ref LASTINPUTINFO plii);

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        public static long GetTickCount()
        {
            return Environment.TickCount;
        }

        private System.Windows.Forms.Timer CheckIdleTimer;

        // ################################################################################################
        // ################################################################################################

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            //_parent._APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGIN] - '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER]);
            //if (e.CloseReason == CloseReason.WindowsShutDown)

            try
            {
                _APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGOFF] - '" + _EMPLOYEE_LIST[_MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
            }
            catch
            {
                _APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGOFF] - '" + "Error: Unknown user" + "'");
                //_APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGOFF] - '" + "Error: Unknown user" + "' (SessionID:#" + _MASTER_SESSION_ID.ToString() + ")");
            }

            _STORE_COMPUTER_INFO();

            string _INFO_FILE_PATH = "";

            if (test)
            {
                _INFO_FILE_PATH = Directory.GetCurrentDirectory() + "\\ITManagement_Config.ini";
            }
            else
            {
                _INFO_FILE_PATH = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini";
            }

            File.Copy(_INFO_FILE_PATH, backup_directory_path + "\\ITManagement_Config_" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_Hr-" + DateTime.Now.Hour.ToString() + ".ini", true);

            try
            {
                File.Copy(_INFO_FILE_PATH, "\\\\10.0.0.8\\shopdata\\Development\\Robin\\test\\sync\\backup" + "\\ITManagement_Config_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".ini", true);
            }
            catch
            {
                // Secondary backup failed
                _APPEND_TO_SETUP_INFORMATION(2, "Secondary backup failed to save");
            }


            //System.Threading.Thread.Sleep(200);
            Environment.Exit(0);
        }



        public ExcoODBC database = ExcoODBC.Instance;
        public OdbcDataReader reader;
                                                                       
        // Update timer
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();

        // MASTER COMPUTER LIST
        //     0          1            2             3           4          5            6             7               8
        // [BUTTON#, DEPARTMENT, COMPUTER_NAME, COMPUTER_IP, MAIN_LOGIN, PASSWORD, HARDWARE_LIST, SOFTWARE_LIST, CHANGE_LOG_LIST]
        public List<List<string>> _MASTER_COMPUTER_LIST = new List<List<string>>();

        // Master information setup list (make sure you update setup count: 
        //
        // INDEX 0 = IT Information list
        // INDEX 1 = INVENTORY
        // INDEX 2 = ACTION LOG
        // INDEX 3 = SETTINGS/CREDENTIALS
        //              - settings_type~value▄...
        // INDEX 4 = DYNAMIC BUTTON LIST
        //              - button_text~x,y~station_type▄...
        // INDEX 5 = Reminders
        //              - reminder_date~message~employee#x,employee#y,employee#_readdate#,employee#_readdate#▄...
        // INDEX 6 = Personal Repository
        //              - employee_number~repository_message~date▄...
        public List<string> _MASTER_SETUP_LIST = new List<string>();
        private int _SETUP_LIST_ENTRIES = 7;

        public Dictionary<string, List<string>> _MASTER_DYNAMIC_BUTTON_DICT = new Dictionary<string, List<string>>();

        // For searching purposes
        public List<string> _SEARCH_TOGGLE_LIST = new List<string>();
        public int _SEARCH_COUNT = 0;

        // MASTER ENCRYPTER/DECRYPTER
        Encrypter _MASTER_CRYPTSCRIPT = new Encrypter();

        // USER INFO OPEN
        public bool USER_INFO_BOX_OPEN = false;
        // Inventory transfer status
        public bool _Pending_Tranfer = false;
        public int _Tranfer_Index = -1;
        public bool _Inventory_Open = false;
        public bool _Information_Open = false;
        public bool _Action_Log_Open = false;
        public bool _Edit_Stations_Open = false;
        public bool _PURCHASE_HISTORY_OPEN = false;
        public bool _SETTINGS_OPEN = false;

        
        // Keep track of newest file
        public DateTime Write_Time_On_Load = new DateTime();

        public string backup_directory_path = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\Apps\\IT_Management_Backup";

        // MASTER BUTTON LIST
        public List<Button> Button_List = new List<Button>();

        // IT MANAGEMENT BUTTON LIST
        public List<Button> Management_Button_List = new List<Button>();

        // MASTER COMPUTER_COUNT
        public int _COMPUTER_COUNT = 0;

        // INACTIVITY VARIABLE
        public int _INACTIVE_COUNTER = 0;

        // TIMEOUT_DISCONNECT_VARIABLE (in minutes)
        public int _TIMEOUT = 15;//1*60;

        // ADVANCED_SEARCH_BOX_OPEN
        public bool ADVANCED_SEARCH_BOX_OPEN = false;

        // EMPLOYEE NUMBER LOGIN
        public string _MASTER_LOGIN_EMPLOYEE_NUMBER;

        // Employee master list
        public Dictionary<string, string> _EMPLOYEE_LIST = new Dictionary<string, string>();

        public bool _ENABLE_ADMINISTRATIVE_COMMANDS = false;

        // X/Y COORDINATES FOR CIRCLE (PAINT EVENT)
        public int PAINT_X;
        public int PAINT_Y;
        public int PAINT_ELLIPSES_SHRINK_FACTOR = 0;

        // Enable moving of stations
        public bool ENABLE_STATION_DRAG = false;
        public bool ENABLE_STATION_DEL = false;

        // Moving buttons (focal point)
        private Point MouseDownLocation;

        private int _MASTER_SESSION_ID = 0;

        // Master tooltip
        public System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();

        public Office(string login_emp_num="")
        {

            InitializeComponent();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.Paint += this.OnPaint;

            Random r = new Random();
            _MASTER_SESSION_ID = r.Next(100000000, 999999999);

            _INACTIVE_COUNTER = _TIMEOUT;

            if (login_emp_num.Length > 0) 
                _MASTER_LOGIN_EMPLOYEE_NUMBER = login_emp_num;

            tick_update.Interval = 60000;// 60000; (1000 per second)
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(Check_Inactivity);

            CheckIdleTimer = new System.Windows.Forms.Timer();
            CheckIdleTimer.Interval = 9000;
            CheckIdleTimer.Tick += new EventHandler(CheckIdleTimer_Tick);
            CheckIdleTimer.Start();

            _COMPUTER_COUNT = 500;// Button_List.Count(); //- 1; //+1 for inventory
            MaximizeBox = false;
            //List<string> temp = new List<string>();
            //for (int i = 0; i < 9; i++)
            //{
            //    temp.Add("");
            //}

            _RETRIEVE_COMPUTER_INFO();


            // Button test
            //Store_Existing_Buttons();
            Populate_Button_Dictionary();
            Place_Dynamic_Buttons();

            Set_Button_Tooltip();

            timeout_label.Text = "Session expires in " + _INACTIVE_COUNTER.ToString() + " minutes";

            //Get_Employee_List();

            Set_Database_Credentials();

            try
            {
                _APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGIN] - '" + _EMPLOYEE_LIST[_MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
            }
            catch
            {
                _APPEND_TO_SETUP_INFORMATION(2, "[USER-LOGIN] - '" + "Error: Unknown user" + "'");
            }
            _STORE_COMPUTER_INFO();

            Add_MGMT_Buttons();

            //test
            //testbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(buttontest_MouseDown);
            //testbutton.MouseMove += new System.Windows.Forms.MouseEventHandler(buttontest_MouseMove);

        }

        /// <summary>
        /// Add default settings buttons 
        /// </summary>
        private void Add_MGMT_Buttons()
        {
            Management_Button_List.Add(settings_button);
            Management_Button_List.Add(search_button);
            Management_Button_List.Add(inventory_button);
            Management_Button_List.Add(purchase_history_button);
            Management_Button_List.Add(log_button);
            Management_Button_List.Add(info_button);
            Management_Button_List.Add(edit_station_button);
            Management_Button_List.Add(add_reminder);
        }

        // Get Mouse location of mousedown on form
        private void buttontest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        // Main moving function
        private void buttontest_MouseMove(object sender, MouseEventArgs e)
        {
            if (ENABLE_STATION_DRAG)
            {
                Button btn = (Button)sender;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    btn.Left = e.X + btn.Left - MouseDownLocation.X;
                    btn.Top = e.Y + btn.Top - MouseDownLocation.Y;
                    //btn.Text = (e.X + btn.Left - MouseDownLocation.X).ToString() + ", " + (e.Y + btn.Top - MouseDownLocation.Y).ToString();
                    List<string> temp = new List<string> { ((e.X + btn.Left - MouseDownLocation.X).ToString() + "," + (e.Y + btn.Top - MouseDownLocation.Y).ToString()) , _MASTER_DYNAMIC_BUTTON_DICT[btn.Text][1] };
                    _MASTER_DYNAMIC_BUTTON_DICT[btn.Text] = temp;
                }
            }
        }

        private void buttontext_MouseUp(object sender, MouseEventArgs e)
        {
            if (ENABLE_STATION_DRAG)
            {
                _STORE_COMPUTER_INFO();
                PAINT_X = 0;
                PAINT_Y = 0;
                Invalidate();
            }
        }

        // Populate button list dictionary 
        public void Populate_Button_Dictionary()
        {
            
            // Reset
            _MASTER_DYNAMIC_BUTTON_DICT = new Dictionary<string, List<string>>();

            string button_setup_text = _MASTER_SETUP_LIST[4];
            string[] temp = button_setup_text.Split(new string[] { "▄" }, StringSplitOptions.None);
            foreach (string g in temp)
            {

                if (g.Length > 0)
                {
                    string[] temp2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                    List<string> temp_list = new List<string>();
                    temp_list.Add(temp2[1]);
                    temp_list.Add(temp2[2]);
                    _MASTER_DYNAMIC_BUTTON_DICT.Add(temp2[0], temp_list);
                }
            }
        }

        public void Store_Existing_Buttons()
        {
            string ref_size;
            foreach (Button g in Button_List)
            {
                ref_size = g.Size.Width == 10 ? "small" : g.Size.Width == 20 ? "medium" : "large";
                _APPEND_TO_SETUP_INFORMATION(4, "▄" + g.Text + "~" + g.Location.X + "," + g.Location.Y + "~" + ref_size, "▄");
            }
        }


        public void Place_Dynamic_Buttons(bool dispose_button = true)
        {
            if (dispose_button)
            {
                foreach (Button g in Button_List)
                {
                    g.Dispose();
                }
            }

            Button_List = new List<Button>();
            
            Size Reference_Size = new Size();
            /* REFERENCE SIZES
            Size printer_size = new System.Drawing.Size(10, 10);
            Size shop_pc_size = new System.Drawing.Size(20, 20);
            Size regular_size = new System.Drawing.Size(31, 27);
            */

            foreach (KeyValuePair<string, List<string>> entry in _MASTER_DYNAMIC_BUTTON_DICT)
            {
                int x = Convert.ToInt32(entry.Value[0].Split(new string[] { "," }, StringSplitOptions.None)[0]);
                int y = Convert.ToInt32(entry.Value[0].Split(new string[] { "," }, StringSplitOptions.None)[1]);
                Reference_Size = (entry.Value[1] == "small" ? new System.Drawing.Size(10, 10) : 
                                 (entry.Value[1] == "medium" ? new System.Drawing.Size(20, 20) : new System.Drawing.Size(31, 27)));
                Button temp = new Button();
                temp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                temp.ForeColor = System.Drawing.SystemColors.ButtonFace;
                temp.BackColor = System.Drawing.SystemColors.ButtonFace;
                temp.Location = new System.Drawing.Point(x, y);
                temp.Name = "button" + entry.Key.ToString();
                temp.Size = Reference_Size;
                temp.TabIndex = 2246 + Convert.ToInt32(entry.Key);
                temp.Text = entry.Key.ToString();
                temp.UseVisualStyleBackColor = true;
                temp.Click += new System.EventHandler(this.button_Click);

                temp.MouseDown += new System.Windows.Forms.MouseEventHandler(buttontest_MouseDown);
                temp.MouseMove += new System.Windows.Forms.MouseEventHandler(buttontest_MouseMove);
                temp.MouseUp += new System.Windows.Forms.MouseEventHandler(buttontext_MouseUp);

                Button_List.Add(temp);

                this.Controls.Add(temp);
            }
            
        }

        // Idle timer
        private void CheckIdleTimer_Tick(object sender, System.EventArgs e)
        {
            //listBox1.Items.Add(Win32.GetIdleTime().ToString());

            if (GetIdleTime() < 10000)
            {
                _RESET_INACTIVITY();
            }
        }

        private void Get_Employee_List()
        {
            string query = "select employeenumber, firstname, lastname from d_user";

            database.Open();
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                _EMPLOYEE_LIST.Add(reader[0].ToString().Trim(), reader[1].ToString().Trim() + " " + reader[2].ToString().Trim());
            }
            reader.Close();
            database.connection.Close();
        }

        
        // Main Update function
        private void Check_Inactivity(object sender, EventArgs e)
        {
            timeout_label.Text = "Session expires in " + _INACTIVE_COUNTER.ToString()+ " minutes";
            if (_INACTIVE_COUNTER <= 2)
            {
                timeout_label.ForeColor = Color.Red;
            }
            else
            {
                timeout_label.ForeColor = Color.Black;
            }
            if (_INACTIVE_COUNTER <= 0)
            {
                _STORE_COMPUTER_INFO();
                this.Close();
                //System.Threading.Thread.Sleep(1000);
                //Environment.Exit(0);
            }
            else
            {
                _INACTIVE_COUNTER--;
            }
        }


        private void Reset_Parameters()
        {
            _MASTER_COMPUTER_LIST = new List<List<string>>();
            Button_List = new List<Button>();
        }

        // default return name (assumes button exists)
        private string _RETRIEVE_USER_DATA(int button_number, int retrieve_index = 1)
        {
            foreach (List<string> i in _MASTER_COMPUTER_LIST)
            {
                if (!(i == null) && i[0] == button_number.ToString())
                {
                    return i[retrieve_index];
                }
            }
            return "";
        }

        // Retrieves DATA from FILE containing the MASTER LIST of all the COMPUTERS (DECRYPTS)
        public void _RETRIEVE_COMPUTER_INFO(string info_path = "")
        {
            _MASTER_COMPUTER_LIST = new List<List<string>>();
            _SEARCH_TOGGLE_LIST = new List<string>();
            #region Add Empty Slots for placeholders
            for (int i = 0; i < _COMPUTER_COUNT; i++)
            {
                _MASTER_COMPUTER_LIST.Add(null);
                _SEARCH_TOGGLE_LIST.Add("0");
            }
            #endregion

            string _INFO_FILE_PATH = "";
            if (test && info_path == "")
            {
                _INFO_FILE_PATH = Directory.GetCurrentDirectory() + "\\ITManagement_Config.ini";
            }
            else if (info_path == "")
            {
                _INFO_FILE_PATH = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini";
            }
            else
            {
                _INFO_FILE_PATH = info_path;
            }

            _MASTER_DYNAMIC_BUTTON_DICT = new Dictionary<string, List<string>>();
            _MASTER_SETUP_LIST = new List<string>();

            if (File.Exists(_INFO_FILE_PATH))
            {
                //Write_Time_On_Load = File.GetLastWriteTime(_INFO_FILE_PATH);

                var text = File.ReadAllText(_INFO_FILE_PATH);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string computer_list_line in lines)
                {
                    if (computer_list_line.Length >= 1)
                    {
                        // If line is a setup/info line
                        if (computer_list_line.StartsWith("#"))
                        {
                            _MASTER_SETUP_LIST.Add(_MASTER_CRYPTSCRIPT.Decrypt(computer_list_line.Substring(1)));
                        }
                        else // Dynamic button (Station)
                        {
                            List<string> _TEMP_LIST = new List<string>();
                            string[] lines2 = computer_list_line.Split(new string[] { "█" }, StringSplitOptions.None);
                            for (int i = 0; i < 9; i++)
                            //foreach (string line in lines2)
                            {
                                //if (!line.Contains("▄")) // If not HARDWARE/SOFTWARE/CHANGE_LOG
                                //{
                                if (i < 6 && i > 0)//6 && i > 0)
                                    _TEMP_LIST.Add(_MASTER_CRYPTSCRIPT.Decrypt(lines2[i]));
                                else if (i > 5)
                                {
                                    // Decrypt based on each split entity of ▄
                                    string parse_split = "";
                                    string[] lines3 = lines2[i].Trim(Convert.ToChar("▄")).Split(new string[] { "▄" }, StringSplitOptions.None);
                                    foreach (string g in lines3)
                                    {
                                        parse_split = parse_split + _MASTER_CRYPTSCRIPT.Decrypt(g) + "▄";
                                    }

                                    _TEMP_LIST.Add(parse_split);
                                }
                                else
                                    _TEMP_LIST.Add(lines2[i]);
                            }
                            _MASTER_COMPUTER_LIST[Convert.ToInt32(_TEMP_LIST[0])] = _TEMP_LIST;
                        }
                    }
                }
                while (!(_MASTER_SETUP_LIST.Count == _SETUP_LIST_ENTRIES)) // Information/Inventory post-setup to define if not existing
                {
                    _MASTER_SETUP_LIST.Add("");
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(_INFO_FILE_PATH)) // Create LOG file
                {
                    //sw.Write("\n");
                    sw.Close();
                }
            }
            #region Remove Empty Slots for placeholders
            for (int i = _COMPUTER_COUNT - 1; i >= 0; i--)
            {
                if (_MASTER_COMPUTER_LIST[i] == null)
                {
                    _MASTER_COMPUTER_LIST.RemoveAt(i);
                    _SEARCH_TOGGLE_LIST.RemoveAt(i);
                }
            }
            _COMPUTER_COUNT = _MASTER_COMPUTER_LIST.Count();
            #endregion
            Populate_Button_Dictionary();


        }

        public void _Reload_Config_Info(string path)
        {
            string transfer_log = "";
            if (_MASTER_SETUP_LIST.Count >= 3)
            {
                transfer_log = _MASTER_SETUP_LIST[2];
            }
            _RETRIEVE_COMPUTER_INFO(path);
            //Populate_Button_Dictionary();
            Place_Dynamic_Buttons(true);
            //_MASTER_SETUP_LIST[2] = _MASTER_SETUP_LIST[2] + (transfer_log.Length > 2 ? "▄" + transfer_log : ""); // Transfer original log over (As to not lose information)
                    
            _MASTER_SETUP_LIST[2] = transfer_log; // Transfer original log over (As to not lose information)

            ToolTip1.Dispose();
            ToolTip1 = new ToolTip();
            Set_Button_Tooltip(false);
            _STORE_COMPUTER_INFO();
        }


        /// <summary>
        /// Store computer information and it does NOT overwrite
        /// </summary>
        /// <param name="index"></param> 
        /// <param name="information"></param>
        public void _STORE_SETUP_INFORMATION(int index, string information)
        {
            _MASTER_SETUP_LIST[index] = information;
        }

        public void _APPEND_TO_SETUP_INFORMATION(int index, string append_text, string trim_char="@##$@#$")
        {
            if (append_text.StartsWith(trim_char) || append_text.EndsWith(trim_char))
            {
                append_text.Trim(Convert.ToChar(trim_char));
            }
            _MASTER_SETUP_LIST[index] = _MASTER_SETUP_LIST[index] + ((index == 2 && _MASTER_SETUP_LIST[index].Length > 3) ? "▄" : "") + (index == 2 ? "[" + DateTime.Now + "] : " : "") + append_text + (index == 2 ? " (SSID:" + _MASTER_SESSION_ID.ToString() + ")" : "" ); // special features for log
        }

        // Store the information into the file
        public void _STORE_COMPUTER_INFO()
        {
            string temp = string.Empty;
            foreach (KeyValuePair<string, List<string>> entry in _MASTER_DYNAMIC_BUTTON_DICT)
            {
                temp = temp + entry.Key + "~" + entry.Value[0] + "~" + entry.Value[1] + "▄";
            }

            _MASTER_SETUP_LIST[4] = temp.Trim(Convert.ToChar("▄"));

            string _INFO_FILE_PATH = "";
            if (test)
            {
                _INFO_FILE_PATH = Directory.GetCurrentDirectory() + "\\ITManagement_Config.ini";
            }
            else
            {
                _INFO_FILE_PATH = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini";
            }
            //string _INFO_FILE_PATH = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini";
            //string _INFO_FILE_PATH = Directory.GetCurrentDirectory() + "\\ITManagement_Config.ini";
            string line = "";

            foreach (string line2 in _MASTER_SETUP_LIST)
            {
                if (line2.Length >= 0)
                {
                    line = line + "#" + _MASTER_CRYPTSCRIPT.Encrypt(line2);
                    line = line + Environment.NewLine;
                }
            }

            foreach (List<string> COMPUTER_INFO in _MASTER_COMPUTER_LIST)
            {
                if (!(COMPUTER_INFO == null))
                {
                    line = line + COMPUTER_INFO[0] + "█"; // Button #
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[1]) + "█"; // Department
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[2]) + "█"; // Computer Name
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[3]) + "█"; // Computer IP
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[4]) + "█"; // Main Login Name
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[5]) + "█"; // Password
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[6].Length > 1 ? COMPUTER_INFO[6].Trim(Convert.ToChar("▄")) : "") + "█"; // Hardware
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[7].Length > 1 ? COMPUTER_INFO[7].Trim(Convert.ToChar("▄")) : "") + "█"; // Software
                    line = line + _MASTER_CRYPTSCRIPT.Encrypt(COMPUTER_INFO[8].Length > 1 ? COMPUTER_INFO[8].Trim(Convert.ToChar("▄")) : ""); // Changes
                    line = line + Environment.NewLine;

                }
            }
            try
            {
                File.Delete(_INFO_FILE_PATH);

                //if (Write_Time_On_Load == File.GetLastWriteTime(_INFO_FILE_PATH)) // If file hasnt been accessed and changed
                //{

                    using (StreamWriter sw = File.CreateText(_INFO_FILE_PATH)) // Create translator file
                    {
                        sw.Write(line.TrimEnd(("█".ToCharArray())));// + Environment.NewLine);
                        sw.Close();
                    }
                //}
            }
            catch
            {
                // Cannot overwrite computer info file
            }


            Set_Button_Tooltip(true);
        }

        public int _PARENT_SEARCH_ITEMS(string search_string)
        {
            _SEARCH_COUNT = 0;
            for (int i = 0; i < _COMPUTER_COUNT; i++)
            {
                _SEARCH_TOGGLE_LIST[i] = "0";
            }
            if (search_string.Length > 0)
            {
                int index = 0;
                foreach (List<string> g in _MASTER_COMPUTER_LIST)
                {
                    foreach (string info in g.GetRange(1, g.Count - 1))
                    {
                        if (info.ToLower().Contains(search_string.ToLower()))
                        {
                            _SEARCH_TOGGLE_LIST[index] = "1";
                        }
                    }
                    index++;
                }
            }
            else
            {
            }
            Set_Button_Tooltip(true);
            //search_count_box.ForeColor = _SEARCH_COUNT > 0 ? Color.Green : Color.Red;
            search_count_box.Visible = (_SEARCH_COUNT > 0 ? true : false);
            search_count_box.Text = _SEARCH_COUNT > 0 ? _SEARCH_COUNT.ToString() + " item(s) found" : "";
            search_count_box.ForeColor = _SEARCH_COUNT > 0 ? Color.Green : Color.Red;
            return _SEARCH_COUNT;
        }

        // Allow public modification of MASTER COMPUTER LIST
        public void _MODIFY_RULE(List<string> _CHANGE_LIST, int index)
        {
            _MASTER_COMPUTER_LIST[index] = _CHANGE_LIST;
        }

        public void _RESET_INACTIVITY()
        {
            this._INACTIVE_COUNTER = this._TIMEOUT;
        }

        // MASTER Button Function -> Determines what happens when any buttons are pressed
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (ENABLE_STATION_DRAG || ENABLE_STATION_DEL)
            {
                if (ENABLE_STATION_DEL)
                {
                    Set_Button_Tooltip(true);
                    btn = (Button)sender;
                    Paint_Circle((Button)sender);

                    Button_List[Convert.ToInt32(btn.Text)].BackColor = Color.Red;
                    Button_List[Convert.ToInt32(btn.Text)].ForeColor = Color.Red;

                    DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete this station? (Station name: '" + _MASTER_COMPUTER_LIST[Convert.ToInt32(btn.Text)][2] + "')", "", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
                        int ref_btn_num = Convert.ToInt32(_MASTER_COMPUTER_LIST[Convert.ToInt32(btn.Text)][0]);
                        Button_List[ref_btn_num].Dispose();
                        Button_List.RemoveAt(ref_btn_num);
                        _MASTER_COMPUTER_LIST.RemoveAt(ref_btn_num);
                        _SEARCH_TOGGLE_LIST.RemoveAt(ref_btn_num);
                        //_COMPUTER_COUNT--;
                        for (int i = 0; i < _MASTER_COMPUTER_LIST.Count; i++)
                        {
                            if (Convert.ToInt32(_MASTER_COMPUTER_LIST[i][0]) > ref_btn_num)
                            {
                                _MASTER_COMPUTER_LIST[i][0] = (Convert.ToInt32(_MASTER_COMPUTER_LIST[i][0]) - 1).ToString();
                            }
                        }
                        _MASTER_DYNAMIC_BUTTON_DICT.Remove(ref_btn_num.ToString());
                        foreach (KeyValuePair<string, List<string>> t in _MASTER_DYNAMIC_BUTTON_DICT)
                        {
                            temp.Add((Convert.ToInt32(t.Key) > ref_btn_num ? Convert.ToInt32(t.Key) - 1 : Convert.ToInt32(t.Key)).ToString(), t.Value);
                        }

                        _MASTER_DYNAMIC_BUTTON_DICT = temp;

                        #region Refresh setup list string
                        string temp11 = string.Empty;
                        foreach (KeyValuePair<string, List<string>> entry in _MASTER_DYNAMIC_BUTTON_DICT)
                        {
                            temp11 = temp11 + entry.Key + "~" + entry.Value[0] + "~" + entry.Value[1] + "▄";
                        }
                        #endregion

                        _MASTER_SETUP_LIST[4] = temp11.Trim(Convert.ToChar("▄"));

                        Populate_Button_Dictionary();
                        Place_Dynamic_Buttons();
                        ToolTip1.Dispose();
                        Set_Button_Tooltip(false);
                        //ENABLE_STATION_DEL = false;
                        Management_Button_List.ForEach(item => item.Enabled = true);
                        PAINT_X = 0;
                        PAINT_Y = 0;
                        Invalidate();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        for (int i = 0; i < _COMPUTER_COUNT; i++)
                        {
                            _SEARCH_TOGGLE_LIST[i] = "0";
                        }
                        ToolTip1.Dispose();
                        Set_Button_Tooltip(false);
                        PAINT_X = 0;
                        PAINT_Y = 0;
                        Invalidate();
                    }
                }
            }
            else
            {
                _INACTIVE_COUNTER = _TIMEOUT;
                if (!_Pending_Tranfer)
                {
                    if (!USER_INFO_BOX_OPEN)
                    {
                        btn = (Button)sender;
                        List<string> _TRANSFER_LIST = _MASTER_COMPUTER_LIST[Convert.ToInt32(btn.Text)];
                        User_Info G = new User_Info(this, _TRANSFER_LIST, Convert.ToInt32(btn.Text), "Hardware", true);//(_MASTER_LOGIN_EMPLOYEE_NUMBER == "10577" || _MASTER_LOGIN_EMPLOYEE_NUMBER == "10403"));

                        //Paint_Circle(btn);
                        G.Show();
                        USER_INFO_BOX_OPEN = true;
                    }
                }
                else
                {
                    Set_Button_Tooltip(true);
                    btn = (Button)sender;
                    Paint_Circle((Button)sender);

                    Button_List[Convert.ToInt32(btn.Text)].BackColor = Color.Red;
                    Button_List[Convert.ToInt32(btn.Text)].ForeColor = Color.Red;
                    _Tranfer_Index = Convert.ToInt32(btn.Text);
                    //_Pending_Tranfer = false;
                }
            }
        }

        /// <summary>
        /// Returns NULL if cannot add hardware (mainly used for inventory transfer)
        /// </summary>
        /// <param name="item_desc"></param>
        /// <param name="index"></param>
        /// <param name="return_string_index"></param>
        /// <returns></returns>
        public string Add_Hardware(string item_desc, int index, int return_string_index=0)
        {
            if (index >= 0)
            {
                _MASTER_COMPUTER_LIST[index][6] = _MASTER_COMPUTER_LIST[index][6] + (_MASTER_COMPUTER_LIST[index][6].Length > 1 ? "▄" : "") + item_desc;
                return _MASTER_COMPUTER_LIST[index][return_string_index];
            }
            else
            {
                return "null";
            }

        }

        public void Set_Button_Tooltip(bool skip_tooltip=false)
        {
            ToolTip1.Dispose();
            ToolTip1 = new ToolTip();
            for (int i = 0; i < Button_List.Count; i++)
            {
                if (_MASTER_COMPUTER_LIST[i] == null)
                {
                }
                else
                {
                    //if (!skip_tooltip)
                    if (true)
                    {
                        ToolTip1.InitialDelay = 1;
                        ToolTip1.ReshowDelay = 1;
                        ToolTip1.SetToolTip(this.Button_List[i], (_RETRIEVE_USER_DATA(i, 2).Length > 0 ? _RETRIEVE_USER_DATA(i, 2) + " " : "") + "(" + _RETRIEVE_USER_DATA(i, 1) + ")");
                    }
                    if (_SEARCH_TOGGLE_LIST[i] == "1")
                    {
                        Button_List[i].BackColor = Color.Red;
                        Button_List[i].ForeColor = Color.Red;
                        _SEARCH_COUNT++;
                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "SHOP-MACHINE")
                    {
                        Button_List[i].BackColor = Color.LightGray;
                        Button_List[i].ForeColor = Color.LightGray;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "CAD")
                    {
                        Button_List[i].BackColor = Color.LightGreen;
                        Button_List[i].ForeColor = Color.LightGreen;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "CAM")
                    {
                        Button_List[i].BackColor = Color.LightYellow;
                        Button_List[i].ForeColor = Color.LightYellow;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "Sales")
                    {
                        Button_List[i].BackColor = Color.LightPink;
                        Button_List[i].ForeColor = Color.LightPink;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "Management")
                    {
                        Button_List[i].BackColor = Color.LightCyan;
                        Button_List[i].ForeColor = Color.LightCyan;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "SHOP-PC")
                    {
                        Button_List[i].BackColor = Color.LightSlateGray;
                        Button_List[i].ForeColor = Color.LightSlateGray;

                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "IT")
                    {
                        Button_List[i].BackColor = Color.Orange;
                        Button_List[i].ForeColor = Color.Orange;
                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "Printer")
                    {
                        Button_List[i].BackColor = Color.Cyan;
                        Button_List[i].ForeColor = Color.Cyan;
                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "Server")
                    {
                        Button_List[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#BCA9F5");//Color.MediumSpringGreen;
                        Button_List[i].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BCA9F5");//Color.MediumSpringGreen;
                    }
                    else if (_RETRIEVE_USER_DATA(i, 1) == "Other")
                    {

                        Button_List[i].BackColor = Color.PeachPuff;// System.Drawing.ColorTranslator.FromHtml("#FBFBFB");
                        Button_List[i].ForeColor = Color.PeachPuff;// System.Drawing.ColorTranslator.FromHtml("#FBFBFB");
                    }
                    else
                    {
                        Button_List[i].BackColor = System.Drawing.SystemColors.ButtonFace;
                        Button_List[i].ForeColor = System.Drawing.SystemColors.ButtonFace;
                    }
                }
            }
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            search_count_box.Visible = true;
            _SEARCH_COUNT = 0;
            _RESET_INACTIVITY();
            for (int i = 0; i < _SEARCH_TOGGLE_LIST.Count(); i++)
            {
                _SEARCH_TOGGLE_LIST[i] = "0";
            }
            if (search_box.Text.Length > 0)
            {
                int index = 0;
                foreach (List<string> g in _MASTER_COMPUTER_LIST)
                {
                    foreach (string info in g.GetRange(1, g.Count - 1))
                    {
                        if (info.ToLower().Contains(search_box.Text.ToLower()))
                        {
                            _SEARCH_TOGGLE_LIST[index] = "1";
                        }
                    }
                    index++;
                }
            }
            if (search_box.Text.ToUpper() == "IAMADMIN")
            {
                search_box.ForeColor = Color.White;
                label1.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
                //invisible_button.Size = new Size(1000, 1000);
                _ENABLE_ADMINISTRATIVE_COMMANDS = true;
            }
            else if (_ENABLE_ADMINISTRATIVE_COMMANDS)
            {
                search_box.ForeColor = Color.Black;
                _ENABLE_ADMINISTRATIVE_COMMANDS = false;
                label1.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
            }
            Set_Button_Tooltip(true);
            search_count_box.Text = search_box.Text.Length > 0 ? _SEARCH_COUNT.ToString() + " item(s) found" : "";
            search_count_box.ForeColor = _SEARCH_COUNT > 0 ? Color.Green : Color.Red;
        }

        private void button213_Click(object sender, EventArgs e)
        {
            string Translator_Rule_File_Path = "";
            if (test)
                Translator_Rule_File_Path = Directory.GetCurrentDirectory() + "\\ITManagement_Config.ini";
            else
                Translator_Rule_File_Path = "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini";
            Process.Start(Translator_Rule_File_Path);
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (!ADVANCED_SEARCH_BOX_OPEN)
            {
                Advanced_Search AS = new Advanced_Search(this, _MASTER_COMPUTER_LIST);
                AS.Show();
                ADVANCED_SEARCH_BOX_OPEN = true;
            }
        }

        public Button Get_Button(int button_text)
        {
            foreach (Button b in Button_List)
            {
                if (b.Text.ToString() == button_text.ToString())
                {
                    return b;
                }
            }
            return new Button();
        }

        private void DrawFocus(int x, int y)
        {
            this.Invalidate();
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                x, y, 150, 150);
            graphics.DrawEllipse(System.Drawing.Pens.Red, rectangle);
            //graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
            this.Invalidate();
        }

        public void Paint_Circle(Button btn, string size_config = "")
        {
            Size printer_size = new System.Drawing.Size(10, 10);
            Size shop_pc_size = new System.Drawing.Size(20, 20);
            Size regular_size = new System.Drawing.Size(31, 27);

            if (size_config.Length > 0)
            {
                btn.Size = size_config == "small" ? printer_size : size_config == "medium" ? shop_pc_size : regular_size;
            }

            PAINT_ELLIPSES_SHRINK_FACTOR = (btn.Size.Width > 15 ? btn.Size.Width > 21 ? 0 : 12 : 25);
            PAINT_X = btn.Location.X - btn.Size.Width / 2 + (btn.Size.Width > 15 ? btn.Size.Width > 21 ? 4 : -1 : -4);
            PAINT_Y = btn.Location.Y - btn.Size.Height / 2 + (btn.Size.Width > 15 ? btn.Size.Width > 21 ? 0 : 0 : -4);
            if (PAINT_X > 0 && PAINT_Y > 0)
            {
                this.Invalidate();
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (PAINT_X > 0 && PAINT_Y > 0)
            {
                Graphics g = e.Graphics;
                g.DrawEllipse(new Pen(Color.Green, 4 + (PAINT_ELLIPSES_SHRINK_FACTOR > 0 ? 0 : 2)), PAINT_X, PAINT_Y, 50 - PAINT_ELLIPSES_SHRINK_FACTOR, 50 - PAINT_ELLIPSES_SHRINK_FACTOR);
            }
        }

        public int Get_Inventory_Index()
        {
            foreach (List<string> info in _MASTER_COMPUTER_LIST) // Error in default, find inventory
            {
                if (info[1] == "Inventory")
                {
                    return Convert.ToInt32(info[0]);
                }
            }
            return -1;
        }

        // Inventory button
        private void button214_Click(object sender, EventArgs e)
        {
            if (!_Inventory_Open)
            {
                _Inventory_Open = true;
                //Inventory IV = new Inventory(this, _MASTER_COMPUTER_LIST[214], 214, _MASTER_LOGIN_EMPLOYEE_NUMBER); // 214 is inventory list INDEX
                Inventory IV = new Inventory(this, _MASTER_SETUP_LIST[1], _MASTER_LOGIN_EMPLOYEE_NUMBER); // 214 is inventory list INDEX
                IV.Show(this);
            }
        }

        private void info_button_Click(object sender, EventArgs e)
        {
            if (!_Information_Open)
            {
                IT_Information ITI = new IT_Information(this, _MASTER_SETUP_LIST[0], _MASTER_LOGIN_EMPLOYEE_NUMBER);
                ITI.Show(this);
                _Information_Open = true;
            }
        }

        private void log_button_Click(object sender, EventArgs e)
        {
            Action_Log AL = new Action_Log(this, _MASTER_SETUP_LIST[2], _MASTER_LOGIN_EMPLOYEE_NUMBER);
            AL.ShowDialog(this);
            //_APPEND_TO_SETUP_INFORMATION(2, "Action log viewed by '" + _EMPLOYEE_LIST[_MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
        }

        private void button214_Click_1(object sender, EventArgs e)
        {
            if (!_SETTINGS_OPEN)
            {
                Settings Settings = new Settings(this, _MASTER_SETUP_LIST[3], _MASTER_LOGIN_EMPLOYEE_NUMBER);
                Settings.ShowDialog(this);
                _SETTINGS_OPEN = false;
            }
        }

        private void Set_Database_Credentials()
        {
            Dictionary<string, string> Global_Settings = new Dictionary<string, string>();

            bool db_failed = false;
            try
            {
                Get_Employee_List();
            }
            catch
            {
                db_failed = true;
            }

            if (_MASTER_SETUP_LIST[3].Length > 0)
            {
                string[] temp = _MASTER_SETUP_LIST[3].Split(new string[] { "▄" }, StringSplitOptions.None);
                string[] settings_list = { "backup_location", "db_login", "db_password", "db_default" };

                // Store empty dictionaries for initial setup
                foreach (string g in settings_list) Global_Settings.Add(g, "");

                foreach (string g in temp)
                {
                    if (g.Length > 0)
                    {
                        string[] temp2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                        Global_Settings[temp2[0]] = temp2[1];                                                                               
                    }
                }
                database.Set_Credentials(Global_Settings["db_login"], Global_Settings["db_password"], Global_Settings["db_default"]);
                if (db_failed) Get_Employee_List();
            }
            else
            {
                MessageBox.Show("Please go to settings and setup database information before proceeding to prevent query errors");
            }
        }

        private void edit_station_button_Click(object sender, EventArgs e)
        {


            if (!USER_INFO_BOX_OPEN && !_PURCHASE_HISTORY_OPEN && !_Inventory_Open && !_Information_Open && !_Action_Log_Open && !_SETTINGS_OPEN && !ADVANCED_SEARCH_BOX_OPEN && !_PURCHASE_HISTORY_OPEN)
            {
                if (!_Edit_Stations_Open)
                {
                    Edit_Stations ES = new Edit_Stations(this);
                    ES.Show(this);
                    ES.Owner = this;
                    _Edit_Stations_Open = true;
                }
            }
            else
            {
                MessageBox.Show("Please close all dialog boxes before proceeding");
            }
        }

        private void purchase_history_button_Click(object sender, EventArgs e)
        {
            if (!_PURCHASE_HISTORY_OPEN)
            {
                PurchaseHistory PH = new PurchaseHistory(this);
                PH.Show(this);
                PH.Owner = this;
                _PURCHASE_HISTORY_OPEN = true;
            }
        }

        private void add_reminder_Click(object sender, EventArgs e)
        {
            if (!_ENABLE_ADMINISTRATIVE_COMMANDS)
            {
                AddReminder AR = new AddReminder(this);
                AR.Show(this);
            }
            else
            {
                Repository RP = new Repository(this);
                RP.Show(this);
                // Show Repository
            }
            //log_str.Trim(Convert.ToChar("▄")));
        }

        private void Office_Load(object sender, EventArgs e)
        {
            try
            {
                this.MouseDown += new MouseEventHandler(Office_MouseDown);
                Reminders reminder = new Reminders(this);
                reminder.Show(this);
            }
            catch
            {
            }
        }


        private void Office_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rc = RectangleDrawer.Draw(this);
            Console.WriteLine(rc.ToString());
        }
    }

    public static class RectangleDrawer
    {
        private static Form mMask;
        private static Point mPos;
        public static Rectangle Draw(Form parent)
        {
            // Record the start point
            mPos = parent.PointToClient(Control.MousePosition);
            // Create a transparent form on top of <frm>
            mMask = new Form();
            mMask.FormBorderStyle = FormBorderStyle.None;
            mMask.BackColor = Color.Magenta;
            mMask.TransparencyKey = mMask.BackColor;
            mMask.ShowInTaskbar = false;
            mMask.StartPosition = FormStartPosition.Manual;
            mMask.Size = parent.ClientSize;
            mMask.Location = parent.PointToScreen(Point.Empty);
            mMask.MouseMove += MouseMove;
            mMask.MouseUp += MouseUp;
            mMask.Paint += PaintRectangle;
            mMask.Load += DoCapture;
            // Display the overlay
            mMask.ShowDialog(parent);
            // Clean-up and calculate return value
            mMask.Dispose();
            mMask = null;
            Point pos = parent.PointToClient(Control.MousePosition);
            int x = Math.Min(mPos.X, pos.X);
            int y = Math.Min(mPos.Y, pos.Y);
            int w = Math.Abs(mPos.X - pos.X);
            int h = Math.Abs(mPos.Y - pos.Y);
            return new Rectangle(x, y, w, h);
        }

        private static void DoCapture(object sender, EventArgs e)
        {
            // Grab the mouse
            mMask.Capture = true;
        }

        private static void MouseMove(object sender, MouseEventArgs e)
        {
            // Repaint the rectangle
            mMask.Invalidate();
        }

        private static void MouseUp(object sender, MouseEventArgs e)
        {
            // Done, close mask
            mMask.Close();
        }

        private static void PaintRectangle(object sender, PaintEventArgs e)
        {
            // Draw the current rectangle
            Point pos = mMask.PointToClient(Control.MousePosition);
            using (Pen pen = new Pen(Brushes.Black))
            {
                pen.DashStyle = DashStyle.Dot;
                e.Graphics.DrawLine(pen, mPos.X, mPos.Y, pos.X, mPos.Y);
                e.Graphics.DrawLine(pen, pos.X, mPos.Y, pos.X, pos.Y);
                e.Graphics.DrawLine(pen, pos.X, pos.Y, mPos.X, pos.Y);
                e.Graphics.DrawLine(pen, mPos.X, pos.Y, mPos.X, mPos.Y);
            }
        }
    }
}
