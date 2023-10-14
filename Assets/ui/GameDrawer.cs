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
            var board = maze.board;
            _boardDrawer = new BoardDrawer(prefabs, board);
            _pacmanDrawer = new PacmanDrawer(prefabs, board.GetRectangleOfBoard());
            var emitter = new FoodRemover(_boardDrawer);
            _game = new Game(board, emitter);
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