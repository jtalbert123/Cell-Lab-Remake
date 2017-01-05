using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Project
{
    class MyPanel : Panel
    {
        public bool IsDoubleBuffered
        {
            get { return DoubleBuffered; }
            set { DoubleBuffered = value; }
        }
    }
}
