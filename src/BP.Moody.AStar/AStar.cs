using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace BP.Moody.AStar
{
    public class AStar
    {
        private readonly Grid grid;
        private IDictionary<Cell, int> heuristicCosts;
        private ISet<Cell> closedList;
        private IPriorityQueue<Cell, int> openQueue;
        private IDictionary<Cell, int> movementCosts;
        private IDictionary<Cell, int> overallCosts;

        public AStar(Grid grid)
        {
            if (!grid.EndCell.HasValue || !grid.StartCell.HasValue)
            {
                throw new Exception("Grid needs to have end cell and start cell defined");
            }

            this.grid = grid;
        }

        public IList<Cell> FindShortestPath()
        {
            Init();

            var cameFrom = new Dictionary<Cell, Cell>();

            while (openQueue.Count > 0)
            {
                var currentCell = openQueue.Dequeue();

                closedList.Add(currentCell);

                if (currentCell.Is(grid.EndCell))
                {
                    return BuildPath(cameFrom, currentCell);
                }

                foreach (var neighbour in grid.GetNeighbours(currentCell))
                {
                    if (closedList.Contains(neighbour) || grid.CellIsBlocked(neighbour))
                    {
                        continue;
                    }

                    var tentativeMovementCost = movementCosts[currentCell] + GetMovementCost(currentCell, neighbour);

                    if (!openQueue.Contains(neighbour))
                    {
                        openQueue.Enqueue(neighbour, tentativeMovementCost);
                    }

                    if (movementCosts.ContainsKey(neighbour) && tentativeMovementCost >= movementCosts[neighbour])
                    {
                        continue;
                    }

                    cameFrom[neighbour] = currentCell;
                    movementCosts[neighbour] = tentativeMovementCost;
                    overallCosts[neighbour] = tentativeMovementCost + heuristicCosts[neighbour];
                }
            }

            return new List<Cell>();
        }

        private static IList<Cell> BuildPath(IDictionary<Cell, Cell> cameFrom, Cell current)
        {
            var path = new LinkedList<Cell>();

            path.AddFirst(current);

            while (cameFrom.ContainsKey(current))
            {
                path.AddFirst(cameFrom[current]);
                current = cameFrom[current];
            }

            return path.ToList();
        }

        private void Init()
        {
            FindAllHeuristicCosts();

            var start = grid.StartCell.GetValueOrDefault();

            closedList = new HashSet<Cell>
            {
                start
            };

            movementCosts = new Dictionary<Cell, int>
            {
                [start] = 0
            };

            overallCosts = new Dictionary<Cell, int>
            {
                [start] = heuristicCosts[start]
            };

            openQueue = new SimplePriorityQueue<Cell, int>();
            openQueue.Enqueue(start, overallCosts[start]);
        }

        private void FindAllHeuristicCosts()
        {
            heuristicCosts = new Dictionary<Cell, int>();

            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var cell = new Cell(x, y);

                    if (!grid.CellIsBlocked(cell))
                    {
                        heuristicCosts[cell] = GetHeuristicCost(cell, grid.EndCell.GetValueOrDefault());
                    }
                }
            }
        }

        // H cost
        private int GetHeuristicCost(Cell from, Cell to)
        {
            if (!grid.CellIsValid(from) || !grid.CellIsValid(to))
            {
                throw new Exception("Cell is not valid");
            }

            return Math.Abs(to.X - from.X) - Math.Abs(to.Y - to.Y);
        }

        // G cost
        private int GetMovementCost(Cell from, Cell to)
        {
            if (from.Equals(grid.StartCell))
            {
                return 0;
            }

            return from.X != to.X && from.Y != to.Y ? 14 : 10;
        }
    }
}