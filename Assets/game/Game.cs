using board;
using game.initializers;

namespace game
{
    public class Game
    {
        private readonly GameState _state;
        private Direction _currentDirection;

        public Game(Board board) : this(board, new PacmanRandomInitializer())
        {
        }

        private Game(Board board, IPacmanInitializer initializer)
        {
            var initialPosition = initializer.GetInitialPosition(board);
            var boardWithFood = GameBoardCreator.FillWithFood(board, initialPosition);
            _state = new GameState(boardWithFood, initialPosition);
            _currentDirection = initializer.GetInitialDirection(_state);
        }

        public void SetDirection(Direction direction)
        {
            _currentDirection = direction;
        }

        public bool Move()
        {
            if (!_state.CanApply(_currentDirection)) return false;
            _state.Move(_currentDirection);
            return true;
        }

        public bool HasFinished()
        {
            return _state.HasFinished();
        }

        public Position GetPacmanPosition()
        {
            return _state.GetPacmanPosition();
        }

        public Board GetBoard()
        {
            return _state.GetBoard();
        }
        
        public Direction GetCurrentDirection()
        {
            return _currentDirection;
        }
    }
}