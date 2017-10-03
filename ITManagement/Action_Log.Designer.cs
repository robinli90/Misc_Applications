namespace ITManagement
{
    partial class Action_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Action_Log));
            this.log_grid = new System.Windows.Forms.DataGridView();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clear_log_button = new System.Windows.Forms.Button();
            this.search_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clear_90_days = new System.Windows.Forms.Button();
            this.search_count_box = new System.Windows.Forms.Label();
            this.show_id = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.log_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // log_grid
            // 
            this.log_grid.AllowUserToAddRows = false;
            this.log_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.log_grid.BackgroundColor = System.Drawing.Color.White;
            this.log_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.log_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.log_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.log_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.D,
            this.Column1});
            this.log_grid.Location = new System.Drawing.Point(-2, 24);
            this.log_grid.MultiSelect = false;
            this.log_grid.Name = "log_grid";
            this.log_grid.ReadOnly = true;
            this.log_grid.RowHeadersVisible = false;
            this.log_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log_grid.Size = new System.Drawing.Size(683, 388);
            this.log_grid.TabIndex = 1;
            this.log_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.log_grid_CellContentClick);
            // 
            // D
            // 
            this.D.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.D.DefaultCellStyle = dataGridViewCellStyle2;
            this.D.HeaderText = "Date";
            this.D.Name = "D";
            this.D.ReadOnly = true;
            this.D.Width = 135;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "Transaction Description";
            this.Column1.MinimumWidth = 180;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // clear_log_button
            // 
            this.clear_log_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_log_button.Location = new System.Drawing.Point(371, 2);
            this.clear_log_button.Name = "clear_log_button";
            this.clear_log_button.Size = new System.Drawing.Size(82, 20);
            this.clear_log_button.TabIndex = 220;
            this.clear_log_button.Text = "Clear Log";
            this.clear_log_button.UseVisualStyleBackColor = true;
            this.clear_log_button.Visible = false;
            this.clear_log_button.Click += new System.EventHandler(this.clear_log_button_Click);
            // 
            // search_box
            // 
            this.search_box.BackColor = System.Drawing.Color.White;
            this.search_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_box.Location = new System.Drawing.Point(73, 2);
            this.search_box.Name = "search_box";
            this.search_box.Size = new System.Drawing.Size(129, 20);
            this.search_box.TabIndex = 221;
            this.search_box.TextChanged += new System.EventHandler(this.search_box_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 222;
            this.label1.Text = "Search Log:";
            // 
            // clear_90_days
            // 
            this.clear_90_days.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_90_days.Location = new System.Drawing.Point(458, 2);
            this.clear_90_days.Name = "clear_90_days";
            this.clear_90_days.Size = new System.Drawing.Size(105, 20);
            this.clear_90_days.TabIndex = 223;
            this.clear_90_days.Text = "Clear last 30 days";
            this.clear_90_days.UseVisualStyleBackColor = true;
            this.clear_90_days.Click += new System.EventHandler(this.clear_90_days_Click);
            // 
            // search_count_box
            // 
            this.search_count_box.Location = new System.Drawing.Point(207, 5);
            this.search_count_box.Name = "search_count_box";
            this.search_count_box.Size = new System.Drawing.Size(91, 19);
            this.search_count_box.TabIndex = 224;
            this.search_count_box.Text = "0 item(s) found";
            this.search_count_box.Visible = false;
            // 
            // show_id
            // 
            this.show_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.show_id.AutoSize = true;
            this.show_id.Location = new System.Drawing.Point(569, 4);
            this.show_id.Name = "show_id";
            this.show_id.Size = new System.Drawing.Size(107, 17);
            this.show_id.TabIndex = 225;
            this.show_id.Text = "Show Session ID";
            this.show_id.UseVisualStyleBackColor = true;
            this.show_id.CheckedChanged += new System.EventHandler(this.show_id_CheckedChanged);
            // 
            // Action_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 411);
            this.Controls.Add(this.show_id);
            this.Controls.Add(this.search_count_box);
            this.Controls.Add(this.clear_90_days);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search_box);
            this.Controls.Add(this.clear_log_button);
            this.Controls.Add(this.log_grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(696, 333);
            this.Name = "Action_Log";
            this.Text = "View Log";
            ((System.ComponentModel.ISupportInitialize)(this.log_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView log_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button clear_log_button;
        private System.Windows.Forms.TextBox search_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clear_90_days;
        private System.Windows.Forms.Label search_count_box;
        private System.Windows.Forms.CheckBox show_id;
    }
}