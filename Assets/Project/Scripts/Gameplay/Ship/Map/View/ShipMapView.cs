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

        [SerializeField] private GameObject panelObject;
        [SerializeField] private Button closeButton;
        [Space]
        [SerializeField] private PlayerTriggerInteractor interactor;

        public void Initialize()//сюда по идее передать сущность, в которой будут все иконки
        {
            panelObject.SetActive(false);
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
            interactor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            closeButton.onClick.RemoveAllListeners();
            interactor.OnInteracted -= OnInteract;
        }

        public void UpdateLocationsVisibility(params int[] ids)
        {
            //тут мы просто обновим видимость иконок
        }

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
    }
}
