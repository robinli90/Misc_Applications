namespace Translator
{
    partial class Add_Loop
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
            this.bin_value_box = new System.Windows.Forms.TextBox();
            this.richTextBox24 = new System.Windows.Forms.RichTextBox();
            this.condition_direction = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.loop_static_box = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.richTextBox23 = new System.Windows.Forms.RichTextBox();
            this.bin_box = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Add_Loop_Button = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // bin_value_box
            // 
            this.bin_value_box.BackColor = System.Drawing.Color.Silver;
            this.bin_value_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bin_value_box.Location = new System.Drawing.Point(532, 28);
            this.bin_value_box.MaxLength = 12;
            this.bin_value_box.Name = "bin_value_box";
            this.bin_value_box.Size = new System.Drawing.Size(34, 20);
            this.bin_value_box.TabIndex = 82;
            this.bin_value_box.TextChanged += new System.EventHandler(this.condition_value_TextChanged);
            // 
            // richTextBox24
            // 
            this.richTextBox24.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox24.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox24.Location = new System.Drawing.Point(438, 31);
            this.richTextBox24.Name = "richTextBox24";
            this.richTextBox24.ReadOnly = true;
            this.richTextBox24.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox24.Size = new System.Drawing.Size(91, 18);
            this.richTextBox24.TabIndex = 81;
            this.richTextBox24.Text = "the following value";
            // 
            // condition_direction
            // 
            this.condition_direction.BackColor = System.Drawing.Color.Silver;
            this.condition_direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.condition_direction.FormattingEnabled = true;
            this.condition_direction.Location = new System.Drawing.Point(339, 27);
            this.condition_direction.Name = "condition_direction";
            this.condition_direction.Size = new System.Drawing.Size(93, 21);
            this.condition_direction.TabIndex = 80;
            this.condition_direction.SelectedIndexChanged += new System.EventHandler(this.condition_direction_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBox2);
            this.groupBox4.Controls.Add(this.richTextBox1);
            this.groupBox4.Controls.Add(this.loop_static_box);
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.richTextBox23);
            this.groupBox4.Controls.Add(this.bin_value_box);
            this.groupBox4.Controls.Add(this.bin_box);
            this.groupBox4.Controls.Add(this.richTextBox24);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.condition_direction);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(682, 79);
            this.groupBox4.TabIndex = 83;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Loop Exit Condition";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.richTextBox2.Location = new System.Drawing.Point(261, 50);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(415, 18);
            this.richTextBox2.TabIndex = 86;
            this.richTextBox2.Text = "*Use with caution as it will easily loop indefinitely if variable is not changed " +
    "within loop";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(116, 55);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(61, 18);
            this.richTextBox1.TabIndex = 85;
            this.richTextBox1.Text = "times";
            // 
            // loop_static_box
            // 
            this.loop_static_box.BackColor = System.Drawing.Color.Silver;
            this.loop_static_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loop_static_box.Location = new System.Drawing.Point(67, 51);
            this.loop_static_box.MaxLength = 6;
            this.loop_static_box.Name = "loop_static_box";
            this.loop_static_box.Size = new System.Drawing.Size(44, 20);
            this.loop_static_box.TabIndex = 84;
            this.loop_static_box.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(20, 53);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 20);
            this.checkBox2.TabIndex = 83;
            this.checkBox2.Text = "Loop ";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // richTextBox23
            // 
            this.richTextBox23.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox23.Location = new System.Drawing.Point(320, 30);
            this.richTextBox23.Name = "richTextBox23";
            this.richTextBox23.ReadOnly = true;
            this.richTextBox23.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox23.Size = new System.Drawing.Size(13, 18);
            this.richTextBox23.TabIndex = 82;
            this.richTextBox23.Text = "is ";
            // 
            // bin_box
            // 
            this.bin_box.BackColor = System.Drawing.Color.Silver;
            this.bin_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bin_box.Location = new System.Drawing.Point(280, 27);
            this.bin_box.MaxLength = 4;
            this.bin_box.Name = "bin_box";
            this.bin_box.Size = new System.Drawing.Size(34, 20);
            this.bin_box.TabIndex = 81;
            this.bin_box.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(20, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(254, 20);
            this.checkBox1.TabIndex = 80;
            this.checkBox1.Text = "Exit loop when value of a variable stored in bin#";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Add_Loop_Button
            // 
            this.Add_Loop_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_Loop_Button.Location = new System.Drawing.Point(593, 97);
            this.Add_Loop_Button.Name = "Add_Loop_Button";
            this.Add_Loop_Button.Size = new System.Drawing.Size(101, 26);
            this.Add_Loop_Button.TabIndex = 84;
            this.Add_Loop_Button.Text = "Add Loop";
            this.Add_Loop_Button.UseVisualStyleBackColor = true;
            this.Add_Loop_Button.Click += new System.EventHandler(this.button20_Click);
            // 
            // Add_Loop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(703, 127);
            this.Controls.Add(this.Add_Loop_Button);
            this.Controls.Add(this.groupBox4);
            this.Name = "Add_Loop";
            this.Text = "Add_Loop";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox bin_value_box;
        private System.Windows.Forms.RichTextBox richTextBox24;
        private System.Windows.Forms.ComboBox condition_direction;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox bin_box;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox loop_static_box;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RichTextBox richTextBox23;
        private System.Windows.Forms.Button Add_Loop_Button;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}