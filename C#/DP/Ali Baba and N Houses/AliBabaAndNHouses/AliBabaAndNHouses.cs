using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaAndNHouses
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the maximum amount of money that Ali baba can get, given the number of houses (N) and a list of the net gained value for each consecutive house (V)
        /// </summary>
        /// <param name="values">Array of the values of each given house (ordered by their consecutive placement in the city)</param>
        /// <param name="N">The number of the houses</param>
        /// <returns>the maximum amount of money the Ali Baba can get </returns>
        static public int SolveValue(int[] values, int N)
        {
            int[] dp = new int[N + 2];
            dp[N] = 0;
            dp[N + 1] = 0;

            for (int i = N - 1; i >= 0; i--)
            {
                dp[i] = Math.Max(dp[i + 1], dp[i + 2] + values[i]);
            }

            return dp[0];
        }


       

        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Array of the indices of the robbed houses (1-based and ordered from left to right) </returns>
        static public int[] ConstructSolution(int[] values, int N)
        {
            int[] dp = new int[N + 2];
            dp[N] = 0;
            dp[N + 1] = 0;

            for (int j = N - 1; j >= 0; j--)
            {
                dp[j] = Math.Max(dp[j + 1], dp[j + 2] + values[j]);
            }

            // Back tracking:
            List<int> taken = new List<int>();
            int i = 0;
            while (i < N)
            {
                if (dp[i + 1] >= dp[i + 2] + values[i]) // House skipped
                {
                    i++;
                }
                else // House robbed
                {
                    taken.Add(i + 1);
                    i += 2;
                }
            }

            return taken.ToArray();
        }



        #endregion

        #endregion
    }
}
