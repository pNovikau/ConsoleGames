using Core.Common;

namespace Core;

public interface IRenderArea
{
    IReadOnlyList<char> Symbols { get; }
    public Vector2<int> Size { get; init; }
    public Vector2<int> Position { get; init; }

    void Clear();
    void Draw(ReadOnlySpan<char> symbols, Vector2<int> position);
    void Display(TextWriter writer);
}