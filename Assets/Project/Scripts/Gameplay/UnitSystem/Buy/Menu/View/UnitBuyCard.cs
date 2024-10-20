using Gameplay.UnitSystem.Data;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UnitSystem.Buy.Menu.View
{
    public class UnitBuyCard : MonoBehaviour
    {
        public Button BuyButton;

        [SerializeField] private TextMeshProUGUI buyButtonText;
        [Space]
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI damageText;
        [Space]
        [SerializeField] private Image unitIcon;
        [SerializeField] private UnitBodyTypeSprite[] iconSprites;

        public void SetIcon(UnitBodyType bodyType)
        {
            var sprite = iconSprites.First(x => x.BodyType == bodyType).Sprite;
            unitIcon.sprite = sprite;
        }

        public void SetStats(float speed, float health, float damage)
        {
            speedText.text = $"speed {speed}";
            healthText.text = $"health {health}";
            damageText.text = $"damage {damage}";
        }

        public void SetPrice(int price) => buyButtonText.text = $"{price}$";

        [Serializable]
        private struct UnitBodyTypeSprite
        {
            public UnitBodyType BodyType;
            public Sprite Sprite;
        }
    }
}
