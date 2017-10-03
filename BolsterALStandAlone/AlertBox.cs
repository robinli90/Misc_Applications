using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BolsterALStandAlone
{

    public partial class AlertBox : Form
    {

        double current_x = get_x() - 298; 
        double current_y = get_y();
        double direction = -1;
        double traverse_factor = 1;
        int traverse_count = 0;
        bool alert_on = true;
        string message;
        System.Windows.Forms.Timer up_direction_tick = new System.Windows.Forms.Timer();
        string employeenumber;

        // Return screen x
        public static int get_x()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            return resolution.Width;
        }

        // Return screen y
        public static int get_y()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            return resolution.Height;
        }

        // Create the box and set scrolling parameters
        public AlertBox(string message, string employeenumber)
        {
            this.employeenumber = employeenumber;
            this.Location = new System.Drawing.Point(Convert.ToInt32(current_x), Convert.ToInt32(current_y));
            this.message = message;
            InitializeComponent();
            up_direction_tick.Interval = 50;
            up_direction_tick.Enabled = true;
            up_direction_tick.Tick += new EventHandler(traverse_alert);
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            this.messagebox.Text = message;
            // Starting traverse point: 
            //this.Location = new System.Drawing.Point(current_x, current_y);
        }

        // Close alert
        private void close_button_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Close();
        }

        // Entire scrolling functionality
        private void traverse_alert(object sender, EventArgs e)
        {
            if (alert_on)
            {
                traverse_count++;
                current_y = current_y + (direction) * (3 * traverse_factor);
                this.Location = new System.Drawing.Point(Convert.ToInt32(current_x), Convert.ToInt32(current_y));
            }
            if (traverse_count < 10)
            {
                traverse_factor = 1.5;
            }
            else if (traverse_count < 17)
            {
                traverse_factor = 2.2;
            }
            InitializeComponent();

            if (traverse_count > 19)
            {
                alert_on = false;
                up_direction_tick.Enabled = false;
                System.Windows.Forms.Timer down_direction_tick = new System.Windows.Forms.Timer();
                up_direction_tick.Interval = 10000;
                up_direction_tick.Enabled = true;
                up_direction_tick.Tick += new EventHandler(Close);
            }
        }

        // Close
        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }

        // Hide button from alert
        public void HideButton()
        {
            this.view_button.Visible = false;
        }

        // Open messenger when view
        private void view_button_Click(object sender, EventArgs e)
        {
        }


        private void alert_mouse_down(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dispose();
            this.Close();
        }
    }
}
