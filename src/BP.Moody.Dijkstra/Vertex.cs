using System;

namespace BP.Moody.Dijkstra
{
    public class Vertex
    {
        public string id { get;  }
        public string name { get; }

        public Vertex(string name)
        {
            id = Guid.NewGuid().ToString();
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            var vertex = obj as Vertex;

            if (vertex == null)
            {
                return false;
            }

            return vertex.id == id && vertex.name == name;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() ^ name.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
