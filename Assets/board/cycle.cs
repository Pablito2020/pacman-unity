using System;
using System.Collections.Generic;

namespace board
{
    public static class Cycle
    {
        public static HashSet<Position> Apply(Donut donut, Maze maze)
        {
            return Apply(donut, maze.board);
        }

        private static HashSet<Position> Apply(Donut donut, Board board)
        {
            if (!IsViable(donut, board)) throw new ArgumentException("Size of cycle is not viable");

            var cyclePosition = GetCycleInitialPosition(donut, board);
            var positions = donut.GetPositions(cyclePosition);

            foreach (var pos in positions) board.Set(pos, Cell.PATH);

            return positions;
        }

        private static Position GetCycleInitialPosition(Donut donut, Board board)
        {
            var rowsLeft = RowsLeftOverAfterCycle(donut, board);
            var columnsLeft = ColumnsLeftOverAfterCycle(donut, board);
            var rand = new Random();
            var row = rowsLeft > 0 ? rand.Next(0, rowsLeft) : 0;
            var column = columnsLeft > 0 ? rand.Next(0, columnsLeft) : 0;
            return new Position(row, column);
        }

        public static bool IsViable(Donut donut, Maze maze)
        {
            return IsViable(donut, maze.board);
        }

        public static bool IsViable(Donut donut, Board board)
        {
            return RowsLeftOverAfterCycle(donut, board) >= 0 && ColumnsLeftOverAfterCycle(donut, board) >= 0;
        }

        private static int RowsLeftOverAfterCycle(Donut donut, Board board)
        {
            return board.GetRows() - donut.Height();
        }

        private static int ColumnsLeftOverAfterCycle(Donut donut, Board board)
        {
            return board.GetColumns() - donut.Width();
        }
    }
}