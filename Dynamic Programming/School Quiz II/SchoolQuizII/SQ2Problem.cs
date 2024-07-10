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
        public override string ProblemName { get { return "SchoolQuizII"; } }

        public override void TryMyCode()
        {
            int[] k = { 1, 5, 10, 20, 25 };
            int N = 0;
            int outputVal, expectedVal;
            int[] outputNumbers;

            N = 30;
            expectedVal = 2 ; 
            outputVal = SchoolQuizII.SolveValue(N, k);
            outputNumbers = SchoolQuizII.ConstructSolution(N, k);
            PrintCase(N, k, outputVal, outputNumbers, expectedVal);

            N = 107;
            expectedVal = 7;
            outputVal = SchoolQuizII.SolveValue(N, k);
            outputNumbers = SchoolQuizII.ConstructSolution(N, k);
            PrintCase(N, k, outputVal, outputNumbers, expectedVal);

            N = 40;
            expectedVal = 2;
            outputVal = SchoolQuizII.SolveValue(N, k);
            outputNumbers = SchoolQuizII.ConstructSolution(N, k);
            PrintCase(N, k, outputVal, outputNumbers, expectedVal);
        }

       

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int N = 0, M = 0;
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
                M = br.ReadInt32();
                numbers = new int[M];

                for (j = 0; j < M; j++)
                {
                    numbers[j] = br.ReadInt32();
                }
                actualResult = br.ReadInt32();
                if (readTimeFromFile)
                {
                    timeOutInMillisec = int.Parse(br.ReadString().Split(':')[1]);
                }

                Console.WriteLine("\n===============================");
                Console.WriteLine("CASE#{0}: N = {1}, |Array| = {2}", i, N, numbers.Length);
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
                                    output = SchoolQuizII.SolveValue(N, numbers);
                                }
                                else
                                {
                                    outputVals = SchoolQuizII.ConstructSolution(N, numbers);
                                    if(outputVals!=null)
                                        output = outputVals.Length;
                                }
                                sw.Stop();
                                //Console.WriteLine("time = {0} ms", sw.ElapsedMilliseconds);
                                //Console.WriteLine("output = {0}", output);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                caseException = true;
                                output = -1;
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
                        else if (CheckOutput(numbers, outputVals, N, actualResult))
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

        private void PrintCase(int N, int[] k, int outputVal, int[] outputNumbers, int expectedVal, bool check = true)
        {
            Console.WriteLine("N = {0}", N);
            Console.Write("numbers = "); PrintNums(k);
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
                    if (CheckOutput(k, outputNumbers, N, expectedVal)) Console.WriteLine("CORRECT");
                    else Console.WriteLine("WRONG");
                }
            }
            Console.WriteLine();
        }

        private bool CheckOutput(int[] k, int[] outputNumbers, int inpVal, int expectedVal)
        {
            if (outputNumbers == null)
                return false;
            int N = outputNumbers.Length;
            int M = k.Length;

            if (N != expectedVal)
            {
                Console.WriteLine("WRONG: length of the returned numbers {0} NOT EQUAL expected value {1}", N, expectedVal);
                return false;
            }
            for (int i = 0; i < N; i++)
            {
                if (!k.Contains(outputNumbers[i]))
                {
                    Console.WriteLine("WRONG: {0} not exists in the given array of numbers", outputNumbers[i]);
                    return false;
                }
            }

            if (outputNumbers.Sum() != inpVal)
            {
                Console.WriteLine("WRONG: sum of returned numbers NOT EQUAL the given value (N = {0})", inpVal);
                return false;
            }
                
            return true;
        }
        #endregion
   
    }
}
