using System;
using System.Collections.Generic;
using System.Linq;

namespace BP.Moody.AStar
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public Cell? StartCell { get; private set; }
        public Cell? EndCell { get; private set; }
        private readonly IDictionary<Cell, bool> blocked;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            blocked = new Dictionary<Cell, bool>();
        }

        public Grid SetBlockedCell(int x, int y)
        {
            var cell = new Cell(x, y);

            if (!CellIsValid(cell))
            {
                throw new Exception($"Invalid cooridinate ({x}, {y})");
            }

            blocked[cell] = true;

            return this;
        }

        public bool CellIsBlocked(Cell cell)
        {
            return blocked.ContainsKey(cell) && blocked[cell];
        }

        public Grid SetStartCell(int x, int y)
        {
            StartCell = new Cell(x, y);

            return this;
        }

        public Grid SetEndCell(int x, int y)
        {
            EndCell = new Cell(x, y);

            return this;
        }

        public bool CellIsValid(Cell cell)
        {
            var x = cell.X;
            var y = cell.Y;
            var outOfBound = x < 0 || x > Width - 1 || y < 0 || y > Height - 1;
            return !outOfBound;
        }

        public int GetOpenCellCount()
        {
            var total = Width * Height;
            return total - blocked.Count;
        }

        public IList<Cell> GetNeighbours(Cell cell)
        {
            var neighbours = new List<Cell>
            {
                new Cell(cell.X - 1, cell.Y),
                new Cell(cell.X + 1, cell.Y),

                new Cell(cell.X, cell.Y - 1),
                new Cell(cell.X, cell.Y + 1),

                new Cell(cell.X - 1, cell.Y - 1),
                new Cell(cell.X - 1, cell.Y + 1),

                new Cell(cell.X + 1, cell.Y - 1),
                new Cell(cell.X + 1, cell.Y + 1)
            };

            return neighbours.Where(neighbour => CellIsValid(neighbour) && !CellIsBlocked(neighbour)).ToList();
        }
    }
}
