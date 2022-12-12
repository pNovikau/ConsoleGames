using Core.Common;

namespace Core;

internal class RenderArea : IRenderArea
{
    private readonly char[] _renderArea;

    public RenderArea(Vector2<int> size, Vector2<int> position)
    {
        size.X += 1;
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
        var index = 0;
        for (var i = 0; i < Size.Y; i++)
        {
            for (var j = 0; j < (Size.X - 1); j++, index++)
            {
                if (_renderArea[index] == '\0')
                    _renderArea[index] = ' ';
            }

            if (index != _renderArea.Length)
                _renderArea[index++] = '\n';
        }

        writer.Write(_renderArea);
    }
}