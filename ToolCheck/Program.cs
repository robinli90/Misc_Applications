using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ToolCheck
{
    class Program
    {
        static string Config_File_Path = "\\\\10.0.0.8\\shopdata\\ncmachs\\MCToolCheck\\tool_check_config.txt";

        static List<string> VALID_MACHINES = new List<string>();
        static List<string> INVALID_MACHINES = new List<string>();

        // Tool_Check_Config tool list
        static List<List<string>> Tool_List = new List<List<string>>();

        static bool SPLIT_MC_LINE = false;

        static int Main(string[] args)
        {
            List<string> Output_G_Code = new List<string>();


            string Machine_Error_Message = "";
            string Machine_Name = args[2];

            // Store configuration  information
            if (File.Exists(Config_File_Path))
            {
                //Output_G_Code.Add("1");
                string[] lines = File.ReadAllText(Config_File_Path).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = 0; i < lines.Count(); i++)
                {
                    if (i==4) //VALID LIST
                    {
                        string[] temp = lines[i].Split(new string[] { ":" }, StringSplitOptions.None);
                        string[] temp3 = temp[1].Split(new string[] { "," }, StringSplitOptions.None);
                        foreach (string g in temp3)
                        {
                            VALID_MACHINES.Add(g.Trim());
                        }
                    }
                    else if (i==5) //INVALID LIST
                    {
                        string[] temp = lines[i].Split(new string[] { ":" }, StringSplitOptions.None);
                        string[] temp3 = temp[1].Split(new string[] { "," }, StringSplitOptions.None);
                        foreach (string g in temp3)
                        {
                            INVALID_MACHINES.Add(g.Trim());
                        }
                    }
                    else if (i == 6) // Machine Message
                    {
                        Machine_Error_Message = lines[i].Split(new string[] { ":" }, StringSplitOptions.None)[1].Trim();
                    }
                    else if (lines[i].StartsWith("#"))
                    {
                        string[] temp = lines[i].Split(new string[] { ";" }, StringSplitOptions.None);
                        List<string> temp2 = new List<string>();
                        temp2.Add(temp[0].Trim());
                        temp2.Add(temp[1].Trim());
                        List<string> temp3 = temp[2].Split(new string[] { "," }, StringSplitOptions.None).ToList();
                        temp3.AddRange(INVALID_MACHINES);
                        if (temp3.Count() > 1)
                        {
                            
                            //temp4.Add("");
                            foreach (string g in temp3)
                            {
                                List<string> temp4 = new List<string>();
                                temp4.Add(temp2[0]);
                                temp4.Add(temp2[1]);
                                //temp4[2] = g.Trim();
                                temp4.Add(g);
                                Tool_List.Add(temp4);
                            }
                        }
                        else
                        {
                            temp2.Add(temp[2].Trim());
                            Tool_List.Add(temp2);
                        }
                    }
                    else if (i == 7)
                    {
                        string[] temp = lines[i].Split(new string[] { ":" }, StringSplitOptions.None);
                        string[] temp3 = temp[1].Split(new string[] { "," }, StringSplitOptions.None);
                        foreach (string g in temp3)
                        {
                             if (g == Machine_Name)
                             {
                                 SPLIT_MC_LINE = true;
                             }
                        }
                    }
                }
            }

            
            string[] G_Code = File.ReadAllText(args[0]).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            bool checking = false;
            string M_Code = "";
            //int index = 0;
            bool run_on_machine = true;

            //foreach (string line in G_Code)
            for (int index = 0; index < G_Code.Length; index++)
            {
                if (G_Code[index].IndexOf("(MSG") > 0)
                {
                    foreach (List<string> tool_param in Tool_List)
                    {
                        bool done = false;
                        if (G_Code[index].Contains(tool_param[0].Substring(1)) && (tool_param[2] == Machine_Name) && !done) // If matching a tool in list and same machine
                        {
                            //Output_G_Code.Add(G_Code[index]);
                            checking = true;
                            M_Code = tool_param[1];
                            //Output_G_Code.Add(line);
                            if (INVALID_MACHINES.Contains(Machine_Name))
                            {
                                run_on_machine = false;
                            }
                            done = true;
                        }
                    }
                    if (checking)
                    {
                        for (int i = index; i < 10 + index; i++)//G_Code.Length - index; i++)
                        {
                            if (i <= G_Code.Length - 1)
                            {
                                if (G_Code[i].Contains("M") && !G_Code[i].Contains("S"))
                                {
                                    if (!SPLIT_MC_LINE)
                                    {
                                        Output_G_Code.Add(G_Code[i].Substring(0, G_Code[i].IndexOf("M")) + M_Code);
                                    }
                                    else
                                    {
                                        Output_G_Code.Add(G_Code[i].Substring(0, G_Code[i].IndexOf("M")) + M_Code.Substring(0,3));
                                        Output_G_Code.Add(G_Code[i].Substring(0, G_Code[i].IndexOf("M")) + M_Code.Substring(3,3));
                                    }
                                    checking = false;
                                    index = i;
                                    i = i + 99999;
                                }
                                else
                                {
                                    Output_G_Code.Add(G_Code[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        Output_G_Code.Add(G_Code[index]);
                    }
                    //index ++;`
                }
                else
                {
                    Output_G_Code.Add(G_Code[index]);   
                }
            }

            if (run_on_machine)
            {
                File.WriteAllLines(args[1], Output_G_Code);
            }
            else
            {
                using (StreamWriter sw = File.CreateText(args[1])) // Create LOG file
                {
                    //sw.Write("\n");
                    sw.Write(Machine_Error_Message);
                    sw.Close();
                }
                MessageBox.Show(Machine_Error_Message);
            }
        return 1;
        }
    }
}


























/*
string lines = "";
foreach (string g in Output_G_Code)
{
    lines = lines + g + Environment.NewLine;
}
if (File.Exists(args[1]))
{
    File.Delete(args[1]);
}
using (StreamWriter sw = File.CreateText(args[1])) // Create LOG file
{
    //sw.Write("\n");
    sw.Write(lines);
    sw.Close();
}
*/