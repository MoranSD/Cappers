using System;
using UnityEngine;

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
        public float MeleeMoveSlowDownDuration;
        public float LongMoveSlowDownDuration;
        public float SlowedMoveSpeed;
        public float MeleePushForce;
        public float PushForceDuration;
        public Vector3 MeleeDamageZoneBorders;
    }
}
