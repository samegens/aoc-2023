namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        Console.WriteLine($"Part 1: {Solver.SolvePart1(lines)}");
        Console.WriteLine($"Part 2: {Solver.SolvePart2(lines)}");
    }
}
