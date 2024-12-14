using System.Collections.Generic;
using Gameplay.QuestSystem.Quests;
using Gameplay.Ship.Fight.Cannon.Data;
using Gameplay.UnitSystem.Data;
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
        //ship inventory
        public List<int> QuestItems;
        public List<UnitData> Units;
        //Ship stats
        public float ShipHealth;
        public List<CannonInfo> Cannons;

        public GameState()
        {
            OpenedLocations = new();
            CurrentQuests = new();
            CompletedQuests = new();
            QuestItems = new();
            Units = new();
            Cannons = new();
        }

        public void OpenLocation(int locationId)
        {
            if(OpenedLocations.Contains(locationId)) return;

            OpenedLocations.Add(locationId);
        }
    }
}
