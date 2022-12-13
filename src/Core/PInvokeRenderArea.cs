using System.Runtime.InteropServices;
using System.Text;
using Core.Common;
using Microsoft.Win32.SafeHandles;

namespace Core;

internal class PInvokeRenderArea : IRenderArea
{
    private readonly SafeFileHandle _hander;
    private readonly CharInfo[] _chars;
    
    private SmallRect _renderArea;

    public PInvokeRenderArea(Vector2<int> size, Vector2<int> position)
    {
        Size = size;

        _hander = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

        if (_hander.IsInvalid)
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

    public void Display(TextWriter writer)
    {
        var index = 0;
        for (var i = 0; i < Size.Y; i++)
        {
            for (var j = 0; j < (Size.X - 1); j++, index++)
            {
                if (_chars[index].Char.UnicodeChar == 0)
                {
                    _chars[index].Char.UnicodeChar = (ushort)' ';
                }
            }
        }

        WriteConsoleOutputW(
            _hander,
            _chars,
            new Coord() { X = (short)Size.X, Y = (short)Size.Y },
            new Coord() { X = 0, Y = 0 },
            ref _renderArea);
    }

    #region Kernel32

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutputW(
        SafeFileHandle hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref SmallRect lpWriteRegion);

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)] public ushort UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes = 7;

        public CharInfo()
        {
            Char = default;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    #endregion
}