
abstract class BaseAlmanacParser
{
    public Almanac Parse(string[] lines)
    {
        // Parse text like:
        // seeds: 79 14 55 13
        //
        // seed-to-soil map:
        // 50 98 2
        // 52 50 48
        // 
        // soil-to-fertilizer map:
        // etc.

        var seedNumbers = lines[0]
            .Split(":")[1]
            .Trim()
            .Split(" ")
            .Select(long.Parse);
        List<Range> seedRanges = ParseSeeds(seedNumbers).ToList();

        const int headerSize = 2;
        List<SeedMap> seedMaps = ParseSeedMaps(lines.Skip(headerSize)).ToList();

        return new Almanac(seedRanges, seedMaps);
    }

    private static IEnumerable<SeedMap> ParseSeedMaps(IEnumerable<string> lines)
    {
        SeedMap? seedMap = null;
        foreach (string line in lines)
        {
            if (line.Contains(':'))
            {
                if (seedMap != null)
                {
                    yield return seedMap;
                }
                string name = line.Split(':')[0];
                seedMap = new SeedMap(name);
            }
            else if (!string.IsNullOrEmpty(line))
            {
                seedMap!.AddEntry(ParseSeedMapEntry(line));
            }
        }

        yield return seedMap!;
    }

    private static SeedMap.Entry ParseSeedMapEntry(string line)
    {
        // Parse lines like
        // 52 50 48
        string[] parts = line.Trim().Split(' ');
        const int destinationIndex = 0;
        const int sourceIndex = 1;
        const int lengthIndex = 2;
        long destination = long.Parse(parts[destinationIndex]);
        long source = long.Parse(parts[sourceIndex]);
        long length = long.Parse(parts[lengthIndex]);
        return new SeedMap.Entry(destination: destination, source: source, length: length);
    }

    abstract protected IEnumerable<Range> ParseSeeds(IEnumerable<long> seedNumbers);
}

class AlmanacParserPart1 : BaseAlmanacParser
{
    protected override IEnumerable<Range> ParseSeeds(IEnumerable<long> seedNumbers)
    {
        return seedNumbers.Select(s => new Range(s, s));
    }
}

class AlmanacParserPart2 : BaseAlmanacParser
{
    protected override IEnumerable<Range> ParseSeeds(IEnumerable<long> seedNumbers)
    {
        long seedRangeStart = -1;
        foreach (long seedNumber in seedNumbers)
        {
            if (seedRangeStart >= 0)
            {
                yield return new Range(seedRangeStart, seedRangeStart + seedNumber - 1);
                seedRangeStart = -1;
            }
            else
            {
                seedRangeStart = seedNumber;
            }
        }
    }
}