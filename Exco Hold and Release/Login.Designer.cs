namespace Exco_Hold_and_Release
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.employee_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.shop_order = new System.Windows.Forms.TextBox();
            this.edit_button = new System.Windows.Forms.Button();
            this.hold_button = new System.Windows.Forms.Button();
            this.release_button = new System.Windows.Forms.Button();
            this.print_checklist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 226;
            this.label1.Text = "Employee #";
            // 
            // employee_box
            // 
            this.employee_box.BackColor = System.Drawing.Color.White;
            this.employee_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.employee_box.Location = new System.Drawing.Point(88, 12);
            this.employee_box.MaxLength = 5;
            this.employee_box.Name = "employee_box";
            this.employee_box.Size = new System.Drawing.Size(76, 20);
            this.employee_box.TabIndex = 225;
            this.employee_box.TextChanged += new System.EventHandler(this.employee_number_TextChanged);
            this.employee_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.password_box_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 230;
            this.label3.Text = "Shop Order #:";
            this.label3.Visible = false;
            // 
            // shop_order
            // 
            this.shop_order.BackColor = System.Drawing.Color.White;
            this.shop_order.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shop_order.Location = new System.Drawing.Point(88, 12);
            this.shop_order.MaxLength = 6;
            this.shop_order.Name = "shop_order";
            this.shop_order.Size = new System.Drawing.Size(76, 20);
            this.shop_order.TabIndex = 229;
            this.shop_order.Visible = false;
            this.shop_order.TextChanged += new System.EventHandler(this.shop_order_TextChanged);
            // 
            // edit_button
            // 
            this.edit_button.Enabled = false;
            this.edit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit_button.Location = new System.Drawing.Point(11, 45);
            this.edit_button.Margin = new System.Windows.Forms.Padding(1);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(67, 38);
            this.edit_button.TabIndex = 2240;
            this.edit_button.Text = "Edit Form";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // hold_button
            // 
            this.hold_button.Enabled = false;
            this.hold_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hold_button.Location = new System.Drawing.Point(85, 45);
            this.hold_button.Margin = new System.Windows.Forms.Padding(1);
            this.hold_button.Name = "hold_button";
            this.hold_button.Size = new System.Drawing.Size(67, 38);
            this.hold_button.TabIndex = 2241;
            this.hold_button.Text = "Hold Job";
            this.hold_button.UseVisualStyleBackColor = true;
            this.hold_button.Click += new System.EventHandler(this.hold_button_Click);
            // 
            // release_button
            // 
            this.release_button.Enabled = false;
            this.release_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.release_button.Location = new System.Drawing.Point(158, 45);
            this.release_button.Margin = new System.Windows.Forms.Padding(1);
            this.release_button.Name = "release_button";
            this.release_button.Size = new System.Drawing.Size(67, 38);
            this.release_button.TabIndex = 2242;
            this.release_button.Text = "Release Job";
            this.release_button.UseVisualStyleBackColor = true;
            this.release_button.Click += new System.EventHandler(this.release_button_Click);
            // 
            // print_checklist
            // 
            this.print_checklist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.print_checklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print_checklist.Location = new System.Drawing.Point(170, 11);
            this.print_checklist.Name = "print_checklist";
            this.print_checklist.Size = new System.Drawing.Size(49, 22);
            this.print_checklist.TabIndex = 2243;
            this.print_checklist.Text = "OK";
            this.print_checklist.UseVisualStyleBackColor = true;
            this.print_checklist.Visible = false;
            this.print_checklist.Click += new System.EventHandler(this.print_checklist_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 45);
            this.Controls.Add(this.print_checklist);
            this.Controls.Add(this.release_button);
            this.Controls.Add(this.hold_button);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.shop_order);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.employee_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "Exco Hold & Release";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox employee_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox shop_order;
        private System.Windows.Forms.Button edit_button;
        private System.Windows.Forms.Button hold_button;
        private System.Windows.Forms.Button release_button;
        private System.Windows.Forms.Button print_checklist;
    }
}

