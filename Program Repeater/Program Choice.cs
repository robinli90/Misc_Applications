using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_Repeater
{
    public partial class Program_Choice : Form
    {
        Program_Manager __parent = new Program_Manager();
        int __index = 0;
        bool __edit;

        public Program_Choice(Program_Manager _parent, int index, bool edit)
        {
            __parent = _parent;
            __index = index;
            __edit = edit;
            InitializeComponent();
        }

        // Run File handler
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Run_File_Handler rfh = new Run_File_Handler(__parent, __index);
            rfh.ShowDialog();
        }

        private void program_button_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Run_Program_Handler rfh = new Run_Program_Handler(__parent, __index);
            rfh.ShowDialog();
        }
    }
}
