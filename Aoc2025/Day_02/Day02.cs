namespace Aoc2025.Day_02 {
    using System.Linq;
    using static InputParser;
    public static class Day02 {
                
        const string FILEPATH = "Day_02/input.txt";

        public static void Part1() =>
            Solve(all: false);

        public static void Part2() =>
            Solve(all: true);   

        private static void Solve(bool all = false)
        {
            var ranges = ParseLinesAsList(FILEPATH, x => x.Split(','))[0];
            long sum = 0;
            Parallel.ForEach(ranges, range =>
            {
                HashSet<string> knownInvalidIds = [];
                var spl = range.Split('-');
                long start = long.Parse(spl[0]);
                long end = long.Parse(spl[1]);
                long localSum = 0;
                for (long i = start; i <= end; i++)
                {
                    var numstring = i.ToString();
                    var factors = all ? GetFactors(numstring.Length) : GetBinaryFactors(numstring.Length);
                    localSum += ProcessNumber(numstring, factors, knownInvalidIds, i);
                }
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine(sum);
        }

        private static long ProcessNumber(string numstring, List<int> factors, HashSet<string> knownInvalidIds, long i)
        {
            var validFactor = factors.FirstOrDefault(factor => IsAllPartsEqual(numstring, factor));
            if (validFactor != 0)
            {
                if (knownInvalidIds.Contains(numstring))
                    return 0;
                knownInvalidIds.Add(numstring);
                return i;
            }
            return 0;
        }

        private static bool IsAllPartsEqual(string numstring, int factor)
        {
            var partLength = numstring.Length / factor;
            var span = numstring.AsSpan();
            var firstPart = span[..partLength];
            for (int p = 1; p < factor; p++)
                if (!firstPart.SequenceEqual(span.Slice(p * partLength, partLength)))
                    return false;
            return true;
        }

        private static List<int> GetFactors(int n)
        {
            var factors = new List<int>();
            for (int i = 2; i <= n; i++)
                if (n % i == 0) factors.Add(i);
            return factors;
        }

        private static List<int> GetBinaryFactors(int n)
        {
            var factors = new List<int>();
            if (n % 2 == 0 && n > 1) factors.Add(2);
            return factors;
        }
    }
}
