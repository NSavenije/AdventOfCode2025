namespace Aoc2025.Day_03 {
    using System.Linq;
    using static InputParser;
    public static class Day03 {
        public static void Part1() =>
            Console.WriteLine(ParseLinesAsList("Day_03/input.txt", x => x.Select(c => c - '0').ToArray()).Sum(b => Enumerable.Range(0, 2).Aggregate((i: 0, res: 0L), (s, c) => (s.i+Array.IndexOf(b[s.i..(b.Length-(1-c))],b[s.i..(b.Length-(1-c))].Max())+1,s.res+b[s.i..(b.Length-(1-c))].Max()*(long)Math.Pow(10,1-c))).res));
        
        public static void Part2() =>
            Console.WriteLine(ParseLinesAsList("Day_03/input.txt", x => x.Select(c => c - '0').ToArray()).Sum(b => Enumerable.Range(0, 12).Aggregate((i: 0, res: 0L), (s, c) => (s.i+Array.IndexOf(b[s.i..(b.Length-(11-c))],b[s.i..(b.Length-(11-c))].Max())+1,s.res+b[s.i..(b.Length-(11-c))].Max()*(long)Math.Pow(10,11-c))).res));
    }
}
