
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
            int firstDigit = -1;
            int lastDigit = -1;
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch >= '0' && ch <= '9')
                {
                    if (firstDigit < 0)
                    {
                        firstDigit = ch - '0';
                    }
                    lastDigit = ch - '0';
                }
            }
            int checksum = firstDigit * 10 + lastDigit;
            total += checksum;
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        List<string> numbers = new() {
            "___",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten"
        };

        int total = 0;
        foreach (var line in lines)
        {
            int firstDigit = -1;
            int firstDigitPos = -1;
            int lastDigit = -1;
            int lastDigitPos = -1;
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch >= '0' && ch <= '9')
                {
                    if (firstDigit < 0)
                    {
                        firstDigit = ch - '0';
                        firstDigitPos = i;
                    }
                    lastDigit = ch - '0';
                    lastDigitPos = i;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                int posFirst = line.IndexOf(numbers[i]);
                if (posFirst >= 0 && (posFirst < firstDigitPos || firstDigitPos < 0))
                {
                    firstDigit = i;
                    firstDigitPos = posFirst;
                }

                int posLast = line.LastIndexOf(numbers[i]);
                if (posLast >= 0 && posLast > lastDigitPos)
                {
                    lastDigit = i;
                    lastDigitPos = posLast;
                }
            }

            int checksum = firstDigit * 10 + lastDigit;
            total += checksum;
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
