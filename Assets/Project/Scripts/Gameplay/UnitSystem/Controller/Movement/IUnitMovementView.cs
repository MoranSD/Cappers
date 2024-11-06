using UnityEngine;

namespace Gameplay.UnitSystem.Controller.Movement
{
    public interface IUnitMovementView
    {
        bool HasDestination { get; }
        float RemainingDistance { get; }
        void SetDestination(Vector3 destination);
        void Stop();
        Vector3 GetPosition();
    }
}
