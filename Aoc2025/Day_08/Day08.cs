namespace Aoc2025.Day_08 {
    using static InputParser;
    public static class Day08 {
        const string FILEPATH = "Day_08/input.txt";
        public static void Part1() {
            List<(int,int,int)> lines = ParseLinesAsList(FILEPATH, c =>
            {
                int[] nums = c.Split(',').Select(int.Parse).ToArray();
                return (nums[0], nums[1], nums[2]);
            });
            SortedDictionary<double, ((int,int,int) p ,(int,int,int) q)> sortedPairs = [];
            List<HashSet<(int,int,int)>> circuits = [];
            foreach(var p in lines)
            {
                circuits.Add([p]);

                foreach(var q in lines)
                {
                    if (p == q)
                        continue;
                    sortedPairs.TryAdd(EuclidianDistance(p,q),(p,q));
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                var(p,q) = sortedPairs.ElementAt(i).Value;
                var setP = circuits.FirstOrDefault(s => s.Contains(p));
                var setQ = circuits.FirstOrDefault(s => s.Contains(q));
                if (setP != null && setQ != null && setP != setQ)
                {
                    setP.UnionWith(setQ);
                    circuits.Remove(setQ);
                }
            }
            circuits.Sort((a, b) => b.Count.CompareTo(a.Count));
            
            long res = circuits[0].Count * circuits[1].Count * circuits[2].Count;

            Console.WriteLine(res);
        }

        public static void Part2() {
            List<(int,int,int)> lines = ParseLinesAsList(FILEPATH, c =>
            {
                int[] nums = c.Split(',').Select(int.Parse).ToArray();
                return (nums[0], nums[1], nums[2]);
            });
            SortedDictionary<double, ((int,int,int) p ,(int,int,int) q)> sortedPairs = [];
            List<HashSet<(int,int,int)>> circuits = [];
            foreach(var p in lines)
            {
                circuits.Add([p]);

                foreach(var q in lines)
                {
                    if (p == q)
                        continue;
                    sortedPairs.TryAdd(EuclidianDistance(p,q),(p,q));
                }
            }
            long res = -1;
            for (int i = 0; i < 1000000; i++)
            {
                var(p,q) = sortedPairs.ElementAt(i).Value;
                var setP = circuits.FirstOrDefault(s => s.Contains(p));
                var setQ = circuits.FirstOrDefault(s => s.Contains(q));
                if (setP != null && setQ != null && setP != setQ)
                {
                    if (circuits.Count == 2)
                    {
                        res = (long)p.Item1 * q.Item1;
                        break;
                    }
                    setP.UnionWith(setQ);
                    circuits.Remove(setQ);
                }
            }
        

            Console.WriteLine(res);
        }

        private static double EuclidianDistance((int x, int y, int z) p, (int x, int y, int z) q)
        {
            double x = Math.Pow(p.x - q.x, 2);
            double y = Math.Pow(p.y - q.y, 2);
            double z = Math.Pow(p.z - q.z, 2);
            return Math.Sqrt(x+y+z);
        }
    }
}
