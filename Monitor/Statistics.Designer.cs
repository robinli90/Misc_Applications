using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Monitor
{
    partial class Statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.minimize_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.today_button = new System.Windows.Forms.Button();
            this.last7_button = new System.Windows.Forms.Button();
            this.between_button = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.to_date_text = new System.Windows.Forms.RichTextBox();
            this.to_date_picker = new System.Windows.Forms.DateTimePicker();
            this.from_date_text = new System.Windows.Forms.RichTextBox();
            this.from_date_picker = new System.Windows.Forms.DateTimePicker();
            this.refresh_button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.delete_report_name_text = new System.Windows.Forms.TextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.add_report_button = new System.Windows.Forms.Button();
            this.report_comment_text = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.report_value_text = new System.Windows.Forms.TextBox();
            this.report_name_text = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.report_table = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(-19, -9);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(691, 10);
            this.textBox4.TabIndex = 80;
            // 
            // minimize_button
            // 
            this.minimize_button.BackColor = System.Drawing.Color.Black;
            this.minimize_button.FlatAppearance.BorderSize = 0;
            this.minimize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.minimize_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimize_button.Location = new System.Drawing.Point(615, -6);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(18, 22);
            this.minimize_button.TabIndex = 78;
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
            this.close_button.Location = new System.Drawing.Point(633, 0);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 77;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(650, -1);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(14, 577);
            this.textBox3.TabIndex = 79;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-21, 406);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(691, 10);
            this.textBox1.TabIndex = 81;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(-13, -79);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(14, 577);
            this.textBox2.TabIndex = 82;
            // 
            // today_button
            // 
            this.today_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.today_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.today_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.today_button.Location = new System.Drawing.Point(6, 19);
            this.today_button.Name = "today_button";
            this.today_button.Size = new System.Drawing.Size(52, 23);
            this.today_button.TabIndex = 85;
            this.today_button.Text = "Today";
            this.today_button.UseVisualStyleBackColor = true;
            this.today_button.Click += new System.EventHandler(this.today_button_Click);
            // 
            // last7_button
            // 
            this.last7_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.last7_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.last7_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.last7_button.Location = new System.Drawing.Point(138, 20);
            this.last7_button.Name = "last7_button";
            this.last7_button.Size = new System.Drawing.Size(78, 23);
            this.last7_button.TabIndex = 86;
            this.last7_button.Text = "  Last 7 days";
            this.last7_button.UseVisualStyleBackColor = true;
            this.last7_button.Click += new System.EventHandler(this.last7_button_Click);
            // 
            // between_button
            // 
            this.between_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.between_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.between_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.between_button.Location = new System.Drawing.Point(222, 20);
            this.between_button.Name = "between_button";
            this.between_button.Size = new System.Drawing.Size(63, 23);
            this.between_button.TabIndex = 87;
            this.between_button.Text = "Between";
            this.between_button.UseVisualStyleBackColor = true;
            this.between_button.Click += new System.EventHandler(this.between_button_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox2.Location = new System.Drawing.Point(375, 19);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(14, 16);
            this.richTextBox2.TabIndex = 89;
            this.richTextBox2.Text = " _";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.to_date_text);
            this.groupBox2.Controls.Add(this.to_date_picker);
            this.groupBox2.Controls.Add(this.from_date_text);
            this.groupBox2.Controls.Add(this.from_date_picker);
            this.groupBox2.Controls.Add(this.refresh_button);
            this.groupBox2.Controls.Add(this.today_button);
            this.groupBox2.Controls.Add(this.last7_button);
            this.groupBox2.Controls.Add(this.richTextBox2);
            this.groupBox2.Controls.Add(this.between_button);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(12, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(627, 53);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Query";
            // 
            // button5
            // 
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button5.Location = new System.Drawing.Point(482, 21);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 23);
            this.button5.TabIndex = 100;
            this.button5.Text = "View Graph";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button4.Location = new System.Drawing.Point(64, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 23);
            this.button4.TabIndex = 99;
            this.button4.Text = "Yesterday";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // to_date_text
            // 
            this.to_date_text.BackColor = System.Drawing.Color.Black;
            this.to_date_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.to_date_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.to_date_text.Location = new System.Drawing.Point(393, 25);
            this.to_date_text.Name = "to_date_text";
            this.to_date_text.ReadOnly = true;
            this.to_date_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.to_date_text.Size = new System.Drawing.Size(62, 19);
            this.to_date_text.TabIndex = 98;
            this.to_date_text.Text = "04/22/2015";
            // 
            // to_date_picker
            // 
            this.to_date_picker.CalendarForeColor = System.Drawing.SystemColors.ButtonFace;
            this.to_date_picker.CalendarMonthBackground = System.Drawing.Color.Black;
            this.to_date_picker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.to_date_picker.CalendarTitleForeColor = System.Drawing.SystemColors.Control;
            this.to_date_picker.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonFace;
            this.to_date_picker.Location = new System.Drawing.Point(458, 22);
            this.to_date_picker.Name = "to_date_picker";
            this.to_date_picker.Size = new System.Drawing.Size(18, 20);
            this.to_date_picker.TabIndex = 97;
            this.to_date_picker.Value = new System.DateTime(2015, 4, 28, 0, 0, 0, 0);
            this.to_date_picker.ValueChanged += new System.EventHandler(this.to_date_picker_ValueChanged);
            // 
            // from_date_text
            // 
            this.from_date_text.BackColor = System.Drawing.Color.Black;
            this.from_date_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.from_date_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.from_date_text.Location = new System.Drawing.Point(289, 25);
            this.from_date_text.Name = "from_date_text";
            this.from_date_text.ReadOnly = true;
            this.from_date_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.from_date_text.Size = new System.Drawing.Size(62, 19);
            this.from_date_text.TabIndex = 96;
            this.from_date_text.Text = "04/22/2015";
            // 
            // from_date_picker
            // 
            this.from_date_picker.CalendarForeColor = System.Drawing.SystemColors.ButtonFace;
            this.from_date_picker.CalendarMonthBackground = System.Drawing.Color.Black;
            this.from_date_picker.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.from_date_picker.CalendarTitleForeColor = System.Drawing.SystemColors.Control;
            this.from_date_picker.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonFace;
            this.from_date_picker.Location = new System.Drawing.Point(354, 22);
            this.from_date_picker.Name = "from_date_picker";
            this.from_date_picker.Size = new System.Drawing.Size(18, 20);
            this.from_date_picker.TabIndex = 94;
            this.from_date_picker.Value = new System.DateTime(2015, 4, 28, 0, 0, 0, 0);
            this.from_date_picker.ValueChanged += new System.EventHandler(this.from_date_picker_ValueChanged);
            // 
            // refresh_button
            // 
            this.refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.refresh_button.Location = new System.Drawing.Point(562, 21);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(59, 23);
            this.refresh_button.TabIndex = 93;
            this.refresh_button.Text = "Refresh";
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.delete_report_name_text);
            this.groupBox3.Controls.Add(this.richTextBox5);
            this.groupBox3.Controls.Add(this.add_report_button);
            this.groupBox3.Controls.Add(this.report_comment_text);
            this.groupBox3.Controls.Add(this.richTextBox4);
            this.groupBox3.Controls.Add(this.report_value_text);
            this.groupBox3.Controls.Add(this.report_name_text);
            this.groupBox3.Controls.Add(this.richTextBox3);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox3.Location = new System.Drawing.Point(12, 314);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(627, 81);
            this.groupBox3.TabIndex = 94;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Report Administration";
            // 
            // button3
            // 
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button3.Location = new System.Drawing.Point(80, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 103;
            this.button3.Text = "Daily Log File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.Location = new System.Drawing.Point(6, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 99;
            this.button2.Text = "Log File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(537, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 102;
            this.button1.Text = "Delete Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // delete_report_name_text
            // 
            this.delete_report_name_text.BackColor = System.Drawing.Color.Black;
            this.delete_report_name_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.delete_report_name_text.Location = new System.Drawing.Point(427, 50);
            this.delete_report_name_text.MaxLength = 255;
            this.delete_report_name_text.Name = "delete_report_name_text";
            this.delete_report_name_text.Size = new System.Drawing.Size(105, 20);
            this.delete_report_name_text.TabIndex = 3;
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.Black;
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox5.Location = new System.Drawing.Point(298, 53);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(135, 12);
            this.richTextBox5.TabIndex = 100;
            this.richTextBox5.Text = "Name of Report to Delete:";
            // 
            // add_report_button
            // 
            this.add_report_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.add_report_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_report_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.add_report_button.Location = new System.Drawing.Point(538, 19);
            this.add_report_button.Name = "add_report_button";
            this.add_report_button.Size = new System.Drawing.Size(83, 23);
            this.add_report_button.TabIndex = 94;
            this.add_report_button.Text = "Add Report";
            this.add_report_button.UseVisualStyleBackColor = true;
            this.add_report_button.Click += new System.EventHandler(this.add_report_button_Click);
            // 
            // report_comment_text
            // 
            this.report_comment_text.BackColor = System.Drawing.Color.Black;
            this.report_comment_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.report_comment_text.Location = new System.Drawing.Point(354, 21);
            this.report_comment_text.MaxLength = 255;
            this.report_comment_text.Name = "report_comment_text";
            this.report_comment_text.Size = new System.Drawing.Size(178, 20);
            this.report_comment_text.TabIndex = 2;
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.Black;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox4.Location = new System.Drawing.Point(300, 24);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox4.Size = new System.Drawing.Size(54, 23);
            this.richTextBox4.TabIndex = 98;
            this.richTextBox4.Text = "Comment:";
            // 
            // report_value_text
            // 
            this.report_value_text.BackColor = System.Drawing.Color.Black;
            this.report_value_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.report_value_text.Location = new System.Drawing.Point(207, 21);
            this.report_value_text.MaxLength = 255;
            this.report_value_text.Name = "report_value_text";
            this.report_value_text.Size = new System.Drawing.Size(86, 20);
            this.report_value_text.TabIndex = 1;
            // 
            // report_name_text
            // 
            this.report_name_text.BackColor = System.Drawing.Color.Black;
            this.report_name_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.report_name_text.Location = new System.Drawing.Point(80, 21);
            this.report_name_text.MaxLength = 255;
            this.report_name_text.Name = "report_name_text";
            this.report_name_text.Size = new System.Drawing.Size(86, 20);
            this.report_name_text.TabIndex = 0;
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.Black;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox3.Location = new System.Drawing.Point(171, 24);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(37, 23);
            this.richTextBox3.TabIndex = 96;
            this.richTextBox3.Text = "Value:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(8, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(68, 23);
            this.richTextBox1.TabIndex = 94;
            this.richTextBox1.Text = "Report Name:";
            // 
            // report_table
            // 
            this.report_table.AutoScroll = true;
            this.report_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.report_table.ColumnCount = 3;
            this.report_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.report_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.report_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 427F));
            this.report_table.ForeColor = System.Drawing.Color.Transparent;
            this.report_table.Location = new System.Drawing.Point(12, 74);
            this.report_table.MaximumSize = new System.Drawing.Size(627, 235);
            this.report_table.Name = "report_table";
            this.report_table.Padding = new System.Windows.Forms.Padding(1);
            this.report_table.Size = new System.Drawing.Size(627, 235);
            this.report_table.TabIndex = 95;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(651, 407);
            this.Controls.Add(this.report_table);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.textBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button minimize_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private Button today_button;
        private Button last7_button;
        private Button between_button;
        private RichTextBox richTextBox2;
        private GroupBox groupBox2;
        private Button refresh_button;
        private GroupBox groupBox3;
        private RichTextBox richTextBox1;
        private Button add_report_button;
        private TextBox report_comment_text;
        private RichTextBox richTextBox4;
        private TextBox report_value_text;
        private TextBox report_name_text;
        private RichTextBox richTextBox3;
        private TableLayoutPanel report_table;
        private DateTimePicker from_date_picker;
        private RichTextBox to_date_text;
        private DateTimePicker to_date_picker;
        private RichTextBox from_date_text;
        private Button button1;
        private TextBox delete_report_name_text;
        private RichTextBox richTextBox5;
        private Button button3;
        private Button button2;
        private Button button4;
        private Button button5;
    }
}