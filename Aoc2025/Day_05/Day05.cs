namespace Aoc2025.Day_05 {
    using static InputParser;
    public static class Day05 {
        const string FILEPATH = "Day_05/input.txt";
        public static void Part1() {
            var (ranges, ids) = ParseInput(true);
            List<(long, long)> merged = MergeRanges(ranges);
            long freshCounter = 0;

            foreach (long id in ids)
            {
                int l = 0;
                int r = merged.Count - 1;
                while (l <= r)
                {
                    int mid = l + (r - l) / 2;
                    (long st, long en) = merged[mid];
                    if (id < st)
                    {
                        r = mid - 1;
                    }
                    else if (id > en)
                    {
                        l = mid + 1;
                    }
                    else
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

        private static List<(long, long)> MergeRanges(List<(long,long)> ranges)
        {
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
            return merged;
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
