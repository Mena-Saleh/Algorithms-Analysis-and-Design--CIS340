n = 8
prices = [1, 5, 8, 9, 10, 17, 17, 20]
# Sol -> 22 (two peices of size 6 and 2)

memo = {}
def topDown(size):
    if size == 0:
        return 0
    if size in memo:
        return memo[size]
    sol = -1
    for i, p in enumerate(prices):
        cut_length = i + 1
        if cut_length <= size:
            sol = max(sol, p + topDown(size - cut_length))
    memo[size] = sol
    return sol

print(topDown(n))
    