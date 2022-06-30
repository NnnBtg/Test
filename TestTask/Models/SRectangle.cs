using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public struct SRectangle : IGeomFigure
    {
        public Point2D P1 { get; set; }
        public Point2D P2 { get; set; }
        public SRectangle(Point2D p1, Point2D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public int MinX
        {
            get { return P1.X < P2.X ? P1.X : P2.X; }
        }
        public int MaxX
        {
            get { return P1.X > P2.X ? P1.X : P2.X; }
        }
        public int MinY
        {
            get { return P1.Y < P2.Y ? P1.Y : P2.Y; }
        }
        public int MaxY
        {
            get { return P1.Y > P2.Y ? P1.Y : P2.Y; }
        }
        public int Width
        {
            get {return MaxX - MinX; }
        }
        public int Height
        {
            get { return MaxY - MinY; }
        }
        public bool ContainsPiece(Piece piece)
        {
            if ((piece.P1.X <= MaxX && piece.P1.X >= MinX && piece.P1.Y <= MaxY && piece.P1.Y >= MinY) ||
                (piece.P2.X <= MaxX && piece.P2.X >= MinX && piece.P2.Y <= MaxY && piece.P2.Y >= MinY))
                return true; //Одна из вершин отрезка расположена внутри прямоугольника
            else if (piece.CheckRectangleIntersection(this))
                return true; //Отрезок пересекает одну из граней прямоугольника
            else
                return false;
        }
    }
}
