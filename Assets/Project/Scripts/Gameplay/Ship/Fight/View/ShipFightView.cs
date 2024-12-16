using Cysharp.Threading.Tasks;
using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;
using System.Threading;
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
            var zoneEntity = world.NewEntity()
                .Replace(new DamageZone()
                {
                    Center = zone.transform.position,
                    Border = zone.Border,
                    Orientation = zone.transform.rotation,
                    Damage = damage,
                })
                .Replace(new OneFrameEntity());
        }

        public Vector3 GetHolePositionInZone(int zoneId)
        {
            var zone = cannonAttackZones[zoneId];
            var center = zone.transform.position;
            var borders = zone.Border;
            float padding = 2;
            float randomX = Random.Range(center.x + padding - borders.x / 2, center.x - padding + borders.x / 2);
            float randomZ = Random.Range(center.z + padding - borders.z / 2, center.z - padding + borders.z / 2);

            return new Vector3(randomX, center.y, randomZ);
        }
        public UniTask DrawCannonZoneDanger(int zoneId, CancellationToken token) => cannonAttackZones[zoneId].DrawDanger(token);
        public Transform GetBoardingPivot(int id) => boardingPivots[id];

        public void DrawGetDamage(float currentHealth)
        {
            Debug.Log($"Ship health {currentHealth}");
        }
    }
}
