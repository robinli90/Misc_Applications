using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic;
using System.Net;

namespace _5_Axis_Translator
{
    class Program
    {

        public static string input_path = "";
        public static string output_path = "";

        public static string ORDERNUMBER = "";

        static void Main(string[] args)
        {
            {
                // Application
                if (args.Length == 3)
                {
                    input_path = args[0];
                    output_path = args[1];
                    ORDERNUMBER = args[2].Substring(0, 6);
                    Program p = new Program();
                    p.Run2();
                }
            }
        }

        bool First_Insert = false;

        List<string> Lines = new List<string>();
        List<string> Lines2 = new List<string>();

        private void Run2()
        {
            if (File.Exists(input_path))
            {
                var text = File.ReadAllText(input_path);
                if (text.Length > 0)
                {
                    Lines = text.Split(new string[] {Environment.NewLine}, StringSplitOptions.None).ToList();
                }
            }

            // Perform actions below
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Contains("Q1703="))
                {
                    string insertStr = "";


                    // Check mandrel file first
                    //string turnFilePath = String.Format(@"\\10.0.0.8\shopdata\turn\{0}C", ORDERNUMBER);
                    string turnFilePath = String.Format(@"\\192.168.12.22\curjobs\ldata\turn\smalllathe\{0}C", ORDERNUMBER);
                    if (File.Exists(turnFilePath))
                    {
                    }
                    else
                    {
                        //turnFilePath = String.Format(@"\\10.0.0.8\shopdata\turn\{0}P", ORDERNUMBER);
                        turnFilePath = String.Format(@"\\192.168.12.22\curjobs\ldata\turn\smalllathe\{0}P", ORDERNUMBER);
                    }

                    if (File.Exists(turnFilePath))
                    {
                        var text = File.ReadAllText(turnFilePath);
                        if (text.Length > 0)
                        {
                            Lines2 = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                        }

                        for (int gi = 0; gi < Lines2.Count; gi++)
                        {
                            if (Lines2[gi].Contains("C="))
                            {
                                insertStr = Convert.ToDouble(Lines2[gi].Substring(2).Trim()) >= 305.5 ? "1" : "2";
                                break;
                            }
                        }
                    }
                    Lines.Insert(i + 1, " Q1704=" + insertStr);
                    break;
                }
            }


            if (File.Exists(output_path)) File.Delete(output_path);

            if (output_path.Length > 5)
            {
                File.WriteAllLines(output_path, Lines);
            }
            else
            {
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
                Console.WriteLine("Probing file not found or other error on file");
            }
        }
    }
}



