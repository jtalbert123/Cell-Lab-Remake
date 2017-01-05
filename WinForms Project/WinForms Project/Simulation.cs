using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WinForms_Project.Sim
{
    public class Simulation
    {
        public IEnumerable<ICell> Cells { get { return cells; } }

        private ISet<Cell> cells;
        private Timer tick;
        private CellConditions conditions;

        public Simulation()
        {
            cells = new HashSet<Cell>();
            conditions = new CellConditions(10, 0, 10);
            tick = new Timer();
            tick.Interval = 33;
            tick.Elapsed += Tick_Elapsed;
            tick.Enabled = true;
        }

        public void SetSalinity(float Salinity)
        {
            if (Salinity < 1)
            {
                Salinity = 1;
            }
            conditions.Salinity = Salinity;
        }
        public void SetSunlight(float Light)
        {
            conditions.Sunlight = Light;
        }

        private void Tick_Elapsed(object sender, ElapsedEventArgs e)
        {
            HashSet<Cell> toRemove = new HashSet<Cell>();
            foreach (Cell c in Cells)
            {
                if (!c.Alive)
                {
                    toRemove.Add(c);
                }
            }
            foreach (Cell c in toRemove)
            {
                cells.Remove(c);
            }

            foreach (Cell c in Cells)
            {
                c.Tick(conditions);
            }
        }

        public ICell AddCell(CellMode mode, PointF location)
        {
            Cell c = mode.Create();
            c.Location = location;
            cells.Add(c);
            return c;
        }
    }
}
