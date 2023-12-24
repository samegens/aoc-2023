namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Board(string[] lines)
    {
        Width = lines[0].Length;
        Height = lines.Length;
        _tiles = new char[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y];
            for (int x = 0; x < Width; x++)
            {
                _tiles[x, y] = line[x];
            }
        }
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point p] => _tiles[p.X, p.Y];

    public bool Contains(Point p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
}
