using Cysharp.Threading.Tasks;
using System.Threading;

namespace Gameplay.SeaFight.Ship.View
{
    public interface IEnemyShipView
    {
        UniTask Show(CancellationToken token);
        void Hide();
        void SetCriticalZonesActive(bool active);
        void DrawCannonAttack();
    }
}
