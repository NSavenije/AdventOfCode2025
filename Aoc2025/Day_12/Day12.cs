namespace Aoc2025.Day_12 {
    using static InputParser;
    public static class Day12 {
        const string FILEPATH = "Day_12/ex.txt";
        public static void Part1() {
            var ls = ParseLinesAsList(FILEPATH);
            char[][,] shapes = new char[6][,];
            List<(int, int, int[])> puzzles = [];
            int si = 0;
            for(int i = 0; i < ls.Count; i++)
            {
                if(ls[i].Length == 2)
                {
                    char[,] shape = new char[3,3];
                    for(int r=0;r<3;r++)
                    {
                        var line = ls[i+1+r];
                        for(int c=0;c<3;c++)
                            shape[r,c] = line[c];
                    }
                    shapes[si] = shape;
                    i += 4;
                    si++;
                    continue;
                }
                var parts = ls[i].Split(':', StringSplitOptions.TrimEntries);
                var dims = parts[0].Split('x').Select(int.Parse).ToArray();
                var counts = parts[1].Split(' ').Select(int.Parse).ToArray();
                puzzles.Add((dims[0],dims[1],counts));
            }
            // Precompute all orientations for each shape
            List<List<char[,]>> shapeOrientations = new();
            for (int i = 0; i < shapes.Length; i++)
            {
                shapeOrientations.Add(GetAllOrientations(shapes[i]));
            }
            int total = 0;
            foreach((int w,int h, int[] cs) in puzzles)
            {
                char[,] matrix = new char[w,h];   
                for(int r = 0; r < h; r++)
                    for(int c = 0; c < w; c++)
                        matrix[c,r] = '.';

                List<int> presents = [];
                for(int i = 0; i < cs.Length; i++)
                    for(int j = 0; j < cs[i]; j++)
                        presents.Add(i);

                bool canPack = CanPack(matrix, presents);
                if (canPack)
                    total++;
                Console.WriteLine($"Room {w}x{h} can pack: {canPack}");
            }

            bool CanPack(char[,] matrix, List<int> presents, int idx=0)
            {
                int w = matrix.GetLength(0);
                int h = matrix.GetLength(1);
                if(idx == presents.Count) return true;
                var orientations = shapeOrientations[presents[idx]];
                for(int oi = 0; oi < orientations.Count; oi++)
                {
                    var shape = orientations[oi];
                    for(int x = 0;x <= w - 3; x++)
                    {
                        for(int y = 0; y <= h - 3; y++)
                        {
                            if(CanPlace(matrix, shape, x, y))
                            {
                                Place(matrix, shape, x, y, '#');
                                if(CanPack(matrix, presents, idx+1)) return true;
                                Place(matrix, shape, x, y, '.'); // backtrack
                            }
                        }
                    }
                }
                return false;
            }

            static bool CanPlace(char[,] matrix, char[,] shape, int x, int y)
            {
                for(int i = 0; i < 3; i++)
                    for(int j = 0; j < 3; j++)
                        if(shape[i,j] == '#' && matrix[x+i, y+j] != '.') 
                            return false;
                return true;
            }

            static void Place(char[,] matrix, char[,] shape, int x, int y, char c)
            {
                for(int i = 0; i < 3; i++)
                    for(int j = 0; j < 3; j++)
                        if(shape[i,j]=='#') 
                            matrix[x+i, y+j] = c;
            }

            // TODO: Implement Part 1
            Console.WriteLine(total);
        }


        public static void Part2() {
                    
            // TODO: Implement Part 2
            Console.WriteLine("Day 12, Part 2: No puzzle, Merry Christmas!.");
        }

        static List<char[,]> GetAllOrientations(char[,] shape)
        {
            List<char[,]> variants = [];
            void AddIfUnique(char[,] s)
            {
                foreach(var v in variants)
                {
                    bool same = true;
                    for(int i = 0;i < 3; i++) 
                        for(int j = 0; j < 3; j++) 
                            if(v[i, j] != s[i, j]) 
                                same = false;
                    if(same) 
                        return;
                }
                variants.Add(DeepCopy(s));
            }

            for(int rot = 0; rot < 4; rot++)
            {
                var cur = DeepCopy(shape);
                // rotate
                for(int r=0;r<rot;r++)
                {
                    char[,] tmp = new char[3,3];
                    for(int x = 0; x < 3; x++) 
                        for(int y = 0; y < 3; y++) 
                            tmp[y,2-x] = cur[x,y];
                    cur = tmp;
                }
                AddIfUnique(cur);
                // flip horizontally
                char[,] flipH = new char[3,3];
                for(int i = 0; i < 3; i++) 
                    for(int j = 0; j < 3; j++) 
                        flipH[i,j] = cur[i,2-j];
                AddIfUnique(flipH);
                // flip vertically
                char[,] flipV = new char[3,3];
                for(int i = 0; i < 3; i++) 
                    for(int j = 0; j < 3; j++) 
                        flipV[i,j] = cur[2-i,j];
                AddIfUnique(flipV);
            }
            return variants;
        }

        static char[,] DeepCopy(char[,] src) 
        {
            var dst = new char[3,3];
            for(int i = 0; i < 3; i++)
                for(int j = 0; j < 3; j++)
                    dst[i,j] = src[i,j];
            return dst;
        }
    }
}
