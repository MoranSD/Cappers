using Gameplay.EnemySystem.Factory;
using Gameplay.QuestSystem;
using Gameplay.SeaFight.Ship;
using Gameplay.SeaFight.View;
using Gameplay.Ship.Fight;
using Gameplay.Travel;
using System.Linq;

namespace Gameplay.SeaFight
{
    public class SeaFightSystem
    {
        public bool IsInFight { get; private set; }
        public EnemyShip EnemyShip { get; private set; }

        private readonly TravelSystem travelSystem;
        private readonly QuestManager questManager;
        private readonly IEnemyFactory enemyFactory;
        private readonly ShipFight shipFight;

        private readonly ISeaFightView view;

        public SeaFightSystem(TravelSystem travelSystem, QuestManager questManager, ISeaFightView view, ShipFight shipFight, IEnemyFactory enemyFactory)
        {
            this.travelSystem = travelSystem;
            this.questManager = questManager;
            this.shipFight = shipFight;
            this.enemyFactory = enemyFactory;

            this.view = view;
        }

        public void Initialize()
        {
            travelSystem.OnLeaveLocation += OnTravelBegin;
        }

        public void Dispose()
        {
            travelSystem.OnLeaveLocation -= OnTravelBegin;

            if(IsInFight)
            {
                EnemyShip.OnFightEnd -= OnFightEnd;
                EnemyShip.Dispose();
            }
        }

        public async void BeginFight()
        {
            if (IsInFight)
                throw new System.Exception();

            IsInFight = true;

            var newShipView = await view.ShowShip();

            EnemyShip = new(newShipView, enemyFactory, shipFight);
            EnemyShip.OnFightEnd += OnFightEnd;
            EnemyShip.BeginFight();
        }

        private void OnTravelBegin()
        {
            //TODO: нужно посчитать просто шанс появления боя либо, если есть квест на доставку, то шанс 100%
            if (questManager.ActiveQuests.Any(x => x.QuestType == QuestSystem.Data.QuestType.delivery) == false)
                return;

            travelSystem.Pause();
            BeginFight();
        }

        private void OnFightEnd()
        {
            if(IsInFight == false)
                throw new System.Exception();

            IsInFight = false;
            view.HideShip();

            EnemyShip.OnFightEnd -= OnFightEnd;
            EnemyShip.Dispose();
            EnemyShip = null;

            //TODO: if player is dead so do nothing
            if (travelSystem.IsTraveling && travelSystem.IsPaused)
                travelSystem.Unpause();
        }
    }
}
