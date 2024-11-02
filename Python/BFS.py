from collections import deque

graph = {}

graph['a'] = ['b', 'c']
graph['b'] = ['a', 'c', 'd']
graph['c'] = ['a']
graph['d'] = ['b', 'e']
graph['e'] = ['d']

# Disconnected bit:
graph['f'] = ['z']
graph['z'] = ['f']

# Visited to mark the visited nodes and queue to achieve BFS traversal
visited = set()
dist = {}
parent = {}

def bfs_start(graph, s):
    q = deque()
    
    # Setting up my start node
    visited.add(s)
    q.append(s)
    dist[s] = 0  # Also resembles the tree level
    parent[s] = None
    
    while q:
        current = q.popleft()
        for neighbor in graph[current]:
            if neighbor not in visited:
                print(neighbor)
                # Visit that neighbor, and add it to the queue to visit its neighbors in the next level
                visited.add(neighbor)
                q.append(neighbor)
                
                # The neighbor's level is that of its parent + 1, and its parent is the one that just got popped from queue
                dist[neighbor] = dist[current] + 1
                parent[neighbor] = current
                
            
def bfs(graph):
    for node in graph.keys():
        if node not in visited:
            bfs_start(graph, node)


bfs(graph)

print("Distances:", dist)
print("Parents:", parent)
