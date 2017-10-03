using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class Administrative
    {

        // Mouse down anywhere to drag
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administrative));
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.minimize_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.deativate_monitor_button = new System.Windows.Forms.Button();
            this.deactivate_process_button = new System.Windows.Forms.Button();
            this.reset_button = new System.Windows.Forms.Button();
            this.test_folder_button = new System.Windows.Forms.Button();
            this.ping_button = new System.Windows.Forms.Button();
            this.ping_test_4 = new System.Windows.Forms.TextBox();
            this.ping_test_1 = new System.Windows.Forms.TextBox();
            this.ping_test_2 = new System.Windows.Forms.TextBox();
            this.ping_test_3 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buffer_text = new System.Windows.Forms.RichTextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(283, -13);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(10, 417);
            this.textBox3.TabIndex = 46;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-586, -20);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(871, 21);
            this.textBox4.TabIndex = 45;
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(247, -3);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(15, 22);
            this.minimize_button.TabIndex = 44;
            this.minimize_button.Text = "_";
            this.minimize_button.UseVisualStyleBackColor = true;
            this.minimize_button.Click += new System.EventHandler(this.minimize_button_Click);
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.close_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.close_button.Location = new System.Drawing.Point(264, 3);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 43;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-9, -86);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 417);
            this.textBox1.TabIndex = 47;
            // 
            // deativate_monitor_button
            // 
            this.deativate_monitor_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deativate_monitor_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deativate_monitor_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deativate_monitor_button.Location = new System.Drawing.Point(10, 26);
            this.deativate_monitor_button.Margin = new System.Windows.Forms.Padding(1);
            this.deativate_monitor_button.Name = "deativate_monitor_button";
            this.deativate_monitor_button.Size = new System.Drawing.Size(84, 37);
            this.deativate_monitor_button.TabIndex = 61;
            this.deativate_monitor_button.Text = "Deactivate All Monitors";
            this.deativate_monitor_button.UseVisualStyleBackColor = true;
            this.deativate_monitor_button.Click += new System.EventHandler(this.deativate_monitor_button_Click);
            // 
            // deactivate_process_button
            // 
            this.deactivate_process_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deactivate_process_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deactivate_process_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deactivate_process_button.Location = new System.Drawing.Point(99, 26);
            this.deactivate_process_button.Margin = new System.Windows.Forms.Padding(1);
            this.deactivate_process_button.Name = "deactivate_process_button";
            this.deactivate_process_button.Size = new System.Drawing.Size(84, 37);
            this.deactivate_process_button.TabIndex = 62;
            this.deactivate_process_button.Text = "Deactivate Processors";
            this.deactivate_process_button.UseVisualStyleBackColor = true;
            this.deactivate_process_button.Click += new System.EventHandler(this.deactivate_process_button_Click);
            // 
            // reset_button
            // 
            this.reset_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.reset_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reset_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reset_button.Location = new System.Drawing.Point(188, 26);
            this.reset_button.Margin = new System.Windows.Forms.Padding(1);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(84, 37);
            this.reset_button.TabIndex = 63;
            this.reset_button.Text = "Reset Parameters";
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // test_folder_button
            // 
            this.test_folder_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.test_folder_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.test_folder_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.test_folder_button.Location = new System.Drawing.Point(10, 68);
            this.test_folder_button.Margin = new System.Windows.Forms.Padding(1);
            this.test_folder_button.Name = "test_folder_button";
            this.test_folder_button.Size = new System.Drawing.Size(84, 37);
            this.test_folder_button.TabIndex = 64;
            this.test_folder_button.Text = "Test Folders";
            this.test_folder_button.UseVisualStyleBackColor = true;
            this.test_folder_button.Click += new System.EventHandler(this.test_folder_button_Click);
            // 
            // ping_button
            // 
            this.ping_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ping_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ping_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ping_button.Location = new System.Drawing.Point(10, 110);
            this.ping_button.Margin = new System.Windows.Forms.Padding(1);
            this.ping_button.Name = "ping_button";
            this.ping_button.Size = new System.Drawing.Size(84, 25);
            this.ping_button.TabIndex = 65;
            this.ping_button.Text = "Ping Test";
            this.ping_button.UseVisualStyleBackColor = true;
            this.ping_button.Click += new System.EventHandler(this.ping_button_Click);
            // 
            // ping_test_4
            // 
            this.ping_test_4.BackColor = System.Drawing.Color.Black;
            this.ping_test_4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ping_test_4.Location = new System.Drawing.Point(239, 113);
            this.ping_test_4.MaxLength = 3;
            this.ping_test_4.Name = "ping_test_4";
            this.ping_test_4.Size = new System.Drawing.Size(35, 20);
            this.ping_test_4.TabIndex = 4;
            this.ping_test_4.TextChanged += new System.EventHandler(this.ping_test_4_TextChanged_1);
            this.ping_test_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ping_test_4_KeyPress);
            // 
            // ping_test_1
            // 
            this.ping_test_1.BackColor = System.Drawing.Color.Black;
            this.ping_test_1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ping_test_1.Location = new System.Drawing.Point(101, 113);
            this.ping_test_1.MaxLength = 3;
            this.ping_test_1.Name = "ping_test_1";
            this.ping_test_1.Size = new System.Drawing.Size(35, 20);
            this.ping_test_1.TabIndex = 1;
            this.ping_test_1.TextChanged += new System.EventHandler(this.ping_test_1_TextChanged);
            // 
            // ping_test_2
            // 
            this.ping_test_2.BackColor = System.Drawing.Color.Black;
            this.ping_test_2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ping_test_2.Location = new System.Drawing.Point(147, 113);
            this.ping_test_2.MaxLength = 3;
            this.ping_test_2.Name = "ping_test_2";
            this.ping_test_2.Size = new System.Drawing.Size(35, 20);
            this.ping_test_2.TabIndex = 2;
            this.ping_test_2.TextChanged += new System.EventHandler(this.ping_test_2_TextChanged);
            // 
            // ping_test_3
            // 
            this.ping_test_3.BackColor = System.Drawing.Color.Black;
            this.ping_test_3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ping_test_3.Location = new System.Drawing.Point(193, 113);
            this.ping_test_3.MaxLength = 3;
            this.ping_test_3.Name = "ping_test_3";
            this.ping_test_3.Size = new System.Drawing.Size(35, 20);
            this.ping_test_3.TabIndex = 3;
            this.ping_test_3.TextChanged += new System.EventHandler(this.ping_test_3_TextChanged);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox8.Location = new System.Drawing.Point(-290, 261);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(871, 21);
            this.textBox8.TabIndex = 74;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buffer_text);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(10, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 118);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            // 
            // buffer_text
            // 
            this.buffer_text.BackColor = System.Drawing.Color.Black;
            this.buffer_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.buffer_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buffer_text.Location = new System.Drawing.Point(7, 10);
            this.buffer_text.Name = "buffer_text";
            this.buffer_text.ReadOnly = true;
            this.buffer_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.buffer_text.Size = new System.Drawing.Size(253, 103);
            this.buffer_text.TabIndex = 0;
            this.buffer_text.Text = "";
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Black;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text.Location = new System.Drawing.Point(136, 118);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(10, 22);
            this.text.TabIndex = 76;
            this.text.Text = " .";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(183, 120);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(10, 22);
            this.richTextBox1.TabIndex = 77;
            this.richTextBox1.Text = " .";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox2.Location = new System.Drawing.Point(136, 121);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(10, 22);
            this.richTextBox2.TabIndex = 78;
            this.richTextBox2.Text = " .";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.Black;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox3.Location = new System.Drawing.Point(228, 119);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(10, 22);
            this.richTextBox3.TabIndex = 79;
            this.richTextBox3.Text = " .";
            // 
            // Administrative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.text);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.ping_test_3);
            this.Controls.Add(this.ping_test_2);
            this.Controls.Add(this.ping_test_1);
            this.Controls.Add(this.ping_test_4);
            this.Controls.Add(this.ping_button);
            this.Controls.Add(this.test_folder_button);
            this.Controls.Add(this.reset_button);
            this.Controls.Add(this.deactivate_process_button);
            this.Controls.Add(this.deativate_monitor_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.close_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Administrative";
            this.Opacity = 0.85D;
            this.Text = "Administrative Tasks";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button minimize_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button deativate_monitor_button;
        private System.Windows.Forms.Button deactivate_process_button;
        private System.Windows.Forms.Button reset_button;
        private System.Windows.Forms.Button test_folder_button;
        private System.Windows.Forms.Button ping_button;
        private System.Windows.Forms.TextBox ping_test_4;
        private System.Windows.Forms.TextBox ping_test_1;
        private System.Windows.Forms.TextBox ping_test_2;
        private System.Windows.Forms.TextBox ping_test_3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox buffer_text;
        private RichTextBox text;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
    }
}