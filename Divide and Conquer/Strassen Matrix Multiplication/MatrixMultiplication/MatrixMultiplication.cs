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


    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        /// 


        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N) //Multiplies two large matrices efficiently using Strassen's algorithm. Overall complexity is around N^2.81
        {

            int[,] result = new int[N, N];


            //Base case
            //Normal Multiplication (More efficient as Strassen is only faster at large input sizes)
            //Strassen is asymptotically faster than the naive approach, where the running time of strassen is indeed lower for all N > N0, N0 is called leaf size and it is experimental.
            //Theta(N^3)
            if (N <= 64) //Leaf size is set to 64 (experimental)
            {
                int rowXcol;
                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < N; k++) //Reversing K and J is more cache friendly because it facilitates contiguous memory access.
                    {
                        //This approach is more cache friendly.
                        rowXcol = 0;
                        for (int j = 0; j < N; j++)
                        {
                            rowXcol += M1[i, j] * M2[j, k];
                        }
                        result[i, k] = rowXcol;
                    }
                }
                return result;
            }

            int N2 = N / 2;

            //Allocating memory for arrays:

            //For M1
            int[,] a = new int[N2, N2];
            int[,] b = new int[N2, N2];
            int[,] c = new int[N2, N2];
            int[,] d = new int[N2, N2];

            //For M2
            int[,] e = new int[N2, N2];
            int[,] f = new int[N2, N2];
            int[,] g = new int[N2, N2];
            int[,] h = new int[N2, N2];


            //Using array.copy is faster than traditional copy using two nested loops, array.copy does shallow copy which is what we need here for better performance
            for (int i = 0; i < N2; i++)
            {
                //Basically copying row by row

                //M1:
                Array.Copy(M1, i * N, a, i * N2, N2);
                Array.Copy(M1, i * N + N2, b, i * N2, N2);
                Array.Copy(M1, (i + N2) * N, c, i * N2, N2);
                Array.Copy(M1, (i + N2) * N + N2, d, i * N2, N2);

                //M2:
                Array.Copy(M2, i * N, e, i * N2, N2);
                Array.Copy(M2, i * N + N2, f, i * N2, N2);
                Array.Copy(M2, (i + N2) * N, g, i * N2, N2);
                Array.Copy(M2, (i + N2) * N + N2, h, i * N2, N2);


            }

            //Strassen has 7 multiplications in it which makes it asymptotically faster than the naive D&C approach which has 8 multiplications instead.

            //Allocating memory for Strassen's formula's 7 products.
            int[,] P1 = new int[N2, N2];
            int[,] P2 = new int[N2, N2];
            int[,] P3 = new int[N2, N2];
            int[,] P4 = new int[N2, N2];
            int[,] P5 = new int[N2, N2];
            int[,] P6 = new int[N2, N2];
            int[,] P7 = new int[N2, N2];


            //Recursive calls to get all 7 submatrices (conquer step)
        
            //Running each divide step in parralel to utilize computational power and minimize processing time.
            Task t1 = Task.Run(() =>
            {
                P1 = MatrixMultiply(a, Subtract(f, h, N2), N2);

            });

            Task t2 = Task.Run(() =>
            {
                P2 = MatrixMultiply(Add(a, b, N2), h, N2);

            });
            Task t3 = Task.Run(() =>
            {
                P3 = MatrixMultiply(Add(c, d, N2), e, N2);
            });
            Task t4 = Task.Run(() =>
            {
                P4 = MatrixMultiply(d, Subtract(g, e, N2), N2);
            });
            Task t5 = Task.Run(() =>
            {
                P5 = MatrixMultiply(Add(a, d, N2), Add(e, h, N2), N2);
            });
            Task t6 = Task.Run(() =>
            {
                P6 = MatrixMultiply(Subtract(b, d, N2), Add(g, h, N2), N2);
            });
            Task t7 = Task.Run(() =>
            {
                P7 = MatrixMultiply(Subtract(a, c, N2), Add(e, f, N2), N2);
            });

            //Await completion of all 7 tasks because combine is dependant on the correctness and coherence of all 7 matrices.
            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7);
            
     

            //Calculate submatrices of result by applying Strassen's idea
            int[,] r = Add(Subtract(Add(P5, P4, N2), P2, N2), P6, N2);
            int[,] s = Add(P1, P2, N2);
            int[,] t = Add(P3, P4, N2);
            int[,] u = Subtract(Subtract(Add(P5, P1, N2), P3, N2), P7, N2);

            //Combining all results into one matrix (combine step)
            for (int i = 0; i < N2; i++)
            {
                Array.Copy(r, i * N2, result, i * N, N2);
                Array.Copy(s, i * N2, result, i * N + N2, N2);
                Array.Copy(t, i * N2, result, (i + N2) * N, N2);
                Array.Copy(u, i * N2, result, (i + N2) * N + N2, N2);
            }

            return result;


        }

        //Add two matrices by adding each respective element individually. Theta(N^2)
        static int[,] Add(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] = M1[i, j] + M2[i, j];
                }
            }

            return result;
        }

        //Subtract two matrices by subtracting each respective element individually. Theta(N^2)
        static int[,] Subtract(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] = M1[i, j] - M2[i, j];
                }
            }

            return result;
        }



        #endregion

    }

}




