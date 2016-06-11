using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    class CountNonDivisible
    {
        static void Main(string[] args)
        {
            Solution sol = new Solution();
            int[] input = GenerateRandomArray(30000);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here            
            int[] result = sol.solution(input);
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds +"\n");
            //Console.WriteLine(string.Join(",", input) + "\n");
            //Console.WriteLine(string.Join(",", result) + "\n");

            Console.ReadKey();            
        }

        static int[] GenerateRandomArray(int length)
        {
            List<int> list = new List<int>();

            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                list.Add(rnd.Next(1, length*2));
            }

            return list.ToArray();
        }

        class Solution
        {
            public int[] solution(int[] A)
            {
                // write your code in C# 6.0 with .NET 4.5 (Mono)
                int inputLength = A.Length;

                List<int> primeList = SieveOfEratosthenes(inputLength * 2);

                Dictionary<int, int> dict = new Dictionary<int, int>();
                var obj = from i in A
                       group i by i into iGroup
                       select new { iKey = iGroup.Key, iCount = iGroup.Count() };
                dict = obj.Select(t => new { t.iKey, t.iCount }).ToDictionary(t => t.iKey, t => t.iCount);

                int numOfOnes = 0;
                if (dict.ContainsKey(1)) numOfOnes = dict[1];
                
                int[] result = new int[inputLength];
                int gg = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    int counter = 0;
                    
                    if (A[i] == 1)
                    {
                        counter = inputLength - numOfOnes;
                    }
                    else if (primeList.Contains(A[i]))
                    {
                        counter = inputLength - numOfOnes - dict[A[i]];
                    }
                    else
                    {
                        gg++;
                        Console.WriteLine(gg);
                        for (int j = 0; j < inputLength; j++)
                        {
                            if (i == j) continue;

                            if (A[i] % A[j] > 0) counter++;
                        }
                    }
                   
                    result[i] = counter;
                }

                return result;
            }

            public static List<int> SieveOfEratosthenes(int length)
            {
                bool[] A = new bool[length];
                for(int i=2; i<length; i++)
                {
                    A[i] = true;
                }

                for (int i = 2; i < Math.Sqrt(length); i++)
                {
                    if(A[i])
                    {
                        for (int y = Convert.ToInt32(Math.Pow(i, 2)); y < length; y+=i)
                        {
                            A[y] = false;
                        }
                    }
                }

                List<int> primeList = new List<int>();
                for (int i = 2; i < length; i++)
                {
                    if (A[i]) primeList.Add(i);
                }

                return primeList;
            }
        }
    }
}
