using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class ReleaseFiles : Form
    {
        private int release_count = 0;

        public ReleaseFiles()
        {
            InitializeComponent();
        }

        private string[] delete_directories = { "\\\\10.0.0.8\\shopdata\\LATHE2\\JOBS\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\DRAMET\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\EDM\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\GAUGE\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\LATHEFIN\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\MILL\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\SPARKY\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\LATHE\\",
                                                "\\\\10.0.0.8\\shopdata\\CURJOBS\\TOOLLISTS\\",
                                                "\\\\10.0.0.8\\shopdata\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\LDATA\\TURN\\",
                                                "\\\\10.0.0.8\\rdrive\\TOOLLISTS\\",
                                              };

        private string[] repository_directories = { "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\LATHE2\\JOBS\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\DRAMET\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\EDM\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\GAUGE\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\LATHEFIN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\MILL\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\SPARKY\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\LATHE\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\CURJOBS\\TOOLLISTS\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\LDATA\\TURN\\",
                                                "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\TOOLLISTS\\"
                                              };

        public void GET_DIRECTORIES()
        {
            return;
        }


        private string delete_from_directories(string ordernumber)
        {
            string files = "";
            string[] File_in_dir;
            foreach (string dir_path in repository_directories)
            {
                try
                {
                    File_in_dir = Directory.GetFiles(dir_path, "*" + ordernumber + "*");
                    foreach (string file in File_in_dir)
                    {
                        string file_name = Path.GetFileName(file);
                        string file_path = Path.GetFullPath(file).Substring(0, Path.GetFullPath(file).Length - file_name.Length - 1);
                        files = files + " " + file_name;
                        //buffer_text.AppendText((file_name, reverse));
                        try
                        {
                            if (File.Exists(delete_directories[_GET_DIRECTORY_INDEX(file)] + file_name))
                            {

                                DialogResult dialogResult = MessageBox.Show("There is an existing file in " + delete_directories[_GET_DIRECTORY_INDEX(file)] + file_name + ". Do you wish to overwrite this file?", "", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    File.Copy(file, delete_directories[_GET_DIRECTORY_INDEX(file)] + file_name);
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    //do something else
                                }
                            }
                            else
                            {
                                File.Copy(file, delete_directories[_GET_DIRECTORY_INDEX(file)] + file_name);
                            }
                        }
                        catch
                        {
                            // No file return
                        }
                        //File.Copy(file, "\\\\10.0.0.8\\shopdata\\ONHOLD_REPOSITORY\\" + Path.GetFileName(file));
                        File.Delete(file);
                        release_count++;
                    }
                }
                catch (Exception e) { Console.WriteLine(e);Console.WriteLine(e); }
            }
            return files;
        }


        private int _GET_DIRECTORY_INDEX(string path)
        {
            if (path.Contains("LATHE2") && path.Contains("JOBS"))
            {
                return 400; //removed from return
            }
            else if (path.Contains("DRAMET"))
            {
                return 1;
            }
            else if (path.Contains("EDM"))
            {
                return 2;
            }
            else if (path.Contains("GAUGE"))

            {
                return 3;
            }
            else if (path.Contains("LATHEFIN"))
            {
                return 4; //removed from return
            }
            else if (path.Contains("MILL"))
            {
                return 5;
            }
            else if (path.Contains("SPARKY"))
            {
                return 6;
            }
            else if (path.Contains("LATHE") && path.Contains("CURJOBS") && !path.Contains("LATHEFIN"))
            {
                return 400; //removed from return
            }
            else if (path.Contains("CURJOBS") && path.Contains("TOOLLISTS"))
            {
                return 8;
            }
            else if (path.Contains("TURN") && !path.Contains("LDATA"))
            {
                return 400; //removed from return
            }
            else if (path.Contains("TURN"))
            {
                return 400; //removed from return
            }
            else if (path.Contains("TOOLLISTS"))
            {
                return 11;
            }
            else
            {
                return 100;
            }
        }

        //Release button
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length >= 5)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you wish to release files for job SO# " + textBox2.Text, "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    release_count = 0;
                    delete_from_directories(textBox2.Text);
                    MessageBox.Show(release_count.ToString() + " file(s) released");
                    this.Close();
                    this.Dispose();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                MessageBox.Show("Invalid shop order number");
            }
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                bool has_decimal = false;
                if (textBox2.Text.Substring(0, textBox2.Text.Length - 1).Contains(".")) has_decimal = true;
                if (!char.IsDigit(textBox2.Text[textBox2.Text.Length - 1]) && !(textBox2.Text[textBox2.Text.Length - 1].ToString() == "."))
                //if (!((textBox2.Text.Substring(textBox2.Text.Length-1, textBox2.Text.Length)) == "a"))
                {
                    // If letter in SO_number box, do not output and move CARET to end
                    textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.SelectionLength = 0;
                }
                if (textBox2.Text[textBox2.Text.Length - 1].ToString() == "." && has_decimal)
                {
                    textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.SelectionLength = 0;
                }
            }
        }

        

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
