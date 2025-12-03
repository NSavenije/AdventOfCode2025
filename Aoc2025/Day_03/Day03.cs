namespace Aoc2025.Day_03 {
    using System.Linq;
    using static InputParser;
    public static class Day03 {

        const string FILEPATH = "Day_03/input.txt";
        
        public static void Part1() {
            // TODO: Implement Part 1
            var banks = ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray());
            long sum = 0;
            foreach(var bank in banks)
            {
                int largest = bank[0];
                int second = 0;
                int index = 0;
                int secondIndex = 0;
                
                for (int i = 1; i < bank.Length - 1; i++)
                {
                    if (bank[i] > largest)
                    {
                        largest = bank[i];
                        index = i;
                    }
                }
                for (int i = index + 1; i < bank.Length; i++)
                {
                    if (bank[i] >= second)
                    {
                        second = bank[i];
                        secondIndex = i;
                    }
                }
                int res = 10 * largest + second;
                Console.WriteLine(res);
                sum += res;
            }
            Console.WriteLine(sum);
        }
        public static void Part2() {
            var banks = ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray());
            long sum = 0;
            foreach(var bank in banks)
            {
                int largest;
                int index = 0;
                long res = 0;
                
                for (int counter = 0; counter < 12; counter++)
                {
                    largest = 0;
                    for (int i = index; i < bank.Length - (11 - counter); i++)
                    {
                        if (bank[i] > largest)
                        {
                            largest = bank[i];
                            index = i + 1;
                        }
                    }
                    res += largest * (long)Math.Pow(10, 11 - counter);
                }
                Console.WriteLine(res);
                sum += res;
            }
            Console.WriteLine(sum);
        }
    }
}
