using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.EnemySystem.Movement
{
    public class EnemyMovementView : MonoBehaviour, IEnemyMovementView
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        private Vector3 destinationPosition;
        private bool hasDestination;

        private void Update()
        {
            if (hasDestination)
            {
                if(Vector3.Distance(GetPosition(), destinationPosition) <= agent.stoppingDistance)
                    OnReachDestination();
            }
        }

        public Vector3 GetPosition() => transform.position;
        public void LookAt(Vector3 position)
        {
            position.y = transform.position.y;
            transform.LookAt(position);
        }

        public void SetDestination(Vector3 destination, float speed)
        {
            hasDestination = true;
            destinationPosition = destination;
            agent.isStopped = false;
            agent.speed = speed;
            animator.SetTrigger(EnemyConstants.WalkAnimationName);
            agent.SetDestination(destination);
        }

        public void Stop()
        {
            OnReachDestination();
            agent.isStopped = true;
        }

        private void OnReachDestination()
        {
            hasDestination = false;
            animator.SetTrigger(EnemyConstants.IdleAnimationName);
        }
    }
}
