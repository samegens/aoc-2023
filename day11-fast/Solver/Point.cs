public class Point
{
    public long X { get; set; }
    public long Y { get; set; }

    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public Point Move(int dX, int dY)
    {
        return new Point(X + dX, Y + dY);
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
        return (int)X * 17 + (int)Y;
    }

    internal void Print()
    {
        Console.WriteLine($"{X},{Y}");
    }
}