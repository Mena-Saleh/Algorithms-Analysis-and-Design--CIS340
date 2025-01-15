#include <iostream>
#include <bits/stdc++.h>
#include <random>

using namespace std;

// Custom function to get randoms reliably
int getRandomInt(int start, int end)
{
    std::random_device rd;                           // Seed for random number engine
    std::mt19937 gen(rd());                          // Mersenne Twister random number engine
    std::uniform_int_distribution<> dis(start, end); // Range inclusive

    return dis(gen);
}

void quickSort(vector<int> &arr, int start, int end)
{
    if (start >= end)
        return;

    int pivot_index = getRandomInt(start, end);
    int pivot = arr[pivot_index];

    swap(arr[start], arr[pivot_index]);

    int l = start + 1;
    int r = end;

    while (l <= r)
    {
        while (l <= r && arr[l] <= pivot)
            l++;
        while (l <= r && arr[r] >= pivot)
            r--;
        if (l < r)
            swap(arr[l], arr[r]);
    }
    swap(arr[start], arr[r]);

    quickSort(arr, start, r - 1);
    quickSort(arr, r + 1, end);
}

int main()
{
    vector<int> arr = {9, 32, 8, 1, 2, 7, 9, 3, 2, 65, 72, 6, 1, 39, 5, 50, 21};

    quickSort(arr, 0, arr.size() - 1);

    for (auto x : arr)
        cout << x << " ";
}