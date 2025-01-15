# Item i has weight and values at weight[i] and value[i]
weight = [10,5,10,15]
value = [20,15,10,10]
# Maximize value while making sure it fits capacity, each item can be either taken or not, hence the name: 0/1 knapsack.


# Prefix solution, taking items from the end at i = n
memo = {}
def topDown(capacity, i):
    if i == -1 or capacity == 0:
        return 0
    if (capacity, i) in memo:
        return memo[(capacity, i)]
    sol_1 = -1
    if weight[i] <= capacity:
        sol_1 = topDown(capacity-weight[i], i-1) + value[i]
        
    sol_2 = topDown(capacity, i-1)
    
    memo[(capacity, i)] = max(sol_1, sol_2)
    return memo[(capacity, i)]
        
    
print(topDown(40, len(weight) - 1))
    
    
def bottomUp(capacity):
    dp = [[0] * (len(weight) + 1) for _ in range(capacity + 1)]
    for i in range(1, capacity + 1):
        for j in range(1, len(weight) + 1):            
            if weight[j-1] > i:
                dp[i][j] = dp[i][j-1]
                
            else:
                dp[i][j] = max(value[j-1] + dp[i-weight[j-1]][j-1], dp[i][j-1])
    print(dp)
    return dp[capacity][len(weight)]


                
print(bottomUp(40))
