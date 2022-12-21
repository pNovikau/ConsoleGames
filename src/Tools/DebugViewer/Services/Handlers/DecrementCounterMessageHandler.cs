using System;
using DebugViewer.Core.Communication.Server;
using DebugViewer.Core.Messages;
using DebugViewer.ViewModels.ComponentsPanel;

namespace DebugViewer.Services.Handlers;

public sealed class DecrementCounterMessageHandler : IMessageHandler<DecrementCounterMessage>
{
    private readonly ComponentsPanelViewModel _componentPanelViewModel;

    public DecrementCounterMessageHandler(ComponentsPanelViewModel componentPanelViewModel)
    {
        _componentPanelViewModel = componentPanelViewModel ?? throw new ArgumentNullException(nameof(componentPanelViewModel));
    }

    public void Handle(ref DecrementCounterMessage message)
    {
        if (_componentPanelViewModel.ComponentsDictionary.TryGetValue(message.Name, out var counter))
            counter.Count--;
    }
}