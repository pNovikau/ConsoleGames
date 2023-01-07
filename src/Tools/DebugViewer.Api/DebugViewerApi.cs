using DebugViewer.Api.Logging;
using DebugViewer.Api.Profiling;
using DebugViewer.Core.Communication.Client;

namespace DebugViewer.Api;

public static class DebugViewerApi
{
    internal static InterProcessPipeClient InterProcessPipeClient = default!;
    
    public static readonly ILogApi Log = default!;
    public static readonly IProfilerApi Profiler = new ProfilerApi();

    internal static event EventHandler? Initialized;
    internal static event EventHandler? ProcessStated;
    
    public static void Initialize()
    {
        InterProcessPipeClient = new InterProcessPipeClient("debug_viewer");

        Initialized?.Invoke(null, EventArgs.Empty);
    }

    public static void Connect()
    {
        InterProcessPipeClient.Connect();
        ProcessStated?.Invoke(null, EventArgs.Empty);
    }

    public static void Close()
    {
        InterProcessPipeClient?.Dispose();
    }
}