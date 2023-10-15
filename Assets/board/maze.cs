using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace board
{
    public class Maze
    {
        public readonly Board board;

        public Maze(int rows, int columns)
        {
            board = new Board(rows, columns);
        }

        private Maze(Board b)
        {
            board = b;
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

        public bool WalkedIsBelowThreshold(float threshold)
        {
            var pathCells = board.GetAllCells().Count(cell => cell == Cell.PATH);
            var allCells = board.GetSize();
            return (float)pathCells / allCells < threshold;
        }

        public Maze Resize(int rows, int columns)
        {
            if (rows < board.GetRows() || columns < board.GetColumns())
                throw new ArgumentException("The new size should be bigger than the current one");
            return new Maze(board.Resize(rows, columns));
        }
    }
}