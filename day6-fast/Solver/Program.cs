namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 1;
        List<int> times = lines[0].Split(':')[1].Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
        List<int> distances = lines[1].Split(':')[1].Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
        for (int i = 0; i < times.Count; i++)
        {
            int time = times[i];
            int distance = distances[i];
            int count = 0;
            for (int j = 1; j <= time; j++)
            {
                int d = j * (time - j);
                if (d > distance)
                {
                    count++;
                }
            }
            total *= count;
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        long time = long.Parse(lines[0].Split(':')[1].Replace(" ", ""));
        long distance = long.Parse(lines[1].Split(':')[1].Replace(" ", ""));
        long count = 0;
        for (long j = 1; j <= time; j++)
        {
            long d = j * (time - j);
            if (d > distance)
            {
                count++;
            }
        }
        Console.WriteLine($"Part 2: {count}");
    }
}
