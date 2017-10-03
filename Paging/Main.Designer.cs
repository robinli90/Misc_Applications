using System;
using Databases;
using System.Data.Odbc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace Paging
{
    public class MyTableLayout : TableLayoutPanel
    {
        public MyTableLayout()
        {
            this.DoubleBuffered = true;
        }
    }

    partial class Main
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
            Environment.Exit(1);
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TextBox textBox7;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameListBox = new System.Windows.Forms.ComboBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.important = new System.Windows.Forms.CheckBox();
            this.fromBox = new System.Windows.Forms.ComboBox();
            this.displayListBox = new System.Windows.Forms.ComboBox();
            this.sent_check = new System.Windows.Forms.CheckBox();
            this.received_check = new System.Windows.Forms.CheckBox();
            this.show_count = new System.Windows.Forms.ComboBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pageSent = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.msg_table = new Paging.MyTableLayout();
            textBox7 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            textBox7.BackColor = System.Drawing.Color.White;
            textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox7.Location = new System.Drawing.Point(1403, 827);
            textBox7.Margin = new System.Windows.Forms.Padding(4);
            textBox7.Name = "textBox7";
            textBox7.Size = new System.Drawing.Size(1444, 15);
            textBox7.TabIndex = 17;
            textBox7.Text = "  Got           From                      Date                                   " +
    "         Message                                                     Reply Messa" +
    "ge                   ";
            textBox7.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(13, 228);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1039, 616);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dgvSomeDataGridView_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Read";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "From";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Date";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 160;
            // 
            // Column4
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column4.HeaderText = "Message";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 400;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Reply Message";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 206;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Importance";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // nameListBox
            // 
            this.nameListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.nameListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nameListBox.FormattingEnabled = true;
            this.nameListBox.Location = new System.Drawing.Point(95, 43);
            this.nameListBox.Margin = new System.Windows.Forms.Padding(4);
            this.nameListBox.Name = "nameListBox";
            this.nameListBox.Size = new System.Drawing.Size(180, 26);
            this.nameListBox.TabIndex = 19;
            this.nameListBox.SelectedIndexChanged += new System.EventHandler(this.nameListBox_SelectedIndexChanged);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(95, 87);
            this.messageBox.Margin = new System.Windows.Forms.Padding(4);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(736, 76);
            this.messageBox.TabIndex = 22;
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(841, 131);
            this.send_button.Margin = new System.Windows.Forms.Padding(4);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(112, 32);
            this.send_button.TabIndex = 23;
            this.send_button.Text = "Send Page";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // important
            // 
            this.important.AutoSize = true;
            this.important.Location = new System.Drawing.Point(841, 85);
            this.important.Margin = new System.Windows.Forms.Padding(4);
            this.important.Name = "important";
            this.important.Size = new System.Drawing.Size(147, 22);
            this.important.TabIndex = 24;
            this.important.Text = "Mark as Important";
            this.important.UseVisualStyleBackColor = true;
            // 
            // fromBox
            // 
            this.fromBox.Enabled = false;
            this.fromBox.FormattingEnabled = true;
            this.fromBox.Location = new System.Drawing.Point(95, 6);
            this.fromBox.Margin = new System.Windows.Forms.Padding(4);
            this.fromBox.Name = "fromBox";
            this.fromBox.Size = new System.Drawing.Size(180, 26);
            this.fromBox.TabIndex = 26;
            // 
            // displayListBox
            // 
            this.displayListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayListBox.FormattingEnabled = true;
            this.displayListBox.Location = new System.Drawing.Point(170, 190);
            this.displayListBox.Margin = new System.Windows.Forms.Padding(4);
            this.displayListBox.Name = "displayListBox";
            this.displayListBox.Size = new System.Drawing.Size(158, 26);
            this.displayListBox.TabIndex = 27;
            this.displayListBox.SelectedIndexChanged += new System.EventHandler(this.displayListBox_SelectedIndexChanged);
            // 
            // sent_check
            // 
            this.sent_check.AutoSize = true;
            this.sent_check.Checked = true;
            this.sent_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sent_check.Enabled = false;
            this.sent_check.Location = new System.Drawing.Point(336, 192);
            this.sent_check.Margin = new System.Windows.Forms.Padding(4);
            this.sent_check.Name = "sent_check";
            this.sent_check.Size = new System.Drawing.Size(103, 22);
            this.sent_check.TabIndex = 28;
            this.sent_check.Text = "Sent Pages";
            this.sent_check.UseVisualStyleBackColor = true;
            this.sent_check.CheckedChanged += new System.EventHandler(this.sent_check_CheckedChanged);
            // 
            // received_check
            // 
            this.received_check.AutoSize = true;
            this.received_check.Location = new System.Drawing.Point(446, 192);
            this.received_check.Margin = new System.Windows.Forms.Padding(4);
            this.received_check.Name = "received_check";
            this.received_check.Size = new System.Drawing.Size(134, 22);
            this.received_check.TabIndex = 29;
            this.received_check.Text = "Received Pages";
            this.received_check.UseVisualStyleBackColor = true;
            this.received_check.CheckedChanged += new System.EventHandler(this.received_check_CheckedChanged);
            // 
            // show_count
            // 
            this.show_count.Cursor = System.Windows.Forms.Cursors.Default;
            this.show_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.show_count.FormattingEnabled = true;
            this.show_count.Location = new System.Drawing.Point(959, 194);
            this.show_count.Margin = new System.Windows.Forms.Padding(4);
            this.show_count.Name = "show_count";
            this.show_count.Size = new System.Drawing.Size(93, 26);
            this.show_count.TabIndex = 32;
            this.show_count.SelectedIndexChanged += new System.EventHandler(this.show_count_SelectedIndexChanged);
            this.show_count.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseWheel);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(893, 199);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(63, 17);
            this.textBox5.TabIndex = 33;
            this.textBox5.Text = "Show #:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 18);
            this.label1.TabIndex = 34;
            this.label1.Text = "Display Pages from:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 35;
            this.label2.Text = "Message:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 36;
            this.label3.Text = "Page To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 18);
            this.label4.TabIndex = 37;
            this.label4.Text = "From:";
            // 
            // pageSent
            // 
            this.pageSent.AutoSize = true;
            this.pageSent.ForeColor = System.Drawing.Color.Green;
            this.pageSent.Location = new System.Drawing.Point(960, 138);
            this.pageSent.Name = "pageSent";
            this.pageSent.Size = new System.Drawing.Size(76, 18);
            this.pageSent.TabIndex = 38;
            this.pageSent.Text = "Page Sent";
            this.pageSent.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(603, 193);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 25);
            this.button1.TabIndex = 39;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // msg_table
            // 
            this.msg_table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.msg_table.AutoScroll = true;
            this.msg_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.msg_table.ColumnCount = 6;
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.msg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.msg_table.Location = new System.Drawing.Point(1403, 856);
            this.msg_table.Margin = new System.Windows.Forms.Padding(4);
            this.msg_table.Name = "msg_table";
            this.msg_table.Padding = new System.Windows.Forms.Padding(1);
            this.msg_table.RowCount = 1;
            this.msg_table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.msg_table.Size = new System.Drawing.Size(1473, 617);
            this.msg_table.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1063, 857);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pageSent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.show_count);
            this.Controls.Add(this.received_check);
            this.Controls.Add(this.sent_check);
            this.Controls.Add(this.displayListBox);
            this.Controls.Add(this.fromBox);
            this.Controls.Add(this.important);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.nameListBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(textBox7);
            this.Controls.Add(this.msg_table);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Internal Paging";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Paging.MyTableLayout msg_table;
        private DataGridView dataGridView1;
        private ComboBox nameListBox;
        private TextBox messageBox;
        private Button send_button;
        private CheckBox important;
        private ComboBox fromBox;
        private ComboBox displayListBox;
        private CheckBox sent_check;
        private CheckBox received_check;
        private ComboBox show_count;
        private TextBox textBox5;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label pageSent;
        private Button button1;


    }
}

