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
            // TODO: Implement Part 2
            Console.WriteLine("Day 11, Part 2: Not implemented yet.");
        }
    }
}
