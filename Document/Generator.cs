using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Document
{
    public partial class Generator : Form
    {
        private int ENC_FACTOR = 415;
        private int MULT_ENC_FACTOR = 20;

        public Generator()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(139, 102);
            password.Visible = true;
            password.Location = new System.Drawing.Point(14, 38);
            password.Text = ((Convert.ToInt32(DateTime.Now.Hour.ToString() + DateTime.Now.Day.ToString()) * MULT_ENC_FACTOR) * ENC_FACTOR).ToString();

        }

        private void password_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
