using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Translator
{
    public partial class translate_line : Form
    {
        Dictionary<string, List<List<string>>> _Master_Rules_List = new Dictionary<string, List<List<string>>>();
        List<List<string>> _COPY_ALGORITHM = new List<List<string>>();
        Translator _parent;
        private string orig_name = "";
        private bool _rename = false;

        public translate_line(Translator parent, Dictionary<string, List<List<string>>> Master_Rules_List, List<List<string>> _child = null, string _PRESET_NAME="", string _orig_name="")
        {
            InitializeComponent();
            _Master_Rules_List = Master_Rules_List;
            _parent = parent;
            if (_orig_name.Length == 0)
            {
                if (!(_child == null)) // if copy command
                {
                    _COPY_ALGORITHM = _child;
                    name.Text = _PRESET_NAME + " - Copy";
                    richTextBox1.Text = "Name of Copy:";
                    button1.Text = "Add Copy";
                }
            }
            else
            {
                orig_name = _orig_name;
                _rename = true;
                _COPY_ALGORITHM = _child;
                name.Text = _orig_name;
                richTextBox1.Text = "New name:";
                button1.Text = "Rename";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<string>> List;
            if ((_Master_Rules_List.TryGetValue(name.Text, out List)))
            {
                error.Visible = true;
            }
            else if (name.Text.Length < 1)
            {
                error2.Visible = true;
            }
            else
            {
                if (_rename)
                {
                    _parent.Delete_Translator_File(orig_name);
                }
                _parent.Create_New_Translator_File(name.Text, _COPY_ALGORITHM);
                this.Close();
                this.Dispose();
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            error.Visible = false;
            error2.Visible = false;
        }
    }
}
