namespace Aoc2025.Day_02 {
    using System.Linq;
    using static InputParser;
    public static class Day02 {
                
        const string FILEPATH = "Day_02/input.txt";
        public static void Part1() {
            long sum = 0;
            var ranges = ParseLinesAsList(FILEPATH, x => x.Split(','))[0];
            System.Threading.Tasks.Parallel.ForEach(ranges, range =>
            {
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                long localSum = 0;
                for (long i = start; i <= end; i++)
                {
                    var numstring = i.ToString();
                    if (numstring.Length % 2 == 1)
                    {
                        if (numstring.Length == end.ToString().Length)
                            break;
                        else
                        {
                            i = (long)Math.Pow(10, numstring.Length);
                            continue;
                        }
                    }
                    var halfLength = numstring.Length / 2;
                    if (numstring[..halfLength] == numstring[halfLength..])
                        localSum += long.Parse(numstring);
                }
                System.Threading.Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine(sum);
        }
        public static void Part2() {
            var ranges = ParseLinesAsList(FILEPATH, x => x.Split(','))[0];
            long sum = 0;
            System.Threading.Tasks.Parallel.ForEach(ranges, range =>
            {
                HashSet<string> knownInvalidIds = [];
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                long localSum = 0;
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
                            localSum += i;
                        }
                    }
                }
                System.Threading.Interlocked.Add(ref sum, localSum);
            });
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
