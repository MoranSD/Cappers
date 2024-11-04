using UnityEngine;

namespace Gameplay.UnitSystem.Buy.View
{
    public class UnitBuySystemView : MonoBehaviour, IUnitBuySystemView
    {
        [SerializeField] private Transform unitStartPivot;

        public Vector3 GetUnitStatsPosition() => unitStartPivot.position;
    }
}
