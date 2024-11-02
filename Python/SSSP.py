import heapq

# Dijkstra's Algorithm with Priority Queue and Parent Tracking
def dijkstra(graph, start):
    # Number of vertices
    n = len(graph)

    # Initialize distances to infinity, except for the start node
    distances = [float('inf')] * n
    distances[start] = 0

    # Initialize parent array to store the path
    parents = [-1] * n  # -1 means no parent (i.e., start node)

    # Priority queue: (distance, vertex)
    pq = [(0, start)]  # Start with the source node at distance 0

    while pq:
        current_distance, current_vertex = heapq.heappop(pq)

        # Explore neighbors of the current vertex
        for neighbor, weight in graph[current_vertex]:
            distance = current_distance + weight

            # Only consider this path if it's better
            if distance < distances[neighbor]:
                distances[neighbor] = distance
                parents[neighbor] = current_vertex  # Update parent
                heapq.heappush(pq, (distance, neighbor))

    return distances, parents

# Function to print the shortest path from the start node to a target node
def print_path(parents, target):
    path = []
    while target != -1:
        path.append(target)
        target = parents[target]
    print(" -> ".join(map(str, path[::-1])))

# Example graph as an adjacency list
# graph[node] = [(neighbor, weight), ...]
graph = {
    0: [(1, 4), (2, 1)],
    1: [(3, 1)],
    2: [(1, 2), (3, 5)],
    3: []
}

# Running Dijkstra's Algorithm from source node 0
start_node = 0
distances, parents = dijkstra(graph, start_node)

# Print the shortest distances from the start node to every other node
print("Shortest distances from node", start_node, ":")
for i, d in enumerate(distances):
    print(f"Node {i} : Distance {d}")
    print("Path: ", end="")
    print_path(parents, i)
