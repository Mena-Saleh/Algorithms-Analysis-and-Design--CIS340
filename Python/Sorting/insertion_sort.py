import numpy as np

arr = np.random.randint(1, 10000, 2000)


def insertion_sort(arr):
    for i in range (1, len(arr)):
        key = arr[i]
        j = i - 1
        while j >= 0 and key < arr[j]:
            arr[j+1] = arr[j]
            j-= 1
        arr[j + 1] = key
        
    return arr


print(insertion_sort(arr))

'''
* Analysis

Time:

- Best case: O(1) in a sorted array or almost sorted array
- Worst case: O(N^2) array sorted in descending order
- Average case: O(N^2)

Space: O(1)
    
* Notes
Fastest sorting algorithm for small arrays of up to 40 elements.

'''