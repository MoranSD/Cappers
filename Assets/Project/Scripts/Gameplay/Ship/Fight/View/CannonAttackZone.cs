using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight.View
{
    public class CannonAttackZone : MonoBehaviour
    {
        [field: SerializeField] public Vector3 Border { get; private set; }

        [SerializeField] private GameObject zoneView;

        private void OnValidate()
        {
            zoneView.transform.localScale = Border;
        }

        private void Awake()
        {
            zoneView.SetActive(false);
        }

        public async Task DrawDanger(CancellationToken token)
        {
            zoneView.SetActive(true);

            await Task.Delay(1500, token);
            if (token.IsCancellationRequested) return;

            zoneView.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, Border);
        }
    }
}
