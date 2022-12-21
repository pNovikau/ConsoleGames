using System;
using DebugViewer.Core.Communication.Server;
using DebugViewer.Core.Messages;
using DebugViewer.ViewModels.MetricsPanel;

namespace DebugViewer.Services.Handlers;

public sealed class EndProfilingScopeMessageHandler : IMessageHandler<EndProfilingScopeMessage>
{
    private readonly MetricsPanelViewModel _metricsPanelViewModel;

    public EndProfilingScopeMessageHandler(MetricsPanelViewModel metricsPanelViewModel)
    {
        _metricsPanelViewModel = metricsPanelViewModel;
    }

    public void Handle(ref EndProfilingScopeMessage message)
    {
        if (_metricsPanelViewModel.PerformanceMeasureDictionary.TryGetValue(message.Scope, out var item))
            item.Time = TimeSpan.FromTicks(message.Ticks).Milliseconds.ToString();
    }
}