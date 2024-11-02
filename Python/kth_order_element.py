import random

arr = [3, 4, 90, 10 ,99, 500, 300, 200, 123 , 12 ,94, 2, 23, 54, 56]

def select_kth_order_element(arr, start, end, order):
    if order > end:
        return 'out of range'
    if start >= end:
        return f'The element is {arr[start]}'
    # Select pivot
    pivot_index = random.randint(start, end)
    pivot_value = arr[pivot_index]
    
    # Put the pivot in first place
    arr[start], arr[pivot_index] = arr[pivot_index], arr[start]
    
    # Place pivot in right spot
    left = start + 1
    right = end
    while left <= right:
        while left <= right and arr[left] <= pivot_value:
            left += 1
        while left <= right and arr[right] >= pivot_value:
            right -= 1
        
        # Swap left and right
        if left < right:        
            arr[left], arr[right] = arr[right], arr[left]
        
    # Swap pivot and right
    arr[start], arr[right] = arr[right], arr[start]
    
    # Divide based on where the pivot is
    if right == order:
        return f'The element is {arr[right]}'
    elif right > order:
        return select_kth_order_element(arr, start, right-1, order)
    else:
        return select_kth_order_element(arr, right+1, end, order)
    
    
 
order = 14
res = select_kth_order_element(arr, 0, len(arr) - 1, order)
print(res)


print('The sorted arr: ', sorted(arr))    
print(f'So the {order} element is actually {arr[order]}')    
