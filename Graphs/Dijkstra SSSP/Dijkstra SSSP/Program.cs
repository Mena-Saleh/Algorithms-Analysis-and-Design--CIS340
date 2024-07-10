using System;
using System.Collections.Generic;

class DijkstraAlgorithm
{
    private int vertices;
    private List<Edge>[] adjacencyList;

    class Edge
    {
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Edge(int destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }

    public DijkstraAlgorithm(int v)
    {
        vertices = v;
        adjacencyList = new List<Edge>[v];

        for (int i = 0; i < v; i++)
        {
            adjacencyList[i] = new List<Edge>();
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        // Add an edge to the adjacency list
        adjacencyList[source].Add(new Edge(destination, weight));
    }

    public void DijkstraSSSP(int source)
    {
        int[] distances = new int[vertices];
        int?[] parents = new int?[vertices];
        bool[] visited = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            distances[i] = int.MaxValue;
            parents[i] = null;
            visited[i] = false;
        }

        distances[source] = 0;

        while (true)
        {
            // Find the vertex with the minimum distance from the source among the unvisited vertices
            int minDistance = int.MaxValue;
            int minVertex = -1;

            for (int v = 0; v < vertices; v++)
            {
                if (!visited[v] && distances[v] < minDistance)
                {
                    minDistance = distances[v];
                    minVertex = v;
                }
            }

            if (minVertex == -1)
            {
                // No more unvisited vertices
                break;
            }

            visited[minVertex] = true;

            // Process all neighbors of the current vertex
            foreach (Edge edge in adjacencyList[minVertex])
            {
                int destination = edge.Destination;
                int weight = edge.Weight;

                // Calculate the new distance via the current vertex
                int newDistance = distances[minVertex] + weight;

                // Update the distance and parent if the new distance is smaller
                if (newDistance < distances[destination])
                {
                    distances[destination] = newDistance;
                    parents[destination] = minVertex;
                }
            }
        }

        // Print the shortest distances from the source vertex to all other vertices
        Console.WriteLine("Shortest Distances from Source Vertex " + source + ":");
        for (int i = 0; i < vertices; i++)
        {
            Console.WriteLine("Vertex " + i + ": " + distances[i]);

            // Print the path if a valid shortest path exists
            if (distances[i] != int.MaxValue)
            {
                Console.Write("Path: " + i);
                int? parent = parents[i];
                while (parent != null)
                {
                    Console.Write(" <- " + parent);
                    parent = parents[parent.Value];
                }
                Console.WriteLine();
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        int vertices = 6;
        DijkstraAlgorithm graph = new DijkstraAlgorithm(vertices);

        // Add the edges of the graph
        graph.AddEdge(0, 1, 4);
        graph.AddEdge(0, 2, 1);
        graph.AddEdge(1, 3, 1);
        graph.AddEdge(2, 1, 2);
        graph.AddEdge(2, 3, 5);
        graph.AddEdge(3, 4, 3);
        graph.AddEdge(4, 0, 2);
        graph.AddEdge(4, 1, 4);
        graph.AddEdge(4, 5, 6);

        int sourceVertex = 0; // Set the source vertex

        // Apply Dijkstra's algorithm to find the shortest paths from the source vertex
        graph.DijkstraSSSP(sourceVertex);
    }
}
