using System;
using System.Collections.Generic;

namespace board
{
    public enum SquarePosition
    {
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }

    public static class SquarePositionExtensions
    {
        public static IEnumerable<Position> GetNeighbours(this SquarePosition position, Position inputPosition)
        {
            switch (position)
            {
                case SquarePosition.TOP_LEFT:
                    yield return new Position(inputPosition.Row, inputPosition.Column + 1);
                    yield return new Position(inputPosition.Row + 1, inputPosition.Column + 1);
                    yield return new Position(inputPosition.Row + 1, inputPosition.Column);
                    break;
                case SquarePosition.TOP_RIGHT:
                    yield return new Position(inputPosition.Row, inputPosition.Column - 1);
                    yield return new Position(inputPosition.Row + 1, inputPosition.Column - 1);
                    yield return new Position(inputPosition.Row + 1, inputPosition.Column);
                    break;
                case SquarePosition.BOTTOM_LEFT:
                    yield return new Position(inputPosition.Row, inputPosition.Column + 1);
                    yield return new Position(inputPosition.Row - 1, inputPosition.Column);
                    yield return new Position(inputPosition.Row - 1, inputPosition.Column + 1);
                    break;
                case SquarePosition.BOTTOM_RIGHT:
                    yield return new Position(inputPosition.Row - 1, inputPosition.Column - 1);
                    yield return new Position(inputPosition.Row - 1, inputPosition.Column);
                    yield return new Position(inputPosition.Row, inputPosition.Column - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }
    }
}