using System.Linq;
using UnityEngine;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/AllWorldsConfig")]
    public class AllWorldsConfig : ScriptableObject
    {
        [SerializeField] private GameWorldConfig[] worlds;

        public GameWorldConfig GetWorldConfig(int worldId)
        {
            return worlds.FirstOrDefault(x => x.Id == worldId);
        }
    }
}
