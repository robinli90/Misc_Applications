using System;
using Databases;
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

namespace Monitor
{
    public partial class CRV_Generator : Form
    {
        Main _parent;
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();
        
        bool running;
        private int running_count = 0;
        string dir_path = "\\\\10.0.0.8\\shopdata\\LDATA\\ModelData\\";
        string hole_location_dir_path = "\\\\10.0.0.8\\shopdata\\holelocation\\";
        string hole_location_dir_path_dump = "\\\\10.0.0.8\\shopdata\\holelocation\\HOLE_LOC_ERROR\\";
        string imperial_path = "\\\\10.0.0.8\\shopdata\\LDATA\\DataCopy\\ImperialDATA\\";
        string ldata_path = "\\\\10.0.0.8\\shopdata\\LDATA\\";
        private string SHOP_NUMBER = "";
        bool test = false;

        bool mandrel, plate, backer;
        bool has_recess = false;
        bool has_dish = false;
        int minute = 0;
        
        // Dictionary of all the dimension values extracted from file DEPENDING on which die
        private Dictionary<string, double> MANDREL_DIMENSIONS = new Dictionary<string, double>();
        private Dictionary<string, double>   PLATE_DIMENSIONS = new Dictionary<string, double>();
        private Dictionary<string, double>  BACKER_DIMENSIONS = new Dictionary<string, double>();


        public void RESET_PARAMETERS()
        {
            MANDREL_DIMENSIONS.Clear();
            PLATE_DIMENSIONS.Clear();
            BACKER_DIMENSIONS.Clear();
            mandrel = false; 
            plate = false;
            backer = false;
            has_recess = false;
            has_dish = false;
        }

        public CRV_Generator(Main form1)
        {
            InitializeComponent();
            tick_update.Interval = 60000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(check_folder);
            running = true;
            start_button.Visible = false;
            buffer_text.AppendText("===================================\r\n");
            buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
            buffer_text.AppendText("===================================\r\n");

        }

        #region FILE HANDLING
        private void check_folder(object sender, EventArgs e)
        {
            if (running)
            {
                minute++;

                string[] File_in_dir = Directory.GetFiles(dir_path, "*" + "CRV" + "*");
                var hole_location_dir = Directory.GetFiles(hole_location_dir_path).Where(x => new FileInfo(x).CreationTime.Date == DateTime.Today.Date);

                foreach (string FILE_PATH in File_in_dir)
                {
                    if (VERIFY_CRV_FILE(FILE_PATH))
                    {
                        if (!test)
                        {
                            PROCESS_FILE(FILE_PATH);
                            buffer_text.AppendText("     -> SO NUMBER: " + SHOP_NUMBER + "\r\n");
                            running_count++;
                        }
                    }
                    else
                    {
                        // DO NOTHING
                    }
                    RESET_PARAMETERS();
                }

                string[] imperial_files = Directory.GetFiles(imperial_path);
                foreach (string IMPERIAL_PATH in imperial_files)
                {
                    Process_Imperial_Conversion(IMPERIAL_PATH, ldata_path);
                }

                if (minute > 59)
                {
                    minute = 0;
                    foreach (string FILE_PATH in hole_location_dir)
                    {
                        if (!test)
                        {
                            VERIFY_HOLE_LOC(FILE_PATH);
                            buffer_text.AppendText("     -> Processing Hole Location file\r\n");
                            buffer_text.AppendText("     -> SO NUMBER: " + SHOP_NUMBER + "\r\n");
                            running_count++;
                        }
                        else
                        {
                            // DO NOTHING
                        }
                    }
                }
                count_text.Text = "Files processed: " + running_count.ToString();
            }
        }

        // Verifying that the fyle is just a CRV file
        private bool VERIFY_CRV_FILE(string path)
        {
            bool VALID = false;
            if (path.Contains("CRV")) VALID = true;
            if (path.Contains("DCRV")) VALID = false;
            if (path.Contains("PDCRV")) VALID = false;
            if (path.Contains("BDCRV")) VALID = false;
            if (path.Contains("LDCRV")) VALID = false;
            return VALID;
        }
        #endregion

