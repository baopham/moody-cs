using System;

namespace BP.Moody.Dijkstra
{
    public class Edge
    {
        public string id { get; }
        public Vertex vertex1 { get;  }
        public Vertex vertex2 { get;  }
        public double weight { get;  }

        public Edge(Vertex vertex1, Vertex vertex2, double weight)
        {
            id = Guid.NewGuid().ToString();
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.weight = weight;
        }

        public bool contains(Vertex vertex)
        {
            return vertex1.Equals(vertex) || vertex2.Equals(vertex);
        }

        public override bool Equals(object obj)
        {
            var edge = obj as Edge;

            if (edge == null)
            {
                return false;
            }

            return edge.id == id &&
                   edge.weight == weight &&
                   edge.vertex1.Equals(vertex1) &&
                   edge.vertex2.Equals(vertex2);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() ^ weight.GetHashCode() ^ vertex1.GetHashCode() ^ vertex2.GetHashCode();
        }

        public override string ToString()
        {
            return $"{vertex1.name} --- {vertex2.name}";
        }
    }
}