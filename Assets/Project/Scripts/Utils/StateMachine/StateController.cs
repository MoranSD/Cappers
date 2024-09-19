using System.Collections.Generic;
using System.Linq;

namespace Utils.StateMachine
{
    public class StateController
    {
        private Dictionary<string, IState> states;
        private IState currentState;
        private IUpdateableState currentUpdateableState;

        public StateController(params IState[] states)
        {
            this.states = states.ToDictionary(x => x.GetType().ToString(), x => x);
            currentState = null;
            currentUpdateableState = null;
        }

        public void AddState<T>(IState state) where T : IState
        {
            states.Add(typeof(T).ToString(), state);
        }

        public void UpdateCurrentState(float deltaTime)
        {
            currentUpdateableState?.Update(deltaTime);
        }

        public void ExitCurrent()
        {
            if(currentState == null) return;

            if (currentState is IExitableState exitable)
                exitable.Exit();

            currentUpdateableState = null;
        }

        public void ChangeState<S, T>(T payload) where S : IState, IPayloadedEnterableState<T>
        {
            ExitCurrent();

            var payloadedState = (S)states[typeof(S).ToString()];
            currentState = payloadedState;
            payloadedState.Enter(payload);

            ResetUpdateableState();
        }

        public void ChangeState<T>() where T : IState
        {
            ExitCurrent();

            currentState = states[typeof(T).ToString()];

            if (currentState is IEnterableState enterable)
                enterable.Enter();

            ResetUpdateableState();
        }

        private void ResetUpdateableState()
        {
            currentUpdateableState = currentState is IUpdateableState updateable ? updateable : null;
        }
    }
}
