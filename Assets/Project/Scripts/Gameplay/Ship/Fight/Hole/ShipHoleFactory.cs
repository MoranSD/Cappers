using Gameplay.Ship.Fight.View;
using Infrastructure.TickManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Ship.Fight.Hole
{
    public class ShipHoleFactory : IShipHoleFactory
    {
        private readonly TickManager tickManager;
        private readonly ShipHoleView viewPrefab;
        private readonly ShipFightView shipFightView;

        private List<ShipHole> createdHoles;

        public ShipHoleFactory(TickManager tickManager, ShipHoleView viewPrefab, ShipFightView shipFightView)
        {
            this.tickManager = tickManager;
            this.viewPrefab = viewPrefab;
            this.shipFightView = shipFightView;
            createdHoles = new List<ShipHole>();
        }

        public void Dispose()
        {
            foreach (var hole in createdHoles)
            {
                hole.OnFixed -= OnHoleFixed;
                tickManager.Remove(hole);
                hole.Dispose();
            }
        }

        public ShipHole CreateHoleInZone(int zoneId, ShipFight shipFight)
        {
            var spawnPosition = shipFightView.GetHolePositionInZone(zoneId);
            var view = Object.Instantiate(viewPrefab, spawnPosition, Quaternion.identity);
            var hole = new ShipHole(view, shipFight);

            hole.OnFixed += OnHoleFixed;
            hole.Initialize();
            tickManager.Add(hole);
            createdHoles.Add(hole);

            return hole;
        }

        private void OnHoleFixed()
        {
            var fixedHole = createdHoles.First(x => x.IsFixed);

            fixedHole.OnFixed -= OnHoleFixed;
            tickManager.Remove(fixedHole);
            createdHoles.Remove(fixedHole);
            fixedHole.Dispose();
        }
    }
}
