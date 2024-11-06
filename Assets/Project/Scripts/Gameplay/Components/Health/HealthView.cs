using UnityEngine;

namespace Gameplay.Components.Health
{
    public class HealthView : MonoBehaviour, IHealthView
    {
        private Collider bodyCollider;

        public void Initialize(Collider bodyCollider)
        {
            this.bodyCollider = bodyCollider;
        }

        public void DrawDie()
        {
            bodyCollider.enabled = false;
        }

        public void DrawGetDamage()
        {
        }

        public Vector3 GetPosition() => transform.position;
    }
}
