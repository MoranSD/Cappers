using Infrastructure.Curtain;
using Infrastructure.Root;
using Infrastructure.SceneLoad;
using Infrastructure.Map;
using System;
using System.Collections.Generic;
using Infrastructure.Composition;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(Game game, ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, ICompositionController compositionController)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(game, this, sceneLoader),
                [typeof(LoadProgressState)] = new LoadProgressState(this, game, ServiceLocator.Get<WorldMapService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, loadingCurtain, sceneLoader, compositionController),
                [typeof(DisposeServicesState)] = new DisposeServicesState(compositionController),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();

            TState state = GetState<TState>();
            activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          states[typeof(TState)] as TState;
    }
}
