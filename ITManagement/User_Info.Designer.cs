namespace ITManagement
{
    partial class User_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Info));
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.deparment = new System.Windows.Forms.ComboBox();
            this.computer_name = new System.Windows.Forms.TextBox();
            this.richTextBox12 = new System.Windows.Forms.RichTextBox();
            this.computer_ip = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.main_login_name = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.hardware_button = new System.Windows.Forms.Button();
            this.software_button = new System.Windows.Forms.Button();
            this.change_button = new System.Windows.Forms.Button();
            this.detail_group = new System.Windows.Forms.GroupBox();
            this.detail_list = new System.Windows.Forms.ListBox();
            this.clear_button = new System.Windows.Forms.Button();
            this.details = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.show_time_stamps = new System.Windows.Forms.CheckBox();
            this.remote_button = new System.Windows.Forms.Button();
            this.open_directory_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.decoy_password_box = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.detail_group.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Location = new System.Drawing.Point(33, 13);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(63, 18);
            this.richTextBox3.TabIndex = 19;
            this.richTextBox3.Text = "Department:";
            // 
            // deparment
            // 
            this.deparment.BackColor = System.Drawing.Color.Silver;
            this.deparment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deparment.FormattingEnabled = true;
            this.deparment.Location = new System.Drawing.Point(100, 9);
            this.deparment.Name = "deparment";
            this.deparment.Size = new System.Drawing.Size(134, 21);
            this.deparment.TabIndex = 10;
            this.deparment.SelectedIndexChanged += new System.EventHandler(this.deparment_SelectedIndexChanged);
            // 
            // computer_name
            // 
            this.computer_name.BackColor = System.Drawing.SystemColors.Control;
            this.computer_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.computer_name.Location = new System.Drawing.Point(100, 36);
            this.computer_name.Name = "computer_name";
            this.computer_name.Size = new System.Drawing.Size(93, 20);
            this.computer_name.TabIndex = 0;
            this.computer_name.TextChanged += new System.EventHandler(this.computer_name_TextChanged);
            this.computer_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.details2_KeyPress);
            // 
            // richTextBox12
            // 
            this.richTextBox12.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox12.Location = new System.Drawing.Point(66, 39);
            this.richTextBox12.Name = "richTextBox12";
            this.richTextBox12.ReadOnly = true;
            this.richTextBox12.Size = new System.Drawing.Size(31, 18);
            this.richTextBox12.TabIndex = 49;
            this.richTextBox12.Text = "User:";
            // 
            // computer_ip
            // 
            this.computer_ip.BackColor = System.Drawing.SystemColors.Control;
            this.computer_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.computer_ip.Location = new System.Drawing.Point(269, 36);
            this.computer_ip.Name = "computer_ip";
            this.computer_ip.Size = new System.Drawing.Size(93, 20);
            this.computer_ip.TabIndex = 1;
            this.computer_ip.TextChanged += new System.EventHandler(this.computer_ip_TextChanged);
            this.computer_ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.details2_KeyPress);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(246, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(22, 18);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = " IP:";
            // 
            // main_login_name
            // 
            this.main_login_name.BackColor = System.Drawing.SystemColors.Control;
            this.main_login_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.main_login_name.Location = new System.Drawing.Point(100, 60);
            this.main_login_name.Name = "main_login_name";
            this.main_login_name.Size = new System.Drawing.Size(93, 20);
            this.main_login_name.TabIndex = 2;
            this.main_login_name.TextChanged += new System.EventHandler(this.main_login_name_TextChanged);
            this.main_login_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.details2_KeyPress);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(6, 63);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(89, 18);
            this.richTextBox2.TabIndex = 53;
            this.richTextBox2.Text = "Main Login Name:";
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.Silver;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Location = new System.Drawing.Point(269, 61);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(93, 20);
            this.password.TabIndex = 3;
            this.password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.details2_KeyPress);
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Location = new System.Drawing.Point(213, 63);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.Size = new System.Drawing.Size(52, 18);
            this.richTextBox4.TabIndex = 55;
            this.richTextBox4.Text = "Password:";
            // 
            // hardware_button
            // 
            this.hardware_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hardware_button.Location = new System.Drawing.Point(3, 89);
            this.hardware_button.Name = "hardware_button";
            this.hardware_button.Size = new System.Drawing.Size(104, 22);
            this.hardware_button.TabIndex = 4;
            this.hardware_button.Text = "View Hardware";
            this.hardware_button.UseVisualStyleBackColor = true;
            this.hardware_button.Click += new System.EventHandler(this.hardware_button_Click);
            // 
            // software_button
            // 
            this.software_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.software_button.Location = new System.Drawing.Point(113, 89);
            this.software_button.Name = "software_button";
            this.software_button.Size = new System.Drawing.Size(104, 22);
            this.software_button.TabIndex = 5;
            this.software_button.Text = "View Software";
            this.software_button.UseVisualStyleBackColor = true;
            this.software_button.Click += new System.EventHandler(this.software_button_Click);
            // 
            // change_button
            // 
            this.change_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_button.Location = new System.Drawing.Point(223, 89);
            this.change_button.Name = "change_button";
            this.change_button.Size = new System.Drawing.Size(139, 22);
            this.change_button.TabIndex = 6;
            this.change_button.Text = "View Information";
            this.change_button.UseVisualStyleBackColor = true;
            this.change_button.Click += new System.EventHandler(this.change_button_Click);
            // 
            // detail_group
            // 
            this.detail_group.Controls.Add(this.detail_list);
            this.detail_group.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detail_group.Location = new System.Drawing.Point(3, 117);
            this.detail_group.Name = "detail_group";
            this.detail_group.Size = new System.Drawing.Size(359, 256);
            this.detail_group.TabIndex = 59;
            this.detail_group.TabStop = false;
            this.detail_group.Text = "Hardware";
            // 
            // detail_list
            // 
            this.detail_list.BackColor = System.Drawing.SystemColors.Control;
            this.detail_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detail_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detail_list.FormattingEnabled = true;
            this.detail_list.Location = new System.Drawing.Point(5, 18);
            this.detail_list.Name = "detail_list";
            this.detail_list.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.detail_list.Size = new System.Drawing.Size(348, 234);
            this.detail_list.TabIndex = 0;
            this.detail_list.SelectedIndexChanged += new System.EventHandler(this.detail_list_SelectedIndexChanged);
            this.detail_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.detail_list_MouseDoubleClick);
            // 
            // clear_button
            // 
            this.clear_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_button.Location = new System.Drawing.Point(113, 400);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(104, 22);
            this.clear_button.TabIndex = 8;
            this.clear_button.Text = "Delete Entry";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // details
            // 
            this.details.BackColor = System.Drawing.SystemColors.Control;
            this.details.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.details.Location = new System.Drawing.Point(3, 429);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(259, 20);
            this.details.TabIndex = 7;
            this.details.TextChanged += new System.EventHandler(this.details_TextChanged);
            this.details.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.details_KeyPress);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(266, 428);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 22);
            this.button4.TabIndex = 9;
            this.button4.Text = "Add Detail ";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // show_time_stamps
            // 
            this.show_time_stamps.AutoSize = true;
            this.show_time_stamps.Location = new System.Drawing.Point(12, 377);
            this.show_time_stamps.Name = "show_time_stamps";
            this.show_time_stamps.Size = new System.Drawing.Size(77, 17);
            this.show_time_stamps.TabIndex = 60;
            this.show_time_stamps.Text = "Show date";
            this.show_time_stamps.UseVisualStyleBackColor = true;
            this.show_time_stamps.CheckedChanged += new System.EventHandler(this.show_time_stamps_CheckedChanged);
            // 
            // remote_button
            // 
            this.remote_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remote_button.Location = new System.Drawing.Point(126, 22);
            this.remote_button.Name = "remote_button";
            this.remote_button.Size = new System.Drawing.Size(103, 22);
            this.remote_button.TabIndex = 61;
            this.remote_button.Text = "Remote Computer";
            this.remote_button.UseVisualStyleBackColor = true;
            this.remote_button.Click += new System.EventHandler(this.remote_button_Click);
            // 
            // open_directory_button
            // 
            this.open_directory_button.Enabled = false;
            this.open_directory_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.open_directory_button.Location = new System.Drawing.Point(17, 22);
            this.open_directory_button.Name = "open_directory_button";
            this.open_directory_button.Size = new System.Drawing.Size(103, 22);
            this.open_directory_button.TabIndex = 62;
            this.open_directory_button.Text = "Open Directory";
            this.open_directory_button.UseVisualStyleBackColor = true;
            this.open_directory_button.Click += new System.EventHandler(this.open_directory_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.open_directory_button);
            this.groupBox1.Controls.Add(this.remote_button);
            this.groupBox1.Location = new System.Drawing.Point(5, 456);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 58);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // decoy_password_box
            // 
            this.decoy_password_box.BackColor = System.Drawing.SystemColors.Control;
            this.decoy_password_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decoy_password_box.Enabled = false;
            this.decoy_password_box.Location = new System.Drawing.Point(269, 61);
            this.decoy_password_box.Name = "decoy_password_box";
            this.decoy_password_box.Size = new System.Drawing.Size(93, 20);
            this.decoy_password_box.TabIndex = 64;
            this.decoy_password_box.Text = "************";
            this.decoy_password_box.Visible = false;
            this.decoy_password_box.TextChanged += new System.EventHandler(this.decoy_password_box_TextChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(3, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 22);
            this.button1.TabIndex = 65;
            this.button1.Text = "Void Item";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(223, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 22);
            this.button2.TabIndex = 66;
            this.button2.Text = "Move to Inventory";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // User_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(371, 520);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.decoy_password_box);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.show_time_stamps);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.details);
            this.Controls.Add(this.detail_group);
            this.Controls.Add(this.change_button);
            this.Controls.Add(this.software_button);
            this.Controls.Add(this.hardware_button);
            this.Controls.Add(this.password);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.main_login_name);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.computer_ip);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.computer_name);
            this.Controls.Add(this.richTextBox12);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.deparment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "User_Info";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Information";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.User_Info_Load);
            this.detail_group.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.ComboBox deparment;
        private System.Windows.Forms.TextBox computer_name;
        private System.Windows.Forms.RichTextBox richTextBox12;
        private System.Windows.Forms.TextBox computer_ip;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox main_login_name;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Button hardware_button;
        private System.Windows.Forms.Button software_button;
        private System.Windows.Forms.Button change_button;
        private System.Windows.Forms.GroupBox detail_group;
        private System.Windows.Forms.ListBox detail_list;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.TextBox details;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox show_time_stamps;
        private System.Windows.Forms.Button remote_button;
        private System.Windows.Forms.Button open_directory_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox decoy_password_box;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}