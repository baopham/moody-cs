using System;

namespace BP.Moody.Dijkstra
{
    public class Edge
    {
        public string Id { get; }
        public Vertex Vertex1 { get; }
        public Vertex Vertex2 { get; }
        public double Weight { get; }

        public Edge(Vertex vertex1, Vertex vertex2, double weight)
        {
            Id = Guid.NewGuid().ToString();
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
        }

        public bool Contains(Vertex vertex)
        {
            return Vertex1.Equals(vertex) || Vertex2.Equals(vertex);
        }

        public override bool Equals(object obj)
        {
            var edge = obj as Edge;

            if (edge == null)
            {
                return false;
            }

            return edge.Id == Id &&
                   edge.Weight.Equals(Weight) &&
                   edge.Vertex1.Equals(Vertex1) &&
                   edge.Vertex2.Equals(Vertex2);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Weight.GetHashCode() ^ Vertex1.GetHashCode() ^ Vertex2.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Vertex1.name} --- {Vertex2.name}";
        }
    }
}
