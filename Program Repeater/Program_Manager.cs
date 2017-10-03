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
using System.Data.Odbc;

namespace Program_Repeater
{

    public partial class Program_Manager : Form
    {

        bool test = true;

        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();

        string Program_List_Path = Directory.GetCurrentDirectory() + "\\program_list.txt";
        string File_Handler_Path = Directory.GetCurrentDirectory() + "\\FileHandler.exe";

        static private int _INT_RESET_FACTOR = 86388; // One day's worth of seconds

        // Global ints

        Process _process = new Process();
        private int _PROGRAM_COUNT = 0;
        private int _TICK = 0;
        private int _TICK_RESET_COUNT = 0;
        private int _SELECTED_INDEX = 0; // selected index of program_list

        // Array of List. Each index represents the seconds 
        private List<List<string>>[] Program_List_By_Seconds = new List<List<string>>[_INT_RESET_FACTOR];
        private List<List<string>> Entire_Program_List = new List<List<string>>();

        public Program_Manager()
        {
            InitializeComponent();
            tick_update.Interval = 1000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(Per_Second_Function);
            START_Setup_Program_List();
        }

        private void Reset_Global_Parameters()
        {
            Program_List.Items.Clear();
             _process = new Process();
             _PROGRAM_COUNT = 0;
             _TICK = 0;
             _TICK_RESET_COUNT = 0;
             //_SELECTED_INDEX = 0; 
             Program_List_By_Seconds = new List<List<string>>[_INT_RESET_FACTOR+1];
             Entire_Program_List = new List<List<string>>();
        }

