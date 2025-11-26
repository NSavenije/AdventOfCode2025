namespace Aoc2025 {
    public static class Utils {

        public static string GetProjectRoot() => 
            Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
    }
}
