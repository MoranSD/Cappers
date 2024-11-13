using Gameplay.UnitSystem.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : MonoBehaviour, IUnitController
    {
        public EcsEntity EcsEntity { get; private set; }
        public UnitData Data { get; private set; }

        public void Initialize(EcsEntity ecsEntity, UnitData data)
        {
            EcsEntity = ecsEntity;
            Data = data;
        }

        public void GoToIdlePosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
