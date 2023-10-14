using System;
using System.Collections.Generic;
using System.Linq;
using board;
using game;
using UnityEngine;

namespace ui
{
    public class BoardDrawer
    {
        private readonly List<List<GameObject>> cells = new();
        private readonly Board board;
        private readonly Prefabs prefabs;
        
        public BoardDrawer(Prefabs prefabs, Game game)
        {
            this.prefabs = prefabs;
            this.board = game.GetBoard();
        }

        public void Draw(GameObject gameObject)
        {
            var boardSize = this.board.GetRectangleOfBoard();
            var boardPrimitives = this.board.GetCells();
            foreach (var (rowOfCells, rowIndex) in boardPrimitives.Select((value, i) => (value, i)))
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
            prefab.SetActive(true);
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
                Cell.FOOD => prefabs.Instantiate(prefabs.Food),
                _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
            };
        }

        public void Destroy()
        {
            cells.ForEach(row => row.ForEach(prefabs.Destroy));
            cells.Clear();
        }

        public void Set(Position position, Cell type)
        {
            var cell = cells[position.Row][position.Column];
            prefabs.Destroy(cell);
            cells[position.Row][position.Column] = GetPrefab(type, position, board.GetRectangleOfBoard(), cell.transform.parent.gameObject);
        }
    }
}