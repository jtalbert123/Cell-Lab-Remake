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

namespace WinForms_Project
{
    public partial class SimDisplay : UserControl
    {
        private MyPanel SimView;

        Simulation simulation;
        bool lit = true;

        System.Timers.Timer tick;

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
            tick = new System.Timers.Timer();
            tick.Interval = 33;
            tick.Elapsed += Tick_Elapsed;
            tick.Enabled = true;
        }

        private void Tick_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new Action(() => Refresh()));
            }
            catch { }
        }

        private void SimView_Click(object sender, EventArgs e)
        {
            Point mousePos = SimView.PointToClient(Cursor.Position);
            simulation.AddCell(new CellMode() { Type = CellType.Photocyte }, new PointF(mousePos.X, mousePos.Y));
            Refresh();
        }

        private void SimView_Paint(object sender, PaintEventArgs e)
        {
            int antiailasing = 4;
            using (Bitmap antiAliasBase = new Bitmap(antiailasing * SimView.Width, antiailasing * SimView.Height))
            {
                Graphics target = Graphics.FromImage(antiAliasBase);
                foreach (Cell c in simulation.Cells)
                {
                    target.FillEllipse(Brushes.Green, new Rectangle((int)(antiailasing * (c.Location.X - c.Mass / 2)), (int)(antiailasing * (c.Location.Y - c.Mass / 2)), (int)(antiailasing * c.Mass), (int)(antiailasing * c.Mass)));
                }
                target.Dispose();
                e.Graphics.DrawImage(antiAliasBase, new Rectangle(0, 0, SimView.Width, SimView.Height));
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void SimDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    lit = !lit;
                    simulation.SetSunlight(lit ? 10 : 5);
                    BackColor = lit ? SystemColors.Control : SystemColors.ControlDark;
                    break;
                case Keys.Right:
                    simulation.SetSalinity(10);
                    break;
                case Keys.Left:
                    simulation.SetSalinity(1);
                    break;
            }
        }

        private void SalinityBar_ValueChanged(object sender, decimal value)
        {
            simulation.SetSalinity(SalinityBar.Value);
        }

        private void SunlightBar_ValueChanged(object sender, decimal value)
        {
            simulation.SetSunlight(SunlightBar.Value);
        }

        private void SimDisplay_Resize(object sender, EventArgs e)
        {
            SalinityBar.Width = flowLayoutPanel1.Width - (flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right);
            SalinityBar.Height = flowLayoutPanel1.Height - (flowLayoutPanel1.Padding.Top + flowLayoutPanel1.Padding.Bottom);

            SunlightBar.Width = flowLayoutPanel1.Width - (flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right);
            SunlightBar.Height = flowLayoutPanel1.Height - (flowLayoutPanel1.Padding.Top + flowLayoutPanel1.Padding.Bottom);
        }
    }
}
