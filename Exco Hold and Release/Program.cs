using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exco_Hold_and_Release
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
            //Application.Run(new Login());

            try
            {
                // Application
                if (args.Length == 0)   
                {
                    Application.Run(new Login("", "", ""));
                }
                // Console
                else
                {
                    // dept, so#, emp#
                    //Checklist CL = new Checklist(parameter, shop_order_no, employee_number, parameter1, parameter2);
                    Application.Run(new Checklist(args[0], args[1], args[2], args[3].Length > 0 ? args[3] : ""));
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("Error: " + e.ToString());
            }
        }
    }
}
