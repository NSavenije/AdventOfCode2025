namespace Aoc2025
{
    public static class InputParser
    {
        private static readonly int[] DX_CARDINAL = [-1, 0, 1, 0];
        private static readonly int[] DY_CARDINAL = [0, -1, 0, 1];
        private static readonly int[] DX_ALL = [-1, -1, -1, 0, 0, 1, 1, 1];
        private static readonly int[] DY_ALL = [-1, 0, 1, -1, 1, -1, 0, 1];
        public static List<T> ParseLinesAsList<T>(string filePath, Func<string, T> parser)
        {
            string[] lines = File.ReadAllLines($"{Utils.GetProjectRoot()}/{filePath}");
            List<T> result = [];
            foreach (var line in lines)
            {
                result.Add(parser(line));
            }
            return result;
        }

        public static List<string> ParseLinesAsList(string filePath) =>
            ParseLinesAsList(filePath, l => l);

        public static T[,] ParseMatrix<T>(string filePath, Func<char, T> parser)
        {
            var lines = File.ReadAllLines($"{Utils.GetProjectRoot()}/{filePath}");
            int rows = lines.Length;
            int cols = lines[0].Length;
            var matrix = new T[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = parser(lines[i][j]);
            return matrix;
        }

        public static ICollection<T> GetNeighbours<T>(int x, int y, T[,] matrix, bool includeCorners = false)
        {
            List<T> neighbours = [];
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var dx = includeCorners ? DX_ALL : DX_CARDINAL;
            var dy = includeCorners ? DY_ALL : DY_CARDINAL;
            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
                    neighbours.Add(matrix[nx, ny]);
            }
            return neighbours;
        }

        public static int[,] ParseDigitMatrix(string filepath) =>
            ParseMatrix(filepath, c => c - '0');
        

        public static char[,] ParseCharMatrix(string filePath) =>
            ParseMatrix(filePath, c => c);
    }
}