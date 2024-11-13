using Gameplay.UnitSystem.Controller;
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

        public UnitController GetBody(UnitBodyType type) => bodies.First(x => x.Type == type).Body;

        [Serializable]
        private struct UnitBodyByType
        {
            public UnitController Body;
            public UnitBodyType Type;
        }
    }
}
