using Cysharp.Threading.Tasks;
using System;

namespace Infrastructure.SceneLoad
{
    public interface ISceneLoader
    {
        void Load(SceneType sceneType, Action onLoaded = null);
        UniTask LoadAsync(SceneType sceneType);
    }
}
