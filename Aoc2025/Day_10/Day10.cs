namespace Aoc2025.Day_10 {
    using System.ComponentModel.DataAnnotations;
    using static InputParser;
    public static class Day10 {
        const string FILEPATH = "Day_10/input.txt";
        public static void Part1() {
            List<(int[] lights, List<List<int>> buttons, List<int> joltages)> machines = ParseLinesAsList(FILEPATH, c =>
            {
                string[] inputs = c.Split(']');
                int[] lights = [.. inputs[0][1..].Select(c => c == '#' ? 1 : 0)];
                List<List<int>> buttons = [];
                var buts = inputs[1].Split('{')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach(string but in buts)
                {
                    buttons.Add([.. but[1..^1].Split(',').Select(int.Parse)]);
                }
                List<int> joltages = [];
                return (lights, buttons, joltages);
            });
            int totalSteps = 0;
            foreach(var (lights,buttons,_) in machines)
            {
                int steps = SolveMachine(lights,buttons);
                Console.WriteLine(steps);
                totalSteps += steps;
            }
            
            Console.WriteLine(totalSteps);
        }
        public static void Part2() {
            // TODO: Implement Part 2
            Console.WriteLine("Day 10, Part 2: Not implemented yet.");
        }

        private static int SolveMachine(int[] lights, List<List<int>> buttons)
        {
            // What im thinking will have a complexity of 2^n...
            // At most like 16 buttons per machine, so about 65k, is doable but oof.
            // (0,2,4) (3,4) (0,1,3) = buttons
              
            int n = buttons.Count;
            List<List<List<int>>> combinations = [];
            for (int i = 0; i < (1 << n); i++)
            {
                List<List<int>> combo = [];
                for (int bit = 0; bit < n; bit++)
                {
                    if (((i >> bit) & 1) == 1)
                    {
                        combo.Add(buttons[bit]);
                    }
                }
                combinations.Add(combo);
            }
            int minSteps = int.MaxValue;
            foreach(var combination in combinations)
            {
                if (combination.Count >= minSteps)
                    continue;
                int[] attempt = (int[])lights.Clone();
                List<int> flat = combination.SelectMany(x => x).ToList();
                foreach(int i in flat)
                {
                    attempt[i]++;
                }
                if (attempt.Select(s => s % 2 == 0).All(s => s))
                    minSteps = combination.Count;
            }
            return minSteps;
        }
    }
}
