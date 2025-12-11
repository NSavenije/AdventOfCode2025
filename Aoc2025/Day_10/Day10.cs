namespace Aoc2025.Day_10 {
    using System.ComponentModel.DataAnnotations;
    using static InputParser;
    public static class Day10 {
        const string FILEPATH = "Day_10/input.txt";
        public static void Part1() {
            List<(int[] lights, List<List<int>> buttons, List<int> joltages)> machines = ParseInput(FILEPATH);
            int totalSteps = 0;
            foreach(var (lights,buttons,_) in machines)
            {
                int steps = SolveMachine(lights,buttons);
                totalSteps += steps;
            }
            Console.WriteLine(totalSteps);
        }
        public static void Part2() {
            List<(int[] lights, List<List<int>> buttons, List<int> joltages)> machines = ParseInput(FILEPATH);
            int totalSteps = 0;
            foreach(var (_,buttons,joltages) in machines)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                int steps = SolveMachineRecursive(buttons, joltages);
                sw.Stop();
                Console.WriteLine($"Steps: {steps}, Time: {sw.ElapsedMilliseconds} ms");
                totalSteps += steps;
            }
            Console.WriteLine(totalSteps);

        }

        // Took inspiration from https://topaz.github.io/paste/#XQAAAQBfEQAAAAAAAAARmknGRw8TogB3Oxzri2QTFyHNmsUnNAyI1YK81s6c3NZG/X85xTOH3vQHZLRFdaYS6TF4wGuUeinY4iISwEIMQxWpWzMTB8Pn87YbLvzONSWxC+3xdj36mdQ763FMXn0foMUmANrlkZpszMFRh9zIxAGUtkJhprcccxOpAhzafjOT+39w9UWDtrXrarq5C9tDFhcAUS/MBpvDmCzTFlleb4dVEOM9OcMfg2YIec7pKpuTUKsy94AjJudINie3WGqMnj87RCkgMo/vGqGC5yRJhhefBp8o0Ye38fxc8IuqlUvSD8+j0v5ND0kWSZBEeMnp8+lz6ix0FneQ6mAmmp2ac8jjPuuv79sfkeGyYiJeH1DNcbKG2PnlvQvbV5dSPx/sU6T73SoP0Ce3/xavZgP1EyxH+cECFnGbr5K1acsVqC32q5fWstOFo5d7/Lw5H5WTWWOfakE+QFYkcXxTPJxbcZdQ7Mo9M1LdKvbg/fDmU1Yn7fTpH8rNkFW1mK7KQUWDSuBCd+KX3vrqbbwJfEEQHRUkgalaQ6eyL6gD6M+W3h1Xy0pYb4l3xaAfV9xQD8ZaBEDhtJbxz6LlQeYDAUj9HoFlXYGGHP9oTCd9ahBB6O/4/4k4Zwxy29QLXinJ/RCS3GUh02TPbrb01nPgWmHF32JhyA5QppkvzN1tinxird5tOjdQtCLe8mIXG5+n86ySfzQBTinG8WLD/6pRBsqJMojBgeoNUwtcI1DMJ4xEkyXDxzIhpQdjNEOP0/EzlGSJThENH0dCSeypxGHwaVWWm9784lAvajlU8oSrhdvBoOu9Dpb9h67jsVcsHtpjlbcrL3YphcDm9s+jY2BpMxuZhqsgUl+wCHt6h9sNa+7li8x37/IWR/hOKt0+bz+7TZYJeKYeXO9mASMoSetMGHdflmEInWGryYwh4vN8V6gz2gfMzM3wbCTgT4ejB1fyL/XaABrVh6XHEtublwTqNOxWTfHZXP9OohQR0LF/W+6v1yfHEZS4Zn81RdI6K5Z/nFp+LTx0wxv21SnUiod/eM9izpWmvW742hRMwzu5pUUB+OO0UEvjzctLjzrgtGm7hEpCUcs+Rgj0D83HHmlAMHK5VBe0QbuLisA00kqhXd2yzG20lvGy1+/PkyqtYh+5ixOAJErxG9lkHeEDazQMGbXnGIQi8eoWnfNtcIBryF4CoU4Wo7wh9AnXrxIS6OdaGDR29BwOyfgIh9vJ30PO0jSC+Pv4XMkPzI5IbmKfskJ0dyj1Xu2ljHilvd1TKHQP9rzv/CfMwd1DsdBuvtdVaXEVU7GG4p1ewaN3EeAbaOKEdac+W1jyIl6B8loaYNf2L05V89l33vstugd4pRGIsLHkb3Kenfs3yRVqCmfeS1SJ4ut5hj5vlsitmkBUB7VXmUwNUSdyIplv+iLYp3g751/nK8yb3rgLqBQkRBBgEgNXYlYW68IS1NklA4FPriDtYx/n79R4er6HbnBmf1ElcfbIbsyHrxUacWz+LSTd6YJ4B2j+vw0tiY76Bp/GlROS/cBe5G3ApzsRKe2UhbduUBwJG42uXhMWB6UyeOTB+aqIbR9IcQqo6OKL8TYg8TfXYD0xVrWS7TjX3TQ1RKLGqnr+4RVxaXi/BiKq6KU95veh5iRDPBtyGP/79Cow
        public static int SolveMachineRecursive(List<List<int>> buttons, List<int> joltages)
        {
            // Convert buttons to bitmasks
            int[] masks = new int[buttons.Count];
            for (int i = 0; i < buttons.Count; i++)
            {
                int mask = 0;
                foreach (int counterIdx in buttons[i])
                    mask |= 1 << counterIdx;
                masks[i] = mask;
            }

            int[] numButtonsForJolt = new int[joltages.Count];
            for (int i = 0; i < masks.Length; i++)
                for (int j = 0; j < joltages.Count; j++)
                    if ((masks[i] & (1 << j)) != 0)
                        numButtonsForJolt[j]++;
            
            int bestAnswer = int.MaxValue;
            Search(0b00000000, 0);
            void Search(int usedMask, int pressCount)
            {
                // If all buttons have been used, check if all counters are zero
                if (usedMask == (1 << masks.Length) - 1)
                {
                    if (joltages.All(x => x == 0))
                        bestAnswer = Math.Min(bestAnswer, pressCount);
                    return;
                }

                // If we can't improve, dont.
                if (joltages.Max() + pressCount >= bestAnswer) 
                    return;

                // Choose button, most restrictive first
                int chosenButton = -1;
                int bestCount = int.MaxValue;
                for (int i = 0; i < buttons.Count; i++)
                {
                    if ((usedMask & (1 << i)) != 0) 
                        continue;

                    int count = int.MaxValue;
                    for (int j = 0; j < joltages.Count; j++)
                        if ((masks[i] & (1 << j)) != 0)
                            count = Math.Min(count, numButtonsForJolt[j]);
                    if (count < bestCount)
                    {
                        bestCount = count;
                        chosenButton = i;
                    }
                }

                // Find which counters are affected by the chosen button
                List<int> joltsHit = [];
                for (int i = 0; i < joltages.Count; i++)
                    if ((masks[chosenButton] & (1 << i)) != 0)
                        joltsHit.Add(i);

                // only 1 button hits this jolt, we press until done.
                if (bestCount == 1)
                {
                    int forcedPresses = -1;
                    foreach (int i in joltsHit)
                    {
                        if (numButtonsForJolt[i] == 1)
                        {
                            forcedPresses = joltages[i];
                            break;
                        }
                    }
                    foreach (int i in joltsHit)
                    {
                        if (forcedPresses > joltages[i]) return; // Prune impossible state 
                    }

                    // Apply forced presses, also this button won't be used anymore. 
                    foreach (int i in joltsHit)
                    {
                        joltages[i] -= forcedPresses;
                        numButtonsForJolt[i]--;
                    }
                    Search(usedMask | (1 << chosenButton), pressCount + forcedPresses);

                    // backtracking
                    foreach (int i in joltsHit)
                    {
                        joltages[i] += forcedPresses;
                        numButtonsForJolt[i]++;
                    }
                    return;
                }

                // If not forced, try all possible press counts for the chosen button...
                int maxPresses = int.MaxValue;
                foreach (int i in joltsHit)
                {
                    maxPresses = Math.Min(maxPresses, joltages[i]);
                    numButtonsForJolt[i]--;
                }
                // Try pressing the button maxPresses times
                for(int presses = maxPresses; presses >= 0; presses--)
                {
                    foreach(int i in joltsHit)
                        joltages[i] -= presses;
                    Search(usedMask | (1 << chosenButton), pressCount + presses);
                    // backtracking
                    foreach(int i in joltsHit)
                        joltages[i] += presses;
                }
                //backtracking
                foreach (int i in joltsHit) 
                    numButtonsForJolt[i]++;
            }
            return bestAnswer == int.MaxValue ? -1 : bestAnswer;
        }

        private static int SolveMachine(int[] lights, List<List<int>> buttons)
        {
            // What im thinking will have a complexity of 2^n...
            // At most like 16 buttons per machine, so about 65k, is doable but oof.
            // (0,2,4) (3,4) (0,1,3) = buttons
              
            int n = buttons.Count;
            List<List<List<int>>> combinations = [];
            for (int i = 0; i < (1 << n); i++)
            {
                List<List<int>> combo = [];
                for (int bit = 0; bit < n; bit++)
                {
                    if (((i >> bit) & 1) == 1)
                    {
                        combo.Add(buttons[bit]);
                    }
                }
                combinations.Add(combo);
            }
            int minSteps = int.MaxValue;
            foreach(var combination in combinations)
            {
                if (combination.Count >= minSteps)
                    continue;
                int[] attempt = (int[])lights.Clone();
                List<int> flat = combination.SelectMany(x => x).ToList();
                foreach(int i in flat)
                {
                    attempt[i]++;
                }
                if (attempt.Select(s => s % 2 == 0).All(s => s))
                    minSteps = combination.Count;
            }
            return minSteps;
        }

        private static List<(int[] lights, List<List<int>> buttons, List<int> joltages)> ParseInput(string filepath)
        {
            return ParseLinesAsList(filepath, c =>
            {
                string[] inputs = c.Split(']');
                int[] lights = [.. inputs[0][1..].Select(c => c == '#' ? 1 : 0)];
                List<List<int>> buttons = [];
                var buts = inputs[1].Split('{')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach(string but in buts)
                {
                    buttons.Add([.. but[1..^1].Split(',').Select(int.Parse)]);
                }
                List<int> joltages = inputs[1].Split('{')[1][..^1].Split(',').Select(int.Parse).ToList();
                return (lights, buttons, joltages);
            });
        }
    }
}
