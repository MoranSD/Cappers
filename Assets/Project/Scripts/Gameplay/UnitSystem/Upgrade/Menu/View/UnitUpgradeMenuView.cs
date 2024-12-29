using Cysharp.Threading.Tasks;
using Gameplay.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Upgrade.Menu
{
    public class UnitUpgradeMenuView : MonoBehaviour, IUnitUpgradeMenuView, IPanel
    {
        public event Action OnTryToClose;
        public event Action OnPlayerInteract;
        public event Action<int> OnHeal;
        public event Action<int> OnUpgrade;

        public PanelType Type => PanelType.unitUpgrade;

        [SerializeField] private GameObject panel;
        [SerializeField] private Button closeButton;
        [SerializeField] private TriggerInteractor triggerInteractor;
        [SerializeField] private Transform cardsPivot;
        [SerializeField] private UnitUpgradeCard cardPrefab;

        private List<UnitUpgradeCard> activeCards;

        public void Initialize()
        {
            activeCards = new();
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
            triggerInteractor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            closeButton.onClick.RemoveAllListeners();

            foreach (var card in activeCards)
                card.BuyButton.onClick.RemoveAllListeners();

            triggerInteractor.OnInteracted -= OnInteract;
        }

        public void ClearItems()
        {
            foreach (var card in activeCards)
            {
                card.BuyButton.onClick.RemoveAllListeners();
                Destroy(card.gameObject);
            }

            activeCards.Clear();
        }

        public void DrawHealFailed(int id) => activeCards.First(x => x.Id == id).DrawHealFailed();

        public void DrawHealItem(UnitToHealData data)
        {
            var card = Instantiate(cardPrefab, cardsPivot);
            activeCards.Add(card);
            card.DrawHealState(data);
            int unitId = data.Id;
            card.BuyButton.onClick.AddListener(() => OnHeal?.Invoke(unitId));
        }

        public void DrawHealSuccess(UnitToUpgradeData data) => activeCards.First(x => x.Id == data.Id).DrawHealSuccess(data);

        public void DrawUpgradeFailed(int id) => activeCards.First(x => x.Id == id).DrawUpgradeFailed();

        public void DrawUpgradeItem(UnitToUpgradeData data)
        {
            var card = Instantiate(cardPrefab, cardsPivot);
            activeCards.Add(card);
            card.DrawUpgradeState(data);
            int unitId = data.Id;
            card.BuyButton.onClick.AddListener(() => OnUpgrade?.Invoke(unitId));
        }

        public void DrawUpgradeSuccess(UnitToUpgradeData data) => activeCards.First(x => x.Id == data.Id).DrawUpgradeSuccess(data);

        public async UniTask Show(CancellationToken token)
        {
            panel.SetActive(true);
            await UniTask.Delay(0);
        }

        public async UniTask Hide(CancellationToken token)
        {
            panel.SetActive(false);
            await UniTask.Delay(0);
        }
        private void OnInteract() => OnPlayerInteract?.Invoke();
    }
}
