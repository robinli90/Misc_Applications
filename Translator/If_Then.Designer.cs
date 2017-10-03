namespace Translator
{
    partial class If_Then
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.copy_button = new System.Windows.Forms.Button();
            this.down_button = new System.Windows.Forms.Button();
            this.up_button = new System.Windows.Forms.Button();
            this.richTextBox14 = new System.Windows.Forms.RichTextBox();
            this.delete_button = new System.Windows.Forms.Button();
            this.statement_list = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.condition_comparison = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reference_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.condition_bin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.condition_value = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.action_comparison = new System.Windows.Forms.ComboBox();
            this.action_bin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.action_value = new System.Windows.Forms.TextBox();
            this.by_to = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.add_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.save_edit_button = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.comment_box = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.copy_button);
            this.groupBox2.Controls.Add(this.down_button);
            this.groupBox2.Controls.Add(this.up_button);
            this.groupBox2.Controls.Add(this.richTextBox14);
            this.groupBox2.Controls.Add(this.delete_button);
            this.groupBox2.Controls.Add(this.statement_list);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(11, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 152);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statements";
            // 
            // copy_button
            // 
            this.copy_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copy_button.Location = new System.Drawing.Point(685, 124);
            this.copy_button.Name = "copy_button";
            this.copy_button.Size = new System.Drawing.Size(67, 22);
            this.copy_button.TabIndex = 70;
            this.copy_button.Text = "Copy";
            this.copy_button.UseVisualStyleBackColor = true;
            this.copy_button.Click += new System.EventHandler(this.copy_button_Click);
            // 
            // down_button
            // 
            this.down_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.down_button.Location = new System.Drawing.Point(639, 124);
            this.down_button.Name = "down_button";
            this.down_button.Size = new System.Drawing.Size(44, 22);
            this.down_button.TabIndex = 69;
            this.down_button.Text = "Down";
            this.down_button.UseVisualStyleBackColor = true;
            this.down_button.Click += new System.EventHandler(this.down_button_Click);
            // 
            // up_button
            // 
            this.up_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.up_button.Location = new System.Drawing.Point(593, 124);
            this.up_button.Name = "up_button";
            this.up_button.Size = new System.Drawing.Size(44, 22);
            this.up_button.TabIndex = 68;
            this.up_button.Text = "Up";
            this.up_button.UseVisualStyleBackColor = true;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // richTextBox14
            // 
            this.richTextBox14.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox14.ForeColor = System.Drawing.Color.Blue;
            this.richTextBox14.Location = new System.Drawing.Point(442, 129);
            this.richTextBox14.Name = "richTextBox14";
            this.richTextBox14.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox14.Size = new System.Drawing.Size(150, 14);
            this.richTextBox14.TabIndex = 67;
            this.richTextBox14.Text = "*Double-click Condition to Edit";
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_button.Location = new System.Drawing.Point(754, 124);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(67, 22);
            this.delete_button.TabIndex = 66;
            this.delete_button.Text = "Delete Selected Condition";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // statement_list
            // 
            this.statement_list.BackColor = System.Drawing.Color.DarkGray;
            this.statement_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statement_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statement_list.FormattingEnabled = true;
            this.statement_list.HorizontalScrollbar = true;
            this.statement_list.Location = new System.Drawing.Point(6, 16);
            this.statement_list.Name = "statement_list";
            this.statement_list.Size = new System.Drawing.Size(815, 104);
            this.statement_list.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(6, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(406, 20);
            this.label8.TabIndex = 68;
            this.label8.Text = "First statement is IF and all consecutive statements are  THEN";
            // 
            // condition_comparison
            // 
            this.condition_comparison.BackColor = System.Drawing.Color.Silver;
            this.condition_comparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.condition_comparison.FormattingEnabled = true;
            this.condition_comparison.Location = new System.Drawing.Point(200, 44);
            this.condition_comparison.Name = "condition_comparison";
            this.condition_comparison.Size = new System.Drawing.Size(55, 21);
            this.condition_comparison.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 18);
            this.label1.TabIndex = 59;
            this.label1.Text = "If the reference line contains:";
            // 
            // reference_box
            // 
            this.reference_box.BackColor = System.Drawing.Color.Silver;
            this.reference_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reference_box.Location = new System.Drawing.Point(159, 12);
            this.reference_box.Name = "reference_box";
            this.reference_box.Size = new System.Drawing.Size(110, 20);
            this.reference_box.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(47, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 61;
            this.label2.Text = "If value in bin #";
            // 
            // condition_bin
            // 
            this.condition_bin.BackColor = System.Drawing.Color.Silver;
            this.condition_bin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.condition_bin.Location = new System.Drawing.Point(128, 44);
            this.condition_bin.Name = "condition_bin";
            this.condition_bin.Size = new System.Drawing.Size(49, 20);
            this.condition_bin.TabIndex = 62;
            this.condition_bin.TextChanged += new System.EventHandler(this.condition_bin_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(183, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 18);
            this.label3.TabIndex = 63;
            this.label3.Text = "is";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(261, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 18);
            this.label4.TabIndex = 64;
            this.label4.Text = "value of";
            // 
            // condition_value
            // 
            this.condition_value.BackColor = System.Drawing.Color.Silver;
            this.condition_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.condition_value.Location = new System.Drawing.Point(307, 45);
            this.condition_value.Name = "condition_value";
            this.condition_value.Size = new System.Drawing.Size(49, 20);
            this.condition_value.TabIndex = 65;
            this.condition_value.TextChanged += new System.EventHandler(this.condition_value_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(362, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 18);
            this.label5.TabIndex = 66;
            this.label5.Text = "then ";
            // 
            // action_comparison
            // 
            this.action_comparison.BackColor = System.Drawing.Color.Silver;
            this.action_comparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.action_comparison.FormattingEnabled = true;
            this.action_comparison.Location = new System.Drawing.Point(394, 44);
            this.action_comparison.Name = "action_comparison";
            this.action_comparison.Size = new System.Drawing.Size(89, 21);
            this.action_comparison.TabIndex = 67;
            // 
            // action_bin
            // 
            this.action_bin.BackColor = System.Drawing.Color.Silver;
            this.action_bin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.action_bin.Location = new System.Drawing.Point(565, 44);
            this.action_bin.Name = "action_bin";
            this.action_bin.Size = new System.Drawing.Size(49, 20);
            this.action_bin.TabIndex = 69;
            this.action_bin.TextChanged += new System.EventHandler(this.action_bin_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(489, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 18);
            this.label6.TabIndex = 68;
            this.label6.Text = "value of bin #";
            // 
            // action_value
            // 
            this.action_value.BackColor = System.Drawing.Color.Silver;
            this.action_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.action_value.Location = new System.Drawing.Point(701, 44);
            this.action_value.Name = "action_value";
            this.action_value.Size = new System.Drawing.Size(49, 20);
            this.action_value.TabIndex = 71;
            this.action_value.TextChanged += new System.EventHandler(this.action_value_TextChanged);
            // 
            // by_to
            // 
            this.by_to.Location = new System.Drawing.Point(620, 47);
            this.by_to.Name = "by_to";
            this.by_to.Size = new System.Drawing.Size(18, 18);
            this.by_to.TabIndex = 70;
            this.by_to.Text = "by";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(636, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 18);
            this.label7.TabIndex = 72;
            this.label7.Text = "the value of";
            // 
            // add_button
            // 
            this.add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_button.Location = new System.Drawing.Point(779, 48);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(59, 22);
            this.add_button.TabIndex = 73;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(728, 234);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 22);
            this.button2.TabIndex = 74;
            this.button2.Text = "Save Condition";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // save_edit_button
            // 
            this.save_edit_button.Enabled = false;
            this.save_edit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_edit_button.Location = new System.Drawing.Point(779, 20);
            this.save_edit_button.Name = "save_edit_button";
            this.save_edit_button.Size = new System.Drawing.Size(59, 22);
            this.save_edit_button.TabIndex = 75;
            this.save_edit_button.Text = "Save";
            this.save_edit_button.UseVisualStyleBackColor = true;
            this.save_edit_button.Click += new System.EventHandler(this.save_edit_button_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(23, 237);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(98, 19);
            this.richTextBox2.TabIndex = 77;
            this.richTextBox2.Text = "Comment (optional):";
            // 
            // comment_box
            // 
            this.comment_box.BackColor = System.Drawing.Color.Silver;
            this.comment_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comment_box.Location = new System.Drawing.Point(125, 235);
            this.comment_box.Name = "comment_box";
            this.comment_box.Size = new System.Drawing.Size(437, 20);
            this.comment_box.TabIndex = 76;
            // 
            // If_Then
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(850, 262);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.comment_box);
            this.Controls.Add(this.save_edit_button);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.action_value);
            this.Controls.Add(this.by_to);
            this.Controls.Add(this.action_bin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.action_comparison);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.condition_value);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.condition_bin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reference_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.condition_comparison);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "If_Then";
            this.Text = "If/Then Condition ";
            this.Load += new System.EventHandler(this.If_Then_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button copy_button;
        private System.Windows.Forms.Button down_button;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.RichTextBox richTextBox14;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.ListBox statement_list;
        private System.Windows.Forms.ComboBox condition_comparison;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox reference_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox condition_bin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox condition_value;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox action_comparison;
        private System.Windows.Forms.TextBox action_bin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox action_value;
        private System.Windows.Forms.Label by_to;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button save_edit_button;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox comment_box;
    }
}