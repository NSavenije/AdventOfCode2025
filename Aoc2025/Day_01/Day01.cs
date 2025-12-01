namespace Aoc2025.Day_01 {
    using static InputParser;

    public static class Day01 {
        const string FILEPATH = "Day_01/input.txt";
        public static void Part1() {
            var inputs = ParseLinesAsList(FILEPATH, x => {
                return x;
            });
            int pos = 50;
            int res = 0;
            foreach (var rot in inputs)
            {
                char dir = rot[0];
                int dist = int.Parse(rot[1..]);
                pos = dir == 'L' ? pos - dist : pos + dist;
                pos = ((pos % 100) + 100) % 100;
                if (pos == 0)
                    res++;
            }
            Console.WriteLine(res);
        }
        public static void Part2() {
            var inputs = ParseLinesAsList(FILEPATH, x => {
                return x;
            });
            int pos = 50;
            int res = 0;
            foreach (var rot in inputs)
            {
                char dir = rot[0];
                int dist = int.Parse(rot[1..]);
                res += dist / 100;
                dist %= 100;

                pos = dir == 'L' ? pos - dist : pos + dist;

                if ((dir == 'L' && pos <= 0 && pos + dist != 0) || (dir == 'R' && pos >= 100))
                    res++;

                pos = ((pos % 100) + 100) % 100;
            }
            Console.WriteLine(res);
        }
    }
}
