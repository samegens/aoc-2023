namespace AoC;

public class PlatformParser
{
    public static Platform Parse(string[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            throw new ArgumentException($"{nameof(lines)} is empty, is input.txt correct?");
        }

        int width = lines[0].Length;
        int height = lines.Length;
        char[,] chars = new char[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                chars[x, y] = lines[y][x];
            }
        }

        return new Platform(width, height, chars);
    }
}