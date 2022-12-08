using System.Diagnostics;

namespace Core.Common;

public class Clock : IClock
{
    private readonly Stopwatch _stopwatch;
    
    public Clock()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public TimeSpan ElapsedTime => _stopwatch.Elapsed;

    public TimeSpan Restart()
    {
        var res = _stopwatch.Elapsed;
        _stopwatch.Restart();

        return res;
    }
}