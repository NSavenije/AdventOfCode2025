namespace Aoc2025.Day_11 {
    using static InputParser;
    public static class Day11 {
        const string FILEPATH = "Day_11/input.txt";
        public static void Part1() {
            var lines = ParseLinesAsList(FILEPATH);
            Dictionary<string,List<string>> servers = [];
            foreach(var line in lines)
            {
                var paths = line.Split(':', StringSplitOptions.TrimEntries);
                servers.Add(paths[0], paths[1].Split(' ').ToList());
            }
            Queue<string> queue = [];
            queue.Enqueue("you");
            long output = 0;
            while(queue.Count > 0)
            {
                string s = queue.Dequeue();
                foreach(var server in servers[s])
                {
                    if (server == "out")
                    {
                        output++;
                    }
                    else
                    {
                        queue.Enqueue(server);
                    }
                }
            }
            Console.WriteLine(output);
        }
        public static void Part2() {
            var lines = ParseLinesAsList(FILEPATH);
            Dictionary<string,List<string>> servers = [];
            foreach(var line in lines)
            {
                var paths = line.Split(':', StringSplitOptions.TrimEntries);
                servers.Add(paths[0], paths[1].Split(' ').ToList());
            }
            Queue<(string me, string state)> queue = [];
            queue.Enqueue(("svr", ""));
            var visited = new HashSet<(string, string)>();
            long output = 0;
            while(queue.Count > 0)
            {
                var (s, state) = queue.Dequeue();
                if (!visited.Add((s, state)))
                    continue;
                foreach(var server in servers[s])
                {
                    if (server == "out")
                    {
                        if (state.Contains("fft") && state.Contains("dac"))
                            output++;
                    }
                    else
                    {
                        queue.Enqueue((server, $"{state},{s}"));
                    }
                }
            }
            Console.WriteLine(output);
        }
    }
}
