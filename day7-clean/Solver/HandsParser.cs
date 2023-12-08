namespace AoC;

public class HandsParser
{
    public IEnumerable<Hand> Parse(string[] lines)
    {
        return lines.Select(ParseHand);
    }

    private Hand ParseHand(string line)
    {
        string[] parts = line.Split(' ');
        const int cardsIndex = 0;
        const int bidIndex = 1;
        string cards = parts[cardsIndex];
        int bid = int.Parse(parts[bidIndex]);
        return new Hand(cards, bid);
    }
}