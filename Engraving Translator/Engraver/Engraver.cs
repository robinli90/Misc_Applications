using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Engraving_Translator
{
    public class Engraver
    {
        internal List<string> Entire_Translation = new List<string>();
        internal List<double> Keyway_Locations = new List<double>();
        internal List<string> Stamp_Information = new List<string>();
        internal double Current_Angle = 0;
        internal double Die_Diameter = 0;
        internal double Alpha_Angle = 0;

        internal string Config_file_path = Directory.GetCurrentDirectory() + "\\config.txt";
        internal string Character_info_path = "\\\\10.0.0.6\\Shopdata\\curjobs\\mill\\";

        public List<string> Get_Engrave_Translation(string Keyways, List<string> Engrave_string, string Die_Diameter_parent)
        {
            //Set die diameter
            Die_Diameter = Convert.ToDouble(Die_Diameter_parent);

            //Set alpha angle
            Set_Alpha();

            //Set keyway locations
            string[] temp = Keyways.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string t in temp)
            {
                try
                {
                    Keyway_Locations.Add(Convert.ToInt32(t));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + " : No int found");
                }
            }

            //start gcode block
            #region start G-Code file
            try
            {
                var text2 = File.ReadAllText(Character_info_path + "start.ptp");
                string[] lines = text2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Entire_Translation.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid character file" + e);
            }
            #endregion

            //middle gcode block (engrave block)
            foreach (string g in Engrave_string)
            {
                Engrave_String(g);
            }

            //end gcode block
            #region end G-Code file
            try
            {
                var text2 = File.ReadAllText(Character_info_path + "end.ptp");
                string[] lines = text2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Entire_Translation.Add(line);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Invalid character file");
            }
            #endregion

            //return translation
            return Entire_Translation;
        }

        private bool Check_Angle_Safety(double angle)
        {
            bool safe = true;
            foreach (double g in Keyway_Locations)
            {
                if (angle > (g-10) && angle < (g + 10))
                {
                    safe = false;
                }
            }
            return safe;
        }

        private void Set_Alpha()
        {
            double theta = 18.0 / Die_Diameter;
            double radians = Math.Atan(theta);
            Alpha_Angle = radians * (180 / Math.PI);
            Console.WriteLine("Alpha angle is :" + Alpha_Angle);
        }

        private void Set_Character_Info_Path()
        {
            var text = File.ReadAllText(Config_file_path);
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            List<List<string>> temp_list = new List<List<string>>();
            foreach (string line in lines)
            {
                if (Get_Line_Definition(line) == "CHARACTER_INFO_PATH")
                {
                    Character_info_path = Get_Line_Value(line);
                }
            }
        }

        #region Depreciated functions
        private string Get_Line_Definition(string line)
        {
            string temp = "";
            bool done = false;
            foreach (char c in line)
            {
                if (!(c.ToString() == "=") && !done)
                {
                    temp = temp + c;
                }
                else
                {
                    done = true;
                }
            }
            return temp;
        }

        private string Get_Line_Value(string line)
        {
            string temp = "";
            bool writing = false;
            foreach (char c in line)
            {
                if ((c.ToString() == "=") && !writing)
                {
                    writing = true;
                }
                else if (writing)
                {
                    temp = temp + c;
                }
                else
                {
                }
            }
            return temp;
        }
        #endregion

        private void Engrave_String(string text) // Verify and return the appropriate angle for next engraving
        {
            // Check if any of the alpha angles are invalid within the safe array, if so, set that angle to current angle and call this method again
            bool valid = true;
            double starting_angle = Current_Angle;
            for (int i = 1; i < text.Length+1; i++)
            {
                if (i == 1)
                {
                    starting_angle = Current_Angle;
                }
                if (((Alpha_Angle * i + Current_Angle) < 360) && Check_Angle_Safety(Current_Angle))
                {
                    Console.WriteLine("angle is okay : " + Current_Angle);
                    Current_Angle = Alpha_Angle + Current_Angle;
                }
                else
                {
                    //valid = false;
                    i = 0;
                    Console.WriteLine("ERROR in angle: " + Current_Angle);
                    // If not, move to next safe angle
                    Current_Angle = Alpha_Angle + Current_Angle;
                    //Engrave_String(text);
                }
            }
            if (valid) //if the string is okay and will not be stamped on a keyway:
            {
                Current_Angle = starting_angle;
                foreach (char c in text)
                {
                    // Move tool to current angle
                    // Open character A file
                    // Append Entire_Translation with the file 'new line split'
                    Entire_Translation.Add("(Character: '" + c.ToString() + "' at angle '" + Current_Angle.ToString() + "')");
                    try
                    {
                        var text2 = File.ReadAllText(Character_info_path + c.ToString() + ".ptp");
                        string[] lines = text2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            if (line.Length > 0)
                            {
                                Entire_Translation.Add(line);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid character file");
                    }
                    Current_Angle = Current_Angle + Alpha_Angle;
                }
                // Add two character spaces between characters
                Current_Angle = Current_Angle + 2*Alpha_Angle;
            }
        }
    }
}
