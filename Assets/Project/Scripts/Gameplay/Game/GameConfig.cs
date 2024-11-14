using UnityEngine;

namespace Gameplay.Game
{
    [CreateAssetMenu(menuName = "Game/Config")]
    public class GameConfig : ScriptableObject
    {
        public LayerMask InteractorLayer;
        public LayerMask EnemyLayer;
        public LayerMask UnitLayer;
        public LayerMask PlayerLayer;

        public LayerMask PlayerTargetLayers;
        public LayerMask EnemyTargetLayers;
    }
}
