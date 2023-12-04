class Card
{
    public int Id { get; private set; }
    public List<int> WinningNumbers { get; } = new();
    public List<int> MyNumbers { get; } = new();

    public Card(int id, IEnumerable<int> winningNumbers, IEnumerable<int> myNumbers)
    {
        Id = id;
        WinningNumbers.AddRange(winningNumbers);
        MyNumbers.AddRange(myNumbers);
    }

    public int Points => NrMatches > 0 ? 1 << (NrMatches - 1) : 0;

    public int NrMatches => WinningNumbers.Intersect(MyNumbers).Count();

    public static Card Parse(string line)
    {
        // Parse a line like 'Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53'.
        string[] mainParts = line.Split(":");
        string[] cardIdParts = mainParts[0].Split(" ");
        int cardId = int.Parse(cardIdParts.Last());

        var winningNumbersText = mainParts[1].Split("|")[0].Trim();
        IEnumerable<int> winningNumbers = ParseIntListText(winningNumbersText);
        var myNumbersText = mainParts[1].Split("|")[1].Trim();
        IEnumerable<int> myNumbers = ParseIntListText(myNumbersText);

        return new Card(cardId, winningNumbers, myNumbers);
    }

    private static IEnumerable<int> ParseIntListText(string winningNumbersText)
    {
        return winningNumbersText.Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse);
    }
}
