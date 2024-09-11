using UnityEngine;

namespace World.Data
{
    [CreateAssetMenu(menuName = "World/LocationConfig")]
    public abstract class LocationConfig : ScriptableObject
    {
        public int Id;
        public string Name;
    }
}
