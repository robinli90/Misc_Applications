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
using Databases;
using System.Data.Odbc;
using System.Runtime.InteropServices;

namespace Exco_Hold_and_Release
{
    public partial class Checklist : Form
    {
        const int MF_BYPOSITION = 0x400;

        [DllImport("User32")]

        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]

        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]

        private static extern int GetMenuItemCount(IntPtr hWnd);

        // #DEPARTMENT|SUB_CATEGORY|CHECKLIST_ITEM|UNIQUE_IDENTIFIER|'NA'[Optional: N/A checkbox]
        public Dictionary<string, List<CheckList_Item>> Sales_Dictionary = new Dictionary<string, List<CheckList_Item>>();
        public Dictionary<string, List<CheckList_Item>> CAD_Dictionary = new Dictionary<string, List<CheckList_Item>>();
        public Dictionary<string, List<CheckList_Item>> CAM_Dictionary = new Dictionary<string, List<CheckList_Item>>();
        public List<CheckBox> Header_Check_List = new List<CheckBox>();

        // INFO LIST:   #INFO|INFO_TYPE|INFO_DESC
        public Dictionary<string, string> Info_List = new Dictionary<string,string>();

        // #UNIQUE IDENTIFIER, REFERENCE CHECKLIST_ITEM
        public Dictionary<int, CheckList_Item> Mapping = new Dictionary<int, CheckList_Item>();

        // #MAP|UNIQUE_IDENTIFIER#1->UNIQUE_IDENTIFIER#2|MAPPING_OPTIONS(flags) 
        public List<string> Mapping_List = new List<string>();

        // Notes textbox
        public TextBox T = new TextBox();

        //public Dictionary<int, CheckList_Item> CAD_Map = new Dictionary<int, CheckList_Item>();
        //public Dictionary<int, CheckList_Item> CAM_Map = new Dictionary<int, CheckList_Item>();

        // Employee master list
        public Dictionary<string, string> _EMPLOYEE_LIST = new Dictionary<string, string>();

        public string Order_Number = string.Empty;
        public string VIEW_MODE = string.Empty;
        public bool MANAGEMENT_MODE = false;
        public bool EDIT_MODE = false;

        public string CAD_CAM_PATH = "";

        public int Control_Width = 200;

        //public bool Print_Check_Boxes = false;
        public bool Print_Check_Boxes = true;
        public bool ShowAll = false;

        //public string Check_Style = "X";
        public string Check_Style = "Check";

        bool Disable_Edit = false;

        public Checklist(string DEPARTMENT, string order_number, string employee_number, string release_print, string VIEW_MODE2 = "HOLD")
        {
            VIEW_MODE = DEPARTMENT;
            if (VIEW_MODE == "ADMIN")
            {
                Print_Check_Boxes = true;
                MANAGEMENT_MODE = true;
            }
            if (MANAGEMENT_MODE)
            {
                List<string> files = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released", "*" + order_number + "*").ToList();
                if (files.Count == 0)
                {
                    files = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\On-Hold", "*" + order_number + "*").ToList();
                }
                else if (files.Count > 0)
                {
                    //files.Sort((y, x) => DateTime.Compare(System.IO.File.GetLastWriteTime(x), System.IO.File.GetLastWriteTime(y)));
                    //File.Copy(files[0], @"\\10.0.0.8\rdrive\OnHold\On-Hold\" + order_number + ".txt", true);
                }
            }

            if (release_print.Contains("1"))
            {
                Print_Check_Boxes = true;
            }

            if (release_print.Contains("-DE"))
            {
                // disable edit
                Disable_Edit = true;
            }

            if (release_print.Contains("-SA"))
            {
                // disable edit
                ShowAll = true;
                Width = Width * 2;
                Show();
            }

            InitializeComponent();

            if (VIEW_MODE2 == "EDIT")
            {
                save_button.Text = "Save";
                EDIT_MODE = true;
                
            }

            Info_List.Add("SHOP_ORDER_NUMBER", order_number);
            Info_List.Add("EMPLOYEE_NUMBER", employee_number);
            Info_List.Add("HOLD_DATE", "");
            Info_List.Add("RELEASE_DATE", "");

            Load_Information();
            Order_Number = order_number;
            label1.Text = order_number;
            Get_Employee_List();

            try
            {
                label2.Text = _EMPLOYEE_LIST[employee_number];
            }
            catch
            {
                label2.Text = "Unknown User";
            }
            string MODE = DEPARTMENT;
            VIEW_MODE = MODE;

            if (VIEW_MODE == "HOLD" || VIEW_MODE == "SALES" && VIEW_MODE2 != "EDIT")
            {
                VIEW_MODE = "HOLD";
                save_button.Text = "Hold " + order_number;
            }

            Font FLP_Font = new Font("Times New Roman", 12f, FontStyle.Bold | FontStyle.Underline);

            #region POPULATE FLOWLAYOUTPANEL
            foreach (CheckBox g in Header_Check_List)
            {
                HeaderLayoutPanel.Controls.Add(g);
            }

            if (DEPARTMENT.Length <= 0 || DEPARTMENT == "SALES" || ShowAll) // If sales
            {
                print_checklist.Visible = true;
                print_checklist.Visible = true;
                foreach (KeyValuePair<string, List<CheckList_Item>> Entry in Sales_Dictionary)
                {
                    GroupBox GB = new GroupBox();
                    GB.Text = Entry.Key.ToString();
                    GB.AutoSize = true;

                    FlowLayoutPanel FLP = new FlowLayoutPanel();
                    int data_height = 35;

                    // Add Checkboxes
                    foreach (CheckList_Item CLI in Entry.Value)
                    {
                        FLP.Controls.Add(CLI.Layout);
                    }

                    FLP.Size = new Size(GB.Size.Height + Control_Width, Entry.Value.Count() * data_height - 15 + (Entry.Value.Count == 1 ? 5 : 0));
                    GB.Font = FLP_Font;
                    FLP.Location = new Point(7, 20);
                    GB.Controls.Add(FLP);
                    //GB.Controls.Add(C
                    LayoutPanel.Controls.Add(GB);
                }
            }
            if (DEPARTMENT == "CAD" || ShowAll)
            {
                foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAD_Dictionary)
                {
                    GroupBox GB = new GroupBox();
                    GB.Text = Entry.Key.ToString();
                    GB.AutoSize = true;

                    FlowLayoutPanel FLP = new FlowLayoutPanel();
                    int data_height = 35;

                    // Add Checkboxes
                    foreach (CheckList_Item CLI in Entry.Value)
                    {
                        FLP.Controls.Add(CLI.Layout);
                    }

                    FLP.Size = new Size(GB.Size.Height + Control_Width, Entry.Value.Count() * data_height - 15 + (Entry.Value.Count == 1 ? 5 : 0));
                    GB.Font = FLP_Font;
                    FLP.Location = new Point(7, 20);
                    GB.Controls.Add(FLP);
                    //GB.Controls.Add(C
                    LayoutPanel.Controls.Add(GB);
                }
            }
            if (DEPARTMENT == "CAM" || ShowAll)
            {
                foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAM_Dictionary)
                {
                    GroupBox GB = new GroupBox();
                    GB.Text = Entry.Key.ToString();
                    GB.AutoSize = true;

                    FlowLayoutPanel FLP = new FlowLayoutPanel();
                    int data_height = 35;

                    // Add Checkboxes
                    foreach (CheckList_Item CLI in Entry.Value)
                    {
                        FLP.Controls.Add(CLI.Layout);
                    }

                    FLP.Size = new Size(GB.Size.Height + Control_Width, Entry.Value.Count() * data_height - 15 + (Entry.Value.Count == 1 ? 5 : 0));
                    GB.Font = FLP_Font;
                    FLP.Location = new Point(7, 20);
                    GB.Controls.Add(FLP);
                    LayoutPanel.Controls.Add(GB);
                }
            }
            T.MaxLength = 20000;
            T.Size = new Size(314, 30);
            T.Multiline = false;
            LayoutPanel.Controls.Add(T);
            T.Text = Notes_String.Length == 0 ? "Add notes here!" : Notes_String;
            this.Size = new Size(LayoutPanel.Size.Width + 10, LayoutPanel.Size.Height + 10);
            #endregion

            if (DEPARTMENT == "SALES")
            {
                if (File.Exists(@"\\10.0.0.8\rdrive\OnHold\On-hold\" + Order_Number + ".txt"))
                {
                    Load_Check_States(@"\\10.0.0.8\rdrive\OnHold\On-hold\" + Order_Number + ".txt");
                }
                else
                {
                    List<string> files = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released", "*" + order_number + "*").ToList();
                    if (files.Count > 0)
                        Load_Check_States(files[0]);
                }
            }
            else
            {
                List<string> files = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released", "*" + order_number + "*").ToList();
                if (files.Count == 0)
                {
                    files = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\On-Hold", "*" + order_number + "*").ToList();
                }
                else if (files.Count > 0)
                {
                    files.Sort((y, x) => DateTime.Compare(System.IO.File.GetLastWriteTime(x), System.IO.File.GetLastWriteTime(y)));
                }

                Load_Check_States(files[0]);
                CAD_CAM_PATH = files[0];
            }

            label5.Visible = true;
            if (Info_List["HOLD_DATE"].Length > 5)
            {
                label5.Visible = true;
                label6.Visible = true;
            }
            label5.Text = Info_List["HOLD_DATE"];
            label7.Text = Info_List["RELEASE_DATE"];
            if (label7.Text.Length > 5)
            {
                label7.Visible = true;
                label8.Visible = true;
            }
            //log.Sort((y, x) => DateTime.Compare(
            //    Convert.ToDateTime(x.Split(new string[] { "] : " }, StringSplitOptions.None)[0].Substring(1)),
            //    Convert.ToDateTime(y.Split(new string[] { "] : " }, StringSplitOptions.None)[0].Substring(1))));
            if (MANAGEMENT_MODE && !ShowAll)
            {
                //this.printPreviewDialog1.Document = this.printDocument2;
                //printPreviewDialog1.TopMost = true;
                //printPreviewDialog1.ShowDialog();
                printDocument2.Print();
                if (File.Exists(@"\\10.0.0.8\rdrive\OnHold\On-Hold\" + order_number + ".txt"))
                {
                    //File.Delete(@"\\10.0.0.8\rdrive\OnHold\On-Hold\" + order_number + ".txt");
                }
            }

            try
            {
                label2.Text = _EMPLOYEE_LIST[Info_List["EMPLOYEE_NUMBER"]];
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error found: " + e.ToString());
            }

            if (ShowAll)
                Width = Width * 2 - 30;
        }

        private string Get_Employee_Release_Name()
        {

            string query = "select employeenumber from d_task where task like 'RL' and ordernumber = '" + Order_Number + "' order by id desc";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            string name = "";
            try
            {
                reader.Read();
                name = reader[0].ToString().Trim();
            }
            catch
            {
            }
            reader.Close();
            return name;
        }

        private void Get_Employee_List()
        {
            string query = "select employeenumber, firstname, lastname from d_user";

            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);

            while (reader.Read())
            {
                _EMPLOYEE_LIST.Add(reader[0].ToString().Trim(), reader[1].ToString().Trim() + " " + reader[2].ToString().Trim());
            }
            reader.Close();
            //database.connection.Close();
        }

        public string Notes_String = "";

        private void Load_Information()
        {
            string Config_Path = "\\\\10.0.0.8\\rdrive\\OnHold\\Checklist_Config.txt";

            if (File.Exists(Config_Path))
            {
                var text = File.ReadAllText(Config_Path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.StartsWith("#") && line.Contains("|"))
                    {
                        string[] info = line.Substring(1).Split(new string[] { "|" }, StringSplitOptions.None);
                        CheckList_Item Temp = new CheckList_Item(this, "", "", "", 0); // Never use this
                        if (!line.Contains("#MAP") && !line.Contains("#HEADER"))
                        {
                            Temp = new CheckList_Item(this, info[2], info[0], info.Length > 4 ? info[4] : "", Convert.ToInt32(info[3]));
                        }
                        if (info[0] == "SALES")
                        {
                            Map_Entry(info[0], info[3], Temp);
                            if (Sales_Dictionary.ContainsKey(info[1]))
                            {
                                Sales_Dictionary[info[1]].Add(Temp);
                            }
                            else
                            {
                                Sales_Dictionary.Add(info[1], new List<CheckList_Item> { Temp });
                            }
                        } 
                        else if (info[0] == "CAD")
                        {
                            Map_Entry(info[0], info[3], Temp);
                            if (CAD_Dictionary.ContainsKey(info[1]))
                            {
                                CAD_Dictionary[info[1]].Add(Temp);
                            }
                            else
                            {
                                CAD_Dictionary.Add(info[1], new List<CheckList_Item> { Temp });
                            }
                        }
                        else if (info[0] == "CAM")
                        {
                            Map_Entry(info[0], info[3], Temp);
                            if (CAM_Dictionary.ContainsKey(info[1]))
                            {
                                CAM_Dictionary[info[1]].Add(Temp);
                            }
                            else
                            {
                                CAM_Dictionary.Add(info[1], new List<CheckList_Item> { Temp });
                            }
                        }
                        else if (info[0] == "HEADER")
                        {
                            bool check_state = false;
                            if (info.Length > 2 && info[2].Contains("-PS"))
                                check_state = true;

                            Header_Check_List.Add(new CheckBox() { Text = info[1], AutoSize = false, Height = 17, Padding = new Padding(0, 0, 0, 0), Checked = check_state, Enabled = (VIEW_MODE == "CAD" || VIEW_MODE == "CAM" ? false : true)});
                        }
                        else if (info[0] == "MAP")
                        {
                            Mapping_List.Add(line.Substring(1));
                        }
                        else if (info[0] == "NOTES")
                        {
                            Notes_String = info[1].ToString();
                            T.Text = Notes_String;
                        }
                    }
                }
            }
        }

        public void Map_Entry(string Department, string ID, CheckList_Item CLI)
        {
            int ref_ID = Convert.ToInt32(ID);
            if (!Mapping.ContainsKey(ref_ID))
            {
                if (Department == "SALES")     Mapping.Add(ref_ID, CLI);
                else if (Department == "CAD")  Mapping.Add(ref_ID, CLI);
                else if (Department == "CAM")  Mapping.Add(ref_ID, CLI);
            }
        }

        public void Load_Check_States(string path)
        {
            Dictionary<int, string> temp = new Dictionary<int, string>();
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                List<int> check_id_list = new List<int>();
                foreach (string line in lines)
                {
                    string[] info = line.Substring(0).Split(new string[] { "|" }, StringSplitOptions.None);


                    if (info[0] == "SALES")
                    {
                        foreach (KeyValuePair<string, List<CheckList_Item>> Entry in Sales_Dictionary)
                        {
                            if (Entry.Key.ToString() == info[1])
                            {
                                foreach (CheckList_Item CLI in Entry.Value)
                                {
                                    if (CLI.CheckListLabel.Text == info[2])
                                    {
                                        CLI.id_switch_state = true;
                                        if (!check_id_list.Contains(CLI.Check_ID))
                                        {
                                            if (info[3] == "4")
                                            {
                                                CLI.YesBox.Checked = true;
                                                CLI.NoBox.Checked = true;
                                            }
                                            if (info[3] == "3")
                                            {
                                                CLI.NABox.Checked = true;
                                            }
                                            if (info[3] == "2")
                                            {
                                                CLI.YesBox.Checked = true;
                                            }
                                            else if (info[3] == "1")
                                            {
                                                CLI.NoBox.Checked = true;
                                            }
                                            CLI.id_switch_state = false;
                                            check_id_list.Add(CLI.Check_ID);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (info[0] == "CAD")
                    {
                        foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAD_Dictionary)
                        {
                            if (Entry.Key.ToString() == info[1])
                            {
                                foreach (CheckList_Item CLI in Entry.Value)
                                {
                                    if (CLI.CheckListLabel.Text == info[2])
                                    {
                                        CLI.id_switch_state = true;
                                        if (info[3] == "4")
                                        {
                                            CLI.YesBox.Checked = true;
                                            CLI.NoBox.Checked = true;
                                        }
                                        if (info[3] == "3")
                                        {
                                            CLI.NABox.Checked = true;
                                        }
                                        if (info[3] == "2")
                                        {
                                            CLI.YesBox.Checked = true;
                                        }
                                        else if (info[3] == "1")
                                        {
                                            CLI.NoBox.Checked = true;
                                        }
                                        CLI.id_switch_state = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (info[0] == "CAM")
                    {
                        foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAM_Dictionary)
                        {
                            if (Entry.Key.ToString() == info[1])
                            {
                                foreach (CheckList_Item CLI in Entry.Value)
                                {
                                    if (CLI.CheckListLabel.Text == info[2])
                                    {

                                        CLI.id_switch_state = true;
                                        if (info[3] == "4")
                                        {
                                            CLI.YesBox.Checked = true;
                                            CLI.NoBox.Checked = true;
                                        }
                                        if (info[3] == "3")
                                        {
                                            CLI.NABox.Checked = true;
                                        }
                                        if (info[3] == "2")
                                        {
                                            CLI.YesBox.Checked = true;
                                        }
                                        else if (info[3] == "1")
                                        {
                                            CLI.NoBox.Checked = true;
                                        }
                                        CLI.id_switch_state = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (info[0] == "INFO")
                    {
                        try
                        {
                            Info_List[info[1]] = info[2];
                            if (info[2] == "EMPLOYEE_NUMBER")
                            {
                                label2.Text = info[2];
                            }
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show("Error found: " + e.ToString());
                        }
                    }
                    else if (info[0] == "NOTES")
                    {
                        Notes_String = info[1].ToString();
                        T.Text = Notes_String;
                    }
                    else if (info[0] == "HEADER")
                    {
                        foreach (CheckBox g in Header_Check_List)
                        {
                            if (g.Text == info[1])
                            {
                                if (info[2] == "2")
                                {
                                    g.Checked = true;
                                }
                            }
                            if (VIEW_MODE == "CAD" || VIEW_MODE == "CAM")
                            {
                                g.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                //MessageBox.Show("No error should show");
            }

            if (VIEW_MODE == "CAD" || VIEW_MODE == "CAM")
            {
                T.Enabled = false;
            }
            if (Disable_Edit)
            {
                foreach (KeyValuePair<string, List<CheckList_Item>> c in Sales_Dictionary)
                {
                    foreach (CheckList_Item gg in c.Value)
                    {
                        gg.YesBox.Enabled = false;
                        gg.NoBox.Enabled = false;
                        gg.NABox.Enabled = false;
                    }
                }
                foreach (KeyValuePair<string, List<CheckList_Item>> c in CAD_Dictionary)
                {
                    foreach (CheckList_Item gg in c.Value)
                    {
                        gg.YesBox.Enabled = false;
                        gg.NoBox.Enabled = false;
                        gg.NABox.Enabled = false;
                    }
                }
                foreach (KeyValuePair<string, List<CheckList_Item>> c in CAM_Dictionary)
                {
                    foreach (CheckList_Item gg in c.Value)
                    {
                        gg.YesBox.Enabled = false;
                        gg.NoBox.Enabled = false;
                        gg.NABox.Enabled = false;
                    }
                }
                T.Enabled = false;

                foreach (CheckBox g in Header_Check_List)
                {
                    g.Enabled = false;
                }
                if (!ShowAll) print_checklist.Visible = false;
                save_button.Visible = false;
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (T.Text.Length <= 0)
            {
                MessageBox.Show("You need to leave a note as to why order is on hold!");
                return;
            }

            if (Info_List["HOLD_DATE"].Length < 5 && !EDIT_MODE) Info_List["HOLD_DATE"] = DateTime.Now.ToString();
            bool error = false;
            string HOLD_PATH = @"\\10.0.0.8\rdrive\OnHold\On-hold\" + Order_Number + ".txt";
            string Save_String = "";
            
            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in Sales_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "SALES|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "HOLD")
                    {
                        error = true;
                        CLI.YesBox.ForeColor = Color.Red;
                        CLI.NoBox.ForeColor = Color.Red;
                        CLI.NABox.ForeColor = Color.Red;
                    }
                }
            }

            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAD_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "CAD|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "CAD")
                    {
                        error = true;
                        CLI.YesBox.ForeColor = Color.Red;
                        CLI.NoBox.ForeColor = Color.Red;
                        CLI.NABox.ForeColor = Color.Red;
                    }
                }
            }

            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAM_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "CAM|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "CAM")
                    {
                        error = true;
                        CLI.YesBox.ForeColor = Color.Red;
                        CLI.NoBox.ForeColor = Color.Red;
                        CLI.NABox.ForeColor = Color.Red;
                    }
                }
            }

            foreach (KeyValuePair<string, string> Entry in Info_List)
            {
                Save_String = Save_String + "INFO|" + Entry.Key.ToString() + "|" + Entry.Value.ToString() + Environment.NewLine;
            }

            foreach (CheckBox Entry in Header_Check_List)
            {
                Save_String = Save_String + "HEADER|" + Entry.Text + "|" + (Entry.Checked ? "2" : "1") + Environment.NewLine;
            }

            // Save notes text
            Save_String = Save_String + "NOTES|" + T.Text + Environment.NewLine;

            if (error)
            {
                MessageBox.Show("Error: Missing a checklist item");
                //Info_List["HOLD_DATE"] = "";
            }
            else
            {
                try
                {
                    if (VIEW_MODE == "CAD" || VIEW_MODE == "CAM")

                        HOLD_PATH = CAD_CAM_PATH;

                    File.Delete(HOLD_PATH);

                    //if (Write_Time_On_Load == File.GetLastWriteTime(_INFO_FILE_PATH)) // If file hasnt been accessed and changed
                    //{

                    using (StreamWriter sw = File.CreateText(HOLD_PATH)) 
                    {
                        sw.Write(Save_String.Trim());// + Environment.NewLine);
                        sw.Close();
                        this.Close();
                    }

                    if (!EDIT_MODE) // if not edit, save in database
                    {
                    }
                    //}
                }
                catch
                {
                    // Cannot overwrite computer info file
                }
            }
        }

        public void External_Save()
        {
            if (Info_List["HOLD_DATE"].Length < 5) Info_List["HOLD_DATE"] = DateTime.Now.ToString();

            bool error = false;
            string HOLD_PATH = @"\\10.0.0.8\rdrive\OnHold\On-hold\" + Order_Number + ".txt";
            string Save_String = "";

            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in Sales_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "SALES|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "HOLD") error = true;
                }
            }
            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAD_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "CAD|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "CAD") error = true;
                }
            }
            foreach (KeyValuePair<string, List<CheckList_Item>> Entry in CAM_Dictionary)
            {
                foreach (CheckList_Item CLI in Entry.Value)
                {
                    Save_String = Save_String + "CAM|" + Entry.Key.ToString() + "|" + CLI.CheckListLabel.Text + "|" + CLI.check_state.ToString() + Environment.NewLine;
                    if (CLI.check_state == 0 && VIEW_MODE == "CAM") error = true;
                }
            }
            foreach (KeyValuePair<string, string> Entry in Info_List)
            {
                Save_String = Save_String + "INFO|" + Entry.Key.ToString() + "|" + Entry.Value.ToString() + Environment.NewLine;
            }

            foreach (CheckBox Entry in Header_Check_List)
            {
                Save_String = Save_String + "HEADER|" + Entry.Text + "|" + (Entry.Checked ? "2" : "1") + Environment.NewLine;
            }
            if (Notes_String.Length > 0)
            {
                Save_String = Save_String + "NOTES|" + Notes_String + Environment.NewLine;
            }
            if (error)
            {
                MessageBox.Show("Error: Missing a checklist item");
                //Info_List["HOLD_DATE"] = "";
            }
            else
            {
                try
                {
                    File.Delete(HOLD_PATH);

                    //if (Write_Time_On_Load == File.GetLastWriteTime(_INFO_FILE_PATH)) // If file hasnt been accessed and changed
                    //{

                    using (StreamWriter sw = File.CreateText(HOLD_PATH)) // Create translator file
                    {
                        sw.Write(Save_String.Trim());// + Environment.NewLine);
                        sw.Close();
                        this.Close();
                    }
                    //}
                }
                catch
                {
                    // Cannot overwrite computer info file
                }
            }
        }

        public bool Check_Mapping(int ID_1, int ID_2, string Mapping_Options)
        {
            // ID_1 = CURRENT BOX
            // ID_2 = REFERENCE BOX

            if (Mapping_Options.Contains("-E"))
            {
                if (Mapping[ID_1].check_state == Mapping[ID_2].check_state)
                {
                    return true;
                }
            }
            else if (Mapping_Options.Contains("-N"))
            {
                if (Mapping[ID_2].check_state == 3)
                {
                    return true;
                }
                else if (Mapping[ID_1].check_state < Mapping[ID_2].check_state)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void print_checklist_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to print?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes && !ShowAll)
            {
                Print_Check_Boxes = false;

                print_checklist.Enabled = false;
                //printPreviewDialog1.TopMost = true;
                //printPreviewDialog1.ShowDialog();
                printDocument2.Print();
            }
            else if (dialogResult == DialogResult.Yes && ShowAll)
            {
                Print_Check_Boxes = true;
                print_checklist.Enabled = false;
                //printPreviewDialog1.TopMost = true;
                //printPreviewDialog1.ShowDialog();
                printDocument2.Print();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        Dictionary<CheckList_Item, bool> Print_Ref = new Dictionary<CheckList_Item, bool>();
        Dictionary<string, List<CheckList_Item>> Print_Ref_2 = new Dictionary<string, List<CheckList_Item>>();
        bool sales_print_done = false;
        bool CAD_print_done = false;
        bool CAM_print_done = false;

        private void printDocument2_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            int startx = 20;
            int starty = 15;
            int indent = 30;
            int dataheight = 35;
            int height = 40;//starty;// +starty;
            int extra_height = 0;

            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;

            Pen p = new Pen(Brushes.Black, 1.5f);
            Pen p2 = new Pen(Brushes.Black, 3f);
            Font f2 = new Font("Times New Roman", 15f);
            Font f6 = new Font("Tahoma", 11f);
            Font f7 = new Font("Tahoma", 9f);
            Font f3 = new Font("Times New Roman", 12f, FontStyle.Bold | FontStyle.Underline);
            Font f4 = new Font("Times New Roman", 12f);
            Font f1 = new Font("Times New Roman", 17.0f, FontStyle.Bold | FontStyle.Underline);
            Font f5 = new Font("Times New Roman", 21f, FontStyle.Bold);

            // PRINTING HEADER LINE

            if (Notes_String.Contains("Add notes here!")) Notes_String = "";

            if (true)//(!sales_print_done)
            {
                height = starty;
                e.Graphics.DrawString("On-Hold Checklist" + (MANAGEMENT_MODE ? " Summary" : ""), f1, Brushes.Black, new Rectangle(startx, height, 500, dataheight * 2));//, format1);
                e.Graphics.DrawString(Order_Number, f5, Brushes.Black, new Rectangle(670, height, 300, dataheight * 2));//, format1);
                height += 36;
                try
                {
                    e.Graphics.DrawString("Employee: " + _EMPLOYEE_LIST[Info_List["EMPLOYEE_NUMBER"]], f6, Brushes.Black, new Rectangle(startx + indent / 2, height, 300, dataheight * 2));//, format1);
                }
                catch
                {
                    e.Graphics.DrawString("Employee: " + "Error", f6, Brushes.Black, new Rectangle(startx + indent / 2, height, 300, dataheight * 2));//, format1);
                }
                if (Info_List["HOLD_DATE"] != "") e.Graphics.DrawString("Hold Date: " + Info_List["HOLD_DATE"], f6, Brushes.Black, new Rectangle(startx + 205 + indent / 2, height, 500, dataheight * 2));//, format1);
                if (Info_List.ContainsKey("RELEASE_DATE") && Info_List["RELEASE_DATE"] != "") e.Graphics.DrawString("Release Date: " + Info_List["RELEASE_DATE"], f6, Brushes.Black, new Rectangle(startx + 485 + indent / 2, height, 500, dataheight * 2));//, format1);
                if (Info_List.ContainsKey("RELEASE_DATE") && Info_List["RELEASE_DATE"] != "") e.Graphics.DrawString("Released by: " + (_EMPLOYEE_LIST.ContainsKey(Get_Employee_Release_Name()) ? _EMPLOYEE_LIST[Get_Employee_Release_Name()] : "Unknown Emp# : " + Get_Employee_Release_Name()), f7, Brushes.Black, new Rectangle(startx + 400 + indent / 2, height - 20, 500, dataheight * 2));//, format1);

                height += dataheight; height += 3;

                int ref_x = startx + 3;

                // Print header checkboxes
                for (int i = 0; i < Header_Check_List.Count; i++)
                {
                    if (i % 5 == 0 && i > 0)
                    {
                        extra_height += dataheight;
                        height += extra_height; 
                        ref_x = startx + 3;
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(ref_x, height + 3, dataheight - 15, dataheight - 15));
                    e.Graphics.DrawString(Header_Check_List[i].Text, f4, Brushes.Black, new Rectangle(ref_x + 23, height + 3, (775 / 5) - 23, dataheight));//, format1);

                    if (Print_Check_Boxes)
                    {
                        if (Header_Check_List[i].Checked)
                        {
                            if (Check_Style == "X")
                            {
                                e.Graphics.DrawLine(p2, ref_x, height + 3, ref_x + 20, height + 22);
                                e.Graphics.DrawLine(p2, ref_x, height + 22, ref_x + 20, height + 2);
                            }
                            else if (Check_Style == "Check")
                            {
                                e.Graphics.DrawLine(p2, ref_x + 3, height + 6, ref_x + 11, height + 15);
                                e.Graphics.DrawLine(p2, ref_x + 11, height + 15, ref_x + 23, height - 5);
                            }
                        }
                    }
                    ref_x += 775 / 5;
                }
                e.Graphics.DrawRectangle(p, new Rectangle(startx - 5, height - dataheight - 5, 780, 35 + extra_height));
                height += dataheight + 9;
                if (Notes_String.Length > 0)
                {
                    e.Graphics.DrawString(Notes_String, f6, Brushes.Black, new Rectangle(startx, height, 900, dataheight * 2));
                    height += dataheight;
                }
            }


            int bottom_page_margin = 20;

            
            #region SALES_PRINT
            if (!sales_print_done)
            {
                foreach (KeyValuePair<string, List<CheckList_Item>> g in Sales_Dictionary)
                {
                    if (startx > 400 && height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin && startx > 400))
                    {
                        height = starty;
                        height += dataheight;
                        height += dataheight;
                        e.HasMorePages = true;
                        startx = 0;
                        return;
                    }

                    if (height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin) && startx == 0)
                    {
                        height = starty;
                        if (Notes_String.Length > 0)
                        {
                            //e.Graphics.DrawString("Notes here", f6, Brushes.Black, new Rectangle(startx, height, 500, dataheight * 2));
                            height += dataheight;
                        }
                        height += 13;
                        height += dataheight;
                        height += dataheight;
                        height += extra_height;
                        height += dataheight;
                        //e.HasMorePages = true;
                        startx = 430;
                        //return;
                    }

                    if (!Print_Ref_2.Contains(g))
                    {
                        e.Graphics.DrawString(g.Key, f3, Brushes.Black, new Rectangle(startx, height, 168, dataheight));//, format1);
                        e.Graphics.DrawRectangle(p, new Rectangle(startx - 5, height - 5, 410 - (startx > 400 ? 40 : 0), g.Value.Count * dataheight + (dataheight - 10)));
                        height += dataheight - 10;
                        try
                        {
                            Print_Ref_2.Add(g.Key, g.Value);
                        }
                        catch
                        {
                            Print_Ref_2[g.Key] = g.Value;
                        }

                    }

                    foreach (CheckList_Item CLI in g.Value)
                    {
                        if (!Print_Ref.ContainsKey(CLI))
                        {
                            Print_Ref.Add(CLI, true);

                            e.Graphics.DrawString(CLI.CheckListLabel.Text, f4, Brushes.Black, new Rectangle(startx + indent, height, 150, dataheight));//, format1);

                            int Yes_Width = Get_Font_Width(CLI.YesBox.Text, "Times New Roman", 12f);
                            int No_Width = Get_Font_Width(CLI.NoBox.Text, "Times New Roman", 12f);

                            //checkboxes
                            e.Graphics.DrawRectangle(p, new Rectangle(startx + 128 + indent, height + 1, dataheight - 15, dataheight - 15));
                            e.Graphics.DrawRectangle(p, new Rectangle(startx + 157 + indent + Yes_Width, height + 1, dataheight - 15, dataheight - 15));
                            if (CLI.hasNA) e.Graphics.DrawRectangle(p, new Rectangle(startx + 192 + indent + Yes_Width + No_Width, height + 1, dataheight - 15, dataheight - 15));
                            e.Graphics.DrawString(CLI.YesBox.Text + "       " + CLI.NoBox.Text + "        " + (CLI.hasNA ? "N/A" : ""), f4, Brushes.Black, new Rectangle(startx + 150 + indent, height, 300, dataheight));//, format1);

                            // automatically check the boxes
                            if (Print_Check_Boxes)
                            {
                                if (CLI.YesBox.Checked)
                                {
                                    if (Check_Style == "X")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 128 + indent, height + 1, startx + 128 + indent + 20, height + 21);
                                        e.Graphics.DrawLine(p2, startx + 128 + indent, height + 21, startx + 128 + indent + 20, height + 1);
                                    }
                                    else if (Check_Style == "Check")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 131 + indent, height + 4, startx + 128 + indent + 11, height + 15);
                                        e.Graphics.DrawLine(p2, startx + 128 + indent + 11, height + 15, startx + 131 + indent + 20, height - 5);
                                    }
                                }
                                if (CLI.NoBox.Checked)
                                {
                                    if (Check_Style == "X")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 1, startx + 157 + indent + Yes_Width + 20, height + 21);
                                        e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 21, startx + 157 + indent + Yes_Width + 20, height + 1);
                                    }
                                    else if (Check_Style == "Check")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 160 + indent + Yes_Width, height + 4, startx + 157 + indent + Yes_Width + 11, height + 15);
                                        e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width + 11, height + 15, startx + 160 + indent + Yes_Width + 20, height - 5);
                                    }
                                }
                                if (CLI.hasNA && CLI.NABox.Checked)
                                {
                                    if (Check_Style == "X")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 1, startx + 192 + indent + Yes_Width + No_Width + 20, height + 21);
                                        e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 21, startx + 192 + indent + Yes_Width + No_Width + 20, height + 1);
                                    }
                                    else if (Check_Style == "Check")
                                    {
                                        e.Graphics.DrawLine(p2, startx + 195 + indent + Yes_Width + No_Width, height + 4, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15);
                                        e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15, startx + 195 + indent + Yes_Width + No_Width + 20, height - 5);
                                    }
                                }
                            }
                            height += dataheight;
                        }
                    }
                }
                sales_print_done = true;
                height += 500;
            }
            #endregion

            if (MANAGEMENT_MODE)
            {
                #region CAD_PRINT
                if (!CAD_print_done)
                {
                    foreach (KeyValuePair<string, List<CheckList_Item>> g in CAD_Dictionary)
                    {
                        if (startx > 400 && height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin && startx > 400))
                        {
                            height = starty;
                            height += dataheight;
                            height += dataheight;
                            e.HasMorePages = true;
                            startx = 0;
                            return;
                        }

                        if (height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin) && startx == 0)
                        {
                            height = starty; height += 9;
                            height += dataheight;
                            height += dataheight;
                            //e.HasMorePages = true;
                            startx = 430;
                            //return;
                        }

                        if (!Print_Ref_2.Contains(g))
                        {
                            e.Graphics.DrawString(g.Key, f3, Brushes.Black, new Rectangle(startx, height, 168, dataheight));//, format1);
                            e.Graphics.DrawRectangle(p, new Rectangle(startx - 5, height - 5, 410 - (startx > 400 ? 40 : 0), g.Value.Count * dataheight + (dataheight - 10)));
                            height += dataheight - 10;
                            try
                            {
                                Print_Ref_2.Add(g.Key, g.Value);
                            }
                            catch
                            {
                                Print_Ref_2[g.Key] = g.Value;
                            }

                        }

                        foreach (CheckList_Item CLI in g.Value)
                        {
                            if (!Print_Ref.ContainsKey(CLI))
                            {
                                Print_Ref.Add(CLI, true);

                                e.Graphics.DrawString(CLI.CheckListLabel.Text, f4, Brushes.Black, new Rectangle(startx + indent, height, 150, dataheight));//, format1);

                                int Yes_Width = Get_Font_Width(CLI.YesBox.Text, "Times New Roman", 12f);
                                int No_Width = Get_Font_Width(CLI.NoBox.Text, "Times New Roman", 12f);

                                //checkboxes
                                e.Graphics.DrawRectangle(p, new Rectangle(startx + 128 + indent, height + 1, dataheight - 15, dataheight - 15));
                                e.Graphics.DrawRectangle(p, new Rectangle(startx + 157 + indent + Yes_Width, height + 1, dataheight - 15, dataheight - 15));
                                if (CLI.hasNA) e.Graphics.DrawRectangle(p, new Rectangle(startx + 192 + indent + Yes_Width + No_Width, height + 1, dataheight - 15, dataheight - 15));
                                e.Graphics.DrawString(CLI.YesBox.Text + "       " + CLI.NoBox.Text + "        " + (CLI.hasNA ? "N/A" : ""), f4, Brushes.Black, new Rectangle(startx + 150 + indent, height, 300, dataheight));//, format1);

                                // automatically check the boxes
                                if (Print_Check_Boxes)
                                {
                                    if (CLI.YesBox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 128 + indent, height + 1, startx + 128 + indent + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 128 + indent, height + 21, startx + 128 + indent + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 131 + indent, height + 4, startx + 128 + indent + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 128 + indent + 11, height + 15, startx + 131 + indent + 20, height - 5);
                                        }
                                    }
                                    if (CLI.NoBox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 1, startx + 157 + indent + Yes_Width + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 21, startx + 157 + indent + Yes_Width + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 160 + indent + Yes_Width, height + 4, startx + 157 + indent + Yes_Width + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width + 11, height + 15, startx + 160 + indent + Yes_Width + 20, height - 5);
                                        }
                                    }
                                    if (CLI.hasNA && CLI.NABox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 1, startx + 192 + indent + Yes_Width + No_Width + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 21, startx + 192 + indent + Yes_Width + No_Width + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 195 + indent + Yes_Width + No_Width, height + 4, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15, startx + 195 + indent + Yes_Width + No_Width + 20, height - 5);
                                        }
                                    }
                                }
                                height += dataheight;
                            }
                        }
                    }
                    CAD_print_done = true;
                }
                #endregion

                #region CAM_PRINT
                if (!CAM_print_done)
                {
                    //Print_Ref_2 = new Dictionary<string, List<CheckList_Item>>();
                    foreach (KeyValuePair<string, List<CheckList_Item>> g in CAM_Dictionary)
                    {
                        if (startx > 400 && (height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin)))
                        {
                            e.HasMorePages = true;
                            height = starty;
                            height += dataheight;
                            height += dataheight;
                            startx = 0;
                            return;
                        }

                        if (height > e.MarginBounds.Height + bottom_page_margin || (height + (g.Value).Count * dataheight > e.MarginBounds.Height + bottom_page_margin) && startx == 0)
                        {
                            height = starty;
                            if (Notes_String.Length > 0)
                            {
                                //e.Graphics.DrawString("Notes here", f6, Brushes.Black, new Rectangle(startx, height, 500, dataheight * 2));
                                height += dataheight;
                            }
                            height += 13;
                            height += dataheight;
                            height += dataheight;
                            height += extra_height;
                            height += dataheight;
                            //e.HasMorePages = true;
                            startx = 430;
                            //return;
                        }

                        if (!Print_Ref_2.Contains(g))
                        {
                            e.Graphics.DrawString(g.Key, f3, Brushes.Black, new Rectangle(startx, height, 168, dataheight));//, format1);
                            e.Graphics.DrawRectangle(p, new Rectangle(startx - 5, height - 5, 410 - (startx > 400 ? 40 : 0), g.Value.Count * dataheight + (dataheight - 10)));
                            height += dataheight - 10;
                            try
                            {
                                Print_Ref_2.Add(g.Key, g.Value);
                            }
                            catch
                            {
                                Print_Ref_2[g.Key] = g.Value;
                            }

                        }

                        foreach (CheckList_Item CLI in g.Value)
                        {
                            if (!Print_Ref.ContainsKey(CLI))
                            {
                                Print_Ref.Add(CLI, true);

                                e.Graphics.DrawString(CLI.CheckListLabel.Text, f4, Brushes.Black, new Rectangle(startx + indent, height, 150, dataheight));//, format1);

                                int Yes_Width = Get_Font_Width(CLI.YesBox.Text, "Times New Roman", 12f);
                                int No_Width = Get_Font_Width(CLI.NoBox.Text, "Times New Roman", 12f);

                                //checkboxes
                                e.Graphics.DrawRectangle(p, new Rectangle(startx + 128 + indent, height + 1, dataheight - 15, dataheight - 15));
                                e.Graphics.DrawRectangle(p, new Rectangle(startx + 157 + indent + Yes_Width, height + 1, dataheight - 15, dataheight - 15));
                                if (CLI.hasNA) e.Graphics.DrawRectangle(p, new Rectangle(startx + 192 + indent + Yes_Width + No_Width, height + 1, dataheight - 15, dataheight - 15));
                                e.Graphics.DrawString(CLI.YesBox.Text + "       " + CLI.NoBox.Text + "        " + (CLI.hasNA ? "N/A" : ""), f4, Brushes.Black, new Rectangle(startx + 150 + indent, height, 300, dataheight));//, format1);

                                // automatically check the boxes
                                if (Print_Check_Boxes)
                                {
                                    if (CLI.YesBox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 128 + indent, height + 1, startx + 128 + indent + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 128 + indent, height + 21, startx + 128 + indent + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 131 + indent, height + 4, startx + 128 + indent + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 128 + indent + 11, height + 15, startx + 131 + indent + 20, height - 5);
                                        }
                                    }
                                    if (CLI.NoBox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 1, startx + 157 + indent + Yes_Width + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width, height + 21, startx + 157 + indent + Yes_Width + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 160 + indent + Yes_Width, height + 4, startx + 157 + indent + Yes_Width + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 157 + indent + Yes_Width + 11, height + 15, startx + 160 + indent + Yes_Width + 20, height - 5);
                                        }
                                    }
                                    if (CLI.hasNA && CLI.NABox.Checked)
                                    {
                                        if (Check_Style == "X")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 1, startx + 192 + indent + Yes_Width + No_Width + 20, height + 21);
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width, height + 21, startx + 192 + indent + Yes_Width + No_Width + 20, height + 1);
                                        }
                                        else if (Check_Style == "Check")
                                        {
                                            e.Graphics.DrawLine(p2, startx + 195 + indent + Yes_Width + No_Width, height + 4, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15);
                                            e.Graphics.DrawLine(p2, startx + 192 + indent + Yes_Width + No_Width + 11, height + 15, startx + 195 + indent + Yes_Width + No_Width + 20, height - 5);
                                        }
                                    }
                                }
                                height += dataheight;
                            }
                        }
                    }
                     CAM_print_done = true;
                }
                #endregion
            }
        }


        private void Checklist_Load(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = this.printDocument2;
            if (MANAGEMENT_MODE)
            {
                this.Close();
                this.Dispose();
            }

            if (VIEW_MODE == "CAD" || VIEW_MODE == "CAM")
            {
                IntPtr hMenu = GetSystemMenu(this.Handle, false);

                int menuItemCount = GetMenuItemCount(hMenu);

                RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
            }
        }

        public int Get_Font_Width(string txt, string Font, float size, FontStyle fS = FontStyle.Regular)
        {
            var font = new Font(txt, size, fS);
            int underscoreWidth = TextRenderer.MeasureText("__", font).Width; 
            return TextRenderer.MeasureText(String.Concat("_", txt.ToString(), "_"), font).Width - underscoreWidth + (txt.Contains(Convert.ToChar("l")) ? 5 : 0);
        }

    }


    public class CheckList_Item : Control
    {
        public Label CheckListLabel { get; set; }
        public CheckBox YesBox;
        public CheckBox NoBox;
        public CheckBox NABox;
        public string Department { get; set; }
        public string Box_Parameter { get; set; }
        public int Check_ID { get; set; }
        Checklist _parent;

        public bool hasNA = false;
        public int ref_check_int { get; set; }
        public bool Enable_Multi_Check = false;
        internal int check_state = 0;

        // To not trigger before setup
        public bool id_switch_state = false;

        public new FlowLayoutPanel Layout = new FlowLayoutPanel();

        public CheckList_Item(Checklist parent, string Label_Name, string _Department, string _Box_Parameter, int CHK_ID)
        {
            Department = _Department;
            Box_Parameter = _Box_Parameter;
            _parent = parent;
            Check_ID = CHK_ID;

            this.CheckListLabel = new Label() { Size = new Size(90, 20), Text = Label_Name.Trim(Convert.ToChar("*")), Padding = new Padding(0, 7, 0, 0)};
            this.YesBox = new CheckBox() { Size = new Size(50, 25), Text = "Yes" };
            this.NoBox = new CheckBox() { Size = new Size(50, 25), Text = "No" };
            this.NABox = new CheckBox() { Size = new Size(50, 25), Text = "N/A" };
            
            this.YesBox.CheckedChanged += new System.EventHandler(this.Yes_CC);
            this.NoBox.CheckedChanged += new System.EventHandler(this.No_CC);
            this.NABox.CheckedChanged += new System.EventHandler(this.NA_CC);

            if (Box_Parameter.Contains(","))
            {
                string[] x = _Box_Parameter.Split(new string[] { "," }, StringSplitOptions.None);
                YesBox.Text = x[0];
                NoBox.Text = x[1];
                YesBox.AutoSize = true;
                NoBox.AutoSize = true;
                NABox.AutoSize = true;
                //YesBox.Size = new Size(14 * YesBox.Text.Length, 25);
                //NoBox.Size = new Size(14 * NoBox.Text.Length, 25);
            }

            Font f2 = new Font("Tahoma", 8);
            Layout.Font = f2;
            this.Font = f2;

            Layout.Size = new Size(2660, 26);
            Layout.Controls.Add(CheckListLabel);
            Layout.Controls.Add(YesBox);
            Layout.Controls.Add(NoBox);

            if (Box_Parameter.Contains("-NA")) 
            {
                Layout.Controls.Add(NABox);
                hasNA = true;
            }
            if (Box_Parameter.Contains("-MC"))
            {
                this.Enable_Multi_Check = true;
            }

        }

        private void NA_CC(object sender, EventArgs e)
        {
            if (NABox.Checked)
            {
                YesBox.ForeColor = Color.Black;
                NoBox.ForeColor = Color.Black;
                NABox.ForeColor = Color.Black;
                Disable_Listener(true);
                if (!Enable_Multi_Check)
                {
                    NoBox.Enabled = true;
                    YesBox.Enabled = true;
                    NoBox.Checked = false;
                    YesBox.Checked = false;
                    NABox.Enabled = false;
                    check_state = 3;
                }
                else
                {
                    NoBox.Checked = false;
                    YesBox.Checked = false;
                    check_state = 3;
                }
                if (!id_switch_state) Check_ID_State();
                Disable_Listener(false);
            }
            else
            {
                check_state = 0;
            }
        }

        private void Yes_CC(object sender, EventArgs e)
        {
            if (YesBox.Checked)
            {
                YesBox.ForeColor = Color.Black;
                NoBox.ForeColor = Color.Black;
                NABox.ForeColor = Color.Black;
                Disable_Listener(true);
                if (!Enable_Multi_Check)
                {
                    NoBox.Enabled = true;
                    NoBox.Checked = false;
                    NABox.Checked = false;
                    YesBox.Enabled = false;
                    NABox.Enabled = true;
                    check_state = 2;
                }
                else
                {
                    NABox.Checked = false;
                    if (NoBox.Checked)
                        check_state = 4;
                    else
                        check_state = 2;
                }
                if (!id_switch_state) Check_ID_State();
                Disable_Listener(false);
            }
            else
            {
                if (NoBox.Checked)
                    check_state = 1;
                else
                    check_state = 0;
            }
        }

        private void No_CC(object sender, EventArgs e)
        {
            if (NoBox.Checked)
            {
                YesBox.ForeColor = Color.Black;
                NoBox.ForeColor = Color.Black;
                NABox.ForeColor = Color.Black;
                Disable_Listener(true);
                if (!Enable_Multi_Check)
                {
                    YesBox.Enabled = true;
                    YesBox.Checked = false;
                    NoBox.Enabled = false;
                    NABox.Checked = false;
                    NoBox.Checked = true;
                    NABox.Enabled = true;
                    check_state = 1;
                }
                else
                {
                    NABox.Checked = false;
                    if (YesBox.Checked)
                        check_state = 4;
                    else
                        check_state = 1;
                }
                if (!id_switch_state) Check_ID_State();
                Disable_Listener(false);
            }
            else
            {
                if (YesBox.Checked)
                    check_state = 2;
                else
                    check_state = 0;
            }
        }

        private void Disable_Listener(bool disable = false)
        {
            if (disable)
            {
                this.YesBox.CheckedChanged -= new System.EventHandler(this.Yes_CC);
                this.NoBox.CheckedChanged -= new System.EventHandler(this.No_CC);
                this.NABox.CheckedChanged -= new System.EventHandler(this.NA_CC);
            }
            else
            {
                this.YesBox.CheckedChanged += new System.EventHandler(this.Yes_CC);
                this.NoBox.CheckedChanged += new System.EventHandler(this.No_CC);
                this.NABox.CheckedChanged += new System.EventHandler(this.NA_CC);
            }
        }

        public void Check_ID_State()
        {
            if (_parent.VIEW_MODE.Length > 0) //has a valid check comparison (has a comparison => sales does not compare with anything)
            {
                try
                {
                    foreach (string g in _parent.Mapping_List)
                    {
                        string[] info = g.Split(new string[] { "|" }, StringSplitOptions.None);
                        string[] ID = info[1].Split(new string[] { "->" }, StringSplitOptions.None);
                        if ((Convert.ToInt32(ID[0]) == Check_ID) && !_parent.Check_Mapping(Convert.ToInt32(ID[0]), Convert.ToInt32(ID[1]), info[2]))
                            MessageBox.Show((Department == "CAD" ? "Sales" : "CAD") + " states that there is " + (_parent.Mapping[Convert.ToInt32(ID[1])].check_state == 1 ? "no" : "a") + " '" + this.CheckListLabel.Text + "'");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
    }
}
