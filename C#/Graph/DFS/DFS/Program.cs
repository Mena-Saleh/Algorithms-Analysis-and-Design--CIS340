using System;
using System.Collections.Generic;

public enum NodeColor
{
    White,
    Grey,
    Black
}

public class Node
{
    public int Value { get; }
    public NodeColor Color { get; set; }
    public int DiscoveryTime { get; set; }
    public int FinishTime { get; set; }
    public List<Node> Neighbors { get; }

    public Node(int value)
    {
        Value = value;
        Color = NodeColor.White;
        DiscoveryTime = -1;
        FinishTime = -1;
        Neighbors = new List<Node>();
    }
}

public class DFS
{
    private int time;

    public void DepthFirstSearch(Node start)
    {
        time = 0;
        DFSVisit(start);
    }

    private void DFSVisit(Node node)
    {
        time++;
        node.DiscoveryTime = time;
        node.Color = NodeColor.Grey;

        Console.WriteLine("Visiting node: " + node.Value);

        foreach (Node neighbor in node.Neighbors)
        {
            if (neighbor.Color == NodeColor.White)
            {
                DFSVisit(neighbor);
            }
        }

        node.Color = NodeColor.Black;
        time++;
        node.FinishTime = time;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create nodes
        Node nodeA = new Node(1);
        Node nodeB = new Node(2);
        Node nodeC = new Node(3);
        Node nodeD = new Node(4);
        Node nodeE = new Node(5);

        // Define node relationships
        nodeA.Neighbors.Add(nodeB);
        nodeA.Neighbors.Add(nodeC);
        nodeB.Neighbors.Add(nodeD);
        nodeC.Neighbors.Add(nodeE);
        nodeE.Neighbors.Add(nodeB);

        DFS dfs = new DFS();
        dfs.DepthFirstSearch(nodeA);
    }
}
