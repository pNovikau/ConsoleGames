using DebugViewer.Core.Messages;

namespace DebugViewer.Core.Communication.Server;

public interface IMessageHandler<TMessage>
    where TMessage : struct, IMessage<TMessage>
{
    void Handle(ref TMessage message);
}