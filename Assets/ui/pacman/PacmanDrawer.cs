using board;

namespace ui.pacman
{
    public class PacmanDrawer
    {
        private readonly Rectangle _boardSize;
        private readonly Prefabs _prefabs;

        public PacmanDrawer(Prefabs prefabs, Rectangle boardSize)
        {
            _prefabs = prefabs;
            _boardSize = boardSize;
        }

        public void GoTo(Position position, Direction direction)
        {
            var pos = CellPositionCalculator.From(position, _boardSize);
            _prefabs.Pacman.GoTo(pos, direction);
        }

        public void InitPacman(Position position, Direction direction, float speed)
        {
            _prefabs.Pacman.InitPacman(CellPositionCalculator.From(position, _boardSize), direction, speed);
        }

        public void DestroyPacman()
        {
            _prefabs.Pacman.Destroy();
        }

    }
}