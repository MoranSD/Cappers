using Gameplay.EnemySystem.Data;
using UnityEngine;

namespace Gameplay.EnemySystem.Spawn
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public Transform SpawnPoint => transform;
        public EnemyConfigSO ConfigSO;
    }
}
