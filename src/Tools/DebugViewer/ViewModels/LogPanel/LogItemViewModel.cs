using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugViewer.ViewModels.LogPanel;

public sealed partial class LogItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private string _content = string.Empty;
}