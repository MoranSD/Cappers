using Gameplay.Game;
using Infrastructure;
using UnityEngine;
using Gameplay.Ship.Fight;
using Gameplay.Ship.Fight.View;
using Leopotam.Ecs;

namespace Gameplay.Ship.Root
{
    public class ShipAtSeaShipInstaller : ShipInstaller
    {
        [SerializeField] private ShipFightView shipFightView;

        public override void PostInitialize()
        {
            base.PostInitialize();

            var gameState = ServiceLocator.Get<GameState>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            shipFightView.Initialize(ecsWorld);
            var shipFight = new ShipFight(shipFightView, gameState);

            ServiceLocator.Register(shipFight);
        }

        public override void Dispose()
        {
            base.Dispose();

            ServiceLocator.Remove<ShipFight>();
        }
    }
}
