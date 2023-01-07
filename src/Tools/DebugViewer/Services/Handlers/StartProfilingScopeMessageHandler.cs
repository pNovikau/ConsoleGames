using DebugViewer.Core.Communication.Server;
using DebugViewer.Core.Messages;
using DebugViewer.ViewModels.MetricsPanel;

namespace DebugViewer.Services.Handlers;

public sealed class StartProfilingScopeMessageHandler : IMessageHandler<StartProfilingScopeMessage>
{
    private readonly MetricsPanelViewModel _metricsPanelViewModel;

    public StartProfilingScopeMessageHandler(MetricsPanelViewModel metricsPanelViewModel)
    {
        _metricsPanelViewModel = metricsPanelViewModel;
    }
    
    public void Handle(ref StartProfilingScopeMessage message)
    {
        if (_metricsPanelViewModel.PerformanceMeasureDictionary.ContainsKey(message.Scope)) 
            return;
        
        _metricsPanelViewModel.PerformanceMeasureDictionary[message.Scope] = new PerformanceMeasureItemViewModel
        {
            Scope = message.Scope
        };
        
        _metricsPanelViewModel.PerformanceMeasureCollection.Add(_metricsPanelViewModel.PerformanceMeasureDictionary[message.Scope]);
    }
}