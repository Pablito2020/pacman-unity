using board;
using game.food;
using game.initializers;

namespace game
{
    public class Game
    {
        private readonly GameState _state;
        private Direction _currentDirection;

        public Game(Board board, IFoodSubscriber foodEventEmitter) : this(board, new PacmanRandomInitializer(), foodEventEmitter)
        {
        }

        private Game(Board board, IPacmanInitializer initializer, IFoodSubscriber foodEventEmitter)
        {
            var initialPosition = initializer.GetInitialPosition(board);
            var boardWithFood = GameBoardCreator.FillWithFood(board, initialPosition);
            _state = new GameState(boardWithFood, initialPosition, foodEventEmitter);
            _currentDirection = initializer.GetInitialDirection(_state);
        }

        public void SetDirection(Direction direction)
        {
            _currentDirection = direction;
        }

        public void Move()
        {
            if (_state.CanApply(_currentDirection))
                _state.Move(_currentDirection);
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
    }
}