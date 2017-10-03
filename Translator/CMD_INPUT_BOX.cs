using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Translator
{
    public partial class CMD_INPUT_BOX : Form
    {
        Translator _parent;

        public CMD_INPUT_BOX(Translator parent, string text)
        {
            InitializeComponent();
            _parent = parent;
            textBox1.Text = text;
        }

        private void CMD_INPUT_BOX_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _parent._CMD_PARAMETER = textBox1.Text.Trim();
            _parent.Run_Parameter_Handler();
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "-5000:F13-5001:336094-5002:10577";
        }
    }
}
