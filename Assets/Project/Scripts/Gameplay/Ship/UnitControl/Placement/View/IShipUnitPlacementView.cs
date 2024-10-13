using UnityEngine;

namespace Gameplay.Ship.UnitControl.Placement.View
{
    public interface IShipUnitPlacementView
    {
        Vector3[] GetUnitPositions();
    }
}
