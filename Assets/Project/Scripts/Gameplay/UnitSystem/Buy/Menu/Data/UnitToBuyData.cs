using Gameplay.UnitSystem.Data;

namespace Gameplay.UnitSystem.Buy.Data
{
    public struct UnitToBuyData
    {
        public int Id;
        public int Price;
        public UnitBodyType BodyType;
        public float Health;
        public float Speed;
        public float Damage;

        public UnitData ToUnitData(int unitId)
        {
            return new()
            {
                Id = unitId,
                UpgradeLevel = 0,
                BodyType = BodyType,
                CurrentHealth = Health,
                MaxHealth = Health,
                Speed = Speed,
                Damage = Damage,
            };
        }
    }
}
