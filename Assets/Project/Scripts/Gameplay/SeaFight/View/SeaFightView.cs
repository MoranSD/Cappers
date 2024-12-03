using Gameplay.SeaFight.Ship.View;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.SeaFight.View
{
    public class SeaFightView : MonoBehaviour, ISeaFightView
    {
        public void HideShip()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnemyShipView> ShowShip(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
