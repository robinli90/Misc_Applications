namespace ITManagement
{
    partial class Office
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Office));
            this.search_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.search_count_box = new System.Windows.Forms.Label();
            this.invisible_button = new System.Windows.Forms.Button();
            this.search_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.inventory_button = new System.Windows.Forms.Button();
            this.timeout_label = new System.Windows.Forms.Label();
            this.info_button = new System.Windows.Forms.Button();
            this.purchase_history_button = new System.Windows.Forms.Button();
            this.log_button = new System.Windows.Forms.Button();
            this.settings_button = new System.Windows.Forms.Button();
            this.edit_station_button = new System.Windows.Forms.Button();
            this.ref_button_circle = new System.Windows.Forms.Button();
            this.add_reminder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // search_box
            // 
            this.search_box.BackColor = System.Drawing.Color.White;
            this.search_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_box.Location = new System.Drawing.Point(1078, 72);
            this.search_box.Name = "search_box";
            this.search_box.Size = new System.Drawing.Size(81, 20);
            this.search_box.TabIndex = 182;
            this.search_box.TextChanged += new System.EventHandler(this.search_box_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1032, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 183;
            this.label1.Text = "Search:";
            // 
            // search_count_box
            // 
            this.search_count_box.Location = new System.Drawing.Point(1075, 95);
            this.search_count_box.Name = "search_count_box";
            this.search_count_box.Size = new System.Drawing.Size(91, 19);
            this.search_count_box.TabIndex = 210;
            this.search_count_box.Text = "0 item(s) found";
            this.search_count_box.Visible = false;
            // 
            // invisible_button
            // 
            this.invisible_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invisible_button.ForeColor = System.Drawing.Color.White;
            this.invisible_button.Location = new System.Drawing.Point(1168, 0);
            this.invisible_button.Name = "invisible_button";
            this.invisible_button.Size = new System.Drawing.Size(10, 10);
            this.invisible_button.TabIndex = 218;
            this.invisible_button.Text = "invisible";
            this.invisible_button.UseVisualStyleBackColor = true;
            this.invisible_button.Click += new System.EventHandler(this.button213_Click);
            // 
            // search_button
            // 
            this.search_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_button.Location = new System.Drawing.Point(1049, 28);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(97, 26);
            this.search_button.TabIndex = 219;
            this.search_button.Text = "Advanced";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1075, 738);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 219;
            this.label3.Text = "10.0.0.8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1075, 705);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 220;
            this.label4.Text = "10.0.0.8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1075, 771);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 218;
            this.label2.Text = "10.0.0.12";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1075, 672);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 221;
            this.label5.Text = "10.0.0.4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1035, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 223;
            this.label6.Text = "Quick";
            // 
            // inventory_button
            // 
            this.inventory_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventory_button.Location = new System.Drawing.Point(1049, 357);
            this.inventory_button.Margin = new System.Windows.Forms.Padding(1);
            this.inventory_button.Name = "inventory_button";
            this.inventory_button.Size = new System.Drawing.Size(97, 26);
            this.inventory_button.TabIndex = 2234;
            this.inventory_button.Text = "Inventory";
            this.inventory_button.UseVisualStyleBackColor = true;
            this.inventory_button.Click += new System.EventHandler(this.button214_Click);
            // 
            // timeout_label
            // 
            this.timeout_label.AutoSize = true;
            this.timeout_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeout_label.Location = new System.Drawing.Point(1067, 855);
            this.timeout_label.Name = "timeout_label";
            this.timeout_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timeout_label.Size = new System.Drawing.Size(0, 7);
            this.timeout_label.TabIndex = 2238;
            // 
            // info_button
            // 
            this.info_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info_button.Location = new System.Drawing.Point(1049, 316);
            this.info_button.Margin = new System.Windows.Forms.Padding(1);
            this.info_button.Name = "info_button";
            this.info_button.Size = new System.Drawing.Size(97, 26);
            this.info_button.TabIndex = 2239;
            this.info_button.Text = "Information";
            this.info_button.UseVisualStyleBackColor = true;
            this.info_button.Click += new System.EventHandler(this.info_button_Click);
            // 
            // purchase_history_button
            // 
            this.purchase_history_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.purchase_history_button.Location = new System.Drawing.Point(1049, 401);
            this.purchase_history_button.Margin = new System.Windows.Forms.Padding(1);
            this.purchase_history_button.Name = "purchase_history_button";
            this.purchase_history_button.Size = new System.Drawing.Size(97, 26);
            this.purchase_history_button.TabIndex = 2240;
            this.purchase_history_button.Text = "Puchase History";
            this.purchase_history_button.UseVisualStyleBackColor = true;
            this.purchase_history_button.Click += new System.EventHandler(this.purchase_history_button_Click);
            // 
            // log_button
            // 
            this.log_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.log_button.Location = new System.Drawing.Point(1049, 530);
            this.log_button.Margin = new System.Windows.Forms.Padding(1);
            this.log_button.Name = "log_button";
            this.log_button.Size = new System.Drawing.Size(97, 26);
            this.log_button.TabIndex = 2241;
            this.log_button.Text = "View Log";
            this.log_button.UseVisualStyleBackColor = true;
            this.log_button.Click += new System.EventHandler(this.log_button_Click);
            // 
            // settings_button
            // 
            this.settings_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settings_button.Location = new System.Drawing.Point(1049, 487);
            this.settings_button.Margin = new System.Windows.Forms.Padding(1);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(97, 26);
            this.settings_button.TabIndex = 2242;
            this.settings_button.Text = "Settings";
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.button214_Click_1);
            // 
            // edit_station_button
            // 
            this.edit_station_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit_station_button.Location = new System.Drawing.Point(1049, 444);
            this.edit_station_button.Margin = new System.Windows.Forms.Padding(1);
            this.edit_station_button.Name = "edit_station_button";
            this.edit_station_button.Size = new System.Drawing.Size(97, 26);
            this.edit_station_button.TabIndex = 2243;
            this.edit_station_button.Text = "Edit Stations";
            this.edit_station_button.UseVisualStyleBackColor = true;
            this.edit_station_button.Click += new System.EventHandler(this.edit_station_button_Click);
            // 
            // ref_button_circle
            // 
            this.ref_button_circle.Enabled = false;
            this.ref_button_circle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ref_button_circle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ref_button_circle.Location = new System.Drawing.Point(484, 407);
            this.ref_button_circle.Name = "ref_button_circle";
            this.ref_button_circle.Size = new System.Drawing.Size(31, 27);
            this.ref_button_circle.TabIndex = 2244;
            this.ref_button_circle.UseVisualStyleBackColor = true;
            this.ref_button_circle.Visible = false;
            // 
            // add_reminder
            // 
            this.add_reminder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_reminder.Location = new System.Drawing.Point(1049, 572);
            this.add_reminder.Margin = new System.Windows.Forms.Padding(1);
            this.add_reminder.Name = "add_reminder";
            this.add_reminder.Size = new System.Drawing.Size(97, 26);
            this.add_reminder.TabIndex = 2245;
            this.add_reminder.Text = "Add Reminder";
            this.add_reminder.UseVisualStyleBackColor = true;
            this.add_reminder.Click += new System.EventHandler(this.add_reminder_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1075, 803);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 2247;
            this.label8.Text = "10.0.1.102 (ADP)";
            // 
            // Office
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::ITManagement.Properties.Resources.EXCOFLOOR;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1175, 864);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.add_reminder);
            this.Controls.Add(this.ref_button_circle);
            this.Controls.Add(this.edit_station_button);
            this.Controls.Add(this.settings_button);
            this.Controls.Add(this.log_button);
            this.Controls.Add(this.purchase_history_button);
            this.Controls.Add(this.info_button);
            this.Controls.Add(this.timeout_label);
            this.Controls.Add(this.inventory_button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.invisible_button);
            this.Controls.Add(this.search_count_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Office";
            this.Text = "Exco Markham";
            this.Load += new System.EventHandler(this.Office_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label search_count_box;
        private System.Windows.Forms.Button invisible_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button inventory_button;
        private System.Windows.Forms.Label timeout_label;
        private System.Windows.Forms.Button info_button;
        private System.Windows.Forms.Button purchase_history_button;
        private System.Windows.Forms.Button log_button;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.Button edit_station_button;
        public System.Windows.Forms.Button ref_button_circle;
        private System.Windows.Forms.Button add_reminder;
        public System.Windows.Forms.TextBox search_box;
        private System.Windows.Forms.Label label8;
    }
}