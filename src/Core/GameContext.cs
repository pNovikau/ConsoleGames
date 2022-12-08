using Core.Ecs;

namespace Core;

public record struct GameContext (GameTime GameTime, IGameWorld GameWorld, IRenderer Renderer);