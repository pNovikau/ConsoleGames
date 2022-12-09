using System.Numerics;

namespace Core.Common;

public record struct Rectangle<TNumber>(Vector2<TNumber> Position, Vector2<TNumber> Size)
    where TNumber : INumber<TNumber>
{
    public static readonly Rectangle<TNumber> Empty = new();

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