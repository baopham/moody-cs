using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BP.Moody.AStar.Tests
{
    public class AStarTests
    {
        [Fact]
        public void Should_Find_Shortest_Path_In_Small_Grid()
        {
            var grid = new Grid(6, 7);
            grid.SetStartCell(1, 2)
                .SetEndCell(4, 5)
                .SetBlockedCell(3, 2)
                .SetBlockedCell(1, 4)
                .SetBlockedCell(3, 4)
                .SetBlockedCell(3, 5);

            var aStar = new AStar(grid);

            var path = aStar.FindShortestPath();

            var expectedPath = new List<Cell>
            {
                new Cell(1, 2),
                new Cell(2, 2),
                new Cell(3, 3),
                new Cell(4, 4),
                new Cell(4, 5)
            };

            Assert.Equal(expectedPath, path);

            PrintPath(path);

            grid.Print(expectedPath);
        }

        [Fact]
        public void Should_Find_Shortest_Path_In_Medium_Grid()
        {
            var grid = new Grid(11, 6);
            grid.SetStartCell(7, 4)
                .SetEndCell(4, 1)
                .SetBlockedCell(3, 1)
                .SetBlockedCell(3, 2)
                .SetBlockedCell(4, 2)
                .SetBlockedCell(5, 2)
                .SetBlockedCell(6, 2)
                .SetBlockedCell(7, 2);

            var aStar = new AStar(grid);

            var path = aStar.FindShortestPath();

            var expectedPath = new List<Cell>
            {
                new Cell(7, 4),
                new Cell(7, 3),
                new Cell(8, 2),
                new Cell(7, 1),
                new Cell(6, 1),
                new Cell(5, 1),
                new Cell(4, 1)
            };

            Assert.Equal(expectedPath, path);

            PrintPath(path);

            grid.Print(expectedPath);
        }

        private static void PrintPath(IEnumerable<Cell> path)
        {
            Console.WriteLine(path.Select(cell => cell.ToString()).Aggregate((from, to) => from + "->" + to));
        }
    }
}
