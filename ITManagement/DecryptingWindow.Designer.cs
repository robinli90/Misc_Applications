namespace ITManagement
{
    partial class DecryptingWindow
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
            this.dec_Box = new System.Windows.Forms.RichTextBox();
            this.Decrypt_Button = new System.Windows.Forms.Button();
            this.dec_file = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dec_Box
            // 
            this.dec_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dec_Box.BackColor = System.Drawing.SystemColors.Control;
            this.dec_Box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dec_Box.Location = new System.Drawing.Point(2, 2);
            this.dec_Box.Name = "dec_Box";
            this.dec_Box.Size = new System.Drawing.Size(865, 546);
            this.dec_Box.TabIndex = 237;
            this.dec_Box.Text = "";
            // 
            // Decrypt_Button
            // 
            this.Decrypt_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Decrypt_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Decrypt_Button.Location = new System.Drawing.Point(751, 554);
            this.Decrypt_Button.Name = "Decrypt_Button";
            this.Decrypt_Button.Size = new System.Drawing.Size(116, 22);
            this.Decrypt_Button.TabIndex = 236;
            this.Decrypt_Button.TabStop = false;
            this.Decrypt_Button.Text = "Decrypt Text";
            this.Decrypt_Button.UseVisualStyleBackColor = true;
            this.Decrypt_Button.Click += new System.EventHandler(this.Decrypt_Button_Click);
            // 
            // dec_file
            // 
            this.dec_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dec_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dec_file.Location = new System.Drawing.Point(12, 554);
            this.dec_file.Name = "dec_file";
            this.dec_file.Size = new System.Drawing.Size(116, 22);
            this.dec_file.TabIndex = 238;
            this.dec_file.TabStop = false;
            this.dec_file.Text = "Decrypt File";
            this.dec_file.UseVisualStyleBackColor = true;
            this.dec_file.Click += new System.EventHandler(this.dec_file_Click);
            // 
            // DecryptingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 579);
            this.Controls.Add(this.dec_file);
            this.Controls.Add(this.dec_Box);
            this.Controls.Add(this.Decrypt_Button);
            this.Name = "DecryptingWindow";
            this.Text = "DecryptingWindow";
            this.Load += new System.EventHandler(this.DecryptingWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox dec_Box;
        private System.Windows.Forms.Button Decrypt_Button;
        private System.Windows.Forms.Button dec_file;
    }
}