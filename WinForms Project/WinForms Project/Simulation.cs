using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WinForms_Project
{
    class Simulation
    {
        public IEnumerable<Cell> Cells { get { return cells; } }
        public bool Light { get; private set; }

        private ISet<Cell> cells;
        private Timer tick;

        public Simulation()
        {
            cells = new HashSet<Cell>();
            Light = true;
            tick = new Timer();
            tick.Interval = 33;
            tick.Elapsed += Tick_Elapsed;
            tick.Enabled = true;
        }

        private void Tick_Elapsed(object sender, ElapsedEventArgs e)
        {
            CellConditions conditions = new CellConditions(Light ? 10 : 0, 0, 0);
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

        public Cell AddCell(PointF location)
        {
            Cell c = new Cell(location);
            cells.Add(c);
            return c;
        }

        public void SetLight(bool state)
        {
            Light = state;
        }
    }
}
