using System;
using System.Collections.Generic;

public class Node
{
    public string Name { get; }
    public List<Node> Neighbors { get; }
    public string Color { get; set; }
    public int Depth { get; set; }
    public Node Parent { get; set; }

    public Node(string name)
    {
        Name = name;
        Neighbors = new List<Node>();
        Color = "White";
        Depth = int.MaxValue;
        Parent = null;
    }
}

public class Traverse
{
    public static void BFS(Node start)
    {
        Queue<Node> queue = new Queue<Node>();

        start.Color = "Gray";
        start.Depth = 0;
        start.Parent = null;

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            Console.WriteLine("Visiting node: " + current.Name);

            foreach (Node neighbor in current.Neighbors)
            {
                if (neighbor.Color == "White")
                {
                    neighbor.Color = "Gray";
                    neighbor.Depth = current.Depth + 1;
                    neighbor.Parent = current;
                    queue.Enqueue(neighbor);
                }
            }

            current.Color = "Black";
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create nodes
        Node nodeA = new Node("A");
        Node nodeB = new Node("B");
        Node nodeC = new Node("C");
        Node nodeD = new Node("D");
        Node nodeE = new Node("E");
        Node nodeF = new Node("F");
        Node nodeG = new Node("G");

        // Define node relationships
        nodeA.Neighbors.Add(nodeB);
        nodeA.Neighbors.Add(nodeC);
        nodeB.Neighbors.Add(nodeD);
        nodeB.Neighbors.Add(nodeE);
        nodeC.Neighbors.Add(nodeF);
        nodeE.Neighbors.Add(nodeG);

        // Perform BFS starting from nodeA
        Traverse.BFS(nodeA);
    }
}
