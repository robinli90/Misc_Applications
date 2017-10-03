using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace AutoBudgetEntry
{
    public partial class Form1 : Form
    {
        
        // import the function in your class
        [DllImport ("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 10)
            {
                string[] temp = (Regex.Replace(textBox1.Text, @"\s+", " ", RegexOptions.Multiline)).Split(new string[] { " " }, StringSplitOptions.None);
                List<string> temp2 = new List<string>();

                foreach (string g in temp)
                {
                    string temp_str = g;

                    if (checkBox1.Checked == true)
                    {
                        temp_str = temp_str + "-";
                    }
                    else
                    {
                        temp_str = temp_str == "-" ? "0" : temp_str;
                    }
                    temp_str = (temp_str.Replace(",", string.Empty));
                    temp_str = (temp_str.Replace(")", string.Empty));
                    temp_str = (temp_str.Replace("(", string.Empty));
                    if (temp_str.Length > 0 && temp_str != "-") temp2.Add(temp_str.Replace("$", string.Empty));
                }

                Thread.Sleep(5000);


                foreach (string g in temp2)
                {
                    SendKeys.SendWait(g);
                    Thread.Sleep(50);
                    SendKeys.Send("{Tab}");
                    Thread.Sleep(50);
                }

                textBox1.Clear();
                SendKeys.Send("{Enter}");
            }
        }
    }
}
