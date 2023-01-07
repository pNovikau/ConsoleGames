using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DebugViewer.Core.Communication.Server;

namespace DebugViewer.ViewModels.MetricsPanel;

public sealed class MetricsPanelViewModel
{
    //TODO: rework
    public MetricsPanelViewModel(BackgroundInterProcessPipeServer server)
    {
        server.ClientDisconnected += OnClientDisconnected;
    }

    private void OnClientDisconnected(object? sender, EventArgs e)
    {
        PerformanceMeasureDictionary.Clear();
        PerformanceMeasureCollection.Clear();
    }

    public readonly Dictionary<string, PerformanceMeasureItemViewModel> PerformanceMeasureDictionary = new();

    public ObservableCollection<PerformanceMeasureItemViewModel> PerformanceMeasureCollection { get; } = new();
}