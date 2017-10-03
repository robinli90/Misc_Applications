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
using CNC_Schedule;

namespace CNC_Schedule
{
    public partial class Assign_Task : Form
    {

        Dictionary<string, string> CNC_Employees = new Dictionary<string, string>();

        private string selected_employee_number = "";

        private string ordernumber = "";

        private string part = "";

        private CNC_Form s;

        public Assign_Task(CNC_Form _s, string _ordernumber, bool assigned, string _part, bool ispanic = false)
        {
            s = _s;
            part = _part;
            InitializeComponent();
            ordernumber = _ordernumber;
            if (!assigned) unassign_button.Visible = false;
            if (ispanic) panic.Checked = true;
            Get_Employees();


            foreach (KeyValuePair<string, string> d in CNC_Employees)
            {
                employee_List.Items.Add(d.Value);
            }
        }

        private void Get_Employees()
        {
            CNC_Employees.Add("10204", "Jacky");
            CNC_Employees.Add("10218", "Alex M");
            CNC_Employees.Add("10223", "Bhavesh");
            CNC_Employees.Add("10235", "Naresh");
            CNC_Employees.Add("10236", "Keetan");
            CNC_Employees.Add("10261", "P");
            CNC_Employees.Add("10327", "John");
            CNC_Employees.Add("10362", "Gary");
            CNC_Employees.Add("10380", "Ramalingam");
            CNC_Employees.Add("10383", "Harinder");
            CNC_Employees.Add("10387", "Zachary");
            CNC_Employees.Add("41500", "Alex B");
            CNC_Employees.Add("41507", "Jason");
            CNC_Employees.Add("10506", "ZHONGQI");
            CNC_Employees.Add("10584", "Prashant");
            CNC_Employees.Add("10576", "Hong");
            CNC_Employees.Add("10351", "Jaroslaw");
            CNC_Employees.Add("55555", "Hold");
            CNC_Employees.Add("20085", "George");
            CNC_Employees.Add("33333", "Panic Job!");
        }

        private void employee_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> d in CNC_Employees)
            {
                if (d.Value == (string)employee_List.SelectedItem)
                {
                    selected_employee_number = d.Key;
                }
            }
        }

        private void unassign_button_Click(object sender, EventArgs e)
        {
            string unassign_query = "delete from d_cadcamjobassignlist where  ordernumber = '" + ordernumber + "' and stage like 'CNC'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(unassign_query);
            reader.Close();
            s.Refresh_Tables();
            this.Close();
            this.Dispose();
        }

        private void assign_button_Click(object sender, EventArgs e)
        {
            if (selected_employee_number == "33333")
            {
                panic.Checked = true;
            }
            if (panic.Checked)
            {
                try { unassign_button.PerformClick(); }
                catch { }
            }
            string unassign_query = "insert into d_cadcamjobassignlist values ('" + ordernumber + "', '" + selected_employee_number + "', 'CNC', '" + DateTime.Now + "', NULL, '" + part + "', " + (panic.Checked ? "1" : "0") + ")";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(unassign_query);
            reader.Close();
            s.Refresh_Tables();
            this.Close();
            this.Dispose();
        }
    }
}
