using UnityEngine;

namespace Gameplay.SeaFight.Ship.View
{
    public class EnemyShipView : MonoBehaviour, IEnemyShipView
    {
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
    }
}
