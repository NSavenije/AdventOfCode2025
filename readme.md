# Advent of Code 2025

Advent of Code (AoC) is an annual programming event that runs every December, featuring daily coding puzzles that challenge participants to solve problems using creative algorithms and data structures. Each day unlocks a new puzzle with two parts, and solutions can be implemented in any programming language.

This repository contains my solutions for Advent of Code 2025, written in C# as a console application. Each day's solution is organized in its own folder and class, with automated input downloading and performance tracking.

## Usage Instructions

### Prerequisites
- .NET 9.0 SDK or later
- Your Advent of Code session cookie (for input download)

### Setup
1. Clone the repository:
   ```powershell
   git clone https://github.com/yourusername/AdventOfCode2025.git
   cd AdventOfCode2025/Aoc2025
   ```
2. Create a `.env` file in the root directory and add your session cookie:
   ```
   AOC_SESSION=your_session_cookie_here
   ```

### Running Solutions
- **Run today's puzzle (Dec 1-12, 2025):**
  ```powershell
  dotnet run
  ```
- **Run a specific day and part:**
  ```powershell
  dotnet run -- <day> <part>
  # Example: dotnet run -- 05 2
  ```
- **Run both parts for a specific day:**
  ```powershell
  dotnet run -- <day>
  # Example: dotnet run -- 03
  ```
- **Run all available days:**
  ```powershell
  dotnet run
  ```

### Features
- Automatic input file download (requires valid session cookie)
- Execution time tracking for each part and the entire run

### Folder Structure
- `Aoc2025/Day_##/Day##.cs` — Solution classes for each day
- `Aoc2025/InputDownloader.cs` — Handles input file downloads
- `Aoc2025/Program.cs` — Main dispatcher and runner

---
Happy coding and good luck with Advent of Code 2025!
