using System;
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
using Engraving_Translator;

namespace PingTest
{
    public partial class Form1 : Form
    {
        private string Order_Num = "";
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            PDFGenerator Generate = new PDFGenerator();
            Generate.Generate_Invoice(Order_Num);
            Process.Start(@"10.0.0.8Development\Robin\Apps\Data_Files\Test_Invoices\" + Order_Num + "_INVOICE.pdf");
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Generate.PerformClick();
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Order_Num = textBox1.Text;
        }
    }
}