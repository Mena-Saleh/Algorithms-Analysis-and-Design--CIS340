activities = [(1,1,4), (2,3,5), (3,0,6), (4,5,7), (5,3,8), (6,5,9), (7,6,10),(8,8,11),(9,8,12), (10,2,13), (11,12,14)] # List of tuples, (index, start_time, finish_time)


# Greedy algorithm to maximize number of activities in one hall, greedy choice is earliest finish time -> O(N.Log(N))
def activity_scheduling(activities):
    activities = sorted(activities, key = lambda x: x[2])
    
    selected_activities = []
    last_finish_time = 0
    
    for activity in activities:
        # If it starts after my last finish time (compatible with last selected)
        if activity[1] >= last_finish_time:
            selected_activities.append(activity[0]) # Add its index
            last_finish_time = activity[2] # Update last finish
            
            
    return selected_activities
        


selected = activity_scheduling(activities)
print(selected)