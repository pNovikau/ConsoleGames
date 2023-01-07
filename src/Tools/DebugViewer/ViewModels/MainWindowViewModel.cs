using DebugViewer.ViewModels.ComponentsPanel;
using DebugViewer.ViewModels.LogPanel;
using DebugViewer.ViewModels.MetricsPanel;

namespace DebugViewer.ViewModels;

public class MainWindowViewModel
{
    public MainWindowViewModel(LogPanelViewModel logPanelViewModel, MetricsPanelViewModel metricsPanelViewModel, ComponentsPanelViewModel componentPanelViewModel)
    {
        LogPanelViewModel = logPanelViewModel;
        MetricsPanelViewModel = metricsPanelViewModel;
        ComponentPanelViewModel = componentPanelViewModel;
    }
    
    public LogPanelViewModel LogPanelViewModel { get; }
    public ComponentsPanelViewModel ComponentPanelViewModel { get; }
    public MetricsPanelViewModel MetricsPanelViewModel { get; }
}