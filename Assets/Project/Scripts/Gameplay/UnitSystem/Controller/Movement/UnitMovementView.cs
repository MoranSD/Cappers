using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.UnitSystem.Controller.Movement
{
    public class UnitMovementView : MonoBehaviour, IUnitMovementView
    {
        [SerializeField] private NavMeshAgent agent;

        public bool HasDestination => agent.isStopped;
        public float RemainingDistance => agent.remainingDistance;

        public Vector3 GetPosition() => transform.position;

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void Stop()
        {
            agent.isStopped = true;
        }
    }
}
