using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class SchoolQuizII
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the minimum number of integers whose sum equals to ‘N’
        /// </summary>
        /// <param name="N">number given by the teacher</param>
        /// <param name="numbers">list of possible numbers given by the teacher (starting by 1)</param>
        /// <returns>minimum number of integers whose sum equals to ‘N’</returns>
        /// 

        // Top-down approach (memoization)
        public static int SolveValueRecursive(int N, int[] numbers, Dictionary<int,int> SavedAnswers)
        {

            // Base case to terminate recursion
            if (N == 0)
            {
                return 0;
            }
            if (SavedAnswers.ContainsKey(N))
            {
                return SavedAnswers[N];
            }
            int Answer = int.MaxValue; // Initially largest number to be checked against to minimize when attempting all dp trials.
            for (int i = 0; i < numbers.Length; i++) // Try all numbers (careful brute force)
            {
                if (numbers[i] <= N) // Possible to subtract this number from N
                {
                    Answer = Math.Min(Answer, SolveValueRecursive(N - numbers[i], numbers, SavedAnswers) + 1); // Try all combinations and get minimum using recursion
                }
            }

            SavedAnswers[N] = Answer;
            return Answer;
        }

        // Helper function that sets up dictionary and calls main function
        public static int SolveValueRecursiveHelper(int N, int[] numbers) {

            Dictionary<int, int> SavedAnswers = new Dictionary<int, int>(); // Creating new dictionary for each problem
            return SolveValueRecursive(N, numbers, SavedAnswers); // Solving using top down memoization approach.
        }


        // Top-down is not the best option here because there is no skipped sub-problems, all sub-problems are required
        // Therefore it is better to use building table method and transform recursive code into iterative.


        // Bottom up solution (Iterative building table code)
        public static int SolveValueIterative(int N, int[] numbers, int[] Answers)
        {
            // Initialize array

            for (int i = 1; i <= N; i++)
            {
                Answers[i] = int.MaxValue;
            }

            // Iterate over all sub-problems
            for (int i = 1; i <= N; i++)
            {
                // Iterate over all possible choices at that sub-problem size
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (i >= numbers[j]) // Possible choice
                    {
                        Answers[i] = Math.Min(Answers[i], Answers[i - numbers[j]] + 1); // Optimize
                    }

                }

            }

            return Answers[N];
        }



        public static int SolveValue(int N, int[] numbers)
        {
            // Building table array.
            int[] Answers = new int[N + 1];

            return SolveValueIterative(N, numbers, Answers);
        }


        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>the numbers themselves whose sum equals to ‘N’</returns>
        /// 
        // Same as solve solution but we need to backtrack from the N and check what choices we took along the way and append them in a list.
        public static int[] ConstructSolution(int N, int[] numbers)
        {
            // Building table array.
            int[] Answers = new int[N + 1];

            // Solve the value and build the answers table.
            SolveValueIterative(N, numbers, Answers);

            // Backtrack on the Answers table that was built.
            return BackTrack(N, numbers, Answers);
        }


        // Backtracks from the end adding the choosed optimal numbers along the way.
        public static int[] BackTrack(int N, int[] numbers, int[] Answers)
        {
            // Initialize an empty list to store the numbers used.
            List<int> NumbersUsed = new List<int>();

            // Start at the last entry of the DP table.
            int Last = N;
            while (Last > 0)
            {
                // Iterate over all possible choices at the current sub-problem size.
                for (int j = 0; j < numbers.Length; j++)
                {
                    // If the current number was subtracted to achieve the optimal value.
                    if (Last >= numbers[j] && Answers[Last - numbers[j]] + 1 == Answers[Last])
                    {
                        // Add that number and update the last.
                        NumbersUsed.Add(numbers[j]); 
                        Last -= numbers[j];
                        break; // Exit the loop after finding the first choice that works.
                    }
                }
            }

            // Convert the list to an array and return it
            return NumbersUsed.ToArray();
        }



        #endregion

        #endregion
    }
}
