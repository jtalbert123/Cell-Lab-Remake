using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Project
{
    public partial class SimDisplay : UserControl
    {
        Simulation simulation;

        System.Timers.Timer tick;

        public SimDisplay()
        {
            InitializeComponent();
            simulation = new Simulation();
            tick = new System.Timers.Timer();
            tick.Interval = 33;
            tick.Elapsed += Tick_Elapsed;
            tick.Enabled = true;
            DoubleBuffered = true;
        }

        private void Tick_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new Action(() => Refresh()));
            }
            catch { }
        }

        private void SimDisplay_Click(object sender, EventArgs e)
        {
            Point mousePos = this.PointToClient(Cursor.Position);
            simulation.AddCell(new PointF(mousePos.X, mousePos.Y));
            Refresh();
        }

        private void SimDisplay_Paint(object sender, PaintEventArgs e)
        {
            int antiailasing = 4;
            using (Bitmap antiAliasBase = new Bitmap(antiailasing * this.Width, antiailasing * this.Height))
            {
                Graphics target = Graphics.FromImage(antiAliasBase);
                foreach (Cell c in simulation.Cells)
                {
                    target.FillEllipse(Brushes.Green, new Rectangle((int)(antiailasing * (c.Location.X - c.Mass / 2)), (int)(antiailasing * (c.Location.Y - c.Mass / 2)), (int)(antiailasing * c.Mass), (int)(antiailasing * c.Mass)));
                }
                target.Dispose();
                e.Graphics.DrawImage(antiAliasBase, new Rectangle(0, 0, this.Width, this.Height));
            }
        }

        private void SimDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                simulation.SetLight(!simulation.Light);
                BackColor = simulation.Light ? SystemColors.Control : SystemColors.ControlDarkDark;
            }
        }
    }
}
