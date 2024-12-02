﻿using Gameplay.EnemySystem.Factory;
using Gameplay.QuestSystem;
using Gameplay.SeaFight.Ship;
using Gameplay.SeaFight.View;
using Gameplay.Ship.Fight;
using Gameplay.Travel;
using System.Linq;
using System.Threading;

namespace Gameplay.SeaFight
{
    public class SeaFightSystem
    {
        public bool IsInFight { get; private set; }

        private readonly TravelSystem travelSystem;
        private readonly QuestManager questManager;
        private readonly IEnemyFactory enemyFactory;
        private readonly ShipFight shipFight;

        private readonly ISeaFightView view;

        private CancellationTokenSource cancellationTokenSource;
        private EnemyShip enemyShip;

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
        }

        public void Dispose()
        {
            travelSystem.OnLeaveLocation -= OnTravelBegin;

            if(cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
            if(IsInFight)
            {
                enemyShip.OnFightEnd -= OnFightEnd;
                enemyShip.Dispose();
            }
        }

        public async void BeginFight()
        {
            if (IsInFight)
                throw new System.Exception();

            IsInFight = true;
            cancellationTokenSource = new();

            var newShipView = await view.ShowShip(cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            enemyShip = new(newShipView, enemyFactory, shipFight);
            enemyShip.OnFightEnd += OnFightEnd;
            enemyShip.BeginFight();

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
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

            enemyShip.OnFightEnd -= OnFightEnd;
            enemyShip.Dispose();
            enemyShip = null;

            //TODO: if player is dead so do nothing
            if (travelSystem.IsTraveling && travelSystem.IsPaused)
                travelSystem.Unpause();
        }
    }
}
