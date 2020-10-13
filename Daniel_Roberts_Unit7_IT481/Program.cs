using System;
using System.IO;

namespace Daniel_Roberts_Unit7_IT481
{
    class Program
    {
        static void Main(string[] args)
        {

            var watch1bs = new System.Diagnostics.Stopwatch();
            var watch2bs = new System.Diagnostics.Stopwatch();
            var watch3bs = new System.Diagnostics.Stopwatch();
            var watch1ms = new System.Diagnostics.Stopwatch();
            var watch2ms = new System.Diagnostics.Stopwatch();
            var watch3ms = new System.Diagnostics.Stopwatch();

            int[] bubbleArraySmall = ImportDataSet(@"dataset_small.txt");
            int[] bubbleArrayMedium = ImportDataSet(@"dataset_medium.txt");
            int[] bubbleArrayLarge = ImportDataSet(@"dataset_large.txt");

            int[] mergeArraySmall = ImportDataSet(@"dataset_small.txt");
            int[] mergeArrayMedium = ImportDataSet(@"dataset_medium.txt");
            int[] mergeArrayLarge = ImportDataSet(@"dataset_large.txt");

            Console.WriteLine("Running sorting tests. Please wait...");
            //Bubble Sort Tests
            watch1bs.Start();
            BubbleSort(bubbleArraySmall);
            watch1bs.Stop();
            watch2bs.Start();
            BubbleSort(bubbleArrayMedium);
            watch2bs.Stop();
            watch3bs.Start();
            BubbleSort(bubbleArrayLarge);
            watch3bs.Stop();
            
            //Merge Sort Tests
            watch1ms.Start();
            MergeSort(mergeArraySmall, 0, mergeArraySmall.Length - 1);
            watch1ms.Stop();
            watch2ms.Start();
            MergeSort(mergeArrayMedium, 0, mergeArrayMedium.Length - 1);
            watch2ms.Stop();
            watch3ms.Start();
            MergeSort(mergeArrayLarge, 0, mergeArrayLarge.Length - 1);
            watch3ms.Stop();
            
            Console.WriteLine("**Bubble Sort Results**");
            Console.WriteLine("Big-O Complexity: n^2");
            Console.Write("Elapsed time for 10,000 items: ");
            Console.WriteLine(CalculateRunTime(watch1bs.ElapsedTicks) + " milliseconds");
            Console.Write("Elapsed time for 50,000 items: ");
            Console.WriteLine(CalculateRunTime(watch2bs.ElapsedTicks) + " milliseconds");
            Console.Write("Elapsed time for 100,000 items: ");
            Console.WriteLine(CalculateRunTime(watch3bs.ElapsedTicks) + " milliseconds");
            Console.WriteLine();
            
            //Print Results
            Console.WriteLine("**Merge Sort Results**");
            Console.WriteLine("Big-O Complexity: n (log n)");
            Console.Write("Elapsed time for 10,000 items: ");
            Console.WriteLine(CalculateRunTime(watch1ms.ElapsedTicks) + " milliseconds");

            Console.Write("Elapsed time for 50,000 items: ");
            Console.WriteLine(CalculateRunTime(watch2ms.ElapsedTicks) + " milliseconds");

            Console.Write("Elapsed time for 100,000 items: ");
            Console.WriteLine(CalculateRunTime(watch3ms.ElapsedTicks) + " milliseconds");
            Console.WriteLine();

            Console.WriteLine("Press any key to close window...");
            Console.ReadKey();
        }

        //Bubble Sort
        //https://www.geeksforgeeks.org/bubble-sort/
        public static void BubbleSort(int[] input)
        {
            int temp;
            for (int i = 0; i <= input.Length - 2; i++)
            {
                for (int j = 0; j <= input.Length - 2; j++)
                {
                    if (input[j] > input[j + 1])
                    {
                        temp = input[j + 1];
                        input[j + 1] = input[j];
                        input[j] = temp;
                    }
                }
            }
        }

        //Merge Sort
        //https://www.geeksforgeeks.org/merge-sort/
        public static void Merge(int[] input, int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        public static void MergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }

        //DataSet Methods

        public static int[] ImportDataSet(string filePath)
        {
            string fileContents = File.ReadAllText(filePath);
            string newline = '\r'.ToString() + '\n'.ToString();
            fileContents = fileContents.Replace(newline, '\n'.ToString());
            string[] array = fileContents.Split(new char[] { '\n' });
            int[] intArray = new int[array.Length];
            for (int i = 0; i < intArray.Length; i++)
            {
                int.TryParse(array[i], out intArray[i]);
            }
            
            return intArray;
        }
    
        //Function to Convert Ticks to Milliseconds as a Float
        public static float CalculateRunTime(long time)
        {
            float runtime = (float)time;
            runtime /= 10000;
            return runtime;
        }
    }
}