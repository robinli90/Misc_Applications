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

namespace CNC_Schedule
{

    partial class CNC_Form
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
            this.plate_button = new System.Windows.Forms.Button();
            this.mandrel_button = new System.Windows.Forms.Button();
            this.toDo_table = new System.Windows.Forms.TableLayoutPanel();
            this.onMachine_table = new System.Windows.Forms.TableLayoutPanel();
            this.machineStatus_table = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toDoList_counts = new System.Windows.Forms.TextBox();
            this.title_text_box = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.onMachineList_counts = new System.Windows.Forms.TextBox();
            this.misPunch_table = new System.Windows.Forms.TableLayoutPanel();
            this.misPunch_counts = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // plate_button
            // 
            this.plate_button.Location = new System.Drawing.Point(12, 12);
            this.plate_button.Name = "plate_button";
            this.plate_button.Size = new System.Drawing.Size(138, 23);
            this.plate_button.TabIndex = 0;
            this.plate_button.Text = "PLATE Schedule";
            this.plate_button.UseVisualStyleBackColor = true;
            this.plate_button.Click += new System.EventHandler(this.plate_button_Click);
            // 
            // mandrel_button
            // 
            this.mandrel_button.Location = new System.Drawing.Point(156, 12);
            this.mandrel_button.Name = "mandrel_button";
            this.mandrel_button.Size = new System.Drawing.Size(138, 23);
            this.mandrel_button.TabIndex = 1;
            this.mandrel_button.Text = "MANDREL Schedule";
            this.mandrel_button.UseVisualStyleBackColor = true;
            this.mandrel_button.Click += new System.EventHandler(this.mandrel_button_Click);
            // 
            // toDo_table
            // 
            this.toDo_table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.toDo_table.AutoScroll = true;
            this.toDo_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.toDo_table.ColumnCount = 7;
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.toDo_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.toDo_table.Location = new System.Drawing.Point(12, 102);
            this.toDo_table.Name = "toDo_table";
            this.toDo_table.Padding = new System.Windows.Forms.Padding(1);
            this.toDo_table.RowCount = 1;
            this.toDo_table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.toDo_table.Size = new System.Drawing.Size(504, 690);
            this.toDo_table.TabIndex = 2;
            // 
            // onMachine_table
            // 
            this.onMachine_table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.onMachine_table.AutoScroll = true;
            this.onMachine_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.onMachine_table.ColumnCount = 6;
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.onMachine_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.onMachine_table.Location = new System.Drawing.Point(543, 250);
            this.onMachine_table.Name = "onMachine_table";
            this.onMachine_table.Padding = new System.Windows.Forms.Padding(1);
            this.onMachine_table.RowCount = 1;
            this.onMachine_table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.onMachine_table.Size = new System.Drawing.Size(428, 342);
            this.onMachine_table.TabIndex = 3;
            // 
            // machineStatus_table
            // 
            this.machineStatus_table.ColumnCount = 5;
            this.machineStatus_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.machineStatus_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.machineStatus_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.machineStatus_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.machineStatus_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.machineStatus_table.Location = new System.Drawing.Point(543, 102);
            this.machineStatus_table.Name = "machineStatus_table";
            this.machineStatus_table.RowCount = 1;
            this.machineStatus_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.machineStatus_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.machineStatus_table.Size = new System.Drawing.Size(425, 60);
            this.machineStatus_table.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(170, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 24);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "TO DO LIST";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(679, 199);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(156, 24);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "ON MACHINE";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(660, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(203, 24);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "MACHINE STATUS";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(640, 639);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(239, 24);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "MIS-PUNCHING JOBS";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.White;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(23, 84);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(496, 15);
            this.textBox6.TabIndex = 11;
            this.textBox6.Text = "   #         S.O. #       DIA       DUE       Station       Status         Assign" +
    "ed";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(893, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toDoList_counts
            // 
            this.toDoList_counts.BackColor = System.Drawing.Color.White;
            this.toDoList_counts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toDoList_counts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDoList_counts.Location = new System.Drawing.Point(15, 801);
            this.toDoList_counts.Name = "toDoList_counts";
            this.toDoList_counts.Size = new System.Drawing.Size(498, 15);
            this.toDoList_counts.TabIndex = 13;
            // 
            // title_text_box
            // 
            this.title_text_box.BackColor = System.Drawing.Color.White;
            this.title_text_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.title_text_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_text_box.Location = new System.Drawing.Point(402, 2);
            this.title_text_box.Name = "title_text_box";
            this.title_text_box.Size = new System.Drawing.Size(307, 31);
            this.title_text_box.TabIndex = 15;
            this.title_text_box.Text = "PLATE SCHEDULE";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.White;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(555, 229);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(406, 15);
            this.textBox7.TabIndex = 16;
            this.textBox7.Text = "#         S.O.#        DIA        DUE        Station          Work By";
            // 
            // onMachineList_counts
            // 
            this.onMachineList_counts.BackColor = System.Drawing.Color.White;
            this.onMachineList_counts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.onMachineList_counts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onMachineList_counts.Location = new System.Drawing.Point(539, 598);
            this.onMachineList_counts.Name = "onMachineList_counts";
            this.onMachineList_counts.Size = new System.Drawing.Size(429, 15);
            this.onMachineList_counts.TabIndex = 17;
            // 
            // misPunch_table
            // 
            this.misPunch_table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.misPunch_table.AutoScroll = true;
            this.misPunch_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.misPunch_table.ColumnCount = 6;
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.misPunch_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.misPunch_table.Location = new System.Drawing.Point(543, 690);
            this.misPunch_table.Name = "misPunch_table";
            this.misPunch_table.Padding = new System.Windows.Forms.Padding(1);
            this.misPunch_table.RowCount = 1;
            this.misPunch_table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.misPunch_table.Size = new System.Drawing.Size(428, 101);
            this.misPunch_table.TabIndex = 18;
            // 
            // misPunch_counts
            // 
            this.misPunch_counts.BackColor = System.Drawing.Color.White;
            this.misPunch_counts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.misPunch_counts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.misPunch_counts.Location = new System.Drawing.Point(539, 798);
            this.misPunch_counts.Name = "misPunch_counts";
            this.misPunch_counts.Size = new System.Drawing.Size(429, 15);
            this.misPunch_counts.TabIndex = 19;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(555, 670);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(406, 15);
            this.textBox8.TabIndex = 20;
            this.textBox8.Text = "#         S.O.#        DIA        DUE        Station          Work By";
            // 
            // CNC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(981, 828);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.misPunch_counts);
            this.Controls.Add(this.misPunch_table);
            this.Controls.Add(this.onMachineList_counts);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.title_text_box);
            this.Controls.Add(this.toDoList_counts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.machineStatus_table);
            this.Controls.Add(this.onMachine_table);
            this.Controls.Add(this.toDo_table);
            this.Controls.Add(this.mandrel_button);
            this.Controls.Add(this.plate_button);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CNC_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "CNC Schedule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button plate_button;
        private System.Windows.Forms.Button mandrel_button;
        private System.Windows.Forms.TableLayoutPanel toDo_table;
        private System.Windows.Forms.TableLayoutPanel onMachine_table;
        private System.Windows.Forms.TableLayoutPanel machineStatus_table;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox6;
        private Button button1;
        private TextBox toDoList_counts;
        private TextBox title_text_box;
        private TextBox textBox7;
        private TextBox onMachineList_counts;
        private TableLayoutPanel misPunch_table;
        private TextBox misPunch_counts;
        private TextBox textBox8;
    }
}

