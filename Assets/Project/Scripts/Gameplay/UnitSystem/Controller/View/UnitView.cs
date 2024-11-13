using Gameplay.Components.Health;
using Gameplay.UnitSystem.Controller.Look;
using Gameplay.UnitSystem.Controller.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.UnitSystem.Controller.View
{
    public class UnitView : MonoBehaviour, IUnitView, IAttackTargetView
    {
        public OldUnitController Controller { get; private set; }
        public IUnitMovementView MovementView => movementView;
        public IHealthView HealthView => healthView;
        public IAttackTarget Target => Controller;
        public IUnitLookView LookView => lookView;

        [SerializeField] private UnitMovementView movementView;
        [SerializeField] private HealthView healthView;
        [SerializeField] private UnitLookView lookView;
        [SerializeField] private Collider bodyCollider;

        public void Initialize(OldUnitController controller)
        {
            Controller = controller;
            healthView.Initialize(bodyCollider);
        }
    }
}
