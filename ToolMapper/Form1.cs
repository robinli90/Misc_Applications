using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databases;

namespace ToolMapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Translate File";
            file.Multiselect = false;
            string file_path = "";
            if (file.ShowDialog() == DialogResult.OK)
            {
                file_path = file.FileName;
                string file_name_loaded = Path.GetFileName(file_path);

            }

            if (file_path.Length > 0)
            {
                var text = File.ReadAllText(file_path);
                string[] lines = text.Split(new string[] {Environment.NewLine}, StringSplitOptions.None);

                foreach (string g in lines)
                {
                    if (g.Contains(",") && !g.StartsWith("#"))
                    {
                        string[] lines2 = g.Split(new string[] {","}, StringSplitOptions.None);
                        string ugName = lines2[0].Trim();
                        string toolListDesc = lines2[1].Trim();
                        string vericut = lines2[2].Trim();

                        string query = String.Format("select top 1 * from decade.dbo.toolmap1 where toolname = '{0}'", ugName);
                        ExcoODBC database = ExcoODBC.Instance;
                        OdbcDataReader reader;
                        database.Open(Database.DECADE_MARKHAM);
                        reader = database.RunQuery(query);
                        bool toolExist = false;
                        while (reader.Read())
                        {
                            toolExist = true;
                            break;
                            //Console.WriteLine("Tool exists for: " + ugName);
                        }
                        reader.Close();
                        if (!toolExist)
                        {
                            // get latest toolID
                            int ID = 0;
                            query = String.Format("select top 1 * from decade.dbo.toolmap1 order by toolid desc");
                            database.Open(Database.DECADE_MARKHAM);
                            reader = database.RunQuery(query);
                            while (reader.Read())
                            {
                                ID = Convert.ToInt32(reader[0].ToString().Trim()) + 1;
                            }
                            reader.Close();

                            query = 
                                "insert into decade.dbo.toolmap1 values (" + ID + ", '" + ugName +
                                "', '" + ugName +
                                "', '" + vericut +
                                "', '" + toolListDesc +
                                "', '', 0, null)";
                            database.Open(Database.DECADE_MARKHAM);
                            database.RunQuery(query);
                            reader.Close();

                            Console.WriteLine("NEW*** Created..........TOOLID = " + ID + ", Name = " + ugName);
                        }
                        else
                        {
                            query = String.Format(
                                "update decade.dbo.toolmap1 set michigantoolname = '{0}' where toolname = '{1}'",
                                toolListDesc, ugName);
                            database.Open(Database.DECADE_MARKHAM);
                            database.RunQuery(query);
                            reader.Close();

                            Console.WriteLine("Updated.........TOOLID = " + ugName);
                        }
                    }
                }
            }*/

            // copy empty michigans from markam

            Dictionary<string, string> toolNameListWithoutMichiganValues = new Dictionary<string, string>();

            string query = String.Format("select * from decade.dbo.toolmap1");
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            bool toolExist = false;
            while (reader.Read())
            {
                if (reader[4].ToString().Trim().Length < 2)
                {
                    toolNameListWithoutMichiganValues.Add(reader[0].ToString().Trim(), reader[2].ToString().Trim());
                }
            }
            reader.Close();

            foreach (KeyValuePair<string, string> g in toolNameListWithoutMichiganValues)
            {
                query = String.Format("update decade.dbo.toolmap1 set michigantoolname = '{0}' where toolid = '{1}'",
                    g.Value, g.Key);

                database.Open(Database.DECADE_MARKHAM);
                reader = database.RunQuery(query);
                reader.Close();
            }
        }
    }
}
