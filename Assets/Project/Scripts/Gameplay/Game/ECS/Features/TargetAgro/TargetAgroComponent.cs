using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct TargetAgroComponent
    {
        public bool HasTarget;
        public EcsEntity Target;
        public float Damage;
        public float AttackDistance;
        public float AttackRate;
        public float NextAttackTime;
    }
}
