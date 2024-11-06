using Gameplay.Components.Health;
using Gameplay.UnitSystem.Controller.Look;
using Gameplay.UnitSystem.Controller.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.UnitSystem.Controller.View
{
    public class UnitView : MonoBehaviour, IUnitView, IAttackTargetView
    {
        public UnitController Controller { get; private set; }
        public IUnitMovementView MovementView => movementView;
        public IHealthView HealthView => healthView;
        public IAttackTarget Target => Controller;
        public IUnitLookView LookView => throw new System.NotImplementedException();

        [SerializeField] private UnitMovementView movementView;
        [SerializeField] private HealthView healthView;

        public void SetController(UnitController controller)
        {
            Controller = controller;
        }
    }
}
