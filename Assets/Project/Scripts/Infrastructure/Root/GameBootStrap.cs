using Infrastructure.Composition;
using Infrastructure.Curtain;
using Infrastructure.Routine;
using UnityEngine;

namespace Infrastructure.Root
{
    public class GameBootStrap : MonoBehaviour, ICoroutineRunner
    {
        public Game Game { get; private set; }

        [SerializeField] private LoadingCurtain loadingCurtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            var curtain = Instantiate(loadingCurtain, transform);
            var sceneLoader = new SceneLoad.SceneLoader(this);
            var compositionController = new CompositionController();
            Game = new Game(curtain, sceneLoader, compositionController);
            Game.Start();
        }

        private void Update()
        {
            Game.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Game.Stop();
        }
    }
}