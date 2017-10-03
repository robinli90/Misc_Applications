using System.Windows.Forms;

namespace ITManagement
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // employee_box
            // 
            this.employee_box.BackColor = System.Drawing.SystemColors.Control;
            this.employee_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.employee_box.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.employee_box, "employee_box");
            this.employee_box.Name = "employee_box";
            this.employee_box.TextChanged += new System.EventHandler(this.employee_box_TextChanged);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.SystemColors.Control;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.text, "text");
            this.text.Name = "text";
            this.text.ReadOnly = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // password_box
            // 
            this.password_box.BackColor = System.Drawing.SystemColors.Control;
            this.password_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_box.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.password_box.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.password_box, "password_box");
            this.password_box.Name = "password_box";
            this.password_box.TextChanged += new System.EventHandler(this.password_box_TextChanged);
            this.password_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.password_box_KeyPress);
            // 
            // login_button
            // 
            resources.ApplyResources(this.login_button, "login_button");
            this.login_button.ForeColor = System.Drawing.Color.Black;
            this.login_button.Name = "login_button";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // error_text
            // 
            this.error_text.BackColor = System.Drawing.SystemColors.Control;
            this.error_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            resources.ApplyResources(this.error_text, "error_text");
            this.error_text.Name = "error_text";
            this.error_text.ReadOnly = true;
            // 
            // logged_in_text
            // 
            this.logged_in_text.BackColor = System.Drawing.SystemColors.Control;
            this.logged_in_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logged_in_text.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.logged_in_text, "logged_in_text");
            this.logged_in_text.Name = "logged_in_text";
            this.logged_in_text.ReadOnly = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logged_in_text);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.password_box);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.employee_box);
            this.Controls.Add(this.text);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
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
        private Button button1;
        private Button button2;
    }
}