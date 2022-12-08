namespace Core.Common.Memory;

public static class ValueStringExtensions
{
    public static ValueString AsValueString(this string @string, Span<char> buffer) => new(@string, buffer);
}