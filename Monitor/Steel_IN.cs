using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using Databases;
using System.Windows.Forms;

namespace Monitor
{
    public partial class Steel_IN : Form
    {
        List<string> dia = new List<string>();
        public double ongoing_total = 0;
        public string prev_dia = "", prev_heat = "";
        public string[,] steel_in_record = new string[50,3]; // 2D Array: [row index][dia,heat,ongoing total];
        private int ongoing_count = 0;

        private Main _parent;

        private void close_button_Click(object sender, EventArgs e)
        {
            _parent.steel_done();
            this.Visible = false;
            this.Close();
        }

        public Steel_IN(Main form1)
        {
            InitializeComponent();
            _parent = form1;
            setup_combo_box();
            steel_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            steel_table.Controls.Add(new Label() { Text = "Diameter" }, 0, steel_table.RowCount);
            steel_table.Controls.Add(new Label() { Text = "Steel Type" }, 1, steel_table.RowCount);
            steel_table.Controls.Add(new Label() { Text = "Length" }, 2, steel_table.RowCount);
            steel_table.Controls.Add(new Label() { Text = "Heat Number" }, 3, steel_table.RowCount);
            steel_table.Controls.Add(new Label() { Text = "Supplier" }, 4, steel_table.RowCount);
            steel_table.RowCount = steel_table.RowCount + 1;
            steel_in_record[steel_table.RowCount, 0] = "";
            steel_in_record[steel_table.RowCount, 1] = "";
            steel_in_record[steel_table.RowCount, 2] = "0";
        }

        // Add row with information
        private void add_row(string diameter, string steel_type, string length, string heat_number, string supplier)
        {
            steel_table.RowCount = steel_table.RowCount + 1;
            steel_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            steel_table.Controls.Add(new Label() { Text = diameter }, 0, steel_table.RowCount - 1);
            steel_table.Controls.Add(new Label() { Text = steel_type }, 1, steel_table.RowCount - 1);
            steel_table.Controls.Add(new Label() { Text = length }, 2, steel_table.RowCount - 1);
            steel_table.Controls.Add(new Label() { Text = heat_number }, 3, steel_table.RowCount - 1);
            steel_table.Controls.Add(new Label() { Text = supplier }, 4, steel_table.RowCount - 1);
        }

        // Updating the ongoing total
        private void update_ongoing_total(string dia, string heat_no, string length)
        {
            if (dia == prev_dia && heat_no == prev_heat)
            {
                //ongoing_total = ongoing_total + Convert.ToDouble(length);
                steel_in_record[steel_table.RowCount, 0] = dia;
                steel_in_record[steel_table.RowCount, 1] = heat_no;
                steel_in_record[steel_table.RowCount, 2] = (Convert.ToDouble(steel_in_record[steel_table.RowCount - 1, 2]) + Convert.ToDouble(length)).ToString();
            }
            else
            {
                prev_dia = dia;
                prev_heat = heat_no;
                ongoing_total = Convert.ToDouble(length);
                steel_in_record[steel_table.RowCount, 0] = dia;
                steel_in_record[steel_table.RowCount, 1] = heat_no;
                steel_in_record[steel_table.RowCount, 2] = length;
            }
            going_total_text.Text = "Current Heat#: " + heatbox.Text + "   Current Diameter: " + diameterbox.Text + "   Ongoing Total: " + steel_in_record[steel_table.RowCount, 2];
        }

        // Get all the diameters from decade
        private void get_diameters() {

            string query = "select distinct dia from d_steelinventory order by dia";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                dia.Add(reader[0].ToString());
            }
            reader.Close();
        }

        // Set up initial dropdown list
        private void setup_combo_box()
        {
            dia.Clear();
            diameterbox.Items.Clear();
            supplierbox.Items.Clear();
            steeltypebox.Items.Clear();
            get_diameters();
            foreach (string diameter in dia) {
                diameterbox.Items.Add(diameter);
            }
            diameterbox.SelectedIndex = 6;
            supplierbox.Items.Add("SAMUAL");
            supplierbox.Items.Add("BOHLER");
            supplierbox.Items.Add("S & B");
            supplierbox.Items.Add("OTHER");
            supplierbox.SelectedIndex = 0;
            steeltypebox.Items.Add("H-13");
            steeltypebox.Items.Add("VEX");
            steeltypebox.Items.Add("DVR");
            steeltypebox.Items.Add("QRO90");
            steeltypebox.Items.Add("W360");
            steeltypebox.SelectedIndex = 0;
        }

