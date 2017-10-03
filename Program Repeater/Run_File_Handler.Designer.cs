using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Program_Repeater
{
    partial class Run_File_Handler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.action_dropdown = new System.Windows.Forms.ComboBox();
            this.richTextBox9 = new System.Windows.Forms.RichTextBox();
            this.source_directory_button = new System.Windows.Forms.Button();
            this.source_directory_text = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.file_parameter = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.destination_1_Text = new System.Windows.Forms.RichTextBox();
            this.browse_1 = new System.Windows.Forms.Button();
            this.destination_1 = new System.Windows.Forms.RichTextBox();
            this.destination_2_Text = new System.Windows.Forms.RichTextBox();
            this.browse_2 = new System.Windows.Forms.Button();
            this.destination_2 = new System.Windows.Forms.RichTextBox();
            this.finish_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.frequency = new System.Windows.Forms.ComboBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.seconds_box = new System.Windows.Forms.TextBox();
            this.seconds_text = new System.Windows.Forms.RichTextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox7 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // action_dropdown
            // 
            this.action_dropdown.BackColor = System.Drawing.Color.Silver;
            this.action_dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.action_dropdown.FormattingEnabled = true;
            this.action_dropdown.Location = new System.Drawing.Point(181, 121);
            this.action_dropdown.Name = "action_dropdown";
            this.action_dropdown.Size = new System.Drawing.Size(335, 21);
            this.action_dropdown.TabIndex = 77;
            this.action_dropdown.SelectedIndexChanged += new System.EventHandler(this.action_dropdown_SelectedIndexChanged);
            // 
            // richTextBox9
            // 
            this.richTextBox9.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox9.Location = new System.Drawing.Point(12, 99);
            this.richTextBox9.Name = "richTextBox9";
            this.richTextBox9.ReadOnly = true;
            this.richTextBox9.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox9.Size = new System.Drawing.Size(166, 16);
            this.richTextBox9.TabIndex = 78;
            this.richTextBox9.Text = "Choose target (source) directory";
            // 
            // source_directory_button
            // 
            this.source_directory_button.Location = new System.Drawing.Point(180, 96);
            this.source_directory_button.Name = "source_directory_button";
            this.source_directory_button.Size = new System.Drawing.Size(81, 22);
            this.source_directory_button.TabIndex = 79;
            this.source_directory_button.Text = "Browse";
            this.source_directory_button.UseVisualStyleBackColor = true;
            this.source_directory_button.Click += new System.EventHandler(this.source_directory_button_Click);
            // 
            // source_directory_text
            // 
            this.source_directory_text.BackColor = System.Drawing.SystemColors.ControlDark;
            this.source_directory_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.source_directory_text.ForeColor = System.Drawing.Color.Red;
            this.source_directory_text.Location = new System.Drawing.Point(267, 99);
            this.source_directory_text.Name = "source_directory_text";
            this.source_directory_text.ReadOnly = true;
            this.source_directory_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.source_directory_text.Size = new System.Drawing.Size(309, 16);
            this.source_directory_text.TabIndex = 80;
            this.source_directory_text.Text = "No directory chosen";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 125);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(152, 16);
            this.richTextBox1.TabIndex = 81;
            this.richTextBox1.Text = "Choose what to do with the files";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(12, 151);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(152, 16);
            this.richTextBox2.TabIndex = 82;
            this.richTextBox2.Text = "Choose which files to pick up";
            // 
            // file_parameter
            // 
            this.file_parameter.BackColor = System.Drawing.Color.Silver;
            this.file_parameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_parameter.Location = new System.Drawing.Point(181, 147);
            this.file_parameter.Name = "file_parameter";
            this.file_parameter.Size = new System.Drawing.Size(81, 20);
            this.file_parameter.TabIndex = 83;
            this.file_parameter.TextChanged += new System.EventHandler(this.file_parameter_TextChanged);
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Location = new System.Drawing.Point(267, 149);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(360, 28);
            this.richTextBox3.TabIndex = 84;
            this.richTextBox3.Text = "Use (*) for wildcard. Type in *.txt for text files, *CRV* for files containing CR" +
    "V";
            // 
            // destination_1_Text
            // 
            this.destination_1_Text.BackColor = System.Drawing.SystemColors.ControlDark;
            this.destination_1_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destination_1_Text.ForeColor = System.Drawing.Color.Red;
            this.destination_1_Text.Location = new System.Drawing.Point(267, 175);
            this.destination_1_Text.Name = "destination_1_Text";
            this.destination_1_Text.ReadOnly = true;
            this.destination_1_Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.destination_1_Text.Size = new System.Drawing.Size(309, 16);
            this.destination_1_Text.TabIndex = 87;
            this.destination_1_Text.Text = "No directory chosen";
            this.destination_1_Text.Visible = false;
            // 
            // browse_1
            // 
            this.browse_1.Location = new System.Drawing.Point(180, 172);
            this.browse_1.Name = "browse_1";
            this.browse_1.Size = new System.Drawing.Size(81, 22);
            this.browse_1.TabIndex = 86;
            this.browse_1.Text = "Browse";
            this.browse_1.UseVisualStyleBackColor = true;
            this.browse_1.Visible = false;
            this.browse_1.Click += new System.EventHandler(this.browse_1_Click);
            // 
            // destination_1
            // 
            this.destination_1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.destination_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destination_1.Location = new System.Drawing.Point(12, 175);
            this.destination_1.Name = "destination_1";
            this.destination_1.ReadOnly = true;
            this.destination_1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.destination_1.Size = new System.Drawing.Size(170, 16);
            this.destination_1.TabIndex = 85;
            this.destination_1.Text = "Choose destination directory";
            this.destination_1.Visible = false;
            // 
            // destination_2_Text
            // 
            this.destination_2_Text.BackColor = System.Drawing.SystemColors.ControlDark;
            this.destination_2_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destination_2_Text.ForeColor = System.Drawing.Color.Red;
            this.destination_2_Text.Location = new System.Drawing.Point(267, 200);
            this.destination_2_Text.Name = "destination_2_Text";
            this.destination_2_Text.ReadOnly = true;
            this.destination_2_Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.destination_2_Text.Size = new System.Drawing.Size(309, 16);
            this.destination_2_Text.TabIndex = 90;
            this.destination_2_Text.Text = "No directory chosen";
            // 
            // browse_2
            // 
            this.browse_2.Location = new System.Drawing.Point(180, 197);
            this.browse_2.Name = "browse_2";
            this.browse_2.Size = new System.Drawing.Size(81, 22);
            this.browse_2.TabIndex = 89;
            this.browse_2.Text = "Browse";
            this.browse_2.UseVisualStyleBackColor = true;
            this.browse_2.Click += new System.EventHandler(this.browse_2_Click);
            // 
            // destination_2
            // 
            this.destination_2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.destination_2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destination_2.Location = new System.Drawing.Point(12, 200);
            this.destination_2.Name = "destination_2";
            this.destination_2.ReadOnly = true;
            this.destination_2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.destination_2.Size = new System.Drawing.Size(169, 16);
            this.destination_2.TabIndex = 88;
            this.destination_2.Text = "Choose move destination directory";
            // 
            // finish_button
            // 
            this.finish_button.Location = new System.Drawing.Point(547, 220);
            this.finish_button.Name = "finish_button";
            this.finish_button.Size = new System.Drawing.Size(81, 22);
            this.finish_button.TabIndex = 91;
            this.finish_button.Text = "Finish";
            this.finish_button.UseVisualStyleBackColor = true;
            this.finish_button.Click += new System.EventHandler(this.finish_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Silver;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(94, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 20);
            this.textBox1.TabIndex = 93;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Location = new System.Drawing.Point(12, 12);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox4.Size = new System.Drawing.Size(81, 16);
            this.richTextBox4.TabIndex = 92;
            this.richTextBox4.Text = "Program Name:";
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.Location = new System.Drawing.Point(12, 34);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(99, 16);
            this.richTextBox5.TabIndex = 94;
            this.richTextBox5.Text = "Repeat Frequency:";
            // 
            // frequency
            // 
            this.frequency.BackColor = System.Drawing.Color.Silver;
            this.frequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frequency.FormattingEnabled = true;
            this.frequency.Location = new System.Drawing.Point(106, 31);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(89, 21);
            this.frequency.TabIndex = 95;
            this.frequency.SelectedIndexChanged += new System.EventHandler(this.frequency_SelectedIndexChanged);
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Location = new System.Drawing.Point(12, 59);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox6.Size = new System.Drawing.Size(117, 16);
            this.richTextBox6.TabIndex = 96;
            this.richTextBox6.Text = "Select diagnoistic color:";
            // 
            // seconds_box
            // 
            this.seconds_box.BackColor = System.Drawing.Color.Silver;
            this.seconds_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seconds_box.Location = new System.Drawing.Point(201, 32);
            this.seconds_box.Name = "seconds_box";
            this.seconds_box.Size = new System.Drawing.Size(44, 20);
            this.seconds_box.TabIndex = 98;
            this.seconds_box.TextChanged += new System.EventHandler(this.seconds_box_TextChanged);
            // 
            // seconds_text
            // 
            this.seconds_text.BackColor = System.Drawing.SystemColors.ControlDark;
            this.seconds_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seconds_text.Location = new System.Drawing.Point(251, 34);
            this.seconds_text.Name = "seconds_text";
            this.seconds_text.ReadOnly = true;
            this.seconds_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.seconds_text.Size = new System.Drawing.Size(81, 16);
            this.seconds_text.TabIndex = 99;
            this.seconds_text.Text = "seconds";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.SystemColors.ControlDark;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.SystemColors.ControlDark;
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.SystemColors.ControlDark;
            this.dateTimePicker1.CustomFormat = "HH:mm:ss tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(201, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(83, 20);
            this.dateTimePicker1.TabIndex = 100;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 22);
            this.button1.TabIndex = 102;
            this.button1.Text = "Select Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox7
            // 
            this.richTextBox7.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox7.ForeColor = System.Drawing.Color.Black;
            this.richTextBox7.Location = new System.Drawing.Point(212, 60);
            this.richTextBox7.Name = "richTextBox7";
            this.richTextBox7.ReadOnly = true;
            this.richTextBox7.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox7.Size = new System.Drawing.Size(82, 16);
            this.richTextBox7.TabIndex = 103;
            this.richTextBox7.Text = "(Default: Black)";
            // 
            // Run_File_Handler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(633, 247);
            this.Controls.Add(this.richTextBox7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.seconds_text);
            this.Controls.Add(this.seconds_box);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.finish_button);
            this.Controls.Add(this.destination_2_Text);
            this.Controls.Add(this.browse_2);
            this.Controls.Add(this.destination_2);
            this.Controls.Add(this.destination_1_Text);
            this.Controls.Add(this.browse_1);
            this.Controls.Add(this.destination_1);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.file_parameter);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.source_directory_text);
            this.Controls.Add(this.source_directory_button);
            this.Controls.Add(this.richTextBox9);
            this.Controls.Add(this.action_dropdown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Run_File_Handler";
            this.Text = "File Handler Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox action_dropdown;
        private System.Windows.Forms.RichTextBox richTextBox9;
        private System.Windows.Forms.Button source_directory_button;
        private System.Windows.Forms.RichTextBox source_directory_text;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox file_parameter;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox destination_1_Text;
        private System.Windows.Forms.Button browse_1;
        private System.Windows.Forms.RichTextBox destination_1;
        private System.Windows.Forms.RichTextBox destination_2_Text;
        private System.Windows.Forms.Button browse_2;
        private System.Windows.Forms.RichTextBox destination_2;
        private System.Windows.Forms.Button finish_button;
        private System.Windows.Forms.TextBox textBox1;
        protected internal System.Windows.Forms.RichTextBox richTextBox4;
        protected internal System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.ComboBox frequency;
        protected internal System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.TextBox seconds_box;
        protected internal System.Windows.Forms.RichTextBox seconds_text;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private ColorDialog colorDialog1;
        private Button button1;
        private RichTextBox richTextBox7;
    }
}