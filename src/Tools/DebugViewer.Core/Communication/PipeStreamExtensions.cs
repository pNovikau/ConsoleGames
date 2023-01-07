using System.Buffers;
using System.Buffers.Binary;
using System.IO.Pipes;
using DebugViewer.Core.Common;
using DebugViewer.Core.Messages;
using MemoryPack;

namespace DebugViewer.Core.Communication;

public static class PipeStreamExtensions
{
    public static void WriteMessage<TMessage>(this PipeStream stream, ref TMessage message)
        where TMessage : struct, IMessage<TMessage>
    {
        var bufferWriter = ObjectPool<ReusableArrayPoolBufferWriter<byte>>.Shared.Rent();
        bufferWriter.Reuse();
        
        // first int32 is a message type
        // second for the message size in bytes
        BinaryPrimitives.WriteInt32LittleEndian(bufferWriter.GetSpan(sizeof(int)), IMessage<TMessage>.Type);
        bufferWriter.Advance(2 * sizeof(int));

        MemoryPackSerializer.Serialize(bufferWriter, in message);

        var messageSize = bufferWriter.WrittenCount - sizeof(int) * 2;
        BinaryPrimitives.WriteInt32LittleEndian(bufferWriter.Array.AsSpan(sizeof(int), sizeof(int)), messageSize);

        stream.BeginWrite(bufferWriter.Array, 0, bufferWriter.WrittenCount, Callback, (bufferWriter, stream));
    }

    public static TMessage ReadMessage<TMessage>(this PipeStream stream)
        where TMessage : struct, IMessage<TMessage>
    {
        var size = stream.ReadInt32LittleEndian();

        if (size <= 1024)
        {
            Span<byte> buffer = stackalloc byte[size];

            stream.ReadExactly(buffer);

            return MemoryPackSerializer.Deserialize<TMessage>(buffer);
        }

        var rentedArray = ArrayPool<byte>.Shared.Rent(size);
        try
        {
            var span = rentedArray.AsSpan(0, size);

            stream.ReadExactly(span);
            return MemoryPackSerializer.Deserialize<TMessage>(span);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rentedArray);
        }
    }

    private static void Callback(IAsyncResult asyncResult)
    {
        if (asyncResult.AsyncState is not (ReusableArrayPoolBufferWriter<byte> bufferWriter, PipeStream stream))
            return;

        stream.EndWrite(asyncResult);
        bufferWriter.Reset();

        ObjectPool<ReusableArrayPoolBufferWriter<byte>>.Shared.Return(bufferWriter);
    }

    public static int ReadMessageType(this PipeStream stream) 
        => stream.ReadInt32LittleEndian();

    private static int ReadInt32LittleEndian(this Stream stream)
    {
        Span<byte> integerSpan = stackalloc byte[sizeof(int)];

        stream.ReadExactly(integerSpan);

        return BinaryPrimitives.ReadInt32LittleEndian(integerSpan);
    }
}