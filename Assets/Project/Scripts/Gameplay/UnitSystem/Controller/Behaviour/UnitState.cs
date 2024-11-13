using Utils.StateMachine;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitState : IState
    {
        protected readonly OldUnitController controller;

        public UnitState(OldUnitController controller)
        {
            this.controller = controller;
        }
    }
}
