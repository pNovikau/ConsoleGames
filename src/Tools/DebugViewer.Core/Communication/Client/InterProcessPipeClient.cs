using System.IO.Pipes;
using DebugViewer.Core.Messages;

namespace DebugViewer.Core.Communication.Client;

public sealed class InterProcessPipeClient : IDisposable
{
    private readonly NamedPipeClientStream _clientStream;

    public InterProcessPipeClient(string name)
    {
        _clientStream = new NamedPipeClientStream(".", name, PipeDirection.Out);
    }

    public void Connect()
    {
        _clientStream.Connect();
    }

    public void PublishMessage<TMessage>(ref TMessage message)
        where TMessage : struct, IMessage<TMessage>
    {
        if (!_clientStream.IsConnected)
            return;

        _clientStream.WriteMessage(ref message);
    }

    public void Dispose()
    {
        _clientStream.Dispose();
    }
}