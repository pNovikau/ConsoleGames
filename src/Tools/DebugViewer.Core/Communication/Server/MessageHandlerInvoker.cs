using System.IO.Pipes;
using DebugViewer.Core.Messages;

namespace DebugViewer.Core.Communication.Server;

public class MessageHandlerInvoker<TMessage, TMessageHandler> : IMessageHandlerInvoker
    where TMessage : struct, IMessage<TMessage>
    where TMessageHandler : class, IMessageHandler<TMessage>
{
    private readonly TMessageHandler _messageHandler;

    public MessageHandlerInvoker(TMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    public void Invoke(PipeStream stream)
    {
        var message = stream.ReadMessage<TMessage>();
        _messageHandler.Handle(ref message);
    }
}