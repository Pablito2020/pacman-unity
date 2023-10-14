using board;
using UnityEngine;

namespace ui.pacman
{
    public class PacmanDrawer
    {
        private readonly Rectangle _boardSize;
        private readonly Prefabs _prefabs;
        private GameObject pacman;

        public PacmanDrawer(Prefabs prefabs, Rectangle boardSize)
        {
            this._prefabs = prefabs;
            this._boardSize = boardSize;
        }

        public void Draw(Position position, GameObject gameObject)
        {
            pacman = GetPrefab(position, gameObject);
        }

        private GameObject GetPrefab(Position position, GameObject gameObjectAttached)
        {
            var prefab = _prefabs.Instantiate(_prefabs.Pacman);
            var prefabPosition = CellPositionCalculator.From(position, _boardSize);
            prefab.transform.position = prefabPosition;
            prefab.transform.parent = gameObjectAttached.transform;
            return prefab;
        }

        public void Destroy()
        {
            _prefabs.Destroy(pacman);
        }
    }
}