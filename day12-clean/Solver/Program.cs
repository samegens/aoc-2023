namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        Solver solver = new(File.ReadAllLines("input.txt"));
        Console.WriteLine($"Part 1: {solver.SolvePart1()}");
        Console.WriteLine($"Part 2: {solver.SolvePart2()}");
    }
}
