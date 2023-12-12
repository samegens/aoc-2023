namespace AoC;

public class PointL
{
    public long X { get; set; }
    public long Y { get; set; }

    public PointL(long x, long y)
    {
        X = x;
        Y = y;
    }

    public PointL Move(int dX, int dY)
    {
        return new PointL(X + dX, Y + dY);
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is PointL otherPoint)
        {
            return X == otherPoint.X && Y == otherPoint.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (int)X * 17 + (int)Y;
    }

    public long GetManhattanDistanceTo(PointL other)
    {
        return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
    }

    internal void Print()
    {
        Console.WriteLine($"{X},{Y}");
    }
}