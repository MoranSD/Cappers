using Cysharp.Threading.Tasks;
using Gameplay.SeaFight.Ship.View;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.SeaFight.View
{
    public interface ISeaFightView
    {
        UniTask<IEnemyShipView> ShowShip(CancellationToken token);
        void HideShip();
    }
}
