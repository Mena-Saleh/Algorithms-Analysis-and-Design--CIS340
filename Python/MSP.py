# A minimum spanning is an algorithm used to find a tree that visits every vertex of a graph with lowest total cost
# Can be used for efficient network design (minimizing total cable list)
# Greedy algorithm

class SimpleUnionFind:
    def __init__(self, size):
        # Initialize the member array, each vertex is its own set
        self.member = [i for i in range(size)]

    # Make-Set operation: for each vertex, it initializes the set
    def make_set(self, v):
        self.member[v] = v

    # Find-Set operation: returns the set that v belongs to
    def find_set(self, v):
        return self.member[v]

    # Union operation: merges sets of u and v
    def union(self, u, v):
        u_set = self.find_set(u)
        v_set = self.find_set(v)
        if u_set != v_set:
            # For all members in u's set, update their membership to v's set
            for i in range(len(self.member)):
                if self.member[i] == u_set:
                    self.member[i] = v_set

class Graph:
    def __init__(self, vertices):
        self.V = vertices  # Number of vertices
        self.graph = []  # List to store all edges (weight, u, v)

    # Add edges to the graph
    def add_edge(self, u, v, w):
        self.graph.append((w, u, v))

    # Kruskal's Algorithm
    def kruskal_mst(self):
        result = []  # This will store the resulting MST
        i, e = 0, 0  # i - index for sorted edges, e - index for result[]

        # Step 1: Sort all edges in non-decreasing order of weight
        self.graph = sorted(self.graph, key=lambda item: item[0])

        # Initialize Union-Find data structure
        uf = SimpleUnionFind(self.V)

        # Step 2: Create a set for each vertex
        for v in range(self.V):
            uf.make_set(v)

        # Step 3: Process edges in sorted order
        while e < self.V - 1:
            # Step 3a: Pick the smallest edge
            w, u, v = self.graph[i]
            i += 1

            # Step 3b: Find sets of the two vertices u and v
            set_u = uf.find_set(u)
            set_v = uf.find_set(v)

            # If u and v are in different sets, include this edge in the result
            if set_u != set_v:
                e += 1
                result.append((u, v, w))
                uf.union(u, v)

        # Print the constructed MST
        print("Edges in the Minimum Spanning Tree (MST):")
        for u, v, w in result:
            print(f"{u} -- {v} == {w}")

# Example usage
g = Graph(4)  # Create a graph with 4 vertices
g.add_edge(0, 1, 10)
g.add_edge(0, 2, 6)
g.add_edge(0, 3, 5)
g.add_edge(1, 3, 15)
g.add_edge(2, 3, 4)

# Run Kruskal's algorithm to find the MST
g.kruskal_mst()



# # More optimized approach
# class Graph:
#     def __init__(self, vertices):
#         self.V = vertices  # Number of vertices
#         self.graph = []  # Store edges as (weight, u, v)

#     # Add an edge to the graph
#     def add_edge(self, u, v, w):
#         self.graph.append((w, u, v))

#     # Find function for Union-Find (using path compression)
#     def find(self, parent, i):
#         if parent[i] != i:
#             parent[i] = self.find(parent, parent[i])
#         return parent[i]

#     # Union function for Union-Find (using union by rank)
#     def union(self, parent, rank, x, y):
#         root_x = self.find(parent, x)
#         root_y = self.find(parent, y)

#         if rank[root_x] < rank[root_y]:
#             parent[root_x] = root_y
#         elif rank[root_x] > rank[root_y]:
#             parent[root_y] = root_x
#         else:
#             parent[root_y] = root_x
#             rank[root_x] += 1

#     # Kruskal's algorithm to find the MST
#     def kruskal_mst(self):
#         result = []  # Store the resultant MST
#         i, e = 0, 0  # i - index for sorted edges, e - index for result[]

#         # Step 1: Sort all edges in non-decreasing order of their weight
#         self.graph = sorted(self.graph, key=lambda item: item[0])

#         # Create parent and rank arrays for Union-Find
#         parent = []
#         rank = []

#         # Initializing each vertex to be its own parent and rank to 0
#         for node in range(self.V):
#             parent.append(node)
#             rank.append(0)

#         # Step 2: Pick the smallest edge and check if it forms a cycle
#         while e < self.V - 1 and i < len(self.graph):
#             w, u, v = self.graph[i]
#             i += 1
#             root_u = self.find(parent, u)
#             root_v = self.find(parent, v)

#             # If u and v are not in the same set, add this edge to the result
#             if root_u != root_v:
#                 e += 1
#                 result.append((u, v, w))
#                 self.union(parent, rank, root_u, root_v)

#         # Print the result
#         print("Edges in the MST:")
#         for u, v, w in result:
#             print(f"{u} -- {v} == {w}")

# # Example usage
# g = Graph(4)  # Create a graph with 4 vertices
# g.add_edge(0, 1, 10)
# g.add_edge(0, 2, 6)
# g.add_edge(0, 3, 5)
# g.add_edge(1, 3, 15)
# g.add_edge(2, 3, 4)

# # Run Kruskal's algorithm
# g.kruskal_mst()
