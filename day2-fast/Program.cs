
class Game
{
    public int Id { get; set; }
    public List<Dictionary<string, int>> Grabs { get; set; } = new();

    public static Game ParseLine(string line)
    {
        var game = new Game();
        game.Id = int.Parse(line.Split(":")[0].Split(" ")[1]);
        var grabs = line.Split(":")[1].Split(";");
        foreach (var grab in grabs)
        {
            Dictionary<string, int> dict = new();
            var parts = grab.Split(",");
            foreach (var part in parts)
            {
                var nr = int.Parse(part.Trim().Split(" ")[0]);
                var color = part.Trim().Split(" ")[1];
                dict[color] = nr;
            }
            game.Grabs.Add(dict);
        }
        return game;
    }
}

internal class Program
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
            bool possible = true;
            foreach (var grab in game.Grabs)
            {
                possible = possible && (!grab.ContainsKey("red") || grab["red"] <= 12) &&
                    (!grab.ContainsKey("green") || grab["green"] <= 13) &&
                    (!grab.ContainsKey("blue") || grab["blue"] <= 14);

            }
            if (possible)
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
            int minRed = 0;
            int minGreen = 0;
            int minBlue = 0;
            foreach (var grab in game.Grabs)
            {
                if (grab.ContainsKey("red"))
                {
                    minRed = Math.Max(minRed, grab["red"]);
                }
                if (grab.ContainsKey("green"))
                {
                    minGreen = Math.Max(minGreen, grab["green"]);
                }
                if (grab.ContainsKey("blue"))
                {
                    minBlue = Math.Max(minBlue, grab["blue"]);
                }
            }
            int power = minRed * minGreen * minBlue;
            total += power;
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
