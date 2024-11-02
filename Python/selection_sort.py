import numpy as np

arr = np.random.randint(1, 10000, 20000)

def selection_sort(arr: list):
    n = len(arr)
    for i in range(n):
        min_index = i
        for j in range(i + 1, n):
            # Select the minimum element in every iteration
            if arr[j] < arr[min_index]:
                min_index = j
         # Swapping the elements to sort the array
        arr[i], arr[min_index] = arr[min_index], arr[i]
        
    return arr
        
print(selection_sort(arr))

'''
Analysis:

Time: O(N^2)
Space: O(1)

Notes:
Brute force sorting algorithm and usually the worst in performance
'''