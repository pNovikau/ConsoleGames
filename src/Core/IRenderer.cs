using Core.Common;

namespace Core;

public interface IRenderer
{
    public int Width { get; }
    public int Height { get; }

    void Clear();
    void Draw(ReadOnlySpan<char> symbols, Vector2<int> position);
    void Display();
}