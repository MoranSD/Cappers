﻿using Cysharp.Threading.Tasks;
using Gameplay.Panels;
using Gameplay.UnitSystem.Buy.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Buy.Menu.View
{
    public class UnitBuyMenuView : MonoBehaviour, IUnitBuyMenuView, IPanel
    {
        public PanelType Type => PanelType.unitBuy;

        public event Action OnTryToClose;
        public event Action OnPlayerInteract;
        public event Action<int> OnSelectUnitToBuy;

        [SerializeField] private GameObject panel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Transform buyCardPivot;
        [SerializeField] private UnitBuyCard cardPrefab;
        [SerializeField] private TriggerInteractor triggerInteractor;

        private UnitBuyCard[] activeCards;

        public void Initialize()
        {
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
            triggerInteractor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            closeButton.onClick.RemoveAllListeners();

            if (activeCards != null)
            {
                foreach (var card in activeCards)
                    card.BuyButton.onClick.RemoveAllListeners();
            }

            triggerInteractor.OnInteracted -= OnInteract;
        }

        public void DrawBuyItems(UnitToBuyData[] dtos)
        {
            if(activeCards != null)
            {
                foreach (var card in activeCards)
                {
                    card.BuyButton.onClick.RemoveAllListeners();
                    Destroy(card.gameObject);
                }
            }

            activeCards = new UnitBuyCard[dtos.Length];
            for (int i = 0; i < dtos.Length; i++)
            {
                var dto = dtos[i];
                var buyCard = Instantiate(cardPrefab, buyCardPivot);

                buyCard.SetIcon(dto.BodyType);
                buyCard.SetStats(dto.Speed, dto.Health, dto.Damage);
                buyCard.SetPrice(dto.Price);
                int buyCardId = dto.Id;
                buyCard.BuyButton.onClick.AddListener(() => OnSelectUnitToBuy?.Invoke(buyCardId));

                activeCards[i] = buyCard;
            }
        }

        public void DrawSoldResult(int unitId, bool success)
        {
            //Debug.Log($"{unitId} {success}");
        }

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
