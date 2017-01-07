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
        public IEnumerable<Cell> Cells { get { return cells; } }

        private ISet<Cell> cells;
        private CellConditions conditions;
        private ISet<Cell> newCells;

        public Simulation()
        {
            cells = new HashSet<Cell>();
            conditions = new CellConditions(10, 0, 10);
            newCells = new HashSet<Cell>();
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

        public void Tick()
        {
            HashSet<Cell> toRemove = new HashSet<Cell>();
            lock (cells)
            {
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
                    CellConditions localConditions = conditions;
                    localConditions.Sunlight = Math.Max(0, conditions.Sunlight * (3 - c.Location.Y/100f)/3);
                    c.Tick(localConditions);
                }
                foreach (Cell c in newCells)
                {
                    cells.Add(c);
                }
                newCells.Clear();
            }
        }

        public Cell NewCell(CellMode mode, PointF location)
        {
            Cell c = mode.CreateCell(this);
            c.Location = location;
            cells.Add(c);
            return c;
        }

        internal void SplitCell(Cell cell)
        {
            PointF child1Loc = cell.Location;
            child1Loc.Y -= 50;
            PointF child2Loc = cell.Location;
            child2Loc.Y += 50;
            Cell child1 = cell.Mode.CreateCell(this);
            child1.Location = child1Loc;
            Cell child2 = cell.Mode.CreateCell(this);
            child2.Location = child2Loc;
            child1.Mass = cell.Mass / 2;
            child2.Mass = cell.Mass / 2;
            if (child1Loc.Y > 0)
            {
                newCells.Add(child1);
            }
            newCells.Add(child2);
            cell.Kill();
        }
    }
}
