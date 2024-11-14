using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Gameplay.UnitSystem.Data;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : MonoBehaviour, IUnitController, IEcsEntityHolder
    {
        public EcsWorld EcsWorld { get; private set; }
        public EcsEntity EcsEntity { get; private set; }
        public UnitData Data { get; private set; }

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        public void Initialize(EcsWorld ecsWorld, EcsEntity ecsEntity, UnitData data)
        {
            EcsWorld = ecsWorld;
            EcsEntity = ecsEntity;
            Data = data;
        }

        public void GoToIdlePosition(Vector3 position)
        {
            var requestEntity = EcsWorld.NewEntity();
            ref var setRequest = ref requestEntity.Get<AgentSetDestinationRequest>();
            setRequest.Target = EcsEntity;
            setRequest.Destination = position;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
