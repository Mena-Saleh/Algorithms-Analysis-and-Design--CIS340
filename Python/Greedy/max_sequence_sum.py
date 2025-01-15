arr = [31, -41, 59, 26, -53, 58, 97, -93, -23, 84]
# output =  59 + 26 + -53 + 58 + 97

# Divide and conquer approach, O(NlogN) time.
def max_sequence_sum_dc(arr, start, end): # Returns (seq beginning, seq end, sum)
    if start == end:
        return (start, end, arr[start])
    
    mid = (start + end) // 2
    (left_start, left_end, left_max) = max_sequence_sum_dc(arr, start, mid)
    (right_start, right_end, right_max) = max_sequence_sum_dc(arr, mid + 1, end)
    (mid_start, mid_end, mid_max) = calculate_mid_max(arr, start, mid, end)

    # Return the maximum of the three segments based on the sum value
    if left_max >= right_max and left_max >= mid_max:
        return (left_start, left_end, left_max)
    elif right_max >= left_max and right_max >= mid_max:
        return (right_start, right_end, right_max)
    else:
        return (mid_start, mid_end, mid_max)
       
def calculate_mid_max(arr, start, mid, end):
    # Get max sum going to the left from the midpoint
    left_sum = 0
    left_max = float('-inf')
    left_max_ptr = mid

    for i in range(mid, start - 1, -1):
        left_sum += arr[i]
        if left_sum > left_max:
            left_max = left_sum
            left_max_ptr = i

    # Get max sum going to the right from the midpoint + 1
    right_sum = 0
    right_max = float('-inf')
    right_max_ptr = mid + 1

    for i in range(mid + 1, end + 1):
        right_sum += arr[i]
        if right_sum > right_max:
            right_max = right_sum
            right_max_ptr = i

    return (left_max_ptr, right_max_ptr, left_max + right_max)

print(max_sequence_sum_dc(arr, 0, len(arr) - 1))





# Greedy algorithm of order O(N)
def max_sequence_sum_greedy(arr):
    # The answer cannot be lower than 0
    current_sum = 0
    max_sum = 0
    for num in arr:
        current_sum += num
        current_sum = max(0, current_sum) # Greedy choice, is the sum at some point becomes negative, restart the local current sum to 0.
        max_sum = max(max_sum, current_sum)

    return max_sum
    
    
print (max_sequence_sum_greedy(arr))


# Kadane's algorithim variant (Works if you have to at least have a sub arary of length_1)
def max_sequence_sum_greedy_kadane(self, nums: list[int]) -> int:
    current_sum = nums[0]
    max_sum = nums[0]
    for i in range(1, len(nums)):
        current_sum = max(nums[i], current_sum + nums[i])
        max_sum = max(current_sum, max_sum)
    return max_sum
