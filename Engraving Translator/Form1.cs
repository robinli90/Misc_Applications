using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engraving_Translator
{
    public partial class Form1 : Form
    {
        List<string> new_str = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> test = new List<string>();
            test.Add("TESTSTRINs");
            test.Add("TES");
            test.Add("GARYTEST");
            test.Add("asdfasdfasdf");
            Engraver de = new Engraver();
            new_str = de.Get_Engrave_Translation("89,140,180", test, "500", "90", "26");

            foreach (string g in new_str)
            {
                //Console.WriteLine(g);
            }
        }
    }
}
