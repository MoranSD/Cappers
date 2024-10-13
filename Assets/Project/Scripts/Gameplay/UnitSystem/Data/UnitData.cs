using System;

namespace Gameplay.UnitSystem.Data
{
    [Serializable]
    public struct UnitData
    {
        public int Id;
        public UnitBodyType BodyType;
        public float Health;
        public float Speed;
        public float Damage;
    }
}
