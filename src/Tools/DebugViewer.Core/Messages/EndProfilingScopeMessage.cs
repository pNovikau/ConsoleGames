using MemoryPack;

namespace DebugViewer.Core.Messages;

[MemoryPackable(SerializeLayout.Sequential)]
public partial struct EndProfilingScopeMessage : IMessage<EndProfilingScopeMessage>
{
    public string Scope;
    public long Ticks;
}