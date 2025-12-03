namespace Aoc2025.Day_03 {
    using System.Linq;
    using static InputParser;
    public static class Day03 {

        const string FILEPATH = "Day_03/input.txt";
        
        public static void Part1() =>
            Console.WriteLine(ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray()).Sum(bank => SolveBank(bank, 1)));
        
        public static void Part2() =>
            Console.WriteLine(ParseLinesAsList(FILEPATH, x => x.Select(c => c - '0').ToArray()).Sum(bank => SolveBank(bank, 11)));

        private static long SolveBank(int[] bank, int l) =>
             Enumerable.Range(0, l + 1)
                .Aggregate((i: 0, res: 0L), (s, c) => 
                    (s.i+Array.IndexOf(bank[s.i..(bank.Length-(l-c))],bank[s.i..(bank.Length-(l-c))].Max())+1,s.res+bank[s.i..(bank.Length-(l-c))].Max()*(long)Math.Pow(10,l-c))).res;
    }
}
