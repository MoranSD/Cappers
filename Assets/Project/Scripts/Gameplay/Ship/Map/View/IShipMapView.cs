﻿using System;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Ship.Map.View
{
    public interface IShipMapView
    {
        event Action<int> OnSelectLocation;
        event Action OnTryToClose;

        IInteractor Interactor { get; }

        Transform GetCameraInteractPivot();
        void UpdateLocationsVisibility(params int[] ids);
    }
}
