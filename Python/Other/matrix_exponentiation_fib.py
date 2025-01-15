# Dot product of two 2x2 matrices
def matrix_multiply(A, B):
    return [
        [
            A[0][0] * B[0][0] + A[0][1] * B[1][0],
            A[0][0] * B[0][1] + A[0][1] * B[1][1]
        ],
        [
            A[1][0] * B[0][0] + A[1][1] * B[1][0],
            A[1][0] * B[0][1] + A[1][1] * B[1][1]
        ]
    ]


# Fast power in log(N)
def matrix_exponentiation(matrix, power):
    result = [[1, 0], [0, 1]]  # Identity matrix
    while power > 0:
        if power % 2 == 1:
            result = matrix_multiply(result, matrix)
        matrix = matrix_multiply(matrix, matrix)
        power //= 2
    return result

# Solving for the matrix expo equation
def fibonacci(n):
    if n == 0:
        return 0
    if n == 1:
        return 1
    matrix = [[1, 1], [1, 0]]
    result = matrix_exponentiation(matrix, n - 1)
    return result[0][0]

print(fibonacci(10))  # Output: 55