        private void VERIFY_HOLE_LOC(string file)
        {
            bool valid = true;
            bool loc_found = false;
            buffer_text.AppendText("     -> Processing file: " + file + "\r\n");
            var text = File.ReadAllText(file);
            string[] lines = text.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Contains("-"))
                {
                    valid = false;
                }
                if (line == "LOC")
                {
                    loc_found = true;
                }
                try
                {
                    if (loc_found && Convert.ToDouble(line) <= 12)
                    {
                        valid = false;
                    }
                }
                catch
                { }
            }
            if (!valid)
            {
                try
                {
                    File.Delete(hole_location_dir_path_dump + Path.GetFileName(file));
                    File.Move(file, hole_location_dir_path_dump + Path.GetFileName(file));
                    buffer_text.AppendText("     -> Error found in file: " + file + "\r\n");
                    buffer_text.AppendText("     -> Moving file: " + file + "\r\n");
                }
                catch
                {
                }
            }
        }

        private void PROCESS_FILE(string file)
        {
            try
            {

                buffer_text.AppendText("     -> Processing file: " + file + "\r\n");
                var text = File.ReadAllText(file);
                string[] lines = text.Split(new string[] {Environment.NewLine}, StringSplitOptions.None);
                int order = 0;
                foreach (string line in lines)
                {
                    if (line.Contains("SO#:")) SHOP_NUMBER = line.Substring(4);
                    // Always comes out in order: backer, plate, mandrel
                    if (parse_front(line) == "BKR")
                    {
                        order = 1;
                        backer = true;
                    }
                    if (parse_front(line) == "PLT")
                    {
                        order = 2;
                        plate = true;
                    }
                    if (parse_front(line) == "MND")
                    {
                        order = 3;
                        mandrel = true;
                    }

                    if (line.Contains("#")) // If dimension
                    {
                        if (order == 1)
                        {
                            BACKER_DIMENSIONS.Add(parse_front(line).Substring(1), parse(line));
                        }
                        if (order == 2)
                        {
                            PLATE_DIMENSIONS.Add(parse_front(line).Substring(1), parse(line));
                        }
                        if (order == 3)
                        {
                            MANDREL_DIMENSIONS.Add(parse_front(line).Substring(1), parse(line));
                        }
                    }
                }

                // Check special conditions
                CHECK_SPECIAL_DIE();

                string old_target_path = "";
                string return_string = "SO#:" + SHOP_NUMBER + "\r\n" + "DUE DATE:." + "\r\n" + "DIE TYPE:" +
                                       (has_dish ? "TUBE" : "SOLID") + "\r\n";
                if (backer) return_string += PROCESS_BACKER();
                if (plate) return_string += PROCESS_PLATE();
                if (mandrel) return_string += PROCESS_MANDREL();

                File.Delete("\\\\10.0.0.8\\shopdata\\LDATA\\DataCopy\\CRV Dump\\" + SHOP_NUMBER + "CRV.TXT");
                try
                {
                    File.Delete("\\\\10.0.0.8\\shopdata\\LDATA\\ModelData\\" + SHOP_NUMBER + "DCRV.TXT");
                }
                catch
                {
                }
                if (test)
                    File.Copy(file, "\\\\10.0.0.8\\shopdata\\LDATA\\DataCopy\\CRV Dump\\" + SHOP_NUMBER + "CRV.TXT",
                        true);
                else
                    File.Move(file, "\\\\10.0.0.8\\shopdata\\LDATA\\DataCopy\\CRV Dump\\" + SHOP_NUMBER + "CRV.TXT");

                string target_path;
                if (test)
                    target_path = "\\\\10.0.0.8\\shopdata\\LDATA\\DataCopy\\CRV Dump\\" + SHOP_NUMBER + "DCRV.txt";
                else
                {
                    //old_target_path = "\\\\10.0.0.8\\shopdata\\LDATA\\ModelData\\" + SHOP_NUMBER + "DCRV.txt";  // old format
                    target_path = "\\\\10.0.0.8\\shopdata\\LDATA\\ModelData\\" + SHOP_NUMBER + "VERI.txt"; // new format
                }
                if (File.Exists(target_path))
                {
                    File.Delete(target_path);
                }
                if (!File.Exists(target_path))
                {
                    using (StreamWriter sw = File.CreateText(target_path)) // Create LDATA file
                    {
                        sw.Write(return_string);
                        sw.Close();
                    }
                    File.Copy(target_path, "W:\\Ldata\\ModelData\\" + SHOP_NUMBER + "VERI.txt", true);
                    //File.Copy(target_path, "W:\\Ldata\\ModelData\\" + SHOP_NUMBER + "DCRV.txt", true);
                }
                else
                {
                    //buffer_text.AppendText("     -> Invalid file dimension, file not processed!\r\n");
                }
            }
            catch
            {}
        }

        // Process BACKER die shape
        private string PROCESS_BACKER()
        {
            string backer_lines = "BKRCRV" + "\r\n" + "CURVE" + "\r\n";
            try
            {

                backer_lines += PROCESS_LINE_DATA(0, 0, 0, 0, BACKER_DIMENSIONS["OD"] / 2 - 2, 0);
                backer_lines += PROCESS_LINE_DATA(0, BACKER_DIMENSIONS["OD"] / 2 - 2, 0, -2, BACKER_DIMENSIONS["OD"] / 2, 0);
                backer_lines += PROCESS_LINE_DATA(-2, BACKER_DIMENSIONS["OD"] / 2, 0, -BACKER_DIMENSIONS["ZOP"] + 2, BACKER_DIMENSIONS["OD"] / 2, 0);
                backer_lines += PROCESS_LINE_DATA(-BACKER_DIMENSIONS["ZOP"] + 2, BACKER_DIMENSIONS["OD"] / 2, 0, -BACKER_DIMENSIONS["ZOP"], BACKER_DIMENSIONS["OD"] / 2 - 2, 0);
                backer_lines += PROCESS_LINE_DATA(-BACKER_DIMENSIONS["ZOP"], BACKER_DIMENSIONS["OD"] / 2 - 2, 0, -BACKER_DIMENSIONS["ZOP"], 0, 0);
                backer_lines += "ENDCURVE" + "\r\n" + "END" + "\r\n";
            }
            catch
            {
            }
            
            //Console.WriteLine(backer_lines);
            return backer_lines;
        }

        private string PROCESS_PLATE()
        {

            string plate_lines = "PLTCRV" + "\r\n" + "CURVE" + "\r\n";
            try
            {
                if (!has_dish) // Does not have dish, starts at origin (0,0,0)  
                {
                    plate_lines += PROCESS_LINE_DATA(0, 0, 0, 0, (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0);
                    plate_lines += PROCESS_LINE_DATA(0, (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0, PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0, PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["OD"] / 2) - 10, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["OD"] / 2) - 10, 0, PLATE_DIMENSIONS["STC"] - 2.3, (PLATE_DIMENSIONS["OD"] / 2) - 3.76, 0);
                    //plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 2.3, (PLATE_DIMENSIONS["OD"] / 2) - 3.76, 0, PLATE_DIMENSIONS["STC"] - 8.3, (PLATE_DIMENSIONS["OD"] / 2) + 0.4, 0);
                    //newplate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 8.3, (PLATE_DIMENSIONS["OD"] / 2) + 0.4, 0, PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 2.3, (PLATE_DIMENSIONS["OD"] / 2) - 3.76, 0, PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    // Gidda chamfer (2x2) April 25, 2016
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, (PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"]) + 2, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    plate_lines += PROCESS_LINE_DATA((PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"]) + 2, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175 - 2, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175 - 2, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], 0, 0);

                }
                else
                {
                    plate_lines += PROCESS_LINE_DATA(0, PLATE_DIMENSIONS["DDM"] / 2, 0, 0, (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0);
                    plate_lines += PROCESS_LINE_DATA(0, (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0, PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["SDB"] / 2) - 1.5, 0, PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["OD"] / 2) - 10, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"], (PLATE_DIMENSIONS["OD"] / 2) - 10, 0, PLATE_DIMENSIONS["STC"] - 2.3, (PLATE_DIMENSIONS["OD"] / 2) - 3.76, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 2.3, (PLATE_DIMENSIONS["OD"] / 2) - 3.76, 0, PLATE_DIMENSIONS["STC"] - 8.3, (PLATE_DIMENSIONS["OD"] / 2) + 0.4, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 8.3, (PLATE_DIMENSIONS["OD"] / 2) + 0.4, 0, PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    // Gidda chamfer (2x2) April 25, 2016
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"] + 2, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"] + 2, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175 - 2, 0);
                    plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, -PLATE_DIMENSIONS["ZOP"] + 10, PLATE_DIMENSIONS["BCLR"] / 2, 0);
                    plate_lines += PROCESS_LINE_DATA(-PLATE_DIMENSIONS["ZOP"] + PLATE_DIMENSIONS["STC"], PLATE_DIMENSIONS["BCLR"] / 2, 0, -(PLATE_DIMENSIONS["DISH"] + PLATE_DIMENSIONS["BRG"]), PLATE_DIMENSIONS["FCLR"] / 2, 0);
                    //
                    plate_lines += PROCESS_LINE_DATA(-(PLATE_DIMENSIONS["DISH"] + PLATE_DIMENSIONS["BRG"]), PLATE_DIMENSIONS["FCLR"] / 2, 0, -(PLATE_DIMENSIONS["DISH"] + PLATE_DIMENSIONS["BRG"]), PLATE_DIMENSIONS["BRGDIA"] / 2, 0);
                    plate_lines += PROCESS_LINE_DATA(-(PLATE_DIMENSIONS["DISH"] + PLATE_DIMENSIONS["BRG"]), PLATE_DIMENSIONS["BRGDIA"] / 2, 0, -PLATE_DIMENSIONS["DISH"], PLATE_DIMENSIONS["BRGDIA"] / 2, 0);
                    plate_lines += PROCESS_LINE_DATA(-PLATE_DIMENSIONS["DISH"], PLATE_DIMENSIONS["BRGDIA"] / 2, 0, -PLATE_DIMENSIONS["DISH"], PLATE_DIMENSIONS["DDM"] / 2, 0);
                    plate_lines += PROCESS_LINE_DATA(-PLATE_DIMENSIONS["DISH"], PLATE_DIMENSIONS["DDM"] / 2, 0, 0, PLATE_DIMENSIONS["DDM"] / 2, 0);

                    /*
                     * 
                    (PLATE_DIMENSIONS["OD"] / 2) - 0.175 - 2, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);
                    //plate_lines += PROCESS_LINE_DATA(PLATE_DIMENSIONS["STC"] - 20, (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0, PLATE_DIMENSIONS["STC"] - PLATE_DIMENSIONS["ZOP"], (PLATE_DIMENSIONS["OD"] / 2) - 0.175, 0);  // Original back
                    plate_lines += PROCESS_LINE_DATA(-PLATE_DIMENSIONS["ZOP"] + PLATE_DIMENSIONS["STC"], 
                     * */

                }
                plate_lines += "ENDCURVE" + "\r\n" + "END" + "\r\n";
                //Console.WriteLine(plate_lines);
            }
            catch
            {
            }
            return plate_lines;
        }

        private string PROCESS_MANDREL()
        {

            string mandrel_lines = "MNDCRV" + "\r\n" + "CURVE" + "\r\n";
            try
            {
                //if (true) // Just generate outside perimeter anyway
                double opposite = ((MANDREL_DIMENSIONS["OD"] / 2) - (MANDREL_DIMENSIONS["TAPERDIA"] / 2));
                double adjacent = opposite / (Math.Tan((MANDREL_DIMENSIONS["TAPERANGLE"] * (Math.PI)) / 180));
                if (!has_recess) // Does not have dish, starts at origin (0,0,0) 
                {

                    // if has taper
                    if (MANDREL_DIMENSIONS["TAPERDIA"] > 0)
                    {
                        mandrel_lines += PROCESS_LINE_DATA(0, 0, 0, 0, MANDREL_DIMENSIONS["TAPERDIA"] / 2, 0);
                        mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["TAPERDIA"] / 2, 0, -adjacent, (MANDREL_DIMENSIONS["OD"] / 2), 0);
                        //mandrel_lines += PROCESS_LINE_DATA(-adjacent, (MANDREL_DIMENSIONS["OD"] / 2), 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0);
                        mandrel_lines += PROCESS_LINE_DATA(-adjacent, (MANDREL_DIMENSIONS["OD"] / 2), 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0); //new
                    }
                    else
                    {
                        if (MANDREL_DIMENSIONS["SD"] > 0)
                        {
                            mandrel_lines += PROCESS_LINE_DATA(0, 0, 0, 0, MANDREL_DIMENSIONS["SD"] / 2, 0);
                            mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["SD"] / 2, 0, -MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["SD"] / 2, 0);
                        }
                        mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["SD"] / 2, 0, -MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["OD"] / 2, 0);
                        //mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["OD"] / 2, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0); OLD ONE
                        mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["OD"] / 2, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0); //REPLACED
                    }
                    // removed
                    //mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0);
                    //new  mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 10, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0);
                    //new mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 10, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), MANDREL_DIMENSIONS["SDB"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), MANDREL_DIMENSIONS["SDB"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), MANDREL_DIMENSIONS["SDB"] / 2, 0, -MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDB"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDB"] / 2, 0, -MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDA"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDA"] / 2, 0, -MANDREL_DIMENSIONS["ZOA"], MANDREL_DIMENSIONS["SDA"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOA"], MANDREL_DIMENSIONS["SDA"] / 2, 0, -MANDREL_DIMENSIONS["ZOA"], 0, 0);
                }
                else
                {
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["RDP"], 0, 0, -MANDREL_DIMENSIONS["RDP"], MANDREL_DIMENSIONS["ROD"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["RDP"], MANDREL_DIMENSIONS["ROD"] / 2, 0, -2, MANDREL_DIMENSIONS["RID"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-2, MANDREL_DIMENSIONS["RID"] / 2, 0, 0, MANDREL_DIMENSIONS["RID"] / 2, 0);

                    // if has taper
                    if (MANDREL_DIMENSIONS["TAPERDIA"] > 0)
                    {
                        mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["RID"] / 2, 0, 0, MANDREL_DIMENSIONS["TAPERDIA"] / 2, 0);
                        mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["TAPERDIA"] / 2, 0, -adjacent, (MANDREL_DIMENSIONS["OD"] / 2), 0);
                        mandrel_lines += PROCESS_LINE_DATA(-adjacent, (MANDREL_DIMENSIONS["OD"] / 2), 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0);
                    }
                    else
                    {
                        // if no taper
                        mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["RID"] / 2, 0, 0, MANDREL_DIMENSIONS["SD"] / 2, 0);
                        if (MANDREL_DIMENSIONS["STEP"] > 0) // sept 28, ignore line if no step (brz customers no step for recessed jobs)
                            mandrel_lines += PROCESS_LINE_DATA(0, MANDREL_DIMENSIONS["SD"] /  2, 0, -MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["SD"] / 2, 0);
                        mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["SD"] / 2, 0, -MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["OD"] / 2, 0);
                        mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["STEP"], MANDREL_DIMENSIONS["OD"] / 2, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0);
                    }

                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 7.6), MANDREL_DIMENSIONS["OD"] / 2, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"] - 2.3), (MANDREL_DIMENSIONS["OD"] / 2) - 3.76, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 10, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), (MANDREL_DIMENSIONS["OD"] / 2) - 10, 0, -(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), MANDREL_DIMENSIONS["SDB"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-(MANDREL_DIMENSIONS["ZOJ"] - MANDREL_DIMENSIONS["STB"]), MANDREL_DIMENSIONS["SDB"] / 2, 0, -MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDB"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDB"] / 2, 0, -MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDA"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOJ"], MANDREL_DIMENSIONS["SDA"] / 2, 0, -MANDREL_DIMENSIONS["ZOA"], MANDREL_DIMENSIONS["SDA"] / 2, 0);
                    mandrel_lines += PROCESS_LINE_DATA(-MANDREL_DIMENSIONS["ZOA"], MANDREL_DIMENSIONS["SDA"] / 2, 0, -MANDREL_DIMENSIONS["ZOA"], 0, 0);
                }
                mandrel_lines += "ENDCURVE" + "\r\n" + "END" + "\r\n";
                //Console.WriteLine(mandrel_lines);
            }
            catch
            {
            }
            return mandrel_lines;
        }


        // Returns the line coordinates from start to end
        private string PROCESS_LINE_DATA(double x1, double y1, double z1, double x2 , double y2, double z2)
        {
            return "LINE " + format_decimal(x1.ToString()) + " " + format_decimal(y1.ToString()) + " " + format_decimal(z1.ToString()) + "  " + format_decimal(x2.ToString()) + " " + format_decimal(y2.ToString()) + " " + format_decimal(z2.ToString()) + "\r\n"; 
        }

        private string PROCESS_ARC_DATA(double d1, double d2, double d3, double d4, double d5)
        {
            // Return the ARC function for the line data.
            double theta = d1; //sweeping angle
                
            return "";
        }

        private void CHECK_SPECIAL_DIE()
        {
            try
            {

                // Check if plate has dish
                if (plate)
                {
                    if (PLATE_DIMENSIONS["DISH"] > 0 &&
                         PLATE_DIMENSIONS["BRG"] > 0 &&
                         PLATE_DIMENSIONS["DDM"] > 0 &&
                         PLATE_DIMENSIONS["BRGDIA"] > 0 &&
                         PLATE_DIMENSIONS["FCLR"] > 0 &&
                         PLATE_DIMENSIONS["BCLR"] > 0)
                    {
                        has_dish = true;
                    }
                }

                if (mandrel)
                {

                    // Check if mandrel has recess
                    if (MANDREL_DIMENSIONS["RID"] > 0 &&
                         MANDREL_DIMENSIONS["ROD"] > 0 && 
                         MANDREL_DIMENSIONS["RDP"] > 0)
                    {
                        has_recess = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void Process_Imperial_Conversion(string input_path, string output_path)
        {
            List<string> output_file_val = new List<string>();
            var text = File.ReadAllText(input_path);
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            try
            {
                foreach (string line in lines)
                {
                    if (line.Contains("LINE") || line.Contains("ARC"))
                    {
                        string[] point = line.Split(new string[] { "  " }, StringSplitOptions.None);
                        string temp = "";
                        if (point.Length == 2)
                        {
                            string[] first_dim = point[0].Split(new string[] { " " }, StringSplitOptions.None);
                            string[] second_dim = point[1].Split(new string[] { " " }, StringSplitOptions.None);
                            temp =
                                first_dim[0] + " " +
                                Convert_Value(first_dim[1]) + " " +
                                Convert_Value(first_dim[2]) + " " +
                                Convert_Value(first_dim[3]) + "  " +
                                Convert_Value(second_dim[0]) + " " +
                                Convert_Value(second_dim[1]) + " " +
                                Convert_Value(second_dim[2]);
                            string temp_2 = temp;
                        }
                        else
                        {
                            string[] first_dim = point[0].Split(new string[] { " " }, StringSplitOptions.None);
                            string[] second_dim_0 = point[1].Split(new string[] { " " }, StringSplitOptions.None);
                            string[] second_dim_1 = point[2].Split(new string[] { " " }, StringSplitOptions.None);
                            temp =
                                first_dim[0] + " " +
                                Convert_Value(first_dim[1]) + " " +
                                Convert_Value(first_dim[2]) + " " +
                                Convert_Value(first_dim[3]) + "  " +
                                Convert_Value(second_dim_0[0]) + " " +
                                Convert_Value(second_dim_1[0]) + " " +
                                Convert_Value(second_dim_1[1]);
                        }
                        output_file_val.Add(temp);

                    }
                    else
                    {
                        output_file_val.Add(line);
                        
                    }
                }
            }
            catch
            {
                AlertBox g = new AlertBox("Error converting file to metric: " + input_path ,"");
            }

            string return_string = "";
            foreach (string line in output_file_val)
                return_string = return_string + line + Environment.NewLine;

            string return_path = output_path + Path.GetFileName(input_path);
            if (!File.Exists(return_path))
            {
                using (StreamWriter sw = File.CreateText(return_path)) // Create LDATA file
                {
                    sw.Write(return_string);
                    sw.Close();
                }
            }
            File.Delete(input_path);
        }

        // Convert input value to whichever system with the 8 digit output formatting
        private string Convert_Value(string input_value, string convert_system_str="M")
        {
            try
            {
                double test_double_conversion = Convert.ToDouble(input_value);
                // if convert input_value to metric
                if (convert_system_str == "M")
                {
                    return format_decimal((Convert.ToDouble(input_value) * 25.4).ToString());
                }
                else if (convert_system_str == "I")
                {
                    return format_decimal((Convert.ToDouble(input_value) / 25.4).ToString());
                }
                else
                {
                    return input_value;
                }
            }
            catch
            {
                return input_value;
            }
        }

        // Get diameter value converted into radius based on which die_type; depreciated
        /*
        private double GET_DIAMETER_VALUE(string dimension, string die_type)
        {
            if (diameter_values.Contains(dimension))
            {
                // BACKER
                if (dimension == "OD" && die_type == "B") return BACKER_DIMENSIONS[dimension] / 2;

                // PLATE
                if (dimension == "OD" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;
                if (dimension == "BCLR" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;
                if (dimension == "FCLR" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;
                if (dimension == "BRGDIA" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;
                if (dimension == "DDM" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;
                if (dimension == "SDB" && die_type == "P") return PLATE_DIMENSIONS[dimension] / 2;

                // MANDREL
                if (dimension == "OD" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;
                if (dimension == "SDA" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;
                if (dimension == "ROD" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;
                if (dimension == "RID" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;
                if (dimension == "SD" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;
                if (dimension == "SDB" && die_type == "M") return MANDREL_DIMENSIONS[dimension] / 2;

                return 0;
            }
            else
            {
                return 0;
            }
        }
         * */


        #region TEXT HANDLING
        // Trim ALL spaces in string
        string trim(string str)
        {
            string temp = "";
            foreach (char c in str)
            {
                if (!(c.ToString() == " ")) temp = temp + c.ToString();
            }
            return temp;
        }

        // Parse number from string and convert from string to double (*if there is arithmetic, execute)
        // Ex. parse_front("#STB=273.75") returns 273.75 (double)
        private double parse(string line)
        {
            bool parsing = false;
            string str = trim(line), temp = "", temp_back = "0";
            int i = 0;
            foreach (char c in str)
            {
                if (parsing == true)
                {
                    temp = temp + c.ToString();
                }
                if (c.ToString() == "=") parsing = true;
                if (c.ToString() == "+")
                {
                    temp_back = temp.Substring(0, temp.Length - 1);
                    temp = "";
                }
                if (c.ToString() == "-")
                {
                    temp_back = temp.Substring(0, temp.Length - 1);
                    temp = "-";
                }
                i++;
            }
            try
            {
                double x = Convert.ToDouble(temp);
                double y = Convert.ToDouble(temp_back);
                return x + y;
            }
            catch
            {
                return 999999999999; // addition error or invalid statement returns invalid value
            }
        }

        // Parse sequence in front of the "=" to obtain dimension label
        // Ex. parse_front("#STB=273.75") returns "#STB"
        private string parse_front(string line)
        {
            string str = trim(line), temp = "";
            bool parsing = true;
            foreach (char c in str)
            {
                if (c.ToString() == "=") parsing = false;
                if (parsing == true) temp = temp + c.ToString();
            }
            return temp;
        }

        // Format 8 decimal places at end of decimal
        private string format_decimal(string @decimal)
        {
            string decimal_temp = @decimal;
            if (!@decimal.Contains(".")) decimal_temp = @decimal + ".0";
            if (decimal_temp == "0" || decimal_temp == "0.0") return "0.00000000";
            bool decimal_found = false;
            string leading_string = "";  string trailing_string = ""; string return_string = "";
            for (int i = 0; i < decimal_temp.Length; i++)
            {
                if (decimal_temp[i].ToString() == ".") decimal_found = true;
                if (decimal_found)
                {
                    trailing_string = trailing_string + decimal_temp[i].ToString();
                }
                else
                {
                    leading_string = leading_string + decimal_temp[i].ToString();
                }
            }
            if (trailing_string.Length > 8) trailing_string = trailing_string.Substring(0, 8);
            for (int i = 0; i < 9 - trailing_string.Length; i++)
            {
                return_string = return_string + "0";
            }
            return leading_string + trailing_string + return_string;
        }

        #endregion



        #region GUI

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            /*
            string query = "update d_active set CAD_Print_processor_active = '0' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            // REPORT REMOVAL //_parent.cad_print_done();
             * */
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                try
                {
                    running = true;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = false;
                    stop_button.Visible = true;
                    pictureBox1.Enabled = true;

                    //string query = "update d_active set CAD_Print_processor_active = '1' where employeenumber = '10577'";
                    //ExcoODBC database = ExcoODBC.Instance;
                    //OdbcDataReader reader;
                    //database.Open(Database.DECADE_MARKHAM);
                    //reader = database.RunQuery(query);
                    //reader.Close();
                }
                catch { }
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (running)
            {
                try
                {
                    running = false;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread stopped at: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = true;
                    stop_button.Visible = false;
                    pictureBox1.Enabled = false;

                    //string query = "update d_active set CAD_Print_processor_active = '0' where employeenumber = '10577'";
                    //ExcoODBC database = ExcoODBC.Instance;
                    //OdbcDataReader reader;
                    //database.Open(Database.DECADE_MARKHAM);
                    //reader = database.RunQuery(query);
                    //reader.Close();
                }
                catch { }
            }
        }

        #endregion

        #region TEST FUNCTIONS
        private void button1_Click(object sender, EventArgs e)
        {
            PROCESS_FILE("C:\\Users\\administrator\\Desktop\\325452CRV.TXT");
            buffer_text.AppendText("     -> SO NUMBER: " + SHOP_NUMBER + "\r\n");
            buffer_text.AppendText("     -> ========================== " + "\r\n");
            buffer_text.AppendText("     -> BACKER : dim count: " + BACKER_DIMENSIONS.Count + "\r\n");
            foreach (KeyValuePair<string, double> d in BACKER_DIMENSIONS)
            {
                buffer_text.AppendText("     -> " + d.Key + " : " + d.Value + "\r\n");
            }
            buffer_text.AppendText("     -> ========================== " + "\r\n");
            buffer_text.AppendText("     -> PLATE : dim count: " + PLATE_DIMENSIONS.Count + "\r\n");
            foreach (KeyValuePair<string, double> d in PLATE_DIMENSIONS)
            {
                buffer_text.AppendText("     -> " + d.Key + " : " + d.Value + "\r\n");
            }
            buffer_text.AppendText("     -> ========================== " + "\r\n");
            buffer_text.AppendText("     -> MANDREL : dim count: " + MANDREL_DIMENSIONS.Count + "\r\n");
            foreach (KeyValuePair<string, double> d in MANDREL_DIMENSIONS)
            {
                buffer_text.AppendText("     -> " + d.Key + " : " + d.Value + "\r\n");
            }
        }
#endregion
    }
}


/* OLD CAD PRINT PROGRAM OVERWRITTEN TO CRV GENERATOR

using System;
using Databases;
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

namespace Monitor
{
    public partial class CRV_Generator : Form
    {
        Main _parent;
        string printer_path;
        System.Windows.Forms.Timer tick_update = new System.Windows.Forms.Timer();
        bool running;
        private string[] CAD_PRINT_File_Array;
        private string[] A4_PRINT_File_Array;
        private int running_count = 0;

        public CRV_Generator(Main form1)
        {
            InitializeComponent();
            _parent = form1;
            string query = "update d_active set CAD_Print_processor_active = '1' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            query = "select CAD_Print_Processor_printer from d_active where employeenumber = '10577'";
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Read();
            printer_path = " " + reader[0].ToString().Trim();
            tick_update.Interval = 40000;
            tick_update.Enabled = true;
            tick_update.Tick += new EventHandler(check_folder);
            running = true;
            start_button.Visible = false;
            buffer_text.AppendText("===================================\r\n");
            buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
            buffer_text.AppendText("===================================\r\n");
            reader.Close();

        }

        private void check_folder(object sender, EventArgs e)
        {
            if (running)
            {
                CAD_PRINT_Retrieve_File_List("
 * 
 * 
 * LDATA\\PLOTS\\PLOT_PRINT\\");
                CAD_PRINT_Process_Files();
                A4_PRINT_Retrieve_File_List("\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\A4_PRINT\\");
                A4_PRINT_Process_Files();
            }
        }

        private void CAD_PRINT_Retrieve_File_List(string path)
        {
            CAD_PRINT_File_Array = Directory.GetFiles(path);
        }

        private void A4_PRINT_Retrieve_File_List(string path)
        {
            A4_PRINT_File_Array = Directory.GetFiles(path);
        }

        private void CAD_PRINT_Process_Files()
        {
            foreach (string file_path in CAD_PRINT_File_Array)
            {
                try
                {
                    string temp = file_path.Substring(26);
                    buffer_text.AppendText("[CAD PRINT] Processing file: " + temp + "\r\n");
                    running_count++;
                    try
                    {
                        string print_command = "/c copy " + "\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\" + temp.Substring(0, temp.Length - 4) + ".ps" + printer_path;
                        Console.WriteLine(print_command);

                        //CAM_PS_OPTRA";

                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.UseShellExecute = false;
                        startInfo.FileName = "cmd.exe";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = print_command;
                        try
                        {
                            using (Process exeProcess = Process.Start(startInfo))
                            {
                                exeProcess.WaitForExit();
                            }
                        }
                        catch { }
                        buffer_text.AppendText("     -> Printing file: " + file_path.Substring(26) + "\r\n");
                        buffer_text.AppendText("     -> Deleting file: " + file_path.Substring(26) + "\r\n");
                    }
                    catch { }
                    File.Delete(file_path);

                    // REPORT REMOVAL //_parent.Main_Update_Transition_Data("PROCESSOR_CAD_PRINT", (_parent.Get_Transition_Data_Value("PROCESSOR_CAD_PRINT") + 1).ToString(), true);
                    count_text.Text = "Files processed: " + running_count.ToString();
                }
                catch { }
            } 
        }

        private void A4_PRINT_Process_Files()
        {
            foreach (string file_path in A4_PRINT_File_Array)
            {
                try
                {
                    buffer_text.AppendText("[A4 PRINT] Processing file: " + file_path.Substring(24) + "\r\n");
                    Console.WriteLine(file_path.Substring(24));
                    running_count++;
                    try
                    {
                        string print_command = "/c copy " + file_path + printer_path;
                        Console.WriteLine(print_command);

                        //CAM_PS_OPTRA";

                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.UseShellExecute = false;
                        startInfo.FileName = "cmd.exe";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = print_command;
                        try
                        {
                            using (Process exeProcess = Process.Start(startInfo))
                            {
                                exeProcess.WaitForExit();
                            }
                        }
                        catch { }
                        buffer_text.AppendText("     -> Printing file: " + file_path.Substring(24) + "\r\n");
                        buffer_text.AppendText("     -> Copying file: " + file_path.Substring(24) + "\r\n");
                        buffer_text.AppendText("     -> Deleting file: " + file_path.Substring(24) + "\r\n");
                    }
                    catch { }
                    File.Delete("\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\" + file_path.Substring(24));
                    File.Copy(file_path, "\\\\10.0.0.8\\shopdata\\LDATA\\PLOTS\\" + file_path.Substring(24));
                    File.Delete(file_path);
                    // REPORT REMOVAL //_parent.Main_Update_Transition_Data("PROCESSOR_CAD_PRINT", (_parent.Get_Transition_Data_Value("PROCESSOR_CAD_PRINT") + 1).ToString(), true);
                    count_text.Text = "Files processed: " + running_count.ToString();
                    buffer_text.SelectionStart = buffer_text.Text.Length;
                    buffer_text.ScrollToCaret();
                }
                catch { }
            }
        }


        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            string query = "update d_active set CAD_Print_processor_active = '0' where employeenumber = '10577'";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            reader.Close();
            // REPORT REMOVAL //_parent.cad_print_done();
            this.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                try
                {
                    running = true;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread started: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = false;
                    stop_button.Visible = true;
                    pictureBox1.Enabled = true;

                    //string query = "update d_active set CAD_Print_processor_active = '1' where employeenumber = '10577'";
                    //ExcoODBC database = ExcoODBC.Instance;
                    //OdbcDataReader reader;
                    //database.Open(Database.DECADE_MARKHAM);
                    //reader = database.RunQuery(query);
                    //reader.Close();
                }
                catch { }
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (running)
            {
                try
                {
                    running = false;
                    buffer_text.AppendText("===================================\r\n");
                    buffer_text.AppendText("  Thread stopped at: " + DateTime.Now.ToString() + "\r\n");
                    buffer_text.AppendText("===================================\r\n");
                    start_button.Visible = true;
                    stop_button.Visible = false;
                    pictureBox1.Enabled = false;

                    //string query = "update d_active set CAD_Print_processor_active = '0' where employeenumber = '10577'";
                    //ExcoODBC database = ExcoODBC.Instance;
                    //OdbcDataReader reader;
                    //database.Open(Database.DECADE_MARKHAM);
                    //reader = database.RunQuery(query);
                    //reader.Close();
                }
                catch { }
            }
        }
    }
}



*/