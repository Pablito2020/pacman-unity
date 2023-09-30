using System;

namespace board
{
    public class Position : IComparable<Position>
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        // IComparable implementation for Board.Position
        public int CompareTo(Position other)
        {
            if (Row == other.Row)
                return Column - other.Column;
            return Row - other.Row;
        }

        public Position ApplyDirection(Direction direction)
        {
            return direction switch
            {
                Direction.UP => new Position(Row - 1, Column),
                Direction.DOWN => new Position(Row + 1, Column),
                Direction.LEFT => new Position(Row, Column - 1),
                Direction.RIGHT => new Position(Row, Column + 1),
                _ => throw new ArgumentException("Invalid direction")
            };
        }

        public Position Plus(Position pos)
        {
            return new Position(Row + pos.Row, Column + pos.Column);
        }

        // Optionally override GetHashCode and Equals for proper hashset functionality
        public override int GetHashCode()
        {
            return (Row, Column).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other) return Row == other.Row && Column == other.Column;
            return false;
        }
    }
}