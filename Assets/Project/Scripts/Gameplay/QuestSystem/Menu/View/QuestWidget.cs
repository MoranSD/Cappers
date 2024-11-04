using Gameplay.QuestSystem.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.QuestSystem.Menu.View
{
    public class QuestWidget : MonoBehaviour
    {
        [HideInInspector] public QuestData QuestData;

        public Button InteractButton;
        public TextMeshProUGUI InteractButtonText;

        public TextMeshProUGUI QuestNameText;
        public TextMeshProUGUI QuestDescriptionText;
    }
}
