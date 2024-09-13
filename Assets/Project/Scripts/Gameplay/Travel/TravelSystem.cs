using Gameplay.Game;
using Gameplay.LevelLoad;
using Infrastructure.Routine;
using System.Collections;
using UnityEngine;

namespace Gameplay.Travel
{
    public class TravelSystem
    {
        public int DestinationLocationId { get; private set; }
        public bool IsTraveling { get; private set; }
        public int TravelTimer { get; private set; }

        private readonly GameState gameState;
        private readonly ILevelLoadService levelLoadService;
        private readonly ICoroutineRunner coroutineRunner;

        public TravelSystem(GameState gameState, ILevelLoadService levelLoadService, ICoroutineRunner coroutineRunner)
        {
            this.gameState = gameState;
            this.levelLoadService = levelLoadService;
            this.coroutineRunner = coroutineRunner;
        }

        public void BeginTravel(int locationId)
        {
            if (IsTraveling)
                throw new System.Exception();

            if (gameState.OpenedLocations.Contains(locationId) == false)
                throw new System.Exception(locationId.ToString());

            DestinationLocationId = locationId;
            IsTraveling = true;

            coroutineRunner.StartCoroutine(TravelProcess(locationId));
        }

        private IEnumerator TravelProcess(int locationId)
        {
            if (gameState.IsInSea == false)
            {
                yield return TimerProcess(3);
                levelLoadService.LoadLocation(GameConstants.SeaLocationId);
                yield return new WaitWhile(() => levelLoadService.IsLoading);
            }

            yield return TimerProcess(3);

            levelLoadService.LoadLocation(locationId);
            yield return new WaitWhile(() => levelLoadService.IsLoading);

            gameState.CurrentLocationId = locationId; 
            IsTraveling = false;
        }

        private IEnumerator TimerProcess(int duration)
        {
            TravelTimer = duration;
            var timerWaiter = new WaitForSeconds(1);

            while (TravelTimer > 0)
            {
                yield return timerWaiter;
                TravelTimer--;
            }
        }
    }
}
