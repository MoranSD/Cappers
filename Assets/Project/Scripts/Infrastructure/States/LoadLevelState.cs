using Infrastructure.Composition;
using Infrastructure.Curtain;
using Infrastructure.SceneLoad;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<SceneType>
    {
        private readonly GameStateMachine stateMachine;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly ICompositionController compositionController;

        public LoadLevelState(GameStateMachine stateMachine, ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ICompositionController compositionController)
        {
            this.stateMachine = stateMachine;
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.compositionController = compositionController;
        }

        public void Enter(SceneType sceneType)
        {
            loadingCurtain.Show();
            compositionController.Dispose();
            sceneLoader.Load(sceneType, OnSceneLoaded);
        }

        public void Exit()
        {

        }

        private void OnSceneLoaded()
        {
            compositionController.Initialize();
            loadingCurtain.Hide();
        }
    }
}