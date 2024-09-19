using UnityEngine;

namespace Utils
{
    public interface IAttackTarget : IDamageable
    {
        Vector3 GetPosition();
    }
}
