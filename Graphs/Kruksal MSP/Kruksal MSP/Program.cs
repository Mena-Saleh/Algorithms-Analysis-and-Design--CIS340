using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int Source { get; set; }
    public int Destination { get; set; }
    public int Weight { get; set; }

    public int CompareTo(Edge other)
    {
        return this.Weight.CompareTo(other.Weight);
    }
}

class KruskalAlgorithm
{
    private int vertices;
    private List<Edge> edges;

    public KruskalAlgorithm(int v)
    {
        vertices = v;
        edges = new List<Edge>();
    }

    public void AddEdge(int source, int destination, int weight)
    {
        edges.Add(new Edge { Source = source, Destination = destination, Weight = weight });
    }

    private int FindSet(int[] member, int v)
    {
        return member[v];
    }

    private void Union(int[] member, int x, int y)
    {
        int xSet = member[x];
        int ySet = member[y];

        // Merge two sets by updating the set ID of vertices belonging to the same set
        for (int i = 0; i < member.Length; i++)
        {
            if (member[i] == ySet)
                member[i] = xSet;
        }
    }

    private void MakeSet(int[] member, int v)
    {
        // Initialize the set ID of a vertex to itself
        member[v] = v;
    }

    public void GenerateMST()
    {
        List<Edge> result = new List<Edge>();
        int[] member = new int[vertices];

        // Initialize each vertex as a separate set
        for (int i = 0; i < vertices; i++)
        {
            MakeSet(member, i);
        }

        // Sort the edges based on their weights
        edges.Sort();

        int edgeCount = 0; // Counter to keep track of the number of edges in the minimum spanning tree
        int totalWeight = 0; // Variable to store the total weight of the minimum spanning tree

        // Process each edge in sorted order
        foreach (Edge edge in edges)
        {
            int x = FindSet(member, edge.Source);
            int y = FindSet(member, edge.Destination);

            // If the source and destination vertices are in different sets, add the edge to the minimum spanning tree
            if (x != y)
            {
                result.Add(edge);
                Union(member, x, y); // Merge the sets
                edgeCount++;
                totalWeight += edge.Weight; // Add the weight of the current edge to the total weight
            }

            // If we have added enough edges to form the minimum spanning tree, exit the loop
            if (edgeCount == vertices - 1)
                break;
        }

        // Print the minimum spanning tree
        Console.WriteLine("Minimum Spanning Tree:");
        foreach (Edge edge in result)
        {
            Console.WriteLine($"{edge.Source} -- {edge.Destination}  Weight: {edge.Weight}");
        }

        // Print the total weight of the minimum spanning tree
        Console.WriteLine("Total Minimum Spanning Weight: " + totalWeight);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int vertices = 6;
        KruskalAlgorithm graph = new KruskalAlgorithm(vertices);

        // Add the edges of the graph
        graph.AddEdge(0, 1, 4);
        graph.AddEdge(0, 2, 3);
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(1, 3, 2);
        graph.AddEdge(2, 3, 4);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 5, 6);

        // Generate and print the minimum spanning tree
        graph.GenerateMST();
    }
}
