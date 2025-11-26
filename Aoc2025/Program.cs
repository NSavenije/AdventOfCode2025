// Advent of Code 2025 Dispatcher
using System.Reflection;

namespace Aoc2025;
static class Program
{
    const int YEAR = 2025;
	static void Main(string[] args)
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();
		var today = DateTime.Today;
		int year = today.Year;
		int month = today.Month;
		int day = today.Day;

		// Advent of Code runs Dec 1-12 this year
		if (year == YEAR && month == 12 && day >= 1 && day <= 12)
		{
			string dayStr = day.ToString().PadLeft(2, '0');
			RunDay(dayStr);
		}
		else if (args.Length == 2)
		{
			string dayStr = args[0].PadLeft(2, '0');
			string part = args[1];
			RunDay(dayStr, part);
		}
		else if (args.Length == 1)
		{
			string dayStr = args[0].PadLeft(2, '0');
			RunDay(dayStr);
		}
		else if (args.Length > 2)
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("  dotnet run -- <day> <part>   # Run specific day and part");
			Console.WriteLine("  dotnet run -- <day>          # Run both parts for a specific day");
			Console.WriteLine("  dotnet run                   # Auto-run today's puzzle if Dec 1-12 2025, else run all days");
		}
		else
		{
			// Run all available days (1-12)
			for (int d = 1; d <= 12; d++)
			{
				string dayStr = d.ToString().PadLeft(2, '0');
				RunDay(dayStr);
			}
		}
		sw.Stop();
		Console.WriteLine($"Total execution time: {sw.ElapsedMilliseconds} ms");
	}

	static void RunDay(string day, string? part = null)
	{
		string className = $"Aoc2025.Day_{day}.Day{day}";
		var assembly = Assembly.GetExecutingAssembly();
		var type = assembly.GetType(className);
		if (type == null)
		{
			Console.WriteLine($"No implementation found for {className}");
			return;
		}

		string inputPath = $"Day_{day}/input.txt";
		EnsureInputDownloaded(day, inputPath);

		if (string.IsNullOrEmpty(part))
			RunBothParts(type, day, className);
		else
			RunSinglePart(type, day, part!, className);
	}

	static void EnsureInputDownloaded(string day, string inputPath)
	{
		if (!File.Exists(inputPath))
		{
			try
			{
				Task<string?> task = InputDownloader.DownloadInputAsync(YEAR, int.Parse(day), inputPath);
				if (!task.Wait(5000))
					Console.WriteLine($"Input download for Day {day} timed out, grab it from https://adventofcode.com/{YEAR}/day/{day}/input");
				else
					Console.WriteLine($"Input download result for Day {day}: {task.Result ?? "No input created."}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Input download for Day {day} failed: {ex.Message}");
			}
		}
	}

	static void RunBothParts(Type type, string day, string className)
	{
		for (int p = 1; p <= 2; p++)
			RunSinglePart(type, day, p.ToString(), className);
	}

	static void RunSinglePart(Type type, string day, string part, string className)
	{
		var method = type.GetMethod($"Part{part}");
		if (method != null)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			Console.WriteLine($"Day {day} - Part {part}:");
			method.Invoke(null, null);
			sw.Stop();
			Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
		}
		else
		{
			Console.WriteLine($"No method Part{part} found in {className}");
		}
	}
}
