namespace Aoc2025.Day_11 {
    using System.Buffers;
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
            // The path is always svr -> fft -> dac -> out
            // Lets find all options :) and multiply

            // Maybe down to up and then trow away all nodes we dont need?

            var lines = ParseLinesAsList(FILEPATH);
            Dictionary<string,List<string>> servers = [];
            foreach(var line in lines)
            {
                var paths = line.Split(':', StringSplitOptions.TrimEntries);
                servers.Add(paths[0], paths[1].Split(' ').ToList());
            }
            Queue<string> queue = [];
            queue.Enqueue("svr");
            long output = 1;
            Dictionary<string, long> memo = [];
            Console.WriteLine($"starting svr fft {CountPathsDFS("svr", "fft")}");
            output *= CountPathsDFS("svr", "fft");
            
            memo.Clear();
            Console.WriteLine($"starting fft dac {CountPathsDFS("fft", "dac")}");
            output *= CountPathsDFS("fft", "dac");

            memo.Clear();
            Console.WriteLine($"starting dac out {CountPathsBFS("dac", "out")}");
            output *= CountPathsBFS("dac", "out");

            Console.WriteLine("Done");

            long CountPathsBFS(string start, string end)
            {
                Queue<string> queue = [];
                queue.Enqueue(start);
                long output = 0;
                while(queue.Count > 0)
                {
                    var s = queue.Dequeue();
                    foreach(var server in servers[s])
                    {
                        if (server == end)
                        {
                            output++;
                        }
                        else if (server != "out")
                        {
                            queue.Enqueue(server);
                        }
                    }
                }
                return output;
            }

            long CountPathsDFS(string start, string end)
            {
                if (start == end) return 1;
                if (memo.TryGetValue(start, out var cached)) return cached;
                long total = 0;
                foreach (var server in servers[start])
                {
                    if (end != "out" && server != "out")
                        total += CountPathsDFS(server, end);
                }
                memo[start] = total;
                return total;
            }
            Console.WriteLine(output);
        }
    }
}
