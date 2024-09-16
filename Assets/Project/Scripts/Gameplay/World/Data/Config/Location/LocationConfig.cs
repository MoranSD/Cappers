using Infrastructure.SceneLoad;
using UnityEngine;

namespace Gameplay.World.Data
{
    public abstract class LocationConfig : ScriptableObject
    {
        public string LocationName;
        public SceneType LocationSceneType;

        public abstract Location CreateLocation(int locationId);
    }
}
