arr = [9, 8, -1, 2, -3, -5, 7]
# output = [9, 8, 2, 7, -1, -3, -5]

# Required to seperate positive and negative numbers without changing their order or using extra space


def modified_insertion_sort(arr): # O(N^2) time and O(1) space
    for i in range (1, len(arr)):
        key = arr[i]
        if key < 0:
            continue
        j = i - 1
        while j >= 0 and arr[j] < 0:
            arr[j+1] = arr[j]
            j-= 1
        arr[j + 1] = key
        
    return arr
    

print(modified_insertion_sort(arr))