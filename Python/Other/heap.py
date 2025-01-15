class MinHeap:
    def __init__(self):
        self.heap = []  # Initialize an empty heap

    def parent(self, index):
        return (index - 1) // 2

    def left_child(self, index):
        return 2 * index + 1

    def right_child(self, index):
        return 2 * index + 2

    def insert(self, value):
        """Insert a new value into the heap."""
        self.heap.append(value)  # Add the new value to the end
        self._heapify_up(len(self.heap) - 1)  # Restore heap property


    def pop_min(self):
        """Remove and return the smallest element (root) from the heap."""
        if len(self.heap) == 0:
            return None  # Heap is empty
        if len(self.heap) == 1:
            return self.heap.pop()  # Only one element in the heap
        
        root = self.heap[0]
        # Replace root with the last element
        self.heap[0] = self.heap.pop()
        # Restore heap property
        self._heapify_down(0)
        return root
    
    def _heapify_up(self, index):
        """Move the value at index up to maintain the heap property."""
        while index > 0 and self.heap[index] < self.heap[self.parent(index)]:
            # Swap if the current value is less than its parent
            self.heap[index], self.heap[self.parent(index)] = self.heap[self.parent(index)], self.heap[index]
            index = self.parent(index)

    def _heapify_down(self, index):
        """Move the value at index down to maintain the heap property."""
        smallest = index
        left = self.left_child(index)
        right = self.right_child(index)

        # Check if the left child is smaller
        if left < len(self.heap) and self.heap[left] < self.heap[smallest]:
            smallest = left
        
        # Check if the right child is smaller
        if right < len(self.heap) and self.heap[right] < self.heap[smallest]:
            smallest = right

        # If the smallest is not the current index, swap and continue heapifying
        if smallest != index:
            self.heap[index], self.heap[smallest] = self.heap[smallest], self.heap[index]
            self._heapify_down(smallest)

    def get_min(self):
        """Return the smallest element (root) without removing it."""
        if len(self.heap) == 0:
            return None
        return self.heap[0]

    def display(self):
        """Display the heap as a list."""
        return self.heap



# Create a MinHeap instance
min_heap = MinHeap()

# Insert values
min_heap.insert(10)
min_heap.insert(5)
min_heap.insert(3)
min_heap.insert(8)

print("Heap after inserts:", min_heap.display())  # [3, 8, 5, 10]

# Get the minimum value
print("Minimum value:", min_heap.get_min())  # 3

# Extract the minimum value
print("Extracted min:", min_heap.pop_min())  # 3
print("Heap after extraction:", min_heap.display())  # [5, 8, 10]
