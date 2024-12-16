﻿using Gameplay.Game;
using Infrastructure;
using Gameplay.Ship.Fight;
using Leopotam.Ecs;
using System.Linq;
using System.Collections.Generic;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.SeaFight;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;
using Gameplay.Player.InteractController;
using Gameplay.SeaFight.Ship.View;
using Gameplay.Ship.Fight.Hole;

namespace Gameplay.Ship.Root
{
    public class ShipAtSeaShipInstaller : ShipInstaller
    {
        private List<Cannon> activeCannons;

        public override void Initialize()
        {
            base.Initialize();

            var gameState = ServiceLocator.Get<GameState>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();
            var tickManager = ServiceLocator.Get<TickManager>();

            shipViewsLink.ShipFightView.Initialize(ecsWorld);
            var holeFactory = new ShipHoleFactory(tickManager, shipViewsLink.HoleView, shipViewsLink.ShipFightView);
            var shipFight = new ShipFight(shipViewsLink.ShipFightView, gameState, holeFactory);

            ServiceLocator.Register(shipFight);
        }
        public override void AfterInitialize()
        {
            base.AfterInitialize();

            var gameState = ServiceLocator.Get<GameState>();
            var playerInteract = ServiceLocator.Get<PlayerInteractController>();
            var input = ServiceLocator.Get<IInput>();
            var seaFightSystem = ServiceLocator.Get<SeaFightSystem>();
            var tickManager = ServiceLocator.Get<TickManager>();
            var seaFightView = ServiceLocator.Get<EnemyShipView>();

            activeCannons = new List<Cannon>();

            for (int i = 0; i < shipViewsLink.CannonViews.Count; i++)
            {
                var cannonView = shipViewsLink.CannonViews[i];
                if (gameState.Cannons.Any(x => x.ShipPosition == i) == false)
                    continue;

                var cannonInfo = gameState.Cannons.First(x => x.ShipPosition == i);
                var cannon = new Cannon(playerInteract, input, seaFightSystem, cannonView);
                cannonView.Initialize(seaFightView, input);
                cannon.Initialize(cannonInfo);
                tickManager.Add(cannon);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            ServiceLocator.Remove<ShipFight>();

            var tickManager = ServiceLocator.Get<TickManager>();
            var gameState = ServiceLocator.Get<GameState>();

            foreach (var cannon in activeCannons)
            {
                tickManager.Remove(cannon);
                cannon.Dispose();
            }

            for (int i = 0; i < shipViewsLink.CannonViews.Count; i++)
            {
                if (gameState.Cannons.Any(x => x.ShipPosition == i) == false)
                    continue;

                var cannonView = shipViewsLink.CannonViews[i];
                cannonView.Dispose();
            }
        }
    }
}
