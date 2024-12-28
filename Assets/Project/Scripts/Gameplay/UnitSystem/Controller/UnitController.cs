using Gameplay.EnemySystem;
using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : MonoBehaviour, IUnitController, IEcsEntityHolder
    {
        public bool IsInteracting { get; private set; }
        public bool HasJob => EcsEntity.Has<UnitInteractJobComponent>();
        public EcsWorld EcsWorld { get; private set; }
        public EcsEntity EcsEntity { get; private set; }
        public int Id { get; private set; }
        public bool IsFollowingPlayer => EcsEntity.Has<TagUnderFollowControl>();

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private Vector3 idlePosition;

        public void Initialize(EcsWorld ecsWorld, EcsEntity ecsEntity, int id, Vector3 idlePosition)
        {
            EcsWorld = ecsWorld;
            EcsEntity = ecsEntity;
            Id = id;
            this.idlePosition = idlePosition;
        }

        public void GoToIdlePosition(Vector3 position)
        {
            idlePosition = position;
            EventBus.Invoke(new AgentSetDestinationRequest()
            {
                Target = EcsEntity,
                Destination = position
            });
        }
        public void GoToIdlePosition()
        {
            EventBus.Invoke(new AgentSetDestinationRequest()
            {
                Target = EcsEntity,
                Destination = idlePosition
            });
        }

        public void InteractWith(IUnitInteractable interactable)
        {
            EventBus.Invoke(new UnitInteractJobRequest()
            {
                Target = EcsEntity,
                Interactable = interactable
            });
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void BeginCannonInteract(Transform cannonPivot)
        {
            IsInteracting = true;
            transform.position = cannonPivot.position;
            EventBus.Invoke<UnitBeginInteractEvent>(new()
            {
                Entity = EcsEntity,
            });
        }

        public void EndCannonInteract()
        {
            IsInteracting = false;
            EventBus.Invoke<UnitEndInteractEvent>(new()
            {
                Entity = EcsEntity,
            });
        }

        public void Attack(IEnemyController enemy)
        {
            EventBus.Invoke<BeginAgroRequest>(new()
            {
                Entity = EcsEntity,
                Target = ((IEcsEntityHolder)enemy).EcsEntity
            });
        }

        public void Repair(ShipHole hole)
        {
            var interactor = hole.View as IUnitInteractable;
            InteractWith(interactor);
        }

        public void Use(Cannon cannon)
        {
            var interactor = cannon.View as IUnitInteractable;
            InteractWith(interactor);
        }
    }
}
