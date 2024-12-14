using Gameplay.EnemySystem.Factory;
using Gameplay.QuestSystem;
using Gameplay.SeaFight.Ship.View;
using Gameplay.Ship.Fight;
using Gameplay.Travel;
using Infrastructure;
using Infrastructure.Composition;
using UnityEngine;

namespace Gameplay.SeaFight.Root
{
    public class SeaFightInstaller : Installer
    {
        [SerializeField] private EnemyShipView seaFightView;

        private SeaFightSystem seaFightSystem;

        public override void Initialize()
        {
            var travelSystem = ServiceLocator.Get<TravelSystem>();
            var questManager = ServiceLocator.Get<QuestManager>();
            var shipFight = ServiceLocator.Get<ShipFight>();
            var enemyFactory = ServiceLocator.Get<IEnemyFactory>();

            seaFightSystem = new(travelSystem, questManager, seaFightView, shipFight, enemyFactory);
            seaFightSystem.Initialize();

            ServiceLocator.Register(seaFightView);
            ServiceLocator.Register(seaFightSystem);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<EnemyShipView>();
            ServiceLocator.Remove<SeaFightSystem>();
            seaFightSystem.Dispose();
        }
    }
}
