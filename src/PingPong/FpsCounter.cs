using Core;
using Core.Common;

namespace PingPong;

public struct FpsCounter
{
    private readonly GameTime _gameTime;

    private IClock _clock;
    private int _lastTotalFrames;
    private int _fps;

    public FpsCounter(GameTime gameTime) : this()
    {
        _gameTime = gameTime;
        _clock = new Clock();
    }

    public int Fps
    {
        get
        {
            if (_clock.ElapsedTime.Seconds < 1.0f)
                return _fps;

            var totalFrames = _gameTime.TotalFrames;
            _fps = totalFrames - _lastTotalFrames;
            _lastTotalFrames = totalFrames;

            _clock.Restart();

            return _fps;
        }
    }
}