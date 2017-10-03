using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Mset.Extensions;
using System.IO;
using System.Data.SQLite;
using Dapper;
using Transitions;
using System.Drawing.Imaging;
using ExpanderApp;

namespace Test
{
    public partial class Form1 : Form
    {

        private List<HarrProgressBar> _items = new List<HarrProgressBar>();
        private Dictionary<string, List<Button>> Button_Dictionary = new Dictionary<string, List<Button>>();
        private Point MouseDownLocation;

        FadeControl TFLP;

        FlowLayoutPanel REF;

        internal int count = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            //flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.Visible = true;


            this.flowLayoutPanel1.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel1.DragDrop += new DragEventHandler(drag_Fade_In);
            this.flowLayoutPanel2.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel2.DragDrop += new DragEventHandler(drag_Fade_In);
            this.flowLayoutPanel3.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel3.DragDrop += new DragEventHandler(drag_Fade_In);
            this.flowLayoutPanel4.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel4.DragDrop += new DragEventHandler(drag_Fade_In);
            this.flowLayoutPanel5.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel5.DragDrop += new DragEventHandler(drag_Fade_In);
            this.flowLayoutPanel6.DragEnter += new DragEventHandler(drag_Fade_Out);
            this.flowLayoutPanel6.DragDrop += new DragEventHandler(drag_Fade_In);

            flowLayoutPanel6.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            flowLayoutPanel1.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            flowLayoutPanel2.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            flowLayoutPanel3.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            flowLayoutPanel4.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            flowLayoutPanel5.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            //this.flowLayoutPanel2.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //this.flowLayoutPanel2.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            //this.flowLayoutPanel3.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //this.flowLayoutPanel3.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            //this.flowLayoutPanel4.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //this.flowLayoutPanel4.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            //this.flowLayoutPanel6.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //this.flowLayoutPanel6.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            //this.flowLayoutPanel5.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //this.flowLayoutPanel5.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);

            REF = flowLayoutPanel1;
            this.flowLayoutPanel1.DoubleClick += new EventHandler(Double_Click);
            this.flowLayoutPanel3.DoubleClick += new EventHandler(Double_Click);
            this.flowLayoutPanel2.DoubleClick += new EventHandler(Double_Click);
            this.flowLayoutPanel4.DoubleClick += new EventHandler(Double_Click);
            this.flowLayoutPanel5.DoubleClick += new EventHandler(Double_Click);
            this.flowLayoutPanel6.DoubleClick += new EventHandler(Double_Click);

        }

