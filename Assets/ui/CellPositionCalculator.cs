using board;
using UnityEngine;

namespace ui
{
    public static class CellPositionCalculator
    {
        public static Vector3 From(Position position, Rectangle board)
        {
            var column = position.Column - board.Width / 2;
            var row = position.Row - board.Height / 2;
            return new Vector3(column, row, 0);
        }
    }
}