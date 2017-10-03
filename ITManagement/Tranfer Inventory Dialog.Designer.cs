namespace ITManagement
{
    partial class Tranfer_Inventory_Dialog
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
            this.transfer_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.note_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // transfer_button
            // 
            this.transfer_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transfer_button.Location = new System.Drawing.Point(171, 55);
            this.transfer_button.Name = "transfer_button";
            this.transfer_button.Size = new System.Drawing.Size(78, 22);
            this.transfer_button.TabIndex = 217;
            this.transfer_button.Text = "Transfer Item";
            this.transfer_button.UseVisualStyleBackColor = true;
            this.transfer_button.Click += new System.EventHandler(this.transfer_button_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 32);
            this.label1.TabIndex = 218;
            this.label1.Text = "Select a station which you want to transfer item to:";
            // 
            // note_box
            // 
            this.note_box.BackColor = System.Drawing.Color.White;
            this.note_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.note_box.Location = new System.Drawing.Point(61, 28);
            this.note_box.Name = "note_box";
            this.note_box.Size = new System.Drawing.Size(188, 20);
            this.note_box.TabIndex = 219;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 220;
            this.label2.Text = "Remark:";
            // 
            // Tranfer_Inventory_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 85);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.note_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.transfer_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Tranfer_Inventory_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tranfer Item";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Tranfer_Inventory_Dialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button transfer_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox note_box;
        private System.Windows.Forms.Label label2;
    }
}