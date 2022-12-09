using Core.Common;

namespace Core;

public class ConsoleRenderer : IRenderer
{
    private readonly int _height;
    private readonly int _width;

    private readonly char[] _renderArea;

    public ConsoleRenderer(int height, int width)
    {
        _height = height;
        _width = width;

        _renderArea = new char[height * width];
        _renderArea.AsSpan().Fill(' ');
    }

    public int Width => _width;
    public int Height => _height;

    public void Clear()
    {
        _renderArea.AsSpan().Clear();

        Console.Clear();
    }

    public void Draw(ReadOnlySpan<char> symbols, Vector2<int> position)
    {
        var area = _renderArea.AsSpan().Slice((_width * position.Y) + position.X, symbols.Length);

        for (var i = 0; i < symbols.Length; i++)
        {
            if (symbols[i] == '\0')
                continue;

            area[i] = symbols[i];
        }
    }

    public void Display()
    {
        for (var i = 0; i < _renderArea.Length; i++)
        {
            if (_renderArea[i] == '\0')
                _renderArea[i] = ' ';
        }
        
        for (var i = 0; i < _height; i++)
        {
            var offset = _width * i;
            Console.Out.WriteLine(_renderArea[offset..(offset + _width)]);
        }
    }
}