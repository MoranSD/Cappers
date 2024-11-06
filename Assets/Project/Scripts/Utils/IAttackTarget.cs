using UnityEngine;

namespace Utils
{
    public interface IAttackTarget : IDamageable
    {
        bool IsDead { get; }
        Vector3 GetPosition();
    }
}
