def fast_expo(num, pow):
    result = 1
    
    while pow:
        if pow & 1:
            result *= num
        num *= num
        pow = pow >> 1
    return result


print(fast_expo(2,10))
