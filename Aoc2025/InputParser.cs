namespace Aoc2025
{
    public static class InputParser
    {
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

        public static int[,] ParseDigitMatrix(string filepath) =>
            ParseMatrix(filepath, c => c - '0');
        

        public static char[,] ParseCharMatrix(string filePath) =>
            ParseMatrix(filePath, c => c);
    }
}