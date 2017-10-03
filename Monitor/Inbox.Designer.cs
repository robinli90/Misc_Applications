using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class Inbox
    {

        #region MOUSE DOWN // Mouse down anywhere to drag
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
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inbox));
            this.minimize_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.message_text = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.inbox_list = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.onebox = new System.Windows.Forms.RichTextBox();
            this.twobox = new System.Windows.Forms.RichTextBox();
            this.threebox = new System.Windows.Forms.RichTextBox();
            this.from_Text = new System.Windows.Forms.RichTextBox();
            this.date_Text = new System.Windows.Forms.RichTextBox();
            this.subject_text = new System.Windows.Forms.RichTextBox();
            this.clearall_button = new System.Windows.Forms.Button();
            this.selectall_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.reply_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(751, -3);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(15, 22);
            this.minimize_button.TabIndex = 29;
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
            this.close_button.Location = new System.Drawing.Point(768, 3);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 28;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-82, -20);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(871, 21);
            this.textBox4.TabIndex = 39;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-47, 373);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(869, 15);
            this.textBox1.TabIndex = 40;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(-9, -18);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(10, 452);
            this.textBox2.TabIndex = 41;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(787, -13);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(10, 417);
            this.textBox3.TabIndex = 42;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.message_text);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(247, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 290);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // message_text
            // 
            this.message_text.BackColor = System.Drawing.Color.Black;
            this.message_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.message_text.Location = new System.Drawing.Point(6, 15);
            this.message_text.Name = "message_text";
            this.message_text.ReadOnly = true;
            this.message_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.message_text.Size = new System.Drawing.Size(523, 269);
            this.message_text.TabIndex = 47;
            this.message_text.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.inbox_list);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(7, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 326);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inbox";
            // 
            // inbox_list
            // 
            this.inbox_list.BackColor = System.Drawing.Color.Black;
            this.inbox_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inbox_list.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.inbox_list.HorizontalScrollbar = true;
            this.inbox_list.Location = new System.Drawing.Point(4, 24);
            this.inbox_list.Name = "inbox_list";
            this.inbox_list.Size = new System.Drawing.Size(226, 300);
            this.inbox_list.TabIndex = 7;
            this.inbox_list.Click += new System.EventHandler(this.inbox_list_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(701, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 25);
            this.button1.TabIndex = 48;
            this.button1.Text = "Refresh Mail";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // onebox
            // 
            this.onebox.BackColor = System.Drawing.Color.Black;
            this.onebox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.onebox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.onebox.Location = new System.Drawing.Point(283, 26);
            this.onebox.Name = "onebox";
            this.onebox.ReadOnly = true;
            this.onebox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.onebox.Size = new System.Drawing.Size(30, 22);
            this.onebox.TabIndex = 45;
            this.onebox.Text = "From:";
            this.onebox.Visible = false;
            // 
            // twobox
            // 
            this.twobox.BackColor = System.Drawing.Color.Black;
            this.twobox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.twobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.twobox.Location = new System.Drawing.Point(248, 43);
            this.twobox.Name = "twobox";
            this.twobox.ReadOnly = true;
            this.twobox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.twobox.Size = new System.Drawing.Size(67, 16);
            this.twobox.TabIndex = 45;
            this.twobox.Text = "Received at:";
            this.twobox.Visible = false;
            // 
            // threebox
            // 
            this.threebox.BackColor = System.Drawing.Color.Black;
            this.threebox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.threebox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.threebox.Location = new System.Drawing.Point(270, 60);
            this.threebox.Name = "threebox";
            this.threebox.ReadOnly = true;
            this.threebox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.threebox.Size = new System.Drawing.Size(47, 22);
            this.threebox.TabIndex = 45;
            this.threebox.Text = "Subject:";
            this.threebox.Visible = false;
            // 
            // from_Text
            // 
            this.from_Text.BackColor = System.Drawing.Color.Black;
            this.from_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.from_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.from_Text.Location = new System.Drawing.Point(321, 26);
            this.from_Text.Name = "from_Text";
            this.from_Text.ReadOnly = true;
            this.from_Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.from_Text.Size = new System.Drawing.Size(374, 22);
            this.from_Text.TabIndex = 46;
            this.from_Text.Text = "";
            // 
            // date_Text
            // 
            this.date_Text.BackColor = System.Drawing.Color.Black;
            this.date_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.date_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.date_Text.Location = new System.Drawing.Point(319, 43);
            this.date_Text.Name = "date_Text";
            this.date_Text.ReadOnly = true;
            this.date_Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.date_Text.Size = new System.Drawing.Size(201, 22);
            this.date_Text.TabIndex = 46;
            this.date_Text.Text = "";
            // 
            // subject_text
            // 
            this.subject_text.BackColor = System.Drawing.Color.Black;
            this.subject_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.subject_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.subject_text.Location = new System.Drawing.Point(319, 60);
            this.subject_text.Name = "subject_text";
            this.subject_text.ReadOnly = true;
            this.subject_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.subject_text.Size = new System.Drawing.Size(389, 22);
            this.subject_text.TabIndex = 46;
            this.subject_text.Text = "";
            // 
            // clearall_button
            // 
            this.clearall_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearall_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearall_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clearall_button.Location = new System.Drawing.Point(83, 342);
            this.clearall_button.Name = "clearall_button";
            this.clearall_button.Size = new System.Drawing.Size(56, 25);
            this.clearall_button.TabIndex = 12;
            this.clearall_button.Text = "Clear All";
            this.clearall_button.UseVisualStyleBackColor = true;
            this.clearall_button.Click += new System.EventHandler(this.clearall_button_Click);
            // 
            // selectall_button
            // 
            this.selectall_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectall_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectall_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectall_button.Location = new System.Drawing.Point(8, 342);
            this.selectall_button.Name = "selectall_button";
            this.selectall_button.Size = new System.Drawing.Size(68, 25);
            this.selectall_button.TabIndex = 11;
            this.selectall_button.Text = "Select All";
            this.selectall_button.UseVisualStyleBackColor = true;
            this.selectall_button.Click += new System.EventHandler(this.selectall_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.delete_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.delete_button.Location = new System.Drawing.Point(145, 342);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(96, 25);
            this.delete_button.TabIndex = 10;
            this.delete_button.Text = "Delete Selected";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // reply_button
            // 
            this.reply_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.reply_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reply_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reply_button.Location = new System.Drawing.Point(701, 54);
            this.reply_button.Name = "reply_button";
            this.reply_button.Size = new System.Drawing.Size(81, 25);
            this.reply_button.TabIndex = 47;
            this.reply_button.Text = "Reply";
            this.reply_button.UseVisualStyleBackColor = true;
            this.reply_button.Click += new System.EventHandler(this.reply_button_Click);
            // 
            // Inbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(788, 374);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reply_button);
            this.Controls.Add(this.clearall_button);
            this.Controls.Add(this.subject_text);
            this.Controls.Add(this.selectall_button);
            this.Controls.Add(this.date_Text);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.from_Text);
            this.Controls.Add(this.threebox);
            this.Controls.Add(this.twobox);
            this.Controls.Add(this.onebox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.close_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inbox";
            this.Text = "Inbox";
            this.Load += new System.EventHandler(this.Inbox_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button minimize_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RichTextBox message_text;
        private RichTextBox onebox;
        private RichTextBox twobox;
        private RichTextBox threebox;
        private RichTextBox from_Text;
        private RichTextBox date_Text;
        private RichTextBox subject_text;
        private CheckedListBox inbox_list;
        private Button clearall_button;
        private Button selectall_button;
        private Button delete_button;
        private Button reply_button;
        private Button button1;
    }
}