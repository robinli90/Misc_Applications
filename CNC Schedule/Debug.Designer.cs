namespace CNC_Schedule
{
    partial class Debug
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
            this.debug_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // debug_text
            // 
            this.debug_text.Location = new System.Drawing.Point(12, 12);
            this.debug_text.Multiline = true;
            this.debug_text.Name = "debug_text";
            this.debug_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.debug_text.Size = new System.Drawing.Size(1034, 619);
            this.debug_text.TabIndex = 0;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 656);
            this.Controls.Add(this.debug_text);
            this.Name = "Debug";
            this.Text = "Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox debug_text;
    }
}