using System;

namespace Gameplay.EnemySystem.Data
{
    [Serializable]
    public class EnemyConfig
    {
        public EnemyMovementConfig MovementConfig;
        public EnemyLookConfig LookConfig;
        public EnemyFollowConfig FollowConfig;
        public EnemyAttackConfig AttackConfig;
        public EnemyHealthConfig HealthConfig;
    }

    [Serializable]
    public class EnemyHealthConfig
    {
        public float StartHealthCount;
        public float MaxHealthCount;
    }

    [Serializable]
    public class EnemyMovementConfig
    {
        public float Speed;
    }

    [Serializable]
    public class EnemyLookConfig
    {
        public float VisionRange;
    }

    [Serializable]
    public class EnemyFollowConfig
    {
        public float UpdateDestinationRate;
        public float MaxFollowDistance;
    }

    [Serializable]
    public class EnemyAttackConfig
    {
        public float AttackDistance;
        public float MinTimeToUpdateDelay;
        public float FirstAttackDelay;
        public float AttackRate;
    }
}
