using Infrastructure.Root;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Map;
using Infrastructure.Composition;

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
            ServiceLocator.Remove<IInput>();
            ServiceLocator.Remove<TickManager>();

            ServiceLocator.Remove<WorldMapService>();
        }
    }
}