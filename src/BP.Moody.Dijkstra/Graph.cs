using System.Collections.Generic;

namespace BP.Moody.Dijkstra
{
    public class Graph
    {
        public IList<Vertex> vertices { get;  }
        public IList<Edge> edges { get; }

        public Graph(IList<Vertex> vertices, IList<Edge> edges)
        {
            this.vertices = vertices;
            this.edges = edges;
        }
    }
}