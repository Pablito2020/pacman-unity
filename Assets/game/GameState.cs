using System;
using board;

namespace game
{
    public class GameState
    {
        private readonly GameBoard _board;
        private Position _pacmanPosition;

        public GameState(Board board, Position pacmanPosition)
        {
            _board = new GameBoard(board);
            _pacmanPosition = pacmanPosition;
        }

        public bool CanApply(Direction direction)
        {
            var newPosition = _pacmanPosition.ApplyDirection(direction);
            return _board.IsWalkable(newPosition);
        }

        public void Move(Direction direction)
        {
            if (!CanApply(direction))
                throw new ArgumentException("Invalid direction");

            _pacmanPosition = _pacmanPosition.ApplyDirection(direction);

            if (!_board.HasFruit(_pacmanPosition)) return;
            _board.EatFruitOn(_pacmanPosition);
        }

        public bool HasFinished()
        {
            return !_board.HasFood();
        }

        public Position GetPacmanPosition()
        {
            return _pacmanPosition;
        }

        public Board GetBoard()
        {
            return _board.GetBoard();
        }
    }
}