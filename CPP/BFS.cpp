#include <iostream>
#include <vector>
#include <unordered_map>
#include <queue>
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

    // Perform BFS
    void bfs(int start)
    {
        queue<int> q;               // Queue for BFS
        unordered_set<int> visited; // Track visited nodes

        // Start BFS from the given node
        q.push(start);
        visited.insert(start);

        cout << "BFS Traversal: ";
        while (!q.empty())
        {
            int current = q.front();
            q.pop();
            cout << current << " ";

            // Add all unvisited neighbors to the queue
            for (int neighbor : adjList[current])
            {
                if (visited.find(neighbor) == visited.end())
                {
                    q.push(neighbor);
                    visited.insert(neighbor);
                }
            }
        }
        cout << endl;
    }

    // Print the adjacency list
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

    // Perform BFS starting from node 1
    g.bfs(1);

    return 0;
}
