using Infrastructure.SceneLoad;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootStrap : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);

            var sceneLoader = new SceneLoader(this);

            ServiceLocator.Register<ISceneLoader>(sceneLoader);
            ServiceLocator.Register<ICoroutineRunner>(this);

            sceneLoader.Load(SceneType.ShipAtSea);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove<ISceneLoader>();
            ServiceLocator.Remove<ICoroutineRunner>();
        }
    }
}