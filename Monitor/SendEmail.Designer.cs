using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class SendEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendEmail));
            this.close_button = new System.Windows.Forms.Button();
            this.save_settings_button = new System.Windows.Forms.Button();
            this.minimize_button = new System.Windows.Forms.Button();
            this.send_button = new System.Windows.Forms.Button();
            this.to_text = new System.Windows.Forms.TextBox();
            this.subject_text = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.from_text = new System.Windows.Forms.RichTextBox();
            this.message_text = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.attachment_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.close_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.close_button.Location = new System.Drawing.Point(397, 1);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 11;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // save_settings_button
            // 
            this.save_settings_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.save_settings_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_settings_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.save_settings_button.Location = new System.Drawing.Point(326, 23);
            this.save_settings_button.Margin = new System.Windows.Forms.Padding(1);
            this.save_settings_button.Name = "save_settings_button";
            this.save_settings_button.Size = new System.Drawing.Size(83, 22);
            this.save_settings_button.TabIndex = 12;
            this.save_settings_button.Text = "Email Settings";
            this.save_settings_button.UseVisualStyleBackColor = true;
            this.save_settings_button.Click += new System.EventHandler(this.save_settings_button_Click);
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(380, -5);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(18, 22);
            this.minimize_button.TabIndex = 27;
            this.minimize_button.Text = "_";
            this.minimize_button.UseVisualStyleBackColor = true;
            this.minimize_button.Click += new System.EventHandler(this.minimize_button_Click);
            // 
            // send_button
            // 
            this.send_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.send_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.send_button.Location = new System.Drawing.Point(179, 23);
            this.send_button.Margin = new System.Windows.Forms.Padding(1);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(58, 22);
            this.send_button.TabIndex = 28;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // to_text
            // 
            this.to_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.to_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.to_text.Location = new System.Drawing.Point(53, 51);
            this.to_text.MaxLength = 100;
            this.to_text.Name = "to_text";
            this.to_text.Size = new System.Drawing.Size(356, 20);
            this.to_text.TabIndex = 2;
            // 
            // subject_text
            // 
            this.subject_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.subject_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.subject_text.Location = new System.Drawing.Point(53, 91);
            this.subject_text.MaxLength = 100;
            this.subject_text.Name = "subject_text";
            this.subject_text.Size = new System.Drawing.Size(356, 20);
            this.subject_text.TabIndex = 3;
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Black;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text.Location = new System.Drawing.Point(22, 55);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(26, 18);
            this.text.TabIndex = 30;
            this.text.Text = "To...";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(8, 93);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(39, 18);
            this.richTextBox1.TabIndex = 31;
            this.richTextBox1.Text = "Subject";
            // 
            // from_text
            // 
            this.from_text.BackColor = System.Drawing.Color.Black;
            this.from_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.from_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.from_text.Location = new System.Drawing.Point(13, 32);
            this.from_text.Name = "from_text";
            this.from_text.ReadOnly = true;
            this.from_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.from_text.Size = new System.Drawing.Size(161, 18);
            this.from_text.TabIndex = 1;
            this.from_text.Text = "From...   rli@etsdies.com";
            // 
            // message_text
            // 
            this.message_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.message_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.message_text.Location = new System.Drawing.Point(8, 117);
            this.message_text.MaxLength = 10000;
            this.message_text.Multiline = true;
            this.message_text.Name = "message_text";
            this.message_text.Size = new System.Drawing.Size(401, 222);
            this.message_text.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-13, -13);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(463, 14);
            this.textBox4.TabIndex = 38;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(-17, 350);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(463, 14);
            this.textBox3.TabIndex = 39;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.Location = new System.Drawing.Point(-10, -5);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(10, 378);
            this.textBox5.TabIndex = 40;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox6.Location = new System.Drawing.Point(415, -7);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(10, 378);
            this.textBox6.TabIndex = 41;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.richTextBox2.Location = new System.Drawing.Point(215, 72);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(199, 15);
            this.richTextBox2.TabIndex = 42;
            this.richTextBox2.Text = "*Seperate multiple recipients with spaces";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-9, -12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 378);
            this.textBox1.TabIndex = 43;
            // 
            // attachment_button
            // 
            this.attachment_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.attachment_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachment_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.attachment_button.Location = new System.Drawing.Point(242, 23);
            this.attachment_button.Margin = new System.Windows.Forms.Padding(1);
            this.attachment_button.Name = "attachment_button";
            this.attachment_button.Size = new System.Drawing.Size(79, 22);
            this.attachment_button.TabIndex = 44;
            this.attachment_button.Text = "Attachments";
            this.attachment_button.UseVisualStyleBackColor = true;
            this.attachment_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // SendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(416, 351);
            this.Controls.Add(this.attachment_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.message_text);
            this.Controls.Add(this.from_text);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.text);
            this.Controls.Add(this.subject_text);
            this.Controls.Add(this.to_text);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.save_settings_button);
            this.Controls.Add(this.close_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendEmail";
            this.Text = "SendEmail";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close_button;
        private Button save_settings_button;
        private Button minimize_button;
        private Button send_button;
        private TextBox to_text;
        private TextBox subject_text;
        private RichTextBox text;
        private RichTextBox richTextBox1;
        private RichTextBox from_text;
        private TextBox message_text;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox5;
        private TextBox textBox6;
        private RichTextBox richTextBox2;
        private TextBox textBox1;
        private Button attachment_button;
    }
}