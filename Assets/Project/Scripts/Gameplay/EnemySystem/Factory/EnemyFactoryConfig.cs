using Gameplay.EnemySystem.Data;
using System.Linq;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    [CreateAssetMenu(menuName = "EnemySystem/Factory")]
    public class EnemyFactoryConfig : ScriptableObject
    {
        [SerializeField] private DefaultEnemyConfigByType[] defaultConfigs;

        public EnemyConfigSO GetDefaultConfig(EnemyType type) => defaultConfigs.First(x => x.Type == type).Config;

        [System.Serializable]
        private struct DefaultEnemyConfigByType
        {
            public EnemyType Type;
            public EnemyConfigSO Config;
        }
    }
}
