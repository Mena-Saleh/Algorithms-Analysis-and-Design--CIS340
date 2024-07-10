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
    public static class OfficeOrganization
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// find the minimum costs in MOST EFFICIENT WAY to organize your office to meet your father needs.
        /// </summary>
        /// <param name="N">initial load</param>
        /// <param name="M">target load required by your father</param>
        /// <param name="A">cost of reducing the load by half</param>
        /// <param name="B">cost of reducing the load by 1</param>
        /// <returns>Min total cost to reduce the load from N to M</returns>
        /// 

        // worst case is O(log(N))
        // best case is O(1)
        // Average is probably near O(log(N))
        public static int OrganizeTheOffice(int N, int M, int A, int B)
        {
            //Console.WriteLine();
            //Console.WriteLine("<-- This is a trial -->");
            //Console.WriteLine($"N = {N}, M = {M}, A = {A}, B = {B}");
            //Console.WriteLine("<-- This is a trial -->");
            //Console.WriteLine();

            int cost = 0;
            int halfN;
            while (N > M)
            {
                halfN = N / 2;
                if (halfN >= M && A < Math.Floor(N / 2.0) * B )//Greedy choice, brother vs sister, determine who is better and ask for their help in each iteration.
                {
                    cost += A;
                    N = halfN;
                }
                else
                {
                    cost += (N - M) * B;
                    N = M;
                }
            }
            return cost;
        }
        #endregion
    }
}
