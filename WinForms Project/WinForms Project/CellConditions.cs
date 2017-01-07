using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Project.Sim
{
    public struct CellConditions
    {
        public float Sunlight {get; set; }
        public float Nitrogen { get; set; }
        public float Salinity { get; set; }

        public CellConditions(float sunlight, float nitrogen, float salinity)
        {
            Sunlight = sunlight;
            Nitrogen = nitrogen;
            Salinity = salinity;
        }
    }
}
