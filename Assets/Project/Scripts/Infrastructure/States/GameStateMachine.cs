using Infrastructure.Curtain;
using Infrastructure.Root;
using Infrastructure.SceneLoad;
using System;
using System.Collections.Generic;
using Infrastructure.Composition;
using Infrastructure.Routine;
using Gameplay.Game;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(Game game, ILoadingCurtain loadingCurtain, ICoroutineRunner coroutineRunner)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(game, this, coroutineRunner),
                [typeof(LoadProgressState)] = new LoadProgressState(this, game, ServiceLocator.Get<GameData>()),
                [typeof(LoadLevelState)] = new LoadLevelState(loadingCurtain, ServiceLocator.Get<ISceneLoader>(), ServiceLocator.Get<ICompositionController>()),
                [typeof(DisposeServicesState)] = new DisposeServicesState(ServiceLocator.Get<ICompositionController>()),
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
