using Gameplay.Game.ECS.Features;
using Gameplay.UnitSystem.Data;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : MonoBehaviour, IUnitController
    {
        public EcsEntity EcsEntity { get; private set; }
        public UnitData Data { get; private set; }

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        public void Initialize(EcsEntity ecsEntity, UnitData data)
        {
            EcsEntity = ecsEntity;
            Data = data;
        }

        public void GoToIdlePosition(Vector3 position)
        {
            ref var setEvent = ref EcsEntity.Get<AgentSetDestinationEvent>();
            setEvent.Destination = position;
        }
    }
}
