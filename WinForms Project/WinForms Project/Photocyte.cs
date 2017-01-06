using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Project.Sim
{
    class Photocyte : Cell
    {
        public Photocyte(CellMode mode) : base(mode)
        {

        }

        public override CellType Type { get { return CellType.Photocyte; } }

        public override void Tick(CellConditions conditions)
        {
            if (!Alive)
            {
                return;
            }
            Mass += conditions.Sunlight * ((float)Math.PI*Radius*Radius) / 10;
            base.Tick(conditions);
        }
    }
}
