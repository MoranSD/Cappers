using System.Linq;
using UnityEngine;

namespace Gameplay.Ship.UnitControl.Placement.View
{
    public class ShipUnitPlacementView : MonoBehaviour, IShipUnitPlacementView
    {
        [SerializeField] private Transform[] unitPositions;

        public Vector3[] GetUnitPositions() => unitPositions.Select(x => x.position).ToArray();
    }
}
