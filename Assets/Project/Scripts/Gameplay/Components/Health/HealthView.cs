using System;
using UnityEngine;
using Utils;

namespace Gameplay.Components.Health
{
    public class HealthView : MonoBehaviour, IHealthView, IAttackTarget
    {
        public event Action<float> OnGetDamage;

        public void ApplyDamage(float damage)
        {
            Debug.Log($"get damage {damage}");
            OnGetDamage?.Invoke(damage);
        }

        public void DrawDie()
        {

        }

        public void DrawGetDamage()
        {
        }

        public Vector3 GetPosition() => transform.position;
    }
}
