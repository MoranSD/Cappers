using System;
using UnityEngine;
using Utils;

namespace Gameplay.EnemySystem.Health
{
    public class EnemyHealthView : MonoBehaviour, IEnemyHealthView, IAttackTarget
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
