using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.Ship.Map.View;
using Infrastructure.Panels;
using Infrastructure.Travel;
using System.Linq;
using Utils.Interaction;

namespace Gameplay.Ship.Map
{
    public class ShipMap
    {
        private readonly GameData gameData;
        private readonly TravelSystem travelSystem;
        private readonly IShipMapView view;
        private readonly PlayerMenuInteractController playerMenuInteract;

        public ShipMap(GameData gameData, TravelSystem travelSystem, IShipMapView view, PlayerMenuInteractController playerMenuInteract)
        {
            this.gameData = gameData;
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
            if (IsPlayerInteractingWithMap() == false)
                throw new System.Exception();

            travelSystem.BeginTravel(mapId);
            playerMenuInteract.ExitInteractState();
        }

        private void OnPlayerInteractWithMap()
        {
            if (IsPlayerInteractingWithMap())
                throw new System.Exception();

            var openedLocationsIds = gameData.OpenedLocations.ToArray();
            view.UpdateLocationsVisibility(openedLocationsIds);

            playerMenuInteract.EnterInteractState(PanelType.shipMap, 
                view.Interactor is ICameraFollowInteractor cameraInteractor ? cameraInteractor : null);
        }

        private void OnViewTryToClose()
        {
            if (IsPlayerInteractingWithMap() == false)
                throw new System.Exception();

            playerMenuInteract.ExitInteractState();
        }

        private bool IsPlayerInteractingWithMap() =>
            playerMenuInteract.IsInteracting && playerMenuInteract.InteractPanelType == PanelType.shipMap;
    }
}
