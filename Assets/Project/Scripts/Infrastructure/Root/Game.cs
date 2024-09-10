namespace Infrastructure.Root
{
    using Infrastructure.Composition;
    using Infrastructure.Curtain;
    using Infrastructure.SceneLoad;
    using Infrastructure.TickManagement;
    using States;
    using World;

    public class Game
    {
        public GameWorld GameWorld { get; private set; }

        private GameStateMachine stateMachine;
        private bool isGameStarted;
        private TickManager tickManager;

        public Game(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ICompositionController compositionController)
        {
            stateMachine = new GameStateMachine(this, sceneLoader, loadingCurtain, compositionController);
        }

        public void Start()
        {
            if (isGameStarted) return;
            isGameStarted = true;

            stateMachine.Enter<BootStrapState>();
            tickManager = ServiceLocator.Get<TickManager>();
        }
        public void Update(float deltaTime)
        {
            if (isGameStarted == false) return;

            tickManager.Update(deltaTime);
        }
        public void Stop()
        {
            if(isGameStarted == false) return;
            isGameStarted = false;

            stateMachine.Enter<DisposeServicesState>();
        }

        public void SetWorld(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }
    }
}
