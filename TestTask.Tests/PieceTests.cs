using System;
using Xunit;
using TestTask.Models;

namespace TestTask.Tests
{
    public class PieceTests
    {
        [Fact]
        public void CheckingIntersectionOfParallels_False()
        {
            //ARRANGE
            var piece1 = new Piece(new Point2D(5, -2), new Point2D(5, 2));
            var piece2 = new Piece(new Point2D(6, -2), new Point2D(6, 2));

            //ACT
            var result = piece1.CheckPieceIntersection(piece2);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 1, 3, 3, 2, 2, 3, 4, true)]//одна общая точка
        [InlineData(2, 1, 2, 4, 1, 2, 5, 4, true)]
        [InlineData(2, 1, 1, 3, 0, -2, 5, 4, false)]
        [InlineData(0, 0, 0, 78, 0, 0, 0, 100, true)] //отрезки лежат на одной вертикальной прямой и пересекаются
        [InlineData(0, 0, 0, 78, 0, 79, 0, 100, false)] //отрезки лежат на одной вертикальной прямой и не пересекаются
        public void CheckingIntersection(int x1, int y1, int x2, int y2,
            int x3, int y3, int x4, int y4, bool areIntersects)
        {
            //ARRANGE
            var piece1 = new Piece(new Point2D(x1, y1), new Point2D(x2, y2));
            var piece2 = new Piece(new Point2D(x3, y3), new Point2D(x4, y4));

            //ACT
            var result = piece1.CheckPieceIntersection(piece2);

            //Assert
            Assert.Equal(areIntersects, result);
        }
    }
}