        void flowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            var t = new Transition(new TransitionType_EaseInEaseOut(13000));
            //t.add(garbagePanel, "BackColor", Color.FromArgb(0, 255, 255, 255));
            //garbagePanel.Visible = true;
            //FadeIn(TFLP, 100);
            FadeOut(TFLP, 1);
            //t.add(TFLP, "Opacity", 0); 
            //garbagePanel
        }


        // Get Mouse location of mousedown on form
        private void buttontest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }


        private void buttontest_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(flowLayoutPanel1.Size.Height + " " + flowLayoutPanel1.Size.Width);
        }

        // Main moving function
        private void buttontest_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                btn.Left = e.X + btn.Left - MouseDownLocation.X;
                btn.Top = e.Y + btn.Top - MouseDownLocation.Y;
                btn.Text = (e.X + btn.Left - MouseDownLocation.X).ToString() + ", " + (e.Y + btn.Top - MouseDownLocation.Y).ToString();
            }
        }

        private void FadeIn_MouseUp(object sender, MouseEventArgs e)
        {
            FadeIn(TFLP, 1);
            //Invalidate();
        }


        private async void FadeIn(FadeControl o, int interval = 80)
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            //Object is not fully invisible. Fade it in
            while (o.Opacity < 99)
            {
                await Task.Delay(interval);
                o.Opacity += 3;
                //o.Opacity += 0.03;
            }
            o.Opacity = 100; //make fully visible    
        }

        private async void FadeOut(FadeControl o, int interval = 80)
        {

            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            //Object is fully visible. Fade it out
            while (o.Opacity > 1)
            {
                await Task.Delay(interval);
                o.Opacity -= 3;
            }
            o.Opacity = 0; //make fully invisible
        }

        private void drag_Fade_In(object sender, DragEventArgs e)
        {
            FadeIn(TFLP, 1);
        }
        private void drag_Fade_Out(object sender, DragEventArgs e)
        {
            FadeOut(TFLP, 1);
        }

        private void flowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            //HarrProgressBar data = (HarrProgressBar)e.Data.GetData(typeof(HarrProgressBar));
            PB data = (PB)e.Data.GetData(typeof(PB));
            FlowLayoutPanel _destination = (FlowLayoutPanel)sender;
            FlowLayoutPanel _source = (FlowLayoutPanel)data.Parent;

            Point p1 = _destination.PointToClient(new Point(e.X, e.Y));

            if (p1.Y > 0)
            {
                if (_destination == garbagePanel || _destination == TFLP)
                {
                    data.Dispose();
                    ExpanderApp.Expander ex = new ExpanderApp.Expander();
                    ex.Expand(); ex = new ExpanderApp.Expander();
                    ex = (Expander)_source.Parent;
                    ex.Collapse();
                    ex.Expand();

                    if (_source.Controls.Count <= 0)
                    {
                        if (_source.Parent.Controls.Count <= 2)
                        {
                            _source.Parent.Dispose();
                        }
                        else
                        {
                            _source.Dispose();
                        }
                    }
                    _destination.Invalidate();
                    _source.Invalidate();
                }
                else if (_source != _destination)
                {
                    // Add control to panel
                    _destination.Controls.Add(data);
                    //_destination.Width = _destination.Width - 8;
                    data.Size = new Size(_destination.Width - 2, data.Height);

                    // Reorder
                    Point p = _destination.PointToClient(new Point(e.X, e.Y));
                    var item = _destination.GetChildAtPoint(p);
                    int index = _destination.Controls.GetChildIndex(item, false);
                    _destination.Controls.SetChildIndex(data, index);

                    // Invalidate to paint!
                    if (_source.Controls.Count <= 0)
                    {
                        if (_source.Parent.Controls.Count <= 2)
                        {
                            _source.Parent.Dispose();
                        }
                        else
                        {
                            _source.Dispose();
                        }
                    }
                    _source.Invalidate();
                    Console.WriteLine(_destination.Parent.ToString());
                    ExpanderApp.Expander ex = new ExpanderApp.Expander();
                    ex = (Expander)_destination.Parent;
                    ex.Collapse();

                    ex.Expand(); ex = new ExpanderApp.Expander();
                    ex = (Expander)_source.Parent;
                    ex.Collapse();
                    ex.Expand();
                    _destination.Invalidate();
                    _source.Invalidate();
                }
                else
                {
                    // Just add the control to the new panel.
                    // No need to remove from the other panel,
                    // this changes the Control.Parent property.
                    Point p = _destination.PointToClient(new Point(e.X, e.Y));
                    var item = _destination.GetChildAtPoint(p);
                    int index = _destination.Controls.GetChildIndex(item, false);
                    _destination.Controls.SetChildIndex(data, index);


                    ExpanderApp.Expander ex = new ExpanderApp.Expander();
                    ex.Expand(); ex = new ExpanderApp.Expander();
                    ex = (Expander)_source.Parent;
                    ex.Collapse();
                    ex.Expand();
                    _destination.Invalidate();
                }
                FadeIn(TFLP, 1);
            }
            FadeIn(TFLP, 1);
            //garbagePanel.Visible = false;
        }

        private void Double_Click(object sender, EventArgs e)
        {
            Button b = new Button();
            //DependencyObject obj = (DependencyObject)e.OriginalSource;
            FlowLayoutPanel data = (FlowLayoutPanel)sender;
            REF = data;
            //HarrProgressBar data = (HarrProgressBar)sender;
            //MessageBox.Show(data.MainText);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            garbagePanel.SendToBack();
            TFLP = new FadeControl();
            TFLP.Size = garbagePanel.Size;
            TFLP.Location = garbagePanel.Location;
            TFLP.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            TFLP.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            TFLP.Visible = true;
            //TFLP.Opacity = 20;
            TFLP.BackColor = System.Drawing.SystemColors.Control;

            TFLP.AllowDrop = true;
            //TFLP.AutoScroll = true;
            //TFLP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            
            //TFLP.BackgroundImage = global::Test.Properties.Resources.qcBXKrARi;
            //TFLP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            //TFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            TFLP.BringToFront();
            this.Controls.Add(TFLP);
            TFLP.BringToFront();

           // garbagePanel.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            //garbagePanel.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);

            this.MouseUp += new MouseEventHandler(FadeIn_MouseUp);

            button2.BringToFront();
            button3.BringToFront();
            button4.BringToFront();
            /*
        
            Random r = new Random();
            Size s = new Size(flowLayoutPanel1.Width - 8, 47);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < 10; i++)
            {
                PB PBA = new PB();
                PBA.ReadOnly = true;
                PBA.AllowDrag = true;
                PBA.RowHeadersVisible = false;
                PBA.Columns.Add("test", "  " + (r.Next(300000, 400000)).ToString());//, "Foo Text");
                //PBA.Rows[0].HeaderCell.Value = "Row Text";
                PBA.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //PBA.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //PBA.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //PBA.AutoSize = true;
                PBA.DefaultCellStyle.SelectionBackColor = Color.White;
                PBA.DefaultCellStyle.ForeColor = Color.Black;
                PBA.Dock = DockStyle.Top;
                PBA.Size = s;
                //PBA.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PBA.Columns["test"].DefaultCellStyle = dataGridViewCellStyle2;
                PBA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
                this.flowLayoutPanel1.Controls.Add(PBA);
            }

            Size s = new Size(flowLayoutPanel1.Width, 50);
            HarrProgressBar pgb;

            pgb = new HarrProgressBar();
            pgb.Padding = new Padding(5);
            pgb.LeftText = "1";
            pgb.MainText = "47x100x5400 - 20/100";
            pgb.FillDegree = 20;
            pgb.RightText = "1";
            pgb.StatusText = "Raw";
            pgb.StatusBarColor = 0;
            pgb.Size = s;
            pgb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pgb.MouseDoubleClick += new MouseEventHandler(Double_Click);

            this._items.Add(pgb);
            this.flowLayoutPanel1.Controls.Add(pgb);

            pgb = new HarrProgressBar();
            pgb.Padding = new Padding(5);
            pgb.LeftText = "2";
            pgb.MainText = "32x150x3300 - 40/100";
            pgb.FillDegree = 40;
            pgb.RightText = "2";
            pgb.StatusText = "Raw inactive";
            pgb.StatusBarColor = 1;
            pgb.Size = s;
            pgb.MouseDoubleClick += new MouseEventHandler(Double_Click);
            this._items.Add(pgb);
            this.flowLayoutPanel1.Controls.Add(pgb);

            pgb = new HarrProgressBar();
            pgb.Padding = new Padding(5);
            pgb.LeftText = "3";
            pgb.MainText = "22x100x2700 - 85/100";
            pgb.FillDegree = 85;
            pgb.RightText = "3";
            pgb.StatusText = "Dry";
            pgb.StatusBarColor = 2;
            pgb.Size = s;
            pgb.MouseDoubleClick += new MouseEventHandler(Double_Click);
            this._items.Add(pgb);
            this.flowLayoutPanel1.Controls.Add(pgb);

            pgb = new HarrProgressBar();
            pgb.Padding = new Padding(5);
            pgb.LeftText = "4";
            pgb.MainText = "47x200x4700 - 95/100";
            pgb.FillDegree = 95;
            pgb.RightText = "4";
            pgb.StatusText = "Dry inactive";
            pgb.StatusBarColor = 3;
            pgb.Size = s;
            pgb.MouseDoubleClick += new MouseEventHandler(Double_Click);
            this._items.Add(pgb);
            this.flowLayoutPanel2.Controls.Add(pgb);

            pgb = new HarrProgressBar();
            pgb.Padding = new Padding(5);
            pgb.LeftText = "5";
            pgb.MainText = "47x200x4700 - 100/100";
            pgb.FillDegree = 100;
            pgb.RightText = "5";
            pgb.Size = s;
            pgb.MouseDoubleClick += new MouseEventHandler(Double_Click);
            this._items.Add(pgb);
            this.flowLayoutPanel2.Controls.Add(pgb);
             * */
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Random randomGen = new Random();

            Expander expander = new Expander();
            //expander.AutoSize = true;
            expander.MinimumSize = new Size(flowLayoutPanel1.Width - 8, expander.Height / 5 - 3);
            expander.MaximumSize = new Size(flowLayoutPanel1.Width - 8, 999);
            expander.Size = expander.MinimumSize;
            expander.Padding = new Padding(0);
            //expander.Margin = new Padding(0);
            expander.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander, "       TRUCK #" + randomGen.Next(0, 100), SystemColors.ActiveBorder, Color.Black);

            FlowLayoutPanel FLP = new FlowLayoutPanel();
            FLP.AllowDrop = true;
            FLP.DragEnter += new DragEventHandler(flowLayoutPanel_DragEnter);
            FLP.DragDrop += new DragEventHandler(flowLayoutPanel_DragDrop);
            FLP.BorderStyle = BorderStyle.FixedSingle;
            FLP.FlowDirection = FlowDirection.TopDown;
            //FLP.ForeColor = Color.Green;
            FLP.DoubleClick += new EventHandler(Double_Click);



            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            float brightness = randomColor.GetBrightness();
            float midnightBlueBrightness = Color.FromKnownColor(KnownColor.MidnightBlue).GetBrightness();
            float transparentBrightness = Color.FromKnownColor(KnownColor.Transparent).GetBrightness();

            while (brightness < midnightBlueBrightness || brightness > transparentBrightness)
            {
                names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
                randomColorName = names[randomGen.Next(names.Length)];
                randomColor = Color.FromKnownColor(randomColorName);
                brightness = randomColor.GetBrightness();
            }

            FLP.BackColor = randomColor;
            FLP.AutoSize = true;
            FLP.MinimumSize = new Size(flowLayoutPanel1.Width - 10, 25);
            FLP.Size = FLP.MinimumSize;
            FLP.MaximumSize = new Size(flowLayoutPanel1.Width - 10, 999 );

            /*
            Label b = new Label();
            b.BorderStyle = BorderStyle.None;
            b.BackColor = randomColor;
            b.Font = new Font("Verdana", 10f, FontStyle.Bold | FontStyle.Underline);

            b.Text = "Truck #" + randomGen.Next(0, 100);
            b.Location = new Point(b.Left + 562, b.Location.Y);
            FLP.Controls.Add(b);
             * */
            FLP.Padding = new Padding(0);

            expander.Content = FLP;
            //expander.Collapse();

            REF.Controls.Add(expander);
        }

        void b_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonMouseDown(object sender, EventArgs e)
        {
            FadeOut(TFLP, 1);
        }
        private void ButtonMouseDown2(object sender, EventArgs e)
        {
            FadeOut(TFLP, 1);
        }

        private void ButtonMouseUp(object sender, EventArgs e)
        {
            PB data = (PB)sender;
            if (data._isDragging)
            {
                FadeIn(TFLP, 1);
            }
            else
            {
                FadeOut(TFLP, 1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Random r = new Random();
            Size s = new Size(REF.Width - 2, 26);

            PB PBA = new PB();
            PBA.AllowDrag = true;
            PBA.Size = s;
            PBA.Text = "" + (r.Next(300000, 400000)).ToString();
            PBA.FlatStyle = FlatStyle.Flat;
            PBA.Margin = new Padding(0);
            PBA.Padding = new Padding(0);
            PBA.MouseUp += new MouseEventHandler(FadeIn_MouseUp);
            REF.Controls.Add(PBA);

            /* ADD BUTTON

            Random r = new Random();
            Size s = new Size(REF.Width - 8, 47);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            PB PBA = new PB();
            PBA.ReadOnly = true;
            PBA.AllowDrag = true;
            PBA.RowHeadersVisible = false;


            PBA.Columns.Add("test", "  " + (r.Next(300000, 400000)).ToString());//, "Foo Text");
            //PBA.Rows[0].HeaderCell.Value = "Row Text";
            PBA.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //PBA.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //PBA.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //PBA.AutoSize = true;
            PBA.DefaultCellStyle.SelectionBackColor = Color.White;
            PBA.DefaultCellStyle.ForeColor = Color.Black;
            PBA.Dock = DockStyle.Top;
            PBA.Size = s;
            //PBA.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PBA.Columns["test"].DefaultCellStyle = dataGridViewCellStyle2;
            PBA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            REF.Controls.Add(PBA);
             * 
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            REF.Dispose();
        }
    }


    public partial class PB : Button //DataGridView
    {
        public bool _isDragging = false;
        private int _DDradius = 40;
        public bool AllowDrag { get; set; }
        private int _mX = 0;
        private int _mY = 0;


        protected override void OnGotFocus(EventArgs e)
        {
            this.BackColor = Color.SandyBrown;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.BackColor = Color.Transparent;
            base.OnLostFocus(e);
        }

        protected override void OnClick(EventArgs e)
        {
            this.Focus();
            base.OnClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            this._isDragging = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isDragging)
            {
                
                // This is a check to see if the mouse is moving while pressed.
                // Without this, the DragDrop is fired directly when the control is clicked, now you have to drag a few pixels first.
                if (e.Button == MouseButtons.Left && _DDradius > 0 && this.AllowDrag)
                {
                    int num1 = _mX - e.X;
                    int num2 = _mY - e.Y;
                    if (((num1 * num1) + (num2 * num2)) > _DDradius)
                    {
                        DoDragDrop(this, DragDropEffects.All);
                        _isDragging = true;
                        return;
                    }
                }
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isDragging = false;
            base.OnMouseUp(e);
        }
    }

    /*
public partial class TransparentFLP : Panel
{

    private const int WS_EX_TRANSPARENT = 0x20;
    public TransparentFLP()
    {
        SetStyle(ControlStyles.Opaque, true);
    }

    private int opacity = 50;

    [DefaultValue(50)]

    public int Opacity
    {
        get
        {
            return this.opacity;
        }
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("value must be between 0 and 100");
            this.opacity = value;
        }
    }
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
            return cp;
        }
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
        {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
        base.OnPaint(e);
    }   
}
    public bool drag = false;
    public bool enab = false;
    private int m_opacity = 100;

    private int alpha;

    public TransparentFLP()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.Opaque, true);
        //this.BackColor = Color.Transparent;
    }

    public int Opacity
    {
        get
        {
            if (m_opacity > 100)
            {
                m_opacity = 100;
            }
            else if (m_opacity < 1)
            {
                m_opacity = 1;
            }
            return this.m_opacity;
        }
        set
        {
            this.m_opacity = value;
            if (this.Parent != null)
            {
                Parent.Invalidate(this.Bounds, true);
            }
        }
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle = cp.ExStyle | 0x20;
            return cp;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

        Color frmColor = this.Parent.BackColor;
        Brush bckColor = default(Brush);

        alpha = (m_opacity * 255) / 100;

        if (drag)
        {
            Color dragBckColor = default(Color);

            if (BackColor != Color.Transparent)
            {
                int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                dragBckColor = Color.FromArgb(Rb, Gb, Bb);
            }
            else
            {
                dragBckColor = frmColor;
            }

            alpha = 255;
            bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
        }
        else
        {
            bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
        }

        if (this.BackColor != Color.Transparent | drag)
        {
            g.FillRectangle(bckColor, bounds);
        }

        bckColor.Dispose();
        g.Dispose();
        base.OnPaint(e);
    }

    protected override void OnBackColorChanged(EventArgs e)
    {
        if (this.Parent != null)
        {
            Parent.Invalidate(this.Bounds, true);
        }
        base.OnBackColorChanged(e);
    }

    protected override void OnParentBackColorChanged(EventArgs e)
    {
        this.Invalidate();
        base.OnParentBackColorChanged(e);
    }
}
    
*/
}


/*
 * 
 * 
        #region SQL DAPPER LITE TEST

        SqlConnection myConnection = new SqlConnection(("user id={0};" +
                                   "password={1};Data Source=np:{2};" +
                                   "Trusted_Connection=yes;" +
                                   "database={3}; " +
                                   "connection timeout=30").FormatWith("jamie", "jamie", "10.0.0.8", "decade"));

        string g = "".FormatWith("test");

        private void button1_Click(object sender, EventArgs e)
        {
            SqLiteBaseRepository sql = new SqLiteBaseRepository();

            for (int i = 0; i < 99999; i++)
            {
                Customer g = new Customer();
                g.FirstName = (88+2*i).ToString();
                g.LastName = "Test2";
                g.DateOfBirth = DateTime.Now;
                sql.SaveCustomer(g);
            }
             
            try
            {

                Console.WriteLine(sql.GetCustomer(50).FirstName);
            }
            catch (Exception ex2)
            {
                Console.WriteLine("Exception:" + ex2);
            }
            count++;
        }

        public void Test_Query()
        {

            try
            {
                string query = "select top 1 *  from d_it_budget";
                myConnection.Open();
                using (SqlCommand g = new SqlCommand(query, myConnection))
                {
                    SqlDataReader r = null;
                    r = g.ExecuteReader();
                    while (r.Read())
                    {
                        Console.WriteLine(r[0].ToString());
                        Console.WriteLine(r[1].ToString());
                        Console.WriteLine(r[2].ToString());
                        Console.WriteLine(r[3].ToString());
                    }
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }

    //========================================================================================================================
    //============================================== DAPPER TEST FUNCTION/CLASS ==============================================
    //========================================================================================================================

    public interface ICustomerRepository
    {
        Customer GetCustomer(long id);
        void SaveCustomer(Customer customer);
    }

    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\SimpleDb.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public void SaveCustomer(Customer customer)
        {
            if (!(File.Exists(DbFile)))
            {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                customer.Id = cnn.Query<int>(
                    @"INSERT INTO Customer 
                    ( FirstName, LastName, DateOfBirth ) VALUES 
                    ( @FirstName, @LastName, @DateOfBirth );
                    select last_insert_rowid()", customer).First();
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table Customer
                      (
                         ID                                  integer primary key AUTOINCREMENT,
                         FirstName                           varchar(100) not null,
                         LastName                            varchar(100) not null,
                         DateOfBirth                         datetime not null
                      )");
            }
        }

        public Customer GetCustomer(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                
                Customer result = cnn.Query<Customer>(
                    @"SELECT Id, FirstName, LastName, DateOfBirth
                    FROM Customer
                    WHERE Id = @id;", new { id }).FirstOrDefault();
                 
                //Customer result = cnn.Query<Customer>("Select * from customer where id = 1").FirstOrDefault();
                
                return result;
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }


#endregion
 * 
 */ 
