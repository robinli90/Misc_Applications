using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Translator
{
    partial class Translator
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
            /*DialogResult dialogResult = MessageBox.Show("Do you wish to save changes?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                button10.PerformClick();
            }
            else if (dialogResult == DialogResult.No)
            {
            }*/
            CompareText comp = new CompareText();
            comp.Delete_Comparison_File();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Translator));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.translation_list = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.changes = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.load_file_button = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.deletebutton = new System.Windows.Forms.Button();
            this.no_file_loaded_text = new System.Windows.Forms.RichTextBox();
            this.number_of_changes = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.admin_text_box = new System.Windows.Forms.RichTextBox();
            this.file_loaded_text = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button23 = new System.Windows.Forms.Button();
            this.desc_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.translation_rules = new System.Windows.Forms.ListBox();
            this.richTextBox14 = new System.Windows.Forms.RichTextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.show_bin_rules = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.view_edit_group_button = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button24 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button27 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.group_color_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox1.Controls.Add(this.translation_list);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button13);
            this.groupBox1.Controls.Add(this.changes);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(158, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Translation Algorithms";
            // 
            // translation_list
            // 
            this.translation_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translation_list.BackColor = System.Drawing.Color.DarkGray;
            this.translation_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.translation_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translation_list.FormattingEnabled = true;
            this.translation_list.Location = new System.Drawing.Point(5, 20);
            this.translation_list.Name = "translation_list";
            this.translation_list.Size = new System.Drawing.Size(317, 143);
            this.translation_list.TabIndex = 0;
            this.translation_list.SelectedIndexChanged += new System.EventHandler(this.translation_list_SelectedIndexChanged);
            this.translation_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.translation_list_MouseDoubleClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGray;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(160, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 40);
            this.button3.TabIndex = 1;
            this.button3.Text = "Translate File Using Current Algorithm (INVISIBLE)";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.DarkGray;
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.Location = new System.Drawing.Point(9, 71);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(140, 26);
            this.button13.TabIndex = 5;
            this.button13.Text = "Overwrite Original File";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Visible = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // changes
            // 
            this.changes.AutoSize = true;
            this.changes.BackColor = System.Drawing.Color.DarkGray;
            this.changes.Location = new System.Drawing.Point(79, 35);
            this.changes.Name = "changes";
            this.changes.Size = new System.Drawing.Size(108, 19);
            this.changes.TabIndex = 53;
            this.changes.Text = "Mark Changes";
            this.changes.UseVisualStyleBackColor = false;
            this.changes.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.DarkGray;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(651, 157);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 17);
            this.checkBox1.TabIndex = 56;
            this.checkBox1.Text = "Show Descriptions Only";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // load_file_button
            // 
            this.load_file_button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.load_file_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load_file_button.Location = new System.Drawing.Point(82, 9);
            this.load_file_button.Name = "load_file_button";
            this.load_file_button.Size = new System.Drawing.Size(67, 26);
            this.load_file_button.TabIndex = 1;
            this.load_file_button.Text = "Load File";
            this.load_file_button.UseVisualStyleBackColor = false;
            this.load_file_button.Click += new System.EventHandler(this.load_file_button_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(9, 38);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 26);
            this.button5.TabIndex = 1;
            this.button5.Text = "Preview Translation";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(9, 151);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(141, 26);
            this.button11.TabIndex = 1;
            this.button11.Text = "New Translation Algorithm";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // deletebutton
            // 
            this.deletebutton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.deletebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletebutton.Location = new System.Drawing.Point(9, 123);
            this.deletebutton.Name = "deletebutton";
            this.deletebutton.Size = new System.Drawing.Size(140, 26);
            this.deletebutton.TabIndex = 1;
            this.deletebutton.Text = "Delete Selected Algorithm";
            this.deletebutton.UseVisualStyleBackColor = false;
            this.deletebutton.Click += new System.EventHandler(this.deletebutton_Click);
            // 
            // no_file_loaded_text
            // 
            this.no_file_loaded_text.BackColor = System.Drawing.Color.DarkGray;
            this.no_file_loaded_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.no_file_loaded_text.ForeColor = System.Drawing.Color.Red;
            this.no_file_loaded_text.Location = new System.Drawing.Point(158, 18);
            this.no_file_loaded_text.Name = "no_file_loaded_text";
            this.no_file_loaded_text.ReadOnly = true;
            this.no_file_loaded_text.Size = new System.Drawing.Size(115, 18);
            this.no_file_loaded_text.TabIndex = 2;
            this.no_file_loaded_text.Text = "No file loaded";
            // 
            // number_of_changes
            // 
            this.number_of_changes.BackColor = System.Drawing.Color.DarkGray;
            this.number_of_changes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.number_of_changes.ForeColor = System.Drawing.Color.Blue;
            this.number_of_changes.Location = new System.Drawing.Point(296, 18);
            this.number_of_changes.Name = "number_of_changes";
            this.number_of_changes.ReadOnly = true;
            this.number_of_changes.Size = new System.Drawing.Size(200, 16);
            this.number_of_changes.TabIndex = 4;
            this.number_of_changes.Text = "Number of changes via translation: 314";
            this.number_of_changes.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(9, 95);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 26);
            this.button4.TabIndex = 1;
            this.button4.Text = "Save Translated File";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(9, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "View File";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.Location = new System.Drawing.Point(9, 179);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(141, 26);
            this.button15.TabIndex = 7;
            this.button15.Text = "Copy Selected Algorithm";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.DarkGray;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.ForeColor = System.Drawing.Color.DarkGray;
            this.button16.Location = new System.Drawing.Point(473, 207);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(10, 10);
            this.button16.TabIndex = 8;
            this.button16.Text = "Copy Selected Algorithm";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.Location = new System.Drawing.Point(8, 67);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(141, 26);
            this.button18.TabIndex = 54;
            this.button18.Text = "Compare Translation";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.DarkGray;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button19.ForeColor = System.Drawing.Color.DarkGray;
            this.button19.Location = new System.Drawing.Point(482, 2);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(10, 10);
            this.button19.TabIndex = 55;
            this.button19.Text = "Copy Selected Algorithm";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // admin_text_box
            // 
            this.admin_text_box.BackColor = System.Drawing.Color.DarkGray;
            this.admin_text_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.admin_text_box.ForeColor = System.Drawing.Color.Lavender;
            this.admin_text_box.Location = new System.Drawing.Point(448, 2);
            this.admin_text_box.Multiline = false;
            this.admin_text_box.Name = "admin_text_box";
            this.admin_text_box.ReadOnly = true;
            this.admin_text_box.Size = new System.Drawing.Size(36, 11);
            this.admin_text_box.TabIndex = 56;
            this.admin_text_box.Text = "";
            this.admin_text_box.Visible = false;
            this.admin_text_box.TextChanged += new System.EventHandler(this.admin_text_box_TextChanged);
            // 
            // file_loaded_text
            // 
            this.file_loaded_text.BackColor = System.Drawing.Color.DarkGray;
            this.file_loaded_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.file_loaded_text.ForeColor = System.Drawing.Color.Black;
            this.file_loaded_text.Location = new System.Drawing.Point(151, 17);
            this.file_loaded_text.Name = "file_loaded_text";
            this.file_loaded_text.ReadOnly = true;
            this.file_loaded_text.Size = new System.Drawing.Size(145, 18);
            this.file_loaded_text.TabIndex = 3;
            this.file_loaded_text.Text = "File loaded (312112P)";
            this.file_loaded_text.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(490, 180);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(299, 330);
            this.dataGridView1.TabIndex = 57;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 22;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Group description";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button23.Location = new System.Drawing.Point(678, 8);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(111, 26);
            this.button23.TabIndex = 58;
            this.button23.Text = "Create New Group";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // desc_box
            // 
            this.desc_box.BackColor = System.Drawing.Color.DarkGray;
            this.desc_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.desc_box.Location = new System.Drawing.Point(555, 43);
            this.desc_box.Multiline = true;
            this.desc_box.Name = "desc_box";
            this.desc_box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.desc_box.Size = new System.Drawing.Size(234, 76);
            this.desc_box.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Description:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(32, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "Get Description of Rule";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(12, 257);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(144, 26);
            this.button7.TabIndex = 1;
            this.button7.Text = "Add New Rule";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(323, 287);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(143, 26);
            this.button6.TabIndex = 1;
            this.button6.Text = "Delete Selected Rule(s)";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(162, 257);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(155, 26);
            this.button8.TabIndex = 1;
            this.button8.Text = "Move Selected Rule(s) Up";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(162, 287);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(155, 26);
            this.button9.TabIndex = 1;
            this.button9.Text = "Move Selected Rule(s) Down";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(323, 317);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(143, 26);
            this.button10.TabIndex = 2;
            this.button10.Text = "Save Changes";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // translation_rules
            // 
            this.translation_rules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translation_rules.BackColor = System.Drawing.Color.DarkGray;
            this.translation_rules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.translation_rules.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.translation_rules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translation_rules.FormattingEnabled = true;
            this.translation_rules.HorizontalScrollbar = true;
            this.translation_rules.Location = new System.Drawing.Point(8, 20);
            this.translation_rules.Name = "translation_rules";
            this.translation_rules.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.translation_rules.Size = new System.Drawing.Size(462, 195);
            this.translation_rules.TabIndex = 3;
            this.translation_rules.SelectedIndexChanged += new System.EventHandler(this.translation_rules_SelectedIndexChanged_1);
            this.translation_rules.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.translation_rules_MouseDoubleClick);
            // 
            // richTextBox14
            // 
            this.richTextBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox14.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox14.ForeColor = System.Drawing.Color.Blue;
            this.richTextBox14.Location = new System.Drawing.Point(15, 209);
            this.richTextBox14.Name = "richTextBox14";
            this.richTextBox14.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox14.Size = new System.Drawing.Size(124, 17);
            this.richTextBox14.TabIndex = 50;
            this.richTextBox14.Text = "*Double-click Rule to Edit";
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button12.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(323, 257);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(143, 26);
            this.button12.TabIndex = 51;
            this.button12.Text = "Duplicate Selected Rule(s)";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button14.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button14.Location = new System.Drawing.Point(162, 317);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(155, 26);
            this.button14.TabIndex = 52;
            this.button14.Text = "Reload Rules";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button17.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.Location = new System.Drawing.Point(13, 287);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(144, 26);
            this.button17.TabIndex = 53;
            this.button17.Text = "File Options";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button20
            // 
            this.button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button20.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button20.Location = new System.Drawing.Point(13, 227);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(144, 26);
            this.button20.TabIndex = 54;
            this.button20.Text = "Add Loop";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button21.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button21.Location = new System.Drawing.Point(163, 227);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(154, 26);
            this.button21.TabIndex = 55;
            this.button21.Text = "Add If/Then Statement";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // show_bin_rules
            // 
            this.show_bin_rules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_bin_rules.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.show_bin_rules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_bin_rules.Location = new System.Drawing.Point(13, 317);
            this.show_bin_rules.Name = "show_bin_rules";
            this.show_bin_rules.Size = new System.Drawing.Size(144, 26);
            this.show_bin_rules.TabIndex = 57;
            this.show_bin_rules.Text = "Show Bin Rules";
            this.show_bin_rules.UseVisualStyleBackColor = false;
            this.show_bin_rules.Click += new System.EventHandler(this.show_bin_rules_Click);
            // 
            // button22
            // 
            this.button22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button22.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button22.Location = new System.Drawing.Point(490, 516);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(100, 39);
            this.button22.TabIndex = 63;
            this.button22.Text = "Add selected rules to group";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button25.Location = new System.Drawing.Point(640, 125);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(149, 26);
            this.button25.TabIndex = 60;
            this.button25.Text = "Rename Group Description";
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // view_edit_group_button
            // 
            this.view_edit_group_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.view_edit_group_button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.view_edit_group_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view_edit_group_button.Location = new System.Drawing.Point(323, 227);
            this.view_edit_group_button.Name = "view_edit_group_button";
            this.view_edit_group_button.Size = new System.Drawing.Size(143, 26);
            this.view_edit_group_button.TabIndex = 58;
            this.view_edit_group_button.Text = "View/Edit Rule Groups";
            this.view_edit_group_button.UseVisualStyleBackColor = false;
            this.view_edit_group_button.Click += new System.EventHandler(this.view_edit_group_button_Click);
            // 
            // button26
            // 
            this.button26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button26.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button26.Location = new System.Drawing.Point(595, 516);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(100, 39);
            this.button26.TabIndex = 64;
            this.button26.Text = "Remove selected rules from group";
            this.button26.UseVisualStyleBackColor = false;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox2.Controls.Add(this.group_color_checkbox);
            this.groupBox2.Controls.Add(this.view_edit_group_button);
            this.groupBox2.Controls.Add(this.show_bin_rules);
            this.groupBox2.Controls.Add(this.button21);
            this.groupBox2.Controls.Add(this.button20);
            this.groupBox2.Controls.Add(this.button17);
            this.groupBox2.Controls.Add(this.button14);
            this.groupBox2.Controls.Add(this.button12);
            this.groupBox2.Controls.Add(this.richTextBox14);
            this.groupBox2.Controls.Add(this.translation_rules);
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 350);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rules (Rules execute from top to bottom; changes are compounded)";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button24.Location = new System.Drawing.Point(126, 38);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(23, 26);
            this.button24.TabIndex = 66;
            this.button24.Text = "»";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Group";
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button27.Location = new System.Drawing.Point(490, 125);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(144, 26);
            this.button27.TabIndex = 68;
            this.button27.Text = "Add Group Condition";
            this.button27.UseVisualStyleBackColor = false;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(703, 516);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 41);
            this.label3.TabIndex = 69;
            this.label3.Text = "* Groups with conditions have red check boxes";
            // 
            // group_color_checkbox
            // 
            this.group_color_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.group_color_checkbox.AutoSize = true;
            this.group_color_checkbox.BackColor = System.Drawing.Color.DarkGray;
            this.group_color_checkbox.Checked = true;
            this.group_color_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.group_color_checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_color_checkbox.Location = new System.Drawing.Point(350, 210);
            this.group_color_checkbox.Name = "group_color_checkbox";
            this.group_color_checkbox.Size = new System.Drawing.Size(117, 17);
            this.group_color_checkbox.TabIndex = 70;
            this.group_color_checkbox.Text = "Show Group Colors";
            this.group_color_checkbox.UseVisualStyleBackColor = false;
            this.group_color_checkbox.CheckedChanged += new System.EventHandler(this.group_color_checkbox_CheckedChanged);
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(800, 566);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button27);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.button26);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.desc_box);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.admin_text_box);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.number_of_changes);
            this.Controls.Add(this.file_loaded_text);
            this.Controls.Add(this.no_file_loaded_text);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.deletebutton);
            this.Controls.Add(this.load_file_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 2000);
            this.Name = "Translator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Translator Manager";
            this.Load += new System.EventHandler(this.Translator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox translation_list;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button deletebutton;
        private System.Windows.Forms.RichTextBox no_file_loaded_text;
        private System.Windows.Forms.RichTextBox number_of_changes;
        private System.Windows.Forms.Button button4;
        private Button button13;
        private Button button1;
        private Button button15;
        private Button button16;
        private CheckBox changes;
        private Button button18;
        private Button button19;
        private RichTextBox admin_text_box;
        private CheckBox checkBox1;
        private RichTextBox file_loaded_text;
        private DataGridView dataGridView1;
        private Button button23;
        private DataGridViewCheckBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private TextBox desc_box;
        private Label label1;
        private Button button2;
        private Button button7;
        private Button button6;
        private Button button8;
        private Button button9;
        private Button button10;
        public ListBox translation_rules;
        private RichTextBox richTextBox14;
        private Button button12;
        private Button button14;
        private Button button17;
        private Button button20;
        private Button button21;
        private Button show_bin_rules;
        private Button button22;
        private Button button25;
        private Button view_edit_group_button;
        private Button button26;
        private GroupBox groupBox2;
        private Button button24;
        private Label label2;
        private Label label3;
        public Button button27;
        public Button load_file_button;
        public Button button5;
        private CheckBox group_color_checkbox;
    }
}
