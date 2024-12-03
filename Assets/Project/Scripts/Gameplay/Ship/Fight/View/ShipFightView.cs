using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public class ShipFightView : MonoBehaviour, IShipFightView
    {
        public int CannonAttackZonesCount => cannonAttackZones.Length;
        public int BoardingPivotsCount => boardingPivots.Length;

        [SerializeField] private CannonAttackZone[] cannonAttackZones;
        [SerializeField] private Transform[] boardingPivots;

        public void ApplyDamageInZone(int zoneId, float damage)
        {
            /*
             * создать дамаг зону в ецс
             */
        }

        public Task DrawCannonZoneDanger(int zoneId, CancellationToken token) => cannonAttackZones[zoneId].DrawDanger(token);
        public Transform GetBoardingPivot(int id) => boardingPivots[id];
    }
}
