using Gameplay.SeaFight.Ship.View;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.SeaFight.View
{
    public interface ISeaFightView
    {
        Task<IEnemyShipView> ShowShip(CancellationToken token);
        void HideShip();
    }
}
