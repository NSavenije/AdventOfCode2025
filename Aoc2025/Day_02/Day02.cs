namespace Aoc2025.Day_02 {
    using System.Linq;
    using static InputParser;
    public static class Day02 {
        const string FILEPATH = "Day_02/input.txt";
        public static void Part1() {
            var input = ParseLinesAsList(FILEPATH, x => x);
            var ranges = input[0].Split(",");
            long sum = 0;
            foreach (var range in ranges)
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
            // TODO: Implement Part 2
            var input = ParseLinesAsList(FILEPATH, x => x);
            var ranges = input[0].Split(",");
            long sum = 0;
            int count = 0;
            foreach (var range in ranges)
            {
                count++;
                Console.WriteLine($"range {count} of {ranges.Length}");
                List<string> knownInvalidIds = [];
                // bool invalid = false;
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                for (long i = start; i <= end; i++)
                {
                    // if (invalid) break;
                    var numstring = i.ToString();
                    var factors = GetFactors(numstring.Length);
                    if (factors.Count == 0)
                        continue;

                    foreach(var factor in factors)
                    {
                        // if (invalid) break;
                        var partLength = numstring.Length / (int)factor;
                        List<string> parts = [];
                        for (int p = 0; p < factor; p++)
                        {
                            parts.Add(numstring[(p * partLength)..((p+1) * partLength)]);
                        }
                        if(parts.All(x => x == parts[0]))
                        {
                            if (knownInvalidIds.Contains(numstring))
                                continue;
                            knownInvalidIds.Add(numstring);
                            sum += i;
                            Console.WriteLine($"{i}, f:{string.Join(',', factors)}");
                            // invalid = true;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
        }

        public static List<int> GetFactors(int n)
        {
            var factors = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                if (n % i == 0)
                {
                    factors.Add(i);
                }
            }
            return factors;
        }
    }
}
