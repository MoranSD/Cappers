using Player.Movement;
using UnityEngine;

namespace Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public IPlayerMovementView MovementView => movementView;

        [SerializeField] private PlayerMovementView movementView;
    }
}
