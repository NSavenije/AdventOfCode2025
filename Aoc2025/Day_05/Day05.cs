namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/input.txt";
        public static void Part1() {
            var (ranges, ids) = ParseInput(true);
            
            long freshCounter = 0;
            foreach (long item in ids)
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
            var (ranges, _) = ParseInput(false);
            long count = 0;
            long pos = 0;
            foreach((long start, long end) in ranges)
            {
                count += Math.Max(0, end - Math.Max(start - 1, pos));
                pos = Math.Max(end, pos);
            }
            Console.WriteLine(count);
        }

        private static (List<(long,long)> ranges, List<long> ids) ParseInput(bool parseIds = true)
        {
            var input = ParseLinesAsList(FILEPATH, c => c);
            List<(long st,long en)> ranges = [];
            List<long> ids = [];

            bool parsingRanges = true;
            foreach (string line in input)
            {
                if (line == "")
                {
                    parsingRanges = false;  
                    if (parseIds)
                        continue;
                    else 
                        break;
                } 
                if (parsingRanges)
                {
                    string[] items = line.Split('-');
                    ranges.Add((long.Parse(items[0]), long.Parse(items[1])));
                }
                else
                {
                    ids.Add(long.Parse(line));
                }
            }
            ranges.Sort();
            return (ranges, ids);
        }
    }
}
