class SeedMap
{
    public long Dest { get; set; }
    public long Source { get; set; }
    public long Length { get; set; }
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
        List<long> seeds = new();
        List<SeedMap> seedMaps = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y].Trim();
            if (y == 0)
            {
                var parts = line.Split(":")[1].Trim().Split(" ");
                // Use this to check if the ranges solution of part 2 matches the results
                // when using separate seeds.
                // for (int i = 0; i < parts.Length; i += 2)
                // {
                //     for (int j = 0; j < int.Parse(parts[i + 1]); j++)
                //     {
                //         seeds.Add(int.Parse(parts[i]) + j);
                //     }
                // }
                seeds = parts.Select(long.Parse).ToList();
            }
            else if (string.IsNullOrEmpty(line))
            {
                for (int i = 0; i < seeds.Count; i++)
                {
                    var seed = seeds[i];
                    foreach (var seedMap in seedMaps)
                    {
                        if (seed >= seedMap.Source && seed < seedMap.Source + seedMap.Length)
                        {
                            seeds[i] = seedMap.Dest + seed - seedMap.Source;
                        }
                    }
                }
                seedMaps = new();
                // Console.WriteLine(string.Join(",", seeds));
            }
            else if (!line.Contains(':'))
            {
                seedMaps.Add(new SeedMap()
                {
                    Dest = long.Parse(line.Split(" ")[0]),
                    Source = long.Parse(line.Split(" ")[1]),
                    Length = long.Parse(line.Split(" ")[2])
                });
            }
            else
            {
                // Console.WriteLine(line);
            }
        }

        if (seedMaps.Any())
        {
            for (int i = 0; i < seeds.Count; i++)
            {
                var seed = seeds[i];
                foreach (var seedMap in seedMaps)
                {
                    if (seed >= seedMap.Source && seed < seedMap.Source + seedMap.Length)
                    {
                        seeds[i] = seedMap.Dest + seed - seedMap.Source;
                    }
                }
            }
            // Console.WriteLine(string.Join(",", seeds));
        }
        long total = seeds.Min();
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        List<Range> seedRanges = new();
        List<SeedMap> seedMaps = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y].Trim();
            if (y == 0)
            {
                var parts = line.Split(":")[1].Trim().Split(" ");
                for (int i = 0; i < parts.Length; i += 2)
                {
                    long start = long.Parse(parts[i]);
                    long end = start + long.Parse(parts[i + 1]) - 1;    // Since range end is inclusive
                    seedRanges.Add(new Range(start, end));
                }
            }
            else if (string.IsNullOrEmpty(line))
            {
                TranslateSeedRanges(seedRanges, seedMaps);
                seedMaps = new();
                seedRanges = seedRanges.Where(r => r.IsValid).ToList();
            }
            else if (!line.Contains(':'))
            {
                seedMaps.Add(new SeedMap()
                {
                    Dest = long.Parse(line.Split(" ")[0]),
                    Source = long.Parse(line.Split(" ")[1]),
                    Length = long.Parse(line.Split(" ")[2])
                });
            }
            else
            {
                // Console.WriteLine(line);
            }
        }

        if (seedMaps.Any())
        {
            TranslateSeedRanges(seedRanges, seedMaps);
            seedRanges = seedRanges.Where(r => r.IsValid).ToList();
        }

        // seedRanges.ForEach(r => r.Print());
        long total = seedRanges.Select(r => r.Start).Min();
        Console.WriteLine($"Part 2: {total}");
    }

    private static void TranslateSeedRanges(List<Range> seedRanges, List<SeedMap> seedMaps)
    {
        List<Range> newRanges = new();
        for (int i = 0; i < seedRanges.Count; i++)
        {
            var seedRange = seedRanges[i];
            foreach (var seedMap in seedMaps)
            {
                var sourceRange = new Range(seedMap.Source, seedMap.Source + seedMap.Length - 1);
                if (sourceRange.Contains(seedRange))
                {
                    seedRange.Translate(seedMap.Dest - seedMap.Source);
                    break;
                }
                else if (seedRange.Contains(sourceRange) && sourceRange.End != seedRange.End && sourceRange.Start != seedRange.Start)
                {
                    // Split in three
                    List<Range> rangesToAdd = seedRange.SplitAt(sourceRange.Start, sourceRange.End + 1).ToList();
                    seedRange.Invalidate();
                    // Translate the middle range.
                    rangesToAdd[1].Translate(seedMap.Dest - seedMap.Source);
                    newRanges.Add(rangesToAdd[1]);
                    seedRanges.Add(rangesToAdd.First());
                    seedRanges.Add(rangesToAdd.Last());
                    break;
                }
                else if (sourceRange.Overlaps(seedRange))
                {
                    // We have to split the seedRange in two parts:
                    // one part that stays the same (the part that is outside the sourceRange),
                    // one part that is moved by the sourceRange.
                    if (sourceRange.Start != seedRange.Start && seedRange.Contains(sourceRange.Start))
                    {
                        List<Range> rangesToAdd = seedRange.SplitAt(sourceRange.Start).ToList();
                        seedRange.Invalidate();
                        rangesToAdd.Last().Translate(seedMap.Dest - seedMap.Source);
                        newRanges.Add(rangesToAdd.Last());
                        seedRanges.Add(rangesToAdd.First());
                    }
                    else if (sourceRange.End != seedRange.End && seedRange.Contains(sourceRange.End))
                    {
                        List<Range> rangesToAdd = seedRange.SplitAt(sourceRange.End + 1).ToList();
                        seedRange.Invalidate();
                        rangesToAdd.First().Translate(seedMap.Dest - seedMap.Source);
                        newRanges.Add(rangesToAdd.First());
                        seedRanges.Add(rangesToAdd.Last());
                    }
                    break;
                }
            }
        }

        seedRanges.AddRange(newRanges);
        // seedRanges.Where(r => r.IsValid).ToList().ForEach(r => r.Print());
    }
}
