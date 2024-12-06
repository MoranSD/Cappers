using Gameplay.EnemySystem;
using Gameplay.EnemySystem.Factory;
using Gameplay.SeaFight.Ship.View;
using Gameplay.Ship.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace Gameplay.SeaFight.Ship
{
    public class EnemyShip
    {
        public event Action OnFightEnd;

        private readonly IEnemyShipView view;
        private readonly IEnemyFactory enemyFactory;
        private readonly ShipFight shipFight;

        private float health;

        private CancellationTokenSource cancellationTokenSource;

        public EnemyShip(IEnemyShipView view, IEnemyFactory enemyFactory, ShipFight shipFight)
        {
            this.view = view;
            this.enemyFactory = enemyFactory;
            this.shipFight = shipFight;
            health = 10;
        }

        public void Dispose()
        {
            if(cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        public void ApplyDamage(float damage, bool isCritical)
        {
            health -= damage * (isCritical ? 2 : 1);
        }

        public void SetCriticalZonesActive(bool active) => view.SetCriticalZonesActive(active);

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

                if (shipFight.IsDead || health <= 0)
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
            var enemies = new List<IEnemyController>(targetPivots.Length);

            foreach(var pivotId in targetPivots)
            {
                var pivot = shipFight.GetBoardingPivot(pivotId);
                var enemy = enemyFactory.CreateBoardingEnemy(pivot);
                enemies.Add(enemy);

                await Task.Delay(1000, cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }

            await TaskUtils.WaitWhile(() => enemies.All(x => x.IsAlive == false), cancellationTokenSource.Token);
        }

        private int[] GenerateIds(int N, int count)
        {
            // Проверяем, достаточно ли уникальных чисел
            if (count > N)
            {
                throw new ArgumentException("Запрашиваемое количество чисел превышает доступные уникальные числа от 0 до N.");
            }

            // Генерируем уникальные числа от 0 до N
            Random random = new Random();
            return Enumerable.Range(0, N)
                             .OrderBy(x => random.Next())
                             .Take(count)
                             .ToArray();
        }
    }
}
