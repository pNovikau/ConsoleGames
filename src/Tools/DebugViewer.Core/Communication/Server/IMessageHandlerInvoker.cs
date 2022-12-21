using System.IO.Pipes;

namespace DebugViewer.Core.Communication.Server;

public interface IMessageHandlerInvoker
{
    void Invoke(PipeStream stream);
}