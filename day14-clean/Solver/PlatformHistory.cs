
namespace AoC;

public class PlatformHistory
{
    private readonly List<Platform> _platforms = new();

    public void Add(Platform platform)
    {
        _platforms.Add(platform);
    }

    public Platform this[int index] => _platforms[index];

    public Platform Last => _platforms.Last();

    public (int loopStartIndex, int loopEndIndex) FindLoop()
    {
        Platform lastPlatform = _platforms.Last();
        for (int i = 0; i < _platforms.Count - 1; i++)
        {
            if (_platforms[i].Equals(lastPlatform))
            {
                return (i, _platforms.Count - 1);
            }
        }

        return (-1, -1);
    }
}