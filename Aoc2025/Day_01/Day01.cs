namespace Aoc2025.Day_01 {
    using static InputParser;

    public static class Day01 {
        const string FILEPATH = "Day_01/input.txt";
        public static void Part1() {
            // TODO: Implement Part 1
            var inputs = ParseLinesAsList(FILEPATH, x => {
                return x;
            });
            var matrix = ParseMatrix(FILEPATH, x => x - '0');
        }
        public static void Part2() {
            // TODO: Implement Part 2
            Console.WriteLine("Day 01, Part 2: Not implemented yet.");
        }
    }
}
