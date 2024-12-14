using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.View;
using Gameplay.Ship.UnitControl.Placement.View;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Ship
{
    public class ShipViewsLink : MonoBehaviour
    {
        public ShipUnitExistenceView ShipUnitExistenceView;
        public ShipFightView ShipFightView;
        public List<CannonView> CannonViews;
    }
}
