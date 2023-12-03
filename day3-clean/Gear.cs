class Gear
{
    public List<PartNr> PartNrs { get; } = new();

    public Gear(List<PartNr> partNrs)
    {
        if (partNrs.Count != 2)
        {
            throw new ArgumentException($"partNrs should have two elements, but has {partNrs.Count}");
        }
        PartNrs.AddRange(partNrs);
    }

    public int Ratio
    {
        get
        {
            return PartNrs[0].Nr * PartNrs[1].Nr;
        }
    }
}
