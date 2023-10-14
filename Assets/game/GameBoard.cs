using System;
using System.Linq;
using board;

namespace game
{
    public class GameBoard
    {
        private readonly Board _board;

        public GameBoard(Board board)
        {
            _board = board;
        }

        public bool IsWalkable(Position position)
        {
            return _board.IsValid(position) && _board.Get(position).IsWalkable();
        }

        public void EatFruitOn(Position position)
        {
            if (!IsWalkable(position) || !HasFruit(position))
                throw new ArgumentException("Invalid position");
            _board.Set(position, Cell.PATH);
        }

        public bool HasFruit(Position pacmanPosition)
        {
            return _board.Get(pacmanPosition) == Cell.FOOD;
        }

        public bool HasFood()
        {
            return _board.GetAllCells().Any(cell => cell == Cell.FOOD);
        }

        public Board GetBoard()
        {
            return _board.DeepCopy();
        }
    }
}