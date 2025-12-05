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
            foreach (string line in input)
            {
                if (line == "") break;

                string[] items = line.Split('-');
                long start = long.Parse(items[0]);
                long end = long.Parse(items[1]);
                ranges.Add((start, end));
            }
            ranges.Sort();
            List<(long st, long en)> merged = [];
            foreach (var (start, end) in ranges)
            {
                if (merged.Count == 0)
                {
                    merged.Add((start, end));
                }
                else
                {
                    var (st, en) = merged[^1];
                    if (start <= en + 1)
                    {
                        merged[^1] = (st, Math.Max(en, end));
                    }
                    else
                    {
                        merged.Add((start, end));
                    }
                }
            }
            long count = 0;
            foreach (var (start, end) in merged)
            {
                count += end - start + 1;
            }
            Console.WriteLine(count);
        }
    }
}
