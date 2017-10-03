namespace Program_Repeater
{
    partial class Program_Choice
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.program_button = new System.Windows.Forms.Button();
            this.console_button = new System.Windows.Forms.Button();
            this.file_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Select the appropriate program you wish to add:";
            // 
            // program_button
            // 
            this.program_button.Location = new System.Drawing.Point(12, 38);
            this.program_button.Name = "program_button";
            this.program_button.Size = new System.Drawing.Size(102, 45);
            this.program_button.TabIndex = 1;
            this.program_button.Text = "Run my own program";
            this.program_button.UseVisualStyleBackColor = true;
            this.program_button.Click += new System.EventHandler(this.program_button_Click);
            // 
            // console_button
            // 
            this.console_button.Location = new System.Drawing.Point(129, 89);
            this.console_button.Name = "console_button";
            this.console_button.Size = new System.Drawing.Size(102, 45);
            this.console_button.TabIndex = 2;
            this.console_button.Text = "Run a console command";
            this.console_button.UseVisualStyleBackColor = true;
            this.console_button.Visible = false;
            // 
            // file_button
            // 
            this.file_button.Location = new System.Drawing.Point(129, 38);
            this.file_button.Name = "file_button";
            this.file_button.Size = new System.Drawing.Size(102, 45);
            this.file_button.TabIndex = 3;
            this.file_button.Text = "Run file handler";
            this.file_button.UseVisualStyleBackColor = true;
            this.file_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // Program_Choice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(243, 90);
            this.Controls.Add(this.file_button);
            this.Controls.Add(this.console_button);
            this.Controls.Add(this.program_button);
            this.Controls.Add(this.textBox1);
            this.Name = "Program_Choice";
            this.Text = "Choose functionality";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button program_button;
        private System.Windows.Forms.Button console_button;
        private System.Windows.Forms.Button file_button;
    }
}