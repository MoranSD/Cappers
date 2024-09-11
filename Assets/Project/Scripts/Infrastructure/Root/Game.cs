using Infrastructure.Curtain;
using Infrastructure.Routine;
using Infrastructure.TickManagement;
using Infrastructure.States;

namespace Infrastructure.Root
{
    public class Game
    {
        private GameStateMachine stateMachine;
        private bool isGameStarted;
        private TickManager tickManager;

        public Game(ILoadingCurtain loadingCurtain, ICoroutineRunner coroutineRunner)
        {
            stateMachine = new GameStateMachine(this, loadingCurtain, coroutineRunner);
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
    }
}
