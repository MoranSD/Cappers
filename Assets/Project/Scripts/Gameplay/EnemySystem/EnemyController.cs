using Gameplay.Game.ECS;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.EnemySystem
{
    public class EnemyController : MonoBehaviour, IEnemyController, IEcsEntityHolder
    {
        public EcsEntity EcsEntity { get; private set; }

        public void Initialize(EcsEntity ecsEntity)
        {
            EcsEntity = ecsEntity;
        }
    }
}
