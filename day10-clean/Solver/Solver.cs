namespace AoC;

public class Solver
{
    public int SolvePart1(string[] lines)
    {
        Field field = FieldParser.Parse(lines);
        List<Point> animalPositions = field.AnimalPositions;
        List<int> distances = new();
        for (int i = 0; i < animalPositions.Count; i++)
        {
            int distance = Math.Min(i, animalPositions.Count - i);
            distances.Add(distance);
        }
        return distances.Max();
    }

    public int SolvePart2(string[] lines)
    {
        Field field = FieldParser.Parse(lines);
        return field.GetNrEnclosedTiles();
    }
}