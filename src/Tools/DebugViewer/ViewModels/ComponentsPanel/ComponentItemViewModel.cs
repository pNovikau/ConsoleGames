using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugViewer.ViewModels.ComponentsPanel;

public sealed partial class ComponentItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _count;
}