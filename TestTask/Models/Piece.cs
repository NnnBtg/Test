using System;

namespace TestTask.Models
{
    public struct Piece : IGeomFigure
    {
        public Point2D P1 { get; set; }
        public Point2D P2 { get; set; }

        public Piece(Point2D p1, Point2D p2)
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

        public bool CheckRectangleIntersection(SRectangle rect)
        {
            //Делим прямоугольник на отрезки
            var rectPoint1 = new Point2D(rect.MinX, rect.MinY);
            var rectPoint2 = new Point2D(rect.MaxX, rect.MinY);
            var rectPoint3 = new Point2D(rect.MaxX, rect.MaxY);
            var rectPoint4 = new Point2D(rect.MinX, rect.MaxY);
            var piece1 = new Piece(rectPoint1, rectPoint2);
            var piece2 = new Piece(rectPoint2, rectPoint3);
            var piece3 = new Piece(rectPoint3, rectPoint4);
            var piece4 = new Piece(rectPoint4, rectPoint1);

            //Проверяем пересечение текущего отрезка с отрезками, из которых состоит прямоугольник
            if (piece1.CheckPieceIntersection(this))
                return true;
            else if (piece2.CheckPieceIntersection(this))
                return true;
            else if (piece3.CheckPieceIntersection(this))
                return true;
            else if (piece4.CheckPieceIntersection(this))
                return true;
            else 
                return false;
        }

        public bool CheckPieceIntersection(Piece anotherPiece)
        {
            Piece piece1;
            Piece piece2;
            if (P1.X <= P2.X)
                piece1 = new Piece(P1, P2);
            else
                piece1 = new Piece(P2, P1);

            if (anotherPiece.P1.X <= anotherPiece.P2.X)
                piece2 = new Piece(anotherPiece.P1, anotherPiece.P2);
            else
                piece2 = new Piece(anotherPiece.P2, anotherPiece.P1);

            //Отрезки однозначно не пересекаются
            if (piece1.MaxX < piece2.MinX || piece1.MinX > piece2.MaxX)
                return false; //Проекции отрезков на ось X не совпадают
            if (piece1.MaxY < piece2.MinY || piece1.MinY > piece2.MaxY)
                return false; //Проекции отрезков на ось Y не совпадают

            //Оба отрезка вертикальны и лежат на одной прямой
            if (piece1.P1.X == piece1.P2.X && piece2.P1.X == piece2.P2.X && piece1.P1.X == piece2.P1.X)
                return IntersectsOneLineVerticals(piece1, piece2);

            //если один из отрезков вертикальный
            if (piece1.P1.X == piece1.P2.X)
            return IntersectsWithVertical(piece1, piece2);
            if (piece2.P1.X == piece2.P2.X)
                return IntersectsWithVertical(piece2, piece1);

            double tgP1 = (piece1.P1.Y - piece1.P2.Y) / (piece1.P1.X - piece1.P2.X);
            double tgP2 = (piece2.P1.Y - piece2.P2.Y) / (piece2.P1.X - piece2.P2.X);
            double b1 = piece1.P1.Y - tgP1 * piece1.P1.X;
            double b2 = piece2.P1.Y - tgP2 * piece2.P1.X;

            if (tgP1 == tgP2)
            {
                return false; //Отрезки параллельны
            }

            //xA - абсцисса точки пересечения двух прямых
            double xA = (b2 - b1) / (tgP1 - tgP2);

            if ((xA < Math.Max(piece1.P1.X, piece2.P1.X)) || (xA > Math.Min(piece1.P2.X, piece2.P2.X)))
            {
                return false; //Точка Xa находится вне пересечения проекций отрезков на ось X 
            }
            else
            {
                return true;
            }
        }

        private bool IntersectsOneLineVerticals(Piece verticalPiece, Piece anotherPiece)
        {
            //Проверка на наличие общего Y (Легче проверить отрицанием случая, когда они НЕ пересекаются)
            if (!(verticalPiece.MaxY < anotherPiece.MinY || verticalPiece.MinY > anotherPiece.MaxY))
                return true;
            else 
                return false;
        }

        private bool IntersectsWithVertical(Piece verticalPiece, Piece anotherPiece)
        {
            double xICoord = verticalPiece.P1.X;//Точка пересечения по X
                double tgNonVertPiece = (anotherPiece.P1.Y - anotherPiece.P2.Y) / (anotherPiece.P1.X - anotherPiece.P2.X);
                double b2Par = anotherPiece.P1.Y - tgNonVertPiece * anotherPiece.P1.X;
                double yICoord = tgNonVertPiece * xICoord + b2Par;//Точка пересечения по Y

                if (anotherPiece.P1.X <= xICoord && anotherPiece.P2.X >= xICoord && Math.Min(verticalPiece.P1.Y, verticalPiece.P2.Y) <= yICoord &&
                        Math.Max(verticalPiece.P1.Y, verticalPiece.P2.Y) >= yICoord)
                    return true;
                else
                    return false;
        }
    }
}
