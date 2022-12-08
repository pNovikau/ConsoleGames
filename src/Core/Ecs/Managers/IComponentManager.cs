using Core.Ecs.Components;

namespace Core.Ecs.Managers;

public interface IComponentManager
{
    ref TComponent CreateComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>;

    ref TComponent GetComponent<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;

    Span<TComponent> GetComponentAsSpan<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;

    void Delete<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;
}