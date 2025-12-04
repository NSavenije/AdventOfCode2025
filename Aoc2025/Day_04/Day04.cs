namespace Aoc2025.Day_04 {
    using System.Linq;
    using static InputParser;
    public static class Day04 {
        const string FILEPATH = "Day_04/input.txt";
        public static void Part1() =>
            Console.WriteLine(Solve(part2: false));

        public static void Part2() =>
            Console.WriteLine(Solve(part2: true));

        private static int Solve(bool part2)
        {
            char[,] matrix = ParseCharMatrix(FILEPATH);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int res = 0;
            
            HashSet<(int x,int y)> toUpdate;
            do {
                toUpdate = [];
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        if (matrix[x,y] == '@' && GetNeighbours(x, y, matrix, true).Count(c => c == '@') < 4)
                        {
                            toUpdate.Add((x,y));
                        }
                    }
                }
                foreach((int x, int y) in toUpdate)
                {
                    matrix[x,y] = '.';
                }
                res += toUpdate.Count;
            } while (toUpdate.Count > 0 && part2);
            return res;
        }
    }
}
