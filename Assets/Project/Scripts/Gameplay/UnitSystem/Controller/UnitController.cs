using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : MonoBehaviour, IUnitController
    {
        public EcsEntity EcsEntity { get; private set; }

        public void Initialize(EcsEntity ecsEntity)
        {
            EcsEntity = ecsEntity;
        }
    }
}
