namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/input.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, c => c);
            List<(long st,long en)> ranges = [];
            bool parsingRanges = true;
            List<long> foodStuffs = [];
            foreach (string line in input)
            {
                if (line == "")
                {
                    parsingRanges = false;  
                    continue;
                } 
                if (parsingRanges)
                {
                    string[] items = line.Split('-');
                    long start = long.Parse(items[0]);
                    long end = long.Parse(items[1]);
                    ranges.Add((start, end));
                }
                else
                {
                    foodStuffs.Add(long.Parse(line));
                }
            }
            long freshCounter = 0;
            ranges.Sort();

            foreach (long item in foodStuffs)
            {
                foreach((long start, long end) in ranges)
                {
                    if( item >= start && item <= end)
                    {
                        freshCounter++;
                        break;
                    }
                }
            }
            Console.WriteLine(freshCounter);
        }
        public static void Part2() {
            // TODO: Implement Part 2
            Console.WriteLine("Day 05, Part 2: Not implemented yet.");
        }
    }
}
