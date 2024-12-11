using Cysharp.Threading.Tasks;
using Gameplay.SeaFight.Ship.View;

namespace Gameplay.SeaFight.View
{
    public interface ISeaFightView
    {
        UniTask<IEnemyShipView> ShowShip();
        void HideShip();
    }
}
