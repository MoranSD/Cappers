using Gameplay.Game;
using Gameplay.Ship.Map.View;
using Infrastructure.Panels;
using Infrastructure.Travel;
using System.Linq;

namespace Gameplay.Ship.Map
{
    public class ShipMap
    {
        private readonly GameData gameData;
        private readonly TravelSystem travelSystem;
        private readonly IShipMapView view;
        private readonly PanelsManager panelsManager;

        public ShipMap(GameData gameData, TravelSystem travelSystem, IShipMapView view, PanelsManager panelsManager)
        {
            this.gameData = gameData;
            this.travelSystem = travelSystem;
            this.view = view;
            this.panelsManager = panelsManager;
        }

        public void Initialize()
        {
            view.OnSelectLocation += OnSelectLocation;
            view.OnPlayerInteract += OnPlayerInteractWithMap;
            view.OnTryToClose += OnViewTryToClose;
        }

        public void Dispose()
        {
            view.OnSelectLocation -= OnSelectLocation;
            view.OnPlayerInteract -= OnPlayerInteractWithMap;
            view.OnTryToClose -= OnViewTryToClose;
        }

        private void OnSelectLocation(int mapId)
        {
            if (panelsManager.ActivePanel != PanelType.shipMap)
                throw new System.Exception();

            travelSystem.BeginTravel(mapId);
            panelsManager.ShowDefault();
        }

        private void OnPlayerInteractWithMap()
        {
            if (panelsManager.ActivePanel == PanelType.shipMap)
                throw new System.Exception();

            var openedLocationsIds = gameData.OpenedLocations.ToArray();
            view.UpdateLocationsVisibility(openedLocationsIds);

            panelsManager.ShowPanel(PanelType.shipMap);
        }

        private void OnViewTryToClose()
        {
            if (panelsManager.ActivePanel != PanelType.shipMap)
                throw new System.Exception();

            panelsManager.ShowDefault();
        }
    }
}
