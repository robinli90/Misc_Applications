namespace Translator
{
    partial class Add_Global_Condition
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.add_condition = new System.Windows.Forms.Button();
            this.value_text = new System.Windows.Forms.TextBox();
            this.condition_box = new System.Windows.Forms.ComboBox();
            this.count_box = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.add_condition);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.value_text);
            this.groupBox1.Controls.Add(this.count_box);
            this.groupBox1.Controls.Add(this.condition_box);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 117);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Condition";
            // 
            // add_condition
            // 
            this.add_condition.Location = new System.Drawing.Point(255, 72);
            this.add_condition.Name = "add_condition";
            this.add_condition.Size = new System.Drawing.Size(73, 35);
            this.add_condition.TabIndex = 12;
            this.add_condition.Text = "Add Global Condition";
            this.add_condition.UseVisualStyleBackColor = true;
            this.add_condition.Click += new System.EventHandler(this.add_condition_Click);
            // 
            // value_text
            // 
            this.value_text.BackColor = System.Drawing.Color.Silver;
            this.value_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.value_text.Location = new System.Drawing.Point(239, 43);
            this.value_text.Name = "value_text";
            this.value_text.Size = new System.Drawing.Size(89, 20);
            this.value_text.TabIndex = 2;
            // 
            // condition_box
            // 
            this.condition_box.BackColor = System.Drawing.Color.Silver;
            this.condition_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.condition_box.FormattingEnabled = true;
            this.condition_box.Location = new System.Drawing.Point(12, 42);
            this.condition_box.Name = "condition_box";
            this.condition_box.Size = new System.Drawing.Size(93, 21);
            this.condition_box.TabIndex = 9;
            // 
            // count_box
            // 
            this.count_box.BackColor = System.Drawing.Color.Silver;
            this.count_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.count_box.FormattingEnabled = true;
            this.count_box.Location = new System.Drawing.Point(111, 42);
            this.count_box.Name = "count_box";
            this.count_box.Size = new System.Drawing.Size(50, 21);
            this.count_box.TabIndex = 10;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(13, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(321, 14);
            this.richTextBox1.TabIndex = 48;
            this.richTextBox1.Text = "This rule will only execute if the entire file contains ";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(167, 48);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(69, 18);
            this.richTextBox2.TabIndex = 49;
            this.richTextBox2.Text = "instance(s) of ";
            // 
            // Add_Global_Condition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(360, 135);
            this.Controls.Add(this.groupBox1);
            this.Name = "Add_Global_Condition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add_Global_Condition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button add_condition;
        private System.Windows.Forms.TextBox value_text;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox count_box;
        private System.Windows.Forms.ComboBox condition_box;
    }
}