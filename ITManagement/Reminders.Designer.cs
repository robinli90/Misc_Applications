namespace ITManagement
{
    partial class Reminders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reminders));
            this.ok_button = new System.Windows.Forms.Button();
            this.no_remind_today = new System.Windows.Forms.CheckBox();
            this.reminder_box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ok_button.Location = new System.Drawing.Point(228, 320);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(54, 22);
            this.ok_button.TabIndex = 4;
            this.ok_button.TabStop = false;
            this.ok_button.Text = "Ok";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // no_remind_today
            // 
            this.no_remind_today.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.no_remind_today.AutoSize = true;
            this.no_remind_today.Location = new System.Drawing.Point(91, 322);
            this.no_remind_today.Name = "no_remind_today";
            this.no_remind_today.Size = new System.Drawing.Size(131, 17);
            this.no_remind_today.TabIndex = 234;
            this.no_remind_today.TabStop = false;
            this.no_remind_today.Text = "Don\'t remind me today";
            this.no_remind_today.UseVisualStyleBackColor = true;
            // 
            // reminder_box
            // 
            this.reminder_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reminder_box.BackColor = System.Drawing.SystemColors.Control;
            this.reminder_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reminder_box.Location = new System.Drawing.Point(6, 4);
            this.reminder_box.Name = "reminder_box";
            this.reminder_box.ReadOnly = true;
            this.reminder_box.Size = new System.Drawing.Size(279, 306);
            this.reminder_box.TabIndex = 235;
            this.reminder_box.Text = "";
            // 
            // Reminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 349);
            this.Controls.Add(this.reminder_box);
            this.Controls.Add(this.no_remind_today);
            this.Controls.Add(this.ok_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reminders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reminders";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Reminders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.CheckBox no_remind_today;
        private System.Windows.Forms.RichTextBox reminder_box;
    }
}