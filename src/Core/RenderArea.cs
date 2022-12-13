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
        var index = 0;
        for (var y = position.Y; y < Size.Y && index < symbols.Length; y++)
        {
            for (var x = position.X; x < Size.X && index < symbols.Length; x++, index++)
            {
                var renderAreaIndex = (Size.X * y) + x;

                if (symbols[index] == '\n')
                {
                    ++index;
                    break;
                }

                if (symbols[index] == '\0')
                    continue;

                _renderArea[renderAreaIndex] = symbols[index];
            }
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
                {
                    _renderArea[index] = ' ';
                }
            }

            if (index != _renderArea.Length)
            {
                _renderArea[index++] = '\n';
            }
        }

        writer.Write(_renderArea);
    }
}