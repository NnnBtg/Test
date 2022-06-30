using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    interface IGeomFigure
    {
        int MaxX { get; }
        int MinX { get; }
        int MaxY { get; }
        int MinY { get; }
    }
}
