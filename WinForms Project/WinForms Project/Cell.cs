using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinForms_Project.Sim
{
    public abstract class Cell
    {
        public PointF Location { get; set; }
        public float Mass { get; set; }
        public bool Alive { get; set; }
        public abstract CellType Type { get; }
        public CellMode Mode { get; }
        public float Radius { get { return (float)Math.Pow(Mass * 3 / 4 / Math.PI, 1 / 3f); } }

        protected Simulation Container;

        public Cell(CellMode mode, Simulation container)
        {
            Mode = mode;
            Mass = 500;
            Alive = true;
            Container = container;
        }

        public virtual void Tick(CellConditions conditions)
        {
            if (!Alive)
            {
                return;
            }
            // Surface area / Salinity
            Mass -= 4 * (float)Math.PI * Radius * Radius * 1f / conditions.Salinity;
            if (Mass > 1000)
            {
                Mass = 1000;
            }
            else if (Mass <= 100)
            {
                Kill();
            }
            if (Mass >= Mode.SplitMass)
            {
                Container.SplitCell(this);
            }
        }

        internal void Kill()
        {
            Alive = false;
        }
    }
}
