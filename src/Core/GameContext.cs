using Core.Ecs;
using Core.Render;

namespace Core;

public record struct GameContext (GameTime GameTime, IGameWorld GameWorld, IRenderer Renderer);