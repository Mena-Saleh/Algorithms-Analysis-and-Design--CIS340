using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {

            //Reading from files:
            string file1 = ReadDoc(doc1FilePath);
            string file2 = ReadDoc(doc2FilePath);

            //Base case where the documents are the same always has an angle of 0 (no difference).
            //Best case with fastest response time.
            if (file1.Equals(file2))
            {
                return 0;
            }

            //First, Split each doucment into words and record the frequency of each word in a dictionary:

            Dictionary<string, double> Doc1WordsCount = SplitDocAndCountWords(file1);
            Dictionary<string, double> Doc2WordsCount = SplitDocAndCountWords(file2);


            //Second, compute the distance. Running time should be around O(N) (Linear time):

            return ComputeDistance(Doc1WordsCount, Doc2WordsCount);
            


        }


        //Reads data from a file and returns string containing the data.
        public static string ReadDoc(string path) { 
            
            return System.IO.File.ReadAllText(path);
        }
        
        //Split the doucment into individual words and returns the count of each word in a dictionary --> O(N):
        public static Dictionary<string, double> SplitDocAndCountWords(string toSplit) {

            toSplit = toSplit.ToLower(); //To make it case insensitive
            Dictionary<string, double> ReturnDictionary = new Dictionary<string, double>();
            string word;
            int j;
            //Loop splitting alphanumeric strings only:
            for (int i = 0; i < toSplit.Length; i++)
            {
                word = "";
                j = i;
                while ( j< toSplit.Length && ((toSplit[j] >= 48 && toSplit[j] <= 57 )|| (toSplit[j] >= 97 && toSplit[j] <= 122))) //while alphanumeric character (a-z and 0-9)
                {
                    word += toSplit[j];
                    j++;
                }

                if (ReturnDictionary.ContainsKey(word))
                {
                    ReturnDictionary[word]++;
                }
                else
                {
                    if (word.Length != 0) //Make sure it is not an empty string and register it in the count dictionary.
                    {
                        ReturnDictionary.Add(word, 1);
                    }
                }

                i = j;

            }

            return ReturnDictionary;


        }


        //Computes distance using mathematical formula for scalar products.
        public static double ComputeDistance(Dictionary<string, double> d1, Dictionary<string,double> d2) 
        {
            //Console.WriteLine("->" + CalculateDotProduct(d1, d2) + "/" + Math.Sqrt((CalculateMagnitude(d1) * CalculateMagnitude(d2))));
            double dotProd;
            if ((dotProd = CalculateDotProduct(d1,d2)) == 0) //No need to proceed calculating magnitude and Acos, because if A . B = 0, the angle is 90 always.
            {
                return 90;
            }
            double Rad = Math.Acos(dotProd / Math.Sqrt((CalculateMagnitude(d1) * CalculateMagnitude(d2))));
            return Rad * (180 / Math.PI);

        }


        //Calculate dot product => v1 (x,y,z) , v2 (a,b,c) , prod = xa + yb + zc --> O(N)
        public static double CalculateDotProduct(Dictionary<string, double> d1, Dictionary<string, double> d2) 
        {
            double Result = 0;
            foreach (string word in (d1.Count > d2.Count? d2: d1).Keys) //Loop over smaller dictionary
            {
                if (d1.ContainsKey(word) && d2.ContainsKey(word))
                {
                    Result += d1[word] * d2[word];
                }
            }

            return Result;
        }


        //Calculate magnitude => |(x,y,z)| = sqrt(x^2 + y^2 + z^2) --> O(N)
        public static double CalculateMagnitude(Dictionary<string, double> d)
        {
            double Result = 0;
            foreach (double count in d.Values)
            {
                Result += count * count;
            }

            return Result;
        }


  


    }
}
