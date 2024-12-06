namespace Gameplay.SeaFight.Ship.View
{
    public interface IEnemyShipView
    {
        void SetCriticalZonesActive(bool active);
        void DrawCannonAttack();
    }
}
