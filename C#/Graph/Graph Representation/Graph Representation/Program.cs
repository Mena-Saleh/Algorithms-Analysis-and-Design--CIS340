using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {

        // Adjacency list:

        Console.WriteLine("Adjacency List Graph:");

        Graph_L graph = new Graph_L(4);
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 3);

        // Print it
        int counter = 0;
        foreach (List<int> list in graph.adjacencyList)
        {
            Console.Write(counter + "--> ");
            foreach (int item in list)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
            counter++;
        }


        // Adjacency Matrix:


        Console.WriteLine();
        Console.WriteLine("Adjacency Matrix");

        Graph_M graph_2 = new Graph_M(4);
        graph_2.AddEdge(1, 3);
        graph_2.AddEdge(1, 0);
        graph_2.AddEdge(2, 3);
        graph_2.AddEdge(3, 0);

        for (int i = 0; i < graph_2.adjacencyMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < graph_2.adjacencyMatrix.GetLength(1); j++)
            {
                Console.Write(graph_2.adjacencyMatrix[i,j] + ", ");
            }
            Console.WriteLine();
        }
    }
}

// Adjacency list:
public class Graph_L
{
    public List<List<int>> adjacencyList;
    public int numVertices;

    public Graph_L(int vertices)
    {
        numVertices = vertices;
        adjacencyList = new List<List<int>>(vertices);
        for (int i = 0; i < vertices; i++)
        {
            adjacencyList.Add(new List<int>());
        }
    }

    public void AddEdge(int source, int destination) // could add weight here as well, the list is going to be a list<pair<int,int>> the vertex num and weight.
    {
        adjacencyList[source].Add(destination);
        adjacencyList[destination].Add(source); // If the graph is undirected, add this line
    }
}


// Adjacency Matrix:
public class Graph_M
{
    public int[,] adjacencyMatrix;
    public int numVertices;

    public Graph_M(int vertices)
    {
        numVertices = vertices;
        adjacencyMatrix = new int[vertices, vertices];
    }

    public void AddEdge(int source, int destination) // Make it weighted by taking weight as paramter and adding it instead of 1
    {
        adjacencyMatrix[source, destination] = 1;
        adjacencyMatrix[destination, source] = 1; // If the graph is undirected, add this line
    }
}
