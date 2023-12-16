namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        Solver solver = new(lines);
        Console.WriteLine($"Part 1: {solver.SolvePart1()}");
        Console.WriteLine($"Part 2: {solver.SolvePart2()}");
    }
}
