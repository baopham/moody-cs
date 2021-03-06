﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BP.Moody.Dijkstra
{
    public class Dijkstra
    {
        private readonly IList<Edge> edges;
        private readonly IList<Vertex> vertices;
        private Vertex source;
        private ISet<Vertex> visited;
        private ISet<Vertex> unvisited;
        private IDictionary<Vertex, double> smallestKnownDistanceFromSource;
        private IDictionary<Vertex, Vertex> previousVertex;

        public Dijkstra(Graph graph)
        {
            edges = new List<Edge>(graph.Edges);
            vertices = new List<Vertex>(graph.Vertices);
        }

        public Dijkstra SetSource(Vertex source)
        {
            Init();

            this.source = source;

            VisitNeighboursAndFindSmallestDistances(source);

            var currentVertex = FindMinimalUnvisitedVertex();

            while (currentVertex != null)
            {
                VisitNeighboursAndFindSmallestDistances(currentVertex);
                currentVertex = FindMinimalUnvisitedVertex();
            }

            return this;
        }

        public void PrintShortestPath(Vertex destination)
        {
            if (source == null)
            {
                throw new Exception("source has not been set");
            }

            var vertices = FindShortestPath(destination);

            Console.WriteLine(vertices.Select(v => v.name).Aggregate((v1, v2) => v1 + "->" + v2));
        }

        public LinkedList<Vertex> FindShortestPath(Vertex destination)
        {
            if (source == null)
            {
                throw new Exception("source has not been set");
            }

            var path = new LinkedList<Vertex>();
            var currentVertex = destination;

            while (!currentVertex.Equals(source))
            {
                path.AddFirst(currentVertex);
                currentVertex = previousVertex[currentVertex];
            }

            path.AddFirst(source);

            return path;
        }

        private Vertex FindMinimalUnvisitedVertex()
        {
            Vertex min = null;

            foreach (var vertex in unvisited)
            {
                var currentDistance = GetSmallestKnownDistance(vertex);
                var minDistance = GetSmallestKnownDistance(min);

                if (currentDistance < minDistance)
                {
                    min = vertex;
                }
            }

            return min;
        }

        private void VisitNeighboursAndFindSmallestDistances(Vertex source)
        {
            var neighbours = GetNeighbours(source);

            foreach (var neighbour in neighbours)
            {
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                var knownDistance = GetSmallestKnownDistance(neighbour);

                var newDistance = (double.IsPositiveInfinity(knownDistance) ? 0 : knownDistance)
                                  + GetDistance(source, neighbour);

                if (!(double.IsPositiveInfinity(knownDistance) || knownDistance > newDistance))
                {
                    continue;
                }

                smallestKnownDistanceFromSource[neighbour] = newDistance;
                previousVertex[neighbour] = source;
            }

            visited.Add(source);
            unvisited.Remove(source);
        }

        private double GetDistance(Vertex from, Vertex to)
        {
            foreach (var edge in edges)
            {
                if (edge.Contains(from) && edge.Contains(to))
                {
                    return edge.Weight;
                }
            }

            throw new Exception("No edge that contains the given Vertices");
        }

        private void Init()
        {
            smallestKnownDistanceFromSource = new Dictionary<Vertex, double>();
            visited = new HashSet<Vertex>();
            unvisited = new HashSet<Vertex>(vertices);
            previousVertex = new Dictionary<Vertex, Vertex>();
        }

        private IEnumerable<Vertex> GetNeighbours(Vertex vertex)
        {
            var neighbours = new List<Vertex>();

            foreach (var edge in edges)
            {
                if (edge.Vertex1.Equals(vertex))
                {
                    neighbours.Add(edge.Vertex2);
                }
                else if (edge.Vertex2.Equals(vertex))
                {
                    neighbours.Add(edge.Vertex1);
                }
            }

            return neighbours;
        }

        private double GetSmallestKnownDistance(Vertex vertex)
        {
            return vertex != null && smallestKnownDistanceFromSource.ContainsKey(vertex)
                ? smallestKnownDistanceFromSource[vertex]
                : double.PositiveInfinity;
        }
    }
}
