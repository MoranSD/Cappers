using System.Collections.Generic;
using Gameplay.QuestSystem;
using Gameplay.QuestSystem.Quests;
using Gameplay.World;

namespace Gameplay.Game
{
    public class GameState
    {
        public GameWorld World;

        public bool IsInSea => CurrentLocationId == GameConstants.SeaLocationId;
        public int CurrentLocationId;
        //locationd id
        public List<int> OpenedLocations;
        //location id - quest id
        public List<QuestData> CurrentQuests;
        public List<QuestData> CompletedQuests;

        public GameState()
        {
            OpenedLocations = new();
            CurrentQuests = new();
            CompletedQuests = new();
        }
    }
}
