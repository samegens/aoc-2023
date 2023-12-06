
namespace AoC;

public class Race
{
    public long Time { get; private set; }
    public long Distance { get; private set; }

    public Race(long time, long distance)
    {
        Time = time;
        Distance = distance;
    }

    public int GetNrWaysToBeatRecord()
    {
        int count = 0;
        for (int timeToHoldButton = 1; timeToHoldButton <= Time; timeToHoldButton++)
        {
            long distanceTravelled = timeToHoldButton * (Time - timeToHoldButton);
            if (distanceTravelled > Distance)
            {
                count++;
            }
        }
        return count;
    }
}