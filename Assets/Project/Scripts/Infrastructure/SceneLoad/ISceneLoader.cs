using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.SceneLoad
{
    public interface ISceneLoader
    {
        void Load(SceneType sceneType, Action onLoaded = null);
        Task LoadAsync(SceneType sceneType, CancellationToken token);
    }
}
