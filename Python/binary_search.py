arr = [1, 3, 6, 8, 20, 33, 90, 130, 200, 290, 300, 340, 500, 540, 560, 700, 1000, 1045, 1088]


# Recurisve approach
def binary_search_recursive_main(arr, item, start, end):
    if start > end:
        return "Not found"
    
    mid = (start + end) // 2
    
    if item == arr[mid]:
        return f"Found the item {item} at index {mid}"
    elif item > arr[mid]:
        return binary_search_recursive_main(arr, item, mid + 1, end)
    else:
        return binary_search_recursive_main(arr, item, start, mid - 1)
    
    
def binary_search_recursive(arr, item):
    return binary_search_recursive_main(arr, item, 0, len(arr) - 1)
    
# Iterative approach
def binary_search_iterative(arr, item):
    start = 0
    end = len(arr) - 1
    
    while(start <= end):
        mid = (start + end) // 2
        if item == arr[mid]:
            return f"Found the item {item} at index {mid}"
        elif item > arr[mid]:
            start = mid + 1
        else:
            end = mid -1 
    
    return "Not Found"
    
    
res = binary_search_recursive(arr, 1088)
print(res)


'''
Analysis:

Time: O(log(N))
Space: O(1)


Notes:
Requires array to be sorted.
'''