using Infrastructure.Root;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Composition;
using Infrastructure.Panels;
using Infrastructure.Travel;
using Infrastructure.SceneLoad;
using Infrastructure.DataProviding;
using Gameplay.Game;
using Gameplay.LevelLoad;

namespace Infrastructure.States
{
    public class DisposeServicesState : IState
    {
        private readonly ICompositionController compositionController;

        public DisposeServicesState(ICompositionController compositionController)
        {
            this.compositionController = compositionController;
        }

        public void Enter()
        {
            ClearServices();
        }

        public void Exit()
        {

        }

        private void ClearServices()
        {
            compositionController.Dispose();

            var input = ServiceLocator.Remove<IInput>();
            ServiceLocator.Remove<ISceneLoader>();
            ServiceLocator.Remove<ICompositionController>();
            ServiceLocator.Remove<IAssetProvider>();
            var tickManager = ServiceLocator.Remove<TickManager>();
            tickManager.Remove(input as ITickable);
            ServiceLocator.Remove<PanelsManager>();
            ServiceLocator.Remove<TravelSystem>();
            ServiceLocator.Remove<ILevelLoadService>();
            ServiceLocator.Remove<GameData>();

            ServiceLocator.Clear();
        }
    }
}