using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct WeaponAttackRequest
    {
        public EcsEntity WeaponSender;
        public EcsEntity Target;
    }
}
