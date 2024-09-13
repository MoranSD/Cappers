using Gameplay.Game;
using Gameplay.World.Data;

namespace Gameplay.QuestSystem.Quests.Factory
{
    public class QuestFactory : IQuestFactory
    {
        private readonly AllWorldsConfig allWorldsConfig;
        private readonly GameState gameState;

        public QuestFactory(AllWorldsConfig allWorldsConfig, GameState gameState)
        {
            this.allWorldsConfig = allWorldsConfig;
            this.gameState = gameState;
        }

        public Quest CreateQuest(QuestData questData)
        {
            var worldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);
            //тут тоже надо будет заменить (см уточнение в QuestManager GetAvailableLocationQuests)
            var locationConfig = (PortLocationConfig)worldConfig.GetLocationConfig(questData.OwnerLocationId);

            return locationConfig.GetQuestConfig(questData.QuestId).CreateQuest();
        }
    }
}
