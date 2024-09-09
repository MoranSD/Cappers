using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoad
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner runner;
        private bool isLoading = false;

        public SceneLoader(ICoroutineRunner runner)
        {
            this.runner = runner;
        }

        public void Load(SceneType sceneType, Action onLoaded = null)
        {
            if (isLoading) return;
            isLoading = true;

            runner.StartCoroutine(LoadProcess(sceneType, onLoaded));
        }

        private IEnumerator LoadProcess(SceneType sceneType, Action onLoaded = null)
        {
            var asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

            yield return new WaitUntil(() => asyncOperation.isDone);

            onLoaded?.Invoke();
            isLoading = false;
        }
    }
}
