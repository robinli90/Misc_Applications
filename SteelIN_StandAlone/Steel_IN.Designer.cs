using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SteelIN_StandAlone
{
    partial class Steel_IN
    {
        // Mouse down anywhere to drag
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

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
            this.diameterbox = new System.Windows.Forms.ComboBox();
            this.text1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.steeltypebox = new System.Windows.Forms.ComboBox();
            this.lengthbox = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.heatbox = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.supplierbox = new System.Windows.Forms.ComboBox();
            this.clear_steel_button = new System.Windows.Forms.Button();
            this.add_steel_button = new System.Windows.Forms.Button();
            this.steel_table = new System.Windows.Forms.TableLayoutPanel();
            this.submit_button = new System.Windows.Forms.Button();
            this.delete_row_button = new System.Windows.Forms.Button();
            this.clear_table_button = new System.Windows.Forms.Button();
            this.lengtherrorbox = new System.Windows.Forms.RichTextBox();
            this.heatnumberror = new System.Windows.Forms.RichTextBox();
            this.going_total_text = new System.Windows.Forms.RichTextBox();
            this.close_button = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.count_box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // diameterbox
            // 
            this.diameterbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.diameterbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.diameterbox.BackColor = System.Drawing.Color.Black;
            this.diameterbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.diameterbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.diameterbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.diameterbox.FormattingEnabled = true;
            this.diameterbox.Location = new System.Drawing.Point(68, 57);
            this.diameterbox.Name = "diameterbox";
            this.diameterbox.Size = new System.Drawing.Size(70, 21);
            this.diameterbox.TabIndex = 1;
            this.diameterbox.SelectedIndexChanged += new System.EventHandler(this.diameterbox_SelectedIndexChanged);
            // 
            // text1
            // 
            this.text1.BackColor = System.Drawing.Color.Black;
            this.text1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text1.Location = new System.Drawing.Point(8, 58);
            this.text1.Name = "text1";
            this.text1.ReadOnly = true;
            this.text1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text1.Size = new System.Drawing.Size(48, 22);
            this.text1.TabIndex = 17;
            this.text1.Text = "Diameter";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(8, 86);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(60, 20);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "Steel Type";
            // 
            // steeltypebox
            // 
            this.steeltypebox.BackColor = System.Drawing.Color.Black;
            this.steeltypebox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.steeltypebox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steeltypebox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.steeltypebox.FormattingEnabled = true;
            this.steeltypebox.Location = new System.Drawing.Point(68, 84);
            this.steeltypebox.Name = "steeltypebox";
            this.steeltypebox.Size = new System.Drawing.Size(70, 21);
            this.steeltypebox.TabIndex = 2;
            // 
            // lengthbox
            // 
            this.lengthbox.BackColor = System.Drawing.Color.Black;
            this.lengthbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lengthbox.Location = new System.Drawing.Point(68, 111);
            this.lengthbox.MaxLength = 8;
            this.lengthbox.Name = "lengthbox";
            this.lengthbox.Size = new System.Drawing.Size(70, 20);
            this.lengthbox.TabIndex = 3;
            this.lengthbox.TextChanged += new System.EventHandler(this.lengthbox_TextChanged);
            this.lengthbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lengthbox_KeyPress);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Black;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.text.Location = new System.Drawing.Point(8, 112);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(39, 22);
            this.text.TabIndex = 15;
            this.text.Text = "Length";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox2.Location = new System.Drawing.Point(8, 140);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(39, 22);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "Heat #";
            // 
            // heatbox
            // 
            this.heatbox.BackColor = System.Drawing.Color.Black;
            this.heatbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.heatbox.Location = new System.Drawing.Point(68, 139);
            this.heatbox.MaxLength = 10;
            this.heatbox.Name = "heatbox";
            this.heatbox.Size = new System.Drawing.Size(70, 20);
            this.heatbox.TabIndex = 4;
            this.heatbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.heatbox_KeyPress);
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.Black;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.richTextBox3.Location = new System.Drawing.Point(8, 167);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(60, 20);
            this.richTextBox3.TabIndex = 12;
            this.richTextBox3.Text = "Supplier";
            // 
            // supplierbox
            // 
            this.supplierbox.BackColor = System.Drawing.Color.Black;
            this.supplierbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.supplierbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.supplierbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.supplierbox.FormattingEnabled = true;
            this.supplierbox.Location = new System.Drawing.Point(68, 165);
            this.supplierbox.Name = "supplierbox";
            this.supplierbox.Size = new System.Drawing.Size(70, 21);
            this.supplierbox.TabIndex = 5;
            // 
            // clear_steel_button
            // 
            this.clear_steel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clear_steel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_steel_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clear_steel_button.Location = new System.Drawing.Point(87, 194);
            this.clear_steel_button.Margin = new System.Windows.Forms.Padding(1);
            this.clear_steel_button.Name = "clear_steel_button";
            this.clear_steel_button.Size = new System.Drawing.Size(51, 22);
            this.clear_steel_button.TabIndex = 13;
            this.clear_steel_button.Text = "Clear";
            this.clear_steel_button.UseVisualStyleBackColor = true;
            this.clear_steel_button.Click += new System.EventHandler(this.clear_steel_button_Click);
            // 
            // add_steel_button
            // 
            this.add_steel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.add_steel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_steel_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.add_steel_button.Location = new System.Drawing.Point(31, 194);
            this.add_steel_button.Margin = new System.Windows.Forms.Padding(1);
            this.add_steel_button.Name = "add_steel_button";
            this.add_steel_button.Size = new System.Drawing.Size(51, 22);
            this.add_steel_button.TabIndex = 6;
            this.add_steel_button.Text = "Add";
            this.add_steel_button.UseVisualStyleBackColor = true;
            this.add_steel_button.Click += new System.EventHandler(this.add_steel_button_Click);
            // 
            // steel_table
            // 
            this.steel_table.AutoScroll = true;
            this.steel_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.steel_table.ColumnCount = 5;
            this.steel_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.steel_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.steel_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.steel_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.steel_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.steel_table.Location = new System.Drawing.Point(152, 55);
            this.steel_table.MaximumSize = new System.Drawing.Size(491, 220);
            this.steel_table.Name = "steel_table";
            this.steel_table.Padding = new System.Windows.Forms.Padding(1);
            this.steel_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.steel_table.Size = new System.Drawing.Size(351, 220);
            this.steel_table.TabIndex = 16;
            // 
            // submit_button
            // 
            this.submit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.submit_button.Location = new System.Drawing.Point(439, 29);
            this.submit_button.Margin = new System.Windows.Forms.Padding(1);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(64, 22);
            this.submit_button.TabIndex = 15;
            this.submit_button.Text = "Submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // delete_row_button
            // 
            this.delete_row_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.delete_row_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_row_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.delete_row_button.Location = new System.Drawing.Point(249, 29);
            this.delete_row_button.Margin = new System.Windows.Forms.Padding(1);
            this.delete_row_button.Name = "delete_row_button";
            this.delete_row_button.Size = new System.Drawing.Size(100, 22);
            this.delete_row_button.TabIndex = 16;
            this.delete_row_button.Text = "Delete Last Row";
            this.delete_row_button.UseVisualStyleBackColor = true;
            this.delete_row_button.Click += new System.EventHandler(this.delete_row_button_Click);
            // 
            // clear_table_button
            // 
            this.clear_table_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clear_table_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_table_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clear_table_button.Location = new System.Drawing.Point(355, 29);
            this.clear_table_button.Margin = new System.Windows.Forms.Padding(1);
            this.clear_table_button.Name = "clear_table_button";
            this.clear_table_button.Size = new System.Drawing.Size(77, 22);
            this.clear_table_button.TabIndex = 16;
            this.clear_table_button.Text = "Clear Table";
            this.clear_table_button.UseVisualStyleBackColor = true;
            this.clear_table_button.Click += new System.EventHandler(this.clear_table_button_Click);
            // 
            // lengtherrorbox
            // 
            this.lengtherrorbox.BackColor = System.Drawing.Color.Black;
            this.lengtherrorbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lengtherrorbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.lengtherrorbox.Location = new System.Drawing.Point(140, 114);
            this.lengtherrorbox.Name = "lengtherrorbox";
            this.lengtherrorbox.ReadOnly = true;
            this.lengtherrorbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lengtherrorbox.Size = new System.Drawing.Size(10, 22);
            this.lengtherrorbox.TabIndex = 17;
            this.lengtherrorbox.Text = "<";
            this.lengtherrorbox.Visible = false;
            // 
            // heatnumberror
            // 
            this.heatnumberror.BackColor = System.Drawing.Color.Black;
            this.heatnumberror.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.heatnumberror.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.heatnumberror.Location = new System.Drawing.Point(140, 142);
            this.heatnumberror.Name = "heatnumberror";
            this.heatnumberror.ReadOnly = true;
            this.heatnumberror.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.heatnumberror.Size = new System.Drawing.Size(10, 22);
            this.heatnumberror.TabIndex = 17;
            this.heatnumberror.Text = "<";
            this.heatnumberror.Visible = false;
            // 
            // going_total_text
            // 
            this.going_total_text.BackColor = System.Drawing.Color.Black;
            this.going_total_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.going_total_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.going_total_text.Location = new System.Drawing.Point(154, 281);
            this.going_total_text.Name = "going_total_text";
            this.going_total_text.Size = new System.Drawing.Size(349, 22);
            this.going_total_text.TabIndex = 18;
            this.going_total_text.Text = "";
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.close_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.close_button.Location = new System.Drawing.Point(495, 2);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(14, 21);
            this.close_button.TabIndex = 19;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(-6, -9);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(522, 10);
            this.textBox3.TabIndex = 36;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(-8, 308);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(532, 13);
            this.textBox1.TabIndex = 37;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(-9, -20);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(10, 370);
            this.textBox2.TabIndex = 38;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(511, -33);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(10, 384);
            this.textBox4.TabIndex = 39;
            // 
            // count_box
            // 
            this.count_box.BackColor = System.Drawing.Color.Black;
            this.count_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.count_box.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.count_box.Location = new System.Drawing.Point(8, 281);
            this.count_box.Name = "count_box";
            this.count_box.Size = new System.Drawing.Size(79, 22);
            this.count_box.TabIndex = 40;
            this.count_box.Text = "";
            // 
            // Steel_IN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(512, 309);
            this.Controls.Add(this.count_box);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.going_total_text);
            this.Controls.Add(this.heatnumberror);
            this.Controls.Add(this.lengtherrorbox);
            this.Controls.Add(this.clear_table_button);
            this.Controls.Add(this.delete_row_button);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.steel_table);
            this.Controls.Add(this.add_steel_button);
            this.Controls.Add(this.clear_steel_button);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.supplierbox);
            this.Controls.Add(this.heatbox);
            this.Controls.Add(this.lengthbox);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.text);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.steeltypebox);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.diameterbox);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "Steel_IN";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steel Inventory";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Steel_IN_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox diameterbox;
        private System.Windows.Forms.RichTextBox text1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox steeltypebox;
        private System.Windows.Forms.TextBox lengthbox;
        private System.Windows.Forms.RichTextBox text;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox heatbox;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.ComboBox supplierbox;
        private System.Windows.Forms.Button clear_steel_button;
        private System.Windows.Forms.Button add_steel_button;
        private System.Windows.Forms.TableLayoutPanel steel_table;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.Button delete_row_button;
        private System.Windows.Forms.Button clear_table_button;
        private System.Windows.Forms.RichTextBox lengtherrorbox;
        private System.Windows.Forms.RichTextBox heatnumberror;
        private System.Windows.Forms.RichTextBox going_total_text;
        private System.Windows.Forms.Button close_button;
        private TextBox textBox3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox4;
        private RichTextBox count_box;

    }
}