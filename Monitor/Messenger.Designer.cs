using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class messenger
    {

        #region SETUP FLASHING WINDOW
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        public enum FlashWindow : uint
            {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state. 
            /// </summary>    
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption 
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button. 
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground. 
            /// </summary>
            FLASHW_TIMERNOFG = 12
            }
        /// <summary>
        /// Flashes a window
        /// </summary>
        /// <param name="hWnd">The handle to the window to flash</param>
        /// <returns>whether or not the window needed flashing</returns>
        public static bool FlashWindowEx(Form frm)
        {
            IntPtr hWnd = frm.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = 3 | 12;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }

        // All this, acll FlashWindowEx(Form);
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(messenger));
            this.lastmsgrec_box = new System.Windows.Forms.RichTextBox();
            this.conversations = new System.Windows.Forms.GroupBox();
            this.conversation_table = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tobox = new System.Windows.Forms.TextBox();
            this.frombox = new System.Windows.Forms.TextBox();
            this.msg_chat_box = new System.Windows.Forms.TextBox();
            this.msg_clear_button = new System.Windows.Forms.Button();
            this.msg_send_button = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.ComboBox();
            this.minimize_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.txtboxsnd = new System.Windows.Forms.RichTextBox();
            this.select_user_button = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.active_text = new System.Windows.Forms.RichTextBox();
            this.conversations.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lastmsgrec_box
            // 
            this.lastmsgrec_box.BackColor = System.Drawing.Color.Black;
            this.lastmsgrec_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastmsgrec_box.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lastmsgrec_box.Location = new System.Drawing.Point(169, 384);
            this.lastmsgrec_box.Name = "lastmsgrec_box";
            this.lastmsgrec_box.ReadOnly = true;
            this.lastmsgrec_box.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lastmsgrec_box.Size = new System.Drawing.Size(282, 22);
            this.lastmsgrec_box.TabIndex = 18;
            this.lastmsgrec_box.Text = "";
            // 
            // conversations
            // 
            this.conversations.Controls.Add(this.conversation_table);
            this.conversations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.conversations.Location = new System.Drawing.Point(5, 50);
            this.conversations.Name = "conversations";
            this.conversations.Size = new System.Drawing.Size(160, 348);
            this.conversations.TabIndex = 19;
            this.conversations.TabStop = false;
            this.conversations.Text = "Conversations";
            // 
            // conversation_table
            // 
            this.conversation_table.AutoScroll = true;
            this.conversation_table.ColumnCount = 1;
            this.conversation_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.conversation_table.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.conversation_table.Location = new System.Drawing.Point(8, 17);
            this.conversation_table.Name = "conversation_table";
            this.conversation_table.Size = new System.Drawing.Size(146, 325);
            this.conversation_table.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tobox);
            this.groupBox1.Controls.Add(this.frombox);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(169, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 270);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // tobox
            // 
            this.tobox.BackColor = System.Drawing.Color.Black;
            this.tobox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tobox.Location = new System.Drawing.Point(178, 19);
            this.tobox.Multiline = true;
            this.tobox.Name = "tobox";
            this.tobox.ReadOnly = true;
            this.tobox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tobox.Size = new System.Drawing.Size(156, 245);
            this.tobox.TabIndex = 2;
            // 
            // frombox
            // 
            this.frombox.BackColor = System.Drawing.Color.Black;
            this.frombox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.frombox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.frombox.Location = new System.Drawing.Point(15, 19);
            this.frombox.Multiline = true;
            this.frombox.Name = "frombox";
            this.frombox.ReadOnly = true;
            this.frombox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.frombox.Size = new System.Drawing.Size(163, 245);
            this.frombox.TabIndex = 1;
            // 
            // msg_chat_box
            // 
            this.msg_chat_box.BackColor = System.Drawing.Color.Black;
            this.msg_chat_box.Enabled = false;
            this.msg_chat_box.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msg_chat_box.Location = new System.Drawing.Point(169, 325);
            this.msg_chat_box.MaxLength = 103;
            this.msg_chat_box.Multiline = true;
            this.msg_chat_box.Name = "msg_chat_box";
            this.msg_chat_box.Size = new System.Drawing.Size(282, 54);
            this.msg_chat_box.TabIndex = 20;
            this.msg_chat_box.Click += new System.EventHandler(this.msg_chat_box_Click);
            this.msg_chat_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.msg_chat_box_KeyPress);
            // 
            // msg_clear_button
            // 
            this.msg_clear_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.msg_clear_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.msg_clear_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msg_clear_button.Location = new System.Drawing.Point(455, 325);
            this.msg_clear_button.Margin = new System.Windows.Forms.Padding(1);
            this.msg_clear_button.Name = "msg_clear_button";
            this.msg_clear_button.Size = new System.Drawing.Size(69, 25);
            this.msg_clear_button.TabIndex = 21;
            this.msg_clear_button.Text = "Clear";
            this.msg_clear_button.UseVisualStyleBackColor = true;
            this.msg_clear_button.Click += new System.EventHandler(this.msg_clear_button_Click);
            // 
            // msg_send_button
            // 
            this.msg_send_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.msg_send_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.msg_send_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msg_send_button.Location = new System.Drawing.Point(455, 354);
            this.msg_send_button.Margin = new System.Windows.Forms.Padding(1);
            this.msg_send_button.Name = "msg_send_button";
            this.msg_send_button.Size = new System.Drawing.Size(69, 25);
            this.msg_send_button.TabIndex = 21;
            this.msg_send_button.Text = "Send";
            this.msg_send_button.UseVisualStyleBackColor = true;
            this.msg_send_button.Click += new System.EventHandler(this.msg_send_button_Click);
            // 
            // nameBox
            // 
            this.nameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.nameBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.nameBox.BackColor = System.Drawing.Color.Black;
            this.nameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nameBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nameBox.FormattingEnabled = true;
            this.nameBox.Location = new System.Drawing.Point(101, 25);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(347, 21);
            this.nameBox.TabIndex = 23;
            this.nameBox.Click += new System.EventHandler(this.nameBox_Click);
            this.nameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress2);
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(495, -5);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(18, 22);
            this.minimize_button.TabIndex = 26;
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
            this.close_button.Location = new System.Drawing.Point(513, 1);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 25;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // txtboxsnd
            // 
            this.txtboxsnd.BackColor = System.Drawing.Color.Black;
            this.txtboxsnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxsnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtboxsnd.Location = new System.Drawing.Point(8, 28);
            this.txtboxsnd.Name = "txtboxsnd";
            this.txtboxsnd.ReadOnly = true;
            this.txtboxsnd.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtboxsnd.Size = new System.Drawing.Size(105, 22);
            this.txtboxsnd.TabIndex = 18;
            this.txtboxsnd.Text = "Send message to:";
            // 
            // select_user_button
            // 
            this.select_user_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.select_user_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_user_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.select_user_button.Location = new System.Drawing.Point(455, 25);
            this.select_user_button.Margin = new System.Windows.Forms.Padding(1);
            this.select_user_button.Name = "select_user_button";
            this.select_user_button.Size = new System.Drawing.Size(68, 21);
            this.select_user_button.TabIndex = 24;
            this.select_user_button.Text = "Select";
            this.select_user_button.UseVisualStyleBackColor = true;
            this.select_user_button.Click += new System.EventHandler(this.select_user_button_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(-4, -10);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(547, 11);
            this.textBox3.TabIndex = 31;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-12, 486);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(547, 11);
            this.textBox1.TabIndex = 32;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(-14, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(15, 488);
            this.textBox2.TabIndex = 32;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(528, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(15, 488);
            this.textBox4.TabIndex = 33;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox5.Location = new System.Drawing.Point(-18, 402);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(547, 11);
            this.textBox5.TabIndex = 34;
            // 
            // active_text
            // 
            this.active_text.BackColor = System.Drawing.Color.Black;
            this.active_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.active_text.Location = new System.Drawing.Point(475, 384);
            this.active_text.Name = "active_text";
            this.active_text.ReadOnly = true;
            this.active_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.active_text.Size = new System.Drawing.Size(48, 14);
            this.active_text.TabIndex = 35;
            this.active_text.Text = "";
            // 
            // messenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(529, 403);
            this.Controls.Add(this.active_text);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.select_user_button);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.msg_send_button);
            this.Controls.Add(this.msg_clear_button);
            this.Controls.Add(this.msg_chat_box);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.conversations);
            this.Controls.Add(this.txtboxsnd);
            this.Controls.Add(this.lastmsgrec_box);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "messenger";
            this.Text = "Messenger";
            this.Activated += new System.EventHandler(this.this_OnActivated);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.conversations.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox lastmsgrec_box;
        private System.Windows.Forms.GroupBox conversations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox msg_chat_box;
        private System.Windows.Forms.Button msg_clear_button;
        private System.Windows.Forms.Button msg_send_button;
        private System.Windows.Forms.ComboBox nameBox;
        private System.Windows.Forms.TableLayoutPanel conversation_table;
        private Button minimize_button;
        private Button close_button;
        private TextBox tobox;
        private TextBox frombox;
        private RichTextBox txtboxsnd;
        private Button select_user_button;
        private TextBox textBox3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox4;
        private TextBox textBox5;
        private RichTextBox active_text;

    }
}