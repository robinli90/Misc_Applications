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
using System.IO;
using Databases;
using System.Data.Odbc;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Translator
{
    public partial class Translator : Form
    {

        #region VARIABLES

        // Current rule state
        //Rule_List = [action, search_value, dimension, value, optional_value, comment];
        public List<List<string>> Rule_List = new List<List<string>>();

        // Allow management of current rule; this toggle allows for saving and loading
        private string Current_Rule_Name;

        // The entire G-Code parsed into this list; fully dynamic and will be re-written into file later
        private List<string> G_Code_List = new List<string>();

        // G_Code of original file; just for reference to compare later
        private List<string> Original_G_Code_List = new List<string>();

        // Keep track of the number of changes made to a document
        private int Change_Count = 0;

        // Main rule list to manage all available rules for loading and unloading
        Dictionary<string, List<List<string>>> _Master_Rules_List = new Dictionary<string, List<List<string>>>();

        // Group Dictionary: Key = UNIQUE_ID, Value = List of indices where the rules are in the structure: [GROUP_dECRIPTION, i1, i2, i3, ...]; (index = rule location index)
        Dictionary<string, List<string>> Group_Dictionary = new Dictionary<string, List<string>>();
        Dictionary<string, string> Group_Dictionary_Condition = new Dictionary<string, string>();
        List<string> Group_Dictionary_Order = new List<string>();

        // Keeping track of index with changes
        private List<int> Change_Index = new List<int>();

        // Current Rule order (for updating recent later)
        private List<string> Rule_Order = new List<string>();

        // Store the loop ID and the ending rule index in dictionary
        private Dictionary<string, int> Loop_Info = new Dictionary<string, int>();

        // File loaded toggle;
        private bool file_loaded = false;

        // File name that is loaded
        public string file_name_loaded = "";

        // File path of loaded file
        private string file_path_loaded;

        // Original file load
        private string original_file_loaded;

        // If translated already
        public bool Translation_Complete = false;

        // Memory locations
        public string[] MEMORY_BIN = new string[10000];

        // Int of stored values
        private int STORED_COUNT = 0;

        // Declaration of whether or not to modify GCode to output N values with 4 leading zeroes: i.e.: N0004
        private int LEADING_ZEROES = 0;

        // Output letter at the beginning of each line: i.e: NXXX1 NXXX2 or MXXX1 MXXX2. Default will be N
        private string LEADING_LETTER = "N";

        // First and Last character output
        private string FIRST_LINE = "";
        private string LAST_LINE = "";

        private bool ADMIN_MODE = false;
        private int ADMIN_COUNT = 0;

        // Number of format rules
        private int FORMAT_RULES_COUNT = 0;

        // No translate booleans
        private bool DO_NOT_TRANSLATE = false;
        private bool IGNORE_CHECK = false;

        public bool Preview_Toggle = true;

        // Database Querying objects
        private ExcoODBC database = ExcoODBC.Instance;
        private Encrypter enc_obj = new Encrypter();

        // Command Line Arguments
        bool _CMD_ISCMDLINE = false;
        string _CMD_INPUT_PATH;
        string _CMD_TRANSLATOR_ALGORITHM_NAME;
        string _CMD_OUTPUT_PATH;
        public string _CMD_PARAMETER;

        private int refresh_value = 0;

        private bool Show_Grouped = false;

        public bool Bin_View_Open = false;

        public string Group_Condition_String = "";

        #endregion

        //===============================================================================================================================================================

        #region MAIN FUNCITON

        // Main translator function
        public Translator(string __CMD_INPUT_PATH, string __CMD_TRANSLATOR_ALGORITHM_NAME, string __CMD_OUTPUT_PATH, string __PARAMETER)
        {
            _CMD_INPUT_PATH = __CMD_INPUT_PATH;
            _CMD_TRANSLATOR_ALGORITHM_NAME = __CMD_TRANSLATOR_ALGORITHM_NAME;
            _CMD_OUTPUT_PATH = __CMD_OUTPUT_PATH;
            _CMD_PARAMETER = __PARAMETER;

            // IF COMMAND LINE
            if (_CMD_INPUT_PATH.Length > 0 && _CMD_TRANSLATOR_ALGORITHM_NAME.Length > 0 && _CMD_OUTPUT_PATH.Length > 0)
            {
                _CMD_ISCMDLINE = true;


                //MessageBox.Show(_CMD_INPUT_PATH + _CMD_TRANSLATOR_ALGORITHM_NAME + _CMD_OUTPUT_PATH);
                //MessageBox.Show("Houston, we have command line!");

                file_loaded = true;
                Get_Rules();

                // Load first rule
                Load_Rule(_CMD_TRANSLATOR_ALGORITHM_NAME);
                _Setup_Rule_List();

                Store_G_Code();
                //if (G_Code_List.Count == 0 && _CMD_ISCMDLINE)
                //MessageBox.Show("EMPTY!");

                /*
                // Load first rule
                Load_Rule(_CMD_TRANSLATOR_ALGORITHM_NAME);
                _Setup_Rule_List();
                */

                Run_Parameter_Handler();

                // Load commands if exists
                // Modify which rule to execute given certain conditions
                //Load_Rule(_CMD_TRANSLATOR_ALGORITHM_NAME);


                Process_File(); // Analyze file based on rule
                string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
                File.Delete(temp_translation);
                if (!File.Exists(temp_translation))
                {
                    Translation_Complete = true;
                    //using (StreamWriter sw = File.CreateText(temp_translation))
                    //{
                        //sw.Write("\n");
                        //int iz = 0;
                        //string line = "";
                        /*
                        foreach (string g in G_Code_List)
                        {
                            if (iz == 0 && FIRST_LINE.Length > 0)
                            {
                                string[] temp = FIRST_LINE.Split(new string[] { "/r/n" }, StringSplitOptions.None);
                                foreach (string t in temp)
                                {
                                    if (t.Length > 0)
                                    {
                                        line = line + t + Environment.NewLine;
                                    }
                                }
                                FIRST_LINE = "";
                            }
                            else if (iz == G_Code_List.Count - 1)
                            {
                                string[] temp = LAST_LINE.Split(new string[] { "/r/n" }, StringSplitOptions.None);
                                foreach (string t in temp)
                                {
                                    line = line + t;
                                }
                            }

                            if (iz < G_Code_List.Count - 1)
                            {

                                //line = line + (g.Contains("`") ? g.Substring(0, g.IndexOf("`")) : "") + LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString() + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 2) : g) + Environment.NewLine;

                                line = line + (g.Contains("`") ? g.Substring(0, g.IndexOf("`")) : "") + (g.Contains("I_L_N") ? "" : LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString());
                                if (g.Contains("I_L_N"))
                                {
                                    if (g.Contains("`"))
                                        line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim()) + Environment.NewLine;
                                    else
                                    {
                                        line = line + "" + g.Substring(5) + Environment.NewLine;
                                        iz--;
                                    }
                                }
                                else
                                {
                                    line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim()) + Environment.NewLine;
                                }
                                //line = line + LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString() + " " + g + Environment.NewLine;
                            }
                            iz++;
                        }
                        */
                        //sw.Write(line);
                        //sw.Close();

                    //}
                    Process_File_Output();
                    File.WriteAllLines(temp_translation, G_Code_List);
                }

                // If do not translate, return original file and copy that to temp
                if (DO_NOT_TRANSLATE)
                {
                    File.Copy(__CMD_INPUT_PATH, temp_translation, true);
                    Translation_Complete = true;
                }

                if (Translation_Complete)
                {
                    File.Copy(temp_translation, __CMD_OUTPUT_PATH, true);
                }

                this.Close();
                this.Dispose();

                if (!(_CMD_PARAMETER == "SKIP_EXIT"))
                    Environment.Exit(0);

            }
            // IF APPLICATION EXECUTION
            else if (_CMD_INPUT_PATH.Length == 0 && _CMD_TRANSLATOR_ALGORITHM_NAME.Length == 0 && _CMD_OUTPUT_PATH.Length == 0)
            {
                InitializeComponent(); 
                translation_rules.DrawItem += new DrawItemEventHandler(translation_rules_DrawItem);
                translation_rules.MeasureItem += new MeasureItemEventHandler(ListBox1_MeasureItem);
                translation_rules.HorizontalExtent = 1080;
                Get_Rules(); // Get all rules
                _Setup_Translation_Rules();
            }
            // ELSE DO NOTHING (INVALID PARAMETERS)

            // Set Memory bin to null information "''";
            for (int i = 0; i < 10000; i++)
            {
                MEMORY_BIN[i] = "";
            }

            /* DEBUG */
            Console.WriteLine();
            Console.WriteLine("Number of changes: " + Change_Count.ToString());
            Console.WriteLine("Translated via: " + Current_Rule_Name);
        }

        // Handle parameter
        public void Run_Parameter_Handler()
        {
            bool error_free = true;
            if (_CMD_PARAMETER.Length > 0) // if has command
            {
                if (!_CMD_PARAMETER.Contains("-") && _CMD_PARAMETER[0] != Convert.ToChar("-"))
                {
                    MessageBox.Show("Invalid parameter");
                    error_free = false;
                }
                else
                {
                    try
                    {
                        string[] Commands = _CMD_PARAMETER.Split(new string[] { "-" }, StringSplitOptions.None);
                        if (Commands[0] == "" && Commands.Length > 1)
                        {
                            for (int i = 1; i <= Commands.Length - 1; i++)
                            {
                                string[] Indiv_command = Commands[i].Split(new string[] { ":" }, StringSplitOptions.None);
                                if (Convert.ToInt32(Indiv_command[0]) < 9997 && Convert.ToInt32(Indiv_command[0]) >= 0)
                                {
                                    MEMORY_BIN[Convert.ToInt32(Indiv_command[0])] = Indiv_command[1];
                                }
                                else
                                {
                                    MessageBox.Show("Invalid parameter");
                                    error_free = false;
                                }
                            }
                        }
                        else
                        {
                            if (error_free)
                                MessageBox.Show("Invalid parameter");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid parameter. Error: " + e);
                        error_free = false;
                    }
                }
                if (!_CMD_ISCMDLINE && error_free)
                {
                    button24.BackColor = Color.LightBlue;
                }
            }
            else
            {
                if (!_CMD_ISCMDLINE)
                {
                    button24.BackColor = SystemColors.Control;
                }
            }
        }

        #endregion

        //===============================================================================================================================================================

        #region FILE HANDLING

        private void Process_File_Output()
        {
            int iz = 0;
            //string line = "";
            if (ignoreFormatting) return;

            string[] temp = FIRST_LINE.Split(new string[] { "/r/n" }, StringSplitOptions.None);

            if (FIRST_LINE.Length > 0)
            {
                foreach (string t in temp.Reverse())
                {
                    if (t.Length > 0)
                    {
                        G_Code_List.Insert(0, t);
                    }
                }
            }
            

            for (int i = temp.Length - (FIRST_LINE.Length == 0 ? 1 : 0); i <= G_Code_List.Count - 1; i++)
            //foreach (string g in G_Code_List)
            {

                if (iz < G_Code_List.Count)
                {
                    string g = G_Code_List[i];
                    string t = "";

                    t += (g.Contains("`") ? g.Substring(0, g.IndexOf("`")) : "") + (g.Contains("I_L_N")
                                ? ""
                                : LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString());
                    if (g.Contains("I_L_N"))
                    {
                        if (g.Contains("`"))
                            t += " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim());
                        else
                        {
                            t += g.Substring(5);
                            //iz--;
                        }
                    }
                    else
                    {
                        t += " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim());
                    }

                    G_Code_List[i] = t;
                }

                if (iz >= G_Code_List.Count)
                {
                    string g = G_Code_List[i];
                    if (g.Contains("I_L_N"))
                    {
                        if (g.Contains("`"))
                            G_Code_List[i] = (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim());
                        else
                        {
                            G_Code_List[i] = g.Substring(5);
                            //iz--;
                        }
                    }

                }
                iz++;

                if (iz >= Reuse_Digits)
                    iz = 1;
            }


            string[] temp2 = LAST_LINE.Split(new string[] { "/r/n" }, StringSplitOptions.None);
            if (LAST_LINE.Length > 0)
                foreach (string t in temp2.Reverse())
                {
                    G_Code_List.Add(t);
                }
            /* OLD DUMB WAY
            foreach (string g in G_Code_List)
            {
                if (ignoreFormatting)
                {
                    line += g + Environment.NewLine;
                }
                else
                {
                    if (iz == 0 && FIRST_LINE.Length > 0)
                    {
                        string[] temp = FIRST_LINE.Split(new string[] {"/r/n"}, StringSplitOptions.None);
                        foreach (string t in temp)
                        {
                            if (t.Length > 0)
                            {
                                line = line + t + Environment.NewLine;
                            }
                        }
                        FIRST_LINE = "";
                    }

                    if (iz < G_Code_List.Count)
                    {
                        line = line + (g.Contains("`") ? g.Substring(0, g.IndexOf("`")) : "") + (g.Contains("I_L_N")
                                   ? ""
                                   : LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString());
                        if (g.Contains("I_L_N"))
                        {
                            if (g.Contains("`"))
                                line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim()) +
                                       Environment.NewLine;
                            else
                            {
                                line = line + "" + g.Substring(5) + Environment.NewLine;
                                //iz--;
                            }
                        }
                        else
                        {
                            line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 1) : g.Trim()) +
                                   Environment.NewLine;
                        }
                    }

                    if (iz >= G_Code_List.Count - 1)
                    {
                        string[] temp = LAST_LINE.Split(new string[] {"/r/n"}, StringSplitOptions.None);
                        foreach (string t in temp)
                        {
                            line = line + t;
                        }
                    }
                    iz++;

                    if (iz >= Reuse_Digits)
                        iz = 1;
                }
            }
            */
            //return line;
        }

        // Create a new translator file for a set of rules; i.e. { TESTRULE1, TESTRULE2, TESTRULE3, ... }
        public void Create_New_Translator_File(string translator_name, List<List<string>> copy = null)
        {

            List<List<string>> New_Translator_File = new List<List<string>>();
            try
            {
                if (copy == null)
                    Rule_List = New_Translator_File;
                else
                    Rule_List = copy;
                _Master_Rules_List.Add(translator_name, New_Translator_File);
                Rule_Order.Insert(0, translator_name);
                Current_Rule_Name = translator_name;
                Update_Rules();

                Reset_Parameters();
                Get_Rules();
                translation_list.Items.Clear();
                _Setup_Translation_Rules();
                Update_Recent(translator_name);
                Load_Rule(translator_name);
                translation_list.SetSelected(0, true);
            }
            catch
            {
                // An existing translator with same name exists already ##raise error
            }
        }

        // Delete an existing translator
        public void Delete_Translator_File(string translator_name)
        {
            try
            {
                translation_list.Items.Clear();

                _Master_Rules_List.Remove(translator_name);
                foreach (string g in Rule_Order.ToList())
                {
                    if (g == translator_name)
                    {
                        Rule_Order.Remove(translator_name);
                    }
                }
                Update_Rules();
                Reset_Parameters();
                Get_Rules();
                translation_list.Items.Clear();
                _Setup_Translation_Rules();
                translation_list.SetSelected(0, true);
            }
            catch
            {
                // Rule does not exist; should not happen
            }
        }

        // Load file
        public void load_file_button_Click(object sender, EventArgs e)
        {
            _Setup_Rule_List();

            Translation_Complete = false;
            Store_G_Code();
            if (file_name_loaded.Length > 0)
            {
                file_loaded_text.Visible = true;
                file_loaded_text.Text = "File loaded (" + file_name_loaded + ")";
                no_file_loaded_text.Visible = false;
                file_loaded = true;
            }
        }

        // Load a G-Code from a file 
        private void Store_G_Code(bool new_file = true, string def_file_path = "")
        {
            string file_path;
            if (!_CMD_ISCMDLINE)
            {
                G_Code_List = new List<string>();
                if (new_file)
                {
                    //file_path = "T:\\LDATA\\Lathe2\\Jobs\\318444C";
                    file_path = "";
                    if (def_file_path == "")
                    {
                        OpenFileDialog file = new OpenFileDialog();
                        file.Title = "Translate File";
                        file.Multiselect = false;
                        if (file.ShowDialog() == DialogResult.OK)
                        {
                            file_path = file.FileName;
                            file_name_loaded = Path.GetFileName(file_path);
                            file_path_loaded = file_path;
                            original_file_loaded = file_path_loaded;
                        }
                    }
                    else
                    {
                        file_path = def_file_path;
                        file_name_loaded = Path.GetFileName(file_path);
                        file_path_loaded = file_path;
                    }
                }
                else
                {
                    file_path = original_file_loaded;
                    file_name_loaded = Path.GetFileName(file_path);
                }
            }
            else // cmd line
            {
                file_path = _CMD_INPUT_PATH;
                file_name_loaded = Path.GetFileName(file_path);
            }
            try
            {
                var text = File.ReadAllText(file_path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                //if (!(lines[0] == "%")) G_Code_List.Add("");
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        G_Code_List.Add(Get_Line(line) + " ") ;
                    }
                }
            }
            catch
            {
                //if (_CMD_ISCMDLINE)
                //MessageBox.Show("Error: " + e.ToString());
            }
        }

        #endregion

        //===============================================================================================================================================================

        #region ALGORITHMS

        // Reset all parameters
        private void Reset_Parameters()
        {
            DO_NOT_TRANSLATE = false;
            IGNORE_CHECK = false;
            _Master_Rules_List = new Dictionary<string, List<List<string>>>();
            Rule_Order = new List<string>();
            Rule_List = new List<List<string>>();
            Reset_Memory();
            LEADING_ZEROES = 0;
        }

        // Add all the transaction rules to the box
        private void _Setup_Translation_Rules()
        {
            foreach (string trans_rule in Rule_Order)
            {
                translation_list.Items.Add(trans_rule);
            }
            if (translation_list.Items.Count > 0)
                translation_list.SetSelected(0, true);
        }

        // Move rule_name to first in the list
        private void Update_Recent(string rule_name)
        {
            foreach (string rules in Rule_Order)
            {
                if (rules == rule_name)
                {
                    Rule_Order.RemoveAt(Rule_Order.IndexOf(rules));
                    Rule_Order.Insert(0, rule_name);
                    break; //Save resources by breaking out of loop earlier
                }
            }
        }

        // Store all the rules from the rule file to the Dictionary
        private void Get_Rules()
        {        //static Dictionary<string, List<List<string>>> _Master_Rules_List = new Dictionary<string, List<List<string>>>();
            //
            string Translator_Rule_File_Path = Directory.GetCurrentDirectory() + "\\translator_rules.txt";

            if (File.Exists(Translator_Rule_File_Path))
            {
                var text = File.ReadAllText(Translator_Rule_File_Path);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                List<List<string>> temp_list = new List<List<string>>();
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        if ((line.StartsWith("[") && line.Contains("]")) || line.Length < 1)
                        {
                            temp_list = new List<List<string>>(); //reset list
                            _Master_Rules_List.Add(line.Substring(1, line.Length - 2), temp_list);
                            Rule_Order.Add(line.Substring(1, line.Length - 2)); // Add to rules
                        }
                        else
                        {
                            if (!(line.Contains("::")))
                            {
                                // ALT+0134 = †   ALT+3 = ♦
                                List<string> paremeters = new List<string>();
                                string[] rule_lines = line.Split(new string[] { "?" }, StringSplitOptions.None);
                                foreach (string rule_entity in rule_lines) //each rule command
                                {
                                    paremeters.Add(rule_entity);
                                }
                                temp_list.Add(paremeters);
                            }
                        }
                    }
                }
            }
            else
            { 
                using (StreamWriter sw = File.CreateText(Translator_Rule_File_Path)) // Create LOG file
                {
                    //sw.Write("\n");
                    sw.Close();
                }
            }
        }

        // Reload appropriate rules when index changed
        private void translation_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (view_edit_group_button.Text.Contains("Hide"))
            {
                view_edit_group_button.PerformClick();
            }
            Loop_Info = new Dictionary<string, int>();
            Load_Rule((string)translation_list.SelectedItem);
            _Setup_Rule_List();
            button24.BackColor = SystemColors.Control;


            //Get and set the colors of the grouping
            Grouping_Color = new Dictionary<string, Color>();
            foreach (List<string> rule in Rule_List)
            {
                if (rule[5].Contains("[/GRP]"))
                {
                    int start_index = rule[5].IndexOf("[GRP]");
                    int end_index = rule[5].IndexOf("[/GRP]") + 6;
                    string[] temp = (rule[5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                    string key = temp[0].Substring(3);
                    if (!Grouping_Color.ContainsKey(key))
                    {
                        Grouping_Color.Add(key, Get_Random_Light_Color());
                        //Thread.Sleep(65);
                    }
                }
            }
        }


        private void translation_list_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            translate_line trans = new translate_line(this, _Master_Rules_List, _Master_Rules_List[(string)translation_list.SelectedItem], "", (string)translation_list.SelectedItem);
            trans.ShowDialog();
        }

        #endregion

        //===============================================================================================================================================================

        #region RULES & PROCESSING

        // MAIN Function:
        // Changes for LINE SPECIFIC Actions (So no deletion or insertion)
        private void Change_Line(int index, string original, string value, string action, string dimension, string optional_value_2 = "", string comment = "")
        {
            string temp = "";
            bool parsing = false;
            string front = "";
            string middle = "";
            string back = "";

            try
            {
                if (action == "Multiply" || action == "Divide" || action == "Add" || action == "Subtract" || action == "Set")
                {
                    string special_char = "";
                    foreach (char c in original)
                    {
                        temp = temp + c.ToString();
                        if (c.ToString() == dimension) //dimension would be X/Y/Z to distinguish where to perform arithmetic
                        {
                            parsing = true;
                        }
                        else if (parsing && middle.Length > 0)
                        {
                            back = back + c.ToString();
                        }
                        else if (parsing && (c.ToString() == ")" || c.ToString() == " " || Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$")))//When end of dimension, set middle to number and perform arithmetic
                        {
                            try
                            {
                                double g = Math.Round((Convert.ToDouble(temp.Substring(1,
                                                temp.Length -
                                                (c.ToString() == ")" || (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$"))
                                                    ? 2
                                                    : 1))) * Convert.ToDouble(value)), 4);

                                special_char = c.ToString();
                                if (action == "Multiply") middle = temp[0] + Math.Round((Convert.ToDouble(temp.Substring(1, temp.Length - (c.ToString() == ")" || (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$")) ? 2 : 1))) * Convert.ToDouble(value)), 4).ToString() + "";// (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") ? c.ToString() : " ");
                                if (action == "Divide") middle = temp[0] + Math.Round((Convert.ToDouble(temp.Substring(1, temp.Length - (c.ToString() == ")" || (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$")) ? 2 : 1))) / Convert.ToDouble(value)), 4).ToString() + "";// (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") ? c.ToString() : " ");
                                if (action == "Add") middle = temp[0] + Math.Round((Convert.ToDouble(temp.Substring(1, temp.Length -(c.ToString() == ")" ||  (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$")) ? 2 : 1))) + Convert.ToDouble(value)), 4).ToString() + "";//(Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") ? c.ToString() : " ");
                                if (action == "Subtract") middle = temp[0] + Math.Round((Convert.ToDouble(temp.Substring(1, temp.Length - (c.ToString() == ")" || (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$")) ? 2 : 1))) - Convert.ToDouble(value)), 4).ToString() + "";//(Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") ? c.ToString() : " ");
                                if (action == "Set") middle = temp[0] + value;
                            }
                            catch
                            {
                                G_Code_List[index] = original;
                                return;
                            }
                        }
                        else if (!parsing && middle.Length < 1)
                        {
                            temp = "";
                            front = front + c.ToString();
                        }
                    }
                    //if (!(middle.Contains("."))) middle = middle +".0"; // Add trailing '.0' for significant digit
                    //Console.WriteLine(middle);
                    if (parsing)
                        G_Code_List[index] = front + (middle.StartsWith(".") ? "" : middle.Contains(".") ? middle : middle.Trim() + ".") + "" + special_char + back;
                    else
                        G_Code_List[index] = front + middle + back;
                    Change_Count++;
                }
                else if (action == "ReplaceSpecific")
                {
                    G_Code_List[index] = original.Replace(value, optional_value_2);
                    Change_Count++;
                }
                else if (action == "Replace")
                {
                    G_Code_List[index] = value;
                    Change_Count++;
                }
                else if (action == "InsertFrom") // For every instance of original // value is the index of the original, value is the value of increment
                {
                    if (Convert.ToInt32(value) < 0)
                    {
                        value = (Convert.ToInt32(value) + 1).ToString();
                    }
                    G_Code_List.Insert(index + Convert.ToInt32(value), (comment.Contains("I_L_N") ? "I_L_N" : "") + optional_value_2 + " ");
                    Change_Count++;
                }
                else if (action == "DeleteFrom") // For every instance of original  
                {
                    if (Convert.ToInt32(value) < 0)
                    {
                        value = (Convert.ToInt32(value)).ToString();
                    }
                    G_Code_List.RemoveAt(index + Convert.ToInt32(value));
                    Change_Count++;
                }
                else if (action == "DeleteAt")
                {
                    G_Code_List.RemoveAt(Convert.ToInt32(value));
                    Change_Count++;
                }
                else if (action == "DELETE_PAST_VALUE")
                {
                    try
                    {
                        G_Code_List[index] = G_Code_List[index].Substring(0, G_Code_List[index].IndexOf(dimension));
                        Change_Count++;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (action == "DeleteIf")
                {
                    G_Code_List.RemoveAt(index);
                    Change_Count++;
                }
                else if (action == "InsertAt")
                {
                    //G_Code_List.Insert(Convert
                    G_Code_List.Insert(Convert.ToInt32(value), (comment.Contains("I_L_N") ? "I_L_N" : "") + optional_value_2 + " ");
                    Change_Count++;
                }
                else if (action == "ReplaceAt")
                {
                    G_Code_List[Convert.ToInt32(value)] = optional_value_2;
                    Change_Count++;
                }
                else if (action == "Switch")
                {
                    string temp_string = "";
                    temp_string = G_Code_List[Convert.ToInt32(value)];
                    G_Code_List[Convert.ToInt32(value)] = G_Code_List[Convert.ToInt32(optional_value_2)];
                    G_Code_List[Convert.ToInt32(optional_value_2)] = temp_string;
                    Change_Count++;
                }
                else if (action == "MultiRule")
                {
                    // PARSE VALUE (CONDITIONS) "-2|M00~-3|GLO~4|GZ";
                    bool condition_satisfied = true;
                    string[] conditions = value.Split(new string[] { "~" }, StringSplitOptions.None);
                    foreach (string condition in conditions)
                    {
                        string[] parameter = condition.Split(new string[] { "|" }, StringSplitOptions.None);
                        if (parameter.Count() > 2 && parameter[2] == "Does Not")
                        {
                            if ((G_Code_List[index + Convert.ToInt32(parameter[0])].Contains(parameter[1])))
                                condition_satisfied = false;
                        }
                        else
                        {
                            if (!(G_Code_List[index + Convert.ToInt32(parameter[0])].Contains(parameter[1])))
                                condition_satisfied = false;
                        }
                    }

                    // PARSE OPTIONAL_VALUE (ACTIONS) "Delete|-4~Insert|6|Hello~Replace|-3|Baby"
                    if (condition_satisfied) // If parameters are satisifed
                    {
                        string[] actions = optional_value_2.Split(new string[] { "~" }, StringSplitOptions.None);
                        foreach (string action2 in actions)
                        {
                            string[] parameter = action2.Split(new string[] { "|" }, StringSplitOptions.None);
                            if (parameter[0].Contains("Delete"))
                            {
                                G_Code_List.RemoveAt(index + Convert.ToInt32(parameter[1]));
                                refresh_value--;
                                Change_Count++;
                            }
                            else if (parameter[0].Contains("InsertAtStart"))
                            {
                                string back_end = G_Code_List[index + Convert.ToInt32(parameter[1])];
                                G_Code_List[index + Convert.ToInt32(parameter[1])] = (comment.Contains("I_L_N") ? "I_L_N" : "") + parameter[2] + "" + back_end;
                                Change_Count++;
                            }
                            else if (parameter[0].Contains("InsertAtEnd"))
                            {
                                string front_end = G_Code_List[index + Convert.ToInt32(parameter[1])];
                                G_Code_List[index + Convert.ToInt32(parameter[1])] = (comment.Contains("I_L_N") ? "I_L_N" : "") + front_end + parameter[2] + "";
                                Change_Count++;
                            }
                            else if (parameter[0].Contains("Insert"))
                            {
                                int INSERT_INDEX = Convert.ToInt32(parameter[1]);
                                G_Code_List.Insert(index + (INSERT_INDEX <= 0 ? INSERT_INDEX + 1 : INSERT_INDEX), parameter[2] + "");
                                refresh_value++;
                                Change_Count++;
                            }
                            else if (parameter[0].Contains("ReplaceWithin"))
                            {
                                G_Code_List[index + Convert.ToInt32(parameter[1])] = G_Code_List[index + Convert.ToInt32(parameter[1])].Replace(parameter[2], parameter[3]);
                                Change_Count++;
                            }
                            else if (parameter[0].Contains("Replace"))
                            {
                                G_Code_List[index + Convert.ToInt32(parameter[1])] = parameter[2];
                                Change_Count++;
                            }

                        }
                    }
                }
                // Replace last index of "" with ""
                else if (action == "LastIndexOfReplace")
                {
                    bool _TWO_SIDED = false;
                    int _INDEX_OF_M00 = 0;
                    int i = 0;
                    // Check if GCode is two sided (check each line for M00)
                    foreach (string line in G_Code_List)
                    {
                        if (line.Contains("M00"))
                        {
                            _TWO_SIDED = true;
                            _INDEX_OF_M00 = i;
                        }
                        i++;
                    }
                    if (!_TWO_SIDED)
                    {
                        //Console.WriteLine(G_Code_List.LastIndexOf(G_Code_List[index]) + "  " + G_Code_List[index]);
                        G_Code_List[G_Code_List.LastIndexOf(G_Code_List[index])] = value; // set last instanec of G_CodeList value
                        Change_Count++;
                    }
                    else
                    {
                        List<string> _PART_ONE = G_Code_List.GetRange(0, _INDEX_OF_M00);
                        List<string> _PART_TWO = G_Code_List.GetRange(_INDEX_OF_M00 + 1, G_Code_List.Count - 1 - _INDEX_OF_M00);
                        if (_PART_ONE.Contains(G_Code_List[index]))
                        {
                            int _INDEX_OF_FIRST = _PART_ONE.LastIndexOf(G_Code_List[index]);
                            G_Code_List[_INDEX_OF_FIRST] = value; // set last instanec of G_CodeList value
                            Change_Count++;
                        }

                        if (_PART_TWO.Contains(G_Code_List[index]))
                        {
                            int _INDEX_OF_SECOND = _PART_TWO.LastIndexOf(G_Code_List[index]);
                            G_Code_List[_INDEX_OF_SECOND + _INDEX_OF_M00 + 1] = value; // set last instanec of G_CodeList value
                            Change_Count++;
                        }
                    }
                }
                else if (action == "FORMAT_RULE")
                {
                    if (dimension == "FILE_CONTAINS")
                    {
                        if (optional_value_2 == "must_not" ? true : false)
                        {
                            if (original.Contains(value) ? true : false)
                                DO_NOT_TRANSLATE = true;
                        }
                        else // must contain value
                        {
                            if (original.Contains(value))
                            {
                                DO_NOT_TRANSLATE = false;
                                IGNORE_CHECK = true;
                            }
                            else if (!IGNORE_CHECK)
                            {
                                DO_NOT_TRANSLATE = true;
                                IGNORE_CHECK = true;
                            }
                        }
                    }
                }
                else if (action == "StoreValue")
                {
                    if (dimension.Length > 0)
                    {
                        int compare_index = 0;
                        bool comparing_dimensions = false;
                        if (dimension.Contains("@!")) // store line number
                        {
                            MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(value))] = index.ToString();
                            STORED_COUNT++;
                        }
                        else if (!(dimension.Contains("@#"))) // If not specific value
                        {
                            foreach (char c in original)
                            {
                                temp = temp + c.ToString();
                                if (c.ToString() == dimension[compare_index].ToString()) //dimension would be X/Y/Z to disguish where to perform arithmetic
                                {
                                    comparing_dimensions = true;
                                    temp = "";
                                }
                                else if (parsing && middle.Length > 0)
                                {
                                    back = back + c.ToString();
                                }
                                else if (parsing && (c.ToString() == " " || c.ToString() == ")" || char.IsLetter(c))) //When end of dimension, set middle to number and perform arithmetic
                                {
                                    middle = temp.Substring(0, temp.Length - 1);
                                }
                                else if (!parsing && comparing_dimensions && Char.IsDigit(c))
                                {
                                    parsing = true;
                                }
                                else if (!parsing && middle.Length < 1)
                                {
                                    temp = "";
                                    front = front + c.ToString();
                                }
                                else if (!(c.ToString() == dimension[compare_index].ToString()))
                                {
                                    comparing_dimensions = false;
                                }
                            }
                        }
                        else
                        {
                            middle = dimension.Substring(2);
                        }
                        //if (!(middle.Contains(".")))
                        //middle = middle;// +".0"; // Add trailing '.0' for significant digit

                        if (optional_value_2.Length > 0 && !dimension.Contains("@!")) //if has rule to perform arithmetic on storage bin
                        {
                            try
                            {
                                double digit = Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(value)]);
                                if (optional_value_2 == "Multiply") MEMORY_BIN[Convert.ToInt32(value)] = (digit * Convert.ToDouble(middle)).ToString();
                                if (optional_value_2 == "Divide") MEMORY_BIN[Convert.ToInt32(value)] = (digit / Convert.ToDouble(middle)).ToString();
                                if (optional_value_2 == "Subtract") MEMORY_BIN[Convert.ToInt32(value)] = (digit - Convert.ToDouble(middle)).ToString();
                                if (optional_value_2 == "Add") MEMORY_BIN[Convert.ToInt32(value)] = (digit + Convert.ToDouble(middle)).ToString();
                            }
                            catch (Exception e)//if performing arithmetic not possible, store value instead
                            {
                                //Console.WriteLine(e);
                                MEMORY_BIN[Convert.ToInt32(value)] = GET_TEXT_WITH_BIN(middle);
                            }
                        }
                        else if (!dimension.Contains("@!"))
                        {
                            MEMORY_BIN[Convert.ToInt32(value)] = GET_TEXT_WITH_BIN(middle);
                        }
                        STORED_COUNT++;
                    }
                    else
                    {
                        MEMORY_BIN[Convert.ToInt32(value)] = original;
                        STORED_COUNT++;
                    }
                }  
                else if (action == "RunQuery")
                {
                    //string query = enc_obj.Decrypt(dimension);
                    string query = dimension;
                    if (!(query.ToUpper().Contains("DROP")))
                    {
                        OdbcDataReader reader;
                        database.Open(Database.DECADE_MARKHAM);
                        reader = database.RunQuery(GET_TEXT_WITH_BIN(query));
                        reader.Read();
                        try
                        {
                            MEMORY_BIN[Convert.ToInt32(value)] = reader[0].ToString();
                        }
                        catch
                        {
                            
                        }
                        reader.Close();
                    }
                    Change_Count++;
                }
                else if (action == "COMPARE_TO_REF_LINE")
                {
                    string[] temp2 = dimension.Split(new string[] { "|" }, StringSplitOptions.None);
                    int check_index_range = Convert.ToInt32((temp2[2] == "Above" ? "-" : "") + temp2[1]);
                    int negative_range = check_index_range < 0 ? check_index_range : 0;
                    bool one_time_only = false;
                    for (int reference_index = index + negative_range; reference_index < check_index_range + index - negative_range; reference_index++)
                    {
                        if (G_Code_List[reference_index].Contains(temp2[0]) && !one_time_only)
                        {
                            if (comment.Contains("FIRST_INSTANCE_ONLY"))
                            {
                                one_time_only = true;
                            }
                            if (temp2[3] == "ReplaceWithin")
                            {
                                G_Code_List[reference_index] = G_Code_List[reference_index].Replace(temp2[0], temp2[4]);
                                Change_Count++;
                            }
                            else if (temp2[3] == "InsertAtEnd")
                            {
                                G_Code_List[reference_index] = G_Code_List[reference_index] + temp2[4];
                                Change_Count++;
                            }
                            else if (temp2[3] == "InsertAtStart")
                            {
                                G_Code_List[reference_index] = temp2[4] + G_Code_List[reference_index];
                                Change_Count++;
                            }
                            if (temp2[5] == "1")
                            {
                                G_Code_List[index] = temp2[4] + G_Code_List[index];
                                Change_Count++;
                            }
                        }
                    }
                }
                else if (action == "RANGE_FUNCTION")
                {
                    int insert_index = 0;
                    string[] temp2 = dimension.Split(new string[] { "|" }, StringSplitOptions.None);
                    if (temp2.Count() > 3) insert_index = Convert.ToInt32(temp2[3]);
                    if (temp2[0] == "Move")
                    {
                        // Copy
                        List<string> temp_list = G_Code_List.GetRange(Convert.ToInt32(temp2[1]), Convert.ToInt32(temp2[2]) - Convert.ToInt32(temp2[1]));

                        // Remove
                        for (int i = Convert.ToInt32(temp2[2]); i > Convert.ToInt32(temp2[1]); i--)
                        {
                            G_Code_List.RemoveAt(Convert.ToInt32(temp2[1]));
                        }

                        // Replace
                        foreach (string p in temp_list)
                        {
                            G_Code_List.Insert(insert_index, p);
                            insert_index++;
                        }
                    }
                    if (temp2[0] == "Copy")
                    {
                        // Copy
                        List<string> temp_list = G_Code_List.GetRange(Convert.ToInt32(temp2[1]), Convert.ToInt32(temp2[2]) - Convert.ToInt32(temp2[1]));

                        // Replace
                        foreach (string p in temp_list)
                        {
                            G_Code_List.Insert(insert_index, p);
                            insert_index++;
                            Change_Count++;
                        }
                    }
                    if (temp2[0] == "Delete")
                    {
                        // Copy
                        List<string> temp_list = G_Code_List.GetRange(Convert.ToInt32(temp2[1]), Convert.ToInt32(temp2[2]) - Convert.ToInt32(temp2[1]));

                        // Remove
                        for (int i = Convert.ToInt32(temp2[2]); i > Convert.ToInt32(temp2[1]); i--)
                        {
                            G_Code_List.RemoveAt(Convert.ToInt32(temp2[1]));
                            Change_Count++;
                        }
                    }
                }
                else if (action == "GET_INPUT")
                {
                    string input = "";
                    string[] temp2 = dimension.Split(new string[] { "|" }, StringSplitOptions.None);
                    while (input.Length <= 0)
                    {
                        input = Microsoft.VisualBasic.Interaction.InputBox(temp2[0], "", "Input", -1, -1);
                    }
                    try
                    {
                        MEMORY_BIN[Convert.ToInt32(temp2[1])] = input;
                    }
                    catch { }
                }
                else if (action == "RunTranslator" && !(dimension == Current_Rule_Name))
                {
                    string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation";
                    try
                    {
                        #region Save what has been translated

                        string temp_translate = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
                        File.Delete(temp_translate);
                        if (File.Exists(temp_translate))
                        {
                            // If file exists, delete it.
                            //button3.PerformClick();
                        }
                        else
                        {
                            Translation_Complete = true;
                            using (StreamWriter sw = File.CreateText(temp_translate)) // Create LOG file
                            {
                                //sw.Write("\n");

                                int iz = 0;
                                string line = "";
                                foreach (string g in G_Code_List)
                                {
                                    if (iz == 0)
                                    {
                                        line = line + g + Environment.NewLine;
                                    }
                                    else if (iz == G_Code_List.Count - 1)
                                    {
                                    }
                                    else
                                    {
                                        line = line + (g.Contains("`") ? g.Substring(0, g.IndexOf("`")) : "") + (g.Contains("I_L_N") ? "" : LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString());
                                        if (g.Contains("I_L_N"))
                                        {
                                            if (g.Contains("`"))
                                                line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 2) : g) + Environment.NewLine;
                                            else
                                            {
                                                line = line + "" + g.Substring(5) + Environment.NewLine;
                                                iz--;
                                            }
                                        }
                                        else
                                        {
                                            line = line + " " + (g.Contains("`") ? g.Substring(g.IndexOf("`") + 2) : g) + Environment.NewLine;
                                        }

                                        //line = line + LEADING_LETTER + Process_Leading_Zeroes(iz, LEADING_ZEROES) + iz.ToString() + " " + g + Environment.NewLine;
                                    }
                                    iz++;
                                    if (iz >= Reuse_Digits)
                                        iz = 1;
                                }
                                sw.Write(line);
                                sw.Close();
                            }
                        }
                        #endregion
                        Translator self = new Translator(temp_translation + ".txt", dimension, temp_translation + "_2.txt", "SKIP_EXIT");
                    }
                    catch { }
                    //testrobin
                    Store_G_Code(true, temp_translation + "_2.txt");
                    File.Delete(temp_translation + "_2.txt");
                    //file_name_loaded = temp_translation + ".txt";
                }
                else if (action.Contains("WarningMSG"))
                {
                    if (action.Contains("wTERM"))
                    {
                        if (action.Contains("_SILENT"))
                        {
                            //End program silently without message (no translation)
                            this.Close();
                            this.Dispose();
                            Environment.Exit(0);
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show(dimension, "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                this.Close();
                                this.Dispose();
                                Environment.Exit(0);
                            }
                            else if (dialogResult == DialogResult.Yes)
                            {
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(dimension);
                    }
                }
                else if (action.Contains("_File"))
                {
                    try
                    {
                        if (action.Contains("Delete"))
                        {
                            File.Delete(dimension);
                        }
                        else if (action.Contains("Move"))
                        {
                            File.Move(dimension, value);
                        }
                        else if (action.Contains("Copy"))
                        {
                            File.Move(dimension, value);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file path for source or destination path");
                    }
                }
                else if (action.Contains("IF_THEN"))
                {
                    List<If_Then_Conditions> If_Then_List = new List<If_Then_Conditions>();
                    string[] info = dimension.Split(new string[] { "~" }, StringSplitOptions.None);
                    foreach (string statement in info)
                    {
                        string[] Statements = statement.Split(new string[] { "|" }, StringSplitOptions.None);
                        {
                            If_Then_Conditions ITC = new If_Then_Conditions();
                            ITC.condition_bin = Convert.ToInt32(Statements[0]);
                            ITC.condition_comparison = Statements[1];
                            ITC.condition_value = Statements[2];
                            ITC.action_comparison = Statements[3];
                            ITC.action_bin = Convert.ToInt32(Statements[4]);
                            ITC.action_value = Statements[5];
                            If_Then_List.Add(ITC);
                        }
                    }
                    for (int i = 0; i < If_Then_List.Count; i++)
                    {
                        if (If_Then_List[i].condition_comparison == "=")
                        {
                            if (Convert.ToDouble(MEMORY_BIN[If_Then_List[i].condition_bin]) == Convert.ToDouble(If_Then_List[i].condition_value))
                            {
                                Execute_IFTHEN_Action(If_Then_List[i]);
                                i = i + 99999;
                            }
                        }
                        else if (If_Then_List[i].condition_comparison == "<")
                        {
                            if (Convert.ToDouble(MEMORY_BIN[If_Then_List[i].condition_bin]) < Convert.ToDouble(If_Then_List[i].condition_value))
                            {
                                Execute_IFTHEN_Action(If_Then_List[i]);
                                i = i + 99999;
                            }
                        }
                        else if (If_Then_List[i].condition_comparison == ">")
                        {
                            if (Convert.ToDouble(MEMORY_BIN[If_Then_List[i].condition_bin]) > Convert.ToDouble(If_Then_List[i].condition_value))
                            {
                                Execute_IFTHEN_Action(If_Then_List[i]);
                                i = i + 99999;
                            }
                        }
                    }
                }
                else if (false) // never happen
                {
                    // Blank
                }
                else
                {
                    // Should never happen
                    G_Code_List[index] = "";
                    Change_Count++;
                }
                Change_Index.Add(index);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        // Go through IF_THEN statement and set bin based on valuation
        private void Execute_IFTHEN_Action(If_Then_Conditions ITC_2)
        {
            If_Then_Conditions ITC = ITC_2;
            try
            {
                if (ITC.action_comparison == "Set")
                {
                    MEMORY_BIN[ITC.action_bin] = ITC.action_value;
                }
                else if (ITC.action_comparison == "Add")
                {
                    MEMORY_BIN[ITC.action_bin] = (Convert.ToDouble(MEMORY_BIN[ITC.action_bin]) + Convert.ToDouble(ITC.action_value)).ToString();
                }
                else if (ITC.action_comparison == "Subtract")
                {
                    MEMORY_BIN[ITC.action_bin] = (Convert.ToDouble(MEMORY_BIN[ITC.action_bin]) - Convert.ToDouble(ITC.action_value)).ToString();
                }
                else if (ITC.action_comparison == "Multiply")
                {
                    MEMORY_BIN[ITC.action_bin] = (Convert.ToDouble(MEMORY_BIN[ITC.action_bin]) * Convert.ToDouble(ITC.action_value)).ToString();
                }
                else if (ITC.action_comparison == "Divide")
                {
                    MEMORY_BIN[ITC.action_bin] = (Convert.ToDouble(MEMORY_BIN[ITC.action_bin]) / Convert.ToDouble(ITC.action_value)).ToString();
                }
            }
            catch
            {
            }
        }

        private int Reuse_Digits = 0;

        private Dictionary<string, Color> Grouping_Color = new Dictionary<string, Color>();

        List<string> HTML_Color_List = new List<string>() { "F5A9A9" , "F3E2A9" , "A9F5A9" , "A9D0F5" , "A9F5E1" , "F5A9F2" , "F5A9BC" , 
                                                            "F3F781" , "81F781" , "F79F81" , "81F781" , "81DAF5" , "F5DA81" , "F781D8" ,
                                                            "8181F7" , "D8D8D8" , "CEF6D8" , "FAAC58" , "9E6DF9" , "87A6C1" , "87C1AA" ,
                                                            "A4C187" , "C19E87" , "EF6FD1" , "6FEFBA" , "87BEC1" , "88EF6F" , "6FA2EF" };

        private int HTML_Color_Index = 0;

        List<int> Do_Not_Touch_Indices = new List<int>();

        private Color Get_Random_Light_Color()
        {
            Color randomColor = System.Drawing.ColorTranslator.FromHtml("#" + HTML_Color_List[HTML_Color_Index]);

            if (HTML_Color_Index >= HTML_Color_List.Count - 1)
            {
                HTML_Color_Index = 0;
            }
            else
            {
                HTML_Color_Index++;
            }

            //System.Drawing.ColorTranslator.FromHtml("#B5E0FF")
            /*
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];

            float brightness = randomColor.GetBrightness();
            float midnightBlueBrightness = 0.52F;// Color.FromKnownColor(KnownColor.LightPink).GetBrightness();
            float transparentBrightness = Color.FromKnownColor(KnownColor.Transparent).GetBrightness();

            while (brightness < midnightBlueBrightness || brightness > transparentBrightness)
            {
                names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
                randomColorName = names[randomGen.Next(names.Length)];
                randomColor = Color.FromKnownColor(randomColorName);
                brightness = randomColor.GetBrightness();
            }
            */
            return randomColor;
        }

        private bool ignoreFormatting = false;

        // Setup the rules associated with translation file
        public string _Setup_Rule_List(bool return_string = false, int index = 0)
        {
            if (!return_string)
            {
                Reset_Memory();
                if (!_CMD_ISCMDLINE) translation_rules.Items.Clear(); // Don't modify any object if cmd line
            }

            List<string> rules;
            int count = Rule_List.Count;
            FORMAT_RULES_COUNT = 0;
            DO_NOT_TRANSLATE = false;
            FIRST_LINE = "";
            LAST_LINE = "";


            // foreach (List<String> rules in Rule_List)
            for (int i = 0; i < count - FORMAT_RULES_COUNT; i++)
            {
                rules = Rule_List[i];
                // Setup all the format rules internally; the GUI will not display these rules (hence, GOTO Finish)
                if (rules[0] == "FORMAT_RULE")
                {
                    List<string> temp = rules;
                    Rule_List.RemoveAt(i);
                    Rule_List.Add(temp);
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "LEADING_ZEROES")
                    {
                        LEADING_ZEROES = Convert.ToInt32(rules[2]);
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "REUSE_VALUES")
                    {
                        Reuse_Digits = Convert.ToInt32(rules[2]);
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "IGNORE_FORMATTING")
                    {
                        ignoreFormatting = rules[2] == "1";
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "LEADING_LETTER")
                    {
                        LEADING_LETTER = rules[2];
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "FIRST_LINE")
                    {
                        FIRST_LINE = rules[2];
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "LAST_LINE")
                    {
                        LAST_LINE = rules[2];
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "BIN_ASSIGNMENT")
                    {
                        //List<int> Do_Not_Touch_Indices = new List<int>();

                        string[] split_1 = rules[2].Split(new string[] { "|" }, StringSplitOptions.None);
                        foreach (string g in split_1)
                        {
                            string[] split_2 = g.Split(new string[] { "~" }, StringSplitOptions.None);
                            MEMORY_BIN[Convert.ToInt32(split_2[0])] = split_2[1];

                            Do_Not_Touch_Indices.Add(Convert.ToInt32(split_2[0]));
                            //Bin_Assignment_List.Add(new List<string>() { split_2[0], split_2[1], split_2[2] });
                        }
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "DATABASE_INFO")
                    {
                        string[] temp_split = rules[2].Split(new string[] { "†" }, StringSplitOptions.None);
                        try
                        {
                            database._SET_CREDENTIALS(enc_obj.Decrypt(temp_split[0]), enc_obj.Decrypt(temp_split[1]), enc_obj.Decrypt(temp_split[2]), enc_obj.Decrypt(temp_split[3]));
                        }
                        catch
                        {
                        }
                    }
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) == "LIST_OF_CONDITIONS" && rules[2] == "FILENAME_CONTAINS" && !DO_NOT_TRANSLATE)
                    {
                        if (rules[4] == "must_not" ? true : false)
                        {
                            DO_NOT_TRANSLATE = file_name_loaded.Contains(rules[3]) ? true : false;
                        }
                        else
                        {
                            DO_NOT_TRANSLATE = file_name_loaded.Contains(rules[3]) ? false : true;
                        }
                    }
                    FORMAT_RULES_COUNT++;
                    i--;
                    goto Finish; // Do not populate format rules here
                }
                if (_CMD_ISCMDLINE) goto Finish;


                string rule_line = "";
                string grouped_line = "";
                int index_value = -1;

                if (!return_string)
                {
                    if (Show_Grouped && rules[5].Contains("[GRP]") && rules[5].Contains("[/GRP]")) grouped_line = "[GROUPED] ";
                    index_value = Rule_List.IndexOf(rules) + 1;
                    if (index_value < 10) rule_line = rule_line + (Rule_List.Count - FORMAT_RULES_COUNT < 100 ? "0" : "00");
                    else if (index_value < 100) rule_line = rule_line + (Rule_List.Count - FORMAT_RULES_COUNT < 100 ? "" : "0");
                    rule_line = rule_line + index_value.ToString() + ")  ";
                    rule_line = rule_line + grouped_line;
                    //rule_line = rule_line + (rules[5].Length > 1 ? "[C] " : "");
                }

                if (!checkBox1.Checked)
                {
                    //Rule_List = [action, search_value, dimension, value, optional_value, comment];
                    if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Length < 1 && (rules[0] == "DeleteAt" || rules[0] == "InsertAt")) // if no search_value (doesn't search)
                    {
                        // DeleteAt, InsertAt
                        rule_line = rule_line + rules[0] + " -> Line " + rules[3];
                        if (rules[4].Length > 0) // If insert
                        {
                            rule_line = rule_line + " with value -> " + rules[4];
                        }
                    }
                    else if ((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Length > 0 && (rules[0] == "DeleteAt" || rules[0] == "InsertAt"))
                    {
                        string[] temp = (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Split(new string[] { "|" }, StringSplitOptions.None);
                        rule_line = rule_line + rules[0] + " -> Line " + rules[3] + " only if global conditions met (" + temp[0] + " " + temp[1] + " instance(s) of '" + temp[2] + "' found in file)";

                        if (rules[4].Length > 0) // If insert
                        {
                            rule_line = rule_line + " with value -> " + rules[4] + " only if global conditions met(" + temp[0] + " " + temp[1] + " instance(s) of '" + temp[2] + "' found in file)";
                        }
                    }
                    else if (rules[0] == "Multiply" || rules[0] == "Divide" || rules[0] == "Subtract" || rules[0] == "Add" || rules[0] == "Set")
                    {
                        rule_line = rule_line + "If line contains: '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) + "' -> " + rules[0] + " -> " + rules[2] + " coordinates by: " + rules[3];
                    }
                    else if (rules[0] == "InsertFrom")
                    {
                        rule_line = rule_line + rules[0] + " -> If line contains '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' -> insert '" + rules[4] + "' -> " + rules[3] + " lines";
                    }
                    else if (rules[0] == "DeleteFrom")
                    {
                        rule_line = rule_line + rules[0] + " -> If line contains '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' -> delete the line " + rules[3] + " units above/below it";
                    }
                    else if (rules[0] == "ReplaceSpecific")
                    {
                        rule_line = rule_line + rules[0] + " -> If line contains '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' -> replace '" + rules[3] + "' with '" + rules[4] + "'";
                    }
                    else if (rules[0] == "Replace")
                    {
                        rule_line = rule_line + rules[0] + " -> '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' with -> '" + rules[3] + "'";
                    }
                    else if (rules[0] == "DeleteIf")
                    {
                        rule_line = rule_line + rules[0] + " -> Line containing '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "'";
                    }
                    else if (rules[0] == "Switch")
                    {
                        rule_line = rule_line + rules[0] + " -> Line #" + rules[3] + " with Line#" + rules[4];
                    }
                    else if (rules[0] == "MultiRule")
                    {
                        rule_line = rule_line + "Multi-Conditional Rule -> reference line: '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", "' or '") + (rules[3].Contains("~") ? "'" : "' check for -> '" + rules[3].Split(new string[] { "|" }, StringSplitOptions.None)[1] + "'");
                        //rule_line = rule_line + "Multi-Conditional Rule based on reference line: '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) + "'" + rules[3] + " = " +  rules[4];

                    }
                    else if (rules[0] == "ReplaceAt")
                    {
                        rule_line = rule_line + "ReplaceAt -> Line #" + rules[3] + " with value '" + rules[4] + "'";
                    }
                    else if (rules[0] == "LastIndexOfReplace")
                    {
                        rule_line = rule_line + "Replace last instance of -> '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' with -> '" + rules[3] + "' (Automatically checks for double-sided 'M00' clause)";
                    }
                    else if (rules[0] == "StoreValue")
                    {
                        if (rules[2].Contains("@#")) // if store specific value
                        {
                            rule_line = rule_line + "StoreValue -> Find value: " + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + " -> " + "Store the value: '" + rules[2].Substring(2) + "' in memory location (bin#): " + rules[3];
                        }
                        else if (rules[2].Contains("@!"))
                        {
                            rule_line = rule_line + "StoreValue -> Find value: " + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + " -> " + "Store line number in memory location (bin#): " + rules[3];
                        }
                        else
                        {
                            rule_line = rule_line + "StoreValue -> Find value: " + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + " -> " + (rules[2].Length > 0 ? "Store digit associated with dimension: '" + rules[2] : "Store entire line") + "' in memory location (bin#): " + rules[3];
                        }
                    }
                    else if (rules[0] == "RunQuery")
                    {
                        //rule_line = rule_line + (true ? "RunQuery -> '" + enc_obj.Decrypt(rules[2]) + "'" : "");
                        rule_line = rule_line + (true ? "RunQuery -> '" + (rules[2]) + "'" : "");
                    }
                    else if (rules[0] == "RunTranslator")
                    {
                        rule_line = rule_line + "Execute translator with name '" + rules[2] + "' -> when " + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + " is found";
                    }
                    else if (rules[0].Contains("WarningMSG"))
                    {
                        rule_line = rule_line + "Display Message:  '" + rules[2] + "' -> when " + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + " is found" + (rules[0].Contains("wTERM") ? " (YES/NO dialog)" : "");
                    }
                    else if (rules[0].Contains("_File"))
                    {
                        if (rules[0].Contains("Delete"))
                        {
                            rule_line = rule_line + "Delete file from:  '" + rules[2] + "' -> when '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' is found";
                        }
                        else
                        {
                            rule_line = rule_line + (rules[0].Contains("Insert") ? "Insert" : "Move") + " file from:  '" + rules[2] + "' to '" + rules[3] + "' -> when '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' is found";
                        }
                    }
                    else if (rules[0] == "LOOP_CONDITION_START")
                    {
                        rule_line = rule_line + "↓ LOOP START " + (rules[3].Length > 0 ? " - Loop until bin#" + rules[4] + " is " + rules[3] + " the value " + rules[2] : " - Loop " + rules[2] + " times");
                    }
                    else if (rules[0] == "LOOP_CONDITION_END")
                    {
                        rule_line = rule_line + "↑ LOOP END";
                        if (Loop_Info.ContainsKey((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1])))
                        {
                            Loop_Info.Remove((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]));
                        }
                        Loop_Info.Add((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]), i);
                    }
                    else if (rules[0] == "IF_THEN")
                    {
                        rule_line = rule_line + "If/Then Condition when '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]).Replace("_", " or ") + "' is found";
                    }
                    else if (rules[0] == "RANGE_FUNCTION")
                    {
                        string[] temp = rules[2].Split(new string[] { "|" }, StringSplitOptions.None);
                        rule_line = rule_line + "Perform Range function '" + temp[0] + "' between line# " + temp[1] + " and line# " + temp[2] + (temp.Count() > 3 ? " to line# " + temp[3] : "");
                    }
                    else if (rules[0] == "COMPARE_TO_REF_LINE")
                    {
                        string[] temp = rules[2].Split(new string[] { "|" }, StringSplitOptions.None);
                        rule_line = rule_line + "Look for reference '" + (rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]) + "', check " + temp[1] + " lines " + temp[2] + " for the value '" + temp[0] + "' and " + temp[3] + " the value '" + temp[4] + "'";
                    }
                    else if (rules[0] == "GET_INPUT")
                    {
                        string[] temp = rules[2].Split(new string[] { "|" }, StringSplitOptions.None);
                        rule_line = rule_line + "Get user input: '" + temp[0] + "' (Store in bin # " + temp[1] + ")";
                    }
                    else if (rules[0] == "DELETE_PAST_VALUE")
                    {
                        rule_line = rule_line + "Find the line containing '" + rules[1] + "' and find the value within this line '" + rules[2] + "' and delete everything past this value (including this value)";
                    }
                    else
                    {
                        // not in action
                    }
                    if (rules[5].Contains("IFCHK")) rule_line = rule_line + " (w/ condition)";
                }
                else
                {
                    string g = rules[5];

                    g = g.Contains("FIRST_INSTANCE_ONLY") ? g.Substring(19) : g;
                    g = g.Contains("I_L_N") ? g.Substring(0, g.IndexOf("I_L_N")) + g.Substring(g.IndexOf("I_L_N") + 5) : g;
                    g = g.Contains("IFCHK") ? g.Substring(g.IndexOf("))") + 2) : g;
                    g = g.Contains("*SPECIAL MODE RULE*") ? g.Substring(g.IndexOf("*SPECIAL MODE RULE*") + 19) : g;
                    if (g.Contains("[/GRP]"))
                    {
                        int start_index = g.IndexOf("[GRP]");
                        int end_index = g.IndexOf("[/GRP]") + 6;
                        g = g.Substring(end_index);
                    }
                    rule_line = rule_line + g;
                }
                if (return_string && index == i)
                {
                    return rule_line;
                }
                else if (!return_string)
                {
                    translation_rules.Items.Add(rule_line);
                }
            Finish:

                if (rules[0] == "LOOP_CONDITION_END" && _CMD_ISCMDLINE)
                {
                    if (Loop_Info.ContainsKey((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1])))
                    {
                        Loop_Info.Remove((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]));
                    }
                    Loop_Info.Add((rules[1].Contains("S~W~I") ? rules[1].Substring(5) : rules[1]), i);
                }
            if (Show_Grouped && rules[5].Contains("[GRP]")) translation_rules.SetSelected(i, true);
            }

            return "";
        }

        // Function to track changes made based on the indices listed in Change_Index
        private void Change_Track(bool show_changes)
        {
            return; // do nothing -- depreciated

            if (!DO_NOT_TRANSLATE && file_loaded)
            {
                string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
                string output_file = "";
                int index = 0;
                var text = File.ReadAllText(temp_translation);
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                if (show_changes)
                {
                    foreach (string line in lines)
                    {
                        if (Change_Index.Contains(index))
                        {
                            output_file = output_file + ">> ";
                        }
                        output_file = output_file + line + Environment.NewLine;
                        index++;
                    }
                }
                else // Revert state;  not currently used but if required, run function as false or empty condition
                {
                    foreach (string line in lines)
                    {
                        if (line.Contains(">> "))
                        {
                            output_file = output_file + line.Substring(3) + Environment.NewLine;
                        }
                        else
                        {
                            output_file = output_file + line + Environment.NewLine;
                        }
                    }
                }

                File.Delete(temp_translation);
                using (StreamWriter sw = File.CreateText(temp_translation)) // Create translator file
                {
                    sw.Write(output_file + Environment.NewLine);
                    sw.Close();
                }
            }
        }

        // Load a rule from the dictionary into the current rule state and move it to RECENT
        private void Load_Rule(string rule_name)
        {
            try
            {
                Rule_List = _Master_Rules_List[rule_name];
                Current_Rule_Name = rule_name;
                Update_Recent(rule_name);
            }
            catch
            {
                // Rule does not exist; should never happen
            }
        }

        // Storage bin for memory slots
        public void Reset_Memory()
        {
            IGNORE_CHECK = false;
            //MEMORY_BIN = new string[10000];
            for (int i = 0; i < 10000; i++)
            {
                if (!Do_Not_Touch_Indices.Contains(i))
                    MEMORY_BIN[i] = "0";
            }
            MEMORY_BIN[9999] = DateTime.Now.ToString();
            MEMORY_BIN[9998] = Path.GetFileName(original_file_loaded);
            MEMORY_BIN[9997] = original_file_loaded;
            STORED_COUNT = 0;
        }

        // Save rule functionality
        private void Update_Rules()
        {
            string Translator_Rule_File_Path = Directory.GetCurrentDirectory() + "\\translator_rules.txt";
            string line = "";

            // Move dictionary data into the file data
            _Master_Rules_List[Current_Rule_Name] = Rule_List; // update current rule to file list rule

            foreach (string rule_name in Rule_Order)

            //foreach (KeyValuePair<string, List<List<string>>> rule_files in _Master_Rules_List)
            {
                //Form1.cs //line = line + "[" + rule_files.Key + "]" + Environment.NewLine;
                line = line + "[" + rule_name + "]" + Environment.NewLine;
                //foreach (List<string> i in rule_files.Value)
                foreach (List<string> i in _Master_Rules_List[rule_name])
                {
                    int o = 0;
                    //Console.WriteLine("[" + rule_files.Key + "]");
                    foreach (string g in i)
                    {
                        if (o > 0)
                        {
                            line = line + "?" + g;
                        }
                        else
                        {
                            line = line + g;
                        }
                        o++;
                    }
                    line = line + Environment.NewLine;
                }
                line = line + Environment.NewLine;
            }
            try
            {
                File.Delete(Translator_Rule_File_Path);
                using (StreamWriter sw = File.CreateText(Translator_Rule_File_Path)) // Create translator file
                {
                    sw.Write(line + Environment.NewLine);
                    sw.Close();
                    
                }

            }
            catch
            {
                // Cannot overwrite rule
            }
            //File.AppendAllText(Translator_Rule_File_Path, line + "``");
        }

        // _Child index selection (mainly for edit save)
        public void Set_Rule_Selection(int index)
        {
            translation_rules.SetSelected(index, true);
        }

        // Add a new rule to the current rule
        public void Add_Rule(string ACTION, string SEARCH_VALUE, string DIMENSION, string VALUE, string OPTIONAL_VALUE, string COMMENT, int index = -1, bool auto_select = true)
        {
            if (index < 0) //no edit
            {
                List<string> testrule = new List<string>();
                testrule.Add(ACTION); // Action
                testrule.Add(SEARCH_VALUE); // Optional value (search value)
                testrule.Add(DIMENSION); // Dimension clause OR ARITHMETIC
                testrule.Add(VALUE); // Insert Index OR ARITHMETIC VALUE
                testrule.Add(OPTIONAL_VALUE);  // Optional Value
                testrule.Add(COMMENT);  // Comment
                Rule_List.Add(testrule);
                _Setup_Rule_List();
                if (auto_select && !(ACTION == "LOOP_CONDITION_START")) translation_rules.SetSelected(Rule_List.Count - 1 - FORMAT_RULES_COUNT, true);
            }
            else
            {
                List<string> testrule = new List<string>();
                testrule.Add(ACTION); // Action
                testrule.Add(SEARCH_VALUE); // Optional value (search value)
                testrule.Add(DIMENSION); // Dimension clause OR ARITHMETIC
                testrule.Add(VALUE); // Insert Index OR ARITHMETIC VALUE
                testrule.Add(OPTIONAL_VALUE);  // Optional Value
                testrule.Add(COMMENT);  // Comment
                Rule_List.Insert(index, testrule);
                _Setup_Rule_List();
                if (auto_select && !(ACTION == "LOOP_CONDITION_START")) translation_rules.SetSelected(index, true);
            }
        }

        // Delete selected rule
        public void delete_rule(int index)
        {
            if (translation_rules.Items.Count > 0)
            {
                try
                {
                    int selected_index = index;
                    //Console.WriteLine(selected_index);
                    Rule_List.RemoveAt(selected_index);
                    _Setup_Rule_List();
                    //Update_Rules();
                    if (index < 0)
                        if (translation_rules.Items.Count > 0)
                            translation_rules.SetSelected(0, true);
                }
                catch { }
            }
        }

        bool isG10Line = false;

        // Ignore the "NXX" value for all the G-Code -> N156 M09 becomes M09
        private string Get_Line(string g)
        {
            if (ignoreFormatting) return g;

            if (g.Contains("G10")) isG10Line = true;
            if (g.Contains("G11")) isG10Line = false;

            if (isG10Line && !g.Contains("I_L_N")) return "I_L_N" + g;

            string input = g.Contains("I_L_N") ? g.Substring(0, g.IndexOf("I_L_N")) + g.Substring(g.IndexOf("I_L_N") + 5) : g;
            if (input[0].ToString() != "N" || (input.Length == 1 && input[0].ToString() != "%")) return ("I_L_N" + input);
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].ToString() == " " || (i > 0 && !(Char.IsDigit(input[i]))))
                {
                    //return input;

                    return input.Substring((i > 0 && !(Char.IsDigit(input[i]))) ? i : i + 1, input.Length - i - ((i > 0 && !(Char.IsDigit(input[i]))) ? 0 : 1)) + (input.EndsWith(" ") ? " " : " ");
                }
            }
            input.Trim();
            return input;// +(input.EndsWith(" ") ? "" : " ");
        }

        // Execute the appropriate changes to the G-Code; the changes are aggregate so changes that happened can/may change again due to other rules
        private void Process_File(List<List<string>> TEMP_RULE_LIST = null)
        {
            List<List<string>> _temp = TEMP_RULE_LIST == null ? Rule_List : TEMP_RULE_LIST;

            int rule_number = 0;
            Change_Count = 0;
            foreach (List<string> rules in _temp) // For each rule in rule_list, iterate through the G_Code_List 
            {
                List<string> rule = new List<string>(rules);
                rule[2] = GET_TEXT_WITH_BIN(rules[2]);
                //Console.WriteLine(rule[0], rule[1], rule[2], rule[3], rule[4]);
                int i = 0;

                if (rule[0] == "LOOP_CONDITION_START")
                {
                    // If compare bin values for loop count
                    if (rule[3].Length > 0)
                    {
                        if (rule[3] == "Equal To")
                        {
                            while (!(Convert.ToInt32(MEMORY_BIN[Convert.ToInt32(rule[4])]) == Convert.ToInt32(rule[2]) - 1))
                            {
                                int gg = rule_number + 1;
                                int ggg = Loop_Info[rule[1]];
                                List<List<string>> _temp2 = Rule_List.GetRange(gg, ggg - gg);
                                Process_File(_temp2);
                            }
                        }
                        else if (rule[3] == "Greater Than")
                        {
                            int gggg = Convert.ToInt32(MEMORY_BIN[Convert.ToInt32(rule[4])]);
                            int ggggg = Convert.ToInt32(rule[2]);
                            Console.WriteLine(gggg + ggggg);
                            while (!(Convert.ToInt32(MEMORY_BIN[Convert.ToInt32(rule[4])]) > Convert.ToInt32(rule[2]) - 1))
                            {
                                int gg = rule_number + 1;
                                int ggg = Loop_Info[rule[1]];
                                List<List<string>> _temp2 = Rule_List.GetRange(gg, ggg - gg);
                                Process_File(_temp2);
                            }
                        }
                        else if (rule[3] == "Less Than")
                        {
                            while (!(Convert.ToInt32(MEMORY_BIN[Convert.ToInt32(rule[4])]) < Convert.ToInt32(rule[2]) - 1))
                            {
                                int gg = rule_number + 1;
                                int ggg = Loop_Info[rule[1]];
                                List<List<string>> _temp2 = Rule_List.GetRange(gg, ggg - gg);
                                Process_File(_temp2);
                            }
                        }
                    }
                    else
                    {
                        for (int iz = 0; iz < Convert.ToInt32(rule[2]) - 1; iz++)
                        {
                            int gg = rule_number + 1;
                            int ggg = Loop_Info[rule[1]];
                            List<List<string>> _temp2 = Rule_List.GetRange(gg, ggg - gg);
                            Process_File(_temp2);
                        }
                    }
                }

                bool line_action_check = false;
                List<string> temp_G_Code = G_Code_List;
                foreach (string G_Code_Line in temp_G_Code.ToList())
                {
                    if (!line_action_check)
                    {
                        bool parameters_valid = false;
                        string[] parameters = rule[1].Split(new string[] { "_" }, StringSplitOptions.None); // Check reference line
                        if (!(rule[0] == "DeleteAt" || rule[0] == "InsertAt" || rule[0] == "Switch" || rule[0] == "ReplaceAt" || rule[0] == "FORMAT_RULE" || rule[0] == "RunQuery"))// Do these only once
                        {
                            foreach (string parameter in parameters)
                            {
                                if (parameter.Contains("S~W~I"))
                                {
                                    if (G_Code_Line.Trim().StartsWith(parameter.Substring(5))) parameters_valid = true;
                                }
                                else
                                {
                                    if (G_Code_Line.Contains(parameter)) parameters_valid = true;
                                }
                            }
                        }
                        else
                        {
                            if (rule[1].Length > 0 && !(rule[1] == "LIST_OF_CONDITIONS"))
                            {
                                string[] global_condition = rule[1].Split(new string[] { "|" }, StringSplitOptions.None);
                                if (global_condition[0] == "Exactly" && COUNT_TOKEN_IN_FILE(temp_G_Code, global_condition[2]) == Convert.ToInt32(global_condition[1]))
                                {
                                    parameters_valid = true;
                                }
                                else if (global_condition[0] == "At least" && COUNT_TOKEN_IN_FILE(temp_G_Code, global_condition[2]) >= Convert.ToInt32(global_condition[1]))
                                {
                                    parameters_valid = true;
                                }
                                else if (global_condition[0] == "At most" && COUNT_TOKEN_IN_FILE(temp_G_Code, global_condition[2]) <= Convert.ToInt32(global_condition[1]))
                                {
                                    parameters_valid = true;
                                }
                            }
                            else
                            {
                                parameters_valid = true;
                            }
                        }

                        // if valid, Check conditions are satisifed (bin comparison)
                        if (parameters_valid && rule.Count > 5 && rule[5].Contains("IFCHK"))
                        {
                            parameters_valid = false;
                            int a = rule[5].IndexOf("))");
                            int b = rule[5].IndexOf("((") + 2;
                            string[] temp = rule[5].Substring(b, a - b).Split(new string[] { ":" }, StringSplitOptions.None);

                            if (temp[1] == "Greater Than")
                            {
                                if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) > Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                {
                                    parameters_valid = true;
                                }
                            }
                            else if (temp[1] == "Less Than")
                            {
                                if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) < Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                {
                                    parameters_valid = true;
                                }
                            }
                            else if (temp[1] == "Equal To")
                            {
                                if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) == Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                {
                                    parameters_valid = true;
                                }
                            }
                            else if (temp[1] == "Not Equal To")
                            {
                                //double value1 = Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]);
                                //double value2 = Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2]));
                                //Console.WriteLine(value1 + ", " + value2);
                                if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) != Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                {
                                    parameters_valid = true;
                                }
                            }
                        }



                        // If has group condition 
                        if (rule[5].Contains("[/GRP]") && rule[5].Contains("[GRP]") && parameters_valid)
                        {
                            parameters_valid = false;

                            int start_index = rule[5].IndexOf("[GRP]");
                            int end_index = rule[5].IndexOf("[/GRP]") + 6;
                            string[] temp2 = (rule[5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                            if (temp2.Length > 2)
                            {
                                string[] temp = temp2[2].Split(new string[] { "~" }, StringSplitOptions.None);
                                if (temp[1] == "Greater Than")
                                {
                                    if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) > Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                    {
                                         parameters_valid = true;
                                    }
                                }
                                else if (temp[1] == "Less Than")
                                {
                                    if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) < Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                    {
                                        parameters_valid = true;
                                    }
                                }
                                else if (temp[1] == "Equal To")
                                {
                                    if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) == Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                    {
                                        parameters_valid = true;
                                    }
                                }
                                else if (temp[1] == "Not Equal To")
                                {
                                    if (Convert.ToDouble(MEMORY_BIN[Convert.ToInt32(GET_TEXT_WITH_BIN(temp[0]))]) != Convert.ToDouble(GET_TEXT_WITH_BIN(temp[2])))
                                    {
                                        parameters_valid = true;
                                    }
                                }
                            }
                            else
                            {
                                parameters_valid = true;
                            }
                        }

                        if (parameters_valid)
                        {
                            string dimension = rule[2];
                            string value = rule[3];
                            string optional_value = rule[4];
                            string comment = rule[5];
                            Change_Line(i, G_Code_Line, GET_TEXT_WITH_BIN(value), rule[0], GET_TEXT_WITH_BIN(dimension), GET_TEXT_WITH_BIN((optional_value)), comment);
                            if (rule[0] == "RunQuery" || rule[0] == "GET_INPUT" || rule[0] == "DeleteAt" || rule[0] == "InsertAt" || rule[0] == "Switch" || rule[0] == "ReplaceAt" || rule[0].Contains("LastIndexOfReplace") || rule[0].Contains("IF_THEN") || (rule[0] != "COMPARE_TO_REF_LINE" && rule[5].Contains("FIRST_INSTANCE_ONLY")) || rule[0].Contains("RANGE_FUNCTION")) // Do these only once
                                line_action_check = true;
                            if (rule[0].Contains("Delete"))
                            {
                                i--;
                            }
                            if (rule[0].Contains("Insert"))
                            {
                                i++;
                            }
                            if (!(refresh_value == 0))
                            {
                                i = i + refresh_value;
                                refresh_value = 0;
                            }
                        }
                        i++;
                    }
                }
                rule_number++;
            }
        }

        // Count number of tokens in file
        public int COUNT_TOKEN_IN_FILE(List<string> ref_list, string token)
        {
            int count = 0;
            foreach (string line in ref_list)
            {
                if (line.Contains(token)) count++;
            }
            return count;
        }

        // Return rule; external access
        public List<string> GET_RULE(int index)
        {
            return Rule_List[index];
        }

        // Count tokens in string
        public int COUNT_TOKEN(string text, string token)
        {
            int count = 0;
            foreach (char c in text)
                if (c.ToString() == token) count++;
            return count;
        }

        // Get string, check if it has any stored values. If so, substitute.
        private string GET_TEXT_WITH_BIN(string text)
        {

            if (text.Length > 2 && COUNT_TOKEN(text, "%") % 2 == 0 && text.Contains("%"))
            {
                string front = "";
                string middle = "";
                string end = "";
                string temp = "";
                bool parsing = false;
                bool done_middle = false;

                foreach (char c in text)
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
                            middle = MEMORY_BIN[Convert.ToInt32(temp.Substring(0, temp.Length - 1))];
                            done_middle = true;
                        }
                        catch
                        {
                            return text;
                        }
                    }
                    else if (!parsing && middle.Length < 1 && !done_middle)
                    {
                        temp = "";
                        front = front + c.ToString();
                    }
                }

                // Recursively parse stored bins %%
                return GET_TEXT_WITH_BIN(front + middle + end);
            }
            else
            {
                return text;
            }
        }

        // Return the bin number integer parsed from %n%;
        public int GET_BIN_NUMBER(string bin_value)
        {
            if (bin_value.StartsWith("%") && bin_value.EndsWith("%"))
            {
                try
                {
                    return Convert.ToInt32(bin_value.Substring(1, bin_value.Length - 2));
                }
                catch
                {
                    return -1;
                }
            }
            return -1;
        }

        // Delete rule with the name rule_name globally
        public void _GLOBAL_DELETE_FORMAT_RULE(string rule_name)
        {
            if (this.Rule_List.Count > 0)
            {
                for (int i = Rule_List.Count - 1; i >= 0; i--)
                {
                    if (this.Rule_List[i][0] == "FORMAT_RULE" && this.Rule_List[i][1] == rule_name)
                        this.Rule_List.RemoveAt(i);
                }
            }
        }

        // Add rule globally
        public void _GLOBAL_ADD_FORMAT_RULE(List<string> rule)
        {
            Rule_List.Add(rule);
        }

        public List<List<string>> Get_Rule_List_Public()
        {
            return Rule_List;
        }



        #endregion

        //===============================================================================================================================================================

        #region BUTTON FUNCTIONS & INTERFACING FUNCTIONS

        // Translate using current rule set; this is hidden due to update on preview which automatically translates
        private void button3_Click(object sender, EventArgs e)
        {
            Change_Index = new List<int>();
            Cursor.Current = Cursors.WaitCursor;
            Store_G_Code(false);
            if (file_loaded)
            {
                Process_File(); // Analyze file based on rule
                //testrobin2
                number_of_changes.Text = "Number of changes via translation: " + Change_Count.ToString();
                number_of_changes.Visible = true;
                string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
                File.Delete(temp_translation);
                if (File.Exists(temp_translation))
                {
                    // If file exists, delete it.
                    //button3.PerformClick();
                }
                else
                {
                    Translation_Complete = true;
                    /*
                    using (StreamWriter sw = File.CreateText(temp_translation))
                    {
                        string line = Process_File_Output();
                        sw.Write(line);
                        sw.Close();
                    }*/
                    Process_File_Output();
                    File.WriteAllLines(temp_translation, G_Code_List);
                }

                if (DO_NOT_TRANSLATE)
                {
                    number_of_changes.Text = "Number of changes via translation: 0";
                    File.Copy(file_path_loaded, temp_translation, true);
                }
            }
            else
            {
                MessageBox.Show("No file loaded");
            }
            Cursor.Current = Cursors.Default;
        }

        // Return the appropriate amount of zeroes leading
        private string Process_Leading_Zeroes(int trailing_digit, int leading_zeroes)
        {
            string return_string = "";
            try
            {
                for (int i = 0; i < leading_zeroes - trailing_digit.ToString().Length; i++)
                {
                    return_string = return_string + "0";
                }
            }
            catch { }
            return return_string;
        }

        // Open new translator dialog 
        private void button11_Click(object sender, EventArgs e)
        {
            translate_line trans = new translate_line(this, _Master_Rules_List);
            trans.ShowDialog();
        }

        // Save translation button
        private void button4_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
            string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
            if (Translation_Complete)
            {
                string file_path = "";
                SaveFileDialog file = new SaveFileDialog();
                file.FileName = file_name_loaded;
                file.Title = "Translate File";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    file_path = file.FileName;
                    File.Copy(temp_translation, file_path, true);
                }
            }

        }

        // Preview button
        public void button5_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Reset_Memory();

            button3.PerformClick();
            Change_Track(changes.Checked);
            if (Translation_Complete)// && !ADMIN_MODE)
            {
                string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
                if (Preview_Toggle) { Process.Start(@temp_translation); }
                Preview_Toggle = true; 
            }
            //ADMIN_COUNT++;
            //admin_text_box.Text = ADMIN_COUNT.ToString();
        }

        // Add Rule button
        private void button7_Click(object sender, EventArgs e)
        {
            Add_Rule new_rule = new Add_Rule(this, this.Location);
            new_rule.Show();
        }

        // Delete Algorithm button
        private void deletebutton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + (string)translation_list.SelectedItem + " rule?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Delete_Translator_File((string)translation_list.SelectedItem);
            }
            else if (dialogResult == DialogResult.No)
            {
            }
            if (translation_list.Items.Count == 0)
            {
                translation_rules.Items.Clear();
            }
        }

        // Delete Rule button
        private void button6_Click(object sender, EventArgs e)
        {
            int sel_index = 0;
            if (translation_rules.Items.Count > 0)
            {
                try
                {
                    for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                    {
                        if (translation_rules.GetSelected(i) == true)
                        {
                            if (Rule_List[i][0] == "LOOP_CONDITION _START")
                            {
                                int end_index = Loop_Info[Rule_List[i][1]];
                                //Rule_List.RemoveAt(end_index); // Remove END LOOP first
                                Rule_List.RemoveAt(i);
                                sel_index = i;  
                            }
                            else if (Rule_List[i][0] == "LOOP_CONDITION_END")
                            {
                                Rule_List.RemoveAt(i);
                                sel_index = i;
                            }
                            else
                            {
                                Rule_List.RemoveAt(i);
                                sel_index = i;
                                //Update_Rules();
                            }
                        }
                    }
                    _Setup_Rule_List();
                }
                catch { }
            }

            if (translation_rules.Items.Count > 0)
            {
                try
                {
                    translation_rules.SetSelected(sel_index, true);
                }
                catch
                {   // Move to end of list
                    translation_rules.SetSelected(sel_index - 1, true);
                }
            }
        }

        // Move rule up
        private void button8_Click(object sender, EventArgs e)
        {
            List<int> selected_indicies = new List<int>();

            int translation_count = translation_rules.Items.Count;
            try
            {
                bool LOOP_START_ERROR = false;
                for (int selected_index = 0; selected_index < translation_count; selected_index++)
                {
                    if (translation_rules.GetSelected(selected_index) == true)
                    {
                        if (Rule_List[selected_index][0].Contains("LOOP_CONDITION"))
                        {
                            int reference_index = 9999;
                            foreach (KeyValuePair<string, int> g in Loop_Info)
                            {
                                // get closest "end loop" index to selected index
                                if (!(g.Key == Rule_List[selected_index][1]) && selected_index - g.Value < reference_index && (selected_index - g.Value) > 0)
                                {
                                    reference_index = selected_index - g.Value;
                                    if (reference_index <= 1)
                                    {
                                        LOOP_START_ERROR = true;
                                    }
                                }
                            }
                            if (reference_index > 1)
                            {
                                if (!LOOP_START_ERROR && !(Rule_List[selected_index - 1][0].Contains("LOOP_CONDITION_START")) && Rule_List[selected_index][0].Contains("LOOP_CONDITION"))
                                {
                                    selected_indicies.Add(selected_index);
                                    List<string> temp_list = Rule_List[selected_index - 1];
                                    Rule_List[selected_index - 1] = Rule_List[selected_index];
                                    Rule_List[selected_index] = temp_list;
                                }
                            }
                            else
                            {
                                //MessageBox.Show("Cannot move loop up because it overlaps with another loop");
                            }
                        }
                        else
                        {
                            selected_indicies.Add(selected_index);
                            List<string> temp_list = Rule_List[selected_index - 1];
                            Rule_List[selected_index - 1] = Rule_List[selected_index];
                            Rule_List[selected_index] = temp_list;
                        }
                    }
                }

                _Setup_Rule_List();

                foreach (int index in selected_indicies)
                    translation_rules.SetSelected(index - 1, true);

            }
            catch { }
        }

        // move rule down
        private void button9_Click(object sender, EventArgs e)
        {
            List<int> selected_indicies = new List<int>();

            int translation_count = translation_rules.Items.Count;
            try
            {
                for (int selected_index = translation_count - 1; selected_index >= 0; selected_index--)
                {
                    if (translation_rules.GetSelected(translation_count - 1) == true) goto Finish;
                    if (translation_rules.GetSelected(selected_index) == true)
                    {
                        if (Rule_List[selected_index + 1][0].Contains("LOOP_CONDITION_START") && Rule_List[selected_index][0].Contains("LOOP_CONDITION"))
                        {
                            //MessageBox.Show("Cannot move loop up because it overlaps with another loop");
                        }
                        else
                        {
                            selected_indicies.Add(selected_index);
                            List<string> temp_list = Rule_List[selected_index + 1];
                            Rule_List[selected_index + 1] = Rule_List[selected_index];
                            Rule_List[selected_index] = temp_list;
                        }
                    }
                }

                _Setup_Rule_List();

                foreach (int index in selected_indicies)
                {
                    translation_rules.SetSelected(index + 1, true);
                }

            }
            catch
            {
                foreach (int index in selected_indicies)
                {
                    translation_rules.SetSelected(index, true);
                }
            }

        Finish: ;
        }

        // Save changes to rules
        private void button10_Click(object sender, EventArgs e)
        {
            Update_Rules();
        }

        // Rule edit
        private void translation_rules_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int selected_index = this.translation_rules.IndexFromPoint(e.Location);
                List<int> SELECTED_INDICIES = new List<int>();
                bool VALID_EDIT = true;
                for (int i = 0; i < translation_rules.Items.Count; i++)
                {
                    if (translation_rules.GetSelected(i) == true)
                        SELECTED_INDICIES.Add(i);
                }
                List<string> temp_list = Rule_List[selected_index];
                if (temp_list[0].Contains("LOOP_CONDITION"))
                {
                    if (temp_list[0].Contains("START"))
                    {
                        Add_Loop g = new Add_Loop(this, temp_list, selected_index);
                        g.ShowDialog();
                    }
                    else
                    {
                        // Do nothing - do not edit any end condition
                    }
                }
                else if (temp_list[0].Contains("IF_THEN"))
                {
                    If_Then g = new If_Then(this, temp_list, selected_index);
                    g.ShowDialog();
                }
                else if (SELECTED_INDICIES.Count == 1) // If only one selected
                {
                    try
                    {
                        if (selected_index != System.Windows.Forms.ListBox.NoMatches)
                        {
                            temp_list = Rule_List[selected_index];
                            if (temp_list[0] == "RunQuery")
                            {
                                if (true)
                                {
                                    Add_Rule g = new Add_Rule(this, this.Location, true, selected_index, temp_list[0], temp_list[1], temp_list[2],
                                                        temp_list[3], temp_list[4], temp_list[5]
                                                        );
                                    g.ShowDialog();
                                }
                                else
                                {
                                    //MessageBox.Show("Only an administrator can review and edit queries");
                                }
                            }
                            else
                            {
                                Add_Rule g = new Add_Rule(this, this.Location, true, selected_index, temp_list[0], temp_list[1], temp_list[2],
                                                            temp_list[3], temp_list[4], temp_list[5]
                                                        );
                                g.ShowDialog();
                            }
                        }
                    }
                    catch { }
                }
                else if (SELECTED_INDICIES.Count > 1)// IF MULTISELECT
                {
                    try
                    {

                        foreach (int index in SELECTED_INDICIES)
                        {
                            string g1 = Rule_List[index][4].Split(new string[] { "|" }, StringSplitOptions.None)[0];
                            string g2 = Rule_List[selected_index][4].Split(new string[] { "|" }, StringSplitOptions.None)[0];
                            string g3 = ""; string g4 = "";
                            if (!(g1 == "Delete" && g2 == "Delete"))
                            {
                                g3 = Rule_List[index][4].Split(new string[] { "|" }, StringSplitOptions.None)[2];
                                g4 = Rule_List[selected_index][4].Split(new string[] { "|" }, StringSplitOptions.None)[2];
                            }


                            if
                                (!(
                                Rule_List[index][0] == Rule_List[selected_index][0] &&
                                Rule_List[index][1] == Rule_List[selected_index][1] &&
                                Rule_List[index][5] == Rule_List[selected_index][5] &&
                                Rule_List[index][3].Split(new string[] { "|" }, StringSplitOptions.None)[1] == Rule_List[selected_index][3].Split(new string[] { "|" }, StringSplitOptions.None)[1] &&
                                //Rule_List[index][0] == Rule_List[selected_index][0] &&
                                (g3 == g4)
                                ))
                            { VALID_EDIT = false; }
                        }

                        if (VALID_EDIT)
                        {
                            temp_list = Rule_List[selected_index];

                            Add_Rule g = new Add_Rule(this, this.Location, true, selected_index, temp_list[0], temp_list[1], temp_list[2],
                                                        temp_list[3], temp_list[4], temp_list[5], SELECTED_INDICIES
                                                 );
                            g.Show();
                        }
                        else
                        {
                            MessageBox.Show("Please make sure the rules you have selected are the same");
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Invalid selection of rules");
                    }
                }
            }
            catch
            {
            }
        }

        // Help button
        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Double-Click the rule to show the description");
        }

        // Overwrite original file
        private void button13_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
            string temp_translation = Directory.GetCurrentDirectory() + "\\temp_translation.txt";
            if (Translation_Complete)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to overwrite the original file " + file_loaded_text.Text + "?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("File overwritten");
                    File.Copy(temp_translation, file_path_loaded, true);
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }

        // View original file
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (file_loaded)
                {
                    string temp_translation = file_path_loaded;
                    Process.Start(@temp_translation);
                }
            }
            catch
            {
                MessageBox.Show("Error with file. Unable to open directly");
            }
        }


        // Edit rule
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_index = (int)translation_rules.SelectedIndex;
                if (selected_index >= 0)
                {
                    if (Rule_List[selected_index][5].Length > 0) // If has description
                    {
                        string g = Rule_List[selected_index][5].ToString();
                        g = g.Contains("FIRST_INSTANCE_ONLY") ? g.Substring(19) : g;
                        g = g.Contains("I_L_N") ? g.Substring(0, g.IndexOf("I_L_N")) + g.Substring(g.IndexOf("I_L_N") + 5) : g;
                        g = g.Contains("IFCHK") ? g.Substring(g.IndexOf("))") + 2) : g;
                        MessageBox.Show(g);
                    }
                    else
                    {
                        MessageBox.Show("No description available");
                    }
                }
            }
            catch { }//MessageBox.Show(ze.ToString()); }
        }

        // Duplicate button
        private void button12_Click_1(object sender, EventArgs e)
        {
            List<int> selection_index = new List<int>();
            int total_selected = 0;
            int translation_count = translation_rules.Items.Count;
            bool Has_Loop = false;
            // Scan for loop conditions
            try
            {
                for (int selected_index = 0; selected_index < translation_count; selected_index++)
                {
                    if (translation_rules.GetSelected(selected_index) == true)
                    {
                        if (Rule_List[selected_index][0].Contains("LOOP_CONDITION_")) Has_Loop = true;

                    }
                }
            }
            catch { }

            if (!Has_Loop)
            {
                try
                {
                    for (int selected_index = 0; selected_index < translation_count; selected_index++)
                    {
                        if (translation_rules.GetSelected(selected_index) == true)
                        {
                            selection_index.Add(selected_index);
                            total_selected++;
                        }
                    }
                }
                catch { }//MessageBox.Show(ze.ToString()); }

                foreach (int selected_index in selection_index)
                {
                    Add_Rule(Rule_List[selected_index][0], Rule_List[selected_index][1], Rule_List[selected_index][2], Rule_List[selected_index][3], Rule_List[selected_index][4], Rule_List[selected_index][5], -1, false);
                }
                /*
                try
                {
                    int selected_index = (int)translation_rules.SelectedIndex;
                    Add_Rule(Rule_List[selected_index][0], Rule_List[selected_index][1], Rule_List[selected_index][2], Rule_List[selected_index][3], Rule_List[selected_index][4], Rule_List[selected_index][5], selected_index+1);
                }
                */
                for (int i = translation_count; i < translation_count + total_selected; i++)
                    //translation_rules.SetSelected(translation_rules.Items.Count - total_selected, true);
                    translation_rules.SetSelected(i, true);
            }
            else
            {
                MessageBox.Show("Cannot duplicate loop condition");
            }
        }

        // Reload button
        private void button14_Click(object sender, EventArgs e)
        {
            translation_list.Items.Clear();
            Reset_Parameters();
            Get_Rules(); // Get all rules
            _Setup_Translation_Rules();
            translation_list.SelectedIndex = 0;
        }

        // Copy selected algorithm
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                translate_line trans = new translate_line(this, _Master_Rules_List, _Master_Rules_List[(string)translation_list.SelectedItem], (string)translation_list.SelectedItem);
                trans.ShowDialog();
            }
            catch
            {
            }
        }

        // Open translation rules file 
        private void button16_Click(object sender, EventArgs e)
        {
            string Translator_Rule_File_Path = Directory.GetCurrentDirectory() + "\\translator_rules.txt";
            Process.Start(Translator_Rule_File_Path);
        }

        // Edit output format
        private void button17_Click(object sender, EventArgs e)
        {
            File_Options g = new File_Options(this, Rule_List);
            g.ShowDialog();
        }

        // Compare translation with original
        private void button18_Click(object sender, EventArgs e)
        {
            if (file_loaded)
            {
                Reset_Memory();
                Original_G_Code_List = new List<string>();
                var text = File.ReadAllText(original_file_loaded); //load original file code
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string temp = Get_Line(line).Contains("I_L_N") ? Get_Line(line).Substring(0, Get_Line(line).IndexOf("I_L_N")) + Get_Line(line).Substring(Get_Line(line).IndexOf("I_L_N") + 5) : Get_Line(line);
                        Original_G_Code_List.Add(temp);
                    }
                }

                button3.PerformClick();
                CompareText comp = new CompareText();
                comp.Compare_Files(Original_G_Code_List, G_Code_List);
                //comp.Delete_Comparison_File();
            }
            else
            {
                MessageBox.Show("No file loaded");
            }
        }

        // Loop button
        private void button20_Click(object sender, EventArgs e)
        {
            Add_Loop AL = new Add_Loop(this);
            AL.ShowDialog();
        }

        // If/then button
        private void button21_Click(object sender, EventArgs e)
        {
            If_Then AL = new If_Then(this);
            AL.ShowDialog();
        }

        // Show bin view button
        private void show_bin_rules_Click(object sender, EventArgs e)
        {
            if (!Bin_View_Open)
            {
                Bin_View_Open = true;
                Bin_View BV = new Bin_View(this, Rule_List, Current_Rule_Name, this.Location);
                BV.Show();
            }
        }
        #endregion

        //===============================================================================================================================================================

        //===============================================================================================================================================================

        #region JUNK FUNCTIONS (DO NOT DELETE)

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }


        #endregion

        private void translation_rules_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            translation_rules.SelectedIndexChanged -= new System.EventHandler(this.translation_rules_SelectedIndexChanged_1);

            
            {
            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                if (translation_rules.GetSelected(i) == true)
                {
                    if (Rule_List[i][0] == "LOOP_CONDITION_START")
                    {
                        translation_rules.SetSelected(Loop_Info[Rule_List[i][1]], true);
                    }
                    if (view_edit_group_button.Text.Contains("Hide") && Rule_List[i][5].Contains("[GRP]") && Rule_List[i][5].Contains("[/GRP]"))
                    {
                        int start_index = Rule_List[i][5].IndexOf("[GRP]");
                        int end_index = Rule_List[i][5].IndexOf("[/GRP]") + 6;
                        string[] temp = (Rule_List[i][5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                        string key = temp[0].Substring(3);

                        int index = 0;
                        foreach (string key6 in Group_Dictionary_Order)
                        {
                            dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                            if (Group_Dictionary_Condition.ContainsKey(key6)) // highlight red
                            {
                                dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#F78181") : System.Drawing.ColorTranslator.FromHtml("#FA5858"));
                            }

                            dataGridView1.Rows[index].Cells[1].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                            index++;
                        }
                        index = 0;
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            dataGridView1[0, j].Value = false;
                        }
                        foreach (string g in Group_Dictionary_Order)
                        {
                            if (g == key)
                            {
                                dataGridView1[0, index].Value = true;
                                desc_box.Text = dataGridView1[1, index].Value.ToString();
                                dataGridView1.Rows[index].Cells[0].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F315");
                                dataGridView1.Rows[index].Cells[1].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F315");
                                if (Group_Dictionary_Condition.ContainsKey(key)) // highlight red
                                {
                                    dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#F78181") : System.Drawing.ColorTranslator.FromHtml("#FA5858"));
                                }
                            }
                            index ++;
                        }
                    }
                }
            }

            translation_rules.SelectedIndexChanged += new System.EventHandler(this.translation_rules_SelectedIndexChanged_1);
        }

        // Hidden test button
        private void button19_Click(object sender, EventArgs e)
        {
            if (!ADMIN_MODE)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter administrative password", "Administrative Configuration Mode", "Password", -1, -1);
                if (enc_obj.Encrypt(input) == "75::")
                {
                    ADMIN_MODE = true;
                    admin_text_box.Visible = true;
                    admin_text_box.Text = ADMIN_COUNT.ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Password");
                }
            }
            else
            {
                ADMIN_MODE = false;
                admin_text_box.Visible = false;
                admin_text_box.Text = ADMIN_COUNT.ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _Setup_Rule_List();
        }


        Size expand_size = new Size(810, 783);
        Size collapsed_size = new Size(505, 783);

        private void Translator_Load(object sender, EventArgs e)
        {
            this.Size = collapsed_size;
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.MaximumSize = new Size(505, 1783);
            this.MinimumSize = new Size(505, 450);

            // Disable sorting mode in group grid
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        // Scrollbar draw
        // Handle the MeasureItem event for an owner-drawn ListBox.
        private void ListBox1_MeasureItem(object sender,
            MeasureItemEventArgs e)
        {

            // Cast the sender object back to ListBox type.
            ListBox theListBox = (ListBox)sender;

            // Get the string contained in each item.
            string itemString = (string)theListBox.Items[e.Index];

            // Split the string at the " . "  character.
            string[] resultStrings = itemString.Split('.');

            // If the string contains more than one period, increase the 
            // height by ten pixels; otherwise, increase the height by 
            // five pixels.
            if (resultStrings.Length > 2)
            {
                e.ItemHeight += 10;
            }
            else
            {
                e.ItemHeight += 5;
            }

        }


        //global brushes with ordinary/selected colors
        private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.White);
        private SolidBrush reportsForegroundBrush = new SolidBrush(Color.Black);
        private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
        private SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.White);
        private SolidBrush reportsBackgroundBrush2 = new SolidBrush(Color.Gray);
        private SolidBrush reportsBackgroundBrush3 = new SolidBrush(Color.DarkGray);
        string set_key = "";
        //custom method to draw the items, don't forget to set DrawMode of the ListBox to OwnerDrawFixed
        private void translation_rules_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < translation_rules.Items.Count)
            {
                
                SolidBrush backgroundBrush;

                List<string> rule = Rule_List[index];
                if (rule[5].Contains("[GRP]") && rule[5].Contains("[/GRP]") && group_color_checkbox.Checked)
                {

                    //reportsBackgroundBrush2 = new SolidBrush(Grouping_Color[key]);

                    string text = translation_rules.Items[index].ToString();
                    Graphics g = e.Graphics;

                    //background:
                    if (selected)
                    {
                        backgroundBrush = reportsBackgroundBrushSelected;
                        g.FillRectangle(backgroundBrush, e.Bounds);
                    }
                    //else if ((index % 2) == 0)
                    //backgroundBrush = reportsBackgroundBrush1;
                    else
                    {
                        int start_index = rule[5].IndexOf("[GRP]");
                        int end_index = rule[5].IndexOf("[/GRP]") + 6;
                        string[] temp = (rule[5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                        string key = temp[0].Substring(3);
                        set_key = key;
                        //backgroundBrush = 
                        g.FillRectangle(new SolidBrush((new GetGroupColor(Grouping_Color, key) ).randomBrush), e.Bounds);
                        //g.FillRectangle(reportsBackgroundBrush1, e.Bounds);
                    }

                    //text:
                    SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                    g.DrawString(text, e.Font, foregroundBrush, translation_rules.GetItemRectangle(index).Location);
                }
                else // Not grouping paint gray
                {
                    string text = translation_rules.Items[index].ToString();
                    Graphics g = e.Graphics;

                    //background:
                    if (selected)
                        backgroundBrush = reportsBackgroundBrushSelected;
                    else
                        backgroundBrush = reportsBackgroundBrush3;
                    g.FillRectangle(backgroundBrush, e.Bounds);

                    //text:
                    SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                    g.DrawString(text, e.Font, foregroundBrush, translation_rules.GetItemRectangle(index).Location);
                }
            }
            //reportsBackgroundBrush1 = new SolidBrush((new GetGroupColor(Grouping_Color, set_key)).randomBrush);
            e.DrawFocusRectangle();
        }

        private void view_edit_group_button_Click(object sender, EventArgs e)
        {
            if (view_edit_group_button.Text.Contains("Hide"))
            {
                this.MaximumSize = new Size(505, 1783);
                this.MinimumSize = new Size(505, 450);
                this.Width += 305;
                view_edit_group_button.Text = "View/Edit Rule Groups";
                button20.Enabled = true;
                button21.Enabled = true;
                button12.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button7.Enabled = true;
                checkBox1.Checked = false;
            }
            else // show group
            {
                desc_box.Text = "";
                this.MaximumSize = new Size(810, 1783);
                this.MinimumSize = new Size(810, 450);
                translation_rules.ClearSelected();
                button20.Enabled = false;
                button21.Enabled = false;
                button12.Enabled = false;
                button6.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button7.Enabled = false;
                this.Width -= 305;
                //this.Size = expand_size;
                view_edit_group_button.Text = "Hide Rule Groups";

                Populate_Group_List();
            }
        }

        private void Populate_Group_List()
        {
            // Populate the group_dictionary_index
            dataGridView1.Rows.Clear();
            Group_Dictionary = new Dictionary<string, List<string>>();
            Group_Dictionary_Order = new List<string>();

            int index = 0;
            foreach (List<string> rule in Rule_List)
            {
                if (rule[5].Contains("[/GRP]"))
                {
                    int start_index = rule[5].IndexOf("[GRP]");
                    int end_index = rule[5].IndexOf("[/GRP]") + 6;
                    string[] temp = (rule[5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                    string key = temp[0].Substring(3);
                    if (Group_Dictionary.ContainsKey(key))
                    {
                        Group_Dictionary[key].Add(index.ToString());
                        if (temp.Count() > 2)
                            Group_Dictionary_Condition[key] = temp[2];
                    }
                    else
                    {
                        Group_Dictionary.Add(key, new List<string>() { temp[1], index.ToString() });
                        Group_Dictionary_Order.Add(key);
                    }
                }
                index++;  
            }

            index = 0;
            foreach (string key in Group_Dictionary_Order)
            {
                dataGridView1.Rows.Add(false, Group_Dictionary[key][0].Replace("``", Environment.NewLine));
                dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                if (Group_Dictionary_Condition.ContainsKey(key)) // highlight red
                    dataGridView1.Rows[index].Cells[0].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#F78181") : System.Drawing.ColorTranslator.FromHtml("#FA5858"));
                
                dataGridView1.Rows[index].Cells[1].Style.BackColor = (index % 2 == 0 ? System.Drawing.ColorTranslator.FromHtml("#A2D7FF") : System.Drawing.ColorTranslator.FromHtml("#D0EBFF"));
                index++;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1[0, i].Value = false;
                }
                dataGridView1[0, e.RowIndex].Value = true;
                desc_box.Text = Group_Dictionary[Group_Dictionary_Order[e.RowIndex]][0].Replace("``", Environment.NewLine);

                // Set group condition
                if (Group_Dictionary_Condition.ContainsKey(Group_Dictionary_Order[e.RowIndex]))
                {
                    Group_Condition_String = Group_Dictionary_Condition[(Group_Dictionary_Order[e.RowIndex])];
                    button27.Text = "Edit Group Condition";
                }
                else
                {
                    Group_Condition_String = "";
                    button27.Text = "Add Group Condition";
                }

                translation_rules.ClearSelected();
                for (int i = 1; i < Group_Dictionary[Group_Dictionary_Order[e.RowIndex]].Count(); i++)
                {
                    translation_rules.SetSelected(Convert.ToInt32(Group_Dictionary[Group_Dictionary_Order[e.RowIndex]][i]), true);
                }
            }
        }

        // Remove highlighting
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Color g = Get_Random_Light_Color();
            if (desc_box.Text.Length > 0)
            {
                Random randGen = new Random();
                string rand_string = randGen.Next(100000000, 999999999).ToString();
                bool has_Group = false;
                int rule_selected_count = 0;
                if (translation_rules.Items.Count > 0)
                {
                    try
                    {
                        for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                        {
                            if (translation_rules.GetSelected(i) == true)
                            {
                                if (Rule_List[i][5].Contains("[GRP]")) has_Group = true;
                                rule_selected_count++;
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (!has_Group && rule_selected_count > 0)
                    {
                        try
                        {
                            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                            {
                                if (translation_rules.GetSelected(i) == true)
                                {
                                    Grouping_Color.Add(rand_string, g);
                                    Rule_List[i][5] = "[GRP]ID=" + rand_string + "||" + desc_box.Text.Replace(Environment.NewLine, "``") + "[/GRP]" + Rule_List[i][5];
                                }
                            }
                        }
                        catch
                        {
                        }
                        Populate_Group_List();
                    }
                    else if (rule_selected_count == 0)
                    {
                        MessageBox.Show("You must select rule(s) to assign to this group");
                    }
                    else
                    {
                        MessageBox.Show("One or more rules selected are already in a group. Please ensure that the selected rules are not already assigned to a group");
                    }
                }
                desc_box.Text = "";
            }
            else
            {
                MessageBox.Show("Missing group description");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                bool has_Group = false;
                if (translation_rules.Items.Count > 0)
                {
                    try
                    {
                        for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                        {
                            if (translation_rules.GetSelected(i) == true)
                            {
                                if (Rule_List[i][5].Contains("[GRP]")) has_Group = true;
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (!has_Group)
                    {
                        int ref_index = 0;
                        try
                        {
                            for (int ji = 0; ji < dataGridView1.Rows.Count; ji++)
                            {
                                if (Convert.ToBoolean(dataGridView1[0, ji].Value))
                                {
                                    ref_index = ji;
                                }
                            }
                            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                            {
                                if (translation_rules.GetSelected(i) == true)
                                {

                                    Rule_List[i][5] = "[GRP]ID=" + Group_Dictionary_Order[ref_index] + "||" + Group_Dictionary[Group_Dictionary_Order[ref_index]][0].Replace(Environment.NewLine, "``") + "[/GRP]" + Rule_List[i][5];
                                }
                            }
                        }
                        catch
                        {
                        }
                        Populate_Group_List();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1[0, i].Value = false;
                        }
                        dataGridView1[0, ref_index].Value = true;

                        translation_rules.ClearSelected();
                        for (int i = 1; i < Group_Dictionary[Group_Dictionary_Order[ref_index]].Count(); i++)
                        {
                            translation_rules.SetSelected(Convert.ToInt32(Group_Dictionary[Group_Dictionary_Order[ref_index]][i]), true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("One or more rules selected are already in a group. Please ensure that the selected rules are not already assigned to a group");
                    }
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                bool has_Group = true;
                if (translation_rules.Items.Count > 0)
                {
                    try
                    {
                        for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                        {
                            if (translation_rules.GetSelected(i) == true)
                            {
                                if (!Rule_List[i][5].Contains("[GRP]") && !Rule_List[i][0].Contains("LOOP_CONDITION")) has_Group = false;
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (has_Group)
                    {
                        int ref_index = 0;
                        int original_row_count = dataGridView1.Rows.Count;
                        try
                        {
                            for (int ji = 0; ji < dataGridView1.Rows.Count; ji++)
                            {
                                if (Convert.ToBoolean(dataGridView1[0, ji].Value))
                                {
                                    ref_index = ji;
                                }
                            }
                            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                            {
                                if (translation_rules.GetSelected(i) == true)
                                {
                                    int reference_index = i;
                                    int start_index = Rule_List[reference_index][5].IndexOf("[GRP]");
                                    int end_index = Rule_List[reference_index][5].IndexOf("[/GRP]") + 6;
                                    string ref_string = Rule_List[reference_index][5].Substring(0, start_index) + Rule_List[reference_index][5].Substring(end_index);
                                    if (end_index == ref_string.Length) ref_string = "";
                                    Rule_List[reference_index][5] = ref_string;
                                }
                            }
                        }
                        catch
                        {
                        }
                        Populate_Group_List();
                        translation_rules.ClearSelected();
                        if (dataGridView1.Rows.Count > 0)
                        {
                            if (original_row_count == dataGridView1.Rows.Count)
                            {
                                dataGridView1[0, ref_index].Value = true;
                                for (int i = 1; i < Group_Dictionary[Group_Dictionary_Order[ref_index]].Count(); i++)
                                {
                                    translation_rules.SetSelected(Convert.ToInt32(Group_Dictionary[Group_Dictionary_Order[ref_index]][i]), true);
                                }
                            }
                            else
                            {
                            }
                        }
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1[0, i].Value = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("One or more rules selected are not in a group. Please ensure that the selected rules are assigned to a group");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Rename Group Description
        private void button25_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                bool has_Group = true;
                if (desc_box.Text.Length > 0)
                {
                    if (translation_rules.Items.Count > 0)
                    {
                        try
                        {
                            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                            {
                                if (translation_rules.GetSelected(i) == true)
                                {
                                    if (!Rule_List[i][5].Contains("[GRP]")) has_Group = false;
                                }
                            }
                        }
                        catch
                        {
                        }
                        if (has_Group)
                        {
                            int ref_index = -1;
                            try
                            {
                                for (int ji = 0; ji < dataGridView1.Rows.Count; ji++)
                                {
                                    if (Convert.ToBoolean(dataGridView1[0, ji].Value))
                                    {
                                        ref_index = ji;
                                    }
                                }
                                if (ref_index >= 0)
                                {
                                    for (int i = 1; i < Group_Dictionary[Group_Dictionary_Order[ref_index]].Count(); i++)
                                    {
                                        int reference_index = Convert.ToInt32(Group_Dictionary[Group_Dictionary_Order[ref_index]][i]);
                                        string orig_string = Rule_List[reference_index][5];
                                        string passed_string = orig_string.Substring(0, orig_string.IndexOf("[GRP]")) + orig_string.Substring(orig_string.IndexOf("[/GRP]") + 6);
                                        Rule_List[reference_index][5] = "[GRP]ID=" + Group_Dictionary_Order[ref_index] + "||" + desc_box.Text.Replace(Environment.NewLine, "``") + "[/GRP]" + passed_string;
                                    }
                                }
                            }
                            catch
                            {
                            }
                            if (ref_index >= 0)
                            {
                                Populate_Group_List();
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    dataGridView1[0, i].Value = false;
                                }
                                dataGridView1[0, ref_index].Value = true;
                            }
                            else
                            {
                                MessageBox.Show("No group selected");
                            }
                        }
                        else
                        {
                            MessageBox.Show("One or more rules selected are not in a group. Please ensure that the selected rules are assigned to a group");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Missing group description");
                }
            }
        }

        private void admin_text_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            CMD_INPUT_BOX CIB = new CMD_INPUT_BOX(this, _CMD_PARAMETER);
            CIB.ShowDialog();
        }

        // Add Group Condition
        private void button27_Click(object sender, EventArgs e)
        {
            Group_Condition GC = new Group_Condition(this, Group_Condition_String);
            GC.ShowDialog();
        }

        public void Set_Group_Condtion()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                bool has_Group = true;
                bool same_ID = true;
                string ref_ID = "";
                if (desc_box.Text.Length > 0)
                {
                    if (translation_rules.Items.Count > 0)
                    {
                        try
                        {
                            for (int i = translation_rules.Items.Count - 1; i >= 0; i--)
                            {
                                if (translation_rules.GetSelected(i) == true)
                                {
                                    if (!Rule_List[i][5].Contains("[GRP]")) has_Group = false;
                                    int start_index = Rule_List[i][5].IndexOf("[GRP]");
                                    int end_index = Rule_List[i][5].IndexOf("[/GRP]") + 6;
                                    string[] temp = (Rule_List[i][5].Substring(start_index + 5, end_index - start_index - 11)).Split(new string[] { "||" }, StringSplitOptions.None);
                                    if (ref_ID == "")
                                    {
                                        ref_ID = temp[0].Substring(3);
                                    }
                                    else if (ref_ID != temp[0].Substring(3))
                                    {
                                        same_ID = false;
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        if (has_Group && same_ID)
                        {
                            int ref_index = -1;
                            try
                            {
                                for (int ji = 0; ji < dataGridView1.Rows.Count; ji++)
                                {
                                    if (Convert.ToBoolean(dataGridView1[0, ji].Value))
                                    {
                                        ref_index = ji;
                                    }
                                }
                                if (ref_index >= 0)
                                {
                                    for (int i = 1; i < Group_Dictionary[Group_Dictionary_Order[ref_index]].Count(); i++)
                                    {
                                        int reference_index = Convert.ToInt32(Group_Dictionary[Group_Dictionary_Order[ref_index]][i]);
                                        string orig_string = Rule_List[reference_index][5];
                                        string passed_string = orig_string.Substring(0, orig_string.IndexOf("[GRP]")) + orig_string.Substring(orig_string.IndexOf("[/GRP]") + 6);
                                        Rule_List[reference_index][5] = "[GRP]ID=" + Group_Dictionary_Order[ref_index] + "||" + desc_box.Text.Replace(Environment.NewLine, "``") + (Group_Condition_String.Length > 0 ? "||" + Group_Condition_String : "") + "[/GRP]" + passed_string;
                                    }
                                    if (Group_Condition_String == "")
                                    {
                                        if (Group_Dictionary_Condition.ContainsKey(Group_Dictionary_Order[ref_index]))
                                            Group_Dictionary_Condition.Remove(Group_Dictionary_Order[ref_index]);
                                    }
                                }
                            }
                            catch
                            {
                            }
                            if (ref_index >= 0)
                            {
                                Populate_Group_List();
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    dataGridView1[0, i].Value = false;
                                }
                                dataGridView1[0, ref_index].Value = true;
                            }
                            else
                            {
                                MessageBox.Show("No group selected");
                            }
                        }
                        else if (!same_ID)
                        {
                            MessageBox.Show("One or more rules selected are not from the same group");
                        }
                        else
                        {
                            MessageBox.Show("One or more rules selected are not in a group. Please ensure that the selected rules are assigned to a group");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Missing group description");
                }
            }
        }

        private void group_color_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _Setup_Rule_List();
        }
        
    }

    public class If_Then_Conditions
    {
        // Conditions
        public int condition_bin { get; set; }
        public string condition_comparison { get; set; }
        public string condition_value { get; set; }

        // Actions
        public int action_bin { get; set; }
        public string action_comparison { get; set; }
        public string action_value { get; set; }

    }

    public class GetGroupColor : IDisposable
    {
        Dictionary<string, Color> Color_Book {get; set;}
        Color defined_Color { get { return Color_Book[defined_Key]; }  }
        public string defined_Key { get; set; }

        public GetGroupColor(Dictionary<string, Color> _Color_Book, string _Key)
        {
            Color_Book = _Color_Book;
            defined_Key = _Key;
            //this.defined_Color = Color_Book[defined_Key];
        }

        public Color randomBrush
        {
            get { return defined_Color; }
        }

        void IDisposable.Dispose()
        {

        }
    }
}
