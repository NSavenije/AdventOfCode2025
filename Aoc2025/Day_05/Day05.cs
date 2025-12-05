namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/input.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, c => c);
            SortedDictionary<long,long> freshStarts = [];
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
                    if (!freshStarts.TryGetValue(start, out long otherEnd))
                        freshStarts.Add(start,end);
                    else if(otherEnd > end)
                        freshStarts[start] = otherEnd;

                }
                else
                {
                    foodStuffs.Add(long.Parse(line));
                }
            }
            long freshCounter = 0;

            foreach (long item in foodStuffs)
            {
                foreach(long start in freshStarts.Keys)
                {
                    if( start > item )
                        break;
                    long end = freshStarts[start];
                    if (end >= item)
                    {
                        freshCounter++;
                        // Console.WriteLine($"{item} found {start}-{end}");
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
