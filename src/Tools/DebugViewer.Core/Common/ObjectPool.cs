using System.Collections.Concurrent;

namespace DebugViewer.Core.Common;

internal class ObjectPool<T>
    where T : new()
{
    public static readonly ObjectPool<T> Shared = new();

    private readonly ConcurrentBag<T> _objects;

    private ObjectPool()
    {
        _objects = new ConcurrentBag<T>();
    }

    public T Rent() => _objects.TryTake(out var item) ? item : new T();
    public void Return(T item) => _objects.Add(item);
}