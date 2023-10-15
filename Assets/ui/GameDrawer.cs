using board;
using game;
using ui.food;
using ui.pacman;
using UnityEngine;

namespace ui
{
    public class GameDrawer
    {
        private readonly BoardDrawer _board;
        private readonly PacmanDrawer _pacman;
        private readonly Game _game;

        public GameDrawer(Prefabs prefabs, Maze maze)
        {
            var emitter = new FoodRemover((position => _board.EatFruit(position)));
            _game = new Game(maze.board, emitter);
            _board = new BoardDrawer(prefabs, _game);
            _pacman = new PacmanDrawer(prefabs, maze.board.GetRectangleOfBoard());
        }

        public void Destroy()
        {
            _board.Destroy();
        }

        public void StartNewGame(GameObject gameObject, float speed)
        {
            _board.Draw(gameObject);
            _pacman.InitPacman(_game.GetPacmanPosition(), _game.GetCurrentDirection(), speed);
        }
        
        public bool HasFinished()
        {
            return _game.HasFinished();
        }
        
        public void Move()
        {
            if (_game.Move())
                _pacman.GoTo(_game.GetPacmanPosition(), _game.GetCurrentDirection());
        }
        
        public void SetDirection(Direction direction)
        {
            _game.SetDirection(direction);
        }
        
        
    }
}