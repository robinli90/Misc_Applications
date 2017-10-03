using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Translator
{

    // Side by side comparison of the code from line 0 to line n where n is the number of 
    //lines of the longest file
    class CompareText
    {

        // File handling variables
        private List<string> File_One;
        private List<string> File_Two;
        private List<string> File_Output = new List<string>();
        private int Standard_Length = 0;
        public int Space_Margin = 5; // Number of units between the two files
        private string Leading_String = "";

        // Output directory of comparison file
        string output_directory = Directory.GetCurrentDirectory() + "\\temp_compare_file.txt";

        internal static CompareText instance;

        // Instantiate Encrypter
        public static CompareText Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new CompareText();
                }
                return instance;
            }
        }

        public void Compare_Files(List<string> a_1, List<string> a_2)
        {

            // Set leading string
            Set_Leading_String();

            File_One = a_1;
            File_Two = a_2;

            // Obtain longest string in first list to set standard (check first file)
            foreach (string line in File_One)
            {
                if ((line.Length) > Standard_Length)
                {
                    Standard_Length = line.Length;
                }
            }

            // Obtain longest string in first list to set standard (check second file)
            foreach (string line in File_Two)
            {
                if ((line.Length) > Standard_Length)
                {
                    Standard_Length = line.Length;
                }
            }

            File_Output.Add("====" + First_Line_Modifier("====", "=") + "====|" + First_Line_Modifier("", "=") + "======");
            File_Output.Add("====" + First_Line_Modifier("================[ Original File]", "=") + "====|" + First_Line_Modifier("====================[ Translated File]", "=") + "======");
            File_Output.Add("====" + First_Line_Modifier("====", "=") + "====|" + First_Line_Modifier("", "=") + "======");
                

            for (int i = 0; i < (File_One.Count < File_Two.Count ? File_Two.Count : File_One.Count); i++)
            {
                string line_1 = "";
                string line_2 = "";

                #region Test if in list, if not, set as empty string
                try
                {
                    line_1 = File_One[i];
                }
                catch { }
                try
                {
                    line_2 = File_Two[i].Contains("I_L_N") ? File_Two[i].Substring(5) : File_Two[i];
                    
                }
                catch { }
                #endregion

                if (i >= 999)
                {
                    File_Output.Add((i + 1).ToString() + ")   " + First_Line_Modifier(line_1) + "| " + (i + 1).ToString() + ")   " + line_2);
                }
                else if (i >= 99)
                {
                    File_Output.Add("0" + (i + 1).ToString() + ")   " + First_Line_Modifier(line_1) + "| " + "0" + (i + 1).ToString() + ")   " + line_2);
                }
                else if (i >= 9)
                {
                    File_Output.Add("00" + (i + 1).ToString() + ")   " + First_Line_Modifier(line_1) + "| " + "00" + (i + 1).ToString() + ")   " + line_2);
                }
                else
                {
                    File_Output.Add("000" + (i + 1).ToString() + ")   " + First_Line_Modifier(line_1) + "| " + "000" + (i + 1).ToString() + ")   " + line_2);
                }
            }
 

            // Delete pre-existing output comparison file
            if (File.Exists(output_directory))
            {
                File.Delete(output_directory);
            }

            /*
            using (StreamWriter sw = File.CreateText(output_directory))
            {
                string lines = "";
                foreach (string line in File_Output)
                {
                    lines = lines + line + Environment.NewLine;
                }
                sw.Write(lines);
                sw.Close();
            }
            */

            File.WriteAllLines(output_directory, File_Output);

            Process.Start(@output_directory);
        }

        private void Set_Leading_String(string token=" ")
        {
            for (int i = 0; i < Space_Margin; i++)
            {
                Leading_String = Leading_String + token;
            }
        }

        public string First_Line_Modifier(string line_1, string token=" ")
        {
            string end_trim = "";
            for (int i = 0; i < Standard_Length + Space_Margin - line_1.Length; i++)
            {
                end_trim = end_trim + token; // Add space to format line equally
            }
            return line_1 + end_trim;
        }

        public void Delete_Comparison_File()
        {
            if (File.Exists(output_directory))
            {
                File.Delete(output_directory);
            }
        }


    }
}
