using Core.Common;

namespace Core;

public class ConsoleRenderer : IRenderer
{
    private readonly IRenderArea _renderArea;

    public ConsoleRenderer(int height, int width)
    {
        Height = height;
        Width = width;

        _renderArea = new RenderArea(new Vector2<int>(width, height), new Vector2<int>());
    }

    public int Width { get; }

    public int Height { get; }

    public void Clear()
    {
        _renderArea.Clear();

        Console.Clear();
    }

    public void Draw(ReadOnlySpan<char> symbols, Vector2<int> position)
    {
        _renderArea.Draw(symbols, position);
    }

    public void Display()
    {
        _renderArea.Display(Console.Out);
    }
}