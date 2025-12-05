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
            var input = ParseLinesAsList(FILEPATH, c => c);
            List<(long st,long en)> ranges = [];

            bool parsingRanges = true;

            foreach (string line in input)
            {
                if (line == "")
                {
                    break;
                } 
                if (parsingRanges)
                {
                    string[] items = line.Split('-');
                    long start = long.Parse(items[0]);
                    long end = long.Parse(items[1]);
                    ranges.Add((start, end));
                }
            }
            ranges.Sort();
            long count = 0;
            long pos = 0;
            foreach((long start, long end) in ranges)
            {
                count += Math.Max(0, end - Math.Max(start - 1, pos));
                pos = end;
            }
            Console.WriteLine(count);
        }
    }
}
