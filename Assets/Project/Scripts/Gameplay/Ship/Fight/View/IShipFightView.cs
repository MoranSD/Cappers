using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public interface IShipFightView
    {
        int CannonAttackZonesCount { get; }
        int BoardingPivotsCount { get; }
        Transform GetBoardingPivot(int id);
        UniTask DrawCannonZoneDanger(int zoneId);
        void ApplyDamageInZone(int zoneId, float damage);
    }
}
