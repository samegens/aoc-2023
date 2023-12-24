namespace AoC;

public class Point3d
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3d Move(double dX, double dY, double dZ)
    {
        return new Point3d(X + dX, Y + dY, Z + dZ);
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point3d otherPoint)
        {
            return X == otherPoint.X && Y == otherPoint.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public void Print()
    {
        Console.WriteLine($"{X},{Y},{Z}");
    }
}
