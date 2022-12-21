using Core.Common;

namespace Core.Render;

public interface IRenderer
{
    public int Width { get; }
    public int Height { get; }

    void AppendRenderArea(Vector2<int> size, Vector2<int> position);
    void Clear();
    void Draw(ReadOnlySpan<char> symbols, Vector2<int> position);
    void Display();
}