using Player.Movement;

namespace Player.View
{
    public interface IPlayerView
    {
        IPlayerMovementView MovementView { get; }
    }
}
