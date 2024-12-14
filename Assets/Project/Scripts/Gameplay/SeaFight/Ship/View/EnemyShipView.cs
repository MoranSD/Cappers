using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Gameplay.SeaFight.Ship.View
{
    public class EnemyShipView : MonoBehaviour, IEnemyShipView
    {
        [field: SerializeField] public EnemyShipAimZone AimZone { get; private set; }
        [SerializeField] private GameObject viewGO;
        [SerializeField] private GameObject[] criticalZones;

        public void DrawCannonAttack()
        {
            //todo
        }

        public void SetCriticalZonesActive(bool active)
        {
            foreach (var zone in criticalZones)
                zone.SetActive(active);
        }

        public async UniTask Show(CancellationToken token)
        {
            viewGO.SetActive(true);
            await UniTask.Delay(0, false, PlayerLoopTiming.Update, token);
        }

        public void Hide()
        {
            viewGO.SetActive(false);
        }
    }
}
