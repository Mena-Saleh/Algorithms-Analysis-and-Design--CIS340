using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{

    public static class PathBetweenNumbers
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given two numbers X and Y, find the min number of operations to convert X into Y
        /// Allowed Operations:
        /// 1.	Multiply the current number by 2 (i.e. replace the number X by 2 × X)
        /// 2.	Subtract 1 from the current number (i.e. replace the number X by X – 1)
        /// 3.	Append the digit 1 to the right of current number (i.e. replace the number X by 10 × X + 1).
        /// </summary>
        /// <param name="X">start number</param>
        /// <param name="Y">target number</param>
        /// <returns>min number of operations to convert X into Y</returns>
        public static int Find(int X, int Y) // RTF -> Shortest path in unweighted graph which means BFS is most suitable.
        {
            // Initialization
            Queue<int> BfsQueue = new Queue<int>();
            HashSet<int> Visited = new HashSet<int>(); // For faster lookup.
            int Count = 0; // Represents level and also how much operations X passes through to get to Y.
            int Operation1, Operation2, Operation3;
            BfsQueue.Enqueue(X);

            // Modified BFS algorithm:
            while (BfsQueue.Count > 0)
            {
                // Another approach would be to omit the for loop and have a dictionary of <int, int> that stores if a node is visitedor not,
                // and the level at it (steps count) but that approach has higher complexity.

      
                    int CurrentNumber = BfsQueue.Dequeue();
                    if (CurrentNumber == Y) // Y is reached at this distance 
                    {
                        return Count;
                    }
                    Operation1 = CurrentNumber * 2; // 1st operation
                    Operation2 = CurrentNumber - 1; // 2nd operation
                    Operation3 = 10 * CurrentNumber + 1; // 3rd operation

                    // For each operation, if it is a new one, register its distance and queue it.
                    if (CurrentNumber < Y && !Visited.Contains(Operation1)) // Make sure it is smaller than Y because if it is already bigger, there is no need to traverse Operation1
                    {
                        Visited.Add(Operation1);
                        BfsQueue.Enqueue(Operation1);

                    }
                    if (!Visited.Contains(Operation2))
                    {
                        Visited.Add(Operation2);
                        BfsQueue.Enqueue(Operation2);

                    }
                    if (CurrentNumber <= Y / 10 && !Visited.Contains(Operation3)) // Make sure curr is at most Y/10 because otherwise traversing Operation3 is pointless and wastes time.
                    {
                        Visited.Add(Operation3);
                        BfsQueue.Enqueue(Operation3);
                    }
                }
                Count++; // One level up.
            
            // Since function must return, and if it didn't return so far, this means no solution exists, it will return -1.
            return -1;
        }
        #endregion

    }
}
