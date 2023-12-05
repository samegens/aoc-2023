class SeedMapApplier
{
    // To properly apply the maps to the ranges, we sometimes have to split
    // the ranges. But some subranges will be translated while others will not.
    // The second kind of subranges will still need to be processed.
    // Therefore we need a queue to collect these kind of subranges.
    // Ranges and subranges that have been translated, and ranges that do not
    // overlap any map, end up in the Done list.
    private Queue<Range> _todo = new();
    private List<Range> _done = new();

    public IEnumerable<Range> Apply(SeedMap seedMap, IEnumerable<Range> ranges)
    {
        // To make sure we can call Apply multiple times on the same applier,
        // we have to start each call with empty data.
        _todo = new();
        _done = new();

        foreach (Range range in ranges)
        {
            _todo.Enqueue(range);
        }

        while (_todo.Any())
        {
            Range range = _todo.Dequeue();
            ApplyToRange(seedMap, range);
        }

        return _done;
    }

    private void ApplyToRange(SeedMap seedMap, Range range)
    {
        foreach (SeedMap.Entry seedMapEntry in seedMap.Entries)
        {
            var translateSourceRange = new Range(seedMapEntry.Source, seedMapEntry.Source + seedMapEntry.Length - 1);
            if (translateSourceRange.Contains(range))
            {
                _done.Add(range.Translated(seedMapEntry.Delta));
                return;
            }
            if (range.Contains(translateSourceRange) &&
                translateSourceRange.End != range.End &&
                translateSourceRange.Start != range.Start)
            {
                // Split in three
                List<Range> newRanges = range.SplitAt(translateSourceRange.Start, translateSourceRange.End + 1).ToList();
                // The first and third subranges may still be translated by other seedmaps.
                _todo.Enqueue(newRanges.First());
                _todo.Enqueue(newRanges.Last());
                // Translate the middle range.
                _done.Add(newRanges[1].Translated(seedMapEntry.Delta));
                return;
            }
            if (translateSourceRange.Overlaps(range))
            {
                // We have to split the seedRange in two parts:
                // one part that stays the same (the part that is outside the sourceRange),
                // one part that is moved by the sourceRange.
                if (translateSourceRange.Start != range.Start && range.Contains(translateSourceRange.Start))
                {
                    // The first subrange stays the same, the second subrange is translated.
                    List<Range> newRanges = range.SplitAt(translateSourceRange.Start).ToList();
                    _todo.Enqueue(newRanges.First());
                    _done.Add(newRanges.Last().Translated(seedMapEntry.Delta));
                }
                else if (translateSourceRange.End != range.End && range.Contains(translateSourceRange.End))
                {
                    // The first subrange is translated, the second subrange stays the same.
                    List<Range> newRanges = range.SplitAt(translateSourceRange.End + 1).ToList();
                    _done.Add(newRanges.First().Translated(seedMapEntry.Delta));
                    _todo.Enqueue(newRanges.Last());
                }
                else
                {
                    throw new NotImplementedException("What's going on here?");
                }
                return;
            }
        }

        // No translation was possible, we're done with this range.
        _done.Add(range);
    }
}
