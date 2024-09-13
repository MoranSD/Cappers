using Infrastructure.SceneLoad;
using UnityEngine;

namespace Gameplay.World.Data
{
    public abstract class LocationConfig : ScriptableObject
    {
        public int Id;
        public string Name;
        public SceneType SceneType;

        public abstract Location CreateLocation();
    }
}
