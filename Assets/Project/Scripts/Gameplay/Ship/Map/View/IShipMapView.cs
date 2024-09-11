using System;

namespace Gameplay.Ship.Map.View
{
    public interface IShipMapView
    {
        event Action<int> OnSelectLocation;
        event Action OnTryToClose;

        event Action OnPlayerInteract;

        void UpdateLocationsVisibility(params int[] ids);
    }
}
