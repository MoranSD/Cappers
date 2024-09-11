using Gameplay.Player.Look;
using Gameplay.Player.Movement;
using UnityEngine;

namespace Gameplay.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public IPlayerMovementView MovementView => movementView;
        public IPlayerLook LookView => lookView;

        [SerializeField] private PlayerMovementView movementView;
        [SerializeField] private PlayerLook lookView;
    }
}
