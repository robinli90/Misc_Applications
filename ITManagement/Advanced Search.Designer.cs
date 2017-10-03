namespace ITManagement
{
    partial class Advanced_Search
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Advanced_Search));
            this.search_grid = new System.Windows.Forms.DataGridView();
            this.User = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.search_box = new System.Windows.Forms.TextBox();
            this.basic_search = new System.Windows.Forms.CheckBox();
            this.search_button = new System.Windows.Forms.Button();
            this.search_count_box = new System.Windows.Forms.Label();
            this.view_all_box = new System.Windows.Forms.CheckBox();
            this.search_by_single_date = new System.Windows.Forms.CheckBox();
            this.search_by_date = new System.Windows.Forms.DateTimePicker();
            this.search_date_range = new System.Windows.Forms.CheckBox();
            this.from_date = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.to_date = new System.Windows.Forms.DateTimePicker();
            this.show_info = new System.Windows.Forms.CheckBox();
            this.show_sw = new System.Windows.Forms.CheckBox();
            this.show_hw = new System.Windows.Forms.CheckBox();
            this.search_by_dept = new System.Windows.Forms.CheckBox();
            this.deparment = new System.Windows.Forms.ComboBox();
            this.search_within = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.search_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // search_grid
            // 
            this.search_grid.AllowUserToAddRows = false;
            this.search_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.search_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.search_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.search_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.search_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.Department,
            this.IP,
            this.D,
            this.Column1});
            this.search_grid.Location = new System.Drawing.Point(12, 138);
            this.search_grid.MultiSelect = false;
            this.search_grid.Name = "search_grid";
            this.search_grid.ReadOnly = true;
            this.search_grid.RowHeadersVisible = false;
            this.search_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.search_grid.Size = new System.Drawing.Size(1011, 372);
            this.search_grid.TabIndex = 0;
            this.search_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.search_grid_CellContentClick_1);
            // 
            // User
            // 
            this.User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.User.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.User.Width = 54;
            // 
            // Department
            // 
            this.Department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Department.DefaultCellStyle = dataGridViewCellStyle2;
            this.Department.HeaderText = "Department";
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            this.Department.Width = 87;
            // 
            // IP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IP.DefaultCellStyle = dataGridViewCellStyle3;
            this.IP.HeaderText = "IP Address";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Width = 83;
            // 
            // D
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.D.DefaultCellStyle = dataGridViewCellStyle4;
            this.D.HeaderText = "Login ID";
            this.D.Name = "D";
            this.D.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column1.HeaderText = "Information";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // search_box
            // 
            this.search_box.BackColor = System.Drawing.Color.White;
            this.search_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_box.Enabled = false;
            this.search_box.Location = new System.Drawing.Point(110, 33);
            this.search_box.Name = "search_box";
            this.search_box.Size = new System.Drawing.Size(129, 20);
            this.search_box.TabIndex = 184;
            this.search_box.TextChanged += new System.EventHandler(this.search_box_TextChanged);
            // 
            // basic_search
            // 
            this.basic_search.AutoSize = true;
            this.basic_search.Location = new System.Drawing.Point(12, 36);
            this.basic_search.Name = "basic_search";
            this.basic_search.Size = new System.Drawing.Size(92, 17);
            this.basic_search.TabIndex = 185;
            this.basic_search.Text = "Basic Search:";
            this.basic_search.UseVisualStyleBackColor = true;
            this.basic_search.CheckedChanged += new System.EventHandler(this.basic_search_CheckedChanged);
            // 
            // search_button
            // 
            this.search_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_button.Location = new System.Drawing.Point(920, 91);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(103, 22);
            this.search_button.TabIndex = 186;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // search_count_box
            // 
            this.search_count_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_count_box.Location = new System.Drawing.Point(928, 116);
            this.search_count_box.Name = "search_count_box";
            this.search_count_box.Size = new System.Drawing.Size(95, 19);
            this.search_count_box.TabIndex = 211;
            this.search_count_box.Text = "0 item(s) found";
            this.search_count_box.Visible = false;
            // 
            // view_all_box
            // 
            this.view_all_box.AutoSize = true;
            this.view_all_box.Location = new System.Drawing.Point(12, 13);
            this.view_all_box.Name = "view_all_box";
            this.view_all_box.Size = new System.Drawing.Size(63, 17);
            this.view_all_box.TabIndex = 212;
            this.view_all_box.Text = "View All";
            this.view_all_box.UseVisualStyleBackColor = true;
            this.view_all_box.CheckedChanged += new System.EventHandler(this.view_all_box_CheckedChanged);
            // 
            // search_by_single_date
            // 
            this.search_by_single_date.AutoSize = true;
            this.search_by_single_date.Location = new System.Drawing.Point(12, 60);
            this.search_by_single_date.Name = "search_by_single_date";
            this.search_by_single_date.Size = new System.Drawing.Size(98, 17);
            this.search_by_single_date.TabIndex = 213;
            this.search_by_single_date.Text = "Search by date";
            this.search_by_single_date.UseVisualStyleBackColor = true;
            this.search_by_single_date.CheckedChanged += new System.EventHandler(this.search_by_single_CheckedChanged);
            // 
            // search_by_date
            // 
            this.search_by_date.Enabled = false;
            this.search_by_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.search_by_date.Location = new System.Drawing.Point(110, 57);
            this.search_by_date.Name = "search_by_date";
            this.search_by_date.Size = new System.Drawing.Size(99, 20);
            this.search_by_date.TabIndex = 214;
            this.search_by_date.ValueChanged += new System.EventHandler(this.search_by_date_ValueChanged);
            // 
            // search_date_range
            // 
            this.search_date_range.AutoSize = true;
            this.search_date_range.Location = new System.Drawing.Point(12, 84);
            this.search_date_range.Name = "search_date_range";
            this.search_date_range.Size = new System.Drawing.Size(148, 17);
            this.search_date_range.TabIndex = 215;
            this.search_date_range.Text = "Search by range of dates:";
            this.search_date_range.UseVisualStyleBackColor = true;
            this.search_date_range.CheckedChanged += new System.EventHandler(this.search_date_range_CheckedChanged);
            // 
            // from_date
            // 
            this.from_date.Enabled = false;
            this.from_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.from_date.Location = new System.Drawing.Point(166, 83);
            this.from_date.Name = "from_date";
            this.from_date.Size = new System.Drawing.Size(99, 20);
            this.from_date.TabIndex = 216;
            this.from_date.ValueChanged += new System.EventHandler(this.from_date_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(272, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 19);
            this.label1.TabIndex = 211;
            this.label1.Text = "to";
            // 
            // to_date
            // 
            this.to_date.Enabled = false;
            this.to_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.to_date.Location = new System.Drawing.Point(293, 83);
            this.to_date.Name = "to_date";
            this.to_date.Size = new System.Drawing.Size(99, 20);
            this.to_date.TabIndex = 216;
            this.to_date.ValueChanged += new System.EventHandler(this.to_date_ValueChanged);
            // 
            // show_info
            // 
            this.show_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.show_info.AutoSize = true;
            this.show_info.Checked = true;
            this.show_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_info.Location = new System.Drawing.Point(920, 68);
            this.show_info.Name = "show_info";
            this.show_info.Size = new System.Drawing.Size(108, 17);
            this.show_info.TabIndex = 217;
            this.show_info.Text = "Show Information";
            this.show_info.UseVisualStyleBackColor = true;
            this.show_info.CheckedChanged += new System.EventHandler(this.show_info_CheckedChanged);
            // 
            // show_sw
            // 
            this.show_sw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.show_sw.AutoSize = true;
            this.show_sw.Checked = true;
            this.show_sw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_sw.Location = new System.Drawing.Point(920, 45);
            this.show_sw.Name = "show_sw";
            this.show_sw.Size = new System.Drawing.Size(98, 17);
            this.show_sw.TabIndex = 218;
            this.show_sw.Text = "Show Software";
            this.show_sw.UseVisualStyleBackColor = true;
            this.show_sw.CheckedChanged += new System.EventHandler(this.show_sw_CheckedChanged);
            // 
            // show_hw
            // 
            this.show_hw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.show_hw.AutoSize = true;
            this.show_hw.Checked = true;
            this.show_hw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_hw.Location = new System.Drawing.Point(920, 22);
            this.show_hw.Name = "show_hw";
            this.show_hw.Size = new System.Drawing.Size(102, 17);
            this.show_hw.TabIndex = 219;
            this.show_hw.Text = "Show Hardware";
            this.show_hw.UseVisualStyleBackColor = true;
            this.show_hw.CheckedChanged += new System.EventHandler(this.show_hw_CheckedChanged);
            // 
            // search_by_dept
            // 
            this.search_by_dept.Location = new System.Drawing.Point(12, 108);
            this.search_by_dept.Name = "search_by_dept";
            this.search_by_dept.Size = new System.Drawing.Size(136, 17);
            this.search_by_dept.TabIndex = 221;
            this.search_by_dept.Text = "Search by Department:";
            this.search_by_dept.UseVisualStyleBackColor = true;
            this.search_by_dept.CheckedChanged += new System.EventHandler(this.search_by_dept_CheckedChanged);
            // 
            // deparment
            // 
            this.deparment.BackColor = System.Drawing.Color.Silver;
            this.deparment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deparment.Enabled = false;
            this.deparment.FormattingEnabled = true;
            this.deparment.Location = new System.Drawing.Point(144, 106);
            this.deparment.Name = "deparment";
            this.deparment.Size = new System.Drawing.Size(134, 21);
            this.deparment.TabIndex = 222;
            this.deparment.SelectedIndexChanged += new System.EventHandler(this.deparment_SelectedIndexChanged_1);
            // 
            // search_within
            // 
            this.search_within.BackColor = System.Drawing.Color.White;
            this.search_within.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_within.Location = new System.Drawing.Point(490, 64);
            this.search_within.Name = "search_within";
            this.search_within.Size = new System.Drawing.Size(129, 20);
            this.search_within.TabIndex = 223;
            this.search_within.Visible = false;
            this.search_within.TextChanged += new System.EventHandler(this.search_within_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(448, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 28);
            this.label2.TabIndex = 224;
            this.label2.Text = "Search within:";
            this.label2.Visible = false;
            // 
            // Advanced_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 522);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.search_within);
            this.Controls.Add(this.deparment);
            this.Controls.Add(this.search_by_dept);
            this.Controls.Add(this.show_hw);
            this.Controls.Add(this.show_sw);
            this.Controls.Add(this.show_info);
            this.Controls.Add(this.to_date);
            this.Controls.Add(this.from_date);
            this.Controls.Add(this.search_date_range);
            this.Controls.Add(this.search_by_date);
            this.Controls.Add(this.search_by_single_date);
            this.Controls.Add(this.view_all_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search_count_box);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.basic_search);
            this.Controls.Add(this.search_box);
            this.Controls.Add(this.search_grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1051, 560);
            this.Name = "Advanced_Search";
            this.Load += new System.EventHandler(this.Advanced_Search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.search_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView search_grid;
        private System.Windows.Forms.TextBox search_box;
        private System.Windows.Forms.CheckBox basic_search;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Label search_count_box;
        private System.Windows.Forms.CheckBox view_all_box;
        private System.Windows.Forms.CheckBox search_by_single_date;
        private System.Windows.Forms.DateTimePicker search_by_date;
        private System.Windows.Forms.CheckBox search_date_range;
        private System.Windows.Forms.DateTimePicker from_date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker to_date;
        private System.Windows.Forms.CheckBox show_info;
        private System.Windows.Forms.CheckBox show_sw;
        private System.Windows.Forms.CheckBox show_hw;
        private System.Windows.Forms.DataGridViewButtonColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.CheckBox search_by_dept;
        private System.Windows.Forms.ComboBox deparment;
        private System.Windows.Forms.TextBox search_within;
        private System.Windows.Forms.Label label2;
    }
}