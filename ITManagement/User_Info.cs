using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace ITManagement
{
    public partial class User_Info : Form
    {

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Override original form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_COMPUTER_INFO[1] != deparment.Text) _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + " department information updated ('" + _COMPUTER_INFO[1] + "' --> '" + deparment.Text + "') by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
            if (_COMPUTER_INFO[3] != computer_ip.Text) _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + " computer IP information updated ('" + _COMPUTER_INFO[3] + "' --> '" + computer_ip.Text + "') by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
            if (_COMPUTER_INFO[4] != main_login_name.Text) _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + " login name information updated ('" + _COMPUTER_INFO[4] + "' --> '" + main_login_name.Text + "') by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
            if (_COMPUTER_INFO[5] != password.Text) _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + " login password information updated ('" + _COMPUTER_INFO[5] + "' --> '" + password.Text + "') by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            _parent.USER_INFO_BOX_OPEN = false;
            //_RUN_TEMP_SCRIPT(); // TEMP SCRIPT
            Update_Info_List();
            _parent._MODIFY_RULE(_COMPUTER_INFO, _SELECTED_INDEX);
            _parent._STORE_COMPUTER_INFO();
            _parent.Focus();
            //this.Close();
            this.Dispose();
        }

        bool enable_inventory_modification = false;

        internal string _OPEN_REFERENCE_PATH = _NCMACHS_PATH;
        internal static string _NCMACHS_PATH = "\\\\10.0.0.8\\shopdata\\ncmachs\\";
        internal static string _IT_PATH = "\\\\10.0.0.8\\shopdata\\development\\";

        List<string> _COMPUTER_INFO = new List<string>();
        List<string> HARDWARE = new List<string>();
        List<string> SOFTWARE = new List<string>();
        List<string> CHANGES = new List<string>();
        int _SELECTED_INDEX = 0;
        Office _parent;
        string VIEW_MODE = "Hardware";
        bool VIEW_TIME_STAMPS = false;

        #region TEXT-WRAP List box
        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                e.ItemHeight = (int)e.Graphics.MeasureString(detail_list.Items[e.Index].ToString(), detail_list.Font, detail_list.Width).Height;
            }
            catch { }
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(detail_list.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
            catch { }
        }
        #endregion

        public User_Info(Office parent_form, List<string> _INFO_TRANSFER, int button_index, string _LOADING_VIEW_MODE = "Hardware", bool is_admin=false)
        {
            InitializeComponent();

            _parent = parent_form;
            // If there are information to transfer, transfer it. Else, create new file
            if (!(_INFO_TRANSFER == null))
                _COMPUTER_INFO = _INFO_TRANSFER;
            else
                for (int i = 0; i < 9; i++)
                    _COMPUTER_INFO.Add("");

            _SELECTED_INDEX = button_index;

            VIEW_MODE = _LOADING_VIEW_MODE;

            // Add items to dropdown menu
            deparment.Items.Add("CAD");
            deparment.Items.Add("CAM");
            deparment.Items.Add("IT");
            deparment.Items.Add("Management");
            deparment.Items.Add("Other");
            deparment.Items.Add("Printer");
            deparment.Items.Add("Sales");
            deparment.Items.Add("Server");
            deparment.Items.Add("SHOP-MACHINE");
            deparment.Items.Add("SHOP-PC");

            if (!is_admin)
            {
                remote_button.Enabled = false;
                password.Visible = false;
                password.Enabled = false;
                decoy_password_box.Visible = true;
            }

            // Text wrap list box
            detail_list.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            detail_list.MeasureItem += lst_MeasureItem;
            detail_list.DrawItem += lst_DrawItem;

            // Display current information
            if (_COMPUTER_INFO[1].Length > 0)
                deparment.Text = _COMPUTER_INFO[1];
            else
                deparment.Text = "Printer";

            computer_name.Text = _COMPUTER_INFO[2];

            if (deparment.Text == "SHOP-MACHINE")
            {
                _OPEN_REFERENCE_PATH = _NCMACHS_PATH;
            }
            else if (deparment.Text == "IT")
            {
                _OPEN_REFERENCE_PATH = _IT_PATH;
            }
                
            if (Directory.Exists(_OPEN_REFERENCE_PATH + computer_name.Text + "\\"))
                open_directory_button.Enabled = true;

            this.Text = _COMPUTER_INFO[2];
            computer_ip.Text = _COMPUTER_INFO[3];
            main_login_name.Text = _COMPUTER_INFO[4];
            password.Text = _COMPUTER_INFO[5];

            HARDWARE = _COMPUTER_INFO[6].Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
            SOFTWARE = _COMPUTER_INFO[7].Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
            CHANGES = _COMPUTER_INFO[8].Split(new string[] { "▄" }, StringSplitOptions.None).ToList();
            if (HARDWARE[0] == "") HARDWARE = new List<string>();
            if (SOFTWARE[0] == "") SOFTWARE = new List<string>();
            if (CHANGES[0] == "") CHANGES = new List<string>();

            // Remove all empty entries
            for (int i = HARDWARE.Count-1; i >= 0; i--)
            {
                if (HARDWARE[i].Length == 0)
                {
                    HARDWARE.RemoveAt(i);
                    i--;
                }
            }
            for (int i = SOFTWARE.Count - 1; i >= 0; i--)
            {
                if (SOFTWARE[i].Length == 0)
                {
                    SOFTWARE.RemoveAt(i);
                    i--;
                }
            }
            for (int i = CHANGES.Count - 1; i >= 0; i--)
            {
                if (CHANGES[i].Length == 0)
                {
                    CHANGES.RemoveAt(i);
                    i--;
                }
            }
            enable_inventory_modification = _parent._ENABLE_ADMINISTRATIVE_COMMANDS;
        }

        private void _RUN_TEMP_SCRIPT()
        {
            for (int i = 0; i < HARDWARE.Count; i++)
            {
                if (HARDWARE[i].Length > 3)
                {
                    HARDWARE[i] = HARDWARE[i].StartsWith("`I`") ? HARDWARE[i] : "`I`" + HARDWARE[i];
                }
            }
        }

        private void Update_Info_List()
        {
            _parent._RESET_INACTIVITY();
            _COMPUTER_INFO[0] = _SELECTED_INDEX.ToString();
            _COMPUTER_INFO[1] = deparment.Text;
            _COMPUTER_INFO[2] = computer_name.Text;
            _COMPUTER_INFO[3] = computer_ip.Text;
            _COMPUTER_INFO[4] = main_login_name.Text;
            _COMPUTER_INFO[5] = password.Text;
            string HARDWARE_LINE = "";
            string SOFTWARE_LINE = "";
            string CHANGES_LINE = "";
            foreach (string HW in HARDWARE) HARDWARE_LINE = HARDWARE_LINE + HW + "▄";
            foreach (string SW in SOFTWARE) SOFTWARE_LINE = SOFTWARE_LINE + SW + "▄";
            foreach (string CH in CHANGES) CHANGES_LINE = CHANGES_LINE + CH + "▄";
            _COMPUTER_INFO[6] = HARDWARE_LINE.Trim("▄".ToCharArray());
            _COMPUTER_INFO[7] = SOFTWARE_LINE.Trim("▄".ToCharArray());
            _COMPUTER_INFO[8] = CHANGES_LINE.Trim("▄".ToCharArray());
        }


        private void hardware_button_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            button1.Visible = true;
            VIEW_MODE = "Hardware";
            detail_group.Text = VIEW_MODE;
            detail_list.Items.Clear();
            hardware_button.BackColor = Color.LightBlue;
            software_button.BackColor = Color.FromKnownColor(KnownColor.Control);
            change_button.BackColor = Color.FromKnownColor(KnownColor.Control);
            if (HARDWARE.Count > 0)
            {
                int index_value = 1;
                string HW = "";
                foreach (string HW2 in HARDWARE)
                {
                    if (HW2.Length > 0)
                    {
                        HW = HW2.StartsWith("`I`") ? HW2.Substring(3) : HW2;
                        string detail_line = "";
                        if (index_value < 10) detail_line = (HARDWARE.Count < 100 ? "0" : "00");
                        else if (index_value < 100) detail_line = (HARDWARE.Count < 100 ? "" : "0");
                        detail_line = detail_line + index_value.ToString() + ")  ";
                        detail_list.Items.Add((detail_line + (HW.Contains("DateStamp") ? (VIEW_TIME_STAMPS ? HW : HW.Substring(0, HW.LastIndexOf("DateStamp") - 2)) : HW)));
                        index_value++;
                    }
                }
            }
        }

        private void software_button_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            _parent._RESET_INACTIVITY();
            VIEW_MODE = "Software";
            detail_group.Text = VIEW_MODE;
            detail_list.Items.Clear();
            software_button.BackColor = Color.LightBlue;
            hardware_button.BackColor = Color.FromKnownColor(KnownColor.Control);;
            change_button.BackColor = Color.FromKnownColor(KnownColor.Control);;
            if (SOFTWARE.Count > 0)
            {
                int index_value = 1;
                foreach (string HW in SOFTWARE)
                {
                    if (HW.Length > 0)
                    {
                        string detail_line = "";
                        if (index_value < 10) detail_line = (SOFTWARE.Count < 100 ? "0" : "00");
                        else if (index_value < 100) detail_line = (SOFTWARE.Count < 100 ? "" : "0");
                        detail_line = detail_line + index_value.ToString() + ")  ";
                        detail_list.Items.Add(detail_line + (HW.Contains("DateStamp") ? (VIEW_TIME_STAMPS ? HW : HW.Substring(0, HW.LastIndexOf("DateStamp") - 2)) : HW));
                        index_value++;
                    }
                }
            }
        }

        private void change_button_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            _parent._RESET_INACTIVITY();
            VIEW_MODE = "Information";
            detail_group.Text = VIEW_MODE;
            detail_list.Items.Clear();
            change_button.BackColor = Color.LightBlue;
            hardware_button.BackColor = Color.FromKnownColor(KnownColor.Control);;
            software_button.BackColor = Color.FromKnownColor(KnownColor.Control);;
            if (CHANGES.Count > 0)
            {
                int index_value = 1;
                foreach (string HW in CHANGES)
                {
                    if (HW.Length > 0)
                    {
                        string detail_line = "";
                        if (index_value < 10) detail_line = (CHANGES.Count < 100 ? "0" : "00");
                        else if (index_value < 100) detail_line = (CHANGES.Count < 100 ? "" : "0");
                        detail_line = detail_line + index_value.ToString() + ")  ";
                        detail_list.Items.Add(detail_line + (HW.Contains("DateStamp") ? (VIEW_TIME_STAMPS ? HW : HW.Substring(0, HW.LastIndexOf("DateStamp") - 2)) : HW));
                        index_value++;
                    }
                }
            }
        }

        // Add information
        private void button4_Click(object sender, EventArgs e)
        {

            _parent._RESET_INACTIVITY();
            if (details.Text.Length > 0)
            {
                details.Text = details.Text + " (DateStamp:" + DateTime.Now.ToString() + ")";
                if (VIEW_MODE == "Hardware") { if (details.Text.Length > 0) HARDWARE.Add(((details.Text.Length > 0) ? details.Text : "")); hardware_button.PerformClick(); }
                if (VIEW_MODE == "Software") { if (details.Text.Length > 0) SOFTWARE.Add(((details.Text.Length > 0) ? details.Text : "")); software_button.PerformClick(); }
                if (VIEW_MODE == "Information") { if (details.Text.Length > 0) CHANGES.Add(((details.Text.Length > 0) ? details.Text : "")); change_button.PerformClick(); }
                _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " updated with '" + details.Text.Substring(0, details.Text.LastIndexOf("DateStamp") - 2) + "' by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                details.Clear();

                Update_Info_List();
                 _parent._MODIFY_RULE(_COMPUTER_INFO, _SELECTED_INDEX);
                _parent._STORE_COMPUTER_INFO();
            }
        }

        //Delete selected
        private void clear_button_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            int sel_index = 0;
            int list_length = detail_list.Items.Count;
            if (detail_list.Items.Count > 0)
            {
                for (int i = list_length - 1; i >= 0; i--)
                {
                    if (detail_list.GetSelected(i) == true)
                    {
                        if (VIEW_MODE == "Hardware") 
                        {
                            if (HARDWARE[i].Contains("`I`") && !enable_inventory_modification)
                            {
                                MessageBox.Show("Cannot remove inventory item");
                            }
                            else
                            {
                                if (HARDWARE.Count > 0)
                                {
                                    //_parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " updated with '" + details.Text.Substring(0, details.Text.LastIndexOf("DateStamp") - 2) + "' by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                                    _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " : '" + (HARDWARE[i].Contains("DateStamp") ? HARDWARE[i].Substring(0, HARDWARE[i].LastIndexOf("DateStamp") - 2) : "") + "' removed by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                                    HARDWARE.RemoveAt(i);
                                }

                            }
                        }
                        if (VIEW_MODE == "Software")
                        {
                            _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " : '" + (SOFTWARE[i].Contains("DateStamp") ? SOFTWARE[i].Substring(0, SOFTWARE[i].LastIndexOf("DateStamp") - 2) : "") + "' removed by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                            if (SOFTWARE.Count > 0) SOFTWARE.RemoveAt(i); 
                        }
                        if (VIEW_MODE == "Information")
                        {
                            _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " : '" + (CHANGES[i].Contains("DateStamp") ? CHANGES[i].Substring(0, CHANGES[i].LastIndexOf("DateStamp") - 2) : "") + "' removed by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                            if (CHANGES.Count > 0) CHANGES.RemoveAt(i); 
                        }
                
                        sel_index = i;
                    }
                }
                Update_Info_List();
                _parent._MODIFY_RULE(_COMPUTER_INFO, _SELECTED_INDEX);
                _parent._STORE_COMPUTER_INFO();
            }
            if (VIEW_MODE == "Hardware") { hardware_button.PerformClick(); }
            if (VIEW_MODE == "Software") { software_button.PerformClick(); }
            if (VIEW_MODE == "Information") { change_button.PerformClick(); }
                

            if (detail_list.Items.Count > 0)
            {
                try
                {
                    detail_list.SetSelected(sel_index, true);
                }
                catch
                {   // Move to end of list
                    detail_list.SetSelected(sel_index - 1, true);
                }
            }
        }

        private void details_KeyPress(object sender, KeyPressEventArgs e)
        {
            _parent._RESET_INACTIVITY();
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        
        private void detail_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _parent._RESET_INACTIVITY();
            int selected_index = this.detail_list.IndexFromPoint(e.Location);
            if (selected_index < 5000)
            {
                try
                {
                    if (VIEW_MODE == "Hardware")
                    {
                        string g = HARDWARE[selected_index].Contains("DateStamp") ? HARDWARE[selected_index].Substring(0, HARDWARE[selected_index].LastIndexOf("DateStamp") - 2) : HARDWARE[selected_index];
                        Clipboard.SetText(g.StartsWith("`I`") ? g.Substring(3) : g);
                    }
                    if (VIEW_MODE == "Software")
                    {
                        string g = SOFTWARE[selected_index].Contains("DateStamp") ? SOFTWARE[selected_index].Substring(0, SOFTWARE[selected_index].LastIndexOf("DateStamp") - 2) : SOFTWARE[selected_index];
                        Clipboard.SetText(g.StartsWith("`I`") ? g.Substring(3) : g);
                    }
                    if (VIEW_MODE == "Information")
                    {
                        string g = CHANGES[selected_index].Contains("DateStamp") ? CHANGES[selected_index].Substring(0, CHANGES[selected_index].LastIndexOf("DateStamp") - 2) : CHANGES[selected_index];
                        Clipboard.SetText(g.StartsWith("`I`") ? g.Substring(3) : g);
                    }
                }
                catch
                {
                }
            }
        }

        private void details2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Close();
            }
        }

        private void show_time_stamps_CheckedChanged(object sender, EventArgs e)
        {
            VIEW_TIME_STAMPS = show_time_stamps.Checked;
            if (VIEW_MODE == "Hardware") { hardware_button.PerformClick(); }
            if (VIEW_MODE == "Software") { software_button.PerformClick(); }
            if (VIEW_MODE == "Information") { change_button.PerformClick(); }
        }

        private void remote_button_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            if (computer_ip.Text.Length > 0)
            {
                try
                {
                    Ping ping = new Ping();
                    IPAddress address = IPAddress.Loopback;
                    PingReply reply = ping.Send(computer_ip.Text);
                    if (reply.Status == IPStatus.Success)
                    {
                        //ping1_text.Text = "Ping: " + reply.RoundtripTime.ToString() + "ms";

                        using (Encrypter ez = new Encrypter())
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to remote to this PC?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string enc_pw = ez.Decrypt("r?Rqaj¡ wxy").Insert(1, "S").Remove(2, 1);
                                string remote_command = "mstsc /f /v:" + computer_ip.Text;

                                Process rdcProcess = new Process();
                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
                                rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/" + computer_ip.Text + " /user:" + main_login_name.Text + " /pass:" + (password.Text == "ADMIN_PASSWORD" ? enc_pw : password.Text);
                                rdcProcess.Start();

                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                                rdcProcess.StartInfo.Arguments = "/v " + computer_ip.Text;// ip or name of computer to connect
                                rdcProcess.Start();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                    else
                    {
                        using (Encrypter ez = new Encrypter())
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to remote to this PC?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string enc_pw = ez.Decrypt("r?Rqaj¡ wxy").Insert(1, "S").Remove(2, 1);
                                string remote_command = "mstsc /f /v:" + computer_ip.Text;

                                Process rdcProcess = new Process();
                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
                                rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/" + computer_ip.Text + " /user:" + main_login_name.Text + " /pass:" + (password.Text == "ADMIN_PASSWORD" ? enc_pw : password.Text);
                                rdcProcess.Start();

                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                                rdcProcess.StartInfo.Arguments = "/v " + computer_ip.Text;// ip or name of computer to connect
                                rdcProcess.Start();
                            }
                        }
                        //MessageBox.Show("The IP chosen is unavailable");
                    }
                }
                catch
                {

                    MessageBox.Show("The IP chosen is unavailable");
                }
            }
            else
            {
                MessageBox.Show("Error: Missing IP - Please provide an IP");
            }
        }

        private void User_Info_Load(object sender, EventArgs e)
        {
            if (VIEW_MODE == "Hardware") { hardware_button.PerformClick(); }
            if (VIEW_MODE == "Software") { software_button.PerformClick(); }
            if (VIEW_MODE == "Information") { change_button.PerformClick(); }
            //hardware_button.PerformClick(); // CHANGE DEFAULT
            password.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void open_directory_button_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(_OPEN_REFERENCE_PATH + computer_name.Text + "\\");
            }
            catch
            {
                MessageBox.Show("Error opening directory");
            }
        }

        private void computer_ip_TextChanged(object sender, EventArgs e)
        {

        }

        private void details_TextChanged(object sender, EventArgs e)
        {
            if (details.Text.EndsWith("~"))
            {
                details.Text = details.Text.Substring(0, details.Text.Length - 1);
                details.SelectionStart = details.Text.Length;
                details.SelectionLength = 0;
            }
        }

        // Void Item
        private void button1_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            if (details.Text.Length > 0)
            {
                int sel_index = 0;
                int list_length = detail_list.Items.Count;
                if (detail_list.Items.Count > 0)
                {
                    for (int i = list_length - 1; i >= 0; i--)
                    {
                        if (detail_list.GetSelected(i) == true && !HARDWARE[i].Contains("(VOIDED: "))
                        {
                            _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " : '" + (HARDWARE[i].Substring(0, HARDWARE[i].LastIndexOf("DateStamp") - 2)).Substring(3) + "' VOIDED (Reason: " + details.Text + ") by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                            string temp = (HARDWARE[i].Contains("DateStamp") ? HARDWARE[i].Substring(0, HARDWARE[i].LastIndexOf("DateStamp") - 1) : HARDWARE[i]);
                            string date = (HARDWARE[i].Contains("DateStamp") ? HARDWARE[i].Substring(HARDWARE[i].LastIndexOf("DateStamp") - 1) : HARDWARE[i]);
                            if (HARDWARE.Count > 0 && !HARDWARE[i].Contains("(VOIDED: ")) HARDWARE[i] = temp + "(VOIDED: " + details.Text + ")" + " " + date;
                            sel_index = i;
                        }
                        else
                        {

                        }
                    }
                    Update_Info_List();
                    _parent._MODIFY_RULE(_COMPUTER_INFO, _SELECTED_INDEX);
                    _parent._STORE_COMPUTER_INFO();
                }
                hardware_button.PerformClick(); 

                if (detail_list.Items.Count > 0)
                {
                    try
                    {
                        detail_list.SetSelected(sel_index, true);
                    }
                    catch
                    {   // Move to end of list
                        detail_list.SetSelected(sel_index - 1, true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please provide a reason below");
            }
        }

        // Transfer to inventory
        private void button2_Click(object sender, EventArgs e)
        {
            _parent._RESET_INACTIVITY();
            int sel_index = 0;
            int list_length = detail_list.Items.Count;
            if (!_parent._Inventory_Open)
            {
                if (detail_list.Items.Count > 0)
                {
                    for (int i = list_length - 1; i >= 0; i--)
                    {
                        if (detail_list.GetSelected(i) == true)
                        {
                            if (VIEW_MODE == "Hardware")
                            {
                                if (HARDWARE[i].Contains("`I`"))
                                {
                                    if (HARDWARE.Count > 0 && details.Text.Length > 0)
                                    {
                                        string temp = HARDWARE[i].Contains("`I`") ? HARDWARE[i].Substring(3) : HARDWARE[i];
                                        string desc = temp.Contains("DateStamp:") ? temp.Substring(0, temp.IndexOf("DateStamp:") - 2) : temp;
                                        desc = desc + " (Transfer from: " + _COMPUTER_INFO[2] + "; Reason: " + details.Text + ")~1~" + (temp.Substring(temp.IndexOf("DateStamp:") + 10).Split(new string[] { " " }, StringSplitOptions.None))[0];
                                        //_parent.Add_Hardware("`I`" + desc.Substring(0, desc.Length), 214);
                                        _parent._APPEND_TO_SETUP_INFORMATION(1, "▄`I`" + desc.Substring(0, desc.Length));
                                        string temp3 = (HARDWARE[i].Substring(0, HARDWARE[i].LastIndexOf("DateStamp") - 2)).Substring(3);
                                        _parent._APPEND_TO_SETUP_INFORMATION(2, "User '" + computer_name.Text + "' " + VIEW_MODE.ToLower() + " : '" + (temp3.Contains("`I`") ? temp3.Substring(3) : temp3) + "' transfered to INVENTORY by '" + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER] + "'");
                                        HARDWARE.RemoveAt(i);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please provide a reason below");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Item selected is not labelled as an inventory item");
                                }
                            }
                            if (VIEW_MODE == "Software") { if (SOFTWARE.Count > 0) SOFTWARE.RemoveAt(i); }
                            if (VIEW_MODE == "Information") { if (CHANGES.Count > 0) CHANGES.RemoveAt(i); }

                            sel_index = i;
                        }
                    }
                    Update_Info_List();
                    _parent._MODIFY_RULE(_COMPUTER_INFO, _SELECTED_INDEX);
                    _parent._STORE_COMPUTER_INFO();
                }
                if (VIEW_MODE == "Hardware") { hardware_button.PerformClick(); }
                if (VIEW_MODE == "Software") { software_button.PerformClick(); }
                if (VIEW_MODE == "Information") { change_button.PerformClick(); }


                if (detail_list.Items.Count > 0)
                {
                    try
                    {
                        detail_list.SetSelected(sel_index, true);
                    }
                    catch
                    {   // Move to end of list
                        detail_list.SetSelected(sel_index - 1, true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please close inventory window before trying to transfer item to inventory");
            }
        }

        private void computer_name_TextChanged(object sender, EventArgs e)
        {
            _COMPUTER_INFO[2] = computer_name.Text;
        }

        private void main_login_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void decoy_password_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void deparment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void detail_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
