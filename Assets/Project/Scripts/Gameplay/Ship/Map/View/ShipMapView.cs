using Gameplay.Ship.Map.View.IconsHolder;
using Gameplay.Panels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils.Interaction;
using System.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.Ship.Map.View
{
    public class ShipMapView : MonoBehaviour, IShipMapView, IPanel
    {
        public event Action<int> OnSelectLocation;
        public event Action OnTryToClose;

        public PanelType Type => PanelType.shipMap;
        public IInteractor Interactor => interactor;

        [field: SerializeField] public Transform IconsHolderPivot { get; private set; }

        [SerializeField] private GameObject panelObject;
        [SerializeField] private Button closeButton;
        [Space]
        [SerializeField] private CameraFollowTriggerInteractor interactor;

        private MapIconsHolder iconsHolder;

        public void Initialize(MapIconsHolder iconsHolder)
        {
            this.iconsHolder = iconsHolder;
            iconsHolder.OnClickOnLocation += OnPressOnLocation;

            panelObject.SetActive(false);
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
        }
        public void Dispose()
        {
            iconsHolder.OnClickOnLocation -= OnPressOnLocation;

            closeButton.onClick.RemoveAllListeners();
        }
        public void UpdateLocationsVisibility(params int[] ids) => iconsHolder.SetIconsVisibility(ids);

        public async UniTask Hide(CancellationToken token)
        {
            panelObject.SetActive(false);
            await UniTask.Delay(0);
        }
        public async UniTask Show(CancellationToken token)
        {
            panelObject.SetActive(true);
            await UniTask.Delay(0);
        }

        private void OnPressOnLocation(int locationId) => OnSelectLocation?.Invoke(locationId);
    }
}
