using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinForms_Project.Sim
{
    public interface ICell
    {
        PointF Location { get; }
        float Mass { get; }
        bool Alive { get; }
        CellType Type { get; }
        float Radius { get; }
    }

    abstract class Cell : ICell
    {
        public PointF Location { get; set; }
        public float Mass { get; protected set; }
        public bool Alive { get; protected set; }
        public abstract CellType Type { get; }
        public CellMode Mode { get; protected set; }
        public float Radius { get { return (float)Math.Pow(Mass * 3 / 4 / Math.PI, 1 / 3f); } }

        public Cell(CellMode mode)
        {
            Mode = mode;
            Mass = 500;
            Alive = true;
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
                Alive = false;
            }
        }
    }
}
