using System;
using System.Collections.Generic;
using System.Linq;

namespace board
{
    public class Board
    {
        private readonly List<List<Cell>> maze;
        private readonly int size;

        public Board(int rows, int columns)
        {
            maze = new List<List<Cell>>();
            size = rows * columns;
            PopulateMaze(rows, columns);
        }

        private Board(List<List<Cell>> maze, int rows, int columns)
        {
            this.maze = maze;
            this.size = rows * columns;
        }

        private void PopulateMaze(int rows, int columns)
        {
            for (var i = 0; i < rows; i++)
                maze.Add(Enumerable.Repeat(Cell.WALL, columns).ToList());
        }

        public bool IsValid(Position position)
        {
            return IsInsideRow(position) && IsInsideColumn(position);
        }

        private bool IsInsideRow(Position position)
        {
            return position.Row >= 0 && position.Row < maze.Count;
        }

        private bool IsInsideColumn(Position position)
        {
            return position.Column >= 0 && position.Column < maze[0].Count;
        }

        public void Set(Position position, Cell cell)
        {
            if (!IsValid(position))
                throw new ArgumentException("Position is not valid");
            maze[position.Row][position.Column] = cell;
        }

        public Cell Get(Position position)
        {
            if (!IsValid(position))
                throw new ArgumentException("Position is not valid");
            return maze[position.Row][position.Column];
        }

        public int GetColumns()
        {
            return maze[0].Count;
        }

        public int GetRows()
        {
            return maze.Count;
        }

        public List<Cell> GetAllCells()
        {
            return maze.SelectMany(row => row).ToList();
        }

        public List<List<Cell>> GetCells()
        {
            return maze;
        }

        public int GetSize()
        {
            return size;
        }
        
        public Board DeepCopy()
        {
            var newBoard = new Board(maze.Count, maze[0].Count);
            for (var i = 0; i < maze.Count; i++)
            for (var j = 0; j < maze[0].Count; j++)
                newBoard.Set(new Position(i, j), maze[i][j]);
            return newBoard;
        }
        
        public Rectangle GetRectangleOfBoard()
        {
            return new Rectangle(GetColumns(), GetRows());
        }

        public Board Resize(int rows, int columns)
        {
            var board = new List<List<Cell>>();
            for (var i = 0; i < rows; i++)
                board.Add(Enumerable.Repeat(Cell.WALL, columns).ToList());
            
            var numberOfNewRows = rows - GetRows();
            var numberOfNewColumns = columns - GetColumns();
            
            for (var row = 0; row < GetRows(); row++)
            {
                for ( var column = 0; column < GetColumns(); column++)
                {
                    board[row + (numberOfNewRows / 2)][column + (numberOfNewColumns / 2)] = maze[row][column];
                }
            }
            return new Board(board, rows, columns);
        }
    }
}