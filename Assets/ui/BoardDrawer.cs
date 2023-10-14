using System;
using System.Collections.Generic;
using System.Linq;
using board;
using UnityEngine;

namespace ui
{
    public class BoardDrawer
    {
        private readonly List<List<GameObject>> cells = new();
        private readonly Maze maze;
        private readonly Prefabs prefabs;
        
        public BoardDrawer(Prefabs prefabs, Maze maze)
        {
            this.prefabs = prefabs;
            this.maze = maze;
        }

        public void Draw(GameObject gameObject)
        {
            var board = maze.GetBoard();
            var boardSize = maze.GetRectangleOfBoard();
            foreach (var (rowOfCells, rowIndex) in board.Select((value, i) => (value, i)))
            {
                var row = new List<GameObject>();
                foreach (var (cell, columnIndex) in rowOfCells.Select((value, i) => (value, i)))
                {
                    var position = new Position(rowIndex, columnIndex);
                    var prefab = GetPrefab(cell, position, boardSize, gameObject);
                    row.Add(prefab);
                }

                cells.Add(row);
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
                Cell.PATH => prefabs.Instantiate(prefabs.Corridor),
                Cell.WALL => prefabs.Instantiate(prefabs.Wall),
                _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
            };
        }

        public void Destroy()
        {
            cells.ForEach(row => row.ForEach(prefabs.Destroy));
            cells.Clear();
        }
    }
}