namespace ITManagement
{
    partial class IT_Information
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IT_Information));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.add_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.subject_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.info_box = new System.Windows.Forms.TextBox();
            this.topic_drop_list = new System.Windows.Forms.ComboBox();
            this.delete_topic = new System.Windows.Forms.Button();
            this.search_text_box = new System.Windows.Forms.TextBox();
            this.search_box = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edit_index_box = new System.Windows.Forms.TextBox();
            this.delete_entry_button = new System.Windows.Forms.Button();
            this.show_edit_history = new System.Windows.Forms.CheckBox();
            this.print_button = new System.Windows.Forms.Button();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Location = new System.Drawing.Point(12, 174);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(901, 415);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // add_button
            // 
            this.add_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_button.Location = new System.Drawing.Point(809, 13);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(104, 22);
            this.add_button.TabIndex = 3;
            this.add_button.TabStop = false;
            this.add_button.Text = "Add Info";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.transfer_button_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(249, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 222;
            this.label2.Text = "Subject: ";
            // 
            // subject_box
            // 
            this.subject_box.BackColor = System.Drawing.Color.White;
            this.subject_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subject_box.Location = new System.Drawing.Point(302, 14);
            this.subject_box.Name = "subject_box";
            this.subject_box.Size = new System.Drawing.Size(188, 20);
            this.subject_box.TabIndex = 1;
            this.subject_box.TabStop = false;
            this.subject_box.TextChanged += new System.EventHandler(this.subject_box_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 224;
            this.label1.Text = "Information:";
            // 
            // info_box
            // 
            this.info_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info_box.BackColor = System.Drawing.Color.White;
            this.info_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.info_box.Location = new System.Drawing.Point(80, 41);
            this.info_box.Multiline = true;
            this.info_box.Name = "info_box";
            this.info_box.Size = new System.Drawing.Size(831, 127);
            this.info_box.TabIndex = 2;
            this.info_box.TabStop = false;
            this.info_box.TextChanged += new System.EventHandler(this.info_box_TextChanged);
            // 
            // topic_drop_list
            // 
            this.topic_drop_list.BackColor = System.Drawing.Color.White;
            this.topic_drop_list.FormattingEnabled = true;
            this.topic_drop_list.Location = new System.Drawing.Point(80, 15);
            this.topic_drop_list.Name = "topic_drop_list";
            this.topic_drop_list.Size = new System.Drawing.Size(134, 21);
            this.topic_drop_list.TabIndex = 0;
            this.topic_drop_list.TabStop = false;
            // 
            // delete_topic
            // 
            this.delete_topic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_topic.Enabled = false;
            this.delete_topic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_topic.Location = new System.Drawing.Point(556, 592);
            this.delete_topic.Name = "delete_topic";
            this.delete_topic.Size = new System.Drawing.Size(124, 22);
            this.delete_topic.TabIndex = 6;
            this.delete_topic.TabStop = false;
            this.delete_topic.Text = "Delete Current Topic";
            this.delete_topic.UseVisualStyleBackColor = true;
            this.delete_topic.Visible = false;
            this.delete_topic.Click += new System.EventHandler(this.delete_topic_Click);
            // 
            // search_text_box
            // 
            this.search_text_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.search_text_box.BackColor = System.Drawing.Color.White;
            this.search_text_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_text_box.Location = new System.Drawing.Point(87, 592);
            this.search_text_box.Name = "search_text_box";
            this.search_text_box.Size = new System.Drawing.Size(140, 20);
            this.search_text_box.TabIndex = 4;
            this.search_text_box.TabStop = false;
            this.search_text_box.TextChanged += new System.EventHandler(this.search_text_box_TextChanged);
            // 
            // search_box
            // 
            this.search_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.search_box.AutoSize = true;
            this.search_box.Location = new System.Drawing.Point(24, 594);
            this.search_box.Name = "search_box";
            this.search_box.Size = new System.Drawing.Size(63, 17);
            this.search_box.TabIndex = 223;
            this.search_box.TabStop = false;
            this.search_box.Text = "Search:";
            this.search_box.UseVisualStyleBackColor = true;
            this.search_box.CheckedChanged += new System.EventHandler(this.search_box_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(805, 597);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 231;
            this.label3.Text = "Edit Entry #:";
            // 
            // edit_index_box
            // 
            this.edit_index_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.edit_index_box.BackColor = System.Drawing.Color.White;
            this.edit_index_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edit_index_box.Location = new System.Drawing.Point(868, 592);
            this.edit_index_box.MaxLength = 4;
            this.edit_index_box.Name = "edit_index_box";
            this.edit_index_box.Size = new System.Drawing.Size(43, 20);
            this.edit_index_box.TabIndex = 6;
            this.edit_index_box.TabStop = false;
            this.edit_index_box.TextChanged += new System.EventHandler(this.edit_index_box_TextChanged);
            // 
            // delete_entry_button
            // 
            this.delete_entry_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_entry_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_entry_button.Location = new System.Drawing.Point(699, 13);
            this.delete_entry_button.Name = "delete_entry_button";
            this.delete_entry_button.Size = new System.Drawing.Size(104, 22);
            this.delete_entry_button.TabIndex = 232;
            this.delete_entry_button.TabStop = false;
            this.delete_entry_button.Text = "Delete Entry";
            this.delete_entry_button.UseVisualStyleBackColor = true;
            this.delete_entry_button.Visible = false;
            this.delete_entry_button.Click += new System.EventHandler(this.delete_entry_button_Click);
            // 
            // show_edit_history
            // 
            this.show_edit_history.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_edit_history.AutoSize = true;
            this.show_edit_history.Location = new System.Drawing.Point(252, 594);
            this.show_edit_history.Name = "show_edit_history";
            this.show_edit_history.Size = new System.Drawing.Size(109, 17);
            this.show_edit_history.TabIndex = 233;
            this.show_edit_history.TabStop = false;
            this.show_edit_history.Text = "Show Edit History";
            this.show_edit_history.UseVisualStyleBackColor = true;
            this.show_edit_history.CheckedChanged += new System.EventHandler(this.show_edit_history_CheckedChanged);
            // 
            // print_button
            // 
            this.print_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.print_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print_button.Location = new System.Drawing.Point(686, 592);
            this.print_button.Name = "print_button";
            this.print_button.Size = new System.Drawing.Size(117, 22);
            this.print_button.TabIndex = 234;
            this.print_button.TabStop = false;
            this.print_button.Text = "Print Current Topic(s)";
            this.print_button.UseVisualStyleBackColor = true;
            this.print_button.Click += new System.EventHandler(this.print_button_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument2;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(35, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 235;
            this.label4.Text = "Topic: ";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(40, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(15, 22);
            this.button1.TabIndex = 236;
            this.button1.TabStop = false;
            this.button1.Text = "•";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(61, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 22);
            this.button2.TabIndex = 237;
            this.button2.TabStop = false;
            this.button2.Text = "≡";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // IT_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 616);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.print_button);
            this.Controls.Add(this.show_edit_history);
            this.Controls.Add(this.delete_entry_button);
            this.Controls.Add(this.edit_index_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.search_box);
            this.Controls.Add(this.search_text_box);
            this.Controls.Add(this.delete_topic);
            this.Controls.Add(this.topic_drop_list);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.info_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.subject_box);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(940, 2000);
            this.MinimumSize = new System.Drawing.Size(940, 407);
            this.Name = "IT_Information";
            this.Text = "General Information";
            this.Load += new System.EventHandler(this.IT_Information_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox subject_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox info_box;
        private System.Windows.Forms.ComboBox topic_drop_list;
        private System.Windows.Forms.Button delete_topic;
        private System.Windows.Forms.TextBox search_text_box;
        private System.Windows.Forms.CheckBox search_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edit_index_box;
        private System.Windows.Forms.Button delete_entry_button;
        private System.Windows.Forms.CheckBox show_edit_history;
        private System.Windows.Forms.Button print_button;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}