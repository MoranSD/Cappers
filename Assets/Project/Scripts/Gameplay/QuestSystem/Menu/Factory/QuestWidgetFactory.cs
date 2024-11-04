using Gameplay.QuestSystem.Menu.View;
using Gameplay.QuestSystem.Quests;
using Gameplay.World.Data;
using UnityEngine;

namespace Gameplay.QuestSystem.Menu.Factory
{
    public class QuestWidgetFactory
    {
        private readonly GameWorldConfig gameWorldConfig;
        private readonly QuestWidget questWidgetPrefab;

        public QuestWidgetFactory(GameWorldConfig gameWorldConfig, QuestWidget questWidgetPrefab)
        {
            this.gameWorldConfig = gameWorldConfig;
            this.questWidgetPrefab = questWidgetPrefab;
        }

        public QuestWidget CreateAvailableWidget(QuestData questData, Transform pivot)
        {
            var widget = CreateWidget(questData, pivot);
            widget.InteractButtonText.text = "Accept";
            return widget;
        }

        public QuestWidget CreateCompleteableWidget(QuestData questData, Transform pivot)
        {
            var widget = CreateWidget(questData, pivot);
            widget.InteractButtonText.text = "Complete";
            return widget;
        }

        public QuestWidget CreateWidget(QuestData questData, Transform pivot)
        {
            var questLocationConfig = (PortLocationConfig)gameWorldConfig.GetLocationConfig(questData.OwnerLocationId);
            var questConfig = questLocationConfig.GetQuestConfig(questData.QuestId);

            var widget = Object.Instantiate(questWidgetPrefab, pivot);

            widget.QuestData = questData;

            widget.QuestNameText.text = questConfig.QuestName;
            widget.QuestDescriptionText.text = questConfig.GetDescription();

            return widget;
        }
    }
}
