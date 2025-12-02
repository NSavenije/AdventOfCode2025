namespace Aoc2025.Day_02 {
    using System.Linq;
    using static InputParser;
    public static class Day02 {
                
        const string FILEPATH = "Day_02/input.txt";
        public static void Part1() {
            var ranges = ParseLinesAsList(FILEPATH, x => x.Split(','));
            long sum = 0;
            foreach (var range in ranges[0])
            {
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                for (long i = start; i <= end; i++)
                {
                    var numstring = i.ToString();
                    if (numstring.Length % 2 == 1)
                        continue;

                    var halfLength = numstring.Length / 2;
                    var firstHalf = numstring[..halfLength];
                    var secondHalf = numstring[halfLength..];
                    if (firstHalf == secondHalf)
                    {
                        
                        sum += long.Parse(numstring);
                    }                    
                }
            }
            Console.WriteLine(sum);
        }
        public static void Part2() {
            var ranges = ParseLinesAsList(FILEPATH, x => x.Split(','));
            long sum = 0;
            foreach (var range in ranges[0])
            {
                HashSet<string> knownInvalidIds = [];
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                for (long i = start; i <= end; i++)
                {
                    var numstring = i.ToString();
                    foreach(var factor in FactorCache[numstring.Length])
                    {
                        var partLength = numstring.Length / factor;
                        var span = numstring.AsSpan();
                        var firstPart = span[..partLength];
                        bool allEqual = true;
                        for (int p = 1; p < factor; p++)
                        {
                            if (!firstPart.SequenceEqual(span.Slice(p * partLength, partLength)))
                            {
                                allEqual = false;
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            if (knownInvalidIds.Contains(numstring))
                                continue;
                            knownInvalidIds.Add(numstring);
                            sum += i;
                        }
                    }
                }
            }
            Console.WriteLine(sum);
        }


        private static readonly Dictionary<int, List<int>> FactorCache = new()
        {
            [1] = [],
            [2] = [2],
            [3] = [3],
            [4] = [2, 4],
            [5] = [5],
            [6] = [2, 3, 6],
            [7] = [7],
            [8] = [2, 4, 8],
            [9] = [3, 9],
            [10] = [2, 5, 10],
            [11] = [11],
            [12] = [2, 3, 4, 6, 12],
            [13] = [13],
            [14] = [2, 7, 14],
            [15] = [3, 5, 15],
            [16] = [2, 4, 8, 16],
            [17] = [17],
            [18] = [2, 3, 6, 9, 18],
            [19] = [19],
            [20] = [2, 4, 5, 10, 20]
        };
    }
}
