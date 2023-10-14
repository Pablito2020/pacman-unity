using board;
using game;
using ui.food;
using ui.pacman;
using UnityEngine;

namespace ui
{
    public class GameDrawer
    {
        private readonly BoardDrawer _boardDrawer;
        private readonly PacmanDrawer _pacmanDrawer;
        private readonly Game _game;

        public GameDrawer(Prefabs prefabs, Maze maze)
        {
            var emitter = new FoodRemover((position, cell) => _boardDrawer?.Set(position, cell));
            _game = new Game(maze.board, emitter);
            _boardDrawer = new BoardDrawer(prefabs, _game);
            _pacmanDrawer = new PacmanDrawer(prefabs, maze.board.GetRectangleOfBoard());
        }

        public void Destroy()
        {
            _boardDrawer.Destroy();
            _pacmanDrawer.Destroy();
        }

        public void Draw(GameObject gameObject)
        {
            _boardDrawer.Draw(gameObject);
        }
        
        public void HasFinished()
        {
            _game.HasFinished();
        }
        
        public void Move(GameObject gameObject)
        {
            _pacmanDrawer.Destroy();
            _game.Move();
            _pacmanDrawer.Draw(_game.GetPacmanPosition(), gameObject);
        }
        
        public void SetDirection(Direction direction)
        {
            _game.SetDirection(direction);
        }
        
        
    }
}