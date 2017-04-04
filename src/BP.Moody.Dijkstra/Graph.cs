using System.Collections.Generic;

namespace BP.Moody.Dijkstra
{
    public class Graph
    {
        public IList<Vertex> Vertices { get; }
        public IList<Edge> Edges { get; }

        public Graph(IList<Vertex> vertices, IList<Edge> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }
    }
}