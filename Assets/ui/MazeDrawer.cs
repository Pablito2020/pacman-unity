using board;
using UnityEngine;

namespace ui
{
    public class MazeDrawer
    {
        private readonly BoardDrawer _boardDrawer;

        public MazeDrawer(Prefabs prefabs, Maze maze)
        {
            _boardDrawer = new BoardDrawer(prefabs, maze);
        }

        public void Destroy()
        {
            _boardDrawer.Destroy();
        }

        public void Draw(GameObject gameObject)
        {
            _boardDrawer.Draw(gameObject);
        }
    }
}