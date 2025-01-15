import numpy as np

arr = np.random.randint(1, 10000, 20000)


def bubble_sort(arr: list):
    n = len(arr)
    for i in range(n):
        swapped = False
        for j in range(n - i - 1):
            # If out of order, swap them (or bubble them)
            if (arr[j] > arr[j + 1]):
                arr[j], arr[j+1] = arr[j+1], arr[j]
                swapped = True
        
        # Optimization, if in one pass no swaps happened, then it is sorted and no need to further exhaust resources
        if not swapped:
            break
    return arr


print(bubble_sort(arr))


'''
Analysis:
Time: O(N^2)
Space: O(1)

Notes:
Brute force sorting algorithm and usually not good in performance
'''