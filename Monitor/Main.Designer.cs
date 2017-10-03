using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.text1 = new System.Windows.Forms.RichTextBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.text2 = new System.Windows.Forms.RichTextBox();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.text3 = new System.Windows.Forms.RichTextBox();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.text4 = new System.Windows.Forms.RichTextBox();
            this.progressBar5 = new System.Windows.Forms.ProgressBar();
            this.text5 = new System.Windows.Forms.RichTextBox();
            this.progressBar6 = new System.Windows.Forms.ProgressBar();
            this.text6 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.print_list = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SO_text = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.print_button = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.retrieve_button = new System.Windows.Forms.Button();
            this.selectall_button = new System.Windows.Forms.Button();
            this.clearall_button = new System.Windows.Forms.Button();
            this.designerBox = new System.Windows.Forms.RichTextBox();
            this.numberOfFilesText = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.shoptrack_button = new System.Windows.Forms.Button();
            this.google_search_box = new System.Windows.Forms.TextBox();
            this.search_button = new System.Windows.Forms.Button();
            this.search_clear_button = new System.Windows.Forms.Button();
            this.calc_button = new System.Windows.Forms.Button();
            this.progressBar7 = new System.Windows.Forms.ProgressBar();
            this.text7 = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.g_button = new System.Windows.Forms.Button();
            this.steel_button = new System.Windows.Forms.Button();
            this.messenger_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.minimize_button = new System.Windows.Forms.Button();
            this.progressBar8 = new System.Windows.Forms.ProgressBar();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.notifications = new System.Windows.Forms.CheckBox();
            this.button9 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.progressBar9 = new System.Windows.Forms.ProgressBar();
            this.button10 = new System.Windows.Forms.Button();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.progressBar10 = new System.Windows.Forms.ProgressBar();
            this.send_email_button = new System.Windows.Forms.Button();
            this.inbox_button = new System.Windows.Forms.Button();
            this.page_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bolster_autolathe_button = new System.Windows.Forms.Button();
            this.BLS_AL_active_text = new System.Windows.Forms.RichTextBox();
            this.cad_print_button = new System.Windows.Forms.Button();
            this.CAD_Print_active = new System.Windows.Forms.RichTextBox();
            this.cad_print_active_text = new System.Windows.Forms.RichTextBox();
            this.turn_check_active = new System.Windows.Forms.RichTextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.turn_check_button = new System.Windows.Forms.Button();
            this.task_track_button = new System.Windows.Forms.Button();
            this.task_tracker_active = new System.Windows.Forms.RichTextBox();
            this.on_hold_active = new System.Windows.Forms.RichTextBox();
            this.on_hold_button = new System.Windows.Forms.Button();
            this.admin_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.ping1_text = new System.Windows.Forms.RichTextBox();
            this.avg_ping = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.cpu_text = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.stat_button = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar1.Location = new System.Drawing.Point(14, 196);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(80, 10);
            this.progressBar1.TabIndex = 0;
            // 
            // text1
            // 
            this.text1.BackColor = System.Drawing.Color.Black;
            this.text1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text1.Location = new System.Drawing.Point(100, 195);
            this.text1.Name = "text1";
            this.text1.ReadOnly = true;
            this.text1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text1.Size = new System.Drawing.Size(84, 22);
            this.text1.TabIndex = 1;
            this.text1.Text = "\\\\10.0.0.8\\shopdata\\ACCT\\CMDS";
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar2.Location = new System.Drawing.Point(14, 212);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(80, 10);
            this.progressBar2.TabIndex = 0;
            // 
            // text2
            // 
            this.text2.BackColor = System.Drawing.Color.Black;
            this.text2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text2.Location = new System.Drawing.Point(100, 210);
            this.text2.Name = "text2";
            this.text2.ReadOnly = true;
            this.text2.Size = new System.Drawing.Size(58, 22);
            this.text2.TabIndex = 1;
            this.text2.Text = "\\\\10.0.0.8\\shopdata\\LDATA";
            // 
            // progressBar3
            // 
            this.progressBar3.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar3.Location = new System.Drawing.Point(14, 228);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(80, 10);
            this.progressBar3.TabIndex = 0;
            // 
            // text3
            // 
            this.text3.BackColor = System.Drawing.Color.Black;
            this.text3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text3.Location = new System.Drawing.Point(100, 226);
            this.text3.Name = "text3";
            this.text3.ReadOnly = true;
            this.text3.Size = new System.Drawing.Size(100, 22);
            this.text3.TabIndex = 1;
            this.text3.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\CHECK";
            // 
            // progressBar4
            // 
            this.progressBar4.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar4.Location = new System.Drawing.Point(14, 244);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(80, 10);
            this.progressBar4.TabIndex = 0;
            // 
            // text4
            // 
            this.text4.BackColor = System.Drawing.Color.Black;
            this.text4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text4.Location = new System.Drawing.Point(100, 242);
            this.text4.Name = "text4";
            this.text4.ReadOnly = true;
            this.text4.Size = new System.Drawing.Size(84, 22);
            this.text4.TabIndex = 1;
            this.text4.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\BOL";
            // 
            // progressBar5
            // 
            this.progressBar5.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar5.Location = new System.Drawing.Point(14, 260);
            this.progressBar5.Name = "progressBar5";
            this.progressBar5.Size = new System.Drawing.Size(80, 10);
            this.progressBar5.TabIndex = 0;
            // 
            // text5
            // 
            this.text5.BackColor = System.Drawing.Color.Black;
            this.text5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text5.Location = new System.Drawing.Point(100, 258);
            this.text5.Name = "text5";
            this.text5.ReadOnly = true;
            this.text5.Size = new System.Drawing.Size(190, 22);
            this.text5.TabIndex = 1;
            this.text5.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\PLOT_PRINT";
            // 
            // progressBar6
            // 
            this.progressBar6.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar6.Location = new System.Drawing.Point(14, 276);
            this.progressBar6.Name = "progressBar6";
            this.progressBar6.Size = new System.Drawing.Size(80, 10);
            this.progressBar6.TabIndex = 2;
            // 
            // text6
            // 
            this.text6.BackColor = System.Drawing.Color.Black;
            this.text6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text6.Location = new System.Drawing.Point(100, 274);
            this.text6.Name = "text6";
            this.text6.ReadOnly = true;
            this.text6.Size = new System.Drawing.Size(164, 22);
            this.text6.TabIndex = 3;
            this.text6.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\A4_PRINT";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(2, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(10, 10);
            this.button1.TabIndex = 4;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(2, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(10, 10);
            this.button2.TabIndex = 4;
            this.button2.TabStop = false;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(2, 228);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(10, 10);
            this.button3.TabIndex = 4;
            this.button3.TabStop = false;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(2, 244);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(10, 10);
            this.button4.TabIndex = 4;
            this.button4.TabStop = false;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(2, 261);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(10, 10);
            this.button5.TabIndex = 4;
            this.button5.TabStop = false;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(2, 277);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(10, 10);
            this.button6.TabIndex = 4;
            this.button6.TabStop = false;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // print_list
            // 
            this.print_list.BackColor = System.Drawing.Color.Black;
            this.print_list.CheckOnClick = true;
            this.print_list.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.print_list.HorizontalScrollbar = true;
            this.print_list.Location = new System.Drawing.Point(12, 405);
            this.print_list.Name = "print_list";
            this.print_list.Size = new System.Drawing.Size(252, 49);
            this.print_list.TabIndex = 6;
            this.print_list.SelectedIndexChanged += new System.EventHandler(this.print_list_SelectedIndexChanged_1);
            this.print_list.SelectedValueChanged += new System.EventHandler(this.print_list_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label1.Location = new System.Drawing.Point(2, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Monitoring Folders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label2.Location = new System.Drawing.Point(2, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Print Options";
            // 
            // SO_text
            // 
            this.SO_text.BackColor = System.Drawing.Color.Black;
            this.SO_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SO_text.Location = new System.Drawing.Point(87, 377);
            this.SO_text.MaxLength = 6;
            this.SO_text.Name = "SO_text";
            this.SO_text.Size = new System.Drawing.Size(56, 20);
            this.SO_text.TabIndex = 8;
            this.SO_text.TextChanged += new System.EventHandler(this.SO_text_TextChanged);
            this.SO_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Black;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text.Location = new System.Drawing.Point(11, 380);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(80, 22);
            this.text.TabIndex = 1;
            this.text.Text = "Shop Order #:";
            // 
            // print_button
            // 
            this.print_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.print_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.print_button.Location = new System.Drawing.Point(174, 475);
            this.print_button.Name = "print_button";
            this.print_button.Size = new System.Drawing.Size(90, 25);
            this.print_button.TabIndex = 7;
            this.print_button.Text = "Print Selected";
            this.print_button.UseVisualStyleBackColor = true;
            this.print_button.Click += new System.EventHandler(this.print_button_Click);
            // 
            // clear_button
            // 
            this.clear_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clear_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clear_button.Location = new System.Drawing.Point(149, 376);
            this.clear_button.Margin = new System.Windows.Forms.Padding(1);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(51, 22);
            this.clear_button.TabIndex = 7;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // retrieve_button
            // 
            this.retrieve_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.retrieve_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retrieve_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.retrieve_button.Location = new System.Drawing.Point(206, 376);
            this.retrieve_button.Margin = new System.Windows.Forms.Padding(1);
            this.retrieve_button.Name = "retrieve_button";
            this.retrieve_button.Size = new System.Drawing.Size(58, 22);
            this.retrieve_button.TabIndex = 7;
            this.retrieve_button.Text = "Retrieve";
            this.retrieve_button.UseVisualStyleBackColor = true;
            this.retrieve_button.Click += new System.EventHandler(this.retrieve_button_Click);
            // 
            // selectall_button
            // 
            this.selectall_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectall_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectall_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectall_button.Location = new System.Drawing.Point(12, 475);
            this.selectall_button.Name = "selectall_button";
            this.selectall_button.Size = new System.Drawing.Size(75, 25);
            this.selectall_button.TabIndex = 9;
            this.selectall_button.Text = "Select All";
            this.selectall_button.UseVisualStyleBackColor = true;
            this.selectall_button.Click += new System.EventHandler(this.selectall_button_Click);
            // 
            // clearall_button
            // 
            this.clearall_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearall_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearall_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clearall_button.Location = new System.Drawing.Point(93, 475);
            this.clearall_button.Name = "clearall_button";
            this.clearall_button.Size = new System.Drawing.Size(75, 25);
            this.clearall_button.TabIndex = 9;
            this.clearall_button.Text = "Clear All";
            this.clearall_button.UseVisualStyleBackColor = true;
            this.clearall_button.Click += new System.EventHandler(this.clearall_button_Click);
            // 
            // designerBox
            // 
            this.designerBox.BackColor = System.Drawing.Color.Black;
            this.designerBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.designerBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.designerBox.Location = new System.Drawing.Point(12, 457);
            this.designerBox.Name = "designerBox";
            this.designerBox.Size = new System.Drawing.Size(135, 22);
            this.designerBox.TabIndex = 1;
            this.designerBox.Text = "";
            // 
            // numberOfFilesText
            // 
            this.numberOfFilesText.BackColor = System.Drawing.Color.Black;
            this.numberOfFilesText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numberOfFilesText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numberOfFilesText.Location = new System.Drawing.Point(186, 457);
            this.numberOfFilesText.Name = "numberOfFilesText";
            this.numberOfFilesText.Size = new System.Drawing.Size(135, 22);
            this.numberOfFilesText.TabIndex = 1;
            this.numberOfFilesText.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quick Task";
            // 
            // shoptrack_button
            // 
            this.shoptrack_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shoptrack_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shoptrack_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shoptrack_button.Location = new System.Drawing.Point(101, 55);
            this.shoptrack_button.Margin = new System.Windows.Forms.Padding(0);
            this.shoptrack_button.Name = "shoptrack_button";
            this.shoptrack_button.Size = new System.Drawing.Size(72, 23);
            this.shoptrack_button.TabIndex = 7;
            this.shoptrack_button.Text = "Shoptrack";
            this.shoptrack_button.UseVisualStyleBackColor = true;
            this.shoptrack_button.Click += new System.EventHandler(this.shoptrack_button_Click);
            // 
            // google_search_box
            // 
            this.google_search_box.BackColor = System.Drawing.Color.Black;
            this.google_search_box.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.google_search_box.Location = new System.Drawing.Point(12, 29);
            this.google_search_box.MaxLength = 100;
            this.google_search_box.Name = "google_search_box";
            this.google_search_box.Size = new System.Drawing.Size(199, 20);
            this.google_search_box.TabIndex = 8;
            this.google_search_box.TextChanged += new System.EventHandler(this.google_search_box_TextChanged);
            this.google_search_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.google_search_box_KeyPress);
            // 
            // search_button
            // 
            this.search_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.search_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.search_button.Location = new System.Drawing.Point(12, 55);
            this.search_button.Margin = new System.Windows.Forms.Padding(1);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(82, 23);
            this.search_button.TabIndex = 7;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // search_clear_button
            // 
            this.search_clear_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.search_clear_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_clear_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.search_clear_button.Location = new System.Drawing.Point(216, 28);
            this.search_clear_button.Margin = new System.Windows.Forms.Padding(1);
            this.search_clear_button.Name = "search_clear_button";
            this.search_clear_button.Size = new System.Drawing.Size(48, 21);
            this.search_clear_button.TabIndex = 7;
            this.search_clear_button.Text = "Clear";
            this.search_clear_button.UseVisualStyleBackColor = true;
            this.search_clear_button.Click += new System.EventHandler(this.search_clear_button_Click);
            // 
            // calc_button
            // 
            this.calc_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.calc_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calc_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calc_button.Location = new System.Drawing.Point(180, 55);
            this.calc_button.Margin = new System.Windows.Forms.Padding(1);
            this.calc_button.Name = "calc_button";
            this.calc_button.Size = new System.Drawing.Size(84, 23);
            this.calc_button.TabIndex = 7;
            this.calc_button.Text = "Calculator";
            this.calc_button.UseVisualStyleBackColor = true;
            this.calc_button.Click += new System.EventHandler(this.calc_button_Click);
            // 
            // progressBar7
            // 
            this.progressBar7.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar7.Location = new System.Drawing.Point(14, 291);
            this.progressBar7.Name = "progressBar7";
            this.progressBar7.Size = new System.Drawing.Size(80, 10);
            this.progressBar7.TabIndex = 2;
            // 
            // text7
            // 
            this.text7.BackColor = System.Drawing.Color.Black;
            this.text7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text7.Location = new System.Drawing.Point(100, 289);
            this.text7.Name = "text7";
            this.text7.ReadOnly = true;
            this.text7.Size = new System.Drawing.Size(127, 22);
            this.text7.TabIndex = 3;
            this.text7.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\LATHEDWG";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Location = new System.Drawing.Point(2, 292);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(10, 10);
            this.button7.TabIndex = 4;
            this.button7.TabStop = false;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // g_button
            // 
            this.g_button.BackColor = System.Drawing.Color.Transparent;
            this.g_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.g_button.FlatAppearance.BorderSize = 0;
            this.g_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.g_button.ForeColor = System.Drawing.Color.Black;
            this.g_button.Location = new System.Drawing.Point(-11, -42);
            this.g_button.Name = "g_button";
            this.g_button.Size = new System.Drawing.Size(10, 10);
            this.g_button.TabIndex = 4;
            this.g_button.TabStop = false;
            this.g_button.UseVisualStyleBackColor = false;
            this.g_button.Click += new System.EventHandler(this.g_button_Click);
            // 
            // steel_button
            // 
            this.steel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.steel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steel_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.steel_button.Location = new System.Drawing.Point(12, 84);
            this.steel_button.Margin = new System.Windows.Forms.Padding(1);
            this.steel_button.Name = "steel_button";
            this.steel_button.Size = new System.Drawing.Size(82, 23);
            this.steel_button.TabIndex = 7;
            this.steel_button.Text = "Steel In";
            this.steel_button.UseVisualStyleBackColor = true;
            this.steel_button.Click += new System.EventHandler(this.steel_button_Click);
            // 
            // messenger_button
            // 
            this.messenger_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.messenger_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.messenger_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.messenger_button.Location = new System.Drawing.Point(101, 84);
            this.messenger_button.Margin = new System.Windows.Forms.Padding(0);
            this.messenger_button.Name = "messenger_button";
            this.messenger_button.Size = new System.Drawing.Size(72, 23);
            this.messenger_button.TabIndex = 7;
            this.messenger_button.Text = "Messenger";
            this.messenger_button.UseVisualStyleBackColor = true;
            this.messenger_button.Click += new System.EventHandler(this.messenger_button_Click);
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.close_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.close_button.Location = new System.Drawing.Point(260, 1);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 10;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(242, -5);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(18, 22);
            this.minimize_button.TabIndex = 11;
            this.minimize_button.Text = "_";
            this.minimize_button.UseVisualStyleBackColor = true;
            this.minimize_button.Click += new System.EventHandler(this.minimize_button_Click);
            // 
            // progressBar8
            // 
            this.progressBar8.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar8.Location = new System.Drawing.Point(14, 306);
            this.progressBar8.Name = "progressBar8";
            this.progressBar8.Size = new System.Drawing.Size(80, 10);
            this.progressBar8.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(100, 305);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(164, 22);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "\\\\10.0.0.8\\shopdata\\DEVELOPMENT\\ROBIN";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button8.Location = new System.Drawing.Point(2, 307);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(10, 10);
            this.button8.TabIndex = 4;
            this.button8.TabStop = false;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(0, 824);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(286, 13);
            this.textBox3.TabIndex = 35;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-1, -9);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(286, 10);
            this.textBox4.TabIndex = 36;
            // 
            // notifications
            // 
            this.notifications.AutoSize = true;
            this.notifications.Checked = true;
            this.notifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.notifications.Location = new System.Drawing.Point(182, 144);
            this.notifications.Name = "notifications";
            this.notifications.Size = new System.Drawing.Size(84, 17);
            this.notifications.TabIndex = 37;
            this.notifications.Text = "Notifications";
            this.notifications.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.Black;
            this.button9.Location = new System.Drawing.Point(2, 323);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(10, 10);
            this.button9.TabIndex = 40;
            this.button9.TabStop = false;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox2.Location = new System.Drawing.Point(100, 321);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(160, 22);
            this.richTextBox2.TabIndex = 39;
            this.richTextBox2.Text = "\\\\10.0.0.8\\shopdata\\LDATA\\COL\\DATACOPY";
            // 
            // progressBar9
            // 
            this.progressBar9.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar9.Location = new System.Drawing.Point(14, 322);
            this.progressBar9.Name = "progressBar9";
            this.progressBar9.Size = new System.Drawing.Size(80, 10);
            this.progressBar9.TabIndex = 38;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Transparent;
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.ForeColor = System.Drawing.Color.Black;
            this.button10.Location = new System.Drawing.Point(2, 339);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(10, 10);
            this.button10.TabIndex = 43;
            this.button10.TabStop = false;
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.Black;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox3.Location = new System.Drawing.Point(100, 337);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(174, 22);
            this.richTextBox3.TabIndex = 42;
            this.richTextBox3.Text = "SDRIVE\\LDATA-COLOMBIA";
            // 
            // progressBar10
            // 
            this.progressBar10.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar10.Location = new System.Drawing.Point(14, 338);
            this.progressBar10.Name = "progressBar10";
            this.progressBar10.Size = new System.Drawing.Size(80, 10);
            this.progressBar10.TabIndex = 41;
            // 
            // send_email_button
            // 
            this.send_email_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.send_email_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send_email_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.send_email_button.Location = new System.Drawing.Point(12, 113);
            this.send_email_button.Margin = new System.Windows.Forms.Padding(1);
            this.send_email_button.Name = "send_email_button";
            this.send_email_button.Size = new System.Drawing.Size(82, 23);
            this.send_email_button.TabIndex = 44;
            this.send_email_button.Text = "Send Email";
            this.send_email_button.UseVisualStyleBackColor = true;
            this.send_email_button.Click += new System.EventHandler(this.send_email_button_Click);
            // 
            // inbox_button
            // 
            this.inbox_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.inbox_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inbox_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.inbox_button.Location = new System.Drawing.Point(101, 113);
            this.inbox_button.Margin = new System.Windows.Forms.Padding(0);
            this.inbox_button.Name = "inbox_button";
            this.inbox_button.Size = new System.Drawing.Size(72, 23);
            this.inbox_button.TabIndex = 45;
            this.inbox_button.Text = "Inbox";
            this.inbox_button.UseVisualStyleBackColor = true;
            this.inbox_button.Click += new System.EventHandler(this.inbox_button_Click);
            // 
            // page_button
            // 
            this.page_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.page_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.page_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.page_button.Location = new System.Drawing.Point(180, 84);
            this.page_button.Margin = new System.Windows.Forms.Padding(1);
            this.page_button.Name = "page_button";
            this.page_button.Size = new System.Drawing.Size(84, 23);
            this.page_button.TabIndex = 46;
            this.page_button.Text = "Quick Page";
            this.page_button.UseVisualStyleBackColor = true;
            this.page_button.Click += new System.EventHandler(this.page_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label4.Location = new System.Drawing.Point(3, 506);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "Production Tasks";
            // 
            // bolster_autolathe_button
            // 
            this.bolster_autolathe_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bolster_autolathe_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bolster_autolathe_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bolster_autolathe_button.Location = new System.Drawing.Point(11, 531);
            this.bolster_autolathe_button.Name = "bolster_autolathe_button";
            this.bolster_autolathe_button.Size = new System.Drawing.Size(118, 23);
            this.bolster_autolathe_button.TabIndex = 48;
            this.bolster_autolathe_button.Text = "Bolster Auto Lathe";
            this.bolster_autolathe_button.UseVisualStyleBackColor = true;
            this.bolster_autolathe_button.Click += new System.EventHandler(this.bolster_autolathe_button_Click);
            // 
            // BLS_AL_active_text
            // 
            this.BLS_AL_active_text.BackColor = System.Drawing.Color.Black;
            this.BLS_AL_active_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BLS_AL_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BLS_AL_active_text.Location = new System.Drawing.Point(136, 535);
            this.BLS_AL_active_text.Name = "BLS_AL_active_text";
            this.BLS_AL_active_text.ReadOnly = true;
            this.BLS_AL_active_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.BLS_AL_active_text.Size = new System.Drawing.Size(84, 17);
            this.BLS_AL_active_text.TabIndex = 49;
            this.BLS_AL_active_text.Text = "";
            // 
            // cad_print_button
            // 
            this.cad_print_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cad_print_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cad_print_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cad_print_button.Location = new System.Drawing.Point(11, 560);
            this.cad_print_button.Name = "cad_print_button";
            this.cad_print_button.Size = new System.Drawing.Size(118, 23);
            this.cad_print_button.TabIndex = 50;
            this.cad_print_button.Text = "CRV Generator";
            this.cad_print_button.UseVisualStyleBackColor = true;
            this.cad_print_button.Click += new System.EventHandler(this.cad_print_button_Click);
            // 
            // CAD_Print_active
            // 
            this.CAD_Print_active.BackColor = System.Drawing.Color.Black;
            this.CAD_Print_active.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CAD_Print_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CAD_Print_active.Location = new System.Drawing.Point(135, 564);
            this.CAD_Print_active.Name = "CAD_Print_active";
            this.CAD_Print_active.ReadOnly = true;
            this.CAD_Print_active.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.CAD_Print_active.Size = new System.Drawing.Size(84, 17);
            this.CAD_Print_active.TabIndex = 51;
            this.CAD_Print_active.Text = "";
            // 
            // cad_print_active_text
            // 
            this.cad_print_active_text.BackColor = System.Drawing.Color.Black;
            this.cad_print_active_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cad_print_active_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cad_print_active_text.Location = new System.Drawing.Point(136, 564);
            this.cad_print_active_text.Name = "cad_print_active_text";
            this.cad_print_active_text.ReadOnly = true;
            this.cad_print_active_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.cad_print_active_text.Size = new System.Drawing.Size(84, 17);
            this.cad_print_active_text.TabIndex = 52;
            this.cad_print_active_text.Text = "";
            // 
            // turn_check_active
            // 
            this.turn_check_active.BackColor = System.Drawing.Color.Black;
            this.turn_check_active.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.turn_check_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.turn_check_active.Location = new System.Drawing.Point(136, 593);
            this.turn_check_active.Name = "turn_check_active";
            this.turn_check_active.ReadOnly = true;
            this.turn_check_active.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.turn_check_active.Size = new System.Drawing.Size(84, 17);
            this.turn_check_active.TabIndex = 55;
            this.turn_check_active.Text = "";
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.Black;
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox5.Location = new System.Drawing.Point(135, 593);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(84, 17);
            this.richTextBox5.TabIndex = 54;
            this.richTextBox5.Text = "";
            // 
            // turn_check_button
            // 
            this.turn_check_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.turn_check_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.turn_check_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.turn_check_button.Location = new System.Drawing.Point(11, 589);
            this.turn_check_button.Name = "turn_check_button";
            this.turn_check_button.Size = new System.Drawing.Size(118, 23);
            this.turn_check_button.TabIndex = 53;
            this.turn_check_button.Text = "Turn Checker";
            this.turn_check_button.UseVisualStyleBackColor = true;
            this.turn_check_button.Click += new System.EventHandler(this.turn_check_button_Click);
            // 
            // task_track_button
            // 
            this.task_track_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.task_track_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.task_track_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.task_track_button.Location = new System.Drawing.Point(11, 618);
            this.task_track_button.Name = "task_track_button";
            this.task_track_button.Size = new System.Drawing.Size(118, 23);
            this.task_track_button.TabIndex = 56;
            this.task_track_button.Text = "Task Tracking";
            this.task_track_button.UseVisualStyleBackColor = true;
            this.task_track_button.Click += new System.EventHandler(this.task_track_button_Click);
            // 
            // task_tracker_active
            // 
            this.task_tracker_active.BackColor = System.Drawing.Color.Black;
            this.task_tracker_active.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.task_tracker_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.task_tracker_active.Location = new System.Drawing.Point(135, 622);
            this.task_tracker_active.Name = "task_tracker_active";
            this.task_tracker_active.ReadOnly = true;
            this.task_tracker_active.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.task_tracker_active.Size = new System.Drawing.Size(84, 17);
            this.task_tracker_active.TabIndex = 57;
            this.task_tracker_active.Text = "";
            // 
            // on_hold_active
            // 
            this.on_hold_active.BackColor = System.Drawing.Color.Black;
            this.on_hold_active.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.on_hold_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.on_hold_active.Location = new System.Drawing.Point(135, 651);
            this.on_hold_active.Name = "on_hold_active";
            this.on_hold_active.ReadOnly = true;
            this.on_hold_active.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.on_hold_active.Size = new System.Drawing.Size(84, 17);
            this.on_hold_active.TabIndex = 59;
            this.on_hold_active.Text = "";
            // 
            // on_hold_button
            // 
            this.on_hold_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.on_hold_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.on_hold_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.on_hold_button.Location = new System.Drawing.Point(11, 647);
            this.on_hold_button.Name = "on_hold_button";
            this.on_hold_button.Size = new System.Drawing.Size(118, 23);
            this.on_hold_button.TabIndex = 58;
            this.on_hold_button.Text = "On Hold Processor";
            this.on_hold_button.UseVisualStyleBackColor = true;
            this.on_hold_button.Click += new System.EventHandler(this.on_hold_button_Click);
            // 
            // admin_button
            // 
            this.admin_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.admin_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admin_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.admin_button.Location = new System.Drawing.Point(180, 113);
            this.admin_button.Margin = new System.Windows.Forms.Padding(1);
            this.admin_button.Name = "admin_button";
            this.admin_button.Size = new System.Drawing.Size(84, 23);
            this.admin_button.TabIndex = 60;
            this.admin_button.Text = "Administrative";
            this.admin_button.UseVisualStyleBackColor = true;
            this.admin_button.Click += new System.EventHandler(this.admin_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label5.Location = new System.Drawing.Point(3, 679);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "Server Statistics";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(-25, 698);
            this.chart1.Margin = new System.Windows.Forms.Padding(1);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BorderColor = System.Drawing.Color.Black;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Name = "Series3";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(298, 107);
            this.chart1.TabIndex = 62;
            this.chart1.Text = "chart1";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-9, -5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 926);
            this.textBox1.TabIndex = 63;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(275, -22);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(10, 933);
            this.textBox2.TabIndex = 64;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Black;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.Black;
            this.textBox5.Location = new System.Drawing.Point(3, 767);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(272, 17);
            this.textBox5.TabIndex = 65;
            this.textBox5.Text = "6";
            // 
            // ping1_text
            // 
            this.ping1_text.BackColor = System.Drawing.Color.Black;
            this.ping1_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ping1_text.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ping1_text.Location = new System.Drawing.Point(211, 689);
            this.ping1_text.Name = "ping1_text";
            this.ping1_text.ReadOnly = true;
            this.ping1_text.Size = new System.Drawing.Size(64, 20);
            this.ping1_text.TabIndex = 66;
            this.ping1_text.Text = "Ping: 0ms";
            // 
            // avg_ping
            // 
            this.avg_ping.BackColor = System.Drawing.Color.Black;
            this.avg_ping.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.avg_ping.ForeColor = System.Drawing.Color.DodgerBlue;
            this.avg_ping.Location = new System.Drawing.Point(14, 772);
            this.avg_ping.Name = "avg_ping";
            this.avg_ping.ReadOnly = true;
            this.avg_ping.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.avg_ping.Size = new System.Drawing.Size(144, 12);
            this.avg_ping.TabIndex = 67;
            this.avg_ping.Text = "Avg Ping (Non-zero): 0ms";
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.Color.Black;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.richTextBox6.Location = new System.Drawing.Point(161, 772);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.Size = new System.Drawing.Size(109, 33);
            this.richTextBox6.TabIndex = 68;
            this.richTextBox6.Text = "Server Data Transfer (10.0.0.8) Rate";
            // 
            // cpu_text
            // 
            this.cpu_text.BackColor = System.Drawing.Color.Black;
            this.cpu_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cpu_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(170)))), ((int)(((byte)(0)))));
            this.cpu_text.Location = new System.Drawing.Point(15, 787);
            this.cpu_text.Name = "cpu_text";
            this.cpu_text.ReadOnly = true;
            this.cpu_text.Size = new System.Drawing.Size(132, 15);
            this.cpu_text.TabIndex = 69;
            this.cpu_text.Text = "CPU Usage: 0%";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.Black;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.ForeColor = System.Drawing.Color.Brown;
            this.richTextBox4.Location = new System.Drawing.Point(15, 800);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.Size = new System.Drawing.Size(132, 15);
            this.richTextBox4.TabIndex = 70;
            this.richTextBox4.Text = "Avg. Core Frequency";
            // 
            // stat_button
            // 
            this.stat_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stat_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stat_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.stat_button.Location = new System.Drawing.Point(11, 142);
            this.stat_button.Margin = new System.Windows.Forms.Padding(1);
            this.stat_button.Name = "stat_button";
            this.stat_button.Size = new System.Drawing.Size(82, 23);
            this.stat_button.TabIndex = 71;
            this.stat_button.Text = "Statistics";
            this.stat_button.UseVisualStyleBackColor = true;
            this.stat_button.Click += new System.EventHandler(this.stat_button_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Transparent;
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Location = new System.Drawing.Point(1, 526);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(10, 10);
            this.button11.TabIndex = 72;
            this.button11.TabStop = false;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(276, 825);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.stat_button);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.cpu_text);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.avg_ping);
            this.Controls.Add(this.ping1_text);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.admin_button);
            this.Controls.Add(this.on_hold_active);
            this.Controls.Add(this.on_hold_button);
            this.Controls.Add(this.task_tracker_active);
            this.Controls.Add(this.task_track_button);
            this.Controls.Add(this.turn_check_active);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.turn_check_button);
            this.Controls.Add(this.cad_print_active_text);
            this.Controls.Add(this.CAD_Print_active);
            this.Controls.Add(this.cad_print_button);
            this.Controls.Add(this.BLS_AL_active_text);
            this.Controls.Add(this.bolster_autolathe_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.page_button);
            this.Controls.Add(this.inbox_button);
            this.Controls.Add(this.send_email_button);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.progressBar10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.progressBar9);
            this.Controls.Add(this.notifications);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.clearall_button);
            this.Controls.Add(this.selectall_button);
            this.Controls.Add(this.google_search_box);
            this.Controls.Add(this.SO_text);
            this.Controls.Add(this.print_button);
            this.Controls.Add(this.retrieve_button);
            this.Controls.Add(this.search_clear_button);
            this.Controls.Add(this.steel_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.calc_button);
            this.Controls.Add(this.messenger_button);
            this.Controls.Add(this.shoptrack_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.print_list);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.g_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.text7);
            this.Controls.Add(this.progressBar8);
            this.Controls.Add(this.text6);
            this.Controls.Add(this.progressBar7);
            this.Controls.Add(this.progressBar6);
            this.Controls.Add(this.text5);
            this.Controls.Add(this.progressBar5);
            this.Controls.Add(this.text4);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.text3);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.numberOfFilesText);
            this.Controls.Add(this.designerBox);
            this.Controls.Add(this.text2);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.text);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.progressBar1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Opacity = 0.85D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor";
            this.Activated += new System.EventHandler(this.this_OnActivated);
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox text1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.RichTextBox text2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.RichTextBox text3;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.RichTextBox text4;
        private System.Windows.Forms.ProgressBar progressBar5;
        private System.Windows.Forms.RichTextBox text5;
        private System.Windows.Forms.ProgressBar progressBar6;
        private System.Windows.Forms.RichTextBox text6;
        private System.Windows.Forms.Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private CheckedListBox print_list;
        private Label label1;
        private Label label2;
        private TextBox SO_text;
        private RichTextBox text;
        private Button print_button;
        private Button clear_button;
        private Button retrieve_button;
        private Button selectall_button;
        private Button clearall_button;
        private RichTextBox designerBox;
        private RichTextBox numberOfFilesText;
        private Label label3;
        private Button shoptrack_button;
        private TextBox google_search_box;
        private Button search_button;
        private Button search_clear_button;
        private Button calc_button;
        private ProgressBar progressBar7;
        private RichTextBox text7;
        private Button button7;
        private Button g_button;
        private Button steel_button;
        private Button messenger_button;
        private Button close_button;
        private Button minimize_button;
        private ProgressBar progressBar8;
        private RichTextBox richTextBox1;
        private Button button8;
        private TextBox textBox3;
        private TextBox textBox4;
        private CheckBox notifications;
        private Button button9;
        private RichTextBox richTextBox2;
        private ProgressBar progressBar9;
        private Button button10;
        private RichTextBox richTextBox3;
        private ProgressBar progressBar10;
        private Button send_email_button;
        private Button inbox_button;
        private Button page_button;
        private Label label4;
        private Button bolster_autolathe_button;
        private RichTextBox BLS_AL_active_text;
        private Button cad_print_button;
        private RichTextBox CAD_Print_active;
        private RichTextBox cad_print_active_text;
        private RichTextBox turn_check_active;
        private RichTextBox richTextBox5;
        private Button turn_check_button;
        private Button task_track_button;
        private RichTextBox task_tracker_active;
        private RichTextBox on_hold_active;
        private Button on_hold_button;
        private Button admin_button;
        private Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox5;
        private RichTextBox ping1_text;
        private RichTextBox avg_ping;
        private RichTextBox richTextBox6;
        private RichTextBox cpu_text;
        private RichTextBox richTextBox4;
        private Button stat_button;
        private Button button11;
    }
}

