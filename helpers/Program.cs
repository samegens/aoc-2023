namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
    }
}
