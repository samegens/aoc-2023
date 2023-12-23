public enum Direction
{
    Up, Right, Down, Left
}

public static class DirectionHelpers
{
    public static readonly Dictionary<Direction, Point> Movements = new()
    {
        [Direction.Up] = new Point(0, -1),
        [Direction.Right] = new Point(1, 0),
        [Direction.Down] = new Point(0, 1),
        [Direction.Left] = new Point(-1, 0),
    };
}