namespace Aoc2025.Day_07 {
    using static InputParser;
    public static class Day07
    {
        const string FILEPATH = "Day_07/input.txt";
        // const string FILEPATH = "Day_07/ex.txt";
        public static void Part1()
        {
            var matrix = ParseCharMatrix(FILEPATH);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int start = -1;
            for (int col = 0; col < cols; col++)
            {
                if (matrix[0, col] == 'S')
                {
                    start = col;
                }
            }
            Queue<(int x, int y)> toVisit = new();
            toVisit.Enqueue((0, start));
            HashSet<(int x, int y)> visited = [];
            int splitCount = 0;
            while (toVisit.Count > 0)
            {
                var (row, col) = toVisit.Dequeue();
                if (visited.Contains((row, col)))
                {
                    continue;
                }
                visited.Add((row, col));
                do
                {
                    row++;
                    if (visited.Contains((row, col)))
                    {
                        break;
                    }
                    if (matrix[row, col] == '^')
                    {

                        toVisit.Enqueue((row, col + 1));
                        toVisit.Enqueue((row, col - 1));
                        splitCount++;
                        break;
                    }
                    visited.Add((row, col));
                } while (row < rows - 1);
            }
            Console.WriteLine(splitCount);
        }

        public static void Part2()
        {
            var matrix = ParseCharMatrix(FILEPATH);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int start = -1;
            for (int col = 0; col < cols; col++)
            {
                if (matrix[0, col] == 'S')
                {
                    start = col;
                }
            }

            Dictionary<(int row, int col), long> memo = [];

            long Search(int row, int col)
            {
                if (row == rows - 1)
                    return 1;
                if (memo.TryGetValue((row, col), out var cached))
                    return cached;
                long total = 0;
                if (matrix[row, col] == '^')
                {
                    total += Search(row, col - 1);
                    total += Search(row, col + 1);
                }
                else
                {
                    total += Search(row + 1, col);
                }
                memo[(row, col)] = total;
                return total;
            }

            var result = Search(0, start);
            Console.WriteLine(result);
        }
        
    }
}
