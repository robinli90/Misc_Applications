using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Program_Repeater
{
    partial class Run_Program_Handler
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
            this.richTextBox7 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.seconds_text = new System.Windows.Forms.RichTextBox();
            this.seconds_box = new System.Windows.Forms.TextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.frequency = new System.Windows.Forms.ComboBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.source_directory_text = new System.Windows.Forms.RichTextBox();
            this.source_directory_button = new System.Windows.Forms.Button();
            this.richTextBox9 = new System.Windows.Forms.RichTextBox();
            this.file_parameter = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.finish_button = new System.Windows.Forms.Button();
            this.target_individual = new System.Windows.Forms.CheckBox();
            this.cast_file_name = new System.Windows.Forms.RichTextBox();
            this.target_button = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
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
            this.richTextBox7.TabIndex = 113;
            this.richTextBox7.Text = "(Default: Black)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 22);
            this.button1.TabIndex = 112;
            this.button1.Text = "Select Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.dateTimePicker1.TabIndex = 111;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
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
            this.seconds_text.TabIndex = 110;
            this.seconds_text.Text = "seconds";
            // 
            // seconds_box
            // 
            this.seconds_box.BackColor = System.Drawing.Color.Silver;
            this.seconds_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seconds_box.Location = new System.Drawing.Point(201, 32);
            this.seconds_box.Name = "seconds_box";
            this.seconds_box.Size = new System.Drawing.Size(44, 20);
            this.seconds_box.TabIndex = 109;
            this.seconds_box.TextChanged += new System.EventHandler(this.seconds_box_TextChanged);
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
            this.richTextBox6.TabIndex = 108;
            this.richTextBox6.Text = "Select diagnoistic color:";
            // 
            // frequency
            // 
            this.frequency.BackColor = System.Drawing.Color.Silver;
            this.frequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frequency.FormattingEnabled = true;
            this.frequency.Location = new System.Drawing.Point(106, 31);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(89, 21);
            this.frequency.TabIndex = 107;
            this.frequency.SelectedIndexChanged += new System.EventHandler(this.frequency_SelectedIndexChanged);
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
            this.richTextBox5.TabIndex = 106;
            this.richTextBox5.Text = "Repeat Frequency:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Silver;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(94, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 20);
            this.textBox1.TabIndex = 105;
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
            this.richTextBox4.TabIndex = 104;
            this.richTextBox4.Text = "Program Name:";
            // 
            // source_directory_text
            // 
            this.source_directory_text.BackColor = System.Drawing.SystemColors.ControlDark;
            this.source_directory_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.source_directory_text.ForeColor = System.Drawing.Color.Red;
            this.source_directory_text.Location = new System.Drawing.Point(217, 83);
            this.source_directory_text.Name = "source_directory_text";
            this.source_directory_text.ReadOnly = true;
            this.source_directory_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.source_directory_text.Size = new System.Drawing.Size(309, 16);
            this.source_directory_text.TabIndex = 116;
            this.source_directory_text.Text = "No directory chosen";
            // 
            // source_directory_button
            // 
            this.source_directory_button.Location = new System.Drawing.Point(130, 80);
            this.source_directory_button.Name = "source_directory_button";
            this.source_directory_button.Size = new System.Drawing.Size(81, 22);
            this.source_directory_button.TabIndex = 115;
            this.source_directory_button.Text = "Browse";
            this.source_directory_button.UseVisualStyleBackColor = true;
            this.source_directory_button.Click += new System.EventHandler(this.source_directory_button_Click);
            // 
            // richTextBox9
            // 
            this.richTextBox9.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox9.Location = new System.Drawing.Point(13, 83);
            this.richTextBox9.Name = "richTextBox9";
            this.richTextBox9.ReadOnly = true;
            this.richTextBox9.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox9.Size = new System.Drawing.Size(166, 16);
            this.richTextBox9.TabIndex = 114;
            this.richTextBox9.Text = "Choose target program";
            // 
            // file_parameter
            // 
            this.file_parameter.BackColor = System.Drawing.Color.Silver;
            this.file_parameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_parameter.Location = new System.Drawing.Point(13, 131);
            this.file_parameter.Multiline = true;
            this.file_parameter.Name = "file_parameter";
            this.file_parameter.Size = new System.Drawing.Size(517, 53);
            this.file_parameter.TabIndex = 118;
            this.file_parameter.TextChanged += new System.EventHandler(this.file_parameter_TextChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(13, 109);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(152, 16);
            this.richTextBox2.TabIndex = 117;
            this.richTextBox2.Text = "Additional parameters (optional):";
            // 
            // finish_button
            // 
            this.finish_button.Location = new System.Drawing.Point(449, 190);
            this.finish_button.Name = "finish_button";
            this.finish_button.Size = new System.Drawing.Size(81, 22);
            this.finish_button.TabIndex = 119;
            this.finish_button.Text = "Finish";
            this.finish_button.UseVisualStyleBackColor = true;
            this.finish_button.Click += new System.EventHandler(this.finish_button_Click);
            // 
            // target_individual
            // 
            this.target_individual.AutoSize = true;
            this.target_individual.Location = new System.Drawing.Point(29, 109);
            this.target_individual.Name = "target_individual";
            this.target_individual.Size = new System.Drawing.Size(136, 17);
            this.target_individual.TabIndex = 120;
            this.target_individual.Text = "Target Files Individually";
            this.target_individual.UseVisualStyleBackColor = true;
            this.target_individual.Visible = false;
            this.target_individual.CheckedChanged += new System.EventHandler(this.target_individual_CheckedChanged);
            // 
            // cast_file_name
            // 
            this.cast_file_name.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cast_file_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cast_file_name.ForeColor = System.Drawing.Color.Red;
            this.cast_file_name.Location = new System.Drawing.Point(367, 108);
            this.cast_file_name.Name = "cast_file_name";
            this.cast_file_name.ReadOnly = true;
            this.cast_file_name.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.cast_file_name.Size = new System.Drawing.Size(162, 17);
            this.cast_file_name.TabIndex = 121;
            this.cast_file_name.Text = "*use %F% to cast target filename";
            // 
            // target_button
            // 
            this.target_button.Location = new System.Drawing.Point(171, 105);
            this.target_button.Name = "target_button";
            this.target_button.Size = new System.Drawing.Size(136, 22);
            this.target_button.TabIndex = 122;
            this.target_button.Text = "Choose Target Directory";
            this.target_button.UseVisualStyleBackColor = true;
            this.target_button.Click += new System.EventHandler(this.target_button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Red;
            this.richTextBox1.Location = new System.Drawing.Point(313, 110);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(228, 15);
            this.richTextBox1.TabIndex = 123;
            this.richTextBox1.Text = "No directory chosen";
            // 
            // Run_Program_Handler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(541, 220);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.target_button);
            this.Controls.Add(this.cast_file_name);
            this.Controls.Add(this.target_individual);
            this.Controls.Add(this.finish_button);
            this.Controls.Add(this.file_parameter);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.source_directory_text);
            this.Controls.Add(this.source_directory_button);
            this.Controls.Add(this.richTextBox9);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Run_Program_Handler";
            this.Text = "Program Handler Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        protected internal System.Windows.Forms.RichTextBox seconds_text;
        private System.Windows.Forms.TextBox seconds_box;
        protected internal System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.ComboBox frequency;
        protected internal System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.TextBox textBox1;
        protected internal System.Windows.Forms.RichTextBox richTextBox4;
        private ColorDialog colorDialog1;
        private System.Windows.Forms.RichTextBox source_directory_text;
        private System.Windows.Forms.Button source_directory_button;
        private System.Windows.Forms.RichTextBox richTextBox9;
        private System.Windows.Forms.TextBox file_parameter;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private Button finish_button;
        private CheckBox target_individual;
        private RichTextBox cast_file_name;
        private Button target_button;
        private RichTextBox richTextBox1;
    }
}