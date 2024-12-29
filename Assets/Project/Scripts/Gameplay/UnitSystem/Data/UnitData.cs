using System;

namespace Gameplay.UnitSystem.Data
{
    [Serializable]
    public struct UnitData
    {
        public int Id;
        public int UpgradeLevel;
        public UnitBodyType BodyType;
        public float CurrentHealth;
        public float MaxHealth;
        public float Speed;
        public float Damage;

        public override string ToString()
        {
            return $"{Id} \n {BodyType} \n {CurrentHealth} \n {Speed} \n {Damage}";
        }
    }
}
