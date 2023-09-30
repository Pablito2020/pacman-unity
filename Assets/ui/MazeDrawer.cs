using System;
using System.Collections.Generic;
using System.Linq;
using board;
using UnityEngine;

namespace ui
{
    public class MazeDrawer
    {
        private readonly List<GameObject> cells = new();
        private readonly GameObject corridorPrefab;
        private readonly Action<GameObject> destroy;
        private readonly Func<GameObject, GameObject> instantiate;
        private readonly GameObject wallPrefab;

        public MazeDrawer(Func<GameObject, GameObject> instantiate, Action<GameObject> destroy, GameObject wallPrefab,
            GameObject corridorPrefab)
        {
            this.instantiate = instantiate;
            this.destroy = destroy;
            this.wallPrefab = wallPrefab;
            this.corridorPrefab = corridorPrefab;
        }

        public void Draw(Maze maze, GameObject gameObject)
        {
            var board = maze.GetBoard();
            var boardSize = maze.GetRectangleOfBoard();
            foreach (var (rowOfCells, rowIndex) in board.Select((value, i) => (value, i)))
            foreach (var (cell, columnIndex) in rowOfCells.Select((value, i) => (value, i)))
            {
                var position = new Position(rowIndex, columnIndex);
                var prefab = GetPrefab(cell, position, boardSize, gameObject);
                cells.Add(prefab);
            }
        }

        private GameObject GetPrefab(Cell cell, Position position, Rectangle boardSize, GameObject gameObjectAttached)
        {
            var prefab = GetPrefab(cell);
            var prefabPosition = CellPositionCalculator.From(position, boardSize);
            prefab.transform.position = prefabPosition;
            prefab.transform.parent = gameObjectAttached.transform;
            return prefab;
        }

        private GameObject GetPrefab(Cell cell)
        {
            return cell switch
            {
                Cell.PATH => instantiate(corridorPrefab),
                Cell.WALL => instantiate(wallPrefab),
                _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
            };
        }


        public void Destroy()
        {
            cells.ForEach(destroy);
            cells.Clear();
        }
    }
}