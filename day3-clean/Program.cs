internal class Program
{
    private static void Main(string[] args)
    {
        Schematic schematic = new(File.ReadAllLines("input.txt"));
        SolvePart1(schematic);
        SolvePart2(schematic);
    }

    private static void SolvePart1(Schematic schematic)
    {
        int total = schematic.GetPartNrsNextToSymbol().Select(p => p.Nr).Sum();
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(Schematic schematic)
    {
        int total = schematic.FindGears().Select(g => g.Ratio).Sum();
        Console.WriteLine($"Part 2: {total}");
    }
}