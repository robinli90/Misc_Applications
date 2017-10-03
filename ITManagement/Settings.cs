using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ITManagement
{
    public partial class Settings : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _parent.Focus();
            _parent._SETTINGS_OPEN = false;
            this.Dispose();
        }

        Office _parent;
        private string _EMP_NUM = string.Empty;

        Dictionary<string, string> Global_Settings = new Dictionary<string, string>();

        public Settings(Office parent, string settings_string, string employee_number)
        {
            InitializeComponent();
            _EMP_NUM = employee_number;
            _parent = parent;

            string[] temp = settings_string.Split(new string[] { "▄" }, StringSplitOptions.None);

            string[] settings_list = { "backup_location", "db_login", "db_password", "db_default" };

            // Store empty dictionaries for initial setup
            foreach (string g in settings_list) Global_Settings.Add(g, "");

            // Populate as more settings are needed

            foreach (string g in temp)
            {
                if (g.Length > 0)
                {
                    string[] temp2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                    Global_Settings[temp2[0]] = temp2[1];
                }
            }

            db_default_db.Items.Add("CMSDAT");
            db_default_db.Items.Add("PRODTEST");
            db_default_db.Items.Add("DECADE_MARKHAM");
            db_default_db.Items.Add("DECADE_MICHIGAN");
            db_default_db.Items.Add("DECADE_TEXAS");
            db_default_db.Items.Add("DECADE_COLOMBIA");

            backup_location_box.Text = Global_Settings["backup_location"];
            db_login.Text = Global_Settings["db_login"];
            db_password.Text = Global_Settings["db_password"];
            db_default_db.Text = Global_Settings["db_default"];

            if (backup_location_box.Text.Length < 5) backup_location_box.ReadOnly = false;

        }

        private void Store_Information()
        {
            string return_string = "";
            foreach (KeyValuePair<string, string> g in Global_Settings)
            {
                return_string = return_string + g.Key + "~" + g.Value + "▄";
            }
            _parent._STORE_SETUP_INFORMATION(3, return_string.Trim(Convert.ToChar("▄")));
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (db_login.Text.Contains("~") || db_password.Text.Contains("~"))
            {
                MessageBox.Show("Error: Invalid character detected: '~'");
            }
            else
            {
                Global_Settings["backup_location"] = backup_location_box.Text;
                Global_Settings["db_login"] = db_login.Text;
                Global_Settings["db_password"] = db_password.Text;
                Global_Settings["db_default"] = db_default_db.Text;

                _parent.backup_directory_path = backup_location_box.Text;

                _parent.database.Set_Credentials(db_login.Text, db_password.Text, db_default_db.Text);

                string query = "select top 1 * from d_user";

                try
                {
                    _parent.database.Open();
                    _parent.reader = _parent.database.RunQuery(query);
                    _parent.reader.Read();
                    Store_Information();
                    _parent.reader.Close();
                    _parent.database.connection.Close();
                    this.Close();
                    this.Dispose();
                }
                catch
                {
                    DialogResult dialogResult = MessageBox.Show("Error in database credentials. Do you wish to save this anyway?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Store_Information();
                        _parent._APPEND_TO_SETUP_INFORMATION(2, "Loaded database information with ERROR by " + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER]);
                        this.Close();
                        this.Dispose();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
            }
        }

        private void load_backup_Click(object sender, EventArgs e)
        {
            string file_path = string.Empty;
            OpenFileDialog file = new OpenFileDialog();
            if (Directory.Exists(Global_Settings["backup_location"]))
                file.InitialDirectory = Global_Settings["backup_location"];
            file.Title = "Load IT Management Config Backup";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                file_path = file.FileName;
                if (file_path.Contains("20") &&file_path.Contains("_Hr-") && file_path.Contains(".ini"))
                {
                    DialogResult dialogResult = MessageBox.Show("You have selected the file: " + Environment.NewLine + file_path + Environment.NewLine + "Do you wish to load this backup file? This will overwrite all your current unsaved changes and overwrite the main config.ini file.", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        _parent._Reload_Config_Info(file_path);
                        File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\Development\\Robin\\ITManagement_Config.ini", true);

                        _parent._APPEND_TO_SETUP_INFORMATION(2, "Loaded configuration from path '" + file_path + "' by " + _parent._EMPLOYEE_LIST[_parent._MASTER_LOGIN_EMPLOYEE_NUMBER]);
                        this.Close();
                        this.Dispose();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Invalid backup file detected. Please select the appropriate backup .ini file");
                }
            }

        }

        private void decrypt_button_Click(object sender, EventArgs e)
        {
            if (dt.Text.Length > 0)
            {
                DecryptingWindow DW = new DecryptingWindow(dt.Text);
                DW.Show();
            }
            else
            {
                MessageBox.Show("Missing decryption key");
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (_parent.search_box.Text != "iamadmin")
            {
                this.Size = new Size(498, 195);
            }
        }
    }
}
