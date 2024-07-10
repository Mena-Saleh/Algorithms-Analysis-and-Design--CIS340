using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Insertion_Sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Random list:
            List<double> list = GenerateRandomList(10000000);


            //Try sorting algorithm here:
            Stopwatch timer = new Stopwatch();
            timer.Start();
            MergeSort(list);
            timer.Stop();

            //Print result:
            //foreach (double item in list)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();

            Console.WriteLine("Elapsed time to sort is " + timer.ElapsedMilliseconds + "ms");

            //Checking algorithm performance on a sorted list:
            timer.Restart();
            QuickSort(list);
            timer.Stop();

            Console.WriteLine("Elapsed time on a sorted list is " + timer.ElapsedMilliseconds + "ms");

            Console.ReadLine();
        }




        //Best case -> O[n], the list is sorted already.
        //Worst case -> O[n^2], the inner loop is always executed for almost n times (list is sorted descendingly).
        //If the list is almost sorted (I.E: one or few elements are out of order) Insertion sort is a great choice because it is almost O[n]
        //In place
        //Fastest sort on small inputs, N < 50
        public static void InsertionSort(List<double> toSort)
        {
            double current;
            int j;
            for (int i = 1; i < toSort.Count; i++)
            {
                current = toSort[i]; //Store current list item to be positioned in a sorted manner.
                j = i; //Determines the new position of the current list item.
                while (j - 1 >= 0 && current < toSort[j - 1])
                {
                    toSort[j] = toSort[j - 1];
                    j--;
                }
                toSort[j] = current;
            }
        }


        //Always O(n^2)
        //In place
        //Works well for small inputs but otherwise not the best choice
        //Does around N swaps
        public static void SelectionSort(List<double> toSort) {
            double min;
            int minIndex = 0;
            for (int i = 0; i < toSort.Count; i++)
            {
                min = double.MaxValue;
                for (int j = i; j < toSort.Count; j++) //Get the minimum element
                {
                    if (toSort[j] <= min)
                    {
                        min = toSort[j];
                        minIndex = j;
                    }
                }
                Swap(toSort, i, minIndex); //Put the minimum element in place.
            }
        }


        //Always O(N^2) except when sorted it is O(N) (Because of adding the swapped flag to detect swaps and exit early)
        //Swaps a lot (around N^2 swaps in the worst case)

        public static void BubbleSort(List<double> toSort) {

            bool swapped = true;
            int i = 0;
            while (swapped)
            {
                swapped = false;
                for (int j = 0; j < toSort.Count - 1 - i; j++) // - i because each iteration one element is bubbled towards the end, -1 so it doesn't go out of bounds.
                {
                    if (toSort[j] > toSort[j+1])
                    {
                        Swap(toSort, j, j + 1);
                        swapped= true;
                    }
                }
                i++;
            }
        }



        //Divide and Conquer Approaches:


        //Easily parallelized
        //O(Nlog(N)) Time and O(N) Space Complexity (out place).
        public static void MergeSort(List<double> toSort) {
            MergeSort(toSort, 0, toSort.Count - 1);
        }

        public static void MergeSort(List<double> toSort, int start, int end, bool parallelize = false) 
        {
            if (start < end)
            {
                int middle = (start + end) / 2;
                if (parallelize) // Use multiple threads:
                {
                    Task t1 = Task.Run(()=> {
                        MergeSort(toSort, start, middle);
                    });

                    Task t2 = Task.Run(() =>
                    {
                        MergeSort(toSort, middle + 1, end);
                    });

                    Task.WaitAll(t1,t2);
                }
                else
                {
                    MergeSort(toSort, start, middle, false);
                    MergeSort(toSort, middle + 1, end, false);
                }

                Merge(toSort, start, middle, end);
            }
   
        }

        public static void Merge(List<double> toSort, int start, int middle, int end)
        {
            //Get the left and right (Two lists to merge)
            List<double> left = toSort.GetRange(start, middle - start + 1);
            List<double> right = toSort.GetRange(middle + 1, end - middle);

            //Trick to make copying the remaining parts of the two lists intuitive:
            left.Add(double.MaxValue);
            right.Add(double.MaxValue);

            int i = 0; int j = 0;
            for (int k = start; k <= end; k++)
            {

                if (left[i] <= right[j])
                {
                    toSort[k] = left[i];
                    i++;
                }
                else
                {
                    toSort[k] = right[j];
                    j++;
                }
            }

        }
      

        //O(NLogN) in most cases
        //Worst case is a sorted list and it is O(N^2), but it can be remedied by choosing a random pivot
        //In place, no extra storage needed
        public static void QuickSort(List<double> toSort) {
            QuickSort(toSort, 0, toSort.Count - 1);
        }


        public static void QuickSort(List<double> toSort, int start, int end) {
            if (start < end)
            {
                int splitPoint = RandomizedPartition(toSort, start, end);
                QuickSort(toSort, start, splitPoint - 1);
                QuickSort(toSort, splitPoint + 1, end);
            }
        
        }


        //Randomize to avoid having a worst case split (the pivot is place as first element or last which results in O(N^2) complexity)
        public static int RandomizedPartition(List<double> toSort, int start, int end)
        {
            Random random = new Random();
            int randomIndex = random.Next(start, end + 1);

            Swap(toSort, randomIndex, start);

            return Partition(toSort, start, end);
        }

        public static int Partition(List<double> toSort, int start, int end)
        {
            double pivotValue = toSort[start];
            int left = start + 1;
            int right = end;
            bool isDone = false;
            while (!isDone) {
                while (left <= right && toSort[left] <= pivotValue) left++;
                while (left <= right && toSort[right] >= pivotValue) right--;
                if (right < left) isDone = true;
                else Swap(toSort, left, right);
            }

            Swap(toSort, start, right);
            return right;
        }

        //Helper functions:
        public static void Swap(List<double> list, int i, int j) { 
        
            double temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }



        //Generates n random numberes each within 1 -> 1000.
        public static List<double> GenerateRandomList(int n)
        {
            Random random = new Random();
            List<double> randomList = new List<double>(n);

            for (int i = 0; i < n; i++)
            {
                double randomValue = random.NextDouble() * (1000 - 1) + 1;
                randomList.Add(Math.Floor(randomValue));
            }

            return randomList;
        }

    }
}
