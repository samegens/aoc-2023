public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(Point point)
    {
        X = point.X;
        Y = point.Y;
    }

    public Point Move(int dX, int dY)
    {
        return new Point(X + dX, Y + dY);
    }

    public Point Move(Point delta)
    {
        return new Point(X + delta.X, Y + delta.Y);
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point otherPoint)
        {
            return X == otherPoint.X && Y == otherPoint.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return X * 17 + Y;
    }

    internal void Print()
    {
        Console.WriteLine($"{X},{Y}");
    }
}