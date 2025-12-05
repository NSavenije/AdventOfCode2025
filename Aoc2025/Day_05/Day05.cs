namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/input.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, c => c);
            SortedDictionary<long,long> freshStarts = [];
            SortedDictionary<long,long> freshEnds = [];
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
                    if (!freshStarts.TryGetValue(start, out _))
                        freshStarts.Add(start,end);
                    if (!freshEnds.TryGetValue(end, out _))
                        freshEnds.Add(end,start);
                }
                else
                {
                    foodStuffs.Add(long.Parse(line));
                }
            }
            List<long> starts = freshStarts.Keys.ToList();
            long freshCounter = 0;

            foreach (long item in foodStuffs)
            {
                int left = 0; // smallest possible index
                int right = freshEnds.Count - 1; // largest possible index
                bool found = false;
                while (left <= right) // if there is still search space available
                {
                    int mid = (left + right) / 2; // try the middle of the search space
                    (long s, long e) ls = (mid, freshStarts[mid]);
                    (long s, long e) rs = (mid, freshEnds[mid]);

                    if (item < Math.Min(ls.s, rs.s))
                    {
                        right = mid - 1;
                    }
                    else if (item > end)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        found = true;
                        Console.WriteLine($"{item} is fresh! {start}-{end}");
                        break;
                        
                    }
                }
                if (found)
                {
                    freshCounter++;
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
