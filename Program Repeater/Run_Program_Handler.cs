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
    public partial class Run_Program_Handler : Form
    {
        Program_Manager __parent = new Program_Manager();
        int __index = 0;
        bool __EDIT_MODE = false;
        List<string> __EDIT_PROGRAM = new List<string>();

        private string _PROGRAM_NAME = "";
        private string _DIAGNOSTIC_COLOR_HEX = "#000000";
        private string _FREQUENCY_CHOICE = "EVERY";
        private string _FREQUENCY = "";
        private string _FILE_NAME = "";
        private string _SOURCE_DIRECTORY = "";
        private string _TARGET_DIRECTORY = "";
        private string _PARAMETERS = "";


        
        
        public Run_Program_Handler(Program_Manager _parent, int index, bool EDIT_MODE=false, List<string> passed_List = null)
        {
            InitializeComponent();

            target_button.Visible = false;
            richTextBox1.Visible = false;
            cast_file_name.Visible = false;

            __EDIT_MODE = EDIT_MODE;
            __parent = _parent;
            __index = index;
            __EDIT_PROGRAM = passed_List ?? new List<String>();


            frequency.Items.Add("By seconds");
            frequency.Items.Add("At time");

            frequency.SelectedIndex = 0;
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

        private void source_directory_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Load Program File";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                _SOURCE_DIRECTORY = file.FileName;
                source_directory_text.Text = file.FileName;
                source_directory_text.ForeColor = Color.DarkGreen;
            }
        }

        private void finish_button_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            if (file_parameter.Text.Length == 0) _PARAMETERS = "*";
            if (_FREQUENCY_CHOICE.Length > 0 && _FREQUENCY.Length > 0 && _PROGRAM_NAME.Length > 0 && _SOURCE_DIRECTORY.Length > 0)
            {

                temp.Add(_PROGRAM_NAME);
                temp.Add(_FILE_NAME);
                temp.Add(_SOURCE_DIRECTORY);
                temp.Add(_TARGET_DIRECTORY);
                temp.Add(_FREQUENCY_CHOICE);
                temp.Add(_FREQUENCY);
                temp.Add(_PARAMETERS);
                temp.Add((target_individual.Checked ? "[TARGET_INDIVIDUAL_FILES]" : ""));
                temp.Add("");
                temp.Add("");
                temp.Add(_DIAGNOSTIC_COLOR_HEX);
                if (target_individual.Checked && _TARGET_DIRECTORY.Length == 0)
                {
                    MessageBox.Show("Missing a required entry in program information");
                }
                else
                {
                    __parent.Insert_Program_PUBLIC(temp, __index, __EDIT_MODE);
                    this.Close();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Missing a required entry in program information");
            }
        }

        private void file_parameter_TextChanged(object sender, EventArgs e)
        {
            _PARAMETERS = file_parameter.Text;
        }

        private void target_individual_CheckedChanged(object sender, EventArgs e)
        {
            if (target_individual.Checked)
            {
                cast_file_name.Visible = true;
                richTextBox1.Visible = true;
                cast_file_name.Visible = true;
            }
            else
            {
                target_button.Visible = false;
                richTextBox1.Visible = false;
                cast_file_name.Visible = false;
            }
        }

        private void target_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Load Program File";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                _TARGET_DIRECTORY = file.FileName;
                richTextBox1.Text = file.FileName;
                richTextBox1.ForeColor = Color.DarkGreen;
            }
        }
    }
}
