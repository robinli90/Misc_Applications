namespace CNC_Schedule
{
    partial class Assign_Task
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
            this.employee_List = new System.Windows.Forms.ListBox();
            this.assign_button = new System.Windows.Forms.Button();
            this.unassign_button = new System.Windows.Forms.Button();
            this.panic = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // employee_List
            // 
            this.employee_List.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_List.FormattingEnabled = true;
            this.employee_List.ItemHeight = 18;
            this.employee_List.Location = new System.Drawing.Point(11, 12);
            this.employee_List.Name = "employee_List";
            this.employee_List.Size = new System.Drawing.Size(177, 346);
            this.employee_List.TabIndex = 0;
            this.employee_List.SelectedIndexChanged += new System.EventHandler(this.employee_List_SelectedIndexChanged);
            // 
            // assign_button
            // 
            this.assign_button.Location = new System.Drawing.Point(105, 390);
            this.assign_button.Name = "assign_button";
            this.assign_button.Size = new System.Drawing.Size(84, 23);
            this.assign_button.TabIndex = 1;
            this.assign_button.Text = "Assign";
            this.assign_button.UseVisualStyleBackColor = true;
            this.assign_button.Click += new System.EventHandler(this.assign_button_Click);
            // 
            // unassign_button
            // 
            this.unassign_button.Location = new System.Drawing.Point(13, 390);
            this.unassign_button.Name = "unassign_button";
            this.unassign_button.Size = new System.Drawing.Size(86, 23);
            this.unassign_button.TabIndex = 2;
            this.unassign_button.Text = "Unassign";
            this.unassign_button.UseVisualStyleBackColor = true;
            this.unassign_button.Click += new System.EventHandler(this.unassign_button_Click);
            // 
            // panic
            // 
            this.panic.AutoSize = true;
            this.panic.Location = new System.Drawing.Point(121, 362);
            this.panic.Name = "panic";
            this.panic.Size = new System.Drawing.Size(73, 17);
            this.panic.TabIndex = 3;
            this.panic.Text = "Panic Job";
            this.panic.UseVisualStyleBackColor = true;
            // 
            // Assign_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 421);
            this.Controls.Add(this.panic);
            this.Controls.Add(this.unassign_button);
            this.Controls.Add(this.assign_button);
            this.Controls.Add(this.employee_List);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Assign_Task";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assign Die ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox employee_List;
        private System.Windows.Forms.Button assign_button;
        private System.Windows.Forms.Button unassign_button;
        private System.Windows.Forms.CheckBox panic;
    }
}