/*
int iteration_count = Lines.Count();

string x_value = "0";
string y_value = "";
string z_value = "";
string MBH_value = "";

bool bypass = false;
bool error = false;

string prob_file_path = @"\\10.0.0.8\shopdata\curjobs\sparky\DIE" + ORDERNUMBER + ".exp";
if (File.Exists(prob_file_path))
{
    var text = File.ReadAllText(prob_file_path);
    if (text.Length > 0)
    {
        Lines2 = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
    }

    foreach (string g in Lines2)
    {
        if (g.Length > 4)
        {
            string coordinate = g.Split(new string[] { "=" }, StringSplitOptions.None).ToList()[0].Trim();
            string value = g.Split(new string[] { "=" }, StringSplitOptions.None).ToList()[1];

            if (coordinate == "X") x_value = "0";// Math.Round(Convert.ToDouble(value.Trim()), 2).ToString();
            if (coordinate == "SPIGOT_DIA") y_value = "-" + Math.Round(Convert.ToDouble(value.Trim()) / 2 - 10, 2).ToString();
            //if (coordinate == "Z") z_value = Math.Round(Convert.ToDouble(value.Trim()), 2).ToString();
            if (coordinate == "MBH") MBH_value = Math.Round(Convert.ToDouble(value.Trim()), 2).ToString();
        }
    }
}
else
{
    error = true;

    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("Probing file not found or other error on file");

    string input = Microsoft.VisualBasic.Interaction.InputBox("Please see Gidda you wish to bypass this error. Enter administrative password", "Administrative Configuration Mode", "Password", -1, -1);
    if (input == "98741")
    {
        bypass = true;
        //Refresh_Tables();
    }
    else
    {
        Microsoft.VisualBasic.Interaction.MsgBox("INVALID PASSWORD. Program will not be downloaded");
        output_path = ""; // Do not output
    }

}

//string begin_str = "99999 ";
string begin_str = " ";

// Perform actions below
for (int i = 0; i < Lines.Count; i++)
{
    if ((!error || bypass) && Lines[i].Contains("M17") && x_value.Length > 0)
    {
        Lines.Insert(i + 1, begin_str + @"CALL PGM TNC:\DMG-MORI\PROBE_Z.H");
        Lines.Insert(i + 1, begin_str + "Q1703=" + MBH_value);
        Lines.Insert(i + 1, begin_str + "Q1702=" + y_value);
        Lines.Insert(i + 1, begin_str + "Q1701=" + x_value);
        i++;
        i++;
        i++;
        i++;
    }

    if (i == 2)
    {
        Lines.Insert(i, "; SHOP ORDER # " + ORDERNUMBER.ToString());
        i++;
    }

    if (Lines[i].Contains("Q359=+0") && Lines[i + 1].Contains("M01"))
    {
        Lines.Insert(i + 2, begin_str + "L XQ1601 YQ1602 R0 FMAX M91");
        Lines.Insert(i + 2, begin_str + "L ZQ1603 R0 FMAX M91");
        i++;
        i++;
    }

    if (Lines[i].Contains("ZQ1603 R0 FMAX M91") && !First_Insert)
    {
        First_Insert = true;
        Lines.Insert(i + 1, "M28");
    }

    if (Lines[i].Contains("M30"))
    {
        Lines.Insert(i, begin_str + @"CALL PGM TNC:\DMG-MORI\ENDCOMMAND.H");
        //Lines.Insert(i, begin_str + "QR10=0; REACTIVATE PROBING CYCLE"); // REMOVED AND REPLACED ABOVE 2/6/2017
        /*
        Lines.Insert(i, begin_str + "Q376=+2    ;SAFETY DISTANCE");
        Lines.Insert(i, begin_str + "Q375=+0    ;APPROACH STRATEGY");
        Lines.Insert(i, begin_str + "Q359=+0    ;ADD. LENGTH CORRECT");
        Lines.Insert(i, begin_str + "Q357=+0    ;RADIAL OFFSET");
        Lines.Insert(i, begin_str + "Q356=+1    ;MEAS. DIRECTION");
        Lines.Insert(i, begin_str + "TCH PROBE 586 TOOL BREAKAGE DETECT");
        //i+=7;
        i+=1;
    }

    if (Lines[i].Contains("Q376=+2"))
    {
        if (Lines[i - 1].Contains("Q375=+0") &&
            Lines[i - 2].Contains("Q359=+0") &&
            Lines[i - 3].Contains("Q357=+0") &&
            Lines[i - 4].Contains("Q356=+1") &&
            Lines[i - 5].Contains("TOOL BREAKAGE DETECT"))
        {
            Lines.Insert(i + 1, begin_str + @"CALL PGM TNC:\DMG-MORI\TOOL_CHECK.H");
            i++;
        }
    }

    if (Lines[i].Contains("CYCL DEF 247 DATUM SETTING"))
    {
        if (Lines[i - 1].Contains("Q339=+1; DATUM SETTINGS"))
        {
            Lines.Insert(i + 1, begin_str + "Q241=+Q1850; DEFAULT WEIGHT");
            Lines.Insert(i + 1, begin_str + "Q240=+1; PROCESS MODE");
            Lines.Insert(i + 1, begin_str + "CYCL DEF 392 ATC");
            i++;
            i++;
            i++;
        }
    }
}

if (File.Exists(output_path)) File.Delete(output_path);

if (output_path.Length > 5)
{
    File.WriteAllLines(output_path, Lines);
}
else
{
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR");
    Console.WriteLine("Probing file not found or other error on file");
}

}

}
}
*/

        /* MICHIGAN MACHINE FOR GARY
        private void Run2()
        {
            if (File.Exists(input_path))
            {
                var text = File.ReadAllText(input_path);
                if (text.Length > 0)
                {
                    Lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                }
            }

            int iteration_count = Lines.Count();
            bool T1010_Found = false;

            // Perform actions below
            for (int i = 0; i < iteration_count; i++)
            {
                int x_location = Lines[i].ToLower().IndexOf('x');
                if (x_location >= 0 && T1010_Found)
                {
                    Lines[i] = Lines[i].Substring(0, x_location) + Get_Value_After_X(Lines[i].Substring(x_location));
                }
                else
                {
                }
                if (Lines[i].ToLower().Contains("t1010"))
                {
                    T1010_Found = true;
                }
                if (T1010_Found && Lines[i].ToLower().Contains("t1000"))
                {
                    T1010_Found = false;
                }
            }

            if (File.Exists(output_path)) File.Delete(output_path);

            File.WriteAllLines(output_path, Lines);
        }
         * 
        

        private string Get_Value_After_X(string g)
        {
            bool Checking_Value = false;
            string value = "";

            for (int i = 0; i < g.Length; i++)
            {
                if ((g[i] == 'x' || g[i] == 'X') && !Checking_Value)
                     {
                    Checking_Value = true;
                    i++;
                }

                if (Checking_Value && (!Char.IsDigit(g[i]) && g[i] != '.' && g[i] != '-' || i == g.Length - 1))
                {
                    if (Convert.ToDouble(value) < 1.33) return "X1.33" + g.Substring(i);
                    else return "X" + value + g.Substring(i);
                }
                else if (Checking_Value)
                 {
                    value += g[i];
                }
            }
            return "";
        }
    }
}
        */