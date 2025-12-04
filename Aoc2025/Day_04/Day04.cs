namespace Aoc2025.Day_04 {
    using static InputParser;
    public static class Day04 {
        const string FILEPATH = "Day_04/input.txt";
        public static void Part1() =>
            Console.WriteLine(Solve(part2: false));

        public static void Part2() =>
            Console.WriteLine(Solve(part2: true));

        private static int Solve(bool part2)
        {
            char[,] matrix = ParseCharMatrix(FILEPATH, '.', 1);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int res = 0;
            
            System.Collections.Concurrent.ConcurrentBag<(int x, int y)> toUpdate;
            do {
                toUpdate = [];
                Parallel.For(1, rows - 1, y =>
                {
                    for (int x = 1; x < cols - 1; x++)
                    {
                        if (matrix[x, y] == '@' && UnsafeCountNeighbours(x, y, matrix, '@', 3, true) < 4)
                        {
                            toUpdate.Add((x, y));
                        }
                    }
                });
                foreach ((int x, int y) in toUpdate)
                {
                    matrix[x, y] = '.';
                }
                res += toUpdate.Count;
            } while (!toUpdate.IsEmpty && part2);
            return res;
        }
    }
}
