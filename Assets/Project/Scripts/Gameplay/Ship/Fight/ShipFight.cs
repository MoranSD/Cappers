using Gameplay.Game;
using Gameplay.Ship.Fight.View;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Ship.Fight
{
    public class ShipFight
    {
        private readonly IShipFightView view;
        private readonly GameState gameState;

        public int CannonAttackZonesCount => view.CannonAttackZonesCount;
        public int BoardingPivotsCount => view.BoardingPivotsCount;
        public bool IsDead => gameState.ShipHealth <= 0;

        public ShipFight(IShipFightView view, GameState gameState)
        {
            this.view = view;
            this.gameState = gameState;
        }

        public Transform GetBoardingPivot(int id) => view.GetBoardingPivot(id);

        public async Task ApplyDamageInZone(int zoneId, float damage, CancellationToken token)
        {
            await view.DrawCannonZoneDanger(zoneId, token);

            if (token.IsCancellationRequested) return;

            view.ApplyDamageInZone(zoneId, damage);

            //todo
            //тут можно посчитать шанс пробоины и нанести урон по хп корабля
            //пробоину можно показать во вьюхе и вызвать ивент, чтобы другая система знала че да как
        }
    }
}
