using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insertion_Sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<double> list = new List<double>();
            list.Add(4);
            list.Add(2);
            list.Add(7);
            list.Add(1);
            list.Add(3);

            InsertionSort(ref list);

            foreach (double item in list)
            {
                Console.WriteLine(item);
            }
         
            Console.ReadLine();
        }

        //Best case -> O[n], the list is sorted already.
        //Worst case -> O[n^2], the inner loop is always executed for almost n times (list is sorted descendingly).
        public static void InsertionSort(ref List<double> toSort)
        {
            double current;
            int j;
            //Check if the list is at least two elements.
            if (toSort.Count >= 2)
            {
                for (int i = 1; i < toSort.Count; i++)
                {
                    current = toSort[i]; //Store current list item to be positioned in a sorted manner.
                    j = i; //Determines the new position of the current list item.
                    while (j-1 >= 0 && current < toSort[j-1])
                    {
                        toSort[j] = toSort[j-1];
                        j--;
                    }
                    toSort[j] = current;
                }
            }
                      
           
        }
    }
}
