using Core.Common;

namespace PingPong.Resources;

public class Sprite
{
    private readonly string _symbols = default!;

    public Sprite(Vector2<int> size)
    {
        Size = size;
    }

    public required string Symbols
    {
        get => _symbols;
        init => _symbols = Normalize(value);
    }

    public Vector2<int> Size { get; }

    private static string Normalize(string symbols)
    {
        return symbols
            .TrimStart('\r', '\n')
            .TrimStart('\r', '\n')
            .Replace(' ', '\0')
            .Replace("\r", string.Empty);
    }
}