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
using Databases;
using System.Data.Odbc;

namespace ExcoHoldViewer
{
    public partial class Form_Template : Form
    {
        bool paint = true;
        private List<Button> View_Hold = new List<Button>();
        private List<Button> History_Hold = new List<Button>();
        private List<Button> View_Released = new List<Button>();
        private List<Button> History_Released = new List<Button>();

        List<string> Temp_On_Hold = new List<string>();
        List<string> Temp_Released = new List<string>();

        private void dynamic_button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            List<string> ref_Hold = item_desc.Text.Length == 6 ? Temp_On_Hold : On_Hold_Jobs;
            List<string> ref_Release = item_desc.Text.Length == 6 ? Temp_Released : Released_Jobs;

            if (b.Name.StartsWith("ph"))
            {
                Print_Order(ref_Hold[Convert.ToInt32(b.Name.Substring(2)) - 1]);
            }
            if (b.Name.StartsWith("vh"))
            {
                View_Order(ref_Hold[Convert.ToInt32(b.Name.Substring(2)) - 1]);
            }
            if (b.Name.StartsWith("pr"))
            {
                Print_Order(ref_Release[Convert.ToInt32(b.Name.Substring(2)) - 1]);
            }
            if (b.Name.StartsWith("vr"))
            {
                View_Order(ref_Release[Convert.ToInt32(b.Name.Substring(2)) - 1]);
            }
            /*
            List<Expenses> Ref_Expense_List = (b.Name.StartsWith("a")) ? Recurring_Expenses_List : Depreciated_Expenses_List;

            parent.Expenses_List.Remove(Ref_Expense_List[Convert.ToInt32(b.Name.Substring(2))]);

            if (b.Name.StartsWith("ad")) // active --> inactive
            {
                Recurring_Expenses_List.RemoveAt(Convert.ToInt32(b.Name.Substring(2)));
            }
            else
            {
                Depreciated_Expenses_List.RemoveAt(Convert.ToInt32(b.Name.Substring(2)));
            }
            */
            this.Invalidate();
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color DrawForeColor = Color.White;
            Color BackColor = Color.FromArgb(64, 64, 64);
            Color HighlightColor = Color.FromArgb(76, 76, 76);

            SolidBrush WritingBrush = new SolidBrush(DrawForeColor);
            SolidBrush GreyBrush = new SolidBrush(Color.FromArgb(88, 88, 88));
            SolidBrush RedBrush = new SolidBrush(Color.LightPink);
            SolidBrush GreenBrush = new SolidBrush(Color.LightGreen);
            Pen p = new Pen(WritingBrush, 1);
            Pen Grey_Pen = new Pen(GreyBrush, 2);

            Font f_asterisk = new Font("MS Reference Sans Serif", 7, FontStyle.Regular);
            Font f = new Font("MS Reference Sans Serif", 12, FontStyle.Regular);
            Font f_strike = new Font("MS Reference Sans Serif", 9, FontStyle.Strikeout);
            Font f_total = new Font("MS Reference Sans Serif", 9, FontStyle.Bold);
            Font f_header = new Font("MS Reference Sans Serif", 13, FontStyle.Underline | FontStyle.Bold);
            Font f_title = new Font("MS Reference Sans Serif", 11, FontStyle.Bold);

            int data_height = 29; //19
            int start_height = Start_Size.Height - 10;
            int start_margin = 15;
            int height_offset = 0;
            //Information
            int margin1 = start_margin +50;       //Amount
            int margin2 = margin1 + 280;             //Frequency 
            int margin3 = margin2 + 90;             //% of income
            int margin4 = margin3 + 120;            //% of income

            int hold_row_count = 1;
            int release_row_count = 1;


            View_Hold.ForEach(button => button.Image.Dispose());
            History_Hold.ForEach(button => button.Image.Dispose());
            View_Released.ForEach(button => button.Image.Dispose());
            History_Released.ForEach(button => button.Image.Dispose());

            // Remove existing buttons
            View_Hold.ForEach(button => button.Dispose());
            History_Hold.ForEach(button => button.Dispose());
            View_Released.ForEach(button => button.Dispose());
            History_Released.ForEach(button => button.Dispose());

            View_Hold.ForEach(button => this.Controls.Remove(button));
            View_Hold = new List<Button>();
            History_Hold.ForEach(button => this.Controls.Remove(button));
            History_Hold = new List<Button>();
            View_Released.ForEach(button => this.Controls.Remove(button));
            View_Released = new List<Button>();
            History_Released.ForEach(button => this.Controls.Remove(button));
            History_Released = new List<Button>();
            
            // If has order

