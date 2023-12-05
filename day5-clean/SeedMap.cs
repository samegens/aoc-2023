
class SeedMap
{
    public class Entry
    {
        public long Destination { get; private set; }
        public long Source { get; private set; }
        public long Length { get; private set; }
        public long Delta => Destination - Source;

        public Entry(long destination, long source, long length)
        {
            Destination = destination;
            Source = source;
            Length = length;
        }

        public void Print()
        {
            Console.WriteLine($"d {Destination}, s {Source}, l {Length}");
        }
    }

    public string Name { get; private set; }
    public List<Entry> Entries { get; private set; } = new();

    public SeedMap(string name)
    {
        Name = name;
    }

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry);
    }

    public void Print()
    {
        Console.WriteLine(Name);
        Entries.ForEach(e => e.Print());
    }
}
