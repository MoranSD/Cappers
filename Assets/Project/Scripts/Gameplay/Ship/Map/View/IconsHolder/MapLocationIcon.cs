using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Ship.Map.View.IconsHolder
{
    public class MapLocationIcon : MonoBehaviour
    {
        [field: SerializeField] public int LocationId { get; private set; }

        public Button SelectButton;
        public TextMeshProUGUI LocationNameText;
        public Image LocationIconImage;
    }
}
