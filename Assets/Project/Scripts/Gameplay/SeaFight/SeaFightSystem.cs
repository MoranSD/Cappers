using Gameplay.EnemySystem.Factory;
using Gameplay.QuestSystem;
using Gameplay.SeaFight.Ship;
using Gameplay.SeaFight.Ship.View;
using Gameplay.Ship.Fight;
using Gameplay.Travel;
using System;
using System.Linq;

namespace Gameplay.SeaFight
{
    public class SeaFightSystem
    {
        public bool IsInFight { get; private set; }
        public EnemyShip EnemyShip { get; private set; }

        private readonly TravelSystem travelSystem;
        private readonly QuestManager questManager;

        public SeaFightSystem(TravelSystem travelSystem, QuestManager questManager, IEnemyShipView shipView, ShipFight shipFight, IEnemyFactory enemyFactory)
        {
            EnemyShip = new(shipView, enemyFactory, shipFight);

            this.travelSystem = travelSystem;
            this.questManager = questManager;
        }

        public void Initialize()
        {
            EnemyShip.Initialize();
            EnemyShip.OnFightEnd += OnFightEnd;
            travelSystem.OnLeaveLocation += OnTravelBegin;
        }

        public void Dispose()
        {
            EnemyShip.OnFightEnd -= OnFightEnd;
            EnemyShip.Dispose();
            travelSystem.OnLeaveLocation -= OnTravelBegin;
        }

        public void BeginFight()
        {
            if (IsInFight)
                throw new System.Exception();

            IsInFight = true;
            EnemyShip.BeginFight();
        }

        private void OnTravelSkip()
        {
            //todo:
        }

        private void OnTravelBegin()
        {
            //TODO: нужно посчитать просто шанс появления боя либо, если есть квест на доставку, то шанс 100%
            //еще не стоит это делать сразу, лучше подождать N время
            //в будущем, всегда будет максимум 1 бой и всегда будет некая задержка
            //но если игрок скипает поездку, то после скипа будет затемнение экрана
            //потом либо поездка скипнется и будет загрузка сцены, либо начнется бой
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

            //TODO: if player is dead do nothing
            if (travelSystem.IsTraveling && travelSystem.IsPaused)
                travelSystem.Unpause();
        }
    }
}
