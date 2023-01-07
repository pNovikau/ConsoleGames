using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using DebugViewer.Core.Communication.Server;

namespace DebugViewer.ViewModels.LogPanel;

public sealed partial class LogPanelViewModel : ObservableObject
{
    [ObservableProperty]
    private string _searchQuery = string.Empty;

    //TODO: rework
    public LogPanelViewModel(BackgroundInterProcessPipeServer server)
    {
        server.ClientDisconnected += OnClientDisconnected;
    }

    private void OnClientDisconnected(object? sender, EventArgs e)
    {
        LogsCollection.Clear();
    }
    
    public ObservableCollection<LogItemViewModel> LogsCollection { get; } = new();
}