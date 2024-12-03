using Gameplay.EnemySystem.Factory;
using Gameplay.SeaFight.Ship.View;
using Gameplay.Ship.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.SeaFight.Ship
{
    public class EnemyShip
    {
        public event Action OnFightEnd;

        private readonly IEnemyShipView view;
        private readonly IEnemyFactory enemyFactory;
        private readonly ShipFight shipFight;

        private CancellationTokenSource cancellationTokenSource;

        public EnemyShip(IEnemyShipView view, IEnemyFactory enemyFactory, ShipFight shipFight)
        {
            this.view = view;
            this.enemyFactory = enemyFactory;
            this.shipFight = shipFight;
        }

        public void Dispose()
        {
            if(cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        public async void BeginFight()
        {
            cancellationTokenSource = new();

            while (true)
            {
                if(new Random().Next(0, 10) >= 5) await CannonAttackProcess();
                else await BoardingAttackProcess();

                if(cancellationTokenSource.IsCancellationRequested) return;

                await Task.Delay(3000, cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;

                //check for win
                if (shipFight.IsDead)
                {
                    OnFightEnd?.Invoke();
                    return;
                }
            }
        }

        private async Task CannonAttackProcess()
        {
            var targetsZones = GenerateIds(shipFight.CannonAttackZonesCount, 3);
            var attackTasks = new List<Task>();

            foreach(var zone in targetsZones)
            {
                var attackTask = shipFight.ApplyDamageInZone(zone, 5, cancellationTokenSource.Token);
                attackTasks.Add(attackTask);
            }

            await Task.WhenAll(attackTasks);

            if (cancellationTokenSource.IsCancellationRequested) return;

            view.DrawCannonAttack();
        }

        private async Task BoardingAttackProcess()
        {
            var targetPivots = GenerateIds(shipFight.BoardingPivotsCount, 3);

            foreach(var pivotId in targetPivots)
            {
                var pivot = shipFight.GetBoardingPivot(pivotId);
                enemyFactory.CreateBoardingEnemy(pivot);

                await Task.Delay(1000, cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }
        }

        private int[] GenerateIds(int N, int count)
        {
            // Проверяем, достаточно ли уникальных чисел
            if (count > N + 1)
            {
                throw new ArgumentException("Запрашиваемое количество чисел превышает доступные уникальные числа от 0 до N.");
            }

            // Генерируем уникальные числа от 0 до N
            Random random = new Random();
            return Enumerable.Range(0, N + 1)
                             .OrderBy(x => random.Next())
                             .Take(count)
                             .ToArray();
        }
    }
}
