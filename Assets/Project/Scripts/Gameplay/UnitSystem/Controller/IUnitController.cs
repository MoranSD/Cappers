using Gameplay.UnitSystem.Data;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Controller
{
    public interface IUnitController
    {
        UnitData Data { get; }

        void GoToIdlePosition(Vector3 position);
        void InteractWith(IUnitInteractable interactable);
        void BeginCannonInteract(Transform cannonPivot);
        void EndCannonInteract();
        void Destroy();
    }
}
