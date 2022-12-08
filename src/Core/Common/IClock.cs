namespace Core.Common;

public interface IClock
{
    TimeSpan ElapsedTime { get; }
    TimeSpan Restart();
}