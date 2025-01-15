
import numpy as np

arr = np.random.randint(1, 10000, 20000)

def merge(left: list, right: list):
    left.append(float('inf'))
    right.append(float('inf'))
    
    result = []
    left_pointer = 0
    right_pointer = 0
    while left[left_pointer] != float('inf') or right[right_pointer] != float('inf'):
        if left[left_pointer] < right[right_pointer]:
            result.append(left[left_pointer])
            left_pointer += 1
        else:
            result.append(right[right_pointer])
            right_pointer += 1
    return result

def merge_sort_main(arr, start, end):
    # Base case
    if start == end:
        return [arr[start]]
    
    # Divide
    mid = (start + end) // 2
    
    # Conquer
    left = merge_sort_main(arr, start, mid)
    right = merge_sort_main(arr, mid + 1, end)
    
    # Combine
    result = merge(left, right)
    
    return result

def merge_sort(arr):
    return merge_sort_main(arr, 0, len(arr) - 1)

print(merge_sort(arr))
