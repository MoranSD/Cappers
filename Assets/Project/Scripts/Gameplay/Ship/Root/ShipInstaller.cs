using Gameplay.Game;
using Gameplay.Ship.Map;
using Gameplay.Ship.Map.View;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.Panels;
using Infrastructure.Travel;
using UnityEngine;

namespace Gameplay.Ship.Root
{
    public class ShipInstaller : Installer
    {
        [SerializeField] private ShipMapView mapView;

        private PanelsManager panelsManager;
        private ShipMap shipMap;

        public override void Initialize()
        {
            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(mapView);

            var gameData = ServiceLocator.Get<GameData>();
            var travelSystem = ServiceLocator.Get<TravelSystem>();

            shipMap = new ShipMap(gameData, travelSystem, mapView, panelsManager);

            mapView.Initialize();
            shipMap.Initialize();
        }

        public override void Dispose()
        {
            shipMap.Dispose();

            panelsManager.UnregisterPanel(mapView);
            mapView.Dispose();
        }
    }
}
