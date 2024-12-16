using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public interface IShipFightView
    {
        int CannonAttackZonesCount { get; }
        int BoardingPivotsCount { get; }
        Transform GetBoardingPivot(int id);
        UniTask DrawCannonZoneDanger(int zoneId, CancellationToken token);
        void DrawGetDamage(float currentHealth);
        void ApplyDamageInZone(int zoneId, float damage);
    }
}
