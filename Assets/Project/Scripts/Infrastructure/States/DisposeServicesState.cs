﻿using Infrastructure.Root;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Composition;
using Gameplay.Panels;
using Gameplay.Travel;
using Infrastructure.SceneLoad;
using Infrastructure.DataProviding;
using Gameplay.Game;
using Gameplay.LevelLoad;
using Gameplay.QuestSystem;
using Infrastructure.Routine;

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

            ServiceLocator.Remove<ICoroutineRunner>();
            var input = ServiceLocator.Remove<IInput>();
            ServiceLocator.Remove<ISceneLoader>();
            ServiceLocator.Remove<ICompositionController>();
            ServiceLocator.Remove<IAssetProvider>();
            var tickManager = ServiceLocator.Remove<TickManager>();
            tickManager.Remove(input as ITickable);
            ServiceLocator.Remove<PanelsManager>();
            ServiceLocator.Remove<TravelSystem>();
            ServiceLocator.Remove<ILevelLoadService>();
            ServiceLocator.Remove<GameState>();
            var questManager = ServiceLocator.Remove<QuestManager>();
            questManager.Dispose();

            ServiceLocator.Clear();
        }
    }
}