using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public class CannonAttackZone : MonoBehaviour
    {
        [field: SerializeField] public Vector3 Border { get; private set; }

        public async Task DrawDanger(CancellationToken token)
        {
            await Task.Delay(0, token);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, Border);
        }
    }
}
