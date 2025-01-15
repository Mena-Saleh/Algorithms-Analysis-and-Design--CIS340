# Recursive algorithm O(2^N)
def fib(i):
    if i <= 1:
        return i
    
    return fib(i - 1) + fib(i - 2)


# DP approach, Top-down O(N)
memo = dict()
def fib_memoization(i): 
    if i in memo:
        return memo[i]
    if i <= 1:
        memo[i] = 1
        return i
    
    sol = fib_memoization(i - 1) + fib_memoization(i - 2)
    memo[i] = sol
    return sol



# DP approach, Bottom-up O(N)
def fib_builing_table(i):
    arr = [0, 1]
    for i in range (2, i + 1):
        arr.append(arr[i-1] + arr[i-2])
        
    return arr[i]

print(fib_memoization(200))
        
        

    