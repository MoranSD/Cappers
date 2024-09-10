using Gameplay.Player.Movement;
using UnityEngine;

namespace Gameplay.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public IPlayerMovementView MovementView => movementView;

        [SerializeField] private PlayerMovementView movementView;
    }
}