        // Detect if non-number input in length
        private void lengthbox_TextChanged(object sender, EventArgs e)
        {
            if (lengthbox.Text.Length > 0)
            {
                bool has_decimal = false;
                if (lengthbox.Text.Substring(0, lengthbox.Text.Length - 1).Contains(".")) has_decimal = true;
                if (!char.IsDigit(lengthbox.Text[lengthbox.Text.Length - 1]) && !(lengthbox.Text[lengthbox.Text.Length - 1].ToString() == "."))
                //if (!((lengthbox.Text.Substring(lengthbox.Text.Length-1, lengthbox.Text.Length)) == "a"))
                {
                    // If letter in SO_number box, do not output and move CARET to end
                    lengthbox.Text = lengthbox.Text.Substring(0, lengthbox.Text.Length - 1);
                    lengthbox.SelectionStart = lengthbox.Text.Length;
                    lengthbox.SelectionLength = 0;
                }
                if (lengthbox.Text[lengthbox.Text.Length - 1].ToString() == "." && has_decimal)
                {
                    lengthbox.Text = lengthbox.Text.Substring(0, lengthbox.Text.Length - 1);
                    lengthbox.SelectionStart = lengthbox.Text.Length;
                    lengthbox.SelectionLength = 0;
                }
            }
        }

        // Clear the box and reset to default
        private void clear_steel_button_Click(object sender, EventArgs e)
        {
            setup_combo_box();
            lengthbox.Text = "";
            heatbox.Text = "";
            lengtherrorbox.Visible = false; heatnumberror.Visible = false; 
        }

        // Add row button, gathers information from text boxes
        private void add_steel_button_Click(object sender, EventArgs e)
        {
            if (lengthbox.Text.Length > 0 && heatbox.Text.Length > 0) 
            {
                if (!(lengthbox.Text.Length == 1 && lengthbox.Text.ToString() == "."))
                {
                    if (lengthbox.Text[lengthbox.Text.Length-1].ToString() == ".") lengthbox.Text = lengthbox.Text + "0";
                    add_row(diameterbox.Text, steeltypebox.Text, lengthbox.Text, heatbox.Text, supplierbox.Text);
                    lengtherrorbox.Visible = false; heatnumberror.Visible = false;
                    update_ongoing_total(diameterbox.Text, heatbox.Text, lengthbox.Text);
                    ongoing_count++;
                    count_box.Text = "Total entries: " + ongoing_count.ToString();
                }
            } 
            if (lengthbox.Text.Length == 0) lengtherrorbox.Visible = true;
            if (heatbox.Text.Length == 0) heatnumberror.Visible = true;
            if (lengthbox.Text.Length > 0) lengtherrorbox.Visible = false;
            if (heatbox.Text.Length > 0) heatnumberror.Visible = false; 
        }

