using System.Numerics;
using System.Runtime.CompilerServices;

namespace Core.Common;

public static class Math
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPointInRectangle<TNumber>(in Rectangle<TNumber> rectangle, in Vector2<TNumber> point)
        where TNumber : INumber<TNumber>
    {
        return point.X >= rectangle.Left &&
               point.X <= rectangle.Right &&
               point.Y >= rectangle.Top &&
               point.Y <= rectangle.Bottom;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> GetOverlappingArea<TNumber>(in Rectangle<TNumber> rectangle1, in Rectangle<TNumber> rectangle2)
        where TNumber : INumber<TNumber>
    {
        var left = TNumber.Max(rectangle1.Left, rectangle2.Left);
        var right = TNumber.Min(rectangle1.Right, rectangle2.Right);
        var top = TNumber.Max(rectangle1.Top, rectangle2.Top);
        var bottom = TNumber.Min(rectangle1.Bottom, rectangle2.Bottom);

        if (left >= right || top >= bottom)
            return Rectangle<TNumber>.Empty;

        var position = new Vector2<TNumber>(left, top);
        var size = new Vector2<TNumber>(right - left, bottom - top);

        return new Rectangle<TNumber>(position, size);
    }
}