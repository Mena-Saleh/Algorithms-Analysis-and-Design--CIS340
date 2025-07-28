import random
# input: unsorted arr, num of elems to remove
# output: min and max sums after removing any k elems
arr = [3,8,1,4,2,9,3,2,5,29,32]
k = 3

# N.Log(N)
def brute_force(arr, k):
    arr.sort()
    return sum(arr[:-k]), sum(arr[k:]) # Min, Max
    
    

# Average case O(N), worst case is O(N^2), mitigated by using random pivots
def quick_select(arr, k, start, end):
    if start == end:
        return
    
    pivot_ind = random.randint(start, end)  # Random pivot
    pivot_val = arr[pivot_ind]
    
    arr[start], arr[pivot_ind] = arr[pivot_ind], arr[start]  # Move pivot to start
    
    # Partitioning
    left = start + 1
    right = end
    
    while left <= right:
        while left <= right and arr[left] <= pivot_val:
            left += 1
        while left <= right and arr[right] >= pivot_val:
            right -= 1
        if left < right:
            arr[left], arr[right] = arr[right], arr[left]
    
    # Swap pivot back to its correct position
    arr[start], arr[right] = arr[right], arr[start]
    
    if right == k:
        return
    elif right > k:
        quick_select(arr, k, start, right - 1)
    else:
        quick_select(arr, k, right + 1, end)


# O(N) solution using QuickSelect
def solve(arr, k):
    # Find min sum by removing k largest elements
    quick_select(arr, len(arr) - k, 0, len(arr) - 1)
    min_sum = sum(arr[:-k]) 
    
    # Find max sum by removing k smallest elements
    quick_select(arr, k, 0, len(arr) - 1)
    max_sum = sum(arr[k:]) 

    return min_sum, max_sum



print(brute_force(arr,k))
print(solve(arr, k))