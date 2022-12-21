using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DebugViewer.Core.Communication.Server;

namespace DebugViewer.ViewModels.ComponentsPanel;

public sealed class ComponentsPanelViewModel
{
    public readonly Dictionary<string, ComponentItemViewModel> ComponentsDictionary = new();

    //TODO: rework
    public ComponentsPanelViewModel(BackgroundInterProcessPipeServer server)
    {
        server.ClientDisconnected += OnClientDisconnected;
    }

    private void OnClientDisconnected(object? sender, EventArgs e)
    {
        ComponentsDictionary.Clear();
        ComponentsCollection.Clear();
    }
    
    public ObservableCollection<ComponentItemViewModel> ComponentsCollection { get; } = new();
}