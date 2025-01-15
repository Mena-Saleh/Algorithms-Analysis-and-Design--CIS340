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
discovery_time = {}
finish_time = {}
parent = {}

time = 0

def dfs_start(graph, s):
    global time
    # Setting up my start node
    visited.add(s)
    time = time + 1
    discovery_time[s] = time

    
    for neighbor in graph[s]:
        if neighbor not in visited:
            # Go deeper after setting the parent
            parent[neighbor] = s
            
            dfs_start(graph, neighbor)
    time += 1       
    finish_time[s] = time               
            
def dfs(graph):
    for node in graph.keys():
        if node not in visited:
            parent[node] = None
            dfs_start(graph, node)


dfs(graph)

print("Discovery times:", discovery_time)
print("Finish Times:", finish_time)
print("Parents:", parent)
