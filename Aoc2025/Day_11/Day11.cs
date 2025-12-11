namespace Aoc2025.Day_11 {
    using static InputParser;
    public static class Day11 {
        const string FILEPATH = "Day_11/input.txt";

        public static void Part1() =>
            Console.WriteLine(CountPathsDFS(ParseInput(), [], "you", "out"));

        public static void Part2() {
            var servers = ParseInput();
            long output = 1;
            output *= CountPathsDFS(servers, [], "svr", "fft");
            output *= CountPathsDFS(servers, [], "fft", "dac");
            output *= CountPathsDFS(servers, [], "dac", "out");
            Console.WriteLine(output);
        }

        private static long CountPathsDFS(Dictionary<string, List<string>> servers, Dictionary<string, long> memo, string start, string end)
        {
            if (start == end) return 1;
            if (memo.TryGetValue(start, out var cached)) return cached;
            long total = 0;
            foreach (var server in servers[start])
                if (end == "out" || server != "out")
                    total += CountPathsDFS(servers, memo, server, end);
            memo[start] = total;
            return total;
        }

        private static Dictionary<string, List<string>> ParseInput() =>
            ParseLinesAsList(FILEPATH)
                .Select(line => line.Split(':', StringSplitOptions.TrimEntries))
                .ToDictionary(parts => parts[0], parts => parts[1].Split(' ').ToList());
    }
}
