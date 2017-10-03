using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNC_Schedule
{
    public partial class Debug : Form
    {
        private string text;

        public Debug()
        {
            InitializeComponent();
            debug_text.Text = text;
        }

        public void Append_Text(string text2)
        {
            text = text + text2 + Environment.NewLine;
            debug_text.Text = text;
        }

    }
}
