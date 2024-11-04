using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;
using Infrastructure.TickManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UnitSystem.Factory
{
    public class UnitFactory : IUnitFactory
    {
        private readonly TickManager tickManager;
        private readonly UnitFactoryConfig config;

        private List<UnitController> createdUnits;

        public UnitFactory(TickManager tickManager, UnitFactoryConfig config)
        {
            this.tickManager = tickManager;
            this.config = config;

            createdUnits = new List<UnitController>();
        }

        public UnitController Create(UnitData unitData, Vector3 position)
        {
            var bodyPrefab = config.GetBody(unitData.BodyType);
            var body = GameObject.Instantiate(bodyPrefab, position, Quaternion.identity);

            var controller = new UnitController(unitData, body);
            controller.Initialize();

            tickManager.Add(controller);

            return controller;
        }

        public void Dispose()
        {
            foreach (var controller in createdUnits)
            {
                tickManager.Remove(controller);
                controller.Dispose();
            }
        }
    }
}
