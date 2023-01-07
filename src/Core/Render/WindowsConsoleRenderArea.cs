using System.Diagnostics;
using Core.Common;
using Core.Windows;
using Microsoft.Win32.SafeHandles;

namespace Core.Render;

internal sealed class WindowsConsoleRenderArea : IRenderArea
{
    private readonly SafeFileHandle _handler;
    private readonly CharInfo[] _chars;
    
    private SmallRect _renderArea;

    public WindowsConsoleRenderArea(Vector2<int> size, Vector2<int> position)
    {
        Size = size;

        _handler = KernelApi.CreateFile("CONOUT$", 0x40000000, 2, nint.Zero, FileMode.Open, 0, nint.Zero);

        if (_handler.IsInvalid)
            throw new Exception();

        Position = position;
        _chars = new CharInfo[size.X * size.Y];
        _renderArea = new SmallRect { Left = (short)position.X, Top = (short)position.Y, Right = (short)size.X, Bottom = (short)size.Y };
    }

    public Vector2<int> Size { get; init; }
    public Vector2<int> Position { get; init; }

    public void Clear()
    {
        _chars.AsSpan().Fill(new CharInfo());
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

                ref var charInfo = ref _chars[renderAreaIndex];

                charInfo.Char.UnicodeChar = symbols[index];
            }
        }
    }

    public void Display()
    {
        var index = 0;
        for (var i = 0; i < Size.Y; i++)
        {
            for (var j = 0; j < (Size.X - 1); j++, index++)
            {
                if (_chars[index].Char.UnicodeChar == 0)
                {
                    _chars[index].Char.UnicodeChar = ' ';
                }
            }
        }

        KernelApi.WriteConsoleOutputW(
            _handler,
            _chars,
            new Coord() { X = (short)Size.X, Y = (short)Size.Y },
            new Coord() { X = 0, Y = 0 },
            ref _renderArea);
    }
}