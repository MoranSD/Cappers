using UnityEngine;

namespace Gameplay.EnemySystem.Movement
{
    public interface IEnemyMovement
    {
        void SetSpeed(float speed);
        void SetDestination(Vector3 destination);
        void Stop();
    }
}
