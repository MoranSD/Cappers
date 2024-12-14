using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoad
{
    public class SceneLoader : ISceneLoader
    {
        private CancellationTokenSource cancellationTokenSource;

        public void Initialize()
        {
            cancellationTokenSource = new();
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        public async void Load(SceneType sceneType, Action onLoaded = null)
        {
            await LoadProcess(sceneType, cancellationTokenSource.Token, onLoaded);
        }

        public async UniTask LoadAsync(SceneType sceneType, CancellationToken token) => await LoadProcess(sceneType, token, null);

        private async UniTask LoadProcess(SceneType sceneType, CancellationToken token, Action onLoaded = null)
        {
            var asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

            await UniTask.WaitWhile(() => asyncOperation.isDone == false, PlayerLoopTiming.Update, token);
            onLoaded?.Invoke();
        }
    }
}
