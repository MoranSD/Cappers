using Gameplay.Components.Health;
using Gameplay.Player.Fight;
using Gameplay.Player.Look;
using Gameplay.Player.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView, IAttackTarget
    {
        public IPlayerMovement Movement => movement;
        public IPlayerLookView Look => look;
        public IPlayerFightView Fight => fight;
        public IHealthView Health => health;

        [SerializeField] private PlayerMovementView movement;
        [SerializeField] private PlayerLookView look;
        [SerializeField] private PlayerFightView fight;
        [SerializeField] private HealthView health;
        [SerializeField] private Collider bodyCollider;

        public void Initialize()
        {
            health.Initialize(bodyCollider);
        }

        public Vector3 GetPosition() => movement.GetPosition();

        public void ApplyDamage(float damage)
        {

        }
    }
}
