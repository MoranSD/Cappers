using Cysharp.Threading.Tasks;
using Gameplay.Game;
using Gameplay.Ship.Fight.Hole;
using Gameplay.Ship.Fight.View;
using System;
using System.Threading;
using UnityEngine;

namespace Gameplay.Ship.Fight
{
    public class ShipFight
    {
        public event Action<ShipHole> OnGotHole;

        private readonly IShipFightView view;
        private readonly GameState gameState;
        private readonly IShipHoleFactory holeFactory;

        public int CannonAttackZonesCount => view.CannonAttackZonesCount;
        public int BoardingPivotsCount => view.BoardingPivotsCount;
        public bool IsDead => gameState.ShipHealth <= 0;

        public ShipFight(IShipFightView view, GameState gameState, IShipHoleFactory holeFactory)
        {
            this.view = view;
            this.gameState = gameState;
            this.holeFactory = holeFactory;
        }

        public Transform GetBoardingPivot(int id) => view.GetBoardingPivot(id);

        public void ApplyDamage(float damage)
        {
            if (gameState.ShipHealth <= 0) return;

            gameState.ShipHealth -= damage;
            view.DrawGetDamage(gameState.ShipHealth);
        }

        public async UniTask ApplyCannonDamageInZone(int zoneId, float damage, CancellationToken token)
        {
            if (gameState.ShipHealth <= 0) return;

            await view.DrawCannonZoneDanger(zoneId, token);

            if (token.IsCancellationRequested) return;

            view.ApplyDamageInZone(zoneId, damage);

            gameState.ShipHealth -= damage;
            view.DrawGetDamage(gameState.ShipHealth);

            if (new System.Random().Next(10) >= 8)
            {
                var hole = holeFactory.CreateHoleInZone(zoneId, this);
                OnGotHole?.Invoke(hole);
            }
        }
    }
}
