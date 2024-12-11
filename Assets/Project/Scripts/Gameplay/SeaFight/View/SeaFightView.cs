using Cysharp.Threading.Tasks;
using Gameplay.SeaFight.Ship.View;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.SeaFight.View
{
    public class SeaFightView : MonoBehaviour, ISeaFightView
    {
        [SerializeField] private EnemyShipView enemyShipViewPrefab;

        private EnemyShipView currentShip;

        public void HideShip()
        {
            Destroy(currentShip.gameObject);
        }

        public async UniTask<IEnemyShipView> ShowShip(CancellationToken token)
        {
            currentShip = Instantiate(enemyShipViewPrefab);
            await UniTask.Delay(0, false, PlayerLoopTiming.Update, token);
            if (token.IsCancellationRequested) return null;
            return currentShip;
        }
    }
}
