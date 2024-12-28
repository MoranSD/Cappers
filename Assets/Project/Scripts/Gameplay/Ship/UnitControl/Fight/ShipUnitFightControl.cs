using Gameplay.EnemySystem;
using Gameplay.Game.ECS.Features;
using Gameplay.SeaFight;
using Gameplay.Ship.Fight;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using Gameplay.UnitSystem;
using Gameplay.UnitSystem.Controller;
using Infrastructure.TickManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.Ship.UnitControl.Fight
{
    public class ShipUnitFightControl : ITickable
    {
        private List<IUnitJob> jobList;
        private List<IUnitJob> processJobs;

        private readonly ShipFight shipFight;
        private readonly SeaFightSystem fightSystem;
        private readonly ShipUnitExistenceControl unitExistenceControl;

        private readonly List<Cannon> activeCannons;
        private readonly List<Cannon> busyCannons;

        private const float setCannonJobRate = 10;
        private float setCannonJobTime = 0;

        public ShipUnitFightControl(ShipFight shipFight, SeaFightSystem fightSystem, ShipUnitExistenceControl unitExistenceControl, List<Cannon> activeCannons)
        {
            this.shipFight = shipFight;
            this.fightSystem = fightSystem;
            this.unitExistenceControl = unitExistenceControl;
            this.activeCannons = activeCannons;
            busyCannons = new();
            jobList = new();
            processJobs = new();
        }

        public void Initialize()
        {
            shipFight.OnGotHole += OnGotHole;
            fightSystem.EnemyShip.OnBoard += OnEnemyBoard;

            EventBus.Subscribe<UnitDieEvent>(OnUnitDie);
            EventBus.Subscribe<UnitCanceledInteractJobEvent>(OnCancelledJob);
        }

        public void Dispose()
        {
            shipFight.OnGotHole -= OnGotHole;
            fightSystem.EnemyShip.OnBoard -= OnEnemyBoard;

            EventBus.Unsubscribe<UnitDieEvent>(OnUnitDie);
            EventBus.Unsubscribe<UnitCanceledInteractJobEvent>(OnCancelledJob);

            foreach (var job in jobList)
                if (job is IDisposableJob disposable)
                    disposable.Dispose();

            foreach (var job in processJobs)
                if (job is IDisposableJob disposable)
                    disposable.Dispose();
        }

        public void Update(float deltaTime)
        {
            if (processJobs.Count > 0 && fightSystem.IsInFight)
            {
                setCannonJobTime += deltaTime;

                if (setCannonJobTime >= setCannonJobRate)
                {
                    setCannonJobTime -= setCannonJobRate;

                    if (HasFreeUnits() == false) return;

                    var availableCannons = activeCannons.Where(x => x.IsAvailable).Except(busyCannons);

                    if(availableCannons.Count() == 0)
                        return;

                    var cannon = availableCannons.ElementAt(new System.Random().Next(availableCannons.Count()));
                    busyCannons.Add(cannon);

                    var job = new UnitUseCannonJob(cannon);
                    job.Initialize();

                    OnNewJob(job);
                }
            }
        }

        private void OnGotHole(ShipHole hole) => OnNewJob(new UnitRepairJob(hole));
        private void OnEnemyBoard(IEnemyController enemy) => OnNewJob(new UnitAttackJob(enemy));

        private bool HasFreeUnits() => GetFreeUnits().Count() > 0;
        private IUnitController GetFreeUnit() => GetFreeUnits().First();
        private IEnumerable<IUnitController> GetFreeUnits()
        {
            return unitExistenceControl.ActiveUnits
            .Except(processJobs.Select(x => x.Executor))
            .Where(x => x.IsFollowingPlayer == false)
            .Where(x => x.HasJob == false)
            .Where(x => x.IsInteracting == false);
        }
        private void OnNewJob(IUnitJob job)
        {
            UpdateLists();

            if (HasFreeUnits()) BeginExecuteJob(job);
            else jobList.Add(job);
        }
        private void UpdateLists()
        {
            for (int i = processJobs.Count - 1; i >= 0; i--)
            {
                if (processJobs[i].IsDone())
                {
                    if (processJobs[i] is ICannonJob job)
                        busyCannons.Remove(job.Cannon);

                    if (processJobs[i] is IDisposableJob disposable)
                        disposable.Dispose();

                    processJobs.RemoveAt(i);
                }
            }

            for (int i = jobList.Count - 1; i >= 0; i--)
            {
                if (jobList[i].IsDone())
                {
                    if (processJobs[i] is ICannonJob job)
                        busyCannons.Remove(job.Cannon);

                    if (jobList[i] is IDisposableJob disposable)
                        disposable.Dispose();

                    jobList.RemoveAt(i);
                }
                else if (HasFreeUnits())
                {
                    var job = jobList[i];
                    jobList.RemoveAt(i);
                    BeginExecuteJob(job);
                }
            }
        }
        private void BeginExecuteJob(IUnitJob job)
        {
            var freeUnit = GetFreeUnit();
            job.Execute(freeUnit);
            processJobs.Add(job);
        }

        private void OnUnitDie(UnitDieEvent dieEvent)
        {
            if (processJobs.Any(x => x.Executor.Id == dieEvent.UnitId) == false) return;

            var job = processJobs.First(x => x.Executor.Id == dieEvent.UnitId);
            processJobs.Remove(job);

            if (job.IsDone() == false && HasFreeUnits()) BeginExecuteJob(job);
            else jobList.Add(job);
        }
        private void OnCancelledJob(UnitCanceledInteractJobEvent cancelledEvent)
        {
            if (processJobs.Any(x => x.Executor.Id == cancelledEvent.UnitId) == false) return;

            var job = processJobs.First(x => x.Executor.Id == cancelledEvent.UnitId);
            processJobs.Remove(job);
            OnNewJob(job);
        }

        private class UnitUseCannonJob : IUnitJob, IDisposableJob, ICannonJob
        {
            public IUnitController Executor { get; private set; }
            public Cannon Cannon { get; private set; }

            private bool isCannonUsed;

            public UnitUseCannonJob(Cannon cannon)
            {
                this.Cannon = cannon;
            }

            public void Initialize()
            {
                Cannon.OnUsed += OnCannonUsed;
            }

            public void Dispose()
            {
                Cannon.OnUsed -= OnCannonUsed;
            }

            public void Execute(IUnitController controller)
            {
                controller.Use(Cannon);
                Executor = controller;
            }
            public bool IsDone() => isCannonUsed;

            private void OnCannonUsed() => isCannonUsed = true;
        }
        private class UnitRepairJob : IUnitJob
        {
            public IUnitController Executor { get; private set; }

            private readonly ShipHole hole;

            public UnitRepairJob(ShipHole hole)
            {
                this.hole = hole;
            }

            public void Execute(IUnitController controller)
            {
                controller.Repair(hole);
                Executor = controller;
            }
            public bool IsDone() => hole.IsFixed;
        }
        private class UnitAttackJob : IUnitJob
        {
            public IUnitController Executor { get; private set; }

            private readonly IEnemyController enemy;

            public UnitAttackJob(IEnemyController enemy)
            {
                this.enemy = enemy;
            }
            public void Execute(IUnitController controller)
            {
                controller.Attack(enemy);
                Executor = controller;
            }
            public bool IsDone() => enemy.IsAlive() == false;
        }
        private interface IUnitJob
        {
            IUnitController Executor { get; }
            void Execute(IUnitController controller);
            bool IsDone();
        }
        private interface ICannonJob
        {
            Cannon Cannon { get; }
        }
        private interface IDisposableJob
        {
            void Dispose();
        }
    }
}
