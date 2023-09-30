using System;
using System.Collections.Generic;
using System.Linq;

namespace board
{
    public class Maze
    {
        public readonly Board board;

        public Maze(int rows, int columns)
        {
            board = new Board(rows, columns);
        }

        public HashSet<Direction> GetDirectionsFrom(Position position)
        {
            return Enum.GetValues(typeof(Direction))
                .Cast<Direction>()
                .Where(direction => IsPositionValid(position.ApplyDirection(direction)))
                .ToHashSet();
        }

        private bool IsPositionValid(Position position)
        {
            return board.IsValid(position) && !CreatesPathSquare(position);
        }

        private bool CreatesPathSquare(Position position)
        {
            return Enum.GetValues(typeof(SquarePosition))
                .Cast<SquarePosition>()
                .Any(squarePosition =>
                {
                    var neighbours = squarePosition.GetNeighbours(position);
                    return neighbours.All(neighbour =>
                        board.IsValid(neighbour) && board.Get(neighbour) == Cell.PATH);
                });
        }

        public void SetAsWalked(Position position)
        {
            if (board.Get(position) == Cell.PATH) throw new ArgumentException("The position is already occupied");
            board.Set(position, Cell.PATH);
        }

        public bool PositionIsPath(Position position)
        {
            return board.Get(position) == Cell.PATH;
        }

        public List<List<Cell>> GetBoard()
        {
            return board.GetCells();
        }

        public Rectangle GetRectangleOfBoard()
        {
            return new Rectangle(board.GetColumns(), board.GetRows());
        }

        public bool WalkedIsBelowThreshold(float threshold)
        {
            var pathCells = board.GetAllCells().Count(cell => cell == Cell.PATH);
            var allCells = board.GetSize();
            return (float)pathCells / allCells < threshold;
        }
    }
}