using Gameplay.UnitSystem.Data;
using UnityEngine;

namespace Gameplay.UnitSystem.Controller
{
    public interface IUnitController
    {
        UnitData Data { get; }

        void GoToIdlePosition(Vector3 position);
        void Destroy();
    }
}
