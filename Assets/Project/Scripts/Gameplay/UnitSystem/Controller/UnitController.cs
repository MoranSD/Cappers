using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Gameplay.UnitSystem.Data;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Utils;
using Utils.Interaction;

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
            EcsWorld.NewOneFrameEntity(new AgentSetDestinationRequest()
            {
                Target = EcsEntity,
                Destination = position
            });
        }

        public void InteractWith(IUnitInteractable interactable)
        {
            EcsWorld.NewOneFrameEntity(new UnitInteractJobRequest()
            {
                Target = EcsEntity,
                Interactable = interactable
            });
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
