using System;
using DebugViewer.Core.Communication.Server;
using DebugViewer.Core.Messages;
using DebugViewer.ViewModels.ComponentsPanel;

namespace DebugViewer.Services.Handlers;

public sealed class IncrementCounterMessageHandler : IMessageHandler<IncrementCounterMessage>
{
    private readonly ComponentsPanelViewModel _componentPanelViewModel;

    public IncrementCounterMessageHandler(ComponentsPanelViewModel componentPanelViewModel)
    {
        _componentPanelViewModel = componentPanelViewModel ?? throw new ArgumentNullException(nameof(componentPanelViewModel));
    }

    public void Handle(ref IncrementCounterMessage message)
    {
        if (_componentPanelViewModel.ComponentsDictionary.TryGetValue(message.Name, out var counter))
            counter.Count++;
        else
        {
            _componentPanelViewModel.ComponentsDictionary[message.Name] = new ComponentItemViewModel { Name = message.Name, Count = 1 };
            _componentPanelViewModel.ComponentsCollection.Add(_componentPanelViewModel.ComponentsDictionary[message.Name]);
        }
    }
}