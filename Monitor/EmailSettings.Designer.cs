using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace Monitor
{
    partial class EmailSettings
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailSettings));
            this.close_button = new System.Windows.Forms.Button();
            this.email_text = new System.Windows.Forms.TextBox();
            this.email_repeat_pw_text = new System.Windows.Forms.TextBox();
            this.smtp_text = new System.Windows.Forms.TextBox();
            this.signature_text = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.save_settings_button = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.email_pw_text = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.test_email_button = new System.Windows.Forms.Button();
            this.smtp_port_text = new System.Windows.Forms.TextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.error_text = new System.Windows.Forms.RichTextBox();
            this.textbox123 = new System.Windows.Forms.RichTextBox();
            this.pop3_text = new System.Windows.Forms.TextBox();
            this.pop3_port_text = new System.Windows.Forms.TextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.close_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.close_button.Location = new System.Drawing.Point(240, 1);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 12;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // email_text
            // 
            this.email_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.email_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.email_text.Location = new System.Drawing.Point(79, 30);
            this.email_text.MaxLength = 100;
            this.email_text.Name = "email_text";
            this.email_text.Size = new System.Drawing.Size(172, 20);
            this.email_text.TabIndex = 1;
            // 
            // email_repeat_pw_text
            // 
            this.email_repeat_pw_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.email_repeat_pw_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.email_repeat_pw_text.Location = new System.Drawing.Point(79, 81);
            this.email_repeat_pw_text.MaxLength = 100;
            this.email_repeat_pw_text.Name = "email_repeat_pw_text";
            this.email_repeat_pw_text.PasswordChar = '*';
            this.email_repeat_pw_text.Size = new System.Drawing.Size(172, 20);
            this.email_repeat_pw_text.TabIndex = 3;
            // 
            // smtp_text
            // 
            this.smtp_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.smtp_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.smtp_text.Location = new System.Drawing.Point(79, 129);
            this.smtp_text.MaxLength = 100;
            this.smtp_text.Name = "smtp_text";
            this.smtp_text.Size = new System.Drawing.Size(100, 20);
            this.smtp_text.TabIndex = 6;
            // 
            // signature_text
            // 
            this.signature_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.signature_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.signature_text.Location = new System.Drawing.Point(79, 155);
            this.signature_text.MaxLength = 100;
            this.signature_text.Multiline = true;
            this.signature_text.Name = "signature_text";
            this.signature_text.Size = new System.Drawing.Size(172, 64);
            this.signature_text.TabIndex = 8;
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Black;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text.Location = new System.Drawing.Point(8, 31);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(71, 22);
            this.text.TabIndex = 14;
            this.text.Text = "Email Address";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(8, 76);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(52, 31);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "Repeat Password";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox2.Location = new System.Drawing.Point(8, 133);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(71, 22);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.Text = "SMTP Client";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.Black;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox3.Location = new System.Drawing.Point(8, 156);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(71, 22);
            this.richTextBox3.TabIndex = 14;
            this.richTextBox3.Text = "Signature";
            // 
            // save_settings_button
            // 
            this.save_settings_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.save_settings_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_settings_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.save_settings_button.Location = new System.Drawing.Point(163, 224);
            this.save_settings_button.Margin = new System.Windows.Forms.Padding(1);
            this.save_settings_button.Name = "save_settings_button";
            this.save_settings_button.Size = new System.Drawing.Size(88, 25);
            this.save_settings_button.TabIndex = 9;
            this.save_settings_button.Text = "Save Settings";
            this.save_settings_button.UseVisualStyleBackColor = true;
            this.save_settings_button.Click += new System.EventHandler(this.save_settings_button_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-12, -9);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(287, 10);
            this.textBox4.TabIndex = 37;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.Location = new System.Drawing.Point(-23, 254);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(287, 10);
            this.textBox5.TabIndex = 38;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox6.Location = new System.Drawing.Point(-10, -3);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(11, 292);
            this.textBox6.TabIndex = 39;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox7.Location = new System.Drawing.Point(257, -11);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(10, 292);
            this.textBox7.TabIndex = 40;
            // 
            // email_pw_text
            // 
            this.email_pw_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.email_pw_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.email_pw_text.Location = new System.Drawing.Point(79, 56);
            this.email_pw_text.MaxLength = 100;
            this.email_pw_text.Name = "email_pw_text";
            this.email_pw_text.PasswordChar = '*';
            this.email_pw_text.Size = new System.Drawing.Size(172, 20);
            this.email_pw_text.TabIndex = 2;
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.Black;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox4.Location = new System.Drawing.Point(8, 56);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox4.Size = new System.Drawing.Size(71, 19);
            this.richTextBox4.TabIndex = 14;
            this.richTextBox4.Text = "Password";
            // 
            // test_email_button
            // 
            this.test_email_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.test_email_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.test_email_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.test_email_button.Location = new System.Drawing.Point(79, 224);
            this.test_email_button.Margin = new System.Windows.Forms.Padding(1);
            this.test_email_button.Name = "test_email_button";
            this.test_email_button.Size = new System.Drawing.Size(79, 25);
            this.test_email_button.TabIndex = 41;
            this.test_email_button.Text = "Test Email";
            this.test_email_button.UseVisualStyleBackColor = true;
            this.test_email_button.Click += new System.EventHandler(this.test_email_button_Click);
            // 
            // smtp_port_text
            // 
            this.smtp_port_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.smtp_port_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.smtp_port_text.Location = new System.Drawing.Point(212, 129);
            this.smtp_port_text.MaxLength = 100;
            this.smtp_port_text.Name = "smtp_port_text";
            this.smtp_port_text.Size = new System.Drawing.Size(39, 20);
            this.smtp_port_text.TabIndex = 7;
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.Black;
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox5.Location = new System.Drawing.Point(185, 133);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(25, 15);
            this.richTextBox5.TabIndex = 43;
            this.richTextBox5.Text = "Port";
            // 
            // error_text
            // 
            this.error_text.BackColor = System.Drawing.Color.Black;
            this.error_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.error_text.Location = new System.Drawing.Point(8, 217);
            this.error_text.Name = "error_text";
            this.error_text.ReadOnly = true;
            this.error_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.error_text.Size = new System.Drawing.Size(65, 32);
            this.error_text.TabIndex = 44;
            this.error_text.Text = "Passwords do not match";
            this.error_text.Visible = false;
            // 
            // textbox123
            // 
            this.textbox123.BackColor = System.Drawing.Color.Black;
            this.textbox123.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox123.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textbox123.Location = new System.Drawing.Point(8, 109);
            this.textbox123.Name = "textbox123";
            this.textbox123.ReadOnly = true;
            this.textbox123.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textbox123.Size = new System.Drawing.Size(71, 22);
            this.textbox123.TabIndex = 46;
            this.textbox123.Text = "POP3 Client";
            // 
            // pop3_text
            // 
            this.pop3_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.pop3_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pop3_text.Location = new System.Drawing.Point(79, 105);
            this.pop3_text.MaxLength = 100;
            this.pop3_text.Name = "pop3_text";
            this.pop3_text.Size = new System.Drawing.Size(100, 20);
            this.pop3_text.TabIndex = 4;
            // 
            // pop3_port_text
            // 
            this.pop3_port_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.pop3_port_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pop3_port_text.Location = new System.Drawing.Point(212, 105);
            this.pop3_port_text.MaxLength = 100;
            this.pop3_port_text.Name = "pop3_port_text";
            this.pop3_port_text.Size = new System.Drawing.Size(39, 20);
            this.pop3_port_text.TabIndex = 5;
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.Color.Black;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox6.Location = new System.Drawing.Point(185, 109);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox6.Size = new System.Drawing.Size(25, 15);
            this.richTextBox6.TabIndex = 43;
            this.richTextBox6.Text = "Port";
            // 
            // EmailSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(258, 255);
            this.Controls.Add(this.textbox123);
            this.Controls.Add(this.pop3_text);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.pop3_port_text);
            this.Controls.Add(this.smtp_port_text);
            this.Controls.Add(this.test_email_button);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.save_settings_button);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.text);
            this.Controls.Add(this.signature_text);
            this.Controls.Add(this.smtp_text);
            this.Controls.Add(this.email_pw_text);
            this.Controls.Add(this.email_repeat_pw_text);
            this.Controls.Add(this.email_text);
            this.Controls.Add(this.close_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmailSettings";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmailSettings";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.TextBox email_text;
        private System.Windows.Forms.TextBox email_repeat_pw_text;
        private System.Windows.Forms.TextBox smtp_text;
        private System.Windows.Forms.TextBox signature_text;
        private System.Windows.Forms.RichTextBox text;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Button save_settings_button;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox email_pw_text;
        private RichTextBox richTextBox4;
        private Button test_email_button;
        private TextBox smtp_port_text;
        private RichTextBox richTextBox5;
        private RichTextBox error_text;
        private RichTextBox textbox123;
        private TextBox pop3_text;
        private TextBox pop3_port_text;
        private RichTextBox richTextBox6;
    }
}