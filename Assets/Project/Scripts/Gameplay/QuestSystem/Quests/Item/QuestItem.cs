using QuestSystem.Quests.Item.View;
using System;
using UnityEngine;

namespace QuestSystem.Quests.Item
{
    public class QuestItem
    {
        private readonly int itemId;
        private readonly IQuestItemView view;

        private bool isTaken;

        public QuestItem(int itemId, IQuestItemView view)//link to ship inventory
        {
            this.itemId = itemId;
            this.view = view;
        }

        public void Initialize()
        {
            view.Interactor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            view.Interactor.OnInteracted -= OnInteract;
        }

        private void OnInteract()
        {
            if(isTaken)
                throw new Exception(itemId.ToString());

            Debug.Log($"Item {itemId} taken");
            isTaken = true;
            view.Destroy();
        }
    }
}
