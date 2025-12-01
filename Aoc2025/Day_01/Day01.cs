namespace Aoc2025.Day_01 {
    using static InputParser;

    public static class Day01 {
        const string FILEPATH = "Day_01/input.txt";
        public static void Part1() {
            int pos = 50;
            int res = 0;

            foreach ((var dir, var dist) in ParseInput())
            {
                pos = (Op(dir)(pos, dist) + 100) % 100;
                res += pos == 0 ? 1 : 0;
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

                pos = Op(dir)(pos, distMod);
                res += ((pos <= 0 && pos + distMod != 0) || pos >= 100) ? 1 : 0;
                pos = (pos + 100) % 100;
            }
            Console.WriteLine(res);
        }

        private static List<(char,int)> ParseInput() =>
            ParseLinesAsList(FILEPATH, x => {
                char dir = x[0];
                int dist = int.Parse(x[1..]);
                return (dir,dist);
            });

        private static Func<int,int,int> Op(char dir) =>
            dir == 'L' ? (x, y) => x - y : (x, y) => x + y;
    }
}
