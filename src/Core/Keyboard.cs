using System.Runtime.InteropServices;

namespace Core;

public static partial class Keyboard
{
    [LibraryImport("user32.dll")]
    private static partial short GetKeyState(int nVirtKey);

    public static bool IsKeyPressed(ConsoleKey keyCode)
    {
        var state = GetKeyState((int)keyCode);

        return (state & 0x8000) != 0;
    }
}