using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinForms_Project
{
    class Cell
    {
        public PointF Location { get; private set; }
        public float Mass { get; private set; }
        public bool Alive { get; private set; }

        public Cell(PointF initialLocation)
        {
            Location = initialLocation;
            Mass = 50;
            Alive = true;
        }

        public void Tick(CellConditions conditions)
        {
            if (!Alive)
            {
                return;
            }
            Mass += (conditions.Sunlight - 3) / 10;
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
