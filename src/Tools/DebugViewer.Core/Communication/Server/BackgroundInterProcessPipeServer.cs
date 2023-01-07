using System.IO.Pipes;
using DebugViewer.Core.Messages;

namespace DebugViewer.Core.Communication.Server;

public sealed class BackgroundInterProcessPipeServer : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly NamedPipeServerStream _serverStream;

    private readonly Dictionary<int, IMessageHandlerInvoker> _messageHandlerInvokers;

    public event EventHandler? ClientConnected;
    public event EventHandler? ClientDisconnected;
    
    public BackgroundInterProcessPipeServer(string name)
    {
        _serverStream = new NamedPipeServerStream(name, PipeDirection.In);
        _cancellationTokenSource = new CancellationTokenSource();

        _messageHandlerInvokers = new Dictionary<int, IMessageHandlerInvoker>();
    }

    public void Run()
    {
        var cancellationToken = _cancellationTokenSource.Token;

        Task.Factory.StartNew(ServerLoop, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public void RegisterHandler<TMessageHandler, TMessage>()
        where TMessage : struct, IMessage<TMessage>
        where TMessageHandler : class, IMessageHandler<TMessage>, new()
    {
        _messageHandlerInvokers[IMessage<TMessage>.Type] = new MessageHandlerInvoker<TMessage, TMessageHandler>(new TMessageHandler());
    }

    public void RegisterHandler<TMessageHandler, TMessage>(TMessageHandler messageHandler)
        where TMessage : struct, IMessage<TMessage>
        where TMessageHandler : class, IMessageHandler<TMessage>
    {
        _messageHandlerInvokers[IMessage<TMessage>.Type] = new MessageHandlerInvoker<TMessage, TMessageHandler>(messageHandler);
    }

    private void ServerLoop()
    {
        while (true)
        {
            _serverStream.WaitForConnection();
            ClientConnected?.Invoke(this, EventArgs.Empty);

            while (_serverStream.IsConnected)
            {
                try
                {
                    var messageType = _serverStream.ReadMessageType();
                
                    if (!_messageHandlerInvokers.TryGetValue(messageType, out var invoker))
                        continue;

                    invoker.Invoke(_serverStream);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            ClientDisconnected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _serverStream.Dispose();
    }
}