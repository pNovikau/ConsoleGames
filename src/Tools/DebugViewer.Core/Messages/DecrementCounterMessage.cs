using MemoryPack;

namespace DebugViewer.Core.Messages;

[MemoryPackable(SerializeLayout.Sequential)]
public partial struct DecrementCounterMessage : IMessage<DecrementCounterMessage>
{
    public string Name;
}