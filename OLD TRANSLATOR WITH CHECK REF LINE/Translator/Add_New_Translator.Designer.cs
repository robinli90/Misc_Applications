namespace Translator
{
    partial class translate_line
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.RichTextBox();
            this.error2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(115, 18);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Name of Translator:";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.Silver;
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.Location = new System.Drawing.Point(117, 9);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(157, 20);
            this.name.TabIndex = 1;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Translator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // error
            // 
            this.error.BackColor = System.Drawing.Color.DarkGray;
            this.error.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(75, 31);
            this.error.Name = "error";
            this.error.ReadOnly = true;
            this.error.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.error.Size = new System.Drawing.Size(100, 29);
            this.error.TabIndex = 4;
            this.error.Text = "Translator with same name already exists";
            this.error.Visible = false;
            // 
            // error2
            // 
            this.error2.BackColor = System.Drawing.Color.DarkGray;
            this.error2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error2.ForeColor = System.Drawing.Color.Red;
            this.error2.Location = new System.Drawing.Point(101, 37);
            this.error2.Name = "error2";
            this.error2.ReadOnly = true;
            this.error2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.error2.Size = new System.Drawing.Size(74, 15);
            this.error2.TabIndex = 5;
            this.error2.Text = "Name missing";
            this.error2.Visible = false;
            // 
            // translate_line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(287, 66);
            this.Controls.Add(this.error2);
            this.Controls.Add(this.error);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "translate_line";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Translator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox error;
        private System.Windows.Forms.RichTextBox error2;
    }
}