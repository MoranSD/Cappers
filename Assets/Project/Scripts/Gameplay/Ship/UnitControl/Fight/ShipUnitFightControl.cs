using Gameplay.EnemySystem;
using Gameplay.SeaFight;
using Gameplay.Ship.Fight;
using Gameplay.Ship.Fight.Cannon;
using Gameplay.Ship.Fight.Hole;
using Gameplay.UnitSystem.Controller;
using Infrastructure.TickManagement;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Ship.UnitControl.Fight
{
    public class ShipUnitFightControl : ITickable
    {
        private List<IUnitJob> jobList;
        private List<IUnitController> freeUnits;
        private List<UnitWithJob> busyUnits;

        private readonly ShipFight shipFight;
        private readonly SeaFightSystem fightSystem;
        private readonly ShipUnitExistenceControl unitExistenceControl;
        private readonly List<Cannon> activeCannons;

        private const float setCannonJobRate = 10;
        private float setCannonJobTime = 0;

        public ShipUnitFightControl(ShipFight shipFight, SeaFightSystem fightSystem, ShipUnitExistenceControl unitExistenceControl, List<Cannon> activeCannons)
        {
            this.shipFight = shipFight;
            this.fightSystem = fightSystem;
            this.unitExistenceControl = unitExistenceControl;
            this.activeCannons = activeCannons;
            jobList = new();
            freeUnits = new();
            busyUnits = new();
        }

        public void Initialize()
        {
            shipFight.OnGotHole += OnGotHole;
            fightSystem.EnemyShip.OnBoard += OnEnemyBoard;

            freeUnits.AddRange(unitExistenceControl.ActiveUnits);
        }

        public void Dispose()
        {
            shipFight.OnGotHole -= OnGotHole;
            fightSystem.EnemyShip.OnBoard -= OnEnemyBoard;

            foreach (var job in jobList)
                if (job is IDisposableJob disposable)
                    disposable.Dispose();

            foreach (var bu in busyUnits)
                if (bu.Job is IDisposableJob disposable)
                    disposable.Dispose();
        }

        public void Update(float deltaTime)
        {
            UpdateLists();

            if (busyUnits.Count > 0 && fightSystem.IsInFight)
            {
                setCannonJobTime += deltaTime;

                if (setCannonJobTime >= setCannonJobRate)
                {
                    setCannonJobTime -= setCannonJobRate;

                    var availableCannons = activeCannons.Where(x => x.IsAvailable);

                    if(availableCannons.Count() == 0)
                    {
                        UpdateLists();
                        return;
                    }

                    var cannon = availableCannons.ElementAt(new System.Random().Next(availableCannons.Count()));
                    var job = new UnitUseCannonJob(cannon);
                    job.Initialize();

                    OnNewJob(job);
                }
            }
        }

        private void OnGotHole(ShipHole hole) => OnNewJob(new UnitRepairJob(hole));
        private void OnEnemyBoard(IEnemyController enemy) => OnNewJob(new UnitAttackJob(enemy));

        private void OnNewJob(IUnitJob job)
        {
            UpdateLists();

            if (freeUnits.Count > 0) BeginExecuteJob(job);
            else jobList.Add(job);
        }
        private void UpdateLists()
        {
            for (int i = freeUnits.Count - 1; i >= 0; i--)
                if(freeUnits[i].IsAlive() == false)
                    freeUnits.RemoveAt(i);

            for (int i = busyUnits.Count - 1; i >= 0; i--)
            {
                if (busyUnits[i].Job.IsDone())
                {
                    if(busyUnits[i].Unit.IsAlive())
                        freeUnits.Add(busyUnits[i].Unit);

                    busyUnits.RemoveAt(i);
                }
                else if (busyUnits[i].Job.IsFailed())
                {
                    if (busyUnits[i].Unit.IsAlive())
                        freeUnits.Add(busyUnits[i].Unit);

                    jobList.Add(busyUnits[i].Job);
                    busyUnits.RemoveAt(i);
                }
            }

            for (int i = jobList.Count - 1; i >= 0; i--)
            {
                if (jobList[i].IsDone())
                {
                    jobList.RemoveAt(i);
                }
                else if (freeUnits.Count > 0)
                {
                    BeginExecuteJob(jobList[i]);
                    jobList.RemoveAt(i);
                }
            }
        }
        private void BeginExecuteJob(IUnitJob job)
        {
            var freeUnit = freeUnits.First();
            freeUnits.Remove(freeUnit);

            job.Execute(freeUnit);

            busyUnits.Add(new()
            {
                Unit = freeUnit,
                Job = job
            });
        }

        private class UnitUseCannonJob : IUnitJob, IDisposableJob
        {
            private readonly Cannon cannon;
            private IUnitController unit;
            private bool isCannonUsed;

            public UnitUseCannonJob(Cannon cannon)
            {
                this.cannon = cannon;
            }

            public void Initialize()
            {
                cannon.OnUsed += OnCannonUsed;
            }

            public void Dispose()
            {
                cannon.OnUsed -= OnCannonUsed;
            }

            public void Execute(IUnitController controller)
            {
                controller.Use(cannon);
                unit = controller;
            }
            public bool IsFailed() => unit.IsAlive() == false;
            public bool IsDone() => isCannonUsed;

            private void OnCannonUsed() => isCannonUsed = true;
        }
        private class UnitRepairJob : IUnitJob
        {
            private readonly ShipHole hole;
            private IUnitController unit;

            public UnitRepairJob(ShipHole hole)
            {
                this.hole = hole;
            }

            public void Execute(IUnitController controller)
            {
                controller.Repair(hole);
                unit = controller;
            }
            public bool IsFailed() => unit.IsAlive() == false;
            public bool IsDone() => hole.IsFixed;
        }
        private class UnitAttackJob : IUnitJob
        {
            private readonly IEnemyController enemy;
            private IUnitController unit;

            public UnitAttackJob(IEnemyController enemy)
            {
                this.enemy = enemy;
            }
            public void Execute(IUnitController controller)
            {
                controller.Attack(enemy);
                unit = controller;
            }
            public bool IsFailed() => unit.IsAlive() == false;
            public bool IsDone() => enemy.IsAlive() == false;
        }
        private struct UnitWithJob
        {
            public IUnitController Unit;
            public IUnitJob Job;
        }
        private interface IUnitJob
        {
            void Execute(IUnitController controller);
            bool IsFailed();
            bool IsDone();
        }
        private interface IDisposableJob
        {
            void Dispose();
        }
    }
}
