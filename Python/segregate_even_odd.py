input = [12, 34, 45, 9, 8, 90, 3]
#output = [12, 34, 8, 90, 45, 9, 3]


def segregate(arr):
    left = 0
    right = len(arr) - 1
    while left <= right:
        while left <= right and arr[left] % 2 == 0:
            left += 1
    
        while left <= right and arr[right] % 2 == 1:
            right -= 1
        
        if left < right:
            arr[left], arr[right] = arr[right], arr[left]
            
    return arr



print(segregate(input))