        // Submit and insert all rows of data into database while simultaneously deleting them
        private void submit_button_Click(object sender, EventArgs e)
        { 
            steel_table.SuspendLayout();


            string diameter, steel_type, length, heat_no, supplier;
            while (steel_table.RowCount > 1)
            {
                Control c = steel_table.GetControlFromPosition(0, steel_table.RowCount - 1);
                diameter = c.Text.ToString();
                steel_table.Controls.Remove(c);
                c = steel_table.GetControlFromPosition(1, steel_table.RowCount - 1);
                steel_type = c.Text.ToString();
                steel_table.Controls.Remove(c);
                c = steel_table.GetControlFromPosition(2, steel_table.RowCount - 1);
                length = c.Text.ToString();
                steel_table.Controls.Remove(c);
                c = steel_table.GetControlFromPosition(3, steel_table.RowCount - 1);
                heat_no = c.Text.ToString();
                steel_table.Controls.Remove(c);
                c = steel_table.GetControlFromPosition(4, steel_table.RowCount - 1);
                supplier = c.Text.ToString();
                steel_table.Controls.Remove(c);

                steel_table.RowStyles.RemoveAt(steel_table.RowCount);
                steel_table.RowCount = steel_table.RowCount - 1;

                string query = "insert into d_steelinventory (steeltype,dia,thk,heat,supplier,time,invtype,bar,isAlive) values ('"
                     + steel_type + "','" + diameter + "','" + length + "','" + heat_no + "','" + supplier + "','" + DateTime.Now.ToString() + "','buy','1','1')";

                //string query = "insert into d_teststeel values ('" + diameter + "', '" + steel_type + "', '" +
                //               length + "', '" + heat_no + "', '" + supplier + "')";
                ExcoODBC database = ExcoODBC.Instance;
                OdbcDataReader reader;
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();

                query = "update d_steelinventoryonorder set isorder = '0' where dia = '" + diameter + "' and steeltype = '" + steel_type + "'";
                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);

                reader.Close();
                ongoing_count = 0;
                count_box.Text = "";
            }
            steel_table.ResumeLayout(false);
            steel_table.PerformLayout();
            steel_table.AutoScroll = false; steel_table.AutoScroll = true;
            lengtherrorbox.Visible = false; heatnumberror.Visible = false; 
        }

        // Execute delete row
        private void delete_row_button_Click(object sender, EventArgs e) 
        {
            going_total_text.Text = "Current Heat#: " + steel_in_record[steel_table.RowCount - 1, 1] +
                "   Current Diameter: " + steel_in_record[steel_table.RowCount - 1, 0] + "   Ongoing Total: " + steel_in_record[steel_table.RowCount - 1, 2];
            delete_row();

            if (steel_table.RowCount < 2) going_total_text.Text = "";
            if (steel_table.RowCount > 1)
            {
                ongoing_count--;
                count_box.Text = "Total entries: " + ongoing_count.ToString();
            }
            else
            {
                ongoing_count = 0;
                count_box.Text = "";
            }
            //prev_dia = "";
        }

        // If press enter on length box, activate add (nmemonics)
        private void lengthbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add_steel_button.PerformClick();
            }
        }

        // If press enter on heat box, activate add (nmemonics)
        private void heatbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add_steel_button.PerformClick();
            }
        }

        // Clear entire table by running delete row multiple times
        private void clear_table_button_Click(object sender, EventArgs e)
        {
            steel_table.SuspendLayout();
            while (steel_table.RowCount > 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Control c = steel_table.GetControlFromPosition(i, steel_table.RowCount - 1);
                    steel_table.Controls.Remove(c);
                }

                steel_table.RowStyles.RemoveAt(steel_table.RowCount);
                steel_table.RowCount = steel_table.RowCount - 1;
            }
            steel_table.ResumeLayout(false);
            steel_table.PerformLayout();
            steel_table.AutoScroll = false; steel_table.AutoScroll = true;
            lengtherrorbox.Visible = false; heatnumberror.Visible = false;
            going_total_text.Text = "";
            prev_dia = "";
            ongoing_count = 0;
            count_box.Text = "";
        } 


        // Delete last row from table
        private void delete_row()
        {
            if (steel_table.RowCount > 1)
            {
                steel_table.SuspendLayout();
                for (int i = 0; i < 5; i++)
                {
                    Control c = steel_table.GetControlFromPosition(i, steel_table.RowCount-1);
                    steel_table.Controls.Remove(c);
                }
                steel_table.RowStyles.RemoveAt(steel_table.RowCount);
                steel_table.RowCount = steel_table.RowCount - 1;
                steel_table.ResumeLayout(false);
                steel_table.PerformLayout();
                steel_table.AutoScroll = false;
                steel_table.AutoScroll = true;
            }
        }

        private void diameterbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Steel_IN_Load(object sender, EventArgs e)
        {

        }

    }
}
 