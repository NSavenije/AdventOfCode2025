namespace Aoc2025.Day_06 {
    using System.Collections.Generic;
    using static InputParser;
    public static class Day06 {
        const string FILEPATH = "Day_06/input.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, c => c);
            List<long[]> numLists =[];
            string[] ops = [];
            foreach (string line in input)
            {
                if (line[0] == '*' || line[0] == '+')
                {
                    ops = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                long[] nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                numLists.Add(nums);
                }
            }
            long sumTotal = 0;
            for(int i = 0; i < ops.Length; i++)
            {
                long total = ops[i] == "+" ? 0 : 1;
                foreach(long[] nums in numLists)
                {
                    long num = nums[i];
                    total = ops[i] == "+" ? total + num : total * num;
                }
                sumTotal += total;
            }
            // TODO: Implement Part 1
            Console.WriteLine(sumTotal);
        }
        public static void Part2() {
            string[] input = ParseLinesAsList(FILEPATH, c => c).ToArray();
            int lines = input.Length;
            int columns = input[0].Length;
            string[] ops = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int numberOfSums = ops.Length;
            List<long>[] sumLists = new List<long>[numberOfSums];
            for(int i = 0; i < numberOfSums; i++)
            {
                sumLists[i] = new();
            }
            int sumIndex = -1;
            for(int col = 0; col < columns; col++)
            {
                if (input[^1][col] == '+' || input[^1][col] == '*')
                {
                    sumIndex++;
                }
                var numStringBuilder = new System.Text.StringBuilder();
                for(int row = 0; row < lines - 1; row++)
                {
                    numStringBuilder.Append(input[row][col]);
                }
                if(long.TryParse(numStringBuilder.ToString(), out long num))
                    sumLists[sumIndex].Add(num);
            }
            long sumTotal = 0;
            for(int i = 0; i < ops.Length; i++)
            {
                long total = ops[i] == "+" ? 0 : 1;
                foreach(long num in sumLists[i])
                {
                    total = ops[i] == "+" ? total + num : total * num;
                }
                sumTotal += total;
            }
            Console.WriteLine(sumTotal);
        }
    }
}
