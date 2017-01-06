using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Project.Sim
{
    class Genome
    {
        public IEnumerable<CellMode> Modes { get { return modes; } }
        public int InitialModeIndex { get; set; }

        private List<CellMode> modes;

        public Genome()
        {
            modes = new List<CellMode>();
        }
    }

    public enum CellType
    {
        Photocyte
    }

    public struct CellMode
    {
        public CellType Type { get; set; }

        internal Cell Create()
        {
            switch (Type)
            {
                case CellType.Photocyte:
                    return new Photocyte(this);
            }
            return null;
        }
    }
}
