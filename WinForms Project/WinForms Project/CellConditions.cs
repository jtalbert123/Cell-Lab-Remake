using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Project
{
    struct CellConditions
    {
        public float Sunlight {get; private set; }
        public float Nitrogen { get; private set; }
        public float Salinity { get; private set; }

        public CellConditions(float sunlight, float nitrogen, float salinity)
        {
            Sunlight = sunlight;
            Nitrogen = nitrogen;
            Salinity = salinity;
        }
    }
}
