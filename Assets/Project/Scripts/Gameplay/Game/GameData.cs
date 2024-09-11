using Infrastructure;
using System.Collections.Generic;
using World;

namespace Gameplay.Game
{
    public class GameData
    {
        /*
         * основная инфа о мире, по сути, это прогресс игрока, тут будет храниться все
         * текущий мир
         * текущая локация в которой находится игрок
         * открытые локации на карте
         * квесты всех видов (взятые, выполненные)
         * уровень прокачки игрока
         * 
         * в общем прогресс в игре
         */

        public GameWorld World;
        public bool IsInSea => CurrentLocationId == Constants.SeaLocationId;
        public int CurrentLocationId;
        public List<int> OpenedLocations;

        public GameData()
        {
            OpenedLocations = new List<int>();
        }
    }
}
