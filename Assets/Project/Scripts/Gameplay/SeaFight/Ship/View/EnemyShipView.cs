using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Gameplay.SeaFight.Ship.View
{
    public class EnemyShipView : MonoBehaviour, IEnemyShipView
    {
        [field: SerializeField] public EnemyShipAimZone AimZone { get; private set; }
        [SerializeField] private GameObject viewGO;
        [SerializeField] private Collider[] criticalZones;

        public void DrawCannonAttack()
        {
            //по большей части это звук выстрела и эффект дума из пушек
            //todo
        }

        public void SetCriticalZonesActive(bool active)
        {
            foreach (var zone in criticalZones)
                zone.gameObject.SetActive(active);
        }

        public async UniTask Show(CancellationToken token)
        {
            viewGO.SetActive(true);
            await UniTask.Delay(0, false, PlayerLoopTiming.Update, token);
        }

        public void Hide()//корабль остается позади (анимация/эффект аля наш корабль движется вперед, но ЭТО наш движется назад)
        {
            viewGO.SetActive(false);
        }

        public void DrawDie()//корабль разрушен но типо все еще движется вместе с нашим кораблем
        {

        }

        public bool DidHitCriticalZone(Transform hitPoint)
        {
            return criticalZones.Any(x => x.bounds.Contains(hitPoint.position));
        }

        public void DrawGetHit(float currentHP)
        {
            //обновить хп на UI
        }
    }
}
