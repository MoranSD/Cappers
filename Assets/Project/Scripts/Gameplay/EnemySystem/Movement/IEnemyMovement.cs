using UnityEngine;

namespace Gameplay.EnemySystem.Movement
{
    public interface IEnemyMovement
    {
        Vector3 GetPosition();
        void LookAt(Vector3 position);
        void SetDestination(Vector3 destination, float speed);
        void Stop();
    }
}
