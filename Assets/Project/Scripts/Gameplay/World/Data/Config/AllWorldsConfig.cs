using UnityEngine;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/AllWorldsConfig")]
    public class AllWorldsConfig : ScriptableObject
    {
        [SerializeField] private GameWorldConfig[] worlds;

        public GameWorldConfig GetWorldConfig(int worldId) => worlds[worldId];
    }
}
