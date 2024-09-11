using Gameplay.Ship.Map.View.IconsHolder;
using Infrastructure.Panels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils.Interaction;

namespace Gameplay.Ship.Map.View
{
    public class ShipMapView : MonoBehaviour, IShipMapView, IPanel
    {
        public event Action<int> OnSelectLocation;
        public event Action OnTryToClose;

        public event Action OnPlayerInteract;

        public PanelType Type => PanelType.shipMap;

        [field: SerializeField] public Transform IconsHolderPivot { get; private set; }

        [SerializeField] private GameObject panelObject;
        [SerializeField] private Button closeButton;
        [Space]
        [SerializeField] private PlayerTriggerInteractor interactor;

        private MapIconsHolder iconsHolder;

        public void Initialize(MapIconsHolder iconsHolder)
        {
            this.iconsHolder = iconsHolder;
            iconsHolder.OnClickOnLocation += OnPressOnLocation;

            panelObject.SetActive(false);
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
            interactor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            iconsHolder.OnClickOnLocation -= OnPressOnLocation;

            closeButton.onClick.RemoveAllListeners();
            interactor.OnInteracted -= OnInteract;
        }

        public void UpdateLocationsVisibility(params int[] ids) => iconsHolder.SetIconsVisibility(ids);

        public IEnumerator Hide()
        {
            panelObject.SetActive(false);
            yield return null;
        }
        public IEnumerator Show()
        {
            panelObject.SetActive(true);
            yield return null;
        }

        private void OnInteract() => OnPlayerInteract?.Invoke();
        private void OnPressOnLocation(int locationId) => OnSelectLocation?.Invoke(locationId);
    }
}
