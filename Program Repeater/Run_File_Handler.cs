using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_Repeater
{
    public partial class Run_File_Handler : Form
    {
        Program_Manager __parent = new Program_Manager();
        int __index = 0;
        bool __EDIT_MODE = false;
        List<string> __EDIT_PROGRAM = new List<string>();
        private string _SOURCE_DIRECTORY = "";
        private string _COPY_DIRECTORY = "";
        private string _MOVE_DIRECTORY = "";
        private string _SET_FILE_ACTION = "";
        private string _FILE_PARAMETER = "";
        private string _PROGRAM_NAME = "";
        private string _DIAGNOSTIC_COLOR_HEX = "#000000";
        private string _FREQUENCY_CHOICE = "EVERY";
        private string _FREQUENCY = "";


        public Run_File_Handler(Program_Manager _parent, int index, bool EDIT_MODE=false, List<string> passed_List = null)
        {
            __EDIT_MODE = EDIT_MODE;
            __parent = _parent;
            __index = index;
            InitializeComponent();
            __EDIT_PROGRAM = passed_List ?? new List<String>();

            action_dropdown.Items.Add("Copy files from source to destination");
            action_dropdown.Items.Add("Move files from source to destination");
            action_dropdown.Items.Add("Copy files to COPY destination & move files to MOVE destination");
            action_dropdown.Items.Add("Delete files from source destination");
            frequency.Items.Add("By seconds");
            frequency.Items.Add("At time");
            action_dropdown.SelectedIndex = 0;
            frequency.SelectedIndex = 0;

            if (__EDIT_MODE)
            {

                string[] paramaters = __EDIT_PROGRAM[7].Split(new string[] { "~" }, StringSplitOptions.None);
                _SOURCE_DIRECTORY = __EDIT_PROGRAM[2];
                if (__EDIT_PROGRAM[1] == "MOVE")
                    _COPY_DIRECTORY = paramaters[1];
                else
                    _COPY_DIRECTORY = paramaters[2];
                _MOVE_DIRECTORY = paramaters[1];
                _SET_FILE_ACTION = __EDIT_PROGRAM[1];
                _FILE_PARAMETER = paramaters[0];
                _PROGRAM_NAME = __EDIT_PROGRAM[0];
                _DIAGNOSTIC_COLOR_HEX = __EDIT_PROGRAM[10];
                _FREQUENCY_CHOICE = __EDIT_PROGRAM[4];
                _FREQUENCY = __EDIT_PROGRAM[5];

                textBox1.Text = _PROGRAM_NAME;
                frequency.Text = _FREQUENCY_CHOICE;
                if (_FREQUENCY.Contains("M"))
                {
                    seconds_text.Text = "";
                    dateTimePicker1.Visible = true;
                    dateTimePicker1.Text = _FREQUENCY;
                    _FREQUENCY_CHOICE = "DAILY";
                    frequency.Text = "At time";
                }
                else
                {
                    _FREQUENCY_CHOICE = "EVERY";
                    frequency.Text = "By seconds";
                    seconds_box.Text = _FREQUENCY;
                    dateTimePicker1.Visible = false;
                }


                _FREQUENCY = __EDIT_PROGRAM[5];
                button1.BackColor = System.Drawing.ColorTranslator.FromHtml(_DIAGNOSTIC_COLOR_HEX);
                source_directory_text.Text = _SOURCE_DIRECTORY;
                action_dropdown.Text = _SET_FILE_ACTION;


                destination_1_Text.Text = _MOVE_DIRECTORY ;
                if (_SET_FILE_ACTION == "COPY")
                {
                    destination_1_Text.Text = _COPY_DIRECTORY;
                    action_dropdown.Text = "Copy files from source to destination";
                }
                if (_SET_FILE_ACTION == "MOVE")
                {
                    action_dropdown.Text = "Move files from source to destination";
                }
                if (_SET_FILE_ACTION == "MOVECOPY")
                {
                    action_dropdown.Text = "Copy files to COPY destination & move files to MOVE destination";
                    destination_2_Text.Text = _COPY_DIRECTORY;
                }
                if (_SET_FILE_ACTION == "DELETE")
                {
                    action_dropdown.Text = "Delete files from source destination";
                }
                    
                file_parameter.Text = _FILE_PARAMETER;

            }

        }

        private void source_directory_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            _SOURCE_DIRECTORY = fbd.SelectedPath;
            source_directory_text.Text = fbd.SelectedPath;
            source_directory_text.ForeColor = Color.DarkGreen;

        }

        private void action_dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            destination_2.Visible = true;
            destination_2_Text.Visible = true;
            browse_2.Visible = true;
            destination_1.Visible = true;
            destination_1_Text.Visible = true;
            browse_1.Visible = true;
            if (action_dropdown.Text == "Copy files from source to destination")                          
                _SET_FILE_ACTION = "COPY";
            if (action_dropdown.Text == "Move files from source to destination")                          
                _SET_FILE_ACTION = "MOVE";
            if (action_dropdown.Text == "Copy files to COPY destination & move files to MOVE destination")
                _SET_FILE_ACTION = "MOVECOPY";
            if (action_dropdown.Text == "Delete files from source destination")                            
                _SET_FILE_ACTION = "DELETE";
            if (_SET_FILE_ACTION == "MOVECOPY")
            {
                destination_2.Visible = true;
                destination_2_Text.Visible = true;
                browse_2.Visible = true;
            }
            else
            {
                destination_2.Visible = false;
                destination_2_Text.Visible = false;
                browse_2.Visible = false;
            }
            if (_SET_FILE_ACTION.Contains("COPY"))
                destination_1.Text = "Choose copy destination directory";
            if (_SET_FILE_ACTION == "MOVE")
                destination_1.Text = "Choose move destination directory";
            if (_SET_FILE_ACTION == "DELETE")
            {
                destination_2.Visible = false;
                destination_2_Text.Visible = false;
                browse_2.Visible = false;
                destination_1.Visible = false;
                destination_1_Text.Visible = false;
                browse_1.Visible = false;
            }
        
        }

        //COPY DESTINATION (fIRST ONE)
        private void browse_1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            _COPY_DIRECTORY = fbd.SelectedPath;
            destination_1_Text.Text = fbd.SelectedPath;
            destination_1_Text.ForeColor = Color.DarkGreen;

        }

        // MOVE DESTINATION
        private void browse_2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            _MOVE_DIRECTORY = fbd.SelectedPath;
            destination_2_Text.Text = fbd.SelectedPath;
            destination_2_Text.ForeColor = Color.DarkGreen;


        }

        private void finish_button_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            if (file_parameter.Text.Length == 0) _FILE_PARAMETER = "*";
            if (_FREQUENCY_CHOICE.Length > 0 && _FREQUENCY.Length > 0 && _PROGRAM_NAME.Length > 0)
            {
                temp.Add(_PROGRAM_NAME);
                temp.Add(_SET_FILE_ACTION);
                if (_SOURCE_DIRECTORY.Length > 1 && _SET_FILE_ACTION.Length > 1) // Base parameters complete
                {
                    temp.Add(_SOURCE_DIRECTORY);
                    temp.Add(_SOURCE_DIRECTORY);
                    temp.Add(_FREQUENCY_CHOICE);
                    temp.Add(_FREQUENCY);
                    temp.Add("");
                    string extra_text = _FILE_PARAMETER + "~";
                    if (_SET_FILE_ACTION=="COPY" || _SET_FILE_ACTION=="MOVE")
                    {
                        if (_COPY_DIRECTORY.Length > 1)
                        {
                            if (_SET_FILE_ACTION == "COPY")
                            {
                                extra_text = extra_text + "~" + _COPY_DIRECTORY;
                                temp.Add(extra_text);
                                temp.Add(""); temp.Add("");
                                temp.Add(_DIAGNOSTIC_COLOR_HEX);
                                __parent.Insert_Program_PUBLIC(temp, __index, __EDIT_MODE);
                                this.Close();
                                this.Dispose();
                            }
                            else
                            {
                                extra_text = extra_text + _COPY_DIRECTORY + "~";
                                temp.Add(extra_text);
                                temp.Add("");temp.Add("");
                                temp.Add(_DIAGNOSTIC_COLOR_HEX);
                                __parent.Insert_Program_PUBLIC(temp, __index, __EDIT_MODE);
                                this.Close();
                                this.Dispose();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Destination directory missing");
                        }
                    }
                    else if (_SET_FILE_ACTION=="MOVECOPY")
                    {
                        if (_COPY_DIRECTORY.Length > 1 && _MOVE_DIRECTORY.Length > 1)
                        {
                            extra_text = extra_text + _MOVE_DIRECTORY + "~" + _COPY_DIRECTORY;
                            temp.Add(extra_text);
                            temp.Add(""); temp.Add("");
                            temp.Add(_DIAGNOSTIC_COLOR_HEX);
                            __parent.Insert_Program_PUBLIC(temp, __index, __EDIT_MODE);
                            this.Close();
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Move or Copy destination directory missing");
                        }
                    }
                    else if (_SET_FILE_ACTION=="DELETE")
                    {
                        extra_text = extra_text + "~";
                        temp.Add(extra_text);
                        temp.Add(""); temp.Add("");
                        temp.Add(_DIAGNOSTIC_COLOR_HEX);
                        __parent.Insert_Program_PUBLIC(temp, __index, __EDIT_MODE);
                        this.Close();
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Source directory missing");
                }
            }
            else
            {
                    MessageBox.Show("Program property missing");
            }
        }

        private void file_parameter_TextChanged(object sender, EventArgs e)
        {
            _FILE_PARAMETER = file_parameter.Text;
        }

        private void frequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frequency.Text == "At time")
            {
                dateTimePicker1.Visible = true;
                seconds_box.Visible = false;
                seconds_text.Visible = false;
                _FREQUENCY_CHOICE = "DAILY";
            }
            else
            {
                dateTimePicker1.Visible = false;
                seconds_box.Visible = true;
                seconds_text.Visible = true;
                _FREQUENCY_CHOICE = "EVERY";
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _FREQUENCY = dateTimePicker1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _DIAGNOSTIC_COLOR_HEX = ColorTranslator.ToHtml(Color.FromArgb(colorDialog1.Color.ToArgb()));
                button1.BackColor = colorDialog1.Color;
                if (!(_DIAGNOSTIC_COLOR_HEX == "#000000"))
                {
                    richTextBox7.Visible = false;
                }
            }
        }

        private void seconds_box_TextChanged(object sender, EventArgs e)
        {
            if (seconds_box.Text.All(char.IsDigit))
            {
                _FREQUENCY = seconds_box.Text;
            }
            else
            {
                // If letter in SO_number box, do not output and move CARET to end
                seconds_box.Text = seconds_box.Text.Substring(0, seconds_box.Text.Length - 1);
                seconds_box.SelectionStart = seconds_box.Text.Length;
                seconds_box.SelectionLength = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("|") || textBox1.Text.Contains("~"))
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }
            else
            {
                _PROGRAM_NAME = textBox1.Text;
            }
        }


    }
}
