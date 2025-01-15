# Stacks _> LIFO
print("-- STACKS --")

stack = []

# Push
stack.append(1)
stack.append(2)
stack.append(3)


print(stack)

# Pop
print(stack.pop())

# Top
print(stack[-1])



# Queues -> FIFO
print("-- QUEUES --")
from collections import deque

queue = deque()

# Push back
queue.append(1)
queue.append(2)
queue.append(3)


print(queue)

# Pop front or left
print(queue.popleft())

# Getting the front
print(queue[0])



# HASH SETS 

print("-- HASH SETS --")

hSet = set()
hSet.add(1)
hSet.add(1)
hSet.add(2)
hSet.add(2)
hSet.add(3)

print(hSet)


# Checking if it contains an element (O(1))
print(1 in hSet)
print("Aloha" in hSet)

hSet.remove(1)
print(hSet)




# Dictionaries (maps or hash maps or unordered maps)
print("-- DICTIONARIES --")
hMap = dict() # or defaultdict(int) or {}
hMap['a'] = 1
hMap['b'] = 3
hMap['c'] = 4

print(hMap)

# Contains, or even retrieval is O(1)
print('a' in hMap)

# Get keys or values
print(hMap.keys())
print(hMap.values())


# Updating a key's value or initialize its default

hMap['a'] = hMap.get('a', 0) + 1 # Finds a with value 1 and increments it to 2, hMap['a'] += 1 can also be done here
hMap['x'] = hMap.get('x', 0) + 1 # Doesn't find x so it initializes it to 0 and adds 1

print(hMap)



# HEAPS
print('-- HEAPS --')

import heapq
# heapq implements min heap by default, we can make it max heap by flipping signs of inserted values

items = [3,6,1,5,8,2] 


# Heapify a list to make a min heap out of it

heapq.heapify(items) # O(N) heapifying entire unsorted array (heapifying all none leaf nodes) takes O(N) but one node only is O(log(N)) which is done in insertion and deletion

print(items)


# To get the min element
print(items[0])


# Insertion (inserts at end of array then heapify up to restore heap property)
heapq.heappush(items, 20)
heapq.heappush(items, -1)

print(items)

# Deletion, deletion is only done efficiently for root node (the min)
heapq.heappop(items)
print(items[0])

# Priority queues implemented using heap too
print("-- PRIORITY QUEUES --")

# Creating an empty priority queue
priority_queue = []

# Adding elements to the priority queue
heapq.heappush(priority_queue, (1, 'Task with priority 1'))  # Priority 1
heapq.heappush(priority_queue, (3, 'Task with priority 3'))  # Priority 3
heapq.heappush(priority_queue, (2, 'Task with priority 2'))  # Priority 2

# Getting the element with the highest priority (smallest number)
highest_priority = heapq.heappop(priority_queue)
print(highest_priority) 

# The next highest priority element can be accessed in the same way:
next_highest_priority = heapq.heappop(priority_queue)
print(next_highest_priority) 