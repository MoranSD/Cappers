using Gameplay.UnitSystem.Data;

namespace Gameplay.UnitSystem.Upgrade.Menu
{
    public struct UnitToUpgradeData
    {
        public int Id;
        public bool IsMaxLevel;

        public int Price;
        public UnitBodyType BodyType;

        public float Health;
        public float Speed;
        public float Damage;

        public string UpgradeStatsInfo;
    }
}
