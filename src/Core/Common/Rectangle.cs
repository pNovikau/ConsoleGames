using System.Numerics;

namespace Core.Common;

public record struct Rectangle<TNumber>
    where TNumber : INumber<TNumber>
{
    public static readonly Rectangle<TNumber> Empty = new();

    public Vector2<TNumber> Position;
    public Vector2<TNumber> Size;

    public Rectangle(Vector2<TNumber> position, Vector2<TNumber> size)
    {
        Position = position;
        Size = size;
    }

    public readonly TNumber Left => Position.X;
    public readonly TNumber Top => Position.Y;
    public readonly TNumber Right => Position.X + Size.X;
    public readonly TNumber Bottom => Position.Y + Size.Y;

    public readonly bool IsEmpty =>
        TNumber.IsZero(Position.X) &&
        TNumber.IsZero(Position.Y) &&
        TNumber.IsZero(Size.X) &&
        TNumber.IsZero(Size.Y);
}