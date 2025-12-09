namespace Aoc2025.Day_09 {
    using System.Collections.Generic;
    using Microsoft.VisualBasic;
    using static InputParser;
    public static class Day09 {
        
        const string FILEPATH = "Day_09/input.txt";
        public static void Part1() {
            List<(long, long)> input = ParseLinesAsList(FILEPATH, C =>
            {
                var nums = C.Split(',');
                (long,long) output = (long.Parse(nums[0]),long.Parse(nums[1]));
                return output;
            });
            long largest = 0;
            foreach(var (x1,y1) in input)
            {
                foreach(var (x2,y2) in input)
                {
                    long x = Math.Abs(x2 - x1) + 1;
                    long y = Math.Abs(y2 - y1) + 1;
                    long size = x * y;
                    if (size > largest)
                        largest = size;
                }
            }

            Console.WriteLine(largest);
        }
        public static void Part2() {
            List<(long, long)> redTiles = ParseLinesAsList(FILEPATH, C =>
            {
                var nums = C.Split(',');
                (long, long) output = (long.Parse(nums[0]), long.Parse(nums[1]));
                return output;
            });

            var greenTiles = new HashSet<(long, long)>();
            int n = redTiles.Count;
            for (int i = 0; i < n; i++)
            {
                var (x1, y1) = redTiles[i];
                var (x2, y2) = redTiles[(i + 1) % n]; // :(
                if (x1 == x2)
                {
                    long minY = Math.Min(y1, y2);
                    long maxY = Math.Max(y1, y2);
                    for (long y = minY; y <= maxY; y++)
                    {
                        greenTiles.Add((x1, y));
                    }
                }
                else if (y1 == y2)
                {
                    long minX = Math.Min(x1, x2);
                    long maxX = Math.Max(x1, x2);
                    for (long x = minX; x <= maxX; x++)
                    {
                        greenTiles.Add((x, y1));
                    }
                }
            }
            Console.WriteLine($"green: {greenTiles.Count}, red: {redTiles.Count}");
            long largest = 0;
            // For each pair of red tiles, treat as opposite corners
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i}: {largest}");
                for (int j = i + 1; j < n; j++)
                {
                    var (x1, y1) = redTiles[i];
                    var (x2, y2) = redTiles[j];
                    long minX = Math.Min(x1, x2); 
                    long maxX = Math.Max(x1, x2);
                    long minY = Math.Min(y1, y2); 
                    long maxY = Math.Max(y1, y2);
                    long area = (maxX - minX + 1) * (maxY - minY + 1);
                    if (area <= largest) continue;
                    if (IsTileStrictInRectangle(greenTiles, minX, maxX, minY, maxY)) continue;
                    if (IsPointOutsideRectangle(x1,y2)) continue;
                    if (IsPointOutsideRectangle(x2,y1)) continue;

                    largest = area;
                }
            }

            Console.WriteLine(largest);

            bool IsTileStrictInRectangle(HashSet<(long x, long y)> polygon, long minX, long maxX, long minY, long maxY) 
            {
                foreach(var (x,y) in polygon)
                {
                    if(x > minX && x < maxX && y > minY && y < maxY)
                        return true;
                }
                return false;
            }

            bool IsPointOutsideRectangle(long x, long y)
            {
                if (greenTiles.Contains((x, y))) return false;

                int crossings = 0;
                int m = redTiles.Count;
                for (int i = 0; i < m; i++)
                {
                    var (_, y0) = redTiles[(i - 1 + m) % m]; // previous
                    var (x1, y1) = redTiles[i];              // current
                    var (_, y2) = redTiles[(i + 1) % m];     // next
                    var (_, y3) = redTiles[(i + 2) % m];     // next next
                    // Check if we are hitting a corner
                    /*
                    HILL
                    ..........
                    ..1----2..
                    ..|....|..
                    ..|....3..
                    ..0.......

                    VALLEY
                    ..0....3..
                    ..|....|..
                    ..|....|..
                    ..1----2..
                    ..........

                    SQUIGGLE (Stair)
                    .......3..
                    .......|..
                    ..1----2..
                    ..|.......
                    ..0.......

                    SQUIGGLE (Lip)
                    ..0.......
                    ..|.......
                    ..1----2..
                    .......|..
                    .......3..
                    */
                    if (y1 == y2 && y == y1) 
                    {
                        bool isUp1 = y0 < y1;
                        bool isUp2 = y3 < y2;
                        bool isValleyOrHill = isUp1 == isUp2;
                        if (!isValleyOrHill && x > x1) 
                            crossings++;
                    }
                    // Otherwise, check normal edge crossing
                    else if (y < Math.Max(y1, y2) && y > Math.Min(y1, y2) && x > x1) 
                    {
                        crossings++;
                    }
                }
                // Even crossings = outside, odd = inside
                return crossings % 2 == 0;
            }
        }
    }
}
