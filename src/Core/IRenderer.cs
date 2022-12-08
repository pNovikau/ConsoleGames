namespace Core;

public interface IRenderer
{
    public int Width { get; }
    public int Height { get; }

    void Clear();
    void Draw(ReadOnlySpan<char> symbols, int x, int y);
    void Display();
}