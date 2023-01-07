using System.Diagnostics;
using DebugViewer.Core.Messages;

namespace DebugViewer.Api.Profiling;

internal class ProfilerApi : IProfilerApi
{
    public ProfileScope BeginScope(string scopeName)
    {
        var timestamp = Stopwatch.GetTimestamp();

        var @event = new StartProfilingScopeMessage
        {
            Scope = scopeName,
            Timestamp = timestamp
        };

        DebugViewerApi.InterProcessPipeClient.PublishMessage(ref @event);

        return new ProfileScope(SendResults, scopeName, timestamp);
    }

    public void IncrementCounter<TComponent>()
    {
        var message = new IncrementCounterMessage
        {
            Name = typeof(TComponent).Name
        };

        DebugViewerApi.InterProcessPipeClient.PublishMessage(ref message);
    }

    public void DecrementCounter<TComponent>()
    {
        var message = new DecrementCounterMessage
        {
            Name = typeof(TComponent).Name
        };

        DebugViewerApi.InterProcessPipeClient.PublishMessage(ref message);
    }

    private static void SendResults(string scope, long timestamp)
    {
        var @event = new EndProfilingScopeMessage
        {
            Scope = scope,
            Ticks = Stopwatch.GetElapsedTime(timestamp).Ticks
        };

        DebugViewerApi.InterProcessPipeClient.PublishMessage(ref @event);
    }
}