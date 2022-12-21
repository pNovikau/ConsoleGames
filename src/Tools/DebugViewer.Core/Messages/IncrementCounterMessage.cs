using MemoryPack;

namespace DebugViewer.Core.Messages;

[MemoryPackable(SerializeLayout.Sequential)]
public partial struct IncrementCounterMessage : IMessage<IncrementCounterMessage>
{
    public string Name;
}