namespace Aoc2025.Day_04 {
    using System.Linq;
    using static InputParser;
    public static class Day04 {
        const string FILEPATH = "Day_04/input.txt";
        public static void Part1() {
            char[,] matrix = ParseCharMatrix(FILEPATH);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int res = 0;
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                {
                    if (matrix[x,y] == '@')
                    {
                        int ns = GetNeighbours(x, y, matrix, true).Count(c => c == '@');
                        if (ns < 4)
                            res++;
                    }

                }
            Console.WriteLine(res);
        }
        public static void Part2() {
            char[,] matrix = ParseCharMatrix(FILEPATH);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int res = 0;
            bool active = true;
            while(active)
            {
                active =false;
                for (int y = 0; y < rows; y++)
                    for (int x = 0; x < cols; x++)
                    {
                        if (matrix[x,y] == '@')
                        {
                            int ns = GetNeighbours(x, y, matrix, true).Count(c => c == '@');
                            if (ns < 4)
                            {
                                active = true;
                                matrix[x,y] = '.';
                                res++;
                            }
                        }

                    }
            }
            Console.WriteLine(res);
        }
    }
}
