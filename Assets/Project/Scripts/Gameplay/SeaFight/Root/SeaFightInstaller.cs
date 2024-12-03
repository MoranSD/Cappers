using Gameplay.EnemySystem.Factory;
using Gameplay.QuestSystem;
using Gameplay.SeaFight.View;
using Gameplay.Ship.Fight;
using Gameplay.Travel;
using Infrastructure;
using Infrastructure.Composition;
using UnityEngine;

namespace Gameplay.SeaFight.Root
{
    public class SeaFightInstaller : Installer
    {
        [SerializeField] private SeaFightView seaFightView;

        private SeaFightSystem seaFightSystem;

        public override void PostInitialize()
        {
            var travelSystem = ServiceLocator.Get<TravelSystem>();
            var questManager = ServiceLocator.Get<QuestManager>();
            var shipFight = ServiceLocator.Get<ShipFight>();
            var enemyFactory = ServiceLocator.Get<IEnemyFactory>();

            seaFightSystem = new(travelSystem, questManager, seaFightView, shipFight, enemyFactory);
            seaFightSystem.Initialize();
        }

        public override void Dispose()
        {
            seaFightSystem.Dispose();
        }
    }
}
