using Core.Common;

namespace Core.Render;

internal class CompositeRenderArea : IRenderArea
{
    private readonly List<IRenderArea> _renderAreas;
    
    public Vector2<int> Size { get; init; }
    public Vector2<int> Position { get; init; }

    public CompositeRenderArea()
    {
        _renderAreas = new List<IRenderArea>();
    }
    
    public CompositeRenderArea(params IRenderArea[] renderAreas)
    {
        _renderAreas = new List<IRenderArea>(renderAreas);
    }

    public void Append(IRenderArea renderArea)
    {
        _renderAreas.Add(renderArea);
    }

    public void Clear()
    {
        foreach (var renderArea in _renderAreas) 
            renderArea.Clear();
    }

    public void Draw(ReadOnlySpan<char> symbols, Vector2<int> position)
    {
        foreach (var renderArea in _renderAreas) 
            renderArea.Draw(symbols, position);
    }

    public void Display()
    {
        foreach (var renderArea in _renderAreas) 
            renderArea.Display();
    }
}