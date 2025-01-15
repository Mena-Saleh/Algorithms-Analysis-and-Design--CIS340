# The head starts at index 0,to get the left node, we index with 2*i+1 and right is 2*i+2
class BinarySearchTree:
    def __init__(self):
        # Initial tree size, but it expands dynamically as we go.
        self.tree = [None] * 1024
   
    def insert(self, value):
        ptr = 0
        while True:
            current_node_value = self.tree[ptr]
            
            # Check if the farthest child is out of range (right child is at index 2*i + 2)
            if 2*ptr + 1 >= len(self.tree):
                self.tree.extend([None] * len(self.tree)) # Basically double tree array if we are running out of space
                
            if current_node_value is None:
                self.tree[ptr] = value
                return
            elif value > current_node_value: # Go to right child
                ptr =2*ptr + 2
            else: # Go to left child
                ptr = 2*ptr + 1
        
    # NOT WORKING
    def delete(self, value):        
        ptr = 0
        current_node = None
        while True:
            current_node = self.tree[ptr]
            if current_node == value: # If found, break the loop
                break
            elif value > current_node: # Go right
                ptr = ptr*2 + 2
            else: # Go left
                ptr = ptr*2 + 1
                
                
            # Delete logic
            left_child = self.tree=[ptr*2 + 1]
            left_child = self.tree=[ptr*2 + 2]
            
            # Case 1: Leaf node (No children)
            if left_child is None and left_child is None:
                current_node = None
                
            # Case 2: One child (lazy deleting, but this ruins insertion and traveresal logic, fix later)
            elif left_child and not left_child:
                current_node = left_child
                left_child = "deleted"
            elif left_child and not left_child:
                current_node = left_child
                left_child = "deleted"
                
            # case 3: Two children, fnd min in right sub tree or max in left sub tree
            else:
                min_node_ptr = ptr*2 + 2
                min_node = self.tree[min_node_ptr]
                while min_node:
                    min_node = self.tree[min_node_ptr*2 + 1] # Keep going left to get the min
                
                current_node = min_node
                min_node = "deleted"
                
                
    
    def contains(self, value):
        ptr = 0
        current_node = self.tree[ptr]
        while current_node:
            if current_node == value:
                return True
            elif value < current_node:
                ptr = ptr*2 + 1
                current_node = self.tree[ptr]
            else:
                ptr = ptr*2 + 2
                current_node = self.tree[ptr]

        return False
        
    def inorder(self, root = 0):
        if self.tree[root] is None:
            return
        
        # Left
        self.inorder(root*2 + 1)
        
        # Print self
        print("Current value:", self.tree[root])
        
        # Right
        self.inorder(root*2 + 2)
    
    def preorder(self, root = 0):
        if self.tree[root] is None:
            return
        
        # Print self
        print("Current value:", self.tree[root])
        
        # Left
        self.preorder(root*2 + 1)
        
        # Right
        self.preorder(root*2 + 2)
    
    
    def postorder(self, root):
        if self.tree[root] is None:
            return
        
        # Left
        self.preorder(root*2 + 1)
        
        # Right
        self.preorder(root*2 + 2)
        
        # Print self
        print("Current value:", self.tree[root])
    
    
    
    
    
tree = BinarySearchTree()
tree.insert(30)
tree.insert(40)
tree.insert(20)
tree.insert(1)
tree.insert(5)
tree.insert(9)
tree.insert(13)
tree.insert(2)


tree.preorder()

print(tree.contains(13))
print(tree.contains(34))

# __init__(): Initialize the array and any necessary variables (like size or root index).
# insert(value): Add a value to the BST.
# delete(value): Remove a value from the BST while maintaining its properties.
# search(value): Check if a value exists in the tree.
# inorder(): Traverse the tree in an in-order fashion (left, root, right).
# preorder(): Traverse the tree in pre-order (root, left, right).
# postorder(): Traverse the tree in post-order (left, right, root).