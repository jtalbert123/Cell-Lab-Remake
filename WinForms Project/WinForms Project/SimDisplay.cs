using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_Project.Sim;
using System.Diagnostics;
using System.Threading;

namespace WinForms_Project
{
    public partial class SimDisplay : UserControl
    {
        private MyPanel SimView;

        Simulation simulation;

        public SimDisplay()
        {
            this.SimView = new MyPanel();

            InitializeComponent();

            this.SuspendLayout();
            // 
            // SimView
            // 
            this.SimView.Dock = DockStyle.Fill;
            this.SimView.Location = new Point(3, 3);
            this.SimView.Name = "SimView";
            this.SimView.Size = new Size(319, 385);
            this.SimView.TabIndex = 0;
            this.SimView.Click += new EventHandler(this.SimView_Click);
            this.SimView.Paint += new PaintEventHandler(this.SimView_Paint);
            this.SimView.IsDoubleBuffered = true;

            this.tableLayoutPanel1.Controls.Add(this.SimView, 0, 0);
            this.ResumeLayout();

            simulation = new Simulation();
            simulation.SetSalinity(SalinityBar.Value);
            simulation.SetSunlight(SunlightBar.Value);
            
            antiAliasBase = new Bitmap(antialiasing * SimView.Width, antialiasing * SimView.Height);
            antialiasTarget = Graphics.FromImage(antiAliasBase);

            Task.Run(new Action(Loop));
        }

        Stopwatch frameLimit;
        private void Loop()
        {
            frameLimit = new Stopwatch();
            while (true)
            {
                frameLimit.Restart();
                simulation.Tick();
                try
                {
                    Invoke(new Action(() => Refresh()));
                }
                catch { }
                frameLimit.Stop();
                if (frameLimit.ElapsedMilliseconds < 33)
                {
                    Thread.Sleep(33 - (int)frameLimit.ElapsedMilliseconds);
                }
            }
        }

        private void SimView_Click(object sender, EventArgs e)
        {
            Point mousePos = SimView.PointToClient(Cursor.Position);
            simulation.NewCell(new CellMode() { Type = CellType.Photocyte, SplitMass = 900 }, new PointF(mousePos.X, mousePos.Y));
            Refresh();
        }

        Bitmap antiAliasBase;
        Graphics antialiasTarget;
        int antialiasing = 1;

        private void SimView_Paint(object sender, PaintEventArgs e)
        {
            antialiasTarget.Clear(Color.CornflowerBlue);

            lock (simulation.Cells)
            {
                foreach (Cell c in simulation.Cells)
                {
                    RectangleF borders = new RectangleF((c.Location.X - c.Radius * 4), (c.Location.Y - c.Radius * 4), c.Radius * 2 * 4, c.Radius * 2 * 4);
                    RectangleF center = new RectangleF((c.Location.X - c.Radius), (c.Location.Y - c.Radius), c.Radius * 2, c.Radius * 2);
                    borders.X *= antialiasing;
                    borders.Y *= antialiasing;
                    borders.Width *= antialiasing;
                    borders.Height *= antialiasing;
                    center.X *= antialiasing;
                    center.Y *= antialiasing;
                    center.Width *= antialiasing;
                    center.Height *= antialiasing;
                    antialiasTarget.FillEllipse(Brushes.Green, borders);
                    antialiasTarget.FillEllipse(Brushes.Blue, center);
                    antialiasTarget.DrawEllipse(Pens.Blue, borders);
                }
            }
            e.Graphics.DrawImage(antiAliasBase, new Rectangle(0, 0, SimView.Width, SimView.Height));
        }

        private void SalinityBar_ValueChanged(object sender, decimal value)
        {
            simulation.SetSalinity((float)Math.Pow(2, SalinityBar.Value/10f));
        }

        private void SunlightBar_ValueChanged(object sender, decimal value)
        {
            simulation.SetSunlight(SunlightBar.Value/10f);
        }

        private void SimDisplay_Resize(object sender, EventArgs e)
        {
            SalinityBar.Width = flowLayoutPanel1.Width - (flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right);
            SalinityBar.Height = flowLayoutPanel1.Height - (flowLayoutPanel1.Padding.Top + flowLayoutPanel1.Padding.Bottom);

            SunlightBar.Width = flowLayoutPanel1.Width - (flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right);
            SunlightBar.Height = flowLayoutPanel1.Height - (flowLayoutPanel1.Padding.Top + flowLayoutPanel1.Padding.Bottom);
            
            // re-allocate antiailasing canvas
            antiAliasBase.Dispose();
            antialiasTarget.Dispose();
            antiAliasBase = new Bitmap(antialiasing * SimView.Width, antialiasing * SimView.Height);
            antialiasTarget = Graphics.FromImage(antiAliasBase);
        }
    }
}
