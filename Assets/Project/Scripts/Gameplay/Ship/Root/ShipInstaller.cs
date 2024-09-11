using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.Ship.Map;
using Gameplay.Ship.Map.View;
using Gameplay.Ship.Map.View.IconsHolder;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.Panels;
using Infrastructure.Travel;
using System.Collections;
using UnityEngine;
using World.Data;

namespace Gameplay.Ship.Root
{
    public class ShipInstaller : Installer
    {
        [SerializeField] private ShipMapView mapView;

        private PanelsManager panelsManager;
        private TemporaryGameplayPanel gameplayTemporary;
        private ShipMap shipMap;
        private MapIconsHolder iconsHolder;

        public override void Initialize()
        {
            panelsManager = ServiceLocator.Get<PanelsManager>();
            gameplayTemporary = new TemporaryGameplayPanel();
            panelsManager.RegisterPanel(gameplayTemporary);
            panelsManager.RegisterPanel(mapView);

            var gameData = ServiceLocator.Get<GameData>();
            var travelSystem = ServiceLocator.Get<TravelSystem>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var playerInteractor = ServiceLocator.Get<PlayerMenuInteractController>();

            shipMap = new ShipMap(gameData, travelSystem, mapView, playerInteractor);

            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameData.World.Id);
            var iconsHolderPrefab = currentWorldConfig.MapIconsHolderPrefab;

            iconsHolder = Instantiate(iconsHolderPrefab, mapView.IconsHolderPivot);
            iconsHolder.Initialize(currentWorldConfig);

            mapView.Initialize(iconsHolder);
            shipMap.Initialize();
        }

        public override void Dispose()
        {
            shipMap.Dispose();

            panelsManager.UnregisterPanel(gameplayTemporary);
            panelsManager.UnregisterPanel(mapView);
            mapView.Dispose();

            iconsHolder.Dispose();
        }
    }

    public class TemporaryGameplayPanel : IPanel
    {
        public PanelType Type => PanelType.gameplay;

        public IEnumerator Hide()
        {
            yield break;
        }

        public IEnumerator Show()
        {
            yield break;
        }
    }
}
