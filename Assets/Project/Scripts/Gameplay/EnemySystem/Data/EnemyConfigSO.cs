using UnityEngine;

namespace Gameplay.EnemySystem.Data
{
    [CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
    public class EnemyConfigSO : ScriptableObject
    {
        public EnemyType enemyType;
        public EnemyController ViewPrefab;
        public EnemyConfig Config;
    }
}
