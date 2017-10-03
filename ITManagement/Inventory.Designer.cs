namespace ITManagement
{
    partial class Inventory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inventory_grid = new System.Windows.Forms.DataGridView();
            this.User = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_inv_button = new System.Windows.Forms.Button();
            this.item_desc = new System.Windows.Forms.TextBox();
            this.item_quantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.transfer_item = new System.Windows.Forms.Button();
            this.update_item = new System.Windows.Forms.Button();
            this.delete_item = new System.Windows.Forms.Button();
            this.display_zero = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.total_count = new System.Windows.Forms.Label();
            this.print_list = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.delete_shopping_item = new System.Windows.Forms.Button();
            this.total_amt = new System.Windows.Forms.TextBox();
            this.move_to_inventory = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.shopping_grid = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.add_shopping_cart = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.notes_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.inventory_grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shopping_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // inventory_grid
            // 
            this.inventory_grid.AllowUserToAddRows = false;
            this.inventory_grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inventory_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.inventory_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.inventory_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.inventory_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inventory_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.Column1,
            this.D,
            this.Column2});
            this.inventory_grid.Location = new System.Drawing.Point(12, 69);
            this.inventory_grid.MultiSelect = false;
            this.inventory_grid.Name = "inventory_grid";
            this.inventory_grid.RowHeadersVisible = false;
            this.inventory_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inventory_grid.Size = new System.Drawing.Size(668, 363);
            this.inventory_grid.TabIndex = 1;
            // 
            // User
            // 
            this.User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.User.HeaderText = " ";
            this.User.Name = "User";
            this.User.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.User.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.User.Width = 35;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Inventory";
            this.Column1.Name = "Column1";
            // 
            // D
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.D.DefaultCellStyle = dataGridViewCellStyle3;
            this.D.HeaderText = "Quantity";
            this.D.Name = "D";
            this.D.Width = 50;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "Date Added";
            this.Column2.Name = "Column2";
            this.Column2.Width = 89;
            // 
            // add_inv_button
            // 
            this.add_inv_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_inv_button.Location = new System.Drawing.Point(441, 37);
            this.add_inv_button.Name = "add_inv_button";
            this.add_inv_button.Size = new System.Drawing.Size(122, 22);
            this.add_inv_button.TabIndex = 187;
            this.add_inv_button.Text = "Add to Inventory";
            this.add_inv_button.UseVisualStyleBackColor = true;
            this.add_inv_button.Visible = false;
            this.add_inv_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // item_desc
            // 
            this.item_desc.BackColor = System.Drawing.Color.White;
            this.item_desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.item_desc.Location = new System.Drawing.Point(69, 11);
            this.item_desc.Name = "item_desc";
            this.item_desc.Size = new System.Drawing.Size(366, 20);
            this.item_desc.TabIndex = 188;
            this.item_desc.TextChanged += new System.EventHandler(this.item_desc_TextChanged);
            // 
            // item_quantity
            // 
            this.item_quantity.BackColor = System.Drawing.Color.White;
            this.item_quantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.item_quantity.Location = new System.Drawing.Point(69, 37);
            this.item_quantity.Name = "item_quantity";
            this.item_quantity.Size = new System.Drawing.Size(46, 20);
            this.item_quantity.TabIndex = 189;
            this.item_quantity.TextChanged += new System.EventHandler(this.item_quantity_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 19);
            this.label1.TabIndex = 212;
            this.label1.Text = "Item:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 213;
            this.label2.Text = "Quantity:";
            // 
            // transfer_item
            // 
            this.transfer_item.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transfer_item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transfer_item.Location = new System.Drawing.Point(468, 438);
            this.transfer_item.Name = "transfer_item";
            this.transfer_item.Size = new System.Drawing.Size(103, 22);
            this.transfer_item.TabIndex = 214;
            this.transfer_item.Text = "Transfer Item";
            this.transfer_item.UseVisualStyleBackColor = true;
            this.transfer_item.Click += new System.EventHandler(this.transfer_item_Click);
            // 
            // update_item
            // 
            this.update_item.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.update_item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update_item.Location = new System.Drawing.Point(359, 438);
            this.update_item.Name = "update_item";
            this.update_item.Size = new System.Drawing.Size(103, 22);
            this.update_item.TabIndex = 215;
            this.update_item.Text = "Update Item";
            this.update_item.UseVisualStyleBackColor = true;
            this.update_item.Visible = false;
            this.update_item.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // delete_item
            // 
            this.delete_item.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delete_item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_item.Location = new System.Drawing.Point(250, 438);
            this.delete_item.Name = "delete_item";
            this.delete_item.Size = new System.Drawing.Size(103, 22);
            this.delete_item.TabIndex = 216;
            this.delete_item.Text = "Delete Item";
            this.delete_item.UseVisualStyleBackColor = true;
            this.delete_item.Visible = false;
            this.delete_item.Click += new System.EventHandler(this.delete_item_Click);
            // 
            // display_zero
            // 
            this.display_zero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.display_zero.AutoSize = true;
            this.display_zero.Location = new System.Drawing.Point(15, 443);
            this.display_zero.Name = "display_zero";
            this.display_zero.Size = new System.Drawing.Size(157, 17);
            this.display_zero.TabIndex = 217;
            this.display_zero.Text = "Display Finished Inventories";
            this.display_zero.UseVisualStyleBackColor = true;
            this.display_zero.CheckedChanged += new System.EventHandler(this.display_zero_CheckedChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(569, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 218;
            this.button1.Text = "View Shopping List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.notes_button);
            this.groupBox1.Controls.Add(this.total_count);
            this.groupBox1.Controls.Add(this.print_list);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.delete_shopping_item);
            this.groupBox1.Controls.Add(this.total_amt);
            this.groupBox1.Controls.Add(this.move_to_inventory);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.shopping_grid);
            this.groupBox1.Location = new System.Drawing.Point(689, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 459);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shopping List";
            // 
            // total_count
            // 
            this.total_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.total_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_count.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.total_count.Location = new System.Drawing.Point(12, 428);
            this.total_count.Name = "total_count";
            this.total_count.Size = new System.Drawing.Size(59, 19);
            this.total_count.TabIndex = 226;
            // 
            // print_list
            // 
            this.print_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print_list.Location = new System.Drawing.Point(300, 19);
            this.print_list.Name = "print_list";
            this.print_list.Size = new System.Drawing.Size(83, 22);
            this.print_list.TabIndex = 225;
            this.print_list.Text = "Create Order";
            this.print_list.UseVisualStyleBackColor = true;
            this.print_list.Click += new System.EventHandler(this.print_list_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 224;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(109, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 19);
            this.label3.TabIndex = 223;
            this.label3.Text = "Paid Total:";
            // 
            // delete_shopping_item
            // 
            this.delete_shopping_item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_shopping_item.Location = new System.Drawing.Point(215, 19);
            this.delete_shopping_item.Name = "delete_shopping_item";
            this.delete_shopping_item.Size = new System.Drawing.Size(82, 22);
            this.delete_shopping_item.TabIndex = 221;
            this.delete_shopping_item.Text = "Delete Item(s)";
            this.delete_shopping_item.UseVisualStyleBackColor = true;
            this.delete_shopping_item.Click += new System.EventHandler(this.delete_shopping_item_Click);
            // 
            // total_amt
            // 
            this.total_amt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.total_amt.BackColor = System.Drawing.Color.White;
            this.total_amt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.total_amt.Location = new System.Drawing.Point(168, 432);
            this.total_amt.Name = "total_amt";
            this.total_amt.Size = new System.Drawing.Size(74, 20);
            this.total_amt.TabIndex = 223;
            this.total_amt.Text = "$";
            this.total_amt.TextChanged += new System.EventHandler(this.total_amt_TextChanged);
            // 
            // move_to_inventory
            // 
            this.move_to_inventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.move_to_inventory.Location = new System.Drawing.Point(108, 19);
            this.move_to_inventory.Name = "move_to_inventory";
            this.move_to_inventory.Size = new System.Drawing.Size(103, 22);
            this.move_to_inventory.TabIndex = 220;
            this.move_to_inventory.Text = "Move to Inventory";
            this.move_to_inventory.UseVisualStyleBackColor = true;
            this.move_to_inventory.Visible = false;
            this.move_to_inventory.Click += new System.EventHandler(this.move_to_inventory_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(248, 431);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 22);
            this.button3.TabIndex = 223;
            this.button3.Text = "Receive Selected";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // shopping_grid
            // 
            this.shopping_grid.AllowUserToAddRows = false;
            this.shopping_grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.shopping_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.shopping_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.shopping_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.shopping_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shopping_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column3});
            this.shopping_grid.Location = new System.Drawing.Point(6, 47);
            this.shopping_grid.MultiSelect = false;
            this.shopping_grid.Name = "shopping_grid";
            this.shopping_grid.RowHeadersVisible = false;
            this.shopping_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.shopping_grid.Size = new System.Drawing.Size(377, 379);
            this.shopping_grid.TabIndex = 2;
            this.shopping_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.shopping_grid_CellContentClick_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(577, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 22);
            this.button2.TabIndex = 222;
            this.button2.Text = "Add to Cart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // add_shopping_cart
            // 
            this.add_shopping_cart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_shopping_cart.Location = new System.Drawing.Point(441, 9);
            this.add_shopping_cart.Name = "add_shopping_cart";
            this.add_shopping_cart.Size = new System.Drawing.Size(122, 23);
            this.add_shopping_cart.TabIndex = 223;
            this.add_shopping_cart.Text = "Add to Shopping List";
            this.add_shopping_cart.UseVisualStyleBackColor = true;
            this.add_shopping_cart.Click += new System.EventHandler(this.add_shopping_cart_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = " ";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 20;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "Item Description";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = " ";
            this.Column3.Name = "Column3";
            this.Column3.Width = 20;
            // 
            // notes_button
            // 
            this.notes_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.notes_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.notes_button.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.notes_button.Image = global::ITManagement.Properties.Resources.Notepad_Bloc_notes_icon;
            this.notes_button.Location = new System.Drawing.Point(361, 431);
            this.notes_button.Name = "notes_button";
            this.notes_button.Size = new System.Drawing.Size(20, 22);
            this.notes_button.TabIndex = 227;
            this.notes_button.UseVisualStyleBackColor = true;
            this.notes_button.Click += new System.EventHandler(this.notes_button_Click);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 468);
            this.Controls.Add(this.add_shopping_cart);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.display_zero);
            this.Controls.Add(this.delete_item);
            this.Controls.Add(this.update_item);
            this.Controls.Add(this.transfer_item);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.item_quantity);
            this.Controls.Add(this.item_desc);
            this.Controls.Add(this.add_inv_button);
            this.Controls.Add(this.inventory_grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1100, 1000);
            this.MinimumSize = new System.Drawing.Size(708, 472);
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Inventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inventory_grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shopping_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView inventory_grid;
        private System.Windows.Forms.Button add_inv_button;
        private System.Windows.Forms.TextBox item_desc;
        private System.Windows.Forms.TextBox item_quantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button transfer_item;
        private System.Windows.Forms.Button update_item;
        private System.Windows.Forms.Button delete_item;
        private System.Windows.Forms.CheckBox display_zero;
        private System.Windows.Forms.DataGridViewCheckBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView shopping_grid;
        private System.Windows.Forms.Button move_to_inventory;
        private System.Windows.Forms.Button delete_shopping_item;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox total_amt;
        private System.Windows.Forms.Button add_shopping_cart;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button print_list;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label total_count;
        private System.Windows.Forms.Button notes_button;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
    }
}