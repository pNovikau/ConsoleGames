using Core.Common;

namespace Core;

public sealed class GameTime
{
    private readonly IClock _clock = new Clock();
    private readonly IClock _deltaClock = new Clock();

    private int _totalFrames;
    private TimeSpan _deltaTime;

    public int TotalFrames => _totalFrames;
    public TimeSpan GetTime => _clock.ElapsedTime;
    public TimeSpan Delta => _deltaTime;
    
    public void Update()
    {
        ++_totalFrames;
        _deltaTime = _deltaClock.Restart();
    }

    public override string ToString()
    {
        return $"TotalFrames: {TotalFrames} GetTime: {GetTime} Delta: {Delta}";
    }
}