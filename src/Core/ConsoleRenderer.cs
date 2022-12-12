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

internal class RenderArea : IRenderArea
{
    private readonly char[] _renderArea;

    public RenderArea(Vector2<int> size, Vector2<int> position)
    {
        Size = size;
        Position = position;
        _renderArea = new char[size.X * size.Y];
    }

    public IReadOnlyList<char> Symbols => _renderArea;
    public Vector2<int> Size { get; init; }
    public Vector2<int> Position { get; init; }

    public void Clear()
    {
        _renderArea.AsSpan().Clear();
    }

    public void Draw(ReadOnlySpan<char> symbols, Vector2<int> position)
    {
        var renderAreaIndex = Size.X * position.Y + position.X;

        for (var i = 0; i < symbols.Length; i++, renderAreaIndex++)
        {
            if (symbols[i] == '\n')
            {
                var nextLineNumber = renderAreaIndex / Size.X + 1;
                renderAreaIndex = nextLineNumber * Size.X + position.X - 1;

                continue;
            }

            if (symbols[i] == '\0')
                continue;

            _renderArea[renderAreaIndex] = symbols[i];
        }
    }

    public void Display(TextWriter writer)
    {
        for (var i = 0; i < Size.Y; i++)
        {
            var offset = Size.X * i;
            
            writer.WriteLine(_renderArea[offset..(offset + Size.X)]);
        }
    }
}

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