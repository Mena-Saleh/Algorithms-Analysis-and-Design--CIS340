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
    
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
        /// 
        static private Dictionary<string, byte[]> already_calced = new Dictionary<string, byte[]>();
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {

            long x_numric = convert_to_long(X);
            long y_numric = convert_to_long(Y);
            if (already_calced.ContainsKey(x_numric.ToString() + "*" + y_numric.ToString()))
                return already_calced[x_numric.ToString() + "*" + y_numric.ToString()];
            else if (already_calced.ContainsKey(y_numric.ToString() + "*" + x_numric.ToString()))
                return already_calced[y_numric.ToString() + "*" + x_numric.ToString()];


            N = Math.Max(X.Length, Y.Length);
            byte[] result = new byte[N * 2];
            Array.Resize(ref X, N);
            Array.Resize(ref Y, N);
            if (N == 1)
            {
                byte newX = (byte)(X[0] * Y[0]);
                if (newX >= 10)
                {
                    result[1] = (byte)(newX / 10);
                    result[0] = (byte)(newX % 10);
                }
                else
                {
                    result[0] = newX;
                    result[1] = 0;
                }
                already_calced[x_numric.ToString() + "*" + y_numric.ToString()] = result;
                already_calced[y_numric.ToString() + "*" + x_numric.ToString()] = result;
                return result;
            }
            else 
            {
                byte[] b = new byte[X.Length / 2];
                byte[] a = new byte[X.Length / 2];
                byte[] d = new byte[Y.Length / 2];
                byte[] c = new byte[Y.Length / 2];
                Array.Copy(X, 0, a, 0, X.Length / 2);
                Array.Copy(X, X.Length / 2, b, 0, X.Length / 2);
                Array.Copy(Y, 0, c, 0, Y.Length / 2);
                Array.Copy(Y, Y.Length / 2, d, 0, Y.Length / 2);
                // calc (a*c) and (b*d)
                byte[] ac = IntegerMultiply(a, c, N/2);
                byte[] bd = IntegerMultiply(b, d, N/2);
                // now calc the Z value ::::: z = (a+b) * (c+d)
                // then a+b
                byte[] a_plus_b = Add(a,b);
                // then c+d
                byte[] c_plus_d = Add(c, d);
                //then Z
                byte[] z= IntegerMultiply(a_plus_b, c_plus_d, a_plus_b.Length);
                // Res = 10N × M2 + 10N/2 × (Z – M1 – M2) + M1 where M1 = A×C		M2 = B×D
                byte[] minas_magic_number = Sub(z, ac, bd);
                // Res = 10N × M2:B×D + 10N/2 × minas_magic_number + M1:A×C
                for (int i = 0; i < ac.Length; i++)
                {
                    byte value = (byte)(result[i] + ac[i]);
                    if (value >= 10)
                    {
                        result[i] = (byte)(value % 10);
                        result[i+1] += (byte)(value / 10);
                    }   
                    else result[i] = value;
                }
                int inner_I = N / 2;
                for (int i = 0; i < minas_magic_number.Length && i + inner_I < result.Length; i++)
                {
                    byte value = (byte)(result[i + inner_I] + minas_magic_number[i]);
                    if (value >= 10)
                    {
                        result[i + inner_I] = (byte)(value % 10);
                        result[i + inner_I + 1] += (byte)(value / 10);
                    }
                    else result[i + inner_I] = value;
                }
                for (int i = 0; i < bd.Length; i++)
                {
                    byte value = (byte)(result[i + N] + bd[i]);
                    if (value >= 10)
                    {
                        result[i + N] = (byte)(value % 10);
                        result[i + N + 1] += (byte)(value / 10);
                    }
                    else result[i + N] = value;
                }
                already_calced[x_numric.ToString() + "*" + y_numric.ToString()] = result;
                already_calced[y_numric.ToString() + "*" + x_numric.ToString()] = result;
                return result;
            }
            byte[] Sub(byte[] z, byte[] m1, byte[] m2)
            {
                long z_int = convert_to_long(z);
                long m1_int = convert_to_long(m1);
                long m2_int = convert_to_long(m2);
                long res = z_int - m1_int - m2_int;
                List<byte> result_for_sub = new List<byte>();
                int i = 0;
                while (res > 0)
                {
                    result_for_sub.Insert(result_for_sub.Count, (byte)(res % 10));
                    res /= 10;
                    i++;
                }
                return result_for_sub.ToArray();
            }
            byte[] Add(byte[] x, byte[] y)
            {

                List<byte> res = new List<byte>();
                byte borrow = 0;
                for (int i = 0; i < x.Length || borrow != 0; i++)
                {
                    byte value = 0;
                    if (i < x.Length)
                        value += (byte)(x[i] + y[i]);
                    value += borrow;
                    if (value >= 10)
                    {
                        res.Insert(res.Count,(byte)(value % 10));
                        borrow = (byte)(value / 10);
                    }
                    else 
                    {
                        res.Insert(res.Count, value);
                        borrow = 0;
                    }
                }
                if (res.Count % 2 != 0 && res.Count != 1)
                {
                    res.Insert(res.Count, (byte)0);
                }
                return res.ToArray();
                /*
                 * int x_int = convert_to_long(x);
                int y_int = convert_to_long(y);
                int res = x_int +y_int;
                int number = res / 10;
                if (res % 10 != 0)
                    number++;
                byte[] result_for_sub = new byte[number];
                int i = 0;
                while (res > 0)
                {
                    result_for_sub[i] = (byte)(res % 10);
                    res /= 10;
                    i++;
                }
                return result_for_sub;
                */
            }
            long convert_to_long(byte[] arr)
            {
                long x = arr[arr.Length - 1];
                for (int digit = arr.Length - 2; digit >= 0; digit--)
                {
                    x *= 10;
                    x += arr[digit];
                }
                return x;
            }
        }
        
        #endregion
    }
}
