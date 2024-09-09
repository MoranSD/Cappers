using System;

namespace Infrastructure.SceneLoad
{
    public interface ISceneLoader
    {
        void Load(SceneType sceneType, Action onLoaded = null);
    }
}
