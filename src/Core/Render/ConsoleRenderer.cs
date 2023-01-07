using Core.Common;

namespace Core.Render;

public class ConsoleRenderer : IRenderer
{
    private readonly CompositeRenderArea _renderArea = new();

    public ConsoleRenderer(int height, int width)
    {
        Height = height;
        Width = width;

        _renderArea.Append(new WindowsConsoleRenderArea(new Vector2<int>(width, height), new Vector2<int>(0, 0)));
    }

    public int Width { get; }

    public int Height { get; }

    public void AppendRenderArea(Vector2<int> size, Vector2<int> position)
    {
        _renderArea.Append(new WindowsConsoleRenderArea(size, position));
    }

    public void Clear()
    {
        _renderArea.Clear();
    }

    public void Draw(ReadOnlySpan<char> symbols, Vector2<int> position)
    {
        _renderArea.Draw(symbols, position);
    }

    public void Display()
    {
        _renderArea.Display();
    }
}