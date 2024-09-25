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
        public IPlayerMovement Movement => movementView;
        public IPlayerLookView Look => lookView;
        public IPlayerFightView Fight => fightView;
        public IHealthView Health => healthView;

        [SerializeField] private PlayerMovementView movementView;
        [SerializeField] private PlayerLookView lookView;
        [SerializeField] private PlayerFightView fightView;
        [SerializeField] private HealthView healthView;

        public Vector3 GetPosition() => movementView.GetPosition();

        public void ApplyDamage(float damage)
        {

        }
    }
}
