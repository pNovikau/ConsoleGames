using Core.Common;

namespace Core.Render;

public interface IRenderArea
{
    public Vector2<int> Size { get; init; }
    public Vector2<int> Position { get; init; }

    void Clear();
    void Draw(ReadOnlySpan<char> symbols, Vector2<int> position);
    void Display();
}