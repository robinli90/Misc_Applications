namespace ITManagement
{
    partial class Edit_Stations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit_Stations));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.add_station_button = new System.Windows.Forms.Button();
            this.station_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.remove_station = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.add_station_button);
            this.groupBox1.Controls.Add(this.station_type);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Station";
            // 
            // add_station_button
            // 
            this.add_station_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_station_button.Location = new System.Drawing.Point(139, 55);
            this.add_station_button.Name = "add_station_button";
            this.add_station_button.Size = new System.Drawing.Size(79, 26);
            this.add_station_button.TabIndex = 7;
            this.add_station_button.Text = "Add Station";
            this.add_station_button.UseVisualStyleBackColor = true;
            this.add_station_button.Click += new System.EventHandler(this.add_station_button_Click);
            // 
            // station_type
            // 
            this.station_type.BackColor = System.Drawing.Color.Silver;
            this.station_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.station_type.FormattingEnabled = true;
            this.station_type.Location = new System.Drawing.Point(95, 28);
            this.station_type.Name = "station_type";
            this.station_type.Size = new System.Drawing.Size(123, 21);
            this.station_type.TabIndex = 215;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 214;
            this.label2.Text = "Station Size:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.remove_station);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(5, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 76);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Existing Stations";
            // 
            // remove_station
            // 
            this.remove_station.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove_station.Location = new System.Drawing.Point(127, 30);
            this.remove_station.Name = "remove_station";
            this.remove_station.Size = new System.Drawing.Size(79, 26);
            this.remove_station.TabIndex = 8;
            this.remove_station.Text = "Remove";
            this.remove_station.UseVisualStyleBackColor = true;
            this.remove_station.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(29, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 26);
            this.button1.TabIndex = 7;
            this.button1.Text = "Rearrange";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Edit_Stations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 191);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Edit_Stations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Stations";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox station_type;
        private System.Windows.Forms.Button add_station_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button remove_station;
        private System.Windows.Forms.Button button1;

    }
}