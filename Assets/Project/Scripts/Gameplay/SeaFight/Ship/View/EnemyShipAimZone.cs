using UnityEngine;

namespace Gameplay.SeaFight.Ship.View
{
    public class EnemyShipAimZone : MonoBehaviour
    {
        [SerializeField] private Vector3 borders;
        [SerializeField] private float padding;

        public Vector3 ClampPosition(Vector3 position)
        {
            var zonePos = transform.position;
            position.x = Mathf.Clamp(position.x, zonePos.x + padding - borders.x / 2, zonePos.x - padding + borders.x / 2);
            position.y = Mathf.Clamp(position.y, zonePos.y + padding - borders.y / 2, zonePos.y - padding + borders.y / 2);
            position.z = Mathf.Clamp(position.z, zonePos.z + padding - borders.z / 2, zonePos.z - padding + borders.z / 2);

            return position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, borders);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, borders - Vector3.one * padding);
        }
    }
}
