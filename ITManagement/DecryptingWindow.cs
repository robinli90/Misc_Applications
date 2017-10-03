using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ITManagement
{
    public partial class DecryptingWindow : Form
    {
        Encrypter en = new Encrypter();
        string file_path = "";

        public DecryptingWindow(string key)
        {
            if (key == "")
            {
                en.ENCRYPTION_KEY = "32487239847";
            }
            else if (key == "@")
            {
                en.ENCRYPTION_KEY = "11111111111111111";
            }
            else
            {
                en.ENCRYPTION_KEY = key;
            }
            InitializeComponent();
        }

        private void DecryptingWindow_Load(object sender, EventArgs e)
        {

        }

        private void Decrypt_Button_Click(object sender, EventArgs e)
        {
            string replacement = en.Decrypt(dec_Box.Text);
            Console.WriteLine(replacement);
            dec_Box.Text = replacement;
        }

        private void dec_file_Click(object sender, EventArgs e)
        {
            List<string> text2 = new List<string>();
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Decrypt File";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                file_path = file.FileName;
            }
            var text = File.ReadAllText(file_path);
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            //if (!(lines[0] == "%")) G_Code_List.Add("");
            foreach (string line in lines)
            {
                text2.Add(en.Decrypt(line));
            }
            File.WriteAllLines(file_path.Substring(file_path.Length - 4) + "_temp.txt", text2);
            Process.Start(file_path.Substring(file_path.Length - 4) + "_temp.txt");
        }
    }
}
