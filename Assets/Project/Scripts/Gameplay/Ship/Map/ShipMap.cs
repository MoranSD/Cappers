using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.Ship.Map.View;
using Gameplay.Panels;
using Gameplay.Travel;
using Utils.Interaction;

namespace Gameplay.Ship.Map
{
    public class ShipMap
    {
        private readonly GameState gameState;
        private readonly TravelSystem travelSystem;
        private readonly IShipMapView view;
        private readonly PlayerInteractController playerMenuInteract;

        public ShipMap(GameState gameState, TravelSystem travelSystem, IShipMapView view, PlayerInteractController playerMenuInteract)
        {
            this.gameState = gameState;
            this.travelSystem = travelSystem;
            this.view = view;
            this.playerMenuInteract = playerMenuInteract;
        }

        public void Initialize()
        {
            view.OnSelectLocation += OnSelectLocation;
            view.Interactor.OnInteracted += OnPlayerInteractWithMap;
            view.OnTryToClose += OnViewTryToClose;
        }

        public void Dispose()
        {
            view.OnSelectLocation -= OnSelectLocation;
            view.Interactor.OnInteracted -= OnPlayerInteractWithMap;
            view.OnTryToClose -= OnViewTryToClose;
        }

        private void OnSelectLocation(int mapId)
        {
            if (playerMenuInteract.CheckInteraction(PanelType.shipMap) == false)
                throw new System.Exception();

            travelSystem.BeginTravel(mapId);
            playerMenuInteract.ExitInteractState();
        }

        private void OnPlayerInteractWithMap()
        {
            if (playerMenuInteract.CheckInteraction(PanelType.shipMap))
                throw new System.Exception();

            var openedLocationsIds = gameState.OpenedLocations.ToArray();
            view.UpdateLocationsVisibility(openedLocationsIds);

            playerMenuInteract.EnterInteractState(PanelType.shipMap, view.GetCameraInteractPivot());
        }

        private void OnViewTryToClose()
        {
            if (playerMenuInteract.CheckInteraction(PanelType.shipMap) == false)
                throw new System.Exception();

            playerMenuInteract.ExitInteractState();
        }
    }
}
