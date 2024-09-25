using Gameplay.Components.Health;
using Gameplay.Player.Fight;
using Gameplay.Player.Look;
using Gameplay.Player.Movement;

namespace Gameplay.Player.View
{
    public interface IPlayerView
    {
        IPlayerMovement Movement { get; }
        IPlayerLookView Look { get; }
        IPlayerFightView Fight { get; }
        IHealthView Health { get; }
    }
}
