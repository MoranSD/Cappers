using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.UnitSystem.Controller.Movement
{
    public class UnitMovementView : MonoBehaviour, IUnitMovementView
    {
        [SerializeField] private NavMeshAgent agent;

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
    }
}
