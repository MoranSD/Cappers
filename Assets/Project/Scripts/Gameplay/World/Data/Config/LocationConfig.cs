using UnityEngine;

namespace World.Data
{
    public abstract class LocationConfig : ScriptableObject
    {
        public int Id;
        public string Name;

        public abstract Location CreateLocation();
    }
}
