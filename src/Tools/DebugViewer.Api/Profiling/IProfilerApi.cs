namespace DebugViewer.Api.Profiling;

public interface IProfilerApi
{
    ProfileScope BeginScope(string scopeName);

    void IncrementCounter<TComponent>();
    void DecrementCounter<TComponent>();
}