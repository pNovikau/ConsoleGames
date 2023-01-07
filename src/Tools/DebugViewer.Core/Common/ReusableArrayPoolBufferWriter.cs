using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;

namespace DebugViewer.Core.Common;

internal sealed class ReusableArrayPoolBufferWriter<T> : IBufferWriter<T>
{
    private const int DefaultInitialBufferSize = 256;

    private int _index;
    private T[]? _array;

    public T[] Array => _array;

    public int WrittenCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _index;
    }

    public void Advance(int count)
    {
        if (_index > _array!.Length - count)
        {
            ThrowArgumentExceptionForAdvancedTooFar();
        }

        _index += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckBufferAndEnsureCapacity(int sizeHint)
    {
        if (sizeHint == 0)
            sizeHint = 1;

        if (sizeHint > _array!.Length - _index) 
            ResizeBuffer(sizeHint);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ResizeBuffer(int sizeHint)
    {
        var minimumSize = (uint)_index + (uint)sizeHint;

        if (minimumSize > 1024 * 1024)
            minimumSize = BitOperations.RoundUpToPowerOf2(minimumSize);

        ArrayPool<T>.Shared.Resize(ref _array, (int)minimumSize);
    }

    public void Reset()
    {
        var array = _array;

        if (array is null)
            return;

        _array = null;
        _index = 0;

        ArrayPool<T>.Shared.Return(array);
    }

    public void Reuse()
    {
        _index = 0;

        if (_array is not null)
            ArrayPool<T>.Shared.Return(_array);

        _array = ArrayPool<T>.Shared.Rent(DefaultInitialBufferSize);
    }

    public Memory<T> GetMemory(int sizeHint = 0)
    {
        CheckBufferAndEnsureCapacity(sizeHint);

        return _array.AsMemory(_index);
    }

    public Span<T> GetSpan(int sizeHint = 0)
    {
        CheckBufferAndEnsureCapacity(sizeHint);

        return _array.AsSpan(_index);
    }

    private static void ThrowArgumentExceptionForAdvancedTooFar()
        => throw new ArgumentException("The buffer writer has advanced too far.");
}