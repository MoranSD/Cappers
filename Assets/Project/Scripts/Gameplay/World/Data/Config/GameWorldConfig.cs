using UnityEngine;

namespace World.Data
{
    [CreateAssetMenu(menuName = "World/Config")]
    public class GameWorldConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }

        [SerializeField] private LocationConfig[] locations;
    }
}
