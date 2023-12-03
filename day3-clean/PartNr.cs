/// <summary>
/// PartNr here has not completely the same meaning as in the puzzle.
/// This class is more a _possible_ part number. The Schematic class
/// has the context to determine if a possible part number is an
/// actual part number.
/// </summary>
class PartNr
{
    public Point Position { get; private set; }
    public int Nr { get; private set; }

    // Also store the length of the number so we don't have to compute it every time.
    public int Length { get; private set; }

    public PartNr(Point pos, int nr, int length)
    {
        Position = pos;
        Nr = nr;
        Length = length;
    }

    public bool ContainsPoint(Point p)
    {
        return p.Y == Position.Y && p.X >= Position.X && p.X < Position.X + Length;
    }

    public bool IsAdjacentTo(Point point)
    {
        return point.X >= Position.X - 1 &&
            point.X <= Position.X + Length &&
            point.Y >= Position.Y - 1 &&
            point.Y <= Position.Y + 1 &&
            !ContainsPoint(point);
    }
}
