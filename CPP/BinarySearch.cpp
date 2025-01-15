#include <iostream>
#include <bits/stdc++.h>

using namespace std;


bool binarySearch(vector<int>& arr, int val)
{
    int start = 0;
    int end = arr.size() - 1;
    while(start <= end)
    {
        int mid = start + (end-start)/2;

        if(arr[mid] == val) return true;

        else if(val > arr[mid]) start = mid + 1;

        else end = mid - 1;
    }
    return false;
}


int main()
{
    vector<int> arr = {9, 32, 8, 1, 2, 7, 9, 3, 2, 65, 72, 6, 1, 39, 5, 50, 21};

    sort(arr.begin(), arr.end());
    
    cout << binarySearch(arr, 6);
    cout << binarySearch(arr, 32);
    cout << binarySearch(arr, 21);
    cout << binarySearch(arr, -5);

   
}