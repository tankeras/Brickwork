using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //wall height
            int M = 1;
            //wall width
            int N = 1;
            while (M % 2 != 0 || N % 2 != 0 || M > 100 || N > 100)
            {
                Console.WriteLine("Width and height must be even numbers, not exceding 100");
                var input = Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
                M = input[0];
                N = input[1];
            }

            //2d array representing the first layer of bricks
            int[,] firstLayer = new int[M, N];

            try
            {
                for (int i = 0; i < M; i++)
                {
                    var input = Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
                    for (int j = 0; j < N; j++)
                    {
                        firstLayer[i, j] = input[j];
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Input has wrong dimensions");
            }

            ValidateNoTripleBricks(firstLayer);
            
            // 1d flattened representation of the 2d firstlayer
            var flattenedList = new List<int>();

            foreach (var number in firstLayer)
            {
                flattenedList.Add(number);
            }
            flattenedList = flattenedList.Distinct().ToList();
            flattenedList.Reverse();

            //2d array representing the second layer of bricks
            int[,] secondLayer = new int[M, N];

            for (int i = 0; i < M-1; i++)
            {
                if (secondLayer[i, 0] != 0)
                {
                    continue;
                }
                for (int j = 0; j < N-1; j++)
                {
                    
                    if (firstLayer[i, j] == firstLayer[i, j+1])
                    {
                        secondLayer[i, j] = flattenedList[0];
                        secondLayer[i+1, j] = flattenedList[0];
                        secondLayer[i, j+1] = flattenedList[1];
                        secondLayer[i + 1, j+1] = flattenedList[1];
                        flattenedList.RemoveAt(0);
                        flattenedList.RemoveAt(0);
                        j++;
                    }
                    else
                        {
                        secondLayer[i, j] = flattenedList[0];
                        secondLayer[i, j+1] = flattenedList[0];
                        secondLayer[i+1, j] = flattenedList[1];
                        secondLayer[i+1, j + 1] = flattenedList[1];
                        j++;
                        flattenedList.RemoveAt(0);
                        flattenedList.RemoveAt(0);
                    }
                }
            }

            PrintOutSecondLayer(secondLayer, N);


            //Method validating there all the bricks are 2 digits long
            static void ValidateNoTripleBricks(int[,] layer)
            {
                var query = from int item in layer                            
                            select item;                
                foreach (var item in layer)
                {
                    var count = query.Where(x => x == item).ToArray().Count();
                    if (count > 2)
                    {
                        throw new InvalidDataException("Bricks should be 2 digits long");
                    }
                }                                
            }
            //Method printing out the second layer to the console
            static void PrintOutSecondLayer(int[,] secondLayer, int wallWidth)
            {
                var counter = 1;
                var previousNumber = -1;
                foreach (var number in secondLayer)
                {
                    if (previousNumber == number)
                    {
                        Console.Write(" " + number);
                    }
                    else
                    {
                        Console.Write("|" + number);
                    }
                    if (counter % wallWidth == 0)
                    {
                        Console.Write("|");
                        Console.WriteLine();
                    }
                    previousNumber = number;
                    counter++;
                }
            }
        }
    }
}
