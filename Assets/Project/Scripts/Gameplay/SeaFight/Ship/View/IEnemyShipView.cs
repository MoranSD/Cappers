using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Gameplay.SeaFight.Ship.View
{
    public interface IEnemyShipView
    {
        UniTask Show(CancellationToken token);
        void Hide();
        void DrawGetHit(float currentHP);
        void DrawDie();
        bool DidHitCriticalZone(Transform hitPoint);
        void SetCriticalZonesActive(bool active);
        void DrawCannonAttack();
    }
}
