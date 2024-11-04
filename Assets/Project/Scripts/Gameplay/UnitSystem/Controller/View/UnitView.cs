using Gameplay.UnitSystem.Controller.Movement;
using UnityEngine;

namespace Gameplay.UnitSystem.Controller.View
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        public IUnitMovementView MovementView => movementView;

        [SerializeField] private UnitMovementView movementView;
    }
}