            if (paint)
            {
                e.Graphics.DrawString("Jobs On Hold", f_header, WritingBrush, margin1, start_height + height_offset);
                e.Graphics.DrawString("Jobs Released", f_header, WritingBrush, margin2, start_height + height_offset);
                height_offset += 20;

                Temp_On_Hold = new List<string>();
                Temp_Released = new List<string>();

                if (item_desc.Text.Length == 6) 
                {
                    string[] file_paths = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\On-hold\", "*" + item_desc.Text + "*.TXT");
                    if (file_paths.Count() > 0)
                    {
                        foreach (string g in file_paths)
                            Temp_On_Hold.Add(Path.GetFileName(g).Substring(0, 6));
                    }
                    else
                    {
                        file_paths = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released\", "*" + item_desc.Text + "*.TXT");
                        foreach (string g in file_paths)
                            Temp_Released.Add(Path.GetFileName(g).Substring(0, 6));
                    }
                }

                foreach (string orderNo in item_desc.Text.Length == 6 ? Temp_On_Hold : On_Hold_Jobs)
                {
                    ToolTip ToolTip1 = new ToolTip();
                    ToolTip1.InitialDelay = 100;
                    ToolTip1.ReshowDelay = 100;

                    Button view_hold_button = new Button();
                    view_hold_button.BackColor = this.BackColor;
                    view_hold_button.ForeColor = this.BackColor;
                    view_hold_button.FlatStyle = FlatStyle.Flat;
                    view_hold_button.Image = global::ExcoHoldViewer.Properties.Resources.eye;
                    view_hold_button.Size = new Size(29, 29);
                    view_hold_button.Location = new Point(margin1 + 75, start_height + height_offset + (hold_row_count * data_height) - 6);
                    view_hold_button.Name = "vh" + hold_row_count.ToString(); // vh = view hold, ph = print hold
                    view_hold_button.Text = "";
                    view_hold_button.Click += new EventHandler(this.dynamic_button_click);
                    View_Hold.Add(view_hold_button);
                    ToolTip1.SetToolTip(view_hold_button, "View " + orderNo);
                    this.Controls.Add(view_hold_button);

                    Button hold_history_button = new Button();
                    hold_history_button.BackColor = this.BackColor;
                    hold_history_button.ForeColor = this.BackColor;
                    hold_history_button.FlatStyle = FlatStyle.Flat;
                    hold_history_button.Image = global::ExcoHoldViewer.Properties.Resources.print;
                    hold_history_button.Size = new Size(29, 29);
                    hold_history_button.Location = new Point(margin1 + 110, start_height + height_offset + (hold_row_count * data_height) - 6);
                    hold_history_button.Name = "ph" + hold_row_count.ToString(); // vh = view hold, hh = print hold
                    hold_history_button.Text = "";
                    hold_history_button.Click += new EventHandler(this.dynamic_button_click);
                    History_Released.Add(hold_history_button);
                    ToolTip1.SetToolTip(hold_history_button, "Print " + orderNo);
                    this.Controls.Add(hold_history_button);


                    e.Graphics.DrawString(orderNo, f, WritingBrush, margin1, start_height + height_offset + (hold_row_count * data_height));
                    hold_row_count ++;
                }

                foreach (string orderNo in item_desc.Text.Length == 6 ? Temp_Released : Released_Jobs)
                {
                    ToolTip ToolTip1 = new ToolTip();
                    ToolTip1.InitialDelay = 100;
                    ToolTip1.ReshowDelay = 100;

                    Button view_hold_button = new Button();
                    view_hold_button.BackColor = this.BackColor;
                    view_hold_button.ForeColor = this.BackColor;
                    view_hold_button.FlatStyle = FlatStyle.Flat;
                    view_hold_button.Image = global::ExcoHoldViewer.Properties.Resources.eye;
                    view_hold_button.Size = new Size(29, 29);
                    view_hold_button.Location = new Point(margin2 + 75, start_height + height_offset + (release_row_count * data_height) - 6);
                    view_hold_button.Name = "vr" + release_row_count.ToString(); // vr= view released, pr = print release
                    view_hold_button.Text = "";
                    view_hold_button.Click += new EventHandler(this.dynamic_button_click);
                    View_Released.Add(view_hold_button);
                    ToolTip1.SetToolTip(view_hold_button, "View " + orderNo);
                    this.Controls.Add(view_hold_button);

                    Button hold_history_button = new Button();
                    hold_history_button.BackColor = this.BackColor;
                    hold_history_button.ForeColor = this.BackColor;
                    hold_history_button.FlatStyle = FlatStyle.Flat;
                    hold_history_button.Image = global::ExcoHoldViewer.Properties.Resources.print;
                    hold_history_button.Size = new Size(29, 29);
                    hold_history_button.Location = new Point(margin2 + 110, start_height + height_offset + (release_row_count * data_height) - 6);
                    hold_history_button.Name = "pr" + release_row_count.ToString(); // vr = view released, pr = print release
                    hold_history_button.Text = "";
                    hold_history_button.Click += new EventHandler(this.dynamic_button_click);
                    History_Hold.Add(hold_history_button);
                    ToolTip1.SetToolTip(hold_history_button, "Print " + orderNo);
                    this.Controls.Add(hold_history_button);

                    e.Graphics.DrawString(orderNo, f, WritingBrush, margin2, start_height + height_offset + (release_row_count * data_height));
                    release_row_count++;
                }

                height_offset += 20;
                this.Height = start_height + height_offset + Math.Max(hold_row_count, release_row_count) * data_height;
                e.Graphics.DrawLine(Grey_Pen, this.Width / 2, start_height, this.Width / 2, this.Height - 15);
            }
            else
            {
                this.Height = Start_Size.Height;
            }

