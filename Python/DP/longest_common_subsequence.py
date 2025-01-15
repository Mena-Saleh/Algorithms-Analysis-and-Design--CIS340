x = 'HIEROGLYPHOLOGY'
y = 'MICHAEL ANGELO'
import numpy as np

memo = dict()
def LCS_memo(x, y, i, j):
    if (i, j) in memo:
        return memo[(i, j)]
    if i == 0 or j == 0:
        memo[(i, j)] = 0
        return 0
    
    # Current characters are equal
    if x[i - 1] == y[j - 1]:
        sol = 1 + LCS_memo(x, y, i - 1, j - 1)
    else:
        # Trying two choices (removing a char from either string)
        choice_1 = LCS_memo(x, y, i - 1, j)
        choice_2 = LCS_memo(x, y, i, j - 1)
        sol = max(choice_1, choice_2)
    
    # Store the result in memo
    memo[(i, j)] = sol
    return sol

print(LCS_memo(x,y, len(x), len(y)))



backtrack_table = np.zeros((len(x)+1, len(y)+1), int) # 1 means its from the left (i-1 ,j), 2 means its from above (i, j-1), and 3 means its diagonal (i-1, j-1)

def LCS_table(x, y):
    n = len(x)
    m = len(y)
    
    dp = np.zeros((n+1, m+1), int)
    
    for i in range(n + 1):
        for j in range(m + 1):
            if i == 0 or j == 0:
                dp[i][j] = 0
            elif x[i-1] == y[j-1]:
                dp[i][j] = 1 + dp[i-1][j-1]
                backtrack_table[i][j] = 3
            else:
                choice_1 = dp[i-1][j]
                choice_2 = dp[i][j-1]
                if choice_1 > choice_2:
                    backtrack_table[i][j] = 1
                    dp[i][j] = choice_1
                else:
                    backtrack_table[i][j] = 2
                    dp[i][j] = choice_2
                
    return dp[n][m]


def backTrack(x, y, n,m, sol = ""):
    if n == 0 or m == 0:
        return sol
    if backtrack_table[n][m] == 3:
        sol = backTrack(x, y, n-1, m-1, sol)
        sol = sol + x[n-1]
    if backtrack_table[n][m] == 2:
        sol = backTrack(x, y, n, m-1, sol)
    if backtrack_table[n][m] == 1:
        sol = backTrack(x, y, n-1, m, sol)
    return sol

    


print(LCS_table(x,y))
print(backTrack(x,y,len(x), len(y)))