using System;
using board;
using game.food;

namespace game
{
    public class GameState
    {
        private readonly GameBoard _board;
        private readonly IFoodSubscriber _foodEventEmitter;
        private Position _pacmanPosition;

        public GameState(Board board, Position pacmanPosition, IFoodSubscriber foodEventEmitter)
        {
            this._board = new GameBoard(board);
            this._pacmanPosition = pacmanPosition;
            this._foodEventEmitter = foodEventEmitter;
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
            _foodEventEmitter.EatFoodOn(_pacmanPosition);
        }

        public bool HasFinished()
        {
            return !_board.HasFood();
        }

        public Position GetPacmanPosition()
        {
            return _pacmanPosition;
        }
    }
}