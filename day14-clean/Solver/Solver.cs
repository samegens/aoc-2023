


namespace AoC;

public class Solver
{
    private readonly string[] _lines;
    private readonly PlatformHistory _platformHistory = new();

    public Solver(string[] lines)
    {
        _lines = lines;
    }

    public int SolvePart1()
    {
        Platform platform = PlatformParser.Parse(_lines);
        platform = platform.TiltedNorth();
        return platform.GetLoad();
    }

    public int SolvePart2()
    {
        Platform platform = PlatformParser.Parse(_lines);
        _platformHistory.Add(platform);

        (int loopStartIndex, int loopEndIndex) = DetermineLoop();

        long cycleTime = loopEndIndex - loopStartIndex;
        long indexToUse = ((4 * 1000000000L - loopStartIndex) % cycleTime) + loopStartIndex;

        return _platformHistory[(int)indexToUse].GetLoad();
    }

    private (int loopstartIndex, int loopEndIndex) DetermineLoop()
    {
        int loopStartIndex = -1;
        int loopEndIndex = -1;
        while (loopStartIndex == -1)
        {
            SpinCycle();
            (loopStartIndex, loopEndIndex) = _platformHistory.FindLoop();
        }

        return (loopStartIndex, loopEndIndex);
    }

    private void SpinCycle()
    {
        Platform platform = _platformHistory.Last;
        platform = platform.TiltedNorth();
        _platformHistory.Add(platform);
        platform = platform.TiltedWest();
        _platformHistory.Add(platform);
        platform = platform.TiltedSouth();
        _platformHistory.Add(platform);
        platform = platform.TiltedEast();
        _platformHistory.Add(platform);
    }
}