            // Dispose all objects
            p.Dispose();
            Grey_Pen.Dispose();
            GreenBrush.Dispose();
            RedBrush.Dispose();
            GreyBrush.Dispose();
            WritingBrush.Dispose();
            f_asterisk.Dispose();
            f.Dispose();
            f_strike.Dispose();
            f_total.Dispose();
            f_header.Dispose();
            base.OnPaint(e);

            MouseInput.ScrollWheel(-1);
        }

        public List<string> On_Hold_Jobs = new List<string>();
        public List<string> Released_Jobs = new List<string>();

        Size Start_Size = new Size();

        public Form_Template()
        {
            //this.Location = new Point(_parent.Location.X + Start_Location_Offset, _parent.Location.Y + Start_Location_Offset - 15);
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Start_Size = this.Size;
            Set_Form_Color(this.ForeColor);
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            // Mousedown anywhere to drag
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);

            Refresh();
        }

        private void Refresh()
        {
            // Populate On Hold
            Populate_On_Hold();

            // Populate Released
            Populate_Released();

            Invalidate();
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        public void Set_Form_Color(Color randomColor)
        {
            //minimize_button.ForeColor = randomColor;
            //close_button.ForeColor = randomColor;
            textBox1.BackColor = randomColor;
            textBox2.BackColor = randomColor;
            textBox3.BackColor = randomColor;
            textBox4.BackColor = randomColor;
        }

        private void Populate_On_Hold()
        {
            On_Hold_Jobs = new List<string>();

            string[] file_paths = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\On-hold\", "*.TXT");

            foreach (string g in file_paths)
            {
                On_Hold_Jobs.Add(Path.GetFileName(g).Substring(0, 6));
            }
        }
        private void Populate_Released()
        {
            Released_Jobs = new List<string>();

            // Past two weeks only
            string[] file_paths = Directory.GetFiles(@"\\10.0.0.8\rdrive\OnHold\Released\", "*.TXT").Where(x => File.GetCreationTime(x) >= DateTime.Now.AddDays(-14)).ToArray();
            
            string lay_orders = "";

            foreach (string g in file_paths)
            {
                string temp = Path.GetFileName(g).Substring(0, 6);
                if (!Released_Jobs.Contains(temp))
                {
                    Released_Jobs.Add(temp);
                    lay_orders += (lay_orders.Length > 0 ? " or " : "") + "ordernumber = '" + temp + "'";
                }
            }

            
            Released_Jobs = new List<string>();

            if (lay_orders.Length > 10)
            {
                string query = "select ordernumber from d_order where (" + lay_orders + ") and (invoicedate is null and shipdate is null)";
                ExcoODBC instance = ExcoODBC.Instance;
                instance.Open(Database.DECADE_MARKHAM);
                OdbcDataReader reader = instance.RunQuery(query);
                while (reader.Read())
                {
                    Released_Jobs.Add(reader[0].ToString().Trim());
                }
            }

        }

        private void View_Order(string Order_Number)
        {
            string Config_Path = "\\\\10.0.0.8\\rdrive\\onhold\\Exco Hold and Release.exe";
            var proc = System.Diagnostics.Process.Start(Config_Path, "SALES " + Order_Number + " 99999 -DE");
        }

        private void Print_Order(string Order_Number)
        {
            string Config_Path = "\\\\10.0.0.8\\rdrive\\onhold\\Exco Hold and Release.exe";
            var proc = System.Diagnostics.Process.Start(Config_Path, "ADMIN " + Order_Number + " 99999 1");
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            item_desc.Text = "";
            Refresh();
        }

        private void item_desc_TextChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
