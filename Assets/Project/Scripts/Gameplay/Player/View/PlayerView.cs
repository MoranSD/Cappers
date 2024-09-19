using Gameplay.Player.Look;
using Gameplay.Player.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView, IAttackTarget
    {
        public IPlayerMovementView MovementView => movementView;
        public IPlayerLook LookView => lookView;

        [SerializeField] private PlayerMovementView movementView;
        [SerializeField] private PlayerLook lookView;

        public Vector3 GetPosition() => movementView.GetPosition();

        public void ApplyDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
