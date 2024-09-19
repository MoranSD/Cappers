using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.EnemySystem.Movement
{
    public class EnemyMovement : MonoBehaviour, IEnemyMovement
    {
        [SerializeField] private NavMeshAgent agent;

        public Vector3 GetPosition() => transform.position;
        public void LookAt(Vector3 position)
        {
            position.y = transform.position.y;
            transform.LookAt(position);
        }

        public void SetDestination(Vector3 destination, float speed)
        {
            agent.isStopped = false;
            agent.speed = speed;
            agent.SetDestination(destination);
        }

        public void Stop()
        {
            agent.isStopped = true;
        }
    }
}
