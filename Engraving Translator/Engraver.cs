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
        internal double Offset_Angle = 0;
        internal double Die_Diameter = 0;
        internal double Alpha_Angle = 0;

        internal string Config_file_path = Directory.GetCurrentDirectory() + "\\config.txt";
        internal string Character_info_path = "\\\\10.0.0.8\\Shopdata\\curjobs\\mill\\";

        public List<string> Get_Engrave_Translation(string Keyways, List<string> Engrave_string, string Die_Diameter_parent, string _Offset_Angle, string _Engraving_Height)
        {
            Offset_Angle = Convert.ToDouble(_Offset_Angle);
            Current_Angle = Current_Angle + Offset_Angle;
            //Set die diameter
            Die_Diameter = Convert.ToDouble(Die_Diameter_parent);


            //Set alpha angle
            Set_Alpha();

            

            //Set keyway locations (check jaw angles if height < 27 for angles 0, 120, 240)
            string[] temp = (Convert.ToInt32(_Engraving_Height) <= 27 ? Keyways + ",4,116,124,236,244,356" : Keyways).Split(new string[] { "," }, StringSplitOptions.None);
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
                        string templine = ParseLine(line);
                        Entire_Translation.Add(templine);
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
                        string templine = ParseLine(line);
                        Entire_Translation.Add(templine);
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
                if (angle + Offset_Angle > (g - 10 + Offset_Angle) && angle + Offset_Angle < (g + 10 + Offset_Angle))
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
            Console.WriteLine("Die diameter is :" + Die_Diameter);
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

        private int Engrave_String(string text) // Verify and return the appropriate angle for next engraving
        {
            // Check if any of the alpha angles are invalid within the safe array, if so, set that angle to current angle and call this method again
            bool valid = true;
            int retry_count = 100;
            double starting_angle = Current_Angle;
            for (int i = 1; i < text.Length + 1; i++)
            {
                if (retry_count < 0)
                {
                    Entire_Translation.Add("ERROR: ENGRAVING STRING TOO LONG ");
                    return 0;
                }
                if (i == 1)
                {
                    starting_angle = Current_Angle;
                }
                if (((Alpha_Angle * i + Current_Angle) < 360 + Offset_Angle) && Check_Angle_Safety(Current_Angle))
                {
                    //Console.WriteLine("angle is okay : " + Current_Angle + "       Without offset angle = " + (Current_Angle - Offset_Angle));
                    Current_Angle = Alpha_Angle + Current_Angle;
                }
                else
                {
                    //valid = false;
                    i = 0;
                    //Console.WriteLine("ERROR in angle: " + Current_Angle + "       Without offset angle = " + (Current_Angle - Offset_Angle));
                    // If not, move to next safe angle
                    Current_Angle = Alpha_Angle + Current_Angle;
                    //Engrave_String(text);
                    retry_count--;
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

                    Entire_Translation.Add("(Character: '" + (c.ToString() == "/" ? "/" : c.ToString()) + "' at angle '" + Math.Round(((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)), 2).ToString() + "')");
                    Entire_Translation.Add("G00 B" + Math.Round(((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)), 2).ToString());
                    Console.WriteLine("G00 B" + ((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)).ToString());
                    try
                    {
                        var text2 = File.ReadAllText(Character_info_path + (c.ToString() == "/" ? "#" : c.ToString()) + ".ptp");
                        string[] lines = text2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            if (line.Length > 0)
                            {
                                string templine = ParseLine(line);
                                Entire_Translation.Add(templine);
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
                Current_Angle = Current_Angle + 2 * Alpha_Angle;
            }
            return 0;
        }


        private string ParseLine(string line)
        {
            string rtnstr = "";

            rtnstr = line;
            int tempint = 0;

            if ((rtnstr.Substring(0, 1) == "N" && Int32.TryParse(rtnstr.Substring(1, 1), out tempint)) && line.Length > 2)
            {//take out N line part
                bool isNumber = true;
                int idx = 1;
                while (isNumber)
                {
                    idx += 1;
                    isNumber = Int32.TryParse(rtnstr.Substring(idx, 1), out tempint);

                    if (!isNumber)
                    {
                        if (rtnstr.Substring(idx, 1) == " ")
                        {
                            isNumber = true;
                        }
                    }
                }
                rtnstr = rtnstr.Substring(idx);
            }

            return rtnstr;
        }
    }
}


// ROBIN OLD 5/11/2016
/*using System;
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
        internal double Offset_Angle = 90;
        internal double Die_Diameter = 0;
        internal double Alpha_Angle = 0;

        internal string Config_file_path = Directory.GetCurrentDirectory() + "\\config.txt";
        internal string Character_info_path = "\\\\10.0.0.8\\Shopdata\\curjobs\\mill\\";

        public List<string> Get_Engrave_Translation(string Keyways, List<string> Engrave_string, string Die_Diameter_parent, string _Offset_Angle, string Engraving_Height)
        {
            Offset_Angle = Convert.ToDouble(_Offset_Angle);
            Current_Angle = Current_Angle + Offset_Angle;
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
                if (angle + Offset_Angle > (g - 10 + Offset_Angle) && angle + Offset_Angle < (g + 10 + Offset_Angle))
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
            Console.WriteLine("Die diameter is :" + Die_Diameter);
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

        private int Engrave_String(string text) // Verify and return the appropriate angle for next engraving
        { 
            // Check if any of the alpha angles are invalid within the safe array, if so, set that angle to current angle and call this method again
            bool valid = true;
            int retry_count = 100;
            double starting_angle = Current_Angle;
            for (int i = 1; i < text.Length + 1; i++)
            {
                if (retry_count < 0)
                {
                    Entire_Translation.Add("ERROR: ENGRAVING STRING TOO LONG ");
                    return 0;
                }
                if (i == 1)
                {
                    starting_angle = Current_Angle;
                }
                if (((Alpha_Angle * i + Current_Angle) < 360 + Offset_Angle) && Check_Angle_Safety(Current_Angle))
                {
                    Console.WriteLine("angle is okay : " + Current_Angle + "       Without offset angle = " + (Current_Angle - Offset_Angle));
                    Current_Angle = Alpha_Angle + Current_Angle;
                }
                else
                {
                    //valid = false;
                    i = 0;
                    Console.WriteLine("ERROR in angle: " + Current_Angle + "       Without offset angle = " + (Current_Angle - Offset_Angle));
                    // If not, move to next safe angle
                    Current_Angle = Alpha_Angle + Current_Angle;
                    //Engrave_String(text);
                    retry_count--;
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

                    Entire_Translation.Add("(Character: '" + (c.ToString() == "/" ? "/" : c.ToString()) + "' at angle '" + ((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)).ToString() + "')");
                    Entire_Translation.Add("G00 B" + ((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)).ToString());
                    Console.WriteLine("G00 B" + ((Current_Angle > 360 ? (Current_Angle - 360) : Current_Angle)).ToString());
                    try
                    {
                        var text2 = File.ReadAllText(Character_info_path + (c.ToString() == "/" ? "#" : c.ToString()) + ".ptp");
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
                Current_Angle = Current_Angle + 2 * Alpha_Angle;
            }
            return 0;
        }
    }
}*/
