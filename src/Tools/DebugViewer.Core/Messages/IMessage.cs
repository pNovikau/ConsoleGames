using DebugViewer.Core.Common;
using DebugViewer.Core.Communication;

namespace DebugViewer.Core.Messages;

public interface IMessage { }

public interface IMessage<TMessage> : IMessage
    where TMessage : IMessage
{
    static IMessage()
    {
        Type = typeof(TMessage).Name.GetDeterministicHashCode();
    }

    // ReSharper disable once StaticMemberInGenericType
    public static int Type { get; }
}