using Cysharp.Threading.Tasks;
using Gameplay.SeaFight.Ship.View;
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

        public async UniTask<IEnemyShipView> ShowShip()
        {
            currentShip = Instantiate(enemyShipViewPrefab);
            await UniTask.Delay(0);
            return currentShip;
        }
    }
}
