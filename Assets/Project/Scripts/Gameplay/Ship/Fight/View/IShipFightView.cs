using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public interface IShipFightView
    {
        int CannonAttackZonesCount { get; }
        int BoardingPivotsCount { get; }
        Transform GetBoardingPivot(int id);
        Task DrawCannonZoneDanger(int zoneId, CancellationToken token);
        void ApplyDamageInZone(int zoneId, float damage);
    }
}
