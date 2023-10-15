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
        private readonly List<List<CellUi>> cells = new();
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
                var row = new List<CellUi>();
                foreach (var (cell, columnIndex) in rowOfCells.Select((value, i) => (value, i)))
                {
                    var position = new Position(rowIndex, columnIndex);
                    var prefab = GetPrefab(cell, position, boardSize, gameObject);
                    row.Add(prefab);
                }

                cells.Add(row);
            }
        }

        
        private CellUi GetPrefab(Cell cell, Position position, Rectangle boardSize, GameObject gameObjectAttached)
        {
            var prefabsFromCell = GetPrefab(cell);
            prefabsFromCell.Apply(p =>
            {
                p.SetActive(true);
                var prefabPosition = CellPositionCalculator.From(position, boardSize);
                p.transform.position = prefabPosition;
                p.transform.parent = gameObjectAttached.transform;
            });
            return prefabsFromCell;
        }

        private CellUi GetPrefab(Cell cell)
        {
            return cell switch
            {
                Cell.PATH => new CellUi(prefabs.Instantiate(prefabs.Corridor)),
                Cell.WALL => new CellUi(prefabs.Instantiate(prefabs.Wall)),
                Cell.FOOD => new CellUi(prefabs.Instantiate(prefabs.Corridor), prefabs.Instantiate(prefabs.Food)),
                Cell.BIG_FOOD => new CellUi(prefabs.Instantiate(prefabs.Corridor), prefabs.Instantiate(prefabs.BigFood)),
                _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
            };
        }

        public void Destroy()
        {
            cells.ForEach(row => row.ForEach(cell => cell.Destroy(prefabs.Destroy)));
            cells.Clear();
        }

    }
}