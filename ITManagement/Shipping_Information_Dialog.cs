using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ITManagement
{
    public partial class Shipping_Information_Dialog : Form
    {
        /* DISABLE CLOSE BUTTON
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
         */

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (Tracking_Info.Length > 0 || ETA.Length > 0)
            {
                //save
                _parent2.tracking_number = Tracking_Info;
                _parent2.eta_date = ETA;
            }
            else
            {
            }
            //this.Close();
            this.Dispose();
        }

        private string ETA = string.Empty;
        private string Tracking_Info = string.Empty;
        Inventory _parent2;
        Office _parent;

        public Shipping_Information_Dialog(Inventory parent, Office parent3, string tracking = "", string _eta = "")
        {
            InitializeComponent();
            _parent2 = parent;
            _parent = parent3;
            search_box.Text = tracking;
            try
            {
                ETA = _eta;
                dateTimePicker1.Text = ETA + " 08:00:00AM";
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime ref_date = Convert.ToDateTime(dateTimePicker1.Text);
            ETA = ref_date.ToString("MM/dd/yyyy");
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            Tracking_Info = search_box.Text;
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            DateTime ref_date = Convert.ToDateTime(dateTimePicker1.Text);
            if (ETA.Length < 4) ETA = ref_date.ToString("MM/dd/yyyy");

            if (Tracking_Info.Length > 0 || ETA.Length > 0)
            {
                if (Tracking_Info.Contains(Convert.ToChar("~")))
                {
                    MessageBox.Show("Error: Invalid character detected: '~'");
                }
                else
                {
                    //save
                    _parent2.tracking_number = Tracking_Info;
                    _parent2.eta_date = ETA;
                    Reminders Rem = new Reminders(_parent, "One of your inventory orders are expected to arrive on this day. Check Inventory for more details" +
                            (Tracking_Info.Length > 0 ? " (Tracking #: " + Tracking_Info + ")" : ""));
                    //Rem.Show();
                    button1.Enabled = false;
                    _parent._APPEND_TO_SETUP_INFORMATION(5, (_parent._MASTER_SETUP_LIST[5].Length > 5 ? "▄" : "") +
                            Convert.ToDateTime(ETA).ToString("MM/dd/yyyy") + "~" +
                            "One of your inventory orders are expected to arrive on this day. Check Inventory for more details" +
                            (Tracking_Info.Length > 0 ? " (Tracking #: " + Tracking_Info + ")" : "") + "~" +
                            "99999x"
                        );
                    this.Dispose();
                }
            }
            else
            {
                this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Depreciated
            Reminders Rem = new Reminders(_parent, "One of your inventory orders are expected to arrive on this day. Check Inventory for more details" +
                    (Tracking_Info.Length > 0 ? " (Tracking #: " + Tracking_Info + ")" : ""));
            //Rem.Show();
            button1.Enabled = false;
            _parent._APPEND_TO_SETUP_INFORMATION(5, (_parent._MASTER_SETUP_LIST[5].Length > 5 ? "▄" : "") +
                    Convert.ToDateTime(ETA).ToString("MM/dd/yyyy") + "~" +
                    "One of your inventory orders are expected to arrive on this day. Check Inventory for more details" +
                    (Tracking_Info.Length > 0 ? " (Tracking #: " + Tracking_Info + ")" : "") + "~" +
                    "99999x"
                );
             * */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tracking_no = Parse_Tracking(search_box.Text);
            string URL = "http://google.ca//?gws_rd=ssl#q=";
            Process.Start(URL + tracking_no);
        }

        private string Parse_Tracking(string input)
        {
            string parsed = "";
            bool parsing = false;
            if (input.Contains("#")) // track after '#'
            {
                foreach (char c in input)
                {
                    if (parsing)
                    {
                        parsed = parsed + c.ToString();
                    }
                    if (c.ToString() == "#")
                    {
                        parsing = true;
                    }
                }
            }
            else // if no # found, try and find tracking number
            {
                foreach (char c in input)
                {
                    if ((char.IsDigit(c) || c.ToString() == " ") && parsing)
                    {
                        parsed = parsed + c.ToString();
                    }
                    else if (char.IsDigit(c))
                    {
                        parsing = true;
                        parsed = c.ToString();
                    }
                    else
                    {
                        parsing = false;
                    }
                }
            }
            while (parsed.EndsWith(" "))
            {
                parsed = parsed.TrimEnd();
            }
            return parsed;
        }

        private void display_zero_CheckedChanged(object sender, EventArgs e)
        {
            if (display_zero.Checked)
            {
                _parent2.overwrite_tracking_info = true;
            }
            else
            {
                _parent2.overwrite_tracking_info = false;
            }
        }
    }
}
