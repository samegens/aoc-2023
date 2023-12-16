using System.Text;

namespace AoC;

public class Platform
{
    private const char RoundedRock = 'O';
    private const char CubeShapedRock = '#';
    private const char EmptySpace = '.';

    public Platform(int width, int height, char[,] tiles)
    {
        Width = width;
        Height = height;
        _tiles = (char[,])tiles.Clone();
    }

    public Platform(Platform otherPlatform)
    {
        Height = otherPlatform.Height;
        Width = otherPlatform.Width;
        _tiles = (char[,])otherPlatform._tiles.Clone();
    }

    public int Width { get; }

    public int Height { get; }

    private readonly char[,] _tiles;

    public char this[int x, int y] => _tiles[x, y];

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"{Width} x {Height}:");
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                sb.Append(_tiles[x, y]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public Platform TiltedNorth()
    {
        Platform newPlatform = new(this);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (newPlatform._tiles[x, y] == RoundedRock)
                {
                    newPlatform.MoveRock(x, y, 0, -1);
                }
            }
        }
        return newPlatform;
    }

    public Platform TiltedSouth()
    {
        Platform newPlatform = new(this);
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                if (newPlatform._tiles[x, y] == RoundedRock)
                {
                    newPlatform.MoveRock(x, y, 0, 1);
                }
            }
        }
        return newPlatform;
    }

    public Platform TiltedEast()
    {
        Platform newPlatform = new(this);
        for (int x = Width - 1; x >= 0; x--)
        {
            for (int y = 0; y < Height; y++)
            {
                if (newPlatform._tiles[x, y] == RoundedRock)
                {
                    newPlatform.MoveRock(x, y, 1, 0);
                }
            }
        }
        return newPlatform;
    }

    public Platform TiltedWest()
    {
        Platform newPlatform = new(this);
        for (int x = 1; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (newPlatform._tiles[x, y] == RoundedRock)
                {
                    newPlatform.MoveRock(x, y, -1, 0);
                }
            }
        }
        return newPlatform;
    }

    public int GetLoad()
    {
        int load = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_tiles[x, y] == RoundedRock)
                {
                    int weight = Height - y;
                    load += weight;
                }
            }
        }
        return load;
    }

    private void MoveRock(int x, int y, int dx, int dy)
    {
        for (int newX = x, newY = y;
             newX + dx < Width && newX + dx >= 0 && newY + dy < Height && newY + dy >= 0;
             newX += dx, newY += dy)
        {
            if (_tiles[newX + dx, newY + dy] == EmptySpace)
            {
                _tiles[newX + dx, newY + dy] = RoundedRock;
                _tiles[newX, newY] = EmptySpace;
            }
            else
            {
                break;
            }
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Platform other = (Platform)obj;

        if (other.Width != Width || other.Height != Height)
        {
            return false;
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (other._tiles[x, y] != _tiles[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}