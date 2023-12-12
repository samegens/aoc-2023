namespace AoC;

public static class ImageParser
{
    public static Image Parse(string[] lines)
    {
        List<PointL> points = new();
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    points.Add(new PointL(x, y));
                }
            }
        }

        return new Image(points);
    }
}