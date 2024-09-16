using UnityEngine;
using Utils.Interaction;

namespace QuestSystem.Quests.Item.View
{
    public class QuestItemView : MonoBehaviour, IQuestItemView
    {
        public IInteractor Interactor => interactor;

        [SerializeField] private TriggerInteractor interactor;

        public void Destroy() => Destroy(gameObject);
    }
}
