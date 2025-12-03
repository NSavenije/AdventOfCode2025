namespace Aoc2025.Day_03 {
    using System.Linq;
    using static InputParser;
    public static class Day03 {

        const string FILEPATH = "Day_03/input.txt";
        
        public static void Part1() {
            var banks = ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray());
            long sum = 0;
            foreach(var bank in banks)
            {
                sum += SolveBank(bank, 2);
            }
            Console.WriteLine(sum);
        }
        public static void Part2() {
            var banks = ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray());
            long sum = 0;
            foreach(var bank in banks)
            {
                sum += SolveBank(bank, 12);
            }
            Console.WriteLine(sum);
        }
        private static long SolveBank(int[] bank, int requiredBatteries)
        {
            int index = 0;
            long res = 0;
            int bat = requiredBatteries - 1;
            for (int counter = 0; counter <= bat; counter++)
            {
                int largest = 0;
                for (int i = index; i < bank.Length - (bat - counter); i++)
                {
                    if (bank[i] > largest)
                    {
                        largest = bank[i];
                        index = i + 1;
                    }
                }
                res += largest * (long)Math.Pow(10, bat - counter);
            }
            return res;
        }
    }
}
