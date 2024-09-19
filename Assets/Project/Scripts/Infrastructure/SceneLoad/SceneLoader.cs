using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoad
{
    public class SceneLoader : ISceneLoader
    {
        public async void Load(SceneType sceneType, Action onLoaded = null)
        {
            await LoadProcess(sceneType, onLoaded);
        }

        public async Task LoadAsync(SceneType sceneType) => await LoadProcess(sceneType);

        private async Task LoadProcess(SceneType sceneType, Action onLoaded = null)
        {
            var asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

            await Utils.TaskUtils.WaitWhile(() => asyncOperation.isDone == false);
            onLoaded?.Invoke();
        }
    }
}