        private void Save_Programs_To_File()
        {
            string save_line = "";
            foreach (List<string> Program in Entire_Program_List)
            {
                save_line = save_line + Program[0] + "|";
                save_line = save_line + Program[1] + "|";
                save_line = save_line + Program[2] + "|";
                save_line = save_line + Program[3] + "|";
                save_line = save_line + Program[4] + "|";
                save_line = save_line + Program[5] + "|";
                save_line = save_line + Program[6] + "|";
                save_line = save_line + Program[7] + "|";
                save_line = save_line + Program[10] + Environment.NewLine;
                //save_line = save_line;// +Environment.NewLine;
            }
            try
            {
                File.Delete(Program_List_Path);
                using (StreamWriter sw = File.CreateText(Program_List_Path)) // Create translator file
                {
                    save_line.Trim();
                    sw.Write(save_line.Substring(0, save_line.Length-1));
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        private void START_Setup_Program_List()
        {

            // If program list file does not exist, create a new one
            if (!File.Exists(Program_List_Path))
            {
                using (StreamWriter sw = File.CreateText(Program_List_Path)) // Create LOG file
                {
                    //sw.Write("\n");
                    sw.Write("");
                    sw.Close();
                }
            }

            // Instantiate empty lists within array
            for (int i = 0; i < _INT_RESET_FACTOR; i++)
            {
                Program_List_By_Seconds[i] = new List<List<string>>();
            }


            var text = File.ReadAllText(Program_List_Path);
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    List<string> temp = new List<string>();
                    string[] program_line = line.Split(new string[] { "|" }, StringSplitOptions.None);
                    temp.Add(program_line[0]); // Program name
                    temp.Add(program_line[1]); // File name
                    temp.Add(program_line[2]); // Original path
                    temp.Add(program_line[3]); // Current path
                    temp.Add(program_line[4]); // Frequency Rate --> 'EVERY', 'DAILY'
                    temp.Add(program_line[5]); // Frequency: EVENT-TRIGGER_FREQUENCY --> EVERY:40, EVERY:60, DAILY:11:00:34, DAILY:21:22:31
                    temp.Add(program_line[6]); // Commands/Parameters
                    temp.Add(program_line[7]); // Bonus/Spare
                    temp.Add(_PROGRAM_COUNT.ToString()); // Program Level Index (in Entire_Program_List List)
                    temp.Add("STOPPED");
                    temp.Add(program_line[8]); // Text Color Output (10 index)
                    temp.Add("0"); // X-Axis Location (11)
                    temp.Add("0"); // Y-Axis Location (12)

                    Entire_Program_List.Add(temp);
                    _PROGRAM_COUNT++;
                    Insert_Program(temp);

                    // If not iterated by second frequency, convert from time to seconds 
                    if (temp[4].Contains("DAILY"))
                    {
                        try
                        {
                            string[] time = program_line[5].Split(new string[] { ":" }, StringSplitOptions.None);

                            Program_List_By_Seconds[Get_Seconds_From_Time(Convert.ToInt32(time[0]), // HOUR
                                                                          Convert.ToInt32(time[1]), // MINUTE
                                                                          Convert.ToInt32(time[2].Substring(0, 2)) // SECOND                       
                                                                        )].Add(temp);
                        }
                        catch
                        {
                            // Report to error log and output error in window
                            Program_List_By_Seconds[0].Add(temp);
                        }
                    }
                    else if (temp[4].Contains("EVERY"))
                    {
                        Program_List_By_Seconds[(Convert.ToInt32(program_line[5]))].Add(temp);
                    }
                }

                output_box.AppendText("Program initialized.", Color.Black);
                output_box.AppendText("Loading threads...", Color.Black);
                output_box.AppendText("Program ready.", Color.Black);
                Program_List.SelectedIndex = _SELECTED_INDEX;
                //output_box.AppendText("ALL PROGRAMS STARTED", Color.Green);
            }
        }

        // Return the number of seconds from now till x time
        private int Get_Seconds_From_Time(int future_time_Hour, int future_time_Minute, int future_time_Second)
        {
            DateTime current_time = DateTime.Now;
            int current_time_in_seconds = current_time.Hour * 60 * 60 + current_time.Minute * 60 + current_time.Second;
            int future_time_in_seconds = future_time_Hour * 60 * 60 + future_time_Minute * 60 + future_time_Second;
            return (future_time_in_seconds > current_time_in_seconds ? future_time_in_seconds - current_time_in_seconds : future_time_in_seconds);
        }

        // Iterate next instance of execution call
        private void Iterate_Next_Index(int current_tick, int frequency, int current_program_index)
        {
            if (current_tick + frequency >= _INT_RESET_FACTOR)
            {
                Program_List_By_Seconds[frequency-(_INT_RESET_FACTOR-current_tick)].Add(Program_List_By_Seconds[current_tick][current_program_index]);
                Program_List_By_Seconds[current_tick].RemoveAt(current_program_index);
            }
            else
            {
                Program_List_By_Seconds[current_tick + frequency].Add(Program_List_By_Seconds[current_tick][current_program_index]);
                Program_List_By_Seconds[current_tick].RemoveAt(current_program_index);
            }
        }


        public void Insert_Program_PUBLIC(List<string> program, int index = -1, bool edit=false)
        {
            try
            {
                if (index > -1)
                {
                    if (edit == false)
                    {
                        Entire_Program_List.Add(program);
                    }
                    else
                    {
                        Entire_Program_List.RemoveAt(index);
                        Entire_Program_List.Insert(index, program);
                    }
                    Save_Programs_To_File();
                    Reset_Global_Parameters();
                    START_Setup_Program_List();
                    
                }
            }
            catch { }
        }

        // Insert program into list
        public void Insert_Program(List<string> program, int index = -1)
        {
            if (index < 0)
            {
                Program_List.Items.Add("Status: " + program[9] + " | " +
                "Program: " + program[0] + " | " +
                "Filename: " + program[1] + " | " +
                "Path: " + program[2] + " | " +
                "Repeat: " + program[4] + (program[4].Contains("DAILY") ? " @ " + program[5] : " @ " + program[5] + " seconds")
                );
            }
            else
            {
                Program_List.Items.RemoveAt(index);
                Program_List.Items.Insert(index, "Status: " + program[9] + " | " +
                "Program: " + program[0] + " | " +
                "Filename: " + program[1] + " | " +
                "Path: " + program[2] + " | " +
                "Repeat: " + program[4] + (program[4].Contains("DAILY") ? " @ " + program[5] : " @ " + program[5] + " seconds")
                );
            }
            try
            {
                Program_List.SetSelected(index < 0 ? _SELECTED_INDEX : index, true);
            }
            catch
            {
                Program_List.SetSelected(index < 0 ? Program_List.Items.Count-1 : index, true);
            }

        }

        // Main program
        private void Per_Second_Function(object sender, EventArgs e)
        {
            if (_TICK >= _INT_RESET_FACTOR)
            {
                _TICK = 0;
                _TICK_RESET_COUNT++;
            }
            string output = "";

            if (admin_mode.Checked)
            {
                foreach (List<string> g in Entire_Program_List)
                {
                    output = output + g[9] + "__";
                }

                output_box.AppendText(output.Substring(0, output.Length - 2), Color.Black);
            }

            timer.Text = "Elapsed time: " + (_TICK_RESET_COUNT * 86400 + _TICK).ToString() + "s";
            // If there is a program within that second
            if (Program_List_By_Seconds[_TICK].Count > 0)
            {
                int Programs_At_Interval = Program_List_By_Seconds[_TICK].Count;
                for (int i = Programs_At_Interval - 1; i >= 0; i--)
                {

                    if (Entire_Program_List[Convert.ToInt32(Program_List_By_Seconds[_TICK][i][8])][9] == "STARTED") //Index 8 is the location of the program in Entire_Program_List
                    {
                        try
                        {
                            string command_line_aggregate_argument = "";

                            ProcessStartInfo startInfo = new ProcessStartInfo();

                            // If simple file handler program, setup parameters
                            if (Program_List_By_Seconds[_TICK][i][1] == "COPY" || Program_List_By_Seconds[_TICK][i][1] == "MOVE" || Program_List_By_Seconds[_TICK][i][1] == "MOVECOPY" || Program_List_By_Seconds[_TICK][i][1] == "DELETE")
                            {

                                //FILEHANDLER.EXE PARAMATERS
                                //PARAMETER
                                //0-MOVE/COPY/MOVECOPY/DELETE
                                //1-SOURCE PATH
                                //2-MOVE PATH
                                //3-COPY PATH
                                //4-FILE PARAMETER
                                string[] file_argument = Program_List_By_Seconds[_TICK][i][7].Split(new string[] { "~" }, StringSplitOptions.None);
                                command_line_aggregate_argument = Program_List_By_Seconds[_TICK][i][1] + // PARAM 0
                                                                  " \"" + Program_List_By_Seconds[_TICK][i][2] + "\" " + // PARAM 1
                                                                  " \"" + file_argument[1] + "\" " + // PARAM 2
                                                                  " \"" + file_argument[2] + "\" " + // PARAM 3
                                                                  file_argument[0]; //+ " && " + // PARAM 4

                                // Run without command window

                                //if (admin_mode.Checked)
                                //{
                                //    output_box.AppendText(File_Handler_Path, Color.Blue);
                                //    output_box.AppendText(command_line_aggregate_argument, Color.Blue);
                                //}
                                //output_box.AppendText("EXECUTING: " + Program_List_By_Seconds[_TICK][i][1]  + " (" + Program_List_By_Seconds[_TICK][i][0] + ")", System.Drawing.ColorTranslator.FromHtml(Program_List_By_Seconds[_TICK][i][10]));
                            }


                            try
                            {
                                _process.StartInfo.FileName = File_Handler_Path;
                                _process.StartInfo.Arguments = command_line_aggregate_argument;
                                _process.StartInfo.CreateNoWindow = true;
                                _process.StartInfo.UseShellExecute = false;
                                _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                                if (Program_List_By_Seconds[_TICK][i][1] == "COPY" || Program_List_By_Seconds[_TICK][i][1] == "MOVE" || Program_List_By_Seconds[_TICK][i][1] == "MOVECOPY" || Program_List_By_Seconds[_TICK][i][1] == "DELETE")
                                {
                                    _process.Start();
                                }
                                else
                                {
                                    if (Program_List_By_Seconds[_TICK][i][7].Contains("[TARGET_INDIVIDUAL_FILES]"))
                                    {
                                        string[] File_in_dir = Directory.GetFiles(Program_List_By_Seconds[_TICK][i][3]);

                                        foreach (string FILE_PATH in File_in_dir)
                                        {
                                            _process.StartInfo.FileName = Program_List_By_Seconds[_TICK][i][2];
                                            string g = Program_List_By_Seconds[_TICK][i][6].Replace("%F%", Path.GetFileName(FILE_PATH));
                                            if (admin_mode.Checked)
                                            {
                                                output_box.AppendText(g, Color.Black);
                                            }
                                            _process.StartInfo.Arguments = "\"\\\\10.0.0.8\\shopdata\\Development\\Robin\test\\113666 (2).txt\" \"TEST\" \"\\\\10.0.0.8\\shopdata\\Development\\Robin\\test\\backuptest\\ASDFADF113666 (2).txt\"";
                                            //Program_List_By_Seconds[_TICK][i][6].Replace("%F%", Path.GetFileName(FILE_PATH));
                                            _process.Start();
                                        }
                                    }
                                    else
                                    {
                                        _process.StartInfo.FileName = Program_List_By_Seconds[_TICK][i][2];
                                        _process.StartInfo.Arguments = Program_List_By_Seconds[_TICK][i][6];
                                        _process.Start();
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                output_box.AppendText("PROGRAM FOUND. ERROR IN EXECUTION: " + Program_List_By_Seconds[_TICK][i][0] + " (" + Program_List_By_Seconds[_TICK][i][1] + ")" + " : ", Color.Red);
                            }
                            if (admin_mode.Checked)
                            {
                                output_box.AppendText(File_Handler_Path, Color.Blue);
                                output_box.AppendText(command_line_aggregate_argument, Color.Blue);
                            }
                            output_box.AppendText("EXECUTING: " + Program_List_By_Seconds[_TICK][i][1] + " (" + Program_List_By_Seconds[_TICK][i][0] + ")", System.Drawing.ColorTranslator.FromHtml(Program_List_By_Seconds[_TICK][i][10]));
                            
                        }
                        catch
                        {
                            output_box.AppendText("ERROR IN PROGRAM: " + Program_List_By_Seconds[_TICK][i][0] + " (" + Program_List_By_Seconds[_TICK][i][1] + ")", Color.Red);
                            // Report to error log and output error in window
                        }
                        // If day, add 0 seconds to frequency so it reoccurs next time it hits _TICK seconds, else return frequency
                    }
                    Iterate_Next_Index(_TICK, Program_List_By_Seconds[_TICK][i][4].Contains("DAILY") ? 0 : Convert.ToInt32(Program_List_By_Seconds[_TICK][i][5]), i);
                }
            }
            _TICK++;
        }   

        private void Program_List_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int selected_index = this.Program_List.IndexFromPoint(e.Location);
            //List<int> SELECTED_INDICIES = new List<int>();
            //bool VALID_EDIT = true;
            for (int i = 0; i < Program_List.Items.Count; i++)
            {
                //if (Program_List.GetSelected(i) == true)
                   // selected_index = i;
                if (Entire_Program_List[selected_index][1].Contains("MOVE") || Entire_Program_List[selected_index][1].Contains("COPY") || Entire_Program_List[selected_index][1].Contains("DELETE"))
                {
                    Run_File_Handler rfh = new Run_File_Handler(this, selected_index, true, Entire_Program_List[selected_index]);
                    rfh.ShowDialog();
                    i = i + 10000;
                }
            }
            try
            {
                if (selected_index != System.Windows.Forms.ListBox.NoMatches)
                {

                    //List<string> temp_list = Rule_List[selected_index];
                    //Add_Rule g = new Add_Rule(this, true, selected_index, temp_list[0], temp_list[1], temp_list[2],
                                                 //   temp_list[3], temp_list[4], temp_list[5]
                                                //);
                    //g.ShowDialog();
                }
            }
            catch { }
        }

        private void Program_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Program_List.Items.Count; i++)
            {
                if (Program_List.GetSelected(i) == true)
                    _SELECTED_INDEX = i;
            }
            Refresh_START_STOP();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            temp.Add(Entire_Program_List[_SELECTED_INDEX][0]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][1]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][2]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][3]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][4]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][5]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][6]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][7]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][8]);
            temp.Add("STOPPED");
            temp.Add(Entire_Program_List[_SELECTED_INDEX][10]);
            Insert_Program(temp, _SELECTED_INDEX);
            Entire_Program_List.RemoveAt(_SELECTED_INDEX);
            Entire_Program_List.Insert(_SELECTED_INDEX, temp);
            output_box.AppendText(Entire_Program_List[_SELECTED_INDEX][0] + " has been stopped", Color.Red);
            Refresh_START_STOP();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            temp.Add(Entire_Program_List[_SELECTED_INDEX][0]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][1]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][2]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][3]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][4]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][5]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][6]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][7]);
            temp.Add(Entire_Program_List[_SELECTED_INDEX][8]);
            temp.Add("STARTED");
            temp.Add(Entire_Program_List[_SELECTED_INDEX][10]);
            Insert_Program(temp, _SELECTED_INDEX);
            Entire_Program_List.RemoveAt(_SELECTED_INDEX);
            Entire_Program_List.Insert(_SELECTED_INDEX, temp);
            output_box.AppendText(Entire_Program_List[_SELECTED_INDEX][0] + " has been started", Color.Green);

            Refresh_START_STOP();
        }

        // Refresh buttons
        private void Refresh_START_STOP()
        {
            try
            {
                if (Entire_Program_List[_SELECTED_INDEX][9] == "STARTED")
                {
                    start_button.Visible = false;
                    stop_button.Visible = true;
                }
                if (Entire_Program_List[_SELECTED_INDEX][9] == "STOPPED")
                {
                    start_button.Visible = true;
                    stop_button.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void view_config_Click(object sender, EventArgs e)
        {
            Process.Start(Program_List_Path);
        }

        // Stop All
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Program_List.Items.Count; i++)
            {
                List<string> temp = new List<string>();
                temp.Add(Entire_Program_List[i][0]);
                temp.Add(Entire_Program_List[i][1]);
                temp.Add(Entire_Program_List[i][2]);
                temp.Add(Entire_Program_List[i][3]);
                temp.Add(Entire_Program_List[i][4]);
                temp.Add(Entire_Program_List[i][5]);
                temp.Add(Entire_Program_List[i][6]);
                temp.Add(Entire_Program_List[i][7]);
                temp.Add(Entire_Program_List[i][8]);
                temp.Add("STOPPED");
                temp.Add(Entire_Program_List[i][10]);
                Insert_Program(temp, i);
                Entire_Program_List.RemoveAt(i);
                Entire_Program_List.Insert(i, temp);
                Refresh_START_STOP();
            }
            output_box.AppendText("ALL PROGRAMS STOPPED", Color.Red);
        }

        private void startall_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Program_List.Items.Count; i++)
            {
                List<string> temp = new List<string>();
                temp.Add(Entire_Program_List[i][0]);
                temp.Add(Entire_Program_List[i][1]);
                temp.Add(Entire_Program_List[i][2]);
                temp.Add(Entire_Program_List[i][3]);
                temp.Add(Entire_Program_List[i][4]);
                temp.Add(Entire_Program_List[i][5]);
                temp.Add(Entire_Program_List[i][6]);
                temp.Add(Entire_Program_List[i][7]);
                temp.Add(Entire_Program_List[i][8]);
                temp.Add("STARTED");
                temp.Add(Entire_Program_List[i][10]);
                Insert_Program(temp, i);
                Entire_Program_List.RemoveAt(i);
                Entire_Program_List.Insert(i, temp);
                Refresh_START_STOP();
            }
            output_box.AppendText("ALL PROGRAMS STARTED" , Color.Green);
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Program_Choice pc = new Program_Choice(this, _PROGRAM_COUNT, false);
            pc.ShowDialog();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (Entire_Program_List.Count > 0)
            {
                List<string> program = Entire_Program_List[_SELECTED_INDEX];
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the following program from manager? " + Environment.NewLine + Environment.NewLine +
                                                            "Program: " + program[0] + Environment.NewLine +
                                                            "Filename: " + program[1] + Environment.NewLine +
                                                            "Original Path: " + program[2] + Environment.NewLine +
                                                            "Repeat: " + program[4] + (program[4].Contains("DAILY") ? "" : " @ " + program[5] + " seconds") +
                                                            Environment.NewLine + Environment.NewLine + "Please note that the program will restart once completed"
                                                            , "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Entire_Program_List.RemoveAt(_SELECTED_INDEX);
                    Program_List.Items.RemoveAt(_SELECTED_INDEX);
                    Save_Programs_To_File();
                    Reset_Global_Parameters();
                    START_Setup_Program_List();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            
        }

        private string parse_file_name(string str, string file_name)
        {
            if (str.Contains("%F%"))
            {
                return str.Replace("%F%", file_name);
            }
            else
            {
                return str;
            }
        }

        private void reload_button_Click(object sender, EventArgs e)
        {
            //Save_Programs_To_File();
            Reset_Global_Parameters();
            START_Setup_Program_List();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            for (int i = 0; i < Program_List.Items.Count; i++)
            {
                if (Program_List.GetSelected(i) == true)
                {
                    Entire_Program_List.Insert(i+1, Entire_Program_List[i]);
                    Save_Programs_To_File();
                    Reset_Global_Parameters();
                    START_Setup_Program_List();
                }
            }
        }

        private void admin_mode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void output_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void Program_Manager_Load(object sender, EventArgs e)
        {

        }
    }   


    // Modified RichTextBox to enable color enhancement
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + Environment.NewLine);
            box.SelectionColor = box.ForeColor;

            box.SelectionStart = box.Text.Length;
            box.ScrollToCaret();
        }
    }


}
