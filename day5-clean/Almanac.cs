
class Almanac
{
    public List<Range> SeedRanges { get; private set; }
    public List<SeedMap> SeedMaps { get; private set; }

    public Almanac(List<Range> seedRanges, List<SeedMap> seedMaps)
    {
        SeedRanges = seedRanges;
        SeedMaps = seedMaps;
    }

    public void Print()
    {
        Console.WriteLine("Seed ranges:");
        SeedRanges.ForEach(r => r.Print());

        SeedMaps.ForEach(m =>
        {
            Console.WriteLine();
            m.Print();
        });
    }

    public IEnumerable<Range> ApplySeedMapsToSeedRanges()
    {
        SeedMapApplier applier = new();
        IEnumerable<Range> seedRanges = SeedRanges;
        foreach (SeedMap seedMap in SeedMaps)
        {
            seedRanges = applier.Apply(seedMap, seedRanges);
        }
        return seedRanges;
    }
}
