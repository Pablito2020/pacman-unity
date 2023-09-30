using System.Collections.Generic;

namespace board
{
    public class Rectangle
    {
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public int Area()
        {
            return Width * Height;
        }

        public HashSet<Position> GetPositionsFromOrigin()
        {
            var set = new HashSet<Position>();
            for (var row = 0; row < Height; row++)
            for (var column = 0; column < Width; column++)
                set.Add(new Position(row, column));
            return set;
        }
    }
}