class Game
{
    public int Id { get; private set; }
    public List<Grab> Grabs { get; } = new();

    public Game(int id)
    {
        Id = id;
    }

    public static Game ParseLine(string line)
    {
        var game = new Game(ParseGameId(line));
        var grabParts = line.Split(":")[1].Split(";");
        foreach (var grabText in grabParts)
        {
            Grab grab = Grab.Parse(grabText);
            game.Grabs.Add(grab);
        }
        return game;
    }

    public bool IsPossible
    {
        get
        {
            bool isPossible = true;
            foreach (Grab grab in Grabs)
            {
                isPossible = isPossible && grab.IsPossible;
            }
            return isPossible;
        }
    }

    public int Power
    {
        get
        {
            return Grabs.Select(g => g[Color.Red]).Max() *
                   Grabs.Select(g => g[Color.Green]).Max() *
                   Grabs.Select(g => g[Color.Blue]).Max();
        }
    }

    private static int ParseGameId(string line)
    {
        return int.Parse(line.Split(":")[0].Split(" ")[1]);
    }
}
