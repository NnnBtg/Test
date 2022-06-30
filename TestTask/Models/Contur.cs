using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public struct Contur
    {
        public List<Piece> Pieces { get; set; }

        public Contur(List<Piece> pieces)
        {
            Pieces = pieces;
        }
    }
}
