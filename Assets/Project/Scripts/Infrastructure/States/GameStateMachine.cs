using Infrastructure.Curtain;
using Infrastructure.Root;
using System;
using System.Collections.Generic;
using Infrastructure.Composition;
using Infrastructure.Routine;
using Gameplay.Game;
using Infrastructure.DataProviding;
using Gameplay.LevelLoad;

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
                [typeof(BootStrapState)] = new BootStrapState(this, coroutineRunner, loadingCurtain),
                [typeof(LoadProgressState)] = new LoadProgressState(ServiceLocator.Get<ILevelLoadService>(), game, ServiceLocator.Get<GameData>(), ServiceLocator.Get<IAssetProvider>()),
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
