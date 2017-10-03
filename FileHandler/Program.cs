using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!(args.Length == 5))
            {
            }
            else
            {
                //FILEHANDLER.EXE PARAMATERS
                
                //0-MOVE/COPY/MOVECOPY/DELETE
                //1-SOURCE PATH
                //2-MOVE PATH
                //3-COPY PATH
                //4-FILE PARAMETER

                try
                {
                    if (args[0].Contains("MOVECOPY"))
                    {
                        string[] File_in_dir = Directory.GetFiles(args[1], args[4]);
                        System.Console.WriteLine("MOVECOPY count = " + File_in_dir.Length);

                        bool done = false;
                        foreach (string FILE_PATH in File_in_dir)
                        {
                            while (!done)
                            {
                                if (File.Exists(args[2] + "\\" + Path.GetFileName(FILE_PATH)))
                                {
                                    File.Delete(args[2] + "\\" + Path.GetFileName(FILE_PATH));
                                }

                                try
                                {
                                    File.Copy(FILE_PATH, args[3] + "\\" + Path.GetFileName(FILE_PATH), true);
                                    File.Move(FILE_PATH, args[2] + "\\" + Path.GetFileName(FILE_PATH));
                                    done = true;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else if (args[0].Contains("MOVE"))
                    {
                        string[] File_in_dir = Directory.GetFiles(args[1], args[4]);
                        System.Console.WriteLine("MOVE count = " + File_in_dir.Length);

                        bool done = false;
                        foreach (string FILE_PATH in File_in_dir)
                        {
                            while (!done)
                            {
                                done = false;
                                if (File.Exists(args[2] + "\\" + Path.GetFileName(FILE_PATH)))
                                {
                                    File.Delete(args[2] + "\\" + Path.GetFileName(FILE_PATH));
                                }
                                try
                                {
                                    File.Move(FILE_PATH, args[2] + "\\" + Path.GetFileName(FILE_PATH));
                                    done = true;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else if (args[0].Contains("COPY"))
                    {
                        string[] File_in_dir = Directory.GetFiles(args[1], args[4]);
                        System.Console.WriteLine("COPY count = " + File_in_dir.Length);
                        
                        bool done = false;
                        foreach (string FILE_PATH in File_in_dir)
                        {
                            while (!done)
                            {
                                done = false;
                                try
                                {
                                    File.Copy(FILE_PATH, args[3] + "\\" + Path.GetFileName(FILE_PATH), true);
                                    done = true;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else if (args[0].Contains("DELETE"))
                    {
                        string[] File_in_dir = Directory.GetFiles(args[1], args[4]);
                        System.Console.WriteLine("DELETE count = " + File_in_dir.Length);

                        foreach (string FILE_PATH in File_in_dir)
                        {
                            File.Delete(FILE_PATH);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("PARAMETER 0 invalid");
                    }
                    //return "";
                }
                catch (Exception e)
                {
                    //return e.ToString();
                }
            }
        }
    }
}
