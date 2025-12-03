namespace Aoc2025.Day_03 {
    using System.Linq;
    using static InputParser;
    public static class Day03 {

        const string FILEPATH = "Day_03/input.txt";
        
        public static void Part1() =>
            Console.WriteLine(ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray()).Sum(bank => SolveBank(bank, 2)));
        
        public static void Part2() =>
            Console.WriteLine(ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray()).Sum(bank => SolveBank(bank, 12)));

        private static long SolveBank(int[] bank, int requiredBatteries) =>
             Enumerable.Range(0, requiredBatteries)
                .Aggregate((index: 0, res: 0L), (state, counter) => 
                    UpdateIndexAndRes(bank, state.index, state.res, requiredBatteries - 1, counter)).res;

        private static (int, long) UpdateIndexAndRes(int[] bank, int index, long res, int bat, int counter)
        {
            var slice = bank[index..(bank.Length - (bat - counter))];
            int largest = slice.Max();
            return (index + Array.IndexOf(slice, largest) + 1, res + largest * (long)Math.Pow(10, bat - counter));
        }
    }
}
