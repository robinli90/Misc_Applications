namespace Translator
{
    partial class Group_Condition
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.condition_bin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.condition_action = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.condition_value = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(507, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adding a group condition means that this group function will execute only if the " +
    "condition is satisfied below:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Condtion: The value of bin#";
            // 
            // condition_bin
            // 
            this.condition_bin.Location = new System.Drawing.Point(176, 30);
            this.condition_bin.Name = "condition_bin";
            this.condition_bin.Size = new System.Drawing.Size(60, 20);
            this.condition_bin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "is";
            // 
            // condition_action
            // 
            this.condition_action.FormattingEnabled = true;
            this.condition_action.Location = new System.Drawing.Point(262, 29);
            this.condition_action.Name = "condition_action";
            this.condition_action.Size = new System.Drawing.Size(89, 21);
            this.condition_action.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "the value of";
            // 
            // condition_value
            // 
            this.condition_value.Location = new System.Drawing.Point(426, 30);
            this.condition_value.Name = "condition_value";
            this.condition_value.Size = new System.Drawing.Size(60, 20);
            this.condition_value.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(444, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(330, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Remove Condition";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Group_Condition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 88);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.condition_value);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.condition_action);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.condition_bin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Group_Condition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit Group Condition";
            this.Load += new System.EventHandler(this.Group_Condition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox condition_bin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox condition_action;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox condition_value;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}