using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugViewer.ViewModels.MetricsPanel;

public sealed partial class PerformanceMeasureItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string scope = string.Empty;

    [ObservableProperty]
    private string _time = string.Empty;
}