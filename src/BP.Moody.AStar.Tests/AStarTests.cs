using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BP.Moody.AStar.Tests
{
    public class AStarTests
    {
        [Fact]
        public void Should_Find_Shortest_Path()
        {
            var grid = new Grid(5, 6);
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

            Console.WriteLine(path.Select(cell => cell.ToString()).Aggregate((from, to) => from + "->" + to));
        }
    }
}
