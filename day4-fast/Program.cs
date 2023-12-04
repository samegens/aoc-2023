
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
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            var winningNumbersText = line.Split(":")[1].Split("|")[0].Trim();
            var myNumbersText = line.Split(":")[1].Split("|")[1].Trim();
            List<int> winningNumbers = new();
            foreach (var t in winningNumbersText.Split(" "))
            {
                if (!string.IsNullOrWhiteSpace(t))
                {
                    winningNumbers.Add(int.Parse(t));
                }
            }
            List<int> myNumbers = new();
            foreach (var t in myNumbersText.Split(" "))
            {
                if (!string.IsNullOrWhiteSpace(t))
                {
                    myNumbers.Add(int.Parse(t));
                }
            }
            var result = winningNumbers.Intersect(myNumbers);
            total += 1 << (result.Count() - 1);
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        Dictionary<int, int> cardCount = new();
        for (int y = 0; y < lines.Length; y++)
        {
            int cardId = y + 1;
            if (!cardCount.ContainsKey(cardId))
            {
                cardCount[cardId] = 1;
            }
            string line = lines[y];
            var winningNumbersText = line.Split(":")[1].Split("|")[0].Trim();
            var myNumbersText = line.Split(":")[1].Split("|")[1].Trim();
            List<int> winningNumbers = new();
            foreach (var t in winningNumbersText.Split(" "))
            {
                if (!string.IsNullOrWhiteSpace(t))
                {
                    winningNumbers.Add(int.Parse(t));
                }
            }
            List<int> myNumbers = new();
            foreach (var t in myNumbersText.Split(" "))
            {
                if (!string.IsNullOrWhiteSpace(t))
                {
                    myNumbers.Add(int.Parse(t));
                }
            }
            var result = winningNumbers.Intersect(myNumbers);
            for (int i = 0; i < result.Count(); i++)
            {
                int newCardId = cardId + i + 1;
                if (!cardCount.ContainsKey(newCardId))
                {
                    cardCount[newCardId] = 1;
                }
                cardCount[newCardId] = cardCount[newCardId] + cardCount[cardId];
            }
        }
        total = cardCount.Sum(kv => kv.Value);
        Console.WriteLine($"Part 1: {total}");
    }
}