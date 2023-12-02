class Program
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
        foreach (var line in lines)
        {
            var game = Game.ParseLine(line);
            if (game.IsPossible)
            {
                total += game.Id;
            }
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        foreach (var line in lines)
        {
            var game = Game.ParseLine(line);
            total += game.Power;
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
