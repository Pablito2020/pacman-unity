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
            var emitter = new FoodRemover();
            _game = new Game(maze.board, emitter);
            _board = new BoardDrawer(prefabs, _game);
            _pacman = new PacmanDrawer(prefabs, maze.board.GetRectangleOfBoard());
        }

        public void Destroy()
        {
            _board.Destroy();
            _pacman.DestroyPacman();
        }

        public void StartNewGame(GameObject gameObject, float speed)
        {
            _pacman.InitPacman(_game.GetPacmanPosition(), _game.GetCurrentDirection(), speed);
            _board.Draw(gameObject);
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