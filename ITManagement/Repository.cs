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
    public partial class Repository : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _parent._MASTER_SETUP_LIST[6] = textBox.Text;
        }


        Office _parent;

        public Repository(Office parent)
        {
            InitializeComponent();
            _parent = parent;
            textBox.Text = _parent._MASTER_SETUP_LIST[6];

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
        }

        private void Repository_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox.Text += Environment.NewLine + "    ≡";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.Text += Environment.NewLine + "        • ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox.Text += DateTime.Now.ToShortDateString() + " ";
        }
    }
}
