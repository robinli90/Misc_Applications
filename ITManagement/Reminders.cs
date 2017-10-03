using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITManagement
{
    public partial class Reminders : Form
    {
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

        Office _parent;
        string _EMP_NUM = string.Empty;

        private List<List<string>> Reminders_List = new List<List<string>>();
        List<string> Valid_Dates = new List<string>();

        bool past = false;
        bool present = false;
        bool upcoming = false;

        public Reminders(Office parent, string manually_delete_old="")
        {
            InitializeComponent();
            _parent = parent;
            _EMP_NUM = _parent._MASTER_LOGIN_EMPLOYEE_NUMBER;

            Valid_Dates = Get_Message_Dates(7, 20);
            
            // INDEX 5 = Reminders
            //              - reminder_date~message~employee#x,employee#y,employee#_readdate#,employee#_readdate#▄...
            string[] temp = _parent._MASTER_SETUP_LIST[5].Split(new string[] { "▄" }, StringSplitOptions.None);
            foreach (string reminder in temp)
            {
                if (reminder.Length > 10)
                {
                    string[] temp2 = reminder.Split(new string[] { "~" }, StringSplitOptions.None);
                    DateTime past_date = Convert.ToDateTime(temp2[0]);
                    // Remove past 7 days
                    if (past_date >= DateTime.Now.AddDays(-7))
                    {
                        Reminders_List.Add(temp2.ToList());
                    }
                }
            }

            // Sort by date
            Reminders_List.Sort((x, y) => DateTime.Compare(
                Convert.ToDateTime(x[0]), 
                Convert.ToDateTime(y[0])));


            if (manually_delete_old == "")
            {
                for (int i = 0; i < Reminders_List.Count; i++)
                {
                    for (int j = i + 1; j < Reminders_List.Count; j++)
                    {
                        if (Reminders_List[i][1] == Reminders_List[j][1])
                        {
                            Reminders_List.RemoveAt(i);
                        }
                    }
                }
                Populate_Reminders();
            }
            else
            {
                // Silently update
                for (int i = 0; i < Reminders_List.Count; i++)
                {
                    if (Reminders_List[i][1] == manually_delete_old)
                    {
                        Reminders_List.RemoveAt(i);
                    }
                }
                string temp2 = "";
                foreach (List<string> g in Reminders_List)
                {
                    temp2 = temp2 + g[0] + "~" + g[1] + "~" + g[2] + "▄";
                }
                _parent._MASTER_SETUP_LIST[5] = temp2.Trim(Convert.ToChar("▄"));
                this.Dispose();
            }
        }

        private void Populate_Reminders()
        {
            int count = 0;
            foreach (List<string> reminder in Reminders_List)
            {
                if (Valid_Dates.Contains(reminder[0]) &&
                    (reminder[2].Contains(_EMP_NUM + "x") || reminder[2].Contains("99999x")) && //msg is for current employee
                    (!reminder[2].Contains(_EMP_NUM + "_" + DateTime.Now.ToString("MM/dd/yyyy"))))
                {
                    DateTime ref_date = Convert.ToDateTime(reminder[0]);
                    // show
                    if (ref_date.Date < DateTime.Now.Date)
                    {
                        if (!past)
                        {
                            count = 0;
                            past = true;
                            reminder_box.AppendText("Past Week", Color.Gray, true);
                            reminder_box.AppendText("", Color.Gray, false);
                            //reminder_box.AppendText(temp[2], Color.Black, false);
                            //reminder_box.AppendText("", Color.Black, false);
                        }
                        count++;
                        reminder_box.AppendText(count.ToString() + ") [" + reminder[0] + "] " + reminder[1], Color.Gray, false);
                        reminder_box.AppendText("", Color.Gray, false);
                    }
                    else if (ref_date.Date == DateTime.Now.Date)
                    {
                        if (!present)
                        {
                            count = 0;
                            present = true;
                            reminder_box.AppendText("Today", Color.Navy, true);
                            reminder_box.AppendText("", Color.Black, false);
                            //reminder_box.AppendText(temp[2], Color.Black, false);
                            //reminder_box.AppendText("", Color.Black, false);
                        }
                        count++;
                        reminder_box.AppendText(count.ToString() + ") [" + reminder[0] + "] " + reminder[1], Color.Black, false);
                        reminder_box.AppendText("", Color.Black, false);
                    }
                    else if (ref_date.Date > DateTime.Now.Date)
                    {
                        if (!upcoming)
                        {
                            count = 0;
                            upcoming = true;
                            reminder_box.AppendText("Upcoming", Color.Green, true);
                            reminder_box.AppendText("", Color.Green, false);
                            //reminder_box.AppendText(temp[2], Color.Black, false);
                            //reminder_box.AppendText("", Color.Black, false);
                        }
                        count++;
                        reminder_box.AppendText(count.ToString() + ") [" + reminder[0] + "] " + reminder[1], Color.Green, false);
                        reminder_box.AppendText("", Color.Green, false);
                    }
                }
            }
        }

        public List<string> Get_Message_Dates(int past_count, int upcoming_count)
        {
            List<string> Dates = new List<string>();
            if (past_count >= 0 && upcoming_count>= 0)
            {
                for (int i = -past_count; i < upcoming_count + 1; i++)
                {
                    Dates.Add(DateTime.Now.AddDays(i).ToString("MM/dd/yyyy"));
                }
            }
            return Dates;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (no_remind_today.Checked)
            {
                // run no remind save
                // INDEX 5 = Reminders
                //              - reminder_date~message~employee#x,employee#y,employee#_readdate#,employee#_readdate#▄...
                string temp = "";
                foreach (List<string> g in Reminders_List)
                {
                    temp = temp + g[0] + "~" + g[1] + "~" + g[2] + "," + _EMP_NUM + "_" + DateTime.Now.ToString("MM/dd/yyyy") + "▄";
                }
                _parent._MASTER_SETUP_LIST[5] = temp.Trim(Convert.ToChar("▄"));
                this.Close();
            }
            else
            {
                string temp = "";
                foreach (List<string> g in Reminders_List)
                {
                    temp = temp + g[0] + "~" + g[1] + "~" + g[2] + "▄";
                }
                _parent._MASTER_SETUP_LIST[5] = temp.Trim(Convert.ToChar("▄"));
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Reminders_Load(object sender, EventArgs e)
        {
            if (!past && !present && !upcoming)
            {
                this.Visible = false;
                this.Close();
            }
        }

    }
}
