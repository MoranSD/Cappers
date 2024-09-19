using Gameplay.EnemySystem.View;
using UnityEngine;

namespace Gameplay.EnemySystem.Data
{
    [CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
    public class EnemyConfigSO : ScriptableObject
    {
        public EnemyType enemyType;
        public EnemyView ViewPrefab;
        public EnemyConfig Config;
    }
}
