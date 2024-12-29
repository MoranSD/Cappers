using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool HasUnit(int unitId) => Units.Any(x => x.Id == unitId);
        public UnitData GetUnitDataById(int unitId) => Units.First(x => x.Id == unitId);
        public void ReplaceUnitDataByIndex(int index, UnitData unitData) => Units[index] = unitData;
        public void ReplaceUnitDataById(int unitId, UnitData unitData) => Units[GetUnitListIndex(unitId)] = unitData;
        public int GetUnitListIndex(int unitId) => Units.IndexOf(Units.First(x => x.Id == unitId));
        public void ChangeUnitData(int unitId, Func<UnitData, UnitData> func) => ReplaceUnitDataById(unitId, func.Invoke(GetUnitDataById(unitId)));
    }
}
