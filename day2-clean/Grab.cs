class Grab
{
    private readonly Dictionary<Color, int> _nrPerColor = new();
    private readonly Dictionary<Color, int> _maxPerColor = new()
    {
        [Color.Red] = 12,
        [Color.Green] = 13,
        [Color.Blue] = 14
    };

    public Grab()
    {
        // To prevent having to use ContainsKey, we ensure that each color is present in the dictionary.
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            _nrPerColor[color] = 0;
        }
    }

    public int this[Color color]
    {
        get
        {
            return _nrPerColor[color];
        }
    }

    public void AddColorNr(Color color, int nr)
    {
        _nrPerColor[color] = nr;
    }

    public bool IsPossible
    {
        get
        {
            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                if (_nrPerColor[color] > _maxPerColor[color])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static Grab Parse(string text)
    {
        // Parse a grab of the form '10 green, 9 blue, 1 red'.
        Grab grab = new();
        string[] parts = text.Split(",");
        foreach (string part in parts)
        {
            string[] subparts = part.Trim().Split(" ");
            int nr = int.Parse(subparts[0]);
            Color color = ParseColor(subparts[1]);
            grab.AddColorNr(color, nr);
        }
        return grab;
    }

    private static Color ParseColor(string text)
    {
        return (Color)Enum.Parse(typeof(Color), text, ignoreCase: true);
    }
}
