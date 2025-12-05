namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/ex.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, c => c);
            SortedDictionary<long,long> ranges = [];
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
                    if (!ranges.TryGetValue(start, out _))
                        ranges.Add(start,end);
                }
                else
                {
                    foodStuffs.Add(long.Parse(line));
                }
            }
            List<long> keys = ranges.Keys.ToList();
            long freshCounter = 0;

            foreach(long item in foodStuffs)
            {
                // Binary Search the dictionary
                int minI = 0;
                long maxV = ranges.Last().Value;
                int maxI = keys.Count;
                
                while(true)
                {
                    int minoldI = minI;
                    int maxoldI = maxI;
                    int index = (minI + maxI) / 2;
                    long start = keys[index];
                    long end = ranges[start];   
                    if(item >= start && item <= end)
                    {
                        freshCounter++;
                        Console.WriteLine($"{item} is fresh!");
                        break; // Fresh
                    }
                    if(item >= start)
                    {
                        minI = index;
                    }
                    if(item <= end)
                    {
                        maxI = index;
                    }
                    if (minoldI == minI && maxoldI == maxI)
                    {
                        Console.WriteLine($"{item} is spoiled...");
                        break; // not fresh
                    }
                    if (minI == maxI)
                    {
                        break;
                    }

                }
            }
            // TODO: Implement Part 1
            Console.WriteLine(freshCounter);
        }
        public static void Part2() {
            // TODO: Implement Part 2
            Console.WriteLine("Day 05, Part 2: Not implemented yet.");
        }
    }
}
