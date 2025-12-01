namespace Aoc2025.Day_01 {
    using static InputParser;

    public static class Day01 {
        const string FILEPATH = "Day_01/input.txt";
        public static void Part1() {
            int pos = 50;
            int res = 0;
            foreach ((var dir, var dist) in ParseInput())
            {
                pos = dir == 'L' ? pos - dist : pos + dist;
                pos = ((pos % 100) + 100) % 100;
                if (pos == 0)
                    res++;
            }
            Console.WriteLine(res);
        }
        public static void Part2() {
            int pos = 50;
            int res = 0;
            foreach ((var dir, var dist) in ParseInput())
            {
                res += dist / 100;
                int distMod = dist % 100;

                pos = dir == 'L' ? pos - distMod : pos + distMod;

                if ((dir == 'L' && pos <= 0 && pos + distMod != 0) || (dir == 'R' && pos >= 100))
                    res++;

                pos = ((pos % 100) + 100) % 100;
            }
            Console.WriteLine(res);
        }

        private static List<(char,int)> ParseInput() =>
            ParseLinesAsList(FILEPATH, x => {
                char dir = x[0];
                int dist = int.Parse(x[1..]);
                return (dir,dist);
            });
    }
}
