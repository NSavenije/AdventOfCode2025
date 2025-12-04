namespace Aoc2025.Day_04 {
    using System.Data;
    using System.Linq;
    using static InputParser;
    public static class Day04 {
        const string FILEPATH = "Day_04/input.txt";
        public static void Part1() {
            // TODO: Implement Part 1
            char[,] matrix = ParseCharMatrix(FILEPATH);
            int[] dx = [-1, 0, 1, -1, 1, -1, 0, 1];
            int[] dy = [-1, -1, -1, 0,0,1,1,1];
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int res = 0;
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                {
                    if (matrix[x,y] == '@')
                    {
                        int ns = 0;
                        for (int i = 0; i < dx.Length; i++)
                        {
                            int nx = x + dx[i];
                            int ny = y + dy[i];
                            if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
                            {
                                if (matrix[nx, ny] == '@')
                                    ns++;
                            }
                        }
                        if (ns < 4)
                            res++;
                    }

                }
            
            Console.WriteLine(res);
        }
        public static void Part2() {
                        // TODO: Implement Part 1
            char[,] matrix = ParseCharMatrix(FILEPATH);
            int[] dx = [-1, 0, 1, -1, 1, -1, 0, 1];
            int[] dy = [-1, -1, -1, 0,0,1,1,1];
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
                            int ns = 0;
                            for (int i = 0; i < dx.Length; i++)
                            {
                                int nx = x + dx[i];
                                int ny = y + dy[i];
                                if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
                                {
                                    if (matrix[nx, ny] == '@')
                                        ns++;
                                }
                            }
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
