namespace AoC;

public class Rect
{
    private readonly Point _topLeft;
    private readonly Point _bottomRight;

    public int Width => _bottomRight.X - _topLeft.X + 1;

    public int Height => _topLeft.Y - _bottomRight.Y + 1;

    public Point TopLeft => _topLeft;

    public Point BottomRight => _bottomRight;

    public Rect(Point topLeft, Point bottomRight)
    {
        _topLeft = topLeft;
        _bottomRight = bottomRight;
    }

    public bool Contains(Point point)
    {
        return point.X >= _topLeft.X && point.X <= _bottomRight.X &&
               point.Y <= _topLeft.Y && point.Y >= _bottomRight.Y;
    }

    public bool IsLeftOf(Point point)
    {
        return _bottomRight.X < point.X;
    }

    public bool IsAbove(Point point)
    {
        return _bottomRight.Y > point.Y;
    }

    public void Print()
    {
        Console.WriteLine($"Box: {_topLeft} to {_bottomRight}");
    }
}