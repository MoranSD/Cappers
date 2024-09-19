using Gameplay.Player.Look;
using Gameplay.Player.Movement;

namespace Gameplay.Player.View
{
    public interface IPlayerView
    {
        IPlayerMovement Movement { get; }
        IPlayerLook Look { get; }
    }
}
