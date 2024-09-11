using Gameplay.Game;
using Gameplay.LevelLoad;
using Infrastructure.Routine;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Travel
{
    public class TravelSystem
    {
        public int DestinationLocationId { get; private set; }
        public bool IsTraveling { get; private set; }
        public int TravelTimer { get; private set; }

        private readonly GameData gameData;
        private readonly ILevelLoadService levelLoadService;
        private readonly ICoroutineRunner coroutineRunner;

        public TravelSystem(GameData gameData, ILevelLoadService levelLoadService, ICoroutineRunner coroutineRunner)
        {
            this.gameData = gameData;
            this.levelLoadService = levelLoadService;
            this.coroutineRunner = coroutineRunner;
        }

        public void BeginTravel(int locationId)
        {
            if (IsTraveling)
                throw new System.Exception();

            if (gameData.OpenedLocations.Contains(locationId) == false)
                throw new System.Exception(locationId.ToString());

            DestinationLocationId = locationId;
            TravelTimer = 3;
            IsTraveling = true;

            coroutineRunner.StartCoroutine(TravelProcess(locationId));
        }

        private IEnumerator TravelProcess(int locationId)
        {
            if (gameData.IsInSea == false)
            {
                levelLoadService.LoadLocation(Constants.SeaLocationId);
                yield return new WaitWhile(() => levelLoadService.IsLoading);
            }

            yield return TimerProcess();

            levelLoadService.LoadLocation(locationId);
            yield return new WaitWhile(() => levelLoadService.IsLoading);

            IsTraveling = false;
        }

        private IEnumerator TimerProcess()
        {
            var timerWaiter = new WaitForSeconds(1);

            while (TravelTimer > 0)
            {
                yield return timerWaiter;
                TravelTimer--;
            }
        }
    }
}
