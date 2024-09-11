using Infrastructure.Root;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Composition;
using Infrastructure.Panels;
using Infrastructure.Travel;
using Infrastructure.SceneLoad;
using Infrastructure.DataProviding;
using Gameplay.Game;

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

            ServiceLocator.Remove<Game>();
            
            var input = ServiceLocator.Get<IInput>();
            ServiceLocator.Remove<IInput>();

            ServiceLocator.Remove<ISceneLoader>();
            ServiceLocator.Remove<ICompositionController>();
            ServiceLocator.Remove<IAssetProvider>();

            var tickManager = ServiceLocator.Get<TickManager>();
            tickManager.Remove(input as ITickable);
            ServiceLocator.Remove<TickManager>();

            ServiceLocator.Remove<PanelsManager>();
            ServiceLocator.Remove<TravelSystem>();
            ServiceLocator.Remove<GameData>();

            ServiceLocator.Clear();
        }
    }
}