using Autofac;
using DebugViewer.ViewModels.ComponentsPanel;
using DebugViewer.ViewModels.LogPanel;
using DebugViewer.ViewModels.MetricsPanel;

namespace DebugViewer.ViewModels;

public class ViewModelsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindowViewModel>().SingleInstance();

        builder.RegisterType<MetricsPanelViewModel>().SingleInstance();
        builder.RegisterType<ComponentsPanelViewModel>().SingleInstance();
        builder.RegisterType<LogPanelViewModel>().SingleInstance();
    }
}