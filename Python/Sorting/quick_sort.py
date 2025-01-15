import random
import numpy as np

arr = np.random.randint(1, 10000, 20000)

def quick_sort(arr, start, end):
    if start >= end:
        return
    
    # Divide
    pivot_index = random.randint(start, end)
    pivot = arr[pivot_index]
    
    # Putting the pivot in 1st place
    arr[start], arr[pivot_index] = arr[pivot_index], arr[start] 
    
    left = start + 1
    right = end
    while left <= right:
        while left <= right and arr[left] <= pivot:
            left+= 1
            
        while left <= right and arr[right] >= pivot:
            right-= 1
        
        # Swap left and right
        if left < right:
            arr[left], arr[right] = arr[right], arr[left]
    
    # Swap pivot with right, so that all elements on the left are smaller than it and those on the right are larger
    arr[start], arr[right] = arr[right], arr[start]
    
    # Conquer
    quick_sort(arr, start, right - 1)
    quick_sort(arr, right + 1, end)
    
    # No combine here
    return arr



print (quick_sort(arr, 0, len(arr) - 1))
    
    
        