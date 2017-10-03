using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Translator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                // Application
                if (args.Length == 0)
                {
                    Application.Run(new Translator("", "", "", ""));
                }
                // Console
                else
                {
                    Application.Run(new Translator(args[0], args[1], args[2], args.Length > 3 ? args[3] : ""));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString());
            }
        }
    }
}
