using Gameplay.EnemySystem;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using Gameplay.UnitSystem.Data;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Controller
{
    public interface IUnitController
    {
        UnitData Data { get; }

        bool IsAlive();
        void Use(Cannon cannon);
        void Attack(IEnemyController enemy);
        void Repair(ShipHole hole);
        void GoToIdlePosition(Vector3 position);
        void GoToIdlePosition();
        void InteractWith(IUnitInteractable interactable);
        void BeginCannonInteract(Transform cannonPivot);
        void EndCannonInteract();
    }
}
