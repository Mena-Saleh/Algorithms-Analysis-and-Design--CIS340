# Fractional knapsack is a variation of the knapsack problem in which the thief can take any amount (any fraction) of the avaialble items.

capacity = 15
items = [(1,12,4), (2,2,2), (3,1,10), (4,1,2)] # List of tuples, each tuple is (index, quantity, price) -> (Kgs, USD)

# Greedy algorithm where the greedy choice is highest price per unit. O(N.Log(N))
def fractional_knapsack(capacity, items):
    
    # Sort by unit price (USD/kgs) in reverse order (highest first)
    items = sorted(items, key= lambda x: x[2]/ x[1], reverse= True)
    print(items)
    selected_items = []
    selected_value = 0
    
    for item in items:
        if capacity == 0:
            break
        weight_to_take = min(item[1], capacity) # Take as much of the most expensive time as possible (bounbed by how much you can carry and avaialable item units)
        selected_items.append((item[0], weight_to_take)) # Add item index and how much units were taken
        selected_value += weight_to_take * (item[2]/item[1]) # Add its value to see maximum value after, value = amount taken * unit price
        capacity -= weight_to_take # Deduct weight from capacity
        
    return selected_items, selected_value
            

selected_items, selected_value = fractional_knapsack(capacity, items)

print('Max value is', selected_value)
for index, amount in selected_items:
    print(f"Taken {amount} Kgs of item {index}")