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

        public override string ToString()
        {
            return $"{Id} \n {BodyType} \n {Health} \n {Speed} \n {Damage}";
        }
    }
}
