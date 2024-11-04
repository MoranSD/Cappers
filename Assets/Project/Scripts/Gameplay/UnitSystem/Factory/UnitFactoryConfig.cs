using Gameplay.UnitSystem.Controller.View;
using Gameplay.UnitSystem.Data;
using System;
using System.Linq;
using UnityEngine;

namespace Gameplay.UnitSystem.Factory
{
    [CreateAssetMenu(menuName = "UnitSystem/FactoryConfig")]
    public class UnitFactoryConfig : ScriptableObject
    {
        [SerializeField] private UnitBodyByType[] bodies;

        public UnitView GetBody(UnitBodyType type) => bodies.First(x => x.Type == type).Body;

        [Serializable]
        private struct UnitBodyByType
        {
            public UnitView Body;
            public UnitBodyType Type;
        }
    }
}
