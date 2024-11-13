using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;
using UnityEngine;

namespace Gameplay.UnitSystem.Factory
{
    public interface IUnitFactory
    {
        OldUnitController Create(UnitData unitData, Vector3 spawnPosition);
    }
}
