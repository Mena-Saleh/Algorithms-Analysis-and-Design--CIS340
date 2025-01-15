#include <iostream>
#include <bits/stdc++.h>

using namespace std;

vector<int> merge(vector<int> left, vector<int> right)
{
    vector<int> result;
    int pl = 0, pr = 0;
    while (pl < left.size() && pr < right.size())
    {
        if (left[pl] < right[pr])
        {
            result.push_back(left[pl]);
            pl++;
        }
        else
        {
            result.push_back(right[pr]);
            pr++;
        }
    }

    while (pl < left.size())
    {
        result.push_back(left[pl]);
        pl++;
    }

    while (pr < right.size())
    {
        result.push_back(right[pr]);
        pr++;
    }

    return result;
}

vector<int> mergeSort(vector<int> &v, int start, int end)
{
    if (start >= end)
        return {v[start]};

    int mid = start + (end - start) / 2;

    vector<int> left = mergeSort(v, start, mid);
    vector<int> right = mergeSort(v, mid + 1, end);

    return merge(left, right);
}

int main()
{
    vector<int> arr = {9, 32, 8, 1, 2, 7, 9, 3, 2, 65, 72, 6, 1, 39, 5, 50, 21};

    arr = mergeSort(arr, 0, arr.size() - 1);

    for (auto x : arr)
        cout << x << " ";
}