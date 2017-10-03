namespace ITManagement
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.backup_location_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.db_login = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.db_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.db_default_db = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.save_exit_button = new System.Windows.Forms.Button();
            this.load_backup = new System.Windows.Forms.Button();
            this.decrypt_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // backup_location_box
            // 
            this.backup_location_box.BackColor = System.Drawing.SystemColors.Control;
            this.backup_location_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backup_location_box.Location = new System.Drawing.Point(112, 11);
            this.backup_location_box.Name = "backup_location_box";
            this.backup_location_box.ReadOnly = true;
            this.backup_location_box.Size = new System.Drawing.Size(357, 20);
            this.backup_location_box.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backup Location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "Database";
            // 
            // db_login
            // 
            this.db_login.BackColor = System.Drawing.SystemColors.Control;
            this.db_login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.db_login.Location = new System.Drawing.Point(71, 74);
            this.db_login.Name = "db_login";
            this.db_login.Size = new System.Drawing.Size(59, 20);
            this.db_login.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Login:";
            // 
            // db_password
            // 
            this.db_password.BackColor = System.Drawing.SystemColors.Control;
            this.db_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.db_password.Location = new System.Drawing.Point(93, 99);
            this.db_password.Name = "db_password";
            this.db_password.PasswordChar = '*';
            this.db_password.Size = new System.Drawing.Size(77, 20);
            this.db_password.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 226;
            this.label4.Text = "Password:";
            // 
            // db_default_db
            // 
            this.db_default_db.BackColor = System.Drawing.Color.Silver;
            this.db_default_db.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.db_default_db.FormattingEnabled = true;
            this.db_default_db.Location = new System.Drawing.Point(129, 123);
            this.db_default_db.Name = "db_default_db";
            this.db_default_db.Size = new System.Drawing.Size(134, 21);
            this.db_default_db.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 228;
            this.label5.Text = "Default Database:";
            // 
            // save_exit_button
            // 
            this.save_exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_exit_button.Location = new System.Drawing.Point(368, 126);
            this.save_exit_button.Name = "save_exit_button";
            this.save_exit_button.Size = new System.Drawing.Size(101, 22);
            this.save_exit_button.TabIndex = 5;
            this.save_exit_button.Text = "Save and Exit";
            this.save_exit_button.UseVisualStyleBackColor = true;
            this.save_exit_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // load_backup
            // 
            this.load_backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load_backup.Location = new System.Drawing.Point(368, 37);
            this.load_backup.Name = "load_backup";
            this.load_backup.Size = new System.Drawing.Size(102, 22);
            this.load_backup.TabIndex = 229;
            this.load_backup.Text = "Load Backup File";
            this.load_backup.UseVisualStyleBackColor = true;
            this.load_backup.Click += new System.EventHandler(this.load_backup_Click);
            // 
            // decrypt_button
            // 
            this.decrypt_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decrypt_button.Location = new System.Drawing.Point(391, 189);
            this.decrypt_button.Name = "decrypt_button";
            this.decrypt_button.Size = new System.Drawing.Size(78, 22);
            this.decrypt_button.TabIndex = 230;
            this.decrypt_button.Text = "Decrypt";
            this.decrypt_button.UseVisualStyleBackColor = true;
            this.decrypt_button.Click += new System.EventHandler(this.decrypt_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 231;
            this.label6.Text = "Decrypt Text";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 232;
            this.label7.Text = "Decrypt Key:";
            // 
            // dt
            // 
            this.dt.BackColor = System.Drawing.SystemColors.Control;
            this.dt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dt.Location = new System.Drawing.Point(84, 190);
            this.dt.Name = "dt";
            this.dt.Size = new System.Drawing.Size(301, 20);
            this.dt.TabIndex = 233;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 221);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.decrypt_button);
            this.Controls.Add(this.load_backup);
            this.Controls.Add(this.save_exit_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.db_default_db);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.db_password);
            this.Controls.Add(this.db_login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backup_location_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox backup_location_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox db_login;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox db_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox db_default_db;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button save_exit_button;
        private System.Windows.Forms.Button load_backup;
        private System.Windows.Forms.Button decrypt_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dt;
    }
}