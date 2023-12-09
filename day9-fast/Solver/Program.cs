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
        int total = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            List<int> n = line.Split(' ').Select(int.Parse).ToList();
            List<List<int>> all_d = new();
            List<int> d = new(n);
            while (d.Any(d => d != 0))
            {
                List<int> d2 = new(d.Count - 1);
                for (int i = 0; i < d.Count - 1; i++)
                {
                    d2.Add(d[i + 1] - d[i]);
                }
                d = d2;
                all_d.Add(d);
            }
            for (int i = all_d.Count - 2; i >= 0; i--)
            {
                var d2 = all_d[i];
                var d3 = all_d[i + 1];
                d2.Add(d2[^1] + d3[^1]);
            }
            total += n[^1] + all_d[0][^1];
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            List<int> n = line.Split(' ').Select(int.Parse).ToList();
            List<List<int>> all_d = new();
            List<int> d = new(n);
            while (d.Any(d => d != 0))
            {
                List<int> d2 = new(d.Count - 1);
                for (int i = 0; i < d.Count - 1; i++)
                {
                    d2.Add(d[i + 1] - d[i]);
                }
                d = d2;
                all_d.Add(d);
            }
            for (int i = all_d.Count - 2; i >= 0; i--)
            {
                var d2 = all_d[i];
                var d3 = all_d[i + 1];
                d2.Insert(0, d2[0] - d3[0]);
            }
            total += n[0] - all_d[0][0];
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
