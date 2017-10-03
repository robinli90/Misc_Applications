namespace ITManagement
{
    partial class AddReminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddReminder));
            this.label1 = new System.Windows.Forms.Label();
            this.description_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.remind_who = new System.Windows.Forms.ComboBox();
            this.add_reminder_button = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 224;
            this.label1.Text = "Description:";
            // 
            // description_box
            // 
            this.description_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description_box.BackColor = System.Drawing.Color.White;
            this.description_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.description_box.Location = new System.Drawing.Point(75, 60);
            this.description_box.Multiline = true;
            this.description_box.Name = "description_box";
            this.description_box.Size = new System.Drawing.Size(263, 57);
            this.description_box.TabIndex = 223;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 225;
            this.label2.Text = "Remind:";
            // 
            // remind_who
            // 
            this.remind_who.BackColor = System.Drawing.Color.White;
            this.remind_who.FormattingEnabled = true;
            this.remind_who.Location = new System.Drawing.Point(75, 6);
            this.remind_who.Name = "remind_who";
            this.remind_who.Size = new System.Drawing.Size(134, 21);
            this.remind_who.TabIndex = 226;
            this.remind_who.TabStop = false;
            // 
            // add_reminder_button
            // 
            this.add_reminder_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add_reminder_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_reminder_button.Location = new System.Drawing.Point(234, 4);
            this.add_reminder_button.Name = "add_reminder_button";
            this.add_reminder_button.Size = new System.Drawing.Size(104, 22);
            this.add_reminder_button.TabIndex = 233;
            this.add_reminder_button.TabStop = false;
            this.add_reminder_button.Text = "Add Reminder";
            this.add_reminder_button.UseVisualStyleBackColor = true;
            this.add_reminder_button.Click += new System.EventHandler(this.add_reminder_button_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 33);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(134, 20);
            this.dateTimePicker1.TabIndex = 234;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 235;
            this.label3.Text = "Date:";
            // 
            // AddReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 125);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.add_reminder_button);
            this.Controls.Add(this.remind_who);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.description_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddReminder";
            this.Text = "Add a New Reminder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox description_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox remind_who;
        private System.Windows.Forms.Button add_reminder_button;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
    }
}