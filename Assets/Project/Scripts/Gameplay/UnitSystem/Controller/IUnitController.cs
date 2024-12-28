using Gameplay.EnemySystem;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Controller
{
    public interface IUnitController
    {
        int Id { get; }
        bool IsInteracting { get; }
        bool HasJob { get; }
        bool IsFollowingPlayer { get; }

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
