namespace AoC;

class Field
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private readonly char[,] _field;

    public Field(int width, int height)
    {
        _field = new char[width, height];
        Width = width;
        Height = height;
    }

    public Field(string[] lines)
    {
        Height = lines.Length;
        Width = lines[0].Length;
        _field = new char[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            string line = lines[y];
            for (int x = 0; x < Width; x++)
            {
                _field[x, y] = line[x];
            }
        }
    }

    public Field(Field otherField)
    {
        Height = otherField.Height;
        Width = otherField.Width;
        // _field = new char[Width, Height];
        _field = (char[,])otherField._field.Clone();
        // for (int y = 0; y < Height; y++)
        // {
        //     for (int x = 0; x < Width; x++)
        //     {
        //         _field[x, y] = otherField._field[x, y];
        //     }
        // }
    }

    // override object.Equals
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Field other = (Field)obj;

        if (other.Width != Width || other.Height != Height)
        {
            return false;
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (other._field[x, y] != _field[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(_field[x, y]);
            }
            Console.WriteLine();
        }
    }

    public Field TiltNorth()
    {
        Field newField = new(this);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (newField._field[x, y] == 'O')
                {
                    for (int newY = y; newY >= 1; newY--)
                    {
                        if (y > 0 && newField._field[x, newY - 1] == '.')
                        {
                            newField._field[x, newY - 1] = 'O';
                            newField._field[x, newY] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return newField;
    }

    public Field TiltSouth()
    {
        Field newField = new(this);
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                if (newField._field[x, y] == 'O')
                {
                    for (int newY = y; newY < Height - 1; newY++)
                    {
                        if (y < Height - 1 && newField._field[x, newY + 1] == '.')
                        {
                            newField._field[x, newY + 1] = 'O';
                            newField._field[x, newY] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return newField;
    }

    public Field TiltEast()
    {
        Field newField = new(this);
        for (int x = Width - 1; x >= 0; x--)
        {
            for (int y = 0; y < Height; y++)
            {
                if (newField._field[x, y] == 'O')
                {
                    for (int newX = x; newX < Width - 1; newX++)
                    {
                        if (x < Width - 1 && newField._field[newX + 1, y] == '.')
                        {
                            newField._field[newX + 1, y] = 'O';
                            newField._field[newX, y] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return newField;
    }

    public Field TiltWest()
    {
        Field newField = new(this);
        for (int x = 1; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (newField._field[x, y] == 'O')
                {
                    for (int newX = x; newX >= 1; newX--)
                    {
                        if (newField._field[newX - 1, y] == '.')
                        {
                            newField._field[newX - 1, y] = 'O';
                            newField._field[newX, y] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return newField;
    }

    internal int GetLoad()
    {
        int load = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_field[x, y] == 'O')
                {
                    int weight = Height - y;
                    load += weight;
                }
            }
        }
        return load;
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 0;
        int height = lines.Length;
        int width = lines[0].Length;
        char[,] field = new char[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < width; x++)
            {
                field[x, y] = line[x];
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (field[x, y] == 'O')
                {
                    for (int newY = y; newY >= 1; newY--)
                    {
                        if (y > 0 && field[x, newY - 1] == '.')
                        {
                            field[x, newY - 1] = 'O';
                            field[x, newY] = '.';
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (field[x, y] == 'O')
                {
                    int weight = height - y;
                    total += weight;
                }
            }
        }

        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        int height = lines.Length;
        int width = lines[0].Length;
        List<Field> fields = new();

        Field field = new(lines);
        fields.Add(field);

        int nrTilts = 0;
        bool isDone = false;
        int indexOriginal = -1;
        int indexFirstCopy = -1;
        while (!isDone)
        {
            field = field.TiltNorth();
            fields.Add(field);
            field = field.TiltWest();
            fields.Add(field);
            field = field.TiltSouth();
            fields.Add(field);
            field = field.TiltEast();
            nrTilts += 4;
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].Equals(field))
                {
                    indexOriginal = i;
                    indexFirstCopy = nrTilts;
                    isDone = true;
                    break;
                }
            }
            fields.Add(field);
        }

        long cycleTime = indexFirstCopy - indexOriginal;
        long indexToUse = ((4000000000L - indexOriginal) % cycleTime) + indexOriginal;
        field = fields[(int)indexToUse];
        total = field.GetLoad();

        Console.WriteLine($"Part 2: {total}");
    }
}
