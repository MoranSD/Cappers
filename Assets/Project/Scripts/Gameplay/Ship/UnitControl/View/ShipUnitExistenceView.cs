using Gameplay.Ship.UnitControl.View;
using System.Linq;
using UnityEngine;

namespace Gameplay.Ship.UnitControl.Placement.View
{
    public class ShipUnitExistenceView : MonoBehaviour, IShipUnitExistenceView
    {
        [SerializeField] private Transform[] unitPositions;

        public Vector3[] GetUnitPositions() => unitPositions.Select(x => x.position).ToArray();
    }
}
