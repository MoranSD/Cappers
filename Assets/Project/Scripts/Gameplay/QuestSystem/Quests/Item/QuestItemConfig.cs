using QuestSystem.Quests.Item.View;
using UnityEngine;

namespace QuestSystem.Quests.Item
{
    [CreateAssetMenu(menuName = "Quests/QuestItemConfig")]
    public class QuestItemConfig : ScriptableObject
    {
        public string ItemName;
        public QuestItemView ViewPrefab;
        [Space]
        public Vector3 WorldPosition;
        public Quaternion WorldRotation;

        private bool viewPositionSetted;

        private void OnValidate()
        {
            if(viewPositionSetted == false && ViewPrefab != null)
            {
                WorldPosition = ViewPrefab.transform.position;
                WorldRotation = ViewPrefab.transform.rotation;

                viewPositionSetted = true;
            }
        }
    }
}
