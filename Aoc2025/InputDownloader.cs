namespace Aoc2025
{
    public static class InputDownloader
    {
        public static async Task<string?> DownloadInputAsync(int year, int day, string savePath)
        {
            string? session = GetSessionFromEnv();
            if (string.IsNullOrEmpty(session))
                throw new InvalidOperationException("AOC_SESSION not set in .env file");
                
            string url = $"https://adventofcode.com/{year}/day/{day}/input";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", $"session={session!}");
            client.DefaultRequestHeaders.Add("User-Agent", "advent-of-code-csharp-client");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var input = await response.Content.ReadAsStringAsync();
            if (input.Contains("Please don't repeatedly request this endpoint before it unlocks!"))
            {
                Console.WriteLine($"Input for day {day} is not yet available.");
                return null;
            }
            string? dir = Path.GetDirectoryName(savePath);
            if (dir == null || !Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Directory does not exist: {dir}");
            await File.WriteAllTextAsync(savePath, input);
            return savePath;
        }

        private static string? GetSessionFromEnv()
        {
            string envPath = $"{Utils.GetProjectRoot()}/.env";
            if (!File.Exists(envPath))
                return null;
            string? sessionLine = File.ReadAllLines(envPath)
                .FirstOrDefault(line => line.StartsWith("AOC_SESSION="));

            if (sessionLine != null)
                return sessionLine.Substring("AOC_SESSION=".Length).Trim();
            return null;
        }
    }
}
