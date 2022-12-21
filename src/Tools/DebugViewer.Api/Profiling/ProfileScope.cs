namespace DebugViewer.Api.Profiling;

public ref struct ProfileScope
{
    private readonly Action<string, long> _action;
    private readonly long _timestamp;
    private readonly string _scope;

    public ProfileScope(Action<string, long> action, string scope, long timestamp)
    {
        _action = action;
        _scope = scope;
        _timestamp = timestamp;
    }

    public void Dispose()
    {
        _action(_scope, _timestamp);
    }
}