using Gameplay.EnemySystem;
using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using Gameplay.UnitSystem.Data;
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
        public EcsWorld EcsWorld { get; private set; }
        public EcsEntity EcsEntity { get; private set; }
        public UnitData Data => data;

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private UnitData data;
        private Vector3 idlePosition;

        public void Initialize(EcsWorld ecsWorld, EcsEntity ecsEntity, UnitData data, Vector3 idlePosition)
        {
            EcsWorld = ecsWorld;
            EcsEntity = ecsEntity;
            this.data = data;
            this.idlePosition = idlePosition;
        }

        public void UpdateHealthData(float health)
        {
            data.Health = health;
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
            ref var agro = ref EcsEntity.Get<TargetAgroComponent>();
            agro.HasTarget = true;
            agro.Target = ((IEcsEntityHolder)enemy).EcsEntity;
        }

        public void Repair(ShipHole hole)
        {
            var interactor = hole.View as IUnitInteractable;
            InteractWith(interactor);
        }

        public bool IsAlive() => data.Health > 0;

        public void Use(Cannon cannon)
        {
            var interactor = cannon.View as IUnitInteractable;
            InteractWith(interactor);
        }
    }
}
