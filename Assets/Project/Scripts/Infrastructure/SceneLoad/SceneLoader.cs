using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoad
{
    public class SceneLoader : ISceneLoader
    {
        public void Dispose()
        {
        }

        public async void Load(SceneType sceneType, Action onLoaded = null)
        {
            await LoadProcess(sceneType, onLoaded);
        }

        public async UniTask LoadAsync(SceneType sceneType) => await LoadProcess(sceneType);

        private async UniTask LoadProcess(SceneType sceneType, Action onLoaded = null)
        {
            var asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

            await UniTask.WaitWhile(() => asyncOperation.isDone == false);
            onLoaded?.Invoke();
        }
    }
}
