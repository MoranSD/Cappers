using Gameplay.Components.Health;
using Gameplay.Player.Fight;
using Gameplay.Player.Look;
using Gameplay.Player.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView, IAttackTargetView
    {
        public OldPlayerController Controller { get; private set; }
        public Transform UnitFollowPivot => transform;
        public IPlayerMovement Movement => movement;
        public IPlayerLookView Look => look;
        public IPlayerFightView Fight => fight;
        public IHealthView Health => health;
        public IAttackTarget Target => Controller;

        [SerializeField] private PlayerMovementView movement;
        [SerializeField] private PlayerLookView look;
        [SerializeField] private PlayerFightView fight;
        [SerializeField] private HealthView health;
        [SerializeField] private Collider bodyCollider;

        public void Initialize(OldPlayerController controller)
        {
            Controller = controller;
            health.Initialize(bodyCollider);
        }
    }
}
