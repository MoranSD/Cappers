using Cysharp.Threading.Tasks;
using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Ship.Fight.View
{
    public class ShipFightView : MonoBehaviour, IShipFightView
    {
        public int CannonAttackZonesCount => cannonAttackZones.Length;
        public int BoardingPivotsCount => boardingPivots.Length;

        [SerializeField] private CannonAttackZone[] cannonAttackZones;
        [SerializeField] private Transform[] boardingPivots;

        private EcsWorld world;

        public void Initialize(EcsWorld world)
        {
            this.world = world;
        }

        public void ApplyDamageInZone(int zoneId, float damage)
        {
            var zone = cannonAttackZones[zoneId];
            world.NewEntityWithComponent<OneFrameDamageZone>(new()
            {
                Center = zone.transform.position,
                Border = zone.Border,
                Orientation = zone.transform.rotation,
                Damage = damage,
            });
        }

        public UniTask DrawCannonZoneDanger(int zoneId) => cannonAttackZones[zoneId].DrawDanger();
        public Transform GetBoardingPivot(int id) => boardingPivots[id];
    }
}
