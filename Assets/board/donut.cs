using System;
using System.Collections.Generic;
using System.Linq;

namespace board
{
    public class Donut
    {
        private const int MINIMUM_SIDE_SIZE = 3;
        public static readonly int MINIMUM_AREA = MINIMUM_SIDE_SIZE * MINIMUM_SIDE_SIZE;
        private readonly Rectangle rectangle;

        public Donut(int width, int height)
        {
            rectangle = new Rectangle(width, height);
            if (rectangle.Area() < MINIMUM_AREA)
                throw new ArgumentException("You are not creating a donut, the donut should have a hole");
        }

        public HashSet<Position> GetPositions(Position initialPosition)
        {
            return rectangle.GetPositionsFromOrigin()
                .Where(pos => !IsInsideDonutHole(pos, rectangle))
                .Select(pos => initialPosition.Plus(pos))
                .ToHashSet();
        }

        public int Width()
        {
            return rectangle.Width;
        }

        public int Height()
        {
            return rectangle.Height;
        }

        private bool IsInsideDonutHole(Position currentPosition, Rectangle rectangle)
        {
            return !FirstOrLastRowOf(rectangle, currentPosition) && !FirstOrLastColumnOf(rectangle, currentPosition);
        }

        private bool FirstOrLastColumnOf(Rectangle rectangle, Position currentPosition)
        {
            return currentPosition.Column == 0 || currentPosition.Column == rectangle.Width - 1;
        }

        private bool FirstOrLastRowOf(Rectangle rectangle, Position currentPosition)
        {
            return currentPosition.Row == 0 || currentPosition.Row == rectangle.Height - 1;
        }
    }
}