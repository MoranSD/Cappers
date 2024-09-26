using System;
using UnityEngine;
using Utils;

namespace Gameplay.Components.Health
{
    public class HealthView : MonoBehaviour, IHealthView, IAttackTarget
    {
        public event Action<float> OnGetDamage;

        private Collider bodyCollider;

        public void Initialize(Collider bodyCollider)
        {
            this.bodyCollider = bodyCollider;
        }

        public void ApplyDamage(float damage)
        {
            Debug.Log($"get damage {damage}");
            OnGetDamage?.Invoke(damage);
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
