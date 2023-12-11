namespace AoC;

public enum Tile
{
    None,
    Start,
    Vertical,
    Horizontal,
    NorthEast,
    NorthWest,
    SouthWest,
    SouthEast
}

public static class TileExtensions
{
    public static bool CanMoveUpTo(this Tile from, Tile to)
    {
        return (from == Tile.Start || from == Tile.Vertical || from == Tile.NorthWest || from == Tile.NorthEast) &&
               (to == Tile.Start || to == Tile.Vertical || to == Tile.SouthEast || to == Tile.SouthWest);
    }

    public static bool CanMoveDownTo(this Tile from, Tile to)
    {
        return (from == Tile.Start || from == Tile.Vertical || from == Tile.SouthEast || from == Tile.SouthWest) &&
               (to == Tile.Start || to == Tile.Vertical || to == Tile.NorthWest || to == Tile.NorthEast);
    }

    public static bool CanMoveLeftTo(this Tile from, Tile to)
    {
        return (from == Tile.Start || from == Tile.Horizontal || from == Tile.NorthWest || from == Tile.SouthWest) &&
               (to == Tile.Start || to == Tile.Horizontal || to == Tile.NorthEast || to == Tile.SouthEast);
    }

    public static bool CanMoveRightTo(this Tile from, Tile to)
    {
        return (from == Tile.Start || from == Tile.Horizontal || from == Tile.NorthEast || from == Tile.SouthEast) &&
               (to == Tile.Start || to == Tile.Horizontal || to == Tile.NorthWest || to == Tile.SouthWest);
    }
}