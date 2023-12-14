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
        int height = lines.Length;
        int width = lines[0].Length;
        char[,] field = new char[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < width; x++)
            {
                field[x, y] = line[x];
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (field[x, y] == 'O')
                {
                    for (int newY = y; newY >= 1; newY--)
                    {
                        if (y > 0 && field[x, newY - 1] == '.')
                        {
                            field[x, newY - 1] = 'O';
                            field[x, newY] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(field[x, y]);
                if (field[x, y] == 'O')
                {
                    int weight = height - y;
                    total += weight;
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
    }
}
