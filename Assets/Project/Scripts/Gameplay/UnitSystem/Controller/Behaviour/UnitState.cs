using Utils.StateMachine;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitState : IState
    {
        protected readonly UnitController controller;

        public UnitState(UnitController controller)
        {
            this.controller = controller;
        }
    }
}
