using MemoryPack;

namespace DebugViewer.Core.Messages;

[MemoryPackable(SerializeLayout.Sequential)]
public partial struct StartProfilingScopeMessage : IMessage<StartProfilingScopeMessage>
{
    public string Scope;
    public long Timestamp;
}