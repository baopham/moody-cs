using System;
using System.Collections.Generic;
using Xunit;

namespace BP.Moody.Dijkstra.Tests
{
    public class DijkstraTests
    {
        [Fact]
        public void Should_Find_The_Shortest_Path()
        {
            var vertexA = new Vertex("A");
            var vertexB = new Vertex("B");
            var vertexC = new Vertex("C");
            var vertexD = new Vertex("D");
            var vertexE = new Vertex("E");

            var vertices = new List<Vertex>
            {
                vertexA,
                vertexB,
                vertexC,
                vertexD,
                vertexE
            };

            var edgeAB = new Edge(vertexA, vertexB, 6.0);
            var edgeAD = new Edge(vertexA, vertexD, 1.0);
            var edgeDB = new Edge(vertexD, vertexB, 2.0);
            var edgeBE = new Edge(vertexB, vertexE, 2.0);
            var edgeDE = new Edge(vertexD, vertexE, 1.0);
            var edgeBC = new Edge(vertexB, vertexC, 5.0);
            var edgeEC = new Edge(vertexE, vertexC, 5.0);

            var edges = new List<Edge>
            {
                edgeAB,
                edgeAD,
                edgeDB,
                edgeBE,
                edgeDE,
                edgeBC,
                edgeEC
            };

            var shortestPathToVertexE = new LinkedList<Vertex>();
            shortestPathToVertexE.AddLast(vertexA);
            shortestPathToVertexE.AddLast(vertexD);
            shortestPathToVertexE.AddLast(vertexE);

            var graph = new Graph(vertices, edges);

            var algorithm = new Dijkstra(graph);

            Assert.Equal(shortestPathToVertexE, algorithm.SetSource(vertexA).FindShortestPath(vertexE));
        }
    }
}