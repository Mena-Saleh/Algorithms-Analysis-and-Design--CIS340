using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "AliBabaAndNHouses"; } }

        public override void TryMyCode()
        {
            int N = 0;
            int outputVal, expectedVal;
            int[] outputNumbers;

            {
                N = 5;
                int[] values = { 5, 2, 1, 3, 1 };
                expectedVal = 8;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
            {
                N = 8;
                int[] values = { 8, 3, 5, 1, 7, 6, 5, 3};
                expectedVal = 25;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
            {
                N = 6;
                int[] values = { 3, 2, 5, 6, 6, 9 };
                expectedVal = 18;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
            {
                N = 3;
                int[] values = { 3, 7, 2 };
                expectedVal = 7;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
            {
                N = 2;
                int[] values = { 13, 17 };
                expectedVal = 17;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
            {
                N = 9;
                int[] values = { 4, 7, 4, 3, 5, 4, 2, 4, 1 };
                expectedVal = 18;
                outputVal = AliBabaAndNHouses.SolveValue(values, N);
                outputNumbers = AliBabaAndNHouses.ConstructSolution(values, N);
                PrintCase(N, values, outputVal, outputNumbers, expectedVal);
            }
     
        }

       

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int N = 0;
            int[] numbers = null;
            int output = -1;
            int actualResult = 0;
            int j=0;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int[] correctCases = new int[2];
            int[] wrongCases = new int[2];
            int[] timeLimitCases = new int[2];
            for (int i = 0; i < 2; i++)
            {
                correctCases[i] = 0;
                wrongCases[i] = 0;
                timeLimitCases[i] = 0;
            }
            bool readTimeFromFile = false;
            if (timeOutInMillisec == -1)
            {
                readTimeFromFile = true;
            }
            float maxTime = float.MinValue;
            float avgTime = 0;
            for (int i = 1; i <= testCases; i++)
            {
                N = br.ReadInt32();
                numbers = new int[N];

                for (j = 0; j < N; j++)
                {
                    numbers[j] = br.ReadInt32();
                }
                actualResult = br.ReadInt32();
                if (readTimeFromFile)
                {
                    timeOutInMillisec = int.Parse(br.ReadString().Split(':')[1]);
                }

                Console.WriteLine("\n===============================");
                Console.WriteLine("CASE#{0}: N = {1}", i, N);
                Console.WriteLine("===============================");

                for (int c = 0; c < 2; c++)
                {
                    caseTimedOut = true;
                    Stopwatch sw = null;
                    caseException = false;
                    int[] outputVals = null;
                    {
                        tstCaseThr = new Thread(() =>
                        {
                            try
                            {
                                sw = Stopwatch.StartNew();
                                if (c == 0)
                                {
                                    output = AliBabaAndNHouses.SolveValue(numbers, N);
                                }
                                else
                                {
                                    outputVals = AliBabaAndNHouses.ConstructSolution(numbers, N);
                                    if(outputVals==null)
                                        output = 0;
                                }
                                sw.Stop();
                                Console.WriteLine("time = {0} ms", sw.ElapsedMilliseconds);
                                //Console.WriteLine("output = {0}", output);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                caseException = true;
                                output = 0;
                                outputVals = null;
                            }
                            caseTimedOut = false;
                        });

                        //StartTimer(timeOutInMillisec);
                        tstCaseThr.Start();
                        tstCaseThr.Join(timeOutInMillisec);
                    }

                    if (caseTimedOut)       //Timedout
                    {
                        tstCaseThr.Abort();
                        Console.WriteLine("Time Limit Exceeded in Case {0} [FUNCTION#{1}].", i, c+1);
                        timeLimitCases[c]++;
                    }
                    else if (caseException) //Exception 
                    {
                        Console.WriteLine("Exception in Case {0} [FUNCTION#{1}].", i, c+1);
                        wrongCases[c]++;
                    }
                    else if (output == actualResult)    //Passed
                    {
                        if (c == 0)
                        {
                            Console.WriteLine("Test Case {0} [FUNCTION#{1}] Passed!", i, c + 1);
                            correctCases[c]++;
                        }
                        else if (CheckOutput(numbers, outputVals, output, actualResult))
                        {
                            Console.WriteLine("Test Case {0} [FUNCTION#{1}] Passed!", i, c + 1);
                            correctCases[c]++;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Answer in Case {0} [FUNCTION#{1}].", i, c + 1);
                            wrongCases[c]++;
                        }
                        //maxTime = Math.Max(maxTime, sw.ElapsedMilliseconds);
                        //avgTime += sw.ElapsedMilliseconds;
                    }
                    else                    //WrongAnswer
                    {
                        Console.WriteLine("Wrong Answer in Case {0} [FUNCTION#{1}].", i, c+1);
                        if (level == HardniessLevel.Easy)
                        {
                            if (output != -1)
                            {
                                PrintCase(N, numbers, output, outputVals, actualResult, false);
                            }
                            else
                            {
                                Console.WriteLine("Exception is occur");
                            }
                        }
                        wrongCases[c]++;
                    }
                }
            }
            s.Close();
            br.Close();
            for (int c = 0; c < 2; c++)
            {
                Console.WriteLine("EVALUATION OF FUNCTION#{0}:", c+1);
                Console.WriteLine("# correct = {0}", correctCases[c]);
                Console.WriteLine("# time limit = {0}", timeLimitCases[c]);
                Console.WriteLine("# wrong = {0}", wrongCases[c]);
                //Console.WriteLine("\nFINAL EVALUATION (%), AVG TIME, MAX TIME = {0} {1} {2}", Math.Round((float)correctCasesPart1 / totalCases * 100, 0), correctCasesPart1 == 0 ? -1 : Math.Round(avgTime / (float)correctCasesPart1, 2), correctCasesPart1 == 0 ? -1 : maxTime);
                //Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0));
                //Console.WriteLine("AVERAGE EXECUTION TIME (ms) = {0}", Math.Round(avgTime / (float)correctCases, 2));
                //Console.WriteLine("MAX EXECUTION TIME (ms) = {0}", maxTime); 
            }
            Console.WriteLine("\nFINAL EVALUATION: FUNCTION#1 (%), FUNCTION#2 (%) = {0} {1}", Math.Round((float)correctCases[0] / totalCases * 100, 0), Math.Round((float)correctCases[1] / totalCases * 100, 0));

        }

       

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintNums(int[] X)
        {

            int N = X.Length;

            for (int i = 0 ; i < N; i++)
            {
                    Console.Write(X[i] + "  ");
            }
            Console.WriteLine();
        }

        private void PrintCase(int N, int[] values, int outputVal, int[] outputNumbers, int expectedVal, bool check = true)
        {
            Console.WriteLine("N = {0}", N);
            Console.Write("numbers = "); PrintNums(values);
            Console.WriteLine("expected value = {0}", expectedVal);
            Console.WriteLine("output value = {0}", outputVal);
            if (outputNumbers != null)
            {
                Console.Write("output numbers = "); PrintNums(outputNumbers);
            }

            if (check)
            {
                if (outputVal != expectedVal)
                {
                    Console.WriteLine("WRONG");
                }
                else
                {
                    if (CheckOutput(values, outputNumbers, outputVal, expectedVal)) Console.WriteLine("CORRECT");
                    else Console.WriteLine("WRONG");
                }
            }
            Console.WriteLine();
        }

        private bool CheckOutput(int[] values, int[] outputNumbers, int outputVal, int expectedVal)
        {
            if (outputNumbers == null)
                return false;
            int H = outputNumbers.Length;
            int N = values.Length;

            if (outputVal != expectedVal)
            {
                Console.WriteLine("WRONG: output value {0} NOT EQUAL expected value {1}", outputVal, expectedVal);
                return false;
            }
            int sum = 0;
            for (int i = 0; i < H; i++)
            {
                if (outputNumbers[i] < 1 || outputNumbers[i] > N)
                {
                    Console.WriteLine("WRONG: {0} not a valid house number (remember it's 1-based)", outputNumbers[i]);
                    return false;
                }
                sum += values[outputNumbers[i] - 1];
            }

            if (sum != expectedVal)
            {
                Console.WriteLine("WRONG: values sum of returned houses NOT EQUAL the expected optimal value", expectedVal);
                return false;
            }
                
            return true;
        }
        #endregion
   
    }
}
