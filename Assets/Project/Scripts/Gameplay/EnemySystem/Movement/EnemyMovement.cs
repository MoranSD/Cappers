using Pathfinding;
using UnityEngine;

namespace Gameplay.EnemySystem.Movement
{
    public class EnemyMovement : MonoBehaviour, IEnemyMovement
    {
        [SerializeField] private AIPath agent;

        public Vector3 GetPosition() => transform.position;
        public void LookAt(Vector3 position)
        {
            position.y = transform.position.y;
            transform.LookAt(position);
        }

        public void SetDestination(Vector3 destination, float speed)
        {
            agent.canMove = true;
            agent.maxSpeed = speed;
            agent.destination = destination;
        }

        public void Stop()
        {
            agent.canMove = false;
        }
    }
}
