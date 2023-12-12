namespace AoC;

public class Image
{
    public List<PointL> Points { get; private set; }

    private List<long> _emptyRows = new();
    private List<long> _emptyColumns = new();

    public Image(IEnumerable<PointL> points)
    {
        Points = new List<PointL>(points);
    }

    public List<long> EmptyRows
    {
        get
        {
            if (!_emptyRows.Any())
            {
                _emptyRows = new List<long>(GetMissingValues(Points.Select(p => p.Y)));
            }
            return _emptyRows;
        }
    }

    public List<long> EmptyColumns
    {
        get
        {
            if (!_emptyColumns.Any())
            {
                _emptyColumns = new List<long>(GetMissingValues(Points.Select(p => p.X)));
            }
            return _emptyColumns;
        }
    }

    public Image Expanded(long factor)
    {
        List<PointL> newPoints = new(Points);
        // We make a copy of EmptyColumns, because we'll be changing the column numbers
        // while expanding.
        List<long> emptyColumns = new(EmptyColumns);
        for (int i = 0; i < emptyColumns.Count; i++)
        {
            IEnumerable<PointL> unchangedPoints = newPoints.Where(p => p.X < emptyColumns[i]);
            IEnumerable<PointL> expandedPoints = newPoints
                .Where(p => p.X > emptyColumns[i])
                .Select(p => new PointL(p.X + factor - 1, p.Y));
            newPoints = unchangedPoints.Concat(expandedPoints).ToList();
            if (newPoints.Count != Points.Count) throw new Exception("coding error");

            IEnumerable<long> unchangedColumns = emptyColumns.Where(c => c <= emptyColumns[i]);
            IEnumerable<long> expandedColumns = emptyColumns
                .Where(c => c > emptyColumns[i])
                .Select(c => c + factor - 1);
            emptyColumns = unchangedColumns.Concat(expandedColumns).ToList();
        }

        List<long> emptyRows = new(EmptyRows);
        for (int i = 0; i < emptyRows.Count; i++)
        {
            IEnumerable<PointL> unchangedPoints = newPoints.Where(p => p.Y < emptyRows[i]);
            IEnumerable<PointL> expandedPoints = newPoints
                .Where(p => p.Y > emptyRows[i])
                .Select(p => new PointL(p.X, p.Y + factor - 1));
            newPoints = unchangedPoints.Concat(expandedPoints).ToList();

            IEnumerable<long> unchangedRows = emptyRows.Where(c => c <= emptyRows[i]);
            IEnumerable<long> expandedRows = emptyRows
                .Where(c => c > emptyRows[i])
                .Select(c => c + factor - 1);
            emptyRows = unchangedRows.Concat(expandedRows).ToList();
        }

        return new Image(newPoints);
    }

    public IEnumerable<Tuple<PointL, PointL>> GetUniquePointPairs()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            for (int j = i + 1; j < Points.Count; j++)
            {
                yield return new Tuple<PointL, PointL>(Points[i], Points[j]);
            }
        }
    }

    private static IEnumerable<long> GetMissingValues(IEnumerable<long> values)
    {
        HashSet<long> valuesSet = new(values);
        long min = valuesSet.Min();
        long max = valuesSet.Max();
        HashSet<long> missingValuesSet = new(Enumerable.Range((int)min, (int)(max - min + 1)).Select(i => (long)i));
        missingValuesSet.ExceptWith(valuesSet);
        return missingValuesSet;
    }
}