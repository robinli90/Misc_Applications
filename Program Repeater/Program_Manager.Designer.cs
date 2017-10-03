using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Program_Repeater
{
    partial class Program_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program_Manager));
            this.Program_List = new System.Windows.Forms.ListBox();
            this.stop_button = new System.Windows.Forms.Button();
            this.output_box = new System.Windows.Forms.RichTextBox();
            this.remove_button = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.log_button = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Button();
            this.view_config = new System.Windows.Forms.Button();
            this.stopall_button = new System.Windows.Forms.Button();
            this.startall_button = new System.Windows.Forms.Button();
            this.admin_mode = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Program_List
            // 
            this.Program_List.FormattingEnabled = true;
            this.Program_List.HorizontalScrollbar = true;
            this.Program_List.Location = new System.Drawing.Point(8, 10);
            this.Program_List.Name = "Program_List";
            this.Program_List.Size = new System.Drawing.Size(631, 212);
            this.Program_List.TabIndex = 0;
            this.Program_List.SelectedIndexChanged += new System.EventHandler(this.Program_List_SelectedIndexChanged);
            this.Program_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Program_List_MouseDoubleClick);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(645, 112);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(113, 28);
            this.stop_button.TabIndex = 1;
            this.stop_button.Text = "Stop Selected";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // output_box
            // 
            this.output_box.Location = new System.Drawing.Point(8, 245);
            this.output_box.Name = "output_box";
            this.output_box.ReadOnly = true;
            this.output_box.Size = new System.Drawing.Size(631, 226);
            this.output_box.TabIndex = 2;
            this.output_box.Text = "";
            this.output_box.TextChanged += new System.EventHandler(this.output_box_TextChanged);
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(645, 78);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(113, 28);
            this.remove_button.TabIndex = 4;
            this.remove_button.Text = "Remove Program";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.remove_button_Click);
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(645, 10);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(113, 28);
            this.add_button.TabIndex = 5;
            this.add_button.Text = "Add Program";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // log_button
            // 
            this.log_button.Location = new System.Drawing.Point(645, 146);
            this.log_button.Name = "log_button";
            this.log_button.Size = new System.Drawing.Size(113, 28);
            this.log_button.TabIndex = 6;
            this.log_button.Text = "View Log";
            this.log_button.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.timer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timer.Location = new System.Drawing.Point(14, 480);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(113, 13);
            this.timer.TabIndex = 7;
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(645, 112);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(113, 28);
            this.start_button.TabIndex = 8;
            this.start_button.Text = "Start Selected";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Visible = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // view_config
            // 
            this.view_config.Location = new System.Drawing.Point(645, 180);
            this.view_config.Name = "view_config";
            this.view_config.Size = new System.Drawing.Size(113, 28);
            this.view_config.TabIndex = 9;
            this.view_config.Text = "View Configuration";
            this.view_config.UseVisualStyleBackColor = true;
            this.view_config.Click += new System.EventHandler(this.view_config_Click);
            // 
            // stopall_button
            // 
            this.stopall_button.Location = new System.Drawing.Point(645, 443);
            this.stopall_button.Name = "stopall_button";
            this.stopall_button.Size = new System.Drawing.Size(113, 28);
            this.stopall_button.TabIndex = 10;
            this.stopall_button.Text = "Stop All";
            this.stopall_button.UseVisualStyleBackColor = true;
            this.stopall_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // startall_button
            // 
            this.startall_button.Location = new System.Drawing.Point(645, 409);
            this.startall_button.Name = "startall_button";
            this.startall_button.Size = new System.Drawing.Size(113, 28);
            this.startall_button.TabIndex = 11;
            this.startall_button.Text = "Start All";
            this.startall_button.UseVisualStyleBackColor = true;
            this.startall_button.Click += new System.EventHandler(this.startall_button_Click);
            // 
            // admin_mode
            // 
            this.admin_mode.AutoSize = true;
            this.admin_mode.Location = new System.Drawing.Point(673, 480);
            this.admin_mode.Name = "admin_mode";
            this.admin_mode.Size = new System.Drawing.Size(85, 17);
            this.admin_mode.TabIndex = 12;
            this.admin_mode.Text = "Admin Mode";
            this.admin_mode.UseVisualStyleBackColor = true;
            this.admin_mode.CheckedChanged += new System.EventHandler(this.admin_mode_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(645, 258);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(645, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 28);
            this.button1.TabIndex = 14;
            this.button1.Text = "Duplicate Selected";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Program_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(764, 502);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.admin_mode);
            this.Controls.Add(this.startall_button);
            this.Controls.Add(this.stopall_button);
            this.Controls.Add(this.view_config);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.log_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.output_box);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.Program_List);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Program_Manager";
            this.Text = "Program Manager";
            this.Load += new System.EventHandler(this.Program_Manager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Program_List;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.RichTextBox output_box;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button log_button;
        private System.Windows.Forms.TextBox timer;
        private Button start_button;
        private Button view_config;
        private Button stopall_button;
        private Button startall_button;
        private CheckBox admin_mode;
        private TextBox textBox1;
        private Button button1;

    }
}

