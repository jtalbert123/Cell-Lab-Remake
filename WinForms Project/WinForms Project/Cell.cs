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
    }

    abstract class Cell : ICell
    {
        public PointF Location { get; set; }
        public float Mass { get; protected set; }
        public bool Alive { get; protected set; }
        public abstract CellType Type { get; }
        public CellMode Mode { get; protected set; }

        public Cell(CellMode mode)
        {
            Mode = mode;
            Mass = 50;
            Alive = true;
        }

        public virtual void Tick(CellConditions conditions)
        {
            if (!Alive)
            {
                return;
            }
            Mass -= 1f / conditions.Salinity;
            if (Mass > 100)
            {
                Mass = 100;
            }
            else if (Mass <= 10)
            {
                Alive = false;
            }
        }
    }
}
