namespace Aoc2025
{
    /// <summary>
    /// Utility methods for parsing Advent of Code input files and working with matrices.
    /// </summary>
    public static class InputParser
    {
        /// <summary>Cardinal direction X offsets (N, S, E, W).</summary>
        private static readonly int[] DX_CARDINAL = [-1, 0, 1, 0];
        /// <summary>Cardinal direction Y offsets (N, S, E, W).</summary>
        private static readonly int[] DY_CARDINAL = [0, -1, 0, 1];
        /// <summary>All 8 direction X offsets (including diagonals).</summary>
        private static readonly int[] DX_ALL = [-1, -1, -1, 0, 0, 1, 1, 1];
        /// <summary>All 8 direction Y offsets (including diagonals).</summary>
        private static readonly int[] DY_ALL = [-1, 0, 1, -1, 1, -1, 0, 1];
        /// <summary>
        /// Reads all lines from a file and parses each line using the provided parser function.
        /// </summary>
        /// <typeparam name="T">The type to parse each line into.</typeparam>
        /// <param name="filePath">Relative path to the input file.</param>
        /// <param name="parser">Function to parse each line.</param>
        /// <returns>List of parsed values.</returns>
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

        /// <summary>
        /// Reads all lines from a file as strings.
        /// </summary>
        /// <param name="filePath">Relative path to the input file.</param>
        /// <returns>List of lines as strings.</returns>
        public static List<string> ParseLinesAsList(string filePath) =>
            ParseLinesAsList(filePath, l => l);

        /// <summary>
        /// Parses a 2D matrix from a file, converting each character using the parser function.
        /// Optionally adds a border of specified character and depth around the matrix.
        /// </summary>
        /// <typeparam name="T">The type of the matrix elements.</typeparam>
        /// <param name="filePath">Relative path to the input file.</param>
        /// <param name="parser">Function to parse each character.</param>
        /// <param name="padBorder">Optional border character to pad with.</param>
        /// <param name="borderDepth">Depth of the border to add (default 0).</param>
        /// <returns>2D array representing the parsed matrix.</returns>
        public static T[,] ParseMatrix<T>(string filePath, Func<char, T> parser, char? padBorder = null, int borderDepth = 0)
        {
            var lines = File.ReadAllLines($"{Utils.GetProjectRoot()}/{filePath}");
            int rows = lines.Length;
            int cols = lines[0].Length;
            if (padBorder.HasValue && borderDepth > 0)
            {
                int newRows = rows + 2 * borderDepth;
                int newCols = cols + 2 * borderDepth;
                var matrix = new T[newRows, newCols];
                T pad = parser(padBorder.Value);
                // Fill entire matrix with pad
                for (int i = 0; i < newRows; i++)
                    for (int j = 0; j < newCols; j++)
                        matrix[i, j] = pad;
                // Copy original data into center
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        matrix[i + borderDepth, j + borderDepth] = parser(lines[i][j]);
                return matrix;
            }
            else
            {
                var matrix = new T[rows, cols];
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        matrix[i, j] = parser(lines[i][j]);
                return matrix;
            }
        }

        /// <summary>
        /// Gets the values of all valid neighboring cells (cardinal or all 8 directions) for a given cell in a matrix.
        /// Performs bounds checking.
        /// </summary>
        /// <typeparam name="T">The type of the matrix elements.</typeparam>
        /// <param name="x">X coordinate (column).</param>
        /// <param name="y">Y coordinate (row).</param>
        /// <param name="matrix">2D array to search in.</param>
        /// <param name="includeCorners">Whether to include diagonal neighbors.</param>
        /// <returns>Collection of neighboring values.</returns>
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

        /// <summary>
        /// Gets the values of all neighboring cells (cardinal or all 8 directions) for a given cell in a matrix.
        /// Does NOT perform bounds checking—assumes all neighbors are valid (useful if you padded the matrix).
        /// </summary>
        /// <typeparam name="T">The type of the matrix elements.</typeparam>
        /// <param name="x">X coordinate (column).</param>
        /// <param name="y">Y coordinate (row).</param>
        /// <param name="matrix">2D array to search in.</param>
        /// <param name="includeCorners">Whether to include diagonal neighbors.</param>
        /// <returns>Collection of neighboring values (may throw if out of bounds).</returns>
        public static ICollection<T> UnsafeGetNeighbours<T>(int x, int y, T[,] matrix, bool includeCorners = false)
        {
            List<T> neighbours = [];
            var dx = includeCorners ? DX_ALL : DX_CARDINAL;
            var dy = includeCorners ? DY_ALL : DY_CARDINAL;
            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                neighbours.Add(matrix[nx, ny]);
            }
            return neighbours;
        }

        /// <summary>
        /// Gets the values of all neighboring cells (cardinal or all 8 directions) for a given cell in a matrix.
        /// Does NOT perform bounds checking—assumes all neighbors are valid (useful if you padded the matrix).
        /// </summary>
        /// <typeparam name="T">The type of the matrix elements.</typeparam>
        /// <param name="x">X coordinate (column).</param>
        /// <param name="y">Y coordinate (row).</param>
        /// <param name="matrix">2D array to search in.</param>
        /// <param name="includeCorners">Whether to include diagonal neighbors.</param>
        /// <returns>Collection of neighboring values (may throw if out of bounds).</returns>
        public static int UnsafeCountNeighbours(int x, int y, char[,] matrix, char c, short limit, bool includeCorners = false)
        {
            int count = 0;
            var dx = includeCorners ? DX_ALL : DX_CARDINAL;
            var dy = includeCorners ? DY_ALL : DY_CARDINAL;
            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (matrix[nx, ny] == c && ++count > limit)
                    return 8;
            }
            return count;
        }

        /// <summary>
        /// Parses a 2D matrix of digits from a file (each character is converted to int).
        /// </summary>
        /// <param name="filepath">Relative path to the input file.</param>
        /// <returns>2D array of integers.</returns>
        public static int[,] ParseDigitMatrix(string filepath) =>
            ParseMatrix(filepath, c => c - '0');
        

        /// <summary>
        /// Parses a 2D matrix of characters from a file, with optional border padding.
        /// </summary>
        /// <param name="filePath">Relative path to the input file.</param>
        /// <param name="padBorder">Optional border character to pad with.</param>
        /// <param name="borderDepth">Depth of the border to add (default 0).</param>
        /// <returns>2D array of characters.</returns>
        public static char[,] ParseCharMatrix(string filePath, char? padBorder = null, int borderDepth = 0) =>
            ParseMatrix(filePath, c => c, padBorder, borderDepth);
    }
}