using System;

namespace Gameplay.Player.Data
{
    [Serializable]
    public class PlayerFightConfig
    {
        public float AttackRange;
        public float MeleeAttackDelay;
        public float LongAttackDelay;
        public float MeleeAttackDistance;
        public float LongAttackDistance;
        public float BaseMeleeDamage;
        public float BaseLongDamage;
    }
}
