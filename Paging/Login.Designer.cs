using System.Windows.Forms;

namespace Paging
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
            this.employee_box = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.password_box = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.error_text = new System.Windows.Forms.RichTextBox();
            this.logged_in_text = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // employee_box
            // 
            this.employee_box.BackColor = System.Drawing.Color.White;
            this.employee_box.ForeColor = System.Drawing.Color.Black;
            this.employee_box.Location = new System.Drawing.Point(72, 23);
            this.employee_box.MaxLength = 5;
            this.employee_box.Name = "employee_box";
            this.employee_box.Size = new System.Drawing.Size(70, 20);
            this.employee_box.TabIndex = 1;
            this.employee_box.TextChanged += new System.EventHandler(this.employee_box_TextChanged);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.White;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.Black;
            this.text.Location = new System.Drawing.Point(9, 26);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(67, 22);
            this.text.TabIndex = 17;
            this.text.Text = "Employee #";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            this.richTextBox1.Location = new System.Drawing.Point(10, 50);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(54, 22);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "Password";
            // 
            // password_box
            // 
            this.password_box.BackColor = System.Drawing.Color.White;
            this.password_box.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.password_box.ForeColor = System.Drawing.Color.Black;
            this.password_box.Location = new System.Drawing.Point(72, 49);
            this.password_box.MaxLength = 20;
            this.password_box.Name = "password_box";
            this.password_box.PasswordChar = '*';
            this.password_box.Size = new System.Drawing.Size(70, 20);
            this.password_box.TabIndex = 2;
            this.password_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.password_box_KeyPress);
            // 
            // login_button
            // 
            this.login_button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.login_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.login_button.ForeColor = System.Drawing.Color.Black;
            this.login_button.Location = new System.Drawing.Point(91, 75);
            this.login_button.Margin = new System.Windows.Forms.Padding(1);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(51, 25);
            this.login_button.TabIndex = 3;
            this.login_button.Text = "Login";
            this.login_button.UseVisualStyleBackColor = false;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // error_text
            // 
            this.error_text.BackColor = System.Drawing.Color.White;
            this.error_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error_text.ForeColor = System.Drawing.Color.Red;
            this.error_text.Location = new System.Drawing.Point(3, 74);
            this.error_text.Name = "error_text";
            this.error_text.ReadOnly = true;
            this.error_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.error_text.Size = new System.Drawing.Size(85, 32);
            this.error_text.TabIndex = 18;
            this.error_text.Text = "Invalid Emp#/Password";
            this.error_text.Visible = false;
            // 
            // logged_in_text
            // 
            this.logged_in_text.BackColor = System.Drawing.Color.White;
            this.logged_in_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logged_in_text.ForeColor = System.Drawing.Color.Black;
            this.logged_in_text.Location = new System.Drawing.Point(2, 74);
            this.logged_in_text.Name = "logged_in_text";
            this.logged_in_text.ReadOnly = true;
            this.logged_in_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.logged_in_text.Size = new System.Drawing.Size(85, 32);
            this.logged_in_text.TabIndex = 42;
            this.logged_in_text.Text = "Already logged in. Try again";
            this.logged_in_text.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(151, 110);
            this.Controls.Add(this.logged_in_text);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.password_box);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.employee_box);
            this.Controls.Add(this.text);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox employee_box;
        private System.Windows.Forms.RichTextBox text;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox password_box;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.RichTextBox error_text;
        private RichTextBox logged_in_text;
    }
}