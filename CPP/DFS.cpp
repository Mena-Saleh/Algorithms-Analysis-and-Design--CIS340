#include <iostream>
#include <vector>
#include <unordered_map>
#include <unordered_set>

using namespace std;

// Define the graph using an adjacency list
class Graph
{
public:
    unordered_map<int, vector<int>> adjList;

    // Add an edge to the graph
    void addEdge(int u, int v, bool isDirected = false)
    {
        adjList[u].push_back(v); // Add an edge from u to v
        if (!isDirected)
        {
            adjList[v].push_back(u); // For undirected graphs, add v to u's list
        }
    }

    // Perform DFS
    void dfs(int start)
    {
        unordered_set<int> visited; // Track visited nodes
        cout << "DFS Traversal: ";
        dfsHelper(start, visited);
        cout << endl;
    }

private:
    // Recursive helper function for DFS
    void dfsHelper(int node, unordered_set<int> &visited)
    {
        // Mark the current node as visited and print it
        visited.insert(node);
        cout << node << " ";

        // Recur for all unvisited neighbors
        for (int neighbor : adjList[node])
        {
            if (visited.find(neighbor) == visited.end())
            {
                dfsHelper(neighbor, visited);
            }
        }
    }

    // Print the adjacency list
public:
    void printGraph()
    {
        for (auto &[node, neighbors] : adjList)
        {
            cout << node << ": ";
            for (auto &neighbor : neighbors)
            {
                cout << neighbor << " ";
            }
            cout << endl;
        }
    }
};

int main()
{
    Graph g;

    // Add edges to the graph (example graph)
    g.addEdge(1, 2);
    g.addEdge(1, 3);
    g.addEdge(2, 4);
    g.addEdge(2, 5);
    g.addEdge(3, 6);
    g.addEdge(3, 7);

    // Print the adjacency list
    g.printGraph();

    // Perform DFS starting from node 1
    g.dfs(1);

    return 0;
}
