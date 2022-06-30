using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    static class Extentions
    {
        public static int GetLocalX(this MouseEventArgs e, int width)
        {
            return e.X - width / 2;
        }

        public static int GetLocalY(this MouseEventArgs e, int height)
        {
            return (e.Y - height / 2) * -1;
        }
    }
}
