namespace AoC;

public class FieldParser
{
    public static Field Parse(string[] lines)
    {
        return new Field(lines);
    }